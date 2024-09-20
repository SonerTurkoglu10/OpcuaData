using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Opc.UaFx;
using Opc.UaFx.Client;

namespace opc.ver.exe
{
    public partial class Form1 : Form
    {
        private OpcClient _client;
        private Dictionary<string, List<OpcVariableNodeInfo>> _nodeDataByType = new Dictionary<string, List<OpcVariableNodeInfo>>();
        private static readonly string[] ValidDataTypes = { "Boolean", "Int32", "Double", "String", "Byte[]", "DateTime" };
        private Timer _dataFetchTimer;

        public Form1()
        {
            InitializeComponent();

            // ComboBox ve Button ekleme kodları
            comboBoxVeriTurleri.Items.AddRange(ValidDataTypes);
            comboBoxVeriTurleri.SelectedIndexChanged += ComboBoxVeriTurleri_SelectedIndexChanged;

            _dataFetchTimer = new Timer
            {
                Interval = 1000
            };
            _dataFetchTimer.Tick += DataFetchTimer_Tick;
        }

        private void btnVeriCek_Click(object sender, EventArgs e)
        {
            try
            {
                if (_client == null || _client.State != OpcClientState.Connected)
                {
                    _client = new OpcClient("opc.tcp://192.168.251.1:4840");
                    _client.Connect();
                }

                if (_client.State != OpcClientState.Connected)
                {
                    throw new Exception("Sunucuya bağlanılamadı.");
                }

                OpcNodeInfo rootNode = _client.BrowseNode(OpcObjectTypes.ObjectsFolder);
                _nodeDataByType.Clear();
                FetchAllNodeData(rootNode);
                UpdateComboBoxVeriTurleri();
                _dataFetchTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri çekme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataFetchTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                OpcNodeInfo rootNode = _client.BrowseNode(OpcObjectTypes.ObjectsFolder);
                _nodeDataByType.Clear();
                FetchAllNodeData(rootNode);
                UpdateComboBoxVeriTurleri();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yenileme hatası: {ex.Message}\nDetaylar: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FetchAllNodeData(OpcNodeInfo node)
        {
            if (node is OpcVariableNodeInfo variableNode)
            {
                try
                {
                    OpcValue value = _client.ReadNode(variableNode.NodeId);

                    if (value == null || value.Value == null)
                    {
                        return;
                    }

                    string valueType = GetValueType(value.Value);

                    if (!ValidDataTypes.Contains(valueType))
                    {
                        return;
                    }

                    if (!_nodeDataByType.ContainsKey(valueType))
                    {
                        _nodeDataByType[valueType] = new List<OpcVariableNodeInfo>();
                    }

                    _nodeDataByType[valueType].Add(variableNode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Düğüm: {variableNode.DisplayName} - Veri çekme hatası: {ex.Message}");
                }
            }

            foreach (var childNode in node.Children())
            {
                FetchAllNodeData(childNode);
            }
        }

        private string GetValueType(object value)
        {
            if (value is Array array)
            {
                if (array.Length > 0)
                {
                    var firstElement = array.GetValue(0);
                    return GetValueType(firstElement) + "[]";
                }
                return "Byte[]";
            }
            if (value is DateTime) return "DateTime";
            if (value is int) return "Int32";
            if (value is bool) return "Boolean";
            if (value is double) return "Double";
            if (value is string) return "String";
            return "Unknown";
        }

        private void UpdateComboBoxVeriTurleri()
        {
            comboBoxVeriTurleri.Items.Clear();

            foreach (var type in ValidDataTypes)
            {
                if (_nodeDataByType.ContainsKey(type))
                {
                    comboBoxVeriTurleri.Items.Add(type);
                }
            }
        }

        private void ComboBoxVeriTurleri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxVeriTurleri.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir veri türü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedType = comboBoxVeriTurleri.SelectedItem.ToString();
            comboBoxNodeID.Items.Clear();

            if (_nodeDataByType.ContainsKey(selectedType))
            {
                foreach (var node in _nodeDataByType[selectedType])
                {
                    try
                    {
                        OpcValue value = _client.ReadNode(node.NodeId);
                        string valueType = GetValueType(value.Value);

                        if (valueType == selectedType)
                        {
                            if (value.Value is Array array)
                            {
                                string displayValue = $"Array ({array.Length} items)";
                                var details = array.Cast<object>()
                                                    .Select((item, index) => $"{index}: {item}")
                                                    .ToList();
                                comboBoxNodeID.Items.Add($"{node.DisplayName}: {displayValue}");
                                foreach (var detail in details)
                                {
                                    comboBoxNodeID.Items.Add($"    {detail}");
                                }
                            }
                            else
                            {
                                string displayValue = value.Value.ToString();
                                comboBoxNodeID.Items.Add($"{node.DisplayName}: {displayValue}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Düğüm: {node.DisplayName} - Veri okuma hatası: {ex.Message}");
                    }
                }
            }
        }

        private string ExtractNodeName(string comboBoxText)
        {
            if (comboBoxText.Contains(":"))
            {
                return comboBoxText.Split(':')[0].Trim();
            }
            return comboBoxText;
        }

        private OpcNodeId GetNodeIdByName(string selectedNodeName)
        {
            foreach (var typeNodes in _nodeDataByType.Values)
            {
                var node = typeNodes.FirstOrDefault(n => string.Equals(n.DisplayName, selectedNodeName, StringComparison.OrdinalIgnoreCase));
                if (node != null)
                {
                    MessageBox.Show($"Bulunan NodeId: {node.NodeId}");
                    return node.NodeId;
                }
            }
            MessageBox.Show("NodeId bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (comboBoxNodeID.SelectedItem == null)
            {
                MessageBox.Show("Lütfen güncellenecek düğümü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedNodeText = comboBoxNodeID.SelectedItem.ToString();
            string selectedNodeName = ExtractNodeName(selectedNodeText);
            OpcNodeId nodeId = GetNodeIdByName(selectedNodeName);
            if (nodeId == null)
            {
                MessageBox.Show("Geçersiz düğüm seçimi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TextBox textBoxVeriGirisi = this.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "textBoxVeriGirisi");
            if (textBoxVeriGirisi == null || !textBoxVeriGirisi.Visible || !textBoxVeriGirisi.Enabled || string.IsNullOrEmpty(textBoxVeriGirisi.Text))
            {
                MessageBox.Show("Lütfen yeni bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                OpcValue currentNodeValue = _client.ReadNode(nodeId);
                if (currentNodeValue == null)
                {
                    MessageBox.Show("Düğüm değeri okunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string currentType = GetValueType(currentNodeValue.Value);
                object newValue = ConvertToType(textBoxVeriGirisi.Text, currentType);

                if (newValue == null)
                {
                    MessageBox.Show("Girilen değer uygun değil.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var writeResult = _client.WriteNode(nodeId, newValue);
                if (writeResult.IsGood)
                {
                    MessageBox.Show("Değer başarıyla güncellendi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Değer güncelleme hatası: {writeResult.Code} - {writeResult.Description}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Değer güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private object ConvertToType(string inputValue, string targetType)
        {
            switch (targetType)
            {
                case "Boolean":
                    return bool.Parse(inputValue);
                case "Int32":
                    return int.Parse(inputValue);
                case "Double":
                    return double.Parse(inputValue);
                case "String":
                    return inputValue;
                case "DateTime":
                    return DateTime.Parse(inputValue);
                case "Byte[]":
                    try
                    {
                        return Convert.FromBase64String(inputValue); // Base64 ile dönüştürme
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("Byte[] dönüştürme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                default:
                    throw new ArgumentException($"Bilinmeyen veri türü: {targetType}");
            }
        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
            try
            {
                // Eğer timer varsa durdur
                if (_dataFetchTimer != null)
                {
                    _dataFetchTimer.Stop();
                    MessageBox.Show("Veri çekme işlemi durduruldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri çekme durdurma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Formu kapat
            this.Close();
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            // TextBox bileşenlerini temizle
            foreach (var control in this.Controls.OfType<TextBox>())
            {
                control.Text = string.Empty;
            }

            // ComboBox bileşenlerini temizle
            foreach (var control in this.Controls.OfType<ComboBox>())
            {
                control.SelectedIndex = -1; // Seçili öğeyi kaldır
            }
        }

    }
}

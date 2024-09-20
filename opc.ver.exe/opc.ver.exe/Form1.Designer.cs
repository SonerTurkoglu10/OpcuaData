namespace opc.ver.exe
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnVeriCek = new System.Windows.Forms.Button();
            this.btncikis = new System.Windows.Forms.Button();
            this.comboBoxVeriTurleri = new System.Windows.Forms.ComboBox();
            this.comboBoxNodeID = new System.Windows.Forms.ComboBox();
            this.btnDurdur = new System.Windows.Forms.Button();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.buttonTemizle = new System.Windows.Forms.Button();
            this.textBoxVeriGirisi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnVeriCek
            // 
            this.btnVeriCek.Location = new System.Drawing.Point(67, 165);
            this.btnVeriCek.Name = "btnVeriCek";
            this.btnVeriCek.Size = new System.Drawing.Size(57, 23);
            this.btnVeriCek.TabIndex = 0;
            this.btnVeriCek.Text = "Veri çek";
            this.btnVeriCek.UseVisualStyleBackColor = true;
            this.btnVeriCek.Click += new System.EventHandler(this.btnVeriCek_Click);
            // 
            // btncikis
            // 
            this.btncikis.Location = new System.Drawing.Point(291, 165);
            this.btncikis.Name = "btncikis";
            this.btncikis.Size = new System.Drawing.Size(57, 23);
            this.btncikis.TabIndex = 1;
            this.btncikis.Text = "Çık";
            this.btncikis.UseVisualStyleBackColor = true;
            this.btncikis.Click += new System.EventHandler(this.btncikis_Click);
            // 
            // comboBoxVeriTurleri
            // 
            this.comboBoxVeriTurleri.FormattingEnabled = true;
            this.comboBoxVeriTurleri.Location = new System.Drawing.Point(66, 77);
            this.comboBoxVeriTurleri.Name = "comboBoxVeriTurleri";
            this.comboBoxVeriTurleri.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVeriTurleri.TabIndex = 3;
            // 
            // comboBoxNodeID
            // 
            this.comboBoxNodeID.FormattingEnabled = true;
            this.comboBoxNodeID.Location = new System.Drawing.Point(67, 128);
            this.comboBoxNodeID.Name = "comboBoxNodeID";
            this.comboBoxNodeID.Size = new System.Drawing.Size(121, 21);
            this.comboBoxNodeID.TabIndex = 4;
            // 
            // btnDurdur
            // 
            this.btnDurdur.Location = new System.Drawing.Point(131, 165);
            this.btnDurdur.Name = "btnDurdur";
            this.btnDurdur.Size = new System.Drawing.Size(57, 23);
            this.btnDurdur.TabIndex = 6;
            this.btnDurdur.Text = "Durdur";
            this.btnDurdur.UseVisualStyleBackColor = true;
            this.btnDurdur.Click += new System.EventHandler(this.btnDurdur_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Location = new System.Drawing.Point(291, 128);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(57, 23);
            this.btnGuncelle.TabIndex = 7;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click_1);
            // 
            // buttonTemizle
            // 
            this.buttonTemizle.Location = new System.Drawing.Point(217, 165);
            this.buttonTemizle.Name = "buttonTemizle";
            this.buttonTemizle.Size = new System.Drawing.Size(57, 23);
            this.buttonTemizle.TabIndex = 9;
            this.buttonTemizle.Text = "Temizle ";
            this.buttonTemizle.UseVisualStyleBackColor = true;
            this.buttonTemizle.Click += new System.EventHandler(this.buttonTemizle_Click);
            // 
            // textBoxVeriGirisi
            // 
            this.textBoxVeriGirisi.Location = new System.Drawing.Point(228, 78);
            this.textBoxVeriGirisi.Name = "textBoxVeriGirisi";
            this.textBoxVeriGirisi.Size = new System.Drawing.Size(120, 20);
            this.textBoxVeriGirisi.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "OPC UA VERİ ÇEKME VE İŞLEME";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 270);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxVeriGirisi);
            this.Controls.Add(this.buttonTemizle);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.btnDurdur);
            this.Controls.Add(this.comboBoxNodeID);
            this.Controls.Add(this.comboBoxVeriTurleri);
            this.Controls.Add(this.btncikis);
            this.Controls.Add(this.btnVeriCek);
            this.Name = "Form1";
            this.Text = "ELEKTROTEKS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVeriCek;
        private System.Windows.Forms.Button btncikis;
        private System.Windows.Forms.ComboBox comboBoxVeriTurleri;
        private System.Windows.Forms.ComboBox comboBoxNodeID;
        private System.Windows.Forms.Button btnDurdur;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.Button buttonTemizle;
        private System.Windows.Forms.TextBox textBoxVeriGirisi;
        private System.Windows.Forms.Label label1;
    }
}


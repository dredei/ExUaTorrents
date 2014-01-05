namespace ExUa_Torrents
{
    partial class FrmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.cbClearTempFolder = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTorrentClientPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTmpFolder = new System.Windows.Forms.TextBox();
            this.btnSelectTorrentPath = new System.Windows.Forms.Button();
            this.btnSelectTempFolder = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.fbd1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // cbClearTempFolder
            // 
            this.cbClearTempFolder.AutoSize = true;
            this.cbClearTempFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbClearTempFolder.Location = new System.Drawing.Point(3, 1);
            this.cbClearTempFolder.Name = "cbClearTempFolder";
            this.cbClearTempFolder.Size = new System.Drawing.Size(224, 17);
            this.cbClearTempFolder.TabIndex = 0;
            this.cbClearTempFolder.Text = "Очищать временную папку при выходе";
            this.cbClearTempFolder.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Путь к торрент-клиенту:";
            // 
            // tbTorrentClientPath
            // 
            this.tbTorrentClientPath.Location = new System.Drawing.Point(3, 37);
            this.tbTorrentClientPath.Name = "tbTorrentClientPath";
            this.tbTorrentClientPath.Size = new System.Drawing.Size(247, 20);
            this.tbTorrentClientPath.TabIndex = 2;
            this.tbTorrentClientPath.Text = "C:\\Program Files (x86)\\BitTorrent\\BitTorrent.exe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Папка для временных файлов:";
            // 
            // tbTmpFolder
            // 
            this.tbTmpFolder.Location = new System.Drawing.Point(3, 76);
            this.tbTmpFolder.Name = "tbTmpFolder";
            this.tbTmpFolder.Size = new System.Drawing.Size(247, 20);
            this.tbTmpFolder.TabIndex = 4;
            this.tbTmpFolder.Text = "C:\\Torr";
            // 
            // btnSelectTorrentPath
            // 
            this.btnSelectTorrentPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectTorrentPath.Location = new System.Drawing.Point(256, 35);
            this.btnSelectTorrentPath.Name = "btnSelectTorrentPath";
            this.btnSelectTorrentPath.Size = new System.Drawing.Size(30, 23);
            this.btnSelectTorrentPath.TabIndex = 5;
            this.btnSelectTorrentPath.Text = "...";
            this.btnSelectTorrentPath.UseVisualStyleBackColor = true;
            this.btnSelectTorrentPath.Click += new System.EventHandler(this.btnSelectTorrentPath_Click);
            // 
            // btnSelectTempFolder
            // 
            this.btnSelectTempFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectTempFolder.Location = new System.Drawing.Point(256, 74);
            this.btnSelectTempFolder.Name = "btnSelectTempFolder";
            this.btnSelectTempFolder.Size = new System.Drawing.Size(30, 23);
            this.btnSelectTempFolder.TabIndex = 6;
            this.btnSelectTempFolder.Text = "...";
            this.btnSelectTempFolder.UseVisualStyleBackColor = true;
            this.btnSelectTempFolder.Click += new System.EventHandler(this.btnSelectTempFolder_Click);
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Location = new System.Drawing.Point(3, 102);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(135, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(151, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ofd1
            // 
            this.ofd1.FileName = "openFileDialog1";
            this.ofd1.Filter = "BitTorrent.exe|BitTorrent.exe";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(288, 126);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnSelectTempFolder);
            this.Controls.Add(this.btnSelectTorrentPath);
            this.Controls.Add(this.tbTmpFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTorrentClientPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbClearTempFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbClearTempFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTorrentClientPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTmpFolder;
        private System.Windows.Forms.Button btnSelectTorrentPath;
        private System.Windows.Forms.Button btnSelectTempFolder;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.FolderBrowserDialog fbd1;
    }
}
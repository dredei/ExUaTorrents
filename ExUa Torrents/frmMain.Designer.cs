namespace ExUa_Torrents
{
    partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.tbLink = new System.Windows.Forms.TextBox();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.rbTorrents = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelDir = new System.Windows.Forms.Button();
            this.cbTorrentSavePath = new System.Windows.Forms.ComboBox();
            this.fbd1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tmpCheckClipbrd = new System.Windows.Forms.Timer(this.components);
            this.llSite = new System.Windows.Forms.LinkLabel();
            this.llDonate = new System.Windows.Forms.LinkLabel();
            this.ssStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ссылка:";
            // 
            // tbLink
            // 
            this.tbLink.Location = new System.Drawing.Point(3, 16);
            this.tbLink.Name = "tbLink";
            this.tbLink.Size = new System.Drawing.Size(269, 20);
            this.tbLink.TabIndex = 1;
            this.tbLink.TextChanged += new System.EventHandler(this.tbLink_TextChanged);
            this.tbLink.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLink_KeyDown);
            // 
            // lvFiles
            // 
            this.lvFiles.CheckBoxes = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.Location = new System.Drawing.Point(3, 42);
            this.lvFiles.MultiSelect = false;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(269, 173);
            this.lvFiles.TabIndex = 2;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvFiles_ColumnClick);
            this.lvFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvFiles_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Название файла";
            this.columnHeader1.Width = 174;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Размер";
            this.columnHeader2.Width = 71;
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.ssStatus.Location = new System.Drawing.Point(0, 356);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(275, 22);
            this.ssStatus.SizingGrip = false;
            this.ssStatus.TabIndex = 3;
            this.ssStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(19, 17);
            this.toolStripStatusLabel1.Text = "...";
            // 
            // rbTorrents
            // 
            this.rbTorrents.AutoSize = true;
            this.rbTorrents.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbTorrents.Location = new System.Drawing.Point(134, 221);
            this.rbTorrents.Name = "rbTorrents";
            this.rbTorrents.Size = new System.Drawing.Size(138, 17);
            this.rbTorrents.TabIndex = 4;
            this.rbTorrents.Text = "Отображать торренты";
            this.rbTorrents.UseVisualStyleBackColor = true;
            this.rbTorrents.CheckedChanged += new System.EventHandler(this.rbTorrents_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbAll.Location = new System.Drawing.Point(3, 221);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(108, 17);
            this.rbAll.TabIndex = 5;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "Отображать все";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // btnDownload
            // 
            this.btnDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownload.Location = new System.Drawing.Point(3, 284);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(269, 23);
            this.btnDownload.TabIndex = 6;
            this.btnDownload.Tag = "0";
            this.btnDownload.Text = "Получить список файлов";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Location = new System.Drawing.Point(3, 313);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(269, 23);
            this.btnSettings.TabIndex = 7;
            this.btnSettings.Text = "Настройки";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Куда будет сохранять файлы торрент-клиент:";
            // 
            // btnSelDir
            // 
            this.btnSelDir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelDir.Location = new System.Drawing.Point(241, 255);
            this.btnSelDir.Name = "btnSelDir";
            this.btnSelDir.Size = new System.Drawing.Size(31, 23);
            this.btnSelDir.TabIndex = 10;
            this.btnSelDir.Text = "...";
            this.btnSelDir.UseVisualStyleBackColor = true;
            this.btnSelDir.Click += new System.EventHandler(this.btnSelDir_Click);
            // 
            // cbTorrentSavePath
            // 
            this.cbTorrentSavePath.FormattingEnabled = true;
            this.cbTorrentSavePath.Items.AddRange(new object[] {
            "C:\\tmpSave"});
            this.cbTorrentSavePath.Location = new System.Drawing.Point(3, 257);
            this.cbTorrentSavePath.Name = "cbTorrentSavePath";
            this.cbTorrentSavePath.Size = new System.Drawing.Size(232, 21);
            this.cbTorrentSavePath.TabIndex = 11;
            // 
            // tmpCheckClipbrd
            // 
            this.tmpCheckClipbrd.Enabled = true;
            this.tmpCheckClipbrd.Interval = 1000;
            this.tmpCheckClipbrd.Tick += new System.EventHandler(this.tmpCheckClipbrd_Tick);
            // 
            // llSite
            // 
            this.llSite.AutoSize = true;
            this.llSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.llSite.Location = new System.Drawing.Point(237, 339);
            this.llSite.Name = "llSite";
            this.llSite.Size = new System.Drawing.Size(35, 13);
            this.llSite.TabIndex = 12;
            this.llSite.TabStop = true;
            this.llSite.Text = "Сайт";
            this.llSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSite_LinkClicked);
            // 
            // llDonate
            // 
            this.llDonate.AutoSize = true;
            this.llDonate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.llDonate.Location = new System.Drawing.Point(0, 339);
            this.llDonate.Name = "llDonate";
            this.llDonate.Size = new System.Drawing.Size(48, 13);
            this.llDonate.TabIndex = 13;
            this.llDonate.TabStop = true;
            this.llDonate.Text = "Donate";
            this.llDonate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDonate_LinkClicked);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(275, 378);
            this.Controls.Add(this.llDonate);
            this.Controls.Add(this.llSite);
            this.Controls.Add(this.cbTorrentSavePath);
            this.Controls.Add(this.btnSelDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.rbAll);
            this.Controls.Add(this.rbTorrents);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.tbLink);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExUa Torrents";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLink;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.RadioButton rbTorrents;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelDir;
        private System.Windows.Forms.ComboBox cbTorrentSavePath;
        private System.Windows.Forms.FolderBrowserDialog fbd1;
        private System.Windows.Forms.Timer tmpCheckClipbrd;
        private System.Windows.Forms.LinkLabel llSite;
        private System.Windows.Forms.LinkLabel llDonate;
    }
}


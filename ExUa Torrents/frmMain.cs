#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Donate;
using ExtensionMethods;
using Ini;

#endregion

namespace ExUa_Torrents
{
    public partial class FrmMain : Form
    {
        private List<ExUaFile> _files;
        private ExUa _eu;
        private SaveHistory _history;
        private Thread _thr;
        public string TorrentClientPath = "C:\\Program Files (x86)\\BitTorrent\\BitTorrent.exe";
        public string TmpFolderPath = "C:\\tmpExUa";
        public bool ClearTempFolder = false;
        private long _selectedSize;

        public FrmMain()
        {
            this.InitializeComponent();
        }

        private void CheckDonateDll()
        {
            if ( File.Exists( Application.StartupPath + @"\Donate.dll" ) )
            {
                return;
            }
            MessageBox.Show( "Верните Donate.dll!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            Application.Exit();
        }

        private void LoadSettings()
        {
            IniFile ini = new IniFile( Application.StartupPath + @"\Settings.ini" );
            this.TorrentClientPath = ini.Read( "TorrentClientPath", "Options", this.TorrentClientPath );
            this.TmpFolderPath = ini.Read( "TmpFolderPath", "Options", this.TmpFolderPath );
            this.ClearTempFolder = bool.Parse( ini.Read( "ClearTmpFolder", "Options", this.ClearTempFolder.ToString() ) );
        }

        public void SaveSettings()
        {
            IniFile ini = new IniFile( Application.StartupPath + @"\Settings.ini" );
            ini.Write( "TorrentClientPath", this.TorrentClientPath, "Options" );
            ini.Write( "TmpFolderPath", this.TmpFolderPath, "Options" );
            ini.Write( "ClearTmpFolder", this.ClearTempFolder.ToString(), "Options" );
        }

        private void GetPaths()
        {
            this.cbTorrentSavePath.Items.Clear();
            this.cbTorrentSavePath.Items.AddRange( this._history.GetPaths() );
            if ( this.cbTorrentSavePath.Items.Count > 0 )
            {
                this.cbTorrentSavePath.SelectedIndex = 0;
            }
        }

        private void PrintFiles()
        {
            this.lvFiles.Items.Clear();
            foreach ( ExUaFile file in this._files )
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = file.Name;
                lvi.SubItems.Add( ExMethods.getSizeReadable( file.Size ) );
                this.lvFiles.Items.Add( lvi );
            }
        }

        private void ChangeDownloadButtonTag( string tag )
        {
            this.btnDownload.Tag = tag;
            if ( this.btnDownload.Tag.ToString() == "0" )
            {
                this.btnDownload.Text = "Получить список файлов";
            }
            else if ( this.btnDownload.Tag.ToString() == "1" )
            {
                this.btnDownload.Text = "Загрузить";
            }
        }

        private void UpdProgress( object sender, UpdEventArgs e )
        {
            this.ssStatus.Items[ 0 ].Text = "{0} / {1}".f( e.Progress, e.MaxProgress );
        }

        private void Work()
        {
            switch ( this.btnDownload.Tag.ToString() )
            {
                case "0":
                    {
                        if ( string.IsNullOrEmpty( this.tbLink.Text ) || this.tbLink.Text.IndexOf( "ex.ua/" ) < 0 )
                        {
                            MessageBox.Show( "Укажите корректную ссылку!", "Ошибка", MessageBoxButtons.OK,
                                MessageBoxIcon.Error );
                            return;
                        }
                        string torrentSavePath = this.cbTorrentSavePath.Text;
                        this._eu = new ExUa( this.TmpFolderPath, this.TorrentClientPath, torrentSavePath,
                            this.ClearTempFolder );
                        this._eu.UpdEvent += new EventHandler<UpdEventArgs>( this.UpdProgress );
                        this._eu.GetFiles( this.tbLink.Text );
                        this._files = this._eu.GetLocalFiles( this.rbTorrents.Checked );
                        this.PrintFiles();
                        this.ChangeDownloadButtonTag( "1" );
                    }
                    break;

                case "1":
                    if ( !this.rbTorrents.Checked )
                    {
                        this._eu.DownloadFile();
                    }
                    else
                    {
                        if ( string.IsNullOrEmpty( this.cbTorrentSavePath.Text ) )
                        {
                            MessageBox.Show( "Укажите путь для сохранения!", "Ошибка", MessageBoxButtons.OK,
                                MessageBoxIcon.Error );
                            return;
                        }
                        this._eu.DownloadTorrents();
                    }
                    break;
            }
            this.ssStatus.Items[ 0 ].Text = "Завершено!";
        }

        private void btnDownload_Click( object sender, EventArgs e )
        {
            this.CheckDonateDll();
            this._thr = new Thread( this.Work );
            this._thr.Start();
        }

        private void lvFiles_ItemChecked( object sender, ItemCheckedEventArgs e )
        {
            bool check = e.Item.Checked;
            int index = e.Item.Index;
            long arrIndex = this._files[ index ].ArrIndex;
            this._eu.CheckFile( arrIndex, check );
            if ( check )
            {
                this._selectedSize += this._eu.GetFileSize( index );
            }
            else
            {
                this._selectedSize -= this._eu.GetFileSize( index );
            }
            if ( this._selectedSize < 0 )
            {
                this._selectedSize = 0;
            }
            this.ssStatus.Items[ 0 ].Text = "Размер: " + ExMethods.getSizeReadable( this._selectedSize );
        }

        private void tbLink_TextChanged( object sender, EventArgs e )
        {
            this.ChangeDownloadButtonTag( "0" );
        }

        private void lvFiles_ColumnClick( object sender, ColumnClickEventArgs e )
        {
            if ( e.Column == 0 )
            {
                bool check = !this.lvFiles.Items[ 0 ].Checked;
                for ( int i = 0; i < this.lvFiles.Items.Count; i++ )
                {
                    this.lvFiles.Items[ i ].Checked = check;
                }
            }
        }

        private void rbTorrents_CheckedChanged( object sender, EventArgs e )
        {
            this.ChangeDownloadButtonTag( "0" );
        }

        private void rbAll_CheckedChanged( object sender, EventArgs e )
        {
            this.ChangeDownloadButtonTag( "0" );
        }

        private void frmMain_Load( object sender, EventArgs e )
        {
            CheckForIllegalCrossThreadCalls = false;
            this.LoadSettings();
            frmDonate frm = new frmDonate();
            frm.ShowDialog();
            this._history = new SaveHistory();
            this.GetPaths();
        }

        private void btnSettings_Click( object sender, EventArgs e )
        {
            FrmSettings frm = new FrmSettings( this );
            frm.ShowDialog();
        }

        private void btnSelDir_Click( object sender, EventArgs e )
        {
            this.fbd1.SelectedPath = this.cbTorrentSavePath.Text;
            if ( this.fbd1.ShowDialog() == DialogResult.OK )
            {
                this._history.AddPath( this.fbd1.SelectedPath );
                this.GetPaths();
                this.cbTorrentSavePath.Text = this.fbd1.SelectedPath;
            }
        }

        private void frmMain_FormClosed( object sender, FormClosedEventArgs e )
        {
            this._history.saveHistory();
            if ( this._thr != null )
            {
                this._thr.Abort();
            }
            if ( this._eu != null )
            {
                this._eu.ClearTmpFolder();
            }
        }

        private void tmpCheckClipbrd_Tick( object sender, EventArgs e )
        {
            string text = Clipboard.GetText();
            if ( ( text.IndexOf( "ex.ua/" ) >= 0 && this.tbLink.Text != text && !this.tbLink.Focused )
                 || ( string.IsNullOrEmpty( this.tbLink.Text ) && text.IndexOf( "ex.ua/view/" ) >= 0 ) )
            {
                this.tbLink.Text = text;
                this.tbLink.SelectionStart = this.tbLink.TextLength;
            }
        }

        private void tbLink_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter && this.btnDownload.Tag.ToString() == "0" )
            {
                this.btnDownload.PerformClick();
            }
        }

        private void llSite_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            Process.Start( @"http://www.softez.pp.ua/" );
        }

        private void llDonate_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            frmDonate frm = new frmDonate( true );
            frm.ShowDialog();
        }
    }
}
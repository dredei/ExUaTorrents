using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ExtensionMethods;
using Ini;

namespace ExUa_Torrents
{
    public partial class frmMain : Form
    {
        List<ExUaFile> files;
        ExUa eu = null;
        Thread thr = null;
        public string torrentClientPath = "C:\\Program Files (x86)\\BitTorrent\\BitTorrent.exe";
        public string tmpFolderPath = "C:\\tmpExUa";
        public bool clearTempFolder = false;

        public frmMain()
        {
            InitializeComponent();
        }

        public void loadSettings()
        {
            IniFile ini = new IniFile( Application.StartupPath + @"\Settings.ini" );
            torrentClientPath = ini.Read( "TorrentClientPath", "Options", torrentClientPath );
            tmpFolderPath = ini.Read( "TmpFolderPath", "Options", tmpFolderPath );
            clearTempFolder = bool.Parse( ini.Read( "ClearTmpFolder", "Options", clearTempFolder.ToString() ) );
        }

        public void saveSettings()
        {
            IniFile ini = new IniFile( Application.StartupPath + @"\Settings.ini" );
            ini.Write( "TorrentClientPath", torrentClientPath, "Options" );
            ini.Write( "TmpFolderPath", tmpFolderPath, "Options" );
            ini.Write( "ClearTmpFolder", clearTempFolder.ToString(), "Options" );
        }

        private void printFiles()
        {
            lvFiles.Items.Clear();
            for ( int i = 0; i < files.Count; i++ )
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = files[ i ].name;
                lvi.SubItems.Add( AddMethods.hSize( files[ i ].size ) );
                lvFiles.Items.Add( lvi );
            }
        }

        private void changeDownloadButtonTag( string tag )
        {
            btnDownload.Tag = tag;
            if ( btnDownload.Tag.ToString() == "0" )
            {
                btnDownload.Text = "Получить список файлов";
            }
            else if ( btnDownload.Tag.ToString() == "1" )
            {
                btnDownload.Text = "Загрузить";
            }
        }

        private void updProgress( object sender, UpdEventArgs e )
        {
            ssStatus.Items[ 0 ].Text = "{0} / {1}".f( e.progress, e.maxProgress );
        }

        private void work()
        {
            if ( btnDownload.Tag.ToString() == "0" )
            {
                string torrentSavePath = cbTorrentSavePath.Text;
                eu = new ExUa( tmpFolderPath, torrentClientPath, torrentSavePath, clearTempFolder );
                eu.updEvent += new EventHandler<UpdEventArgs>( updProgress );
                eu.getFiles( tbLink.Text );
                files = eu.getLocalFiles( rbTorrents.Checked );
                printFiles();
                changeDownloadButtonTag( "1" );
            }
            else if ( btnDownload.Tag.ToString() == "1" )
            {
                if ( !rbTorrents.Checked )
                {
                    eu.downloadFile();
                }
                else
                {
                    eu.downloadTorrents();
                }
            }
            ssStatus.Items[ 0 ].Text = "Завершено!";
        }

        private void btnDownload_Click( object sender, EventArgs e )
        {
            thr = new Thread( work );
            thr.Start();
        }

        private void lvFiles_ItemChecked( object sender, ItemCheckedEventArgs e )
        {
            bool check = e.Item.Checked;
            int index = e.Item.Index;
            long arrIndex = this.files[ index ].arrIndex;
            eu.checkFile( arrIndex, check );
        }

        private void tbLink_TextChanged( object sender, EventArgs e )
        {
            changeDownloadButtonTag( "0" );
        }

        private void lvFiles_ColumnClick( object sender, ColumnClickEventArgs e )
        {
            if ( e.Column == 0 )
            {
                bool check = !lvFiles.Items[ 0 ].Checked;
                for ( int i = 0; i < lvFiles.Items.Count; i++ )
                {
                    lvFiles.Items[ i ].Checked = check;
                }
            }
        }

        private void rbTorrents_CheckedChanged( object sender, EventArgs e )
        {
            changeDownloadButtonTag( "0" );
        }

        private void rbAll_CheckedChanged( object sender, EventArgs e )
        {
            changeDownloadButtonTag( "0" );
        }

        private void frmMain_Load( object sender, EventArgs e )
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            loadSettings();
            cbTorrentSavePath.SelectedIndex = 0;
        }

        private void btnSettings_Click( object sender, EventArgs e )
        {
            frmSettings frm = new frmSettings( this );
            frm.ShowDialog();
        }

        private void btnSelDir_Click( object sender, EventArgs e )
        {
            fbd1.SelectedPath = cbTorrentSavePath.Text;
            if ( fbd1.ShowDialog() == DialogResult.OK )
            {
                cbTorrentSavePath.Text = fbd1.SelectedPath;
            }
        }

        private void frmMain_FormClosed( object sender, FormClosedEventArgs e )
        {
            if ( thr != null )
            {
                thr.Abort();
            }
            if ( eu != null )
            {
                eu.clearTmpFolder();
            }
        }

        private void tmpCheckClipbrd_Tick( object sender, EventArgs e )
        {
            string text = Clipboard.GetText();
            if ( text.IndexOf( "ex.ua/view/" ) >= 0 && tbLink.Text != text && !tbLink.Focused )
            {
                tbLink.Text = text;
                tbLink.SelectionStart = tbLink.TextLength;
            }
        }

        private void tbLink_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter && btnDownload.Tag.ToString() == "0" )
            {
                btnDownload.PerformClick();
            }
        }
    }
}

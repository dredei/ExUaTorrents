using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExtensionMethods;

namespace ExUa_Torrents
{
    public partial class frmMain : Form
    {
        List<ExUaFile> files;
        ExUa eu = null;
        string torrentClientPath = "C:\\Program Files (x86)\\BitTorrent\\BitTorrent.exe";
        string tmpFolderPath = "C:\\tmpExUa";
        string torrentClient = "BitTorrent";
        string torrentSavePath = "C:\\tmpSave";
        bool clearFolder = false;

        public frmMain()
        {
            InitializeComponent();
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

        private void btnDownload_Click( object sender, EventArgs e )
        {
            if ( btnDownload.Tag.ToString() == "0" )
            {
                eu = new ExUa( tmpFolderPath, torrentClient, torrentClientPath, torrentSavePath, clearFolder );
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
    }
}

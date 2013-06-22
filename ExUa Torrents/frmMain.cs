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

        public frmMain()
        {
            InitializeComponent();
        }

        private void printFiles()
        {
            for ( int i = 0; i < files.Count; i++ )
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = files[ i ].name;
                lvi.SubItems.Add( AddMethods.hSize( files[ i ].size ) );
                lvFiles.Items.Add( lvi );
            }
        }

        private void btnDownload_Click( object sender, EventArgs e )
        {
            if ( btnDownload.Tag.ToString() == "0" )
            {
                eu = new ExUa();
                eu.getFiles( tbLink.Text );
                files = eu.getLocalFiles( rbTorrents.Checked );
                printFiles();
                btnDownload.Tag = "1";
                btnDownload.Text = "Загрузить";
            }
            else if ( btnDownload.Tag.ToString() == "1" )
            {
                if ( !rbTorrents.Checked )
                {
                    eu.downloadFile( @"C:\tmpExUa" );
                }
                /*btnDownload.Tag = "0";
                btnDownload.Text = "Получить список файлов";*/
            }
        }

        private void lvFiles_ItemChecked( object sender, ItemCheckedEventArgs e )
        {
            bool check = e.Item.Checked;
            int index = e.Item.Index;
            long arrIndex = this.files[ index ].arrIndex;
            eu.checkFile( arrIndex, check );
        }
    }
}

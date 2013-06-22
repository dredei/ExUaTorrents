using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExUa_Torrents
{
    public partial class frmSettings : Form
    {
        frmMain MainForm;

        public frmSettings( frmMain MainForm )
        {
            InitializeComponent();
            this.MainForm = MainForm;
        }

        private void getSettings()
        {
            cbClearTempFolder.Checked = MainForm.clearTempFolder;
            tbTorrentClientPath.Text = MainForm.torrentClientPath;
            tbTmpFolder.Text = MainForm.tmpFolderPath;
        }

        private void saveSettings()
        {
            MainForm.clearTempFolder = cbClearTempFolder.Checked;
            MainForm.torrentClientPath = tbTorrentClientPath.Text;
            MainForm.tmpFolderPath = tbTmpFolder.Text;
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void frmSettings_Load( object sender, EventArgs e )
        {
            getSettings();
        }

        private void btnOk_Click( object sender, EventArgs e )
        {
            saveSettings();
            this.Close();
        }

        private void btnSelectTorrentPath_Click( object sender, EventArgs e )
        {
            ofd1.FileName = tbTorrentClientPath.Text;
            if ( ofd1.ShowDialog() == DialogResult.OK )
            {
                tbTorrentClientPath.Text = ofd1.FileName;
            }
        }

        private void btnSelectTempFolder_Click( object sender, EventArgs e )
        {
            fbd1.SelectedPath = tbTmpFolder.Text;
            if ( fbd1.ShowDialog() == DialogResult.OK )
            {
                tbTmpFolder.Text = fbd1.SelectedPath;
            }
        }
    }
}

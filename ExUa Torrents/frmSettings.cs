#region Using

using System;
using System.Windows.Forms;

#endregion

namespace ExUa_Torrents
{
    public partial class FrmSettings : Form
    {
        private readonly FrmMain _mainForm;

        public FrmSettings( FrmMain mainForm )
        {
            this.InitializeComponent();
            this._mainForm = mainForm;
        }

        private void GetSettings()
        {
            this.cbClearTempFolder.Checked = this._mainForm.ClearTempFolder;
            this.tbTorrentClientPath.Text = this._mainForm.TorrentClientPath;
            this.tbTmpFolder.Text = this._mainForm.TmpFolderPath;
        }

        private void SaveSettings()
        {
            this._mainForm.ClearTempFolder = this.cbClearTempFolder.Checked;
            this._mainForm.TorrentClientPath = this.tbTorrentClientPath.Text;
            this._mainForm.TmpFolderPath = this.tbTmpFolder.Text;
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void frmSettings_Load( object sender, EventArgs e )
        {
            this.GetSettings();
        }

        private void btnOk_Click( object sender, EventArgs e )
        {
            this.SaveSettings();
            this._mainForm.SaveSettings();
            this.Close();
        }

        private void btnSelectTorrentPath_Click( object sender, EventArgs e )
        {
            this.ofd1.FileName = this.tbTorrentClientPath.Text;
            if ( this.ofd1.ShowDialog() == DialogResult.OK )
            {
                this.tbTorrentClientPath.Text = this.ofd1.FileName;
            }
        }

        private void btnSelectTempFolder_Click( object sender, EventArgs e )
        {
            this.fbd1.SelectedPath = this.tbTmpFolder.Text;
            if ( this.fbd1.ShowDialog() == DialogResult.OK )
            {
                this.tbTmpFolder.Text = this.fbd1.SelectedPath;
            }
        }
    }
}
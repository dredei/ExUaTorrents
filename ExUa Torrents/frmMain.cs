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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnDownload_Click( object sender, EventArgs e )
        {
            ExUa eu = new ExUa();
            eu.getFiles( tbLink.Text );

        }
    }
}

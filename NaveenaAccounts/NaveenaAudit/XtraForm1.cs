using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NaveenaAccounts
{
    public partial class XtraForm1 : DevExpress.DXperience.Demos.frmMain
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.ShowDialog();
            this.Hide();
        }

        private void XtraForm1_Activated(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
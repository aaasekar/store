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
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        StringBuilder m_sbQueryText = new StringBuilder();
        db dbHandler = new db();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            m_sbQueryText.Length = 0;
            m_sbQueryText.AppendFormat("select id from nexusGarments_Master_Employee  where username='{0}' and password='{1}'", txtUserName.Text.ToString().Trim(), txtPassword.Text.ToString().Trim());
            if (dbHandler.IsExists(m_sbQueryText.ToString()))
            {

                Form1 main = new Form1();
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("select id from  nexusGarments_Master_Employee  where username='{0}' ", txtUserName.Text.ToString().Trim());
                dbHandler.loadUserid(m_sbQueryText.ToString(), Sess.id);
                //XtraMessageBox.Show(main.lblID.Text.ToString());
                //this.Close();
                Sess.id = Convert.ToInt32(Sess.id);

                this.Hide();
                main.ShowDialog();
                this.Close();

            }
            else
            {
                XtraMessageBox.Show("Login Failed", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
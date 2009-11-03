using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NaveenaAccounts.Modules
{
    public partial class ChequeBook : DevExpress.XtraEditors.XtraUserControl
    {
        StringBuilder m_sbQueryText = new StringBuilder();
        DataTable ProcessDt = new DataTable();
        DataTable ProgramDt = new DataTable();
        db dbHandler = new db();
        public ChequeBook()
        {
            InitializeComponent();
        }

        private void ChequeBook_Load(object sender, EventArgs e)
        {

            loadBank();

        }

        private void loadBank()
        {
            DataTable dt = new DataTable();
            dbHandler.LoadChequeDetails(dt);
            lkeChqBookNo.Properties.DataSource = dt;
            lkeChqBookNo.Properties.PopulateColumns();
            lkeChqBookNo.Properties.ValueMember = dt.Columns[0].ToString();
            lkeChqBookNo.Properties.DisplayMember = dt.Columns[1].ToString();
            lkeChqBookNo.Properties.Columns[0].Visible = false;
            lkeChqBookNo.EditValue = null;

            lkeChqBookNo.Properties.PopupWidth = 200;
           
        }

        private void btnBankNew_Click(object sender, EventArgs e)
        {
            if (btnBankNew.Text == "Insert")
            {
                
                if (txtFrom.Text.Trim() == "" || txtTo.Text.Trim()=="")
                {
                    XtraMessageBox.Show("Please Enter Cheque Number", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("select id from  NexusGarments_Master_ChequeBook where BankID = '{0}' and NoFrom='{1}' and NoTo='{1}'",lkeBank.GetColumnValue("id").ToString(),txtFrom.Text.Trim(),txtTo.Text.Trim());
                if (dbHandler.IsExists(m_sbQueryText.ToString()))
                {
                    XtraMessageBox.Show("Cheque Entry already exits", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("insert into  NexusGarments_Master_ChequeBook (BankID,NoFrom,NoTo) values('{0}','{1}','{2}')", lkeBank.GetColumnValue("id").ToString(), txtFrom.Text.Trim(), txtTo.Text.Trim());
                if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
                {
                    XtraMessageBox.Show("Cheque Entry Inserted Sucessfully", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadBank();
                  lkeBank.Visible =   txtFrom.Visible = txtTo.Visible = false;
                    txtFrom.Text = txtTo.Text = ""; 
                    btnBankNew.Text = "New";
                 
                    btnBankEdit.Enabled = btnBankDelete.Enabled = true;
                    loadBank();
                }
                else
                {
                    XtraMessageBox.Show("Error on Insertion", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                btnBankEdit.Enabled = btnBankDelete.Enabled = false;
                btnBankNew.Text = "Insert";
                txtFrom.Visible = txtTo.Visible = true;
            }
        }

       
    }
}

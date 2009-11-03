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
    public partial class BankMaster : DevExpress.XtraEditors.XtraUserControl
    {
        StringBuilder m_sbQueryText = new StringBuilder();
        DataTable ProcessDt = new DataTable();
        DataTable ProgramDt = new DataTable();
     
        db dbHandler = new db();
        public BankMaster()
        {
            InitializeComponent();
        }

       
        private void btnBankDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void BankMaster_Load(object sender, EventArgs e)
        {

            loadBank();

        }

        private void loadBank()
        {
            DataTable dt = new DataTable();
            dbHandler.LoadBank(dt);
            lkeBank.Properties.DataSource = dt;
            lkeBank.Properties.PopulateColumns();
            lkeBank.Properties.ValueMember = dt.Columns[0].ToString();
            lkeBank.Properties.DisplayMember = dt.Columns[1].ToString();
            lkeBank.Properties.Columns[0].Visible = false;
            lkeBank.EditValue = null;
        }

        private void btnBankNew_Click_1(object sender, EventArgs e)
        {
            if (btnBankNew.Text == "Insert")
            {

                if (txtBank.Text == "")
                {
                    XtraMessageBox.Show("Please Enter Bank", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("select id from  nexusGarments_Master_Bank where lower(BankName) = '{0}'", txtBank.Text.ToString());
                if (dbHandler.IsExists(m_sbQueryText.ToString()))
                {
                    XtraMessageBox.Show("Bank already exits", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("insert into  nexusGarments_Master_Bank (BankName) values('{0}')", txtBank.Text.ToString());
                if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
                {
                    XtraMessageBox.Show("Bank Inserted Sucessfully", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadBank();
                    txtBank.Visible = false;
                    txtBank.Text = "";
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
                txtBank.Visible = true;
            }
        }

        private void btnBankEdit_Click_1(object sender, EventArgs e)
        {
            if (btnBankEdit.Text == "Update")
            {
                if (lkeBank.EditValue == null)
                {
                    XtraMessageBox.Show("Please Select Bank", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtBank.Text == "")
                {
                    XtraMessageBox.Show("Please Enter Bank", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("select id from  nexusGarments_Master_Bank where lower(BankName) = '{0}'", txtBank.Text.ToString());
                if (dbHandler.IsExists(m_sbQueryText.ToString()))
                {
                    XtraMessageBox.Show("Bank already exits", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("Update nexusGarments_Master_Bank set BankName='{0}' where  id='{1}'", txtBank.Text.ToString(), lkeBank.GetColumnValue("id").ToString());
                if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
                {
                    XtraMessageBox.Show("Bank Updated Sucessfully", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadBank();
                    txtBank.Visible = false;
                    txtBank.Text = "";
                    btnBankEdit.Text = "Edit";
                    btnBankNew.Enabled = btnBankDelete.Enabled = true;
                }
                else
                {
                    XtraMessageBox.Show("Error on Updation", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (lkeBank.EditValue == null)
                {
                    XtraMessageBox.Show("Please select Bank", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnBankNew.Enabled = btnBankDelete.Enabled = false;
                btnBankEdit.Text = "Update";
                txtBank.Visible = true;
                txtBank.Text = lkeBank.Text;
            }
        }

        private void btnBankDelete_Click_1(object sender, EventArgs e)
        {
            if (lkeBank.EditValue == null)
            {
                XtraMessageBox.Show("Please Select Bank", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Are you sure want to delete", "Nexus Garments", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            m_sbQueryText.Length = 0;
            m_sbQueryText.AppendFormat("delete from nexusGarments_Master_Bank  where  id='{0}'", lkeBank.GetColumnValue("id").ToString());
            if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
            {
                XtraMessageBox.Show("Bank Deleted Sucessfully", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadBank();

            }
            else
            {
                XtraMessageBox.Show("Error on Deletion", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

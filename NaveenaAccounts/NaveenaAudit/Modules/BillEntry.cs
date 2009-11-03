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
    public partial class BillEntry : DevExpress.XtraEditors.XtraUserControl
    {
        StringBuilder m_sbQueryText = new StringBuilder();
        DataTable ProcessDt = new DataTable();
        DataTable ProgramDt = new DataTable();
       //  private OdbcDataReader m_dataReader;

        db dbHandler = new db();

        public BillEntry()
        {
            InitializeComponent();
        }

        private void BillEntry_Load(object sender, EventArgs e)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Clear();
            dt.Rows.Clear();
            dbHandler.LoadCompanyProcess(dt);

            lkeProcess.Properties.DataSource = dt;
            lkeProcess.Properties.ValueMember = dt.Columns[0].ToString();
            lkeProcess.Properties.DisplayMember = dt.Columns[1].ToString();
            lkeProcess.Properties.PopulateColumns();
            lkeProcess.Properties.Columns[0].Visible = false;
        }

        private void lkeProcess_EditValueChanged(object sender, EventArgs e)
        {
            lkeCompany.EditValue = null;
            if (lkeProcess.GetColumnValue("id").ToString() == "3")
            {
                LoadKnittingLke();
            }
            if (lkeProcess.GetColumnValue("id").ToString() == "4")
            {
                LoadHeatLke();
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "5")
            {
                LoadDyingLke();
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "6")
            {
                LoadPrintingLke();
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "7")
            {
                LoadWashingLke();
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "13")
            {
                LoadStoreLke();
            }
            
        }

        private void LoadKnittingLke()
        {
            DataTable CompanyDT = new DataTable();
            dbHandler.LoadKnittingComapany(CompanyDT);
            lkeCompany.Properties.DataSource = CompanyDT;
            lkeCompany.Properties.PopulateColumns();
            lkeCompany.Properties.DisplayMember = CompanyDT.Columns[1].ToString();
            lkeCompany.Properties.ValueMember = CompanyDT.Columns[0].ToString();
            lkeCompany.Properties.Columns[0].Visible = false;
            lkeCompany.EditValue = null;
            
        }

        private void LoadHeatLke()
        {
            DataTable CompanyDT = new DataTable();
            dbHandler.LoadHeatComapany(CompanyDT);
            lkeCompany.Properties.DataSource = CompanyDT;
            lkeCompany.Properties.PopulateColumns();
            lkeCompany.Properties.DisplayMember = CompanyDT.Columns[1].ToString();
            lkeCompany.Properties.ValueMember = CompanyDT.Columns[0].ToString();
            lkeCompany.Properties.Columns[0].Visible = false;
            lkeCompany.EditValue = null;

        }

        private void LoadDyingLke()
        {
            DataTable CompanyDT = new DataTable();
            dbHandler.LoadDyingComapany(CompanyDT);
            lkeCompany.Properties.DataSource = CompanyDT;
            lkeCompany.Properties.PopulateColumns();
            lkeCompany.Properties.DisplayMember = CompanyDT.Columns[1].ToString();
            lkeCompany.Properties.ValueMember = CompanyDT.Columns[0].ToString();
            lkeCompany.Properties.Columns[0].Visible = false;
            lkeCompany.EditValue = null;

        }

        private void LoadPrintingLke()
        {
            DataTable CompanyDT = new DataTable();
            dbHandler.LoadPrintingComapany(CompanyDT);
            lkeCompany.Properties.DataSource = CompanyDT;
            lkeCompany.Properties.PopulateColumns();
            lkeCompany.Properties.DisplayMember = CompanyDT.Columns[1].ToString();
            lkeCompany.Properties.ValueMember = CompanyDT.Columns[0].ToString();
            lkeCompany.Properties.Columns[0].Visible = false;
            lkeCompany.EditValue = null;

        }

        private void LoadWashingLke()
        {
            DataTable CompanyDT = new DataTable();
            dbHandler.LoadWashingComapany(CompanyDT);
            lkeCompany.Properties.DataSource = CompanyDT;
            lkeCompany.Properties.PopulateColumns();
            lkeCompany.Properties.DisplayMember = CompanyDT.Columns[1].ToString();
            lkeCompany.Properties.ValueMember = CompanyDT.Columns[0].ToString();
            lkeCompany.Properties.Columns[0].Visible = false;
            lkeCompany.EditValue = null;

        }


        private void LoadStoreLke()
        {
            DataTable CompanyDT = new DataTable();
            dbHandler.LoadStoreComapany(CompanyDT);
            lkeCompany.Properties.DataSource = CompanyDT;
            lkeCompany.Properties.PopulateColumns();
            lkeCompany.Properties.DisplayMember = CompanyDT.Columns[1].ToString();
            lkeCompany.Properties.ValueMember = CompanyDT.Columns[0].ToString();
            lkeCompany.Properties.Columns[0].Visible = false;
            lkeCompany.EditValue = null;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (dateEdit1.Text.Trim() == "")
            {
                XtraMessageBox.Show("Please Select Billing  Date", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtBillNumber.Text.Trim() == "")
            {
                XtraMessageBox.Show("Please Select Billing  Date", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (Convert.ToDateTime(dateEdit1.Text) > DateTime.Now)
            {
               
                XtraMessageBox.Show("Please Check the Date", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string dcno =  dbHandler.AccountsRefGen();
            m_sbQueryText.Length = 0;
            m_sbQueryText.AppendFormat("update nexusGarments_Accounts_RefGen set value=value + 1 where processid='0'");
            if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            { 
               
                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("INSERT INTO [nexusGarments_Accounts_LOTBill] ([dcno] ,[billweight] ,[debitweight] ,[BillUPrice],[refno],[billno],[BillDate],[DebitAmmount],[TDS],preparedby) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", gridView1.GetRowCellValue(i, "DC Number").ToString(), gridView1.GetRowCellValue(i, "Bill Weight").ToString(), gridView1.GetRowCellValue(i, "Debit Weight").ToString(), gridView1.GetRowCellValue(i, "Ammount").ToString(), dcno, txtBillNumber.Text.ToString(), CovertDate(dateEdit1.Text.ToString()), gridView1.GetRowCellValue(i, "Debit Ammount").ToString(), gridView1.GetRowCellValue(i, "TDS").ToString(), Sess.id.ToString());
                    if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
                    {
                    }
                    else {
                       
                        XtraMessageBox.Show("Error on Process", "Nexus Garments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //m_sbQueryText.Length = 0;
                    //m_sbQueryText.AppendFormat("INSERT INTO [nexusGarments_Billingid] ([Process] ,[processid]) values ('{0}','{1}')", lkeProcess.GetColumnValue("id").ToString(), gridView1.GetRowCellValue(i, "id").ToString());
                    //if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
                    //{
                    //}
                
            }


            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("INSERT INTO [nexusGarments_Accounts_BillTax] (mapid,Taxid,Prec,notes) values ('{0}','{1}','{2}','{3}')", dcno, gridView2.GetRowCellValue(i, "Tax").ToString(), gridView2.GetRowCellValue(i, "Precentage").ToString(), gridView2.GetRowCellValue(i, "Note").ToString());
                if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
                {
                }
            }


            XtraMessageBox.Show("Bill Has Been Send for the Approval","Nexus Garments",  MessageBoxButtons.OK, MessageBoxIcon.Information);

            gridControl1.DataSource = null;
            gridControl2.DataSource = null;

        }

        private string CovertDate(string Dat)
        {
            string[] ndate = Dat.Split('-');

            return ndate[1] + "-" + ndate[0] + "-" + ndate[2];

        }

        private void chkDCNo_EditValueChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;

            gridView1.Columns.Clear();
            gridView2.Columns.Clear();
            if (lkeProcess.EditValue == null)
            {
                return;
            }

            if (chkDCNo.Text == "Select DC" || chkDCNo.Text.Trim() == "")
            {
                return;
            }

                DataTable tdt = new DataTable();
                dbHandler.LoadTaxInfo(tdt);
                gridControl2.DataSource = tdt;

                DataTable TAxdt = new DataTable();
                dbHandler.LoadTax(TAxdt);
                repLkeTax.DataSource = TAxdt;
                repLkeTax.PopulateColumns();
                repLkeTax.Properties.DisplayMember = TAxdt.Columns[1].ToString();
                repLkeTax.Properties.ValueMember = TAxdt.Columns[0].ToString();
                repLkeTax.Properties.Columns[0].Visible = false;

                gridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

                gridView2.Columns[0].ColumnEdit = repLkeTax;

            if (lkeProcess.GetColumnValue("id").ToString() == "3")
            {


                DataSet dt = new DataSet();
                dbHandler.LoadKnittingBill(dt, chkDCNo.Text.ToString().Trim());
                gridControl1.DataSource = dt.Tables["nexusGarments_KnittingProgram"];
               // gridView1.Columns["id"].Visible = false;
                gridView1.BestFitColumns();


                

              
               
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "4")
            {


                DataTable dt = new DataTable();
                //dbHandler.(dt, chkDCNo.Text.ToString().Trim());
                gridControl1.DataSource = dt;
                gridView1.Columns["id"].Visible = false;
                gridView1.BestFitColumns();

            }
            if (lkeProcess.GetColumnValue("id").ToString() == "5")
            {
                DataTable dt = new DataTable();
                dbHandler.LoadDyingChequeDetails(dt, chkDCNo.Text.ToString().Trim());
                gridControl1.DataSource = dt;
                gridView1.Columns["id"].Visible = false;
                gridView1.BestFitColumns();
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "6")
            {
                DataTable dt = new DataTable();
                dbHandler.LoadPrintingChequeDetails(dt, chkDCNo.Text.ToString().Trim());
                gridControl1.DataSource = dt;
                gridView1.Columns["id"].Visible = false;
                gridView1.BestFitColumns();
            }


            if (lkeProcess.GetColumnValue("id").ToString() == "7")
            {
                DataTable dt = new DataTable();
                dbHandler.LoadWashingChequeDetails(dt, chkDCNo.Text.ToString().Trim());
                gridControl1.DataSource = dt;
                gridView1.Columns["id"].Visible = false;
                gridView1.BestFitColumns();
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "13")
            {
                DataTable dt = new DataTable();
                m_sbQueryText.Length = 0;
                
                

                dbHandler.LoadStorePODetails(dt,chkDCNo.Text.ToString().Trim());
                gridControl1.DataSource = dt;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("select sum(orderQuantity),sum(recQuantity) from  nexusGarments_StorePO where ponumber='{0}'", gridView1.GetRowCellDisplayText(i,"PO Number"));
                    string[] receivedDetails=new String[2];
                    receivedDetails=dbHandler.loadNetQuantity(m_sbQueryText.ToString());
                    {
                        gridView1.SetRowCellValue(i, "Ordered Quantity",receivedDetails[0]);
                        gridView1.SetRowCellValue(i, "Received Quantity",receivedDetails[1]);
                       // MessageBox.Show("success");
                    }
                }

                //gridView1.Columns["id"].Visible = false;
                gridView1.BestFitColumns();
            }

        }

        private void lkeCompany_EditValueChanged(object sender, EventArgs e)
        {
            chkDCNo.Properties.Items.Clear();
            if (lkeCompany.EditValue == null)
            {
                return;
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "3")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT DISTINCT nexusGarments_KnittingProcess.DCNo FROM nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id WHERE nexusGarments_KnittingProcess.isclosed='True' and nexusGarments_KnittingProgram.companyid=" + lkeCompany.GetColumnValue("id").ToString() + "  and  (nexusGarments_KnittingProcess.id NOT IN (SELECT     Processid FROM          nexusGarments_Billingid WHERE      (Process = '3')))");
                dbHandler.LoadKnittingBillDC(chkDCNo, m_sbQueryText.ToString());
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "4")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT DISTINCT nexusGarments_HeatsettingProcess.DCNo FROM nexusGarments_HeatsettingProcess INNER JOIN nexusGarments_HeatsettingProgram ON nexusGarments_HeatsettingProcess.mapid = nexusGarments_HeatsettingProgram.id WHERE nexusGarments_HeatsettingProcess.isclosed='True' and nexusGarments_HeatsettingProgram.heatsettingcompany=" + lkeCompany.GetColumnValue("id").ToString() + "  and  (nexusGarments_HeatsettingProcess.id NOT IN (SELECT     Processid FROM          nexusGarments_Billingid WHERE      (Process = '4')))");
                dbHandler.LoadKnittingBillDC(chkDCNo, m_sbQueryText.ToString());
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "5")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT DISTINCT nexusGarments_DyingProcess.DCNo FROM nexusGarments_DyingProcess INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id WHERE nexusGarments_DyingProcess.isclosed='True' and nexusGarments_DyingProgram.dyingcomp=" + lkeCompany.GetColumnValue("id").ToString() + "  and  (nexusGarments_DyingProcess.id NOT IN (SELECT     Processid FROM          nexusGarments_Billingid WHERE      (Process = '5')))");
                dbHandler.LoadKnittingBillDC(chkDCNo, m_sbQueryText.ToString());
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "6")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT DISTINCT nexusGarments_printingProcess.DCNo FROM nexusGarments_printingProcess INNER JOIN nexusGarments_printingProgram ON nexusGarments_printingProcess.mapid = nexusGarments_printingProgram.id WHERE nexusGarments_printingProcess.isclosed='True' and nexusGarments_printingProgram.printingcomp=" + lkeCompany.GetColumnValue("id").ToString() + "  and  (nexusGarments_printingProcess.id NOT IN (SELECT     Processid FROM          nexusGarments_Billingid WHERE      (Process = '6')))");
                dbHandler.LoadKnittingBillDC(chkDCNo, m_sbQueryText.ToString());
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "7")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT DISTINCT nexusGarments_washingProcess.DCNo FROM nexusGarments_washingProcess INNER JOIN nexusGarments_washingProgram ON nexusGarments_washingProcess.mapid = nexusGarments_washingProgram.id WHERE nexusGarments_washingProcess.isclosed='True' and nexusGarments_washingProgram.dyingcomp=" + lkeCompany.GetColumnValue("id").ToString() + "  and  (nexusGarments_washingProcess.id NOT IN (SELECT     Processid FROM          nexusGarments_Billingid WHERE      (Process = '7')))");
                dbHandler.LoadKnittingBillDC(chkDCNo, m_sbQueryText.ToString());
            }


            if (lkeProcess.GetColumnValue("id").ToString() == "13")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT   DISTINCT  ponumber as dcno FROM         nexusGarments_StorePO where isbilled='False' and supplier =  " + lkeCompany.GetColumnValue("id").ToString());
                dbHandler.LoadKnittingBillDC(chkDCNo, m_sbQueryText.ToString());
            }

        
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}

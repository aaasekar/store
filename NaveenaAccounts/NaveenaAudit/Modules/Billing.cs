using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net.Sockets;
using System.Net;
namespace NaveenaAccounts.Modules
{
    public partial class Billing : DevExpress.XtraEditors.XtraUserControl
    {
        NumberToText nm = new NumberToText();
        StringBuilder m_sbQueryText = new StringBuilder();
        
        db dbHandler = new db();
        public Billing()
        {
            InitializeComponent();
        }

        private void Billing_Load(object sender, EventArgs e)
        {

            //IPAddress sd = new IPAddress(
            //MessageBox.Show(sd.Address.ToString());

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



        private void lkeCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (lkeCompany.EditValue == null)
            {
                return;
            }
            if (lkeProcess.GetColumnValue("id").ToString() == "3")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT     DISTINCT  nexusGarments_Accounts_LOTBill_1.Refno FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id  where nexusGarments_Accounts_LOTBill_1.process=3 and nexusGarments_Accounts_LOTBill_1.isbilled='False' and nexusGarments_Accounts_LOTBill_1.isapproved='True' and nexusGarments_KnittingProgram.companyid={0}", lkeCompany.GetColumnValue("id").ToString());
                dbHandler.LoadRefDC(chkDCNo, m_sbQueryText.ToString());
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "4")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT     DISTINCT  nexusGarments_Accounts_LOTBill_1.Refno FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_HeatSettingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_HeatSettingProcess.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id  where nexusGarments_Accounts_LOTBill_1.process=4 and nexusGarments_Accounts_LOTBill_1.isbilled='False' and nexusGarments_Accounts_LOTBill_1.isapproved='True' and nexusGarments_HeatSettingProgram.heatsettingcompany={0}", lkeCompany.GetColumnValue("id").ToString());
                dbHandler.LoadRefDC(chkDCNo, m_sbQueryText.ToString());
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "5")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT     DISTINCT  nexusGarments_Accounts_LOTBill_1.Refno FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_DyingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_DyingProcess.id INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id  where nexusGarments_Accounts_LOTBill_1.process=5 and nexusGarments_Accounts_LOTBill_1.isbilled='False' and nexusGarments_Accounts_LOTBill_1.isapproved='True' and nexusGarments_DyingProgram.dyingcomp={0}", lkeCompany.GetColumnValue("id").ToString());
                dbHandler.LoadRefDC(chkDCNo, m_sbQueryText.ToString());
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "6")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT     DISTINCT  nexusGarments_Accounts_LOTBill_1.Refno FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_PrintingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_PrintingProcess.id INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id  where nexusGarments_Accounts_LOTBill_1.process=6 and nexusGarments_Accounts_LOTBill_1.isbilled='False' and nexusGarments_Accounts_LOTBill_1.isapproved='True' and nexusGarments_PrintingProgram.printingcomp={0}", lkeCompany.GetColumnValue("id").ToString());
                dbHandler.LoadRefDC(chkDCNo, m_sbQueryText.ToString());
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "7")
            {
                m_sbQueryText.Length = 0;
                m_sbQueryText.AppendFormat("SELECT     DISTINCT  nexusGarments_Accounts_LOTBill_1.Refno FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_WashingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_WashingProcess.id INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id  where nexusGarments_Accounts_LOTBill_1.process=7 and nexusGarments_Accounts_LOTBill_1.isbilled='False' and nexusGarments_Accounts_LOTBill_1.isapproved='True' and nexusGarments_WashingProgram.dyingcomp={0}", lkeCompany.GetColumnValue("id").ToString());
                dbHandler.LoadRefDC(chkDCNo, m_sbQueryText.ToString());
            }



        }

        private void chkDCNo_EditValueChanged(object sender, EventArgs e)
        { 
            
            string[] sd = chkDCNo.Text.Split(',');
                DataTable dt = new DataTable();
                m_sbQueryText.Length = 0;
            if (chkDCNo.Text.Trim() == "")
            {
                return;
            }

            if (lkeProcess.GetColumnValue("id").ToString() == "3")
            {

               
                m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_KnittingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax     INNER JOIN  nexusGarments_Accounts_LOTBill  ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_KnittingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_KnittingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_KnittingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_KnittingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_KnittingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM  nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[0]);
                if (sd.Length > 0)
                {
                    for (int i = 1; i <= sd.Length-1; i++)
                    {
                        m_sbQueryText.AppendFormat(" union ");
                        m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_KnittingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_KnittingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_KnittingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_KnittingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_KnittingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_KnittingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM   nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[i]);

                    }
                }
                
            }


            if (lkeProcess.GetColumnValue("id").ToString() == "4")
            {
                m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_HeatSettingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax     INNER JOIN  nexusGarments_Accounts_LOTBill  ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_HeatSettingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_HeatSettingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_HeatSettingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_HeatSettingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_HeatSettingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM  nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_HeatSettingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_HeatSettingProcess.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[0]);
                if (sd.Length > 0)
                {
                    for (int i = 1; i <= sd.Length - 1; i++)
                    {
                        m_sbQueryText.AppendFormat(" union ");
                        m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_HeatSettingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_HeatSettingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_HeatSettingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_HeatSettingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_HeatSettingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_HeatSettingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM   nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_HeatSettingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_HeatSettingProcess.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[i]);

                    }
                }

            }



            if (lkeProcess.GetColumnValue("id").ToString() == "5")
            {
                m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_DyingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax     INNER JOIN  nexusGarments_Accounts_LOTBill  ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_DyingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_DyingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_DyingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_DyingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_DyingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM  nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_DyingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_DyingProcess.id INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[0]);
                if (sd.Length > 0)
                {
                    for (int i = 1; i <= sd.Length - 1; i++)
                    {
                        m_sbQueryText.AppendFormat(" union ");
                        m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_DyingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_DyingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_DyingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_DyingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_DyingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_DyingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM   nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_DyingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_DyingProcess.id INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[i]);

                    }
                }

            }



            if (lkeProcess.GetColumnValue("id").ToString() == "6")
            {
                m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_PrintingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax     INNER JOIN  nexusGarments_Accounts_LOTBill  ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_PrintingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_PrintingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_PrintingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_PrintingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_PrintingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM  nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_PrintingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_PrintingProcess.id INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[0]);
                if (sd.Length > 0)
                {
                    for (int i = 1; i <= sd.Length - 1; i++)
                    {
                        m_sbQueryText.AppendFormat(" union ");
                        m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_PrintingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_PrintingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_PrintingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_PrintingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_PrintingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_PrintingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM   nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_PrintingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_PrintingProcess.id INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[i]);

                    }
                }

            }

            if (lkeProcess.GetColumnValue("id").ToString() == "7")
            {
                m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_WashingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax     INNER JOIN  nexusGarments_Accounts_LOTBill  ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_WashingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_WashingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_WashingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_WashingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_WashingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM  nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_WashingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_WashingProcess.id INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[0]);
                if (sd.Length > 0)
                {
                    for (int i = 1; i <= sd.Length - 1; i++)
                    {
                        m_sbQueryText.AppendFormat(" union ");
                        m_sbQueryText.AppendFormat("SELECT     nexusGarments_Accounts_LOTBill_1.id, nexusGarments_WashingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT     SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM    nexusGarments_Accounts_BillTax INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE      (nexusGarments_Accounts_BillTax.mapid = '{0}')) AS sumprecentage, nexusGarments_WashingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount,  (nexusGarments_WashingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100 AS tds, (nexusGarments_WashingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount) - ((nexusGarments_WashingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)  * nexusGarments_Accounts_LOTBill_1.TDS / 100)  +  ((nexusGarments_WashingProcess.recWeight * nexusGarments_Accounts_LOTBill_1.BillUPrice - nexusGarments_Accounts_LOTBill_1.DebitAmmount)/100 *  (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM   nexusGarments_Accounts_BillTax  INNER JOIN nexusGarments_Accounts_LOTBill ON nexusGarments_Accounts_LOTBill.refno = nexusGarments_Accounts_BillTax.mapid WHERE (nexusGarments_Accounts_BillTax.mapid = '{0}')))AS BillingPrice FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_WashingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_WashingProcess.id INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id WHERE     (nexusGarments_Accounts_LOTBill_1.Refno = '{0}') and nexusGarments_Accounts_LOTBill_1.isbilled='False'", sd[i]);

                    }
                }

            }



            dbHandler.LoadBillingGrid(m_sbQueryText.ToString(), dt);
            gridControl1.DataSource = dt;
            gridView1.Columns["id"].Visible = false;
            gridView1.Columns["Select"].ColumnEdit = repchk;
            gridView1.Columns["bm"].Visible = false;
            gridView1.BestFitColumns();
            // gridView1.Columns["Billing Ammount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.OptionsView.ShowFooter = false;
            NewMethod();


        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblAmmount.Text == "")
            {
                return;
            }
            txtNo.Text = "";
            if (radioGroup1.Text.Trim() == "DD" || radioGroup1.Text.Trim() == "Cheque")
            {
                lblNO.Visible = txtNo.Visible = true;
                if (radioGroup1.Text.Trim() == "Cheque")
                {
                    NumberToText nm = new NumberToText();
                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("select nextno from NexusGarments_Master_ChequeBook where id = (select min(id) from NexusGarments_Master_ChequeBook where isClosed='False')");
                    dbHandler.Loadchequeno(txtNo, m_sbQueryText.ToString());
                    lblamount.Visible = lblDate.Visible = lblPay.Visible = true;
                    txtAmmount.Visible = datChequeDate.Visible = txtPay.Visible = true;
                    txtAmmount.Text = nm.changeCurrencyToWords(lblAmmount.Text.ToString());
                    txtPay.Text = lkeCompany.Text.ToString();     
          
                }
                else {
                    lblamount.Visible = lblDate.Visible = lblPay.Visible = false;
                    txtAmmount.Visible = datChequeDate.Visible = txtPay.Visible = false;
                }

               



            }
            else {
                lblNO.Visible = txtNo.Visible = false;
                lblamount.Visible = lblDate.Visible = lblPay.Visible = false;
                txtAmmount.Visible = datChequeDate.Visible = txtPay.Visible = false;
            }
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
           
        }

        private void repchk_EditValueChanged(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            lblAmmount.Focus();
            double sd = 0;
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {
                if (gridView1.GetRowCellValue(i, "Select").ToString() == "True")
                {
                    sd += Convert.ToDouble(gridView1.GetRowCellValue(i, "bm").ToString());
                }
            }
            lblAmmount.Text = Math.Round(Convert.ToDouble(sd.ToString())).ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (txtPay.Text.Trim() == "")
            {
                dxErrorProvider1.SetError(txtPay, "Please Enter Paye");
                return;
            }

            if (txtAmmount.Text.Trim() == "")
            {
                dxErrorProvider1.SetError(txtAmmount , "Please Enter Ammount");
                return;
            }

            if (txtNo.Text.Trim() == "")
            {
                dxErrorProvider1.SetError(txtNo, "Please Enter Cheque Number");
                return;
            }
            if (datChequeDate.Text == "")
            {
                dxErrorProvider1.SetError(datChequeDate, "Please Enter Payment Date");
                return;
            }

            if (XtraMessageBox.Show("Are you sure want to Print Cheque \n\nPlease Make sure Cheque is Placed at correct position", "Nexus Garments", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            Report.ChequePrint cp = new NaveenaAccounts.Report.ChequePrint();
            cp.lblAmmount.Text = "**"+txtAmmount.Text+"**";
            cp.lblDate.Text = datChequeDate.Text;
            cp.lblDigit.Text = "**"+lblAmmount.Text + "/-";
            cp.lblPayBy.Text = txtPay.Text;

            cp.ShowPreview();
        }

        private void lblAmmount_TextChanged(object sender, EventArgs e)
        {
            txtAmmount.Text = nm.changeCurrencyToWords(lblAmmount.Text.ToString());
        }

        private void Bill_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, "Select").ToString() == "True")
                {
                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("update nexusGarments_Accounts_LOTBill set IsBilled='True',BilledBy='{0}',BilledOn=getdate(),Paymentmode='{1}',Number='{2}' where id={3}", Sess.id.ToString(), radioGroup1.SelectedIndex.ToString(),txtNo.Text.Trim(),gridView1.GetRowCellDisplayText(i,"id").ToString());
                    if (dbHandler.UpdateQuery(m_sbQueryText.ToString()))
                    {
                        
                    }
                }
            }

        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }


       
        
        
    }
}

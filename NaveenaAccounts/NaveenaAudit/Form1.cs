using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace NaveenaAccounts
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }


        #region General Function

        private void newtab(UserControl form, string Header, Bitmap bitmap)
        {
            this.tabMain.TabPages.Add(Header);

            this.tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1;
            this.tabMain.SelectedTabPage.Controls.Add(form);
            this.tabMain.SelectedTabPage.Image = bitmap;
        }

        public void newtab(UserControl form, string Header)
        {

            // tablist.

            this.tabMain.TabPages.Add(Header);

            this.tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1;
            this.tabMain.SelectedTabPage.Controls.Add(form);
        }
        #endregion

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Modules.BankMaster inv = new NaveenaAccounts.Modules.BankMaster();
            newtab(inv, " Bank Master");
        }

        private void tabMain_CloseButtonClick(object sender, EventArgs e)
        {
            tabMain.SelectedTabPage.Dispose();
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Modules.ChequeBook inv = new NaveenaAccounts.Modules.ChequeBook();
            newtab(inv, " Cheque Book Master");
            newtab(inv, "check number");
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Modules.BillEntry inv = new NaveenaAccounts.Modules.BillEntry();
            newtab(inv, " Bill Entry");
            newtab(inv, "bill laod");
            newtab(inv, "bill laodbill laodbill laodbill laod");

        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Modules.Billing inv = new NaveenaAccounts.Modules.Billing();
            newtab(inv, " Billing");
            newtab(inv, "gfgfghh");
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Modules.SalaryUnPaid inv = new NaveenaAccounts.Modules.SalaryUnPaid();
            newtab(inv, " Salary Un Paid");
            //nothing
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Modules.Sample inv = new NaveenaAccounts.Modules.Sample();
            newtab(inv, " Salary Un Paid");
            //nothing
        }

        



    }
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Native;
namespace NaveenaAccounts.Report
{
    public partial class ChequePrint : DevExpress.XtraReports.UI.XtraReport
    {
        public ChequePrint()
        {
            InitializeComponent();
        }

        private void ChequePrint_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
         
        }

    }
}

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
    public partial class Sample : DevExpress.XtraEditors.XtraUserControl
    {
        DevExpress.XtraPivotGrid.PivotGridField f1 = new DevExpress.XtraPivotGrid.PivotGridField();
        public Sample()
        {
            InitializeComponent();
        }

        private void Sample_Load(object sender, EventArgs e)
        {

            
            //db dbhandler = new db();
            //DataSet ds = new DataSet();
            //dbhandler.LoadHeatSettingApproval(ds);
            //pivotGridControl1.DataSource = ds.Tables["nexusGarments_HeatSettingLog"];

            //f1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            //f1.Index = 1;
            //f1.FieldName = "Company";
            //f1.Name = "Companydsd";
            //pivotGridControl1.Fields.Add(f1);          
            //pivotGridControl1.
        }
    }
}

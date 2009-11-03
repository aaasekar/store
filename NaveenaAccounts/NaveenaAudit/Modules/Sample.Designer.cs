namespace NaveenaAccounts.Modules
{
    partial class Sample
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraPivotGrid.PivotGridGroup pivotGridGroup1 = new DevExpress.XtraPivotGrid.PivotGridGroup();
            DevExpress.XtraPivotGrid.PivotGridFieldSortCondition pivotGridFieldSortCondition1 = new DevExpress.XtraPivotGrid.PivotGridFieldSortCondition();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.fieldbillno = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldBillUPrice = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldDebitAmmount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fielddebitweight = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldNumber = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldrefno = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldTDS = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldBillDate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.nexusGarmentsAccountsLOTBillBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nexusGarmentsDataSet = new NaveenaAccounts.nexusGarmentsDataSet();
            this.nexusGarments_Accounts_LOTBillTableAdapter = new NaveenaAccounts.nexusGarmentsDataSetTableAdapters.nexusGarments_Accounts_LOTBillTableAdapter();
            this.nexusGarments_Accounts_LOTBillTableAdapter1 = new NaveenaAccounts.nexusGarmentsDataSetTableAdapters.nexusGarments_Accounts_LOTBillTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nexusGarmentsAccountsLOTBillBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nexusGarmentsDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pivotGridControl1.DataSource = this.nexusGarmentsAccountsLOTBillBindingSource;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldbillno,
            this.fieldBillUPrice,
            this.fieldDebitAmmount,
            this.fielddebitweight,
            this.fieldNumber,
            this.fieldrefno,
            this.fieldTDS,
            this.fieldBillDate});
            pivotGridGroup1.Caption = "fghjk";
            pivotGridGroup1.Fields.Add(this.fieldbillno);
            pivotGridGroup1.Fields.Add(this.fieldrefno);
            pivotGridGroup1.Hierarchy = null;
            this.pivotGridControl1.Groups.AddRange(new DevExpress.XtraPivotGrid.PivotGridGroup[] {
            pivotGridGroup1});
            this.pivotGridControl1.Location = new System.Drawing.Point(3, 3);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(794, 311);
            this.pivotGridControl1.TabIndex = 0;
            // 
            // fieldbillno
            // 
            this.fieldbillno.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldbillno.AreaIndex = 0;
            this.fieldbillno.FieldName = "billno";
            this.fieldbillno.Name = "fieldbillno";
            pivotGridFieldSortCondition1.Field = this.fieldBillDate;
            pivotGridFieldSortCondition1.Value = new System.DateTime(2009, 10, 17, 0, 0, 0, 0);
            this.fieldbillno.SortBySummaryInfo.Conditions.Add(pivotGridFieldSortCondition1);
            this.fieldbillno.SortBySummaryInfo.Field = this.fieldDebitAmmount;
            // 
            // fieldBillUPrice
            // 
            this.fieldBillUPrice.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldBillUPrice.AreaIndex = 0;
            this.fieldBillUPrice.FieldName = "BillUPrice";
            this.fieldBillUPrice.Name = "fieldBillUPrice";
            // 
            // fieldDebitAmmount
            // 
            this.fieldDebitAmmount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldDebitAmmount.AreaIndex = 1;
            this.fieldDebitAmmount.FieldName = "DebitAmmount";
            this.fieldDebitAmmount.Name = "fieldDebitAmmount";
            // 
            // fielddebitweight
            // 
            this.fielddebitweight.AreaIndex = 2;
            this.fielddebitweight.FieldName = "debitweight";
            this.fielddebitweight.Name = "fielddebitweight";
            // 
            // fieldNumber
            // 
            this.fieldNumber.AreaIndex = 1;
            this.fieldNumber.FieldName = "Number";
            this.fieldNumber.Name = "fieldNumber";
            // 
            // fieldrefno
            // 
            this.fieldrefno.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldrefno.AreaIndex = 1;
            this.fieldrefno.FieldName = "refno";
            this.fieldrefno.Name = "fieldrefno";
            // 
            // fieldTDS
            // 
            this.fieldTDS.AreaIndex = 0;
            this.fieldTDS.FieldName = "TDS";
            this.fieldTDS.Name = "fieldTDS";
            // 
            // fieldBillDate
            // 
            this.fieldBillDate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldBillDate.AreaIndex = 0;
            this.fieldBillDate.FieldName = "BillDate";
            this.fieldBillDate.Name = "fieldBillDate";
            // 
            // chartControl1
            // 
            this.chartControl1.DataAdapter = this.nexusGarments_Accounts_LOTBillTableAdapter;
            this.chartControl1.DataSource = this.pivotGridControl1;
            this.chartControl1.Location = new System.Drawing.Point(144, 320);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            sideBySideBarSeriesLabel1.LineVisible = true;
            this.chartControl1.SeriesTemplate.Label = sideBySideBarSeriesLabel1;
            this.chartControl1.Size = new System.Drawing.Size(300, 200);
            this.chartControl1.TabIndex = 1;
            // 
            // nexusGarmentsAccountsLOTBillBindingSource
            // 
            this.nexusGarmentsAccountsLOTBillBindingSource.DataMember = "nexusGarments_Accounts_LOTBill";
            this.nexusGarmentsAccountsLOTBillBindingSource.DataSource = this.nexusGarmentsDataSet;
            // 
            // nexusGarmentsDataSet
            // 
            this.nexusGarmentsDataSet.DataSetName = "nexusGarmentsDataSet";
            this.nexusGarmentsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // nexusGarments_Accounts_LOTBillTableAdapter
            // 
            this.nexusGarments_Accounts_LOTBillTableAdapter.ClearBeforeFill = true;
            // 
            // nexusGarments_Accounts_LOTBillTableAdapter1
            // 
            this.nexusGarments_Accounts_LOTBillTableAdapter1.ClearBeforeFill = true;
            // 
            // Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.pivotGridControl1);
            this.Name = "Sample";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.Sample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nexusGarmentsAccountsLOTBillBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nexusGarmentsDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private System.Windows.Forms.BindingSource nexusGarmentsAccountsLOTBillBindingSource;
        private nexusGarmentsDataSet nexusGarmentsDataSet;
        private NaveenaAccounts.nexusGarmentsDataSetTableAdapters.nexusGarments_Accounts_LOTBillTableAdapter nexusGarments_Accounts_LOTBillTableAdapter;
        private DevExpress.XtraPivotGrid.PivotGridField fieldbillno;
        private DevExpress.XtraPivotGrid.PivotGridField fieldBillUPrice;
        private DevExpress.XtraPivotGrid.PivotGridField fieldDebitAmmount;
        private DevExpress.XtraPivotGrid.PivotGridField fielddebitweight;
        private DevExpress.XtraPivotGrid.PivotGridField fieldNumber;
        private DevExpress.XtraPivotGrid.PivotGridField fieldrefno;
        private DevExpress.XtraPivotGrid.PivotGridField fieldTDS;
        private DevExpress.XtraPivotGrid.PivotGridField fieldBillDate;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private NaveenaAccounts.nexusGarmentsDataSetTableAdapters.nexusGarments_Accounts_LOTBillTableAdapter nexusGarments_Accounts_LOTBillTableAdapter1;
    }
}

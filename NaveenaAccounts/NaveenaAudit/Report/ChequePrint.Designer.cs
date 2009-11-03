namespace NaveenaAccounts.Report
{
    partial class ChequePrint
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblDigit = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPayBy = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAmmount = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDigit,
            this.lblPayBy,
            this.lblDate,
            this.lblAmmount});
            this.Detail.Height = 246;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UsePadding = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblDigit
            // 
            this.lblDigit.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDigit.Location = new System.Drawing.Point(533, 142);
            this.lblDigit.Name = "lblDigit";
            this.lblDigit.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDigit.Size = new System.Drawing.Size(108, 25);
            this.lblDigit.StylePriority.UseFont = false;
            this.lblDigit.Text = "104345345527/-";
            // 
            // lblPayBy
            // 
            this.lblPayBy.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayBy.Location = new System.Drawing.Point(58, 83);
            this.lblPayBy.Name = "lblPayBy";
            this.lblPayBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPayBy.Size = new System.Drawing.Size(475, 25);
            this.lblPayBy.StylePriority.UseFont = false;
            this.lblPayBy.Text = "Taggin Technologies";
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(592, 50);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDate.Size = new System.Drawing.Size(83, 17);
            this.lblDate.StylePriority.UseFont = false;
            this.lblDate.Text = "12/4/2009";
            // 
            // lblAmmount
            // 
            this.lblAmmount.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmmount.Location = new System.Drawing.Point(58, 117);
            this.lblAmmount.Multiline = true;
            this.lblAmmount.Name = "lblAmmount";
            this.lblAmmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAmmount.Size = new System.Drawing.Size(408, 42);
            this.lblAmmount.StylePriority.UseFont = false;
            this.lblAmmount.StylePriority.UsePadding = false;
            this.lblAmmount.Text = "Ten Thoushand  Five Hundered twenty seven  Ten Thoushand  Five Hundered twenty se" +
                "ven only ";
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ChequePrint
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(100, 44, 0, 100);
            this.Name = "ChequePrint";
            this.PageHeight = 1100;
            this.PageWidth = 850;
            this.Version = "9.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ChequePrint_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        public DevExpress.XtraReports.UI.XRLabel lblPayBy;
        public DevExpress.XtraReports.UI.XRLabel lblDate;
        public DevExpress.XtraReports.UI.XRLabel lblAmmount;
        public DevExpress.XtraReports.UI.XRLabel lblDigit;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
    }
}

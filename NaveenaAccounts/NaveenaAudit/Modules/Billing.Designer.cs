namespace NaveenaAccounts.Modules
{
    partial class Billing
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lkeCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.lkeProcess = new DevExpress.XtraEditors.LookUpEdit();
            this.chkDCNo = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repchk = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.datChequeDate = new DevExpress.XtraEditors.DateEdit();
            this.txtAmmount = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.txtPay = new DevExpress.XtraEditors.TextEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.lblAmmount = new DevExpress.XtraEditors.LabelControl();
            this.lblamount = new DevExpress.XtraEditors.LabelControl();
            this.lblPay = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblNO = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkeCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDCNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repchk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datChequeDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datChequeDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkeCompany);
            this.groupControl1.Controls.Add(this.lkeProcess);
            this.groupControl1.Controls.Add(this.chkDCNo);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Location = new System.Drawing.Point(0, 13);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(800, 84);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Billing Information";
            // 
            // lkeCompany
            // 
            this.lkeCompany.Location = new System.Drawing.Point(325, 36);
            this.lkeCompany.Name = "lkeCompany";
            this.lkeCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkeCompany.Properties.NullText = "Select Company";
            this.lkeCompany.Size = new System.Drawing.Size(164, 20);
            this.lkeCompany.TabIndex = 2;
            this.lkeCompany.EditValueChanged += new System.EventHandler(this.lkeCompany_EditValueChanged);
            // 
            // lkeProcess
            // 
            this.lkeProcess.Location = new System.Drawing.Point(83, 36);
            this.lkeProcess.Name = "lkeProcess";
            this.lkeProcess.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkeProcess.Properties.NullText = "Select Process";
            this.lkeProcess.Size = new System.Drawing.Size(152, 20);
            this.lkeProcess.TabIndex = 1;
            this.lkeProcess.EditValueChanged += new System.EventHandler(this.lkeProcess_EditValueChanged);
            // 
            // chkDCNo
            // 
            this.chkDCNo.EditValue = "Select DC";
            this.chkDCNo.Location = new System.Drawing.Point(632, 36);
            this.chkDCNo.Name = "chkDCNo";
            this.chkDCNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkDCNo.Size = new System.Drawing.Size(152, 20);
            this.chkDCNo.TabIndex = 3;
            this.chkDCNo.EditValueChanged += new System.EventHandler(this.chkDCNo_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(265, 39);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(45, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Company";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Process";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(526, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Referance Number";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(0, 103);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repchk});
            this.gridControl1.Size = new System.Drawing.Size(800, 309);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gridView1_BeforeLeaveRow);
            // 
            // repchk
            // 
            this.repchk.AutoHeight = false;
            this.repchk.Name = "repchk";
            this.repchk.EditValueChanged += new System.EventHandler(this.repchk_EditValueChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(706, 574);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(91, 23);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "Submit";
            this.simpleButton1.Click += new System.EventHandler(this.Bill_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.pictureEdit1);
            this.groupControl2.Controls.Add(this.datChequeDate);
            this.groupControl2.Controls.Add(this.txtAmmount);
            this.groupControl2.Controls.Add(this.simpleButton2);
            this.groupControl2.Controls.Add(this.txtPay);
            this.groupControl2.Controls.Add(this.txtNo);
            this.groupControl2.Controls.Add(this.lblDate);
            this.groupControl2.Controls.Add(this.lblAmmount);
            this.groupControl2.Controls.Add(this.lblamount);
            this.groupControl2.Controls.Add(this.lblPay);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.lblNO);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.radioGroup1);
            this.groupControl2.Location = new System.Drawing.Point(0, 418);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(800, 150);
            this.groupControl2.TabIndex = 8;
            this.groupControl2.Text = "Billing Information";
            this.groupControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl2_Paint);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::NaveenaAccounts.Properties.Resources.CustomReccurence;
            this.pictureEdit1.Location = new System.Drawing.Point(285, 54);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Size = new System.Drawing.Size(25, 27);
            this.pictureEdit1.TabIndex = 8;
            this.pictureEdit1.ToolTip = "Please Click to change to next cheque number";
            this.pictureEdit1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.pictureEdit1.EditValueChanged += new System.EventHandler(this.pictureEdit1_EditValueChanged);
            // 
            // datChequeDate
            // 
            this.datChequeDate.EditValue = null;
            this.datChequeDate.Location = new System.Drawing.Point(117, 96);
            this.datChequeDate.Name = "datChequeDate";
            this.datChequeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datChequeDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.datChequeDate.Size = new System.Drawing.Size(193, 20);
            this.datChequeDate.TabIndex = 3;
            this.datChequeDate.Visible = false;
            // 
            // txtAmmount
            // 
            this.txtAmmount.Location = new System.Drawing.Point(499, 96);
            this.txtAmmount.Name = "txtAmmount";
            this.txtAmmount.Size = new System.Drawing.Size(260, 20);
            this.txtAmmount.TabIndex = 2;
            this.txtAmmount.Visible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = global::NaveenaAccounts.Properties.Resources.MenuBar_Print;
            this.simpleButton2.Location = new System.Drawing.Point(684, 122);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(111, 23);
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "Print Cheque";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtPay
            // 
            this.txtPay.Location = new System.Drawing.Point(499, 61);
            this.txtPay.Name = "txtPay";
            this.txtPay.Size = new System.Drawing.Size(260, 20);
            this.txtPay.TabIndex = 2;
            this.txtPay.Visible = false;
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(117, 60);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(162, 20);
            this.txtNo.TabIndex = 2;
            this.txtNo.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(11, 103);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(23, 13);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Date";
            this.lblDate.Visible = false;
            // 
            // lblAmmount
            // 
            this.lblAmmount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAmmount.Appearance.Options.UseFont = true;
            this.lblAmmount.Location = new System.Drawing.Point(499, 33);
            this.lblAmmount.Name = "lblAmmount";
            this.lblAmmount.Size = new System.Drawing.Size(0, 13);
            this.lblAmmount.TabIndex = 1;
            this.lblAmmount.TextChanged += new System.EventHandler(this.lblAmmount_TextChanged);
            // 
            // lblamount
            // 
            this.lblamount.Location = new System.Drawing.Point(378, 103);
            this.lblamount.Name = "lblamount";
            this.lblamount.Size = new System.Drawing.Size(45, 13);
            this.lblamount.TabIndex = 1;
            this.lblamount.Text = "Ammount";
            this.lblamount.Visible = false;
            // 
            // lblPay
            // 
            this.lblPay.Location = new System.Drawing.Point(378, 68);
            this.lblPay.Name = "lblPay";
            this.lblPay.Size = new System.Drawing.Size(18, 13);
            this.lblPay.TabIndex = 1;
            this.lblPay.Text = "Pay";
            this.lblPay.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(378, 33);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(99, 13);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Final Billing Ammount";
            // 
            // lblNO
            // 
            this.lblNO.Location = new System.Drawing.Point(11, 68);
            this.lblNO.Name = "lblNO";
            this.lblNO.Size = new System.Drawing.Size(37, 13);
            this.lblNO.TabIndex = 1;
            this.lblNO.Text = "Number";
            this.lblNO.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 33);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 13);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Payment Mode";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(117, 23);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Cash", "Cash"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("DD", "DD"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Cheque", "Cheque")});
            this.radioGroup1.Size = new System.Drawing.Size(193, 23);
            this.radioGroup1.TabIndex = 4;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // Billing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.Name = "Billing";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.Billing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkeCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDCNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repchk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datChequeDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datChequeDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LookUpEdit lkeCompany;
        private DevExpress.XtraEditors.LookUpEdit lkeProcess;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkDCNo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblNO;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblAmmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repchk;
        private DevExpress.XtraEditors.TextEdit txtPay;
        private DevExpress.XtraEditors.LabelControl lblPay;
        private DevExpress.XtraEditors.DateEdit datChequeDate;
        private DevExpress.XtraEditors.TextEdit txtAmmount;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.LabelControl lblamount;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
    }
}

namespace NaveenaAccounts.Modules
{
    partial class ChequeBook
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtTo = new DevExpress.XtraEditors.TextEdit();
            this.txtFrom = new DevExpress.XtraEditors.TextEdit();
            this.btnBankEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnBankDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnBankNew = new DevExpress.XtraEditors.SimpleButton();
            this.lkeChqBookNo = new DevExpress.XtraEditors.LookUpEdit();
            this.lkeBank = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeChqBookNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeBank.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtTo);
            this.groupControl1.Controls.Add(this.txtFrom);
            this.groupControl1.Controls.Add(this.btnBankEdit);
            this.groupControl1.Controls.Add(this.btnBankDelete);
            this.groupControl1.Controls.Add(this.btnBankNew);
            this.groupControl1.Controls.Add(this.lkeChqBookNo);
            this.groupControl1.Controls.Add(this.lkeBank);
            this.groupControl1.Location = new System.Drawing.Point(0, 13);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(800, 138);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Bank Information";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(183, 96);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(153, 20);
            this.txtTo.TabIndex = 3;
            this.txtTo.Visible = false;
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(6, 96);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(153, 20);
            this.txtFrom.TabIndex = 2;
            this.txtFrom.Visible = false;
            // 
            // btnBankEdit
            // 
            this.btnBankEdit.Location = new System.Drawing.Point(568, 93);
            this.btnBankEdit.Name = "btnBankEdit";
            this.btnBankEdit.Size = new System.Drawing.Size(75, 23);
            this.btnBankEdit.TabIndex = 5;
            this.btnBankEdit.Text = "Edit";
            // 
            // btnBankDelete
            // 
            this.btnBankDelete.Location = new System.Drawing.Point(683, 93);
            this.btnBankDelete.Name = "btnBankDelete";
            this.btnBankDelete.Size = new System.Drawing.Size(75, 23);
            this.btnBankDelete.TabIndex = 6;
            this.btnBankDelete.Text = "Delete";
            // 
            // btnBankNew
            // 
            this.btnBankNew.Location = new System.Drawing.Point(458, 93);
            this.btnBankNew.Name = "btnBankNew";
            this.btnBankNew.Size = new System.Drawing.Size(75, 23);
            this.btnBankNew.TabIndex = 4;
            this.btnBankNew.Text = "New";
            this.btnBankNew.Click += new System.EventHandler(this.btnBankNew_Click);
            // 
            // lkeChqBookNo
            // 
            this.lkeChqBookNo.Location = new System.Drawing.Point(5, 46);
            this.lkeChqBookNo.Name = "lkeChqBookNo";
            this.lkeChqBookNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkeChqBookNo.Properties.NullText = "Select Book No";
            this.lkeChqBookNo.Size = new System.Drawing.Size(154, 20);
            this.lkeChqBookNo.TabIndex = 0;
            // 
            // lkeBank
            // 
            this.lkeBank.Location = new System.Drawing.Point(183, 46);
            this.lkeBank.Name = "lkeBank";
            this.lkeBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkeBank.Properties.NullText = "Select Bank";
            this.lkeBank.Size = new System.Drawing.Size(154, 20);
            this.lkeBank.TabIndex = 1;
            this.lkeBank.Visible = false;
            // 
            // ChequeBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ChequeBook";
            this.Size = new System.Drawing.Size(799, 600);
            this.Load += new System.EventHandler(this.ChequeBook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeChqBookNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeBank.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnBankEdit;
        private DevExpress.XtraEditors.SimpleButton btnBankDelete;
        private DevExpress.XtraEditors.SimpleButton btnBankNew;
        private DevExpress.XtraEditors.LookUpEdit lkeBank;
        private DevExpress.XtraEditors.LookUpEdit lkeChqBookNo;
        private DevExpress.XtraEditors.TextEdit txtTo;
        private DevExpress.XtraEditors.TextEdit txtFrom;
    }
}

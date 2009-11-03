namespace NaveenaAccounts.Modules
{
    partial class BankMaster
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
            this.btnBankEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnBankDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnBankNew = new DevExpress.XtraEditors.SimpleButton();
            this.txtBank = new DevExpress.XtraEditors.TextEdit();
            this.lkeBank = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeBank.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnBankEdit);
            this.groupControl1.Controls.Add(this.btnBankDelete);
            this.groupControl1.Controls.Add(this.btnBankNew);
            this.groupControl1.Controls.Add(this.txtBank);
            this.groupControl1.Controls.Add(this.lkeBank);
            this.groupControl1.Location = new System.Drawing.Point(0, 13);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(800, 128);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Bank Information";
            // 
            // btnBankEdit
            // 
            this.btnBankEdit.Location = new System.Drawing.Point(504, 47);
            this.btnBankEdit.Name = "btnBankEdit";
            this.btnBankEdit.Size = new System.Drawing.Size(75, 23);
            this.btnBankEdit.TabIndex = 2;
            this.btnBankEdit.Text = "Edit";
            this.btnBankEdit.Click += new System.EventHandler(this.btnBankEdit_Click_1);
            // 
            // btnBankDelete
            // 
            this.btnBankDelete.Location = new System.Drawing.Point(619, 47);
            this.btnBankDelete.Name = "btnBankDelete";
            this.btnBankDelete.Size = new System.Drawing.Size(75, 23);
            this.btnBankDelete.TabIndex = 2;
            this.btnBankDelete.Text = "Delete";
            this.btnBankDelete.Click += new System.EventHandler(this.btnBankDelete_Click_1);
            // 
            // btnBankNew
            // 
            this.btnBankNew.Location = new System.Drawing.Point(394, 47);
            this.btnBankNew.Name = "btnBankNew";
            this.btnBankNew.Size = new System.Drawing.Size(75, 23);
            this.btnBankNew.TabIndex = 2;
            this.btnBankNew.Text = "New";
            this.btnBankNew.Click += new System.EventHandler(this.btnBankNew_Click_1);
            // 
            // txtBank
            // 
            this.txtBank.Location = new System.Drawing.Point(213, 50);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(129, 20);
            this.txtBank.TabIndex = 1;
            this.txtBank.Visible = false;
            // 
            // lkeBank
            // 
            this.lkeBank.Location = new System.Drawing.Point(5, 50);
            this.lkeBank.Name = "lkeBank";
            this.lkeBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkeBank.Properties.NullText = "Select Bank";
            this.lkeBank.Size = new System.Drawing.Size(154, 20);
            this.lkeBank.TabIndex = 0;
            // 
            // BankMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "BankMaster";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.BankMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeBank.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnBankEdit;
        private DevExpress.XtraEditors.SimpleButton btnBankDelete;
        private DevExpress.XtraEditors.SimpleButton btnBankNew;
        private DevExpress.XtraEditors.TextEdit txtBank;
        private DevExpress.XtraEditors.LookUpEdit lkeBank;
    }
}

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
    public partial class SalaryUnPaid : DevExpress.XtraEditors.XtraUserControl
    {
        NumberToText nm = new NumberToText();
        StringBuilder m_sbQueryText = new StringBuilder();
       private int M1000 = 0, M500 = 0, M100 = 0, M50 = 0, M10 = 0, M5 = 0, M2 = 0, M1 = 0;

        db dbHandler = new db();
        public SalaryUnPaid()
        {
            InitializeComponent();
        }

        private void SalaryUnPaid_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            dbHandler.LoadUnPaidSalary(ds);
            gridControl1.DataSource = ds.Tables["nexusGarments_EmpAllogation_1"];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            M1000 = 0; M500 = 0; M100 = 0; M50 = 0; M10 = 0; M5 = 0; M2 = 0; M1 = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {

                int sal = Convert.ToInt32(gridView1.GetRowCellDisplayText(i, "salary").ToString());
               
                if (sal >= 1000)
                {
                    M1000 += (sal - (sal % 1000)) / 1000;
                    sal = (sal % 1000);
                }

                if (sal >= 500)
                {
                    M500 += (sal - (sal % 500)) / 500;
                    sal = (sal % 500);
                    // MessageBox.Show(M500.ToString());
                }

                if (sal >= 100)
                {
                    M100 += (sal - (sal % 100)) / 100;
                    sal = (sal % 100);
                    // MessageBox.Show(M500.ToString());
                }


                if (sal >= 50)
                {
                    M50 += (sal - (sal % 50)) / 50;
                    sal = (sal % 50);
                    // MessageBox.Show(M500.ToString());
                }

                if (sal >= 10)
                {
                    M10 += (sal - (sal % 10)) / 10;
                    sal = (sal % 10);
                    // MessageBox.Show(M500.ToString());
                }


                if (sal >= 5)
                {
                    M5 += (sal - (sal % 5)) / 5;
                    sal = (sal % 5);
                    // MessageBox.Show(M500.ToString());
                }


                if (sal >= 2)
                {
                    M2 += (sal - (sal % 2)) / 2;
                    sal = (sal % 2);
                    // MessageBox.Show(M500.ToString());
                }


                if (sal >= 1)
                {
                    M1 += (sal - (sal % 1)) / 1;
                    sal = (sal % 1);
                    // MessageBox.Show(M500.ToString());
                }


                

            }

            MessageBox.Show("1000 = " + M1000 + "\n 500 = " + M500 + "\n 100 = " + M100 + "\n 50 = " + M50 + "\n 10 = " + M10 + "\n 5 =" + M5 + "\n 2 = " + M2 + "\n 1 = " + M1);

        }




    }
}

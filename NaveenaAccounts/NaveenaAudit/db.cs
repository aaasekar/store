    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace NaveenaAccounts
{
    class db
    {
       // private Report.KnittingDC kdc;
        private string supplier, counts, delbags, delweight, gg, ll, gsm, dia, company, deldate, style, fabric,lbltip;
        private OdbcConnection m_odbcConnection, m_odbcConnection_1;
        private OdbcCommand m_odbcCommand, m_odbcCommand_1;
        private StringBuilder m_sbQueryText;
        private OdbcDataReader m_dataReader, m_dataReader1;
        private string dcno;
        public string preparedby, Approved, prefix, adminprefix;
        public Image PreparedSign, ApprovedSign;
        public string conns = "Data Source=TAGGIN-ADE7F3DA\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;";
        public string connString = "Data Source=TAGGIN-ADE7F3DA\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;";
        public int no;
        public db()
        {
            //string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"];
            m_odbcConnection = new OdbcConnection("Dsn=NexusGarments;uid=sa;pwd=openforsk");
            m_odbcCommand = new OdbcCommand();
            m_odbcCommand.Connection = m_odbcConnection;
            m_sbQueryText = new StringBuilder();


            m_odbcConnection_1 = new OdbcConnection("Dsn=NexusGarments;uid=sa;pwd=openforsk");
            m_odbcCommand_1 = new OdbcCommand();
            m_odbcCommand_1.Connection = m_odbcConnection_1;
            // m_sbQueryText_1 = new StringBuilder();
            //m_odbcConnection_1 = new OdbcConnection(connectionInfo);
            //m_odbcCommand_1 = new OdbcCommand();
            //m_odbcCommand_1.Connection = m_odbcConnection_1;

        }

        # region Common Functions 
        private void OpenDB()
        {
            if (m_odbcConnection.State != ConnectionState.Open)
            {
                m_odbcConnection.Open();
            }
        }




        private void OpenDB1()
        {
            if (m_odbcConnection_1.State != ConnectionState.Open)
            {
                m_odbcConnection_1.Open();
            }
        }
       
       public bool IsExists(string sQuery)
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = sQuery;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                    return true;
            }
            catch
            {
                ;
            }
            finally
            {
                CloseDB(1);
            }
            return false;
        }

        public bool UpdateQuery(string sQuery)
        {
            OdbcTransaction trans = null;
            m_odbcCommand.CommandText = sQuery;
            try
            {
                OpenDB();
                trans = m_odbcConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                m_odbcCommand.Transaction = trans;
                m_odbcCommand.ExecuteNonQuery();
                trans.Commit();
            }
            catch
            {
                if (trans != null)
                    trans.Rollback();
                return false;
            }
            finally
            {
                CloseDB(0);
            }
            return true;
        } 
        /// <summary>
        /// 
        /// Closes the database Connection with DataReader if iState=1.
        /// </summary>
        private void CloseDB(int iState)
        {
            if (iState == 1)
                if (m_dataReader != null)
                    if (!m_dataReader.IsClosed)
                        m_dataReader.Close();

            if (m_odbcConnection.State != ConnectionState.Closed)
                m_odbcConnection.Close();
        }



        private void CloseDB1(int iState)
        {
            if (iState == 1)
                if (m_dataReader1 != null)
                    if (!m_dataReader1.IsClosed)
                        m_dataReader1.Close();

            if (m_odbcConnection_1.State != ConnectionState.Closed)
                m_odbcConnection_1.Close();
        }

# endregion
        
        internal void loadDevDll(string sQuery, DevExpress.XtraEditors.CheckedComboBoxEdit Devddl, string field)
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = sQuery;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    Devddl.Properties.Items.Add(m_dataReader[field].ToString());
                   

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadProcessInfo(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Process ID");
            dt.Columns.Add("Buyer");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarmants_Master_Process.id,nexusGarments_Master_Buyer.BuyerName, nexusGarmants_Master_Process.ProcessID FROM nexusGarmants_Master_Process INNER JOIN nexusGarments_Master_Buyer ON nexusGarmants_Master_Process.BuyerId = nexusGarments_Master_Buyer.id where nexusGarmants_Master_Process.isclosed  = 'False'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessID"].ToString().Trim(), m_dataReader["BuyerName"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void loadUserid(string p, int sd )
        {



            m_dataReader = null;
            m_odbcCommand.CommandText = p;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                   sd = Convert.ToInt32(m_dataReader["id"].ToString());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void ApproveJobInward(string p)
        {
            string[] sd = p.Split('-');
            sd[0] = sd[0].Trim();
            sd[1] = sd[1].Trim();
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "SELECT     nexusGarments_StoreJobworkInwardLog.id FROM         nexusGarments_StoreJobworkInwardLog INNER JOIN nexusGarments_StoreJobPO ON nexusGarments_StoreJobworkInwardLog.ProcessID = nexusGarments_StoreJobPO.id where nexusGarments_StoreJobworkInwardLog.invoiceno='" + sd[1] + "' and nexusGarments_StoreJobPO.ponumber='" + sd[0] + "'";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    if (UpdateQuery("Update nexusGarments_StoreJobPO set recQuantity=recQuantity+(select inwardQuantity from nexusGarments_StoreJobworkInwardLog where id=" + m_dataReader1["id"].ToString() + ") where id  = (SELECT     nexusGarments_StoreJobPO.id FROM         nexusGarments_StoreJobworkInwardLog INNER JOIN nexusGarments_StoreJobPO ON nexusGarments_StoreJobworkInwardLog.ProcessID = nexusGarments_StoreJobPO.id where nexusGarments_StoreJobworkInwardLog.invoiceno='" + sd[1] + "' and nexusGarments_StoreJobPO.ponumber='" + sd[0] + "' and  nexusGarments_StoreJobworkInwardLog.id = " + m_dataReader1["id"].ToString() + "        )"))
                    {
                        if (UpdateQuery("Update nexusGarments_PattenResource set recPic=recPic+(select (select inwardQuantity from nexusGarments_StoreJobworkInwardLog where id=" + m_dataReader1["id"].ToString() + ") * (SELECT     nexusGarments_Master_Accessories.PPU FROM         nexusGarments_PattenResource INNER JOIN nexusGarments_Master_Accessories ON nexusGarments_PattenResource.accid = nexusGarments_Master_Accessories.id where nexusGarments_PattenResource.id=  (SELECT     nexusGarments_StoreJobworkInwardLog.kid FROM         nexusGarments_StoreJobworkInwardLog INNER JOIN nexusGarments_StoreJobPO ON nexusGarments_StoreJobworkInwardLog.ProcessID = nexusGarments_StoreJobPO.id where  nexusGarments_StoreJobworkInwardLog.id=" + m_dataReader1["id"].ToString() + " and nexusGarments_StoreJobworkInwardLog.invoiceno='" + sd[1] + "' and nexusGarments_StoreJobPO.ponumber='" + sd[0] + "')   )) where id =  (SELECT     nexusGarments_StoreJobworkInwardLog.kid FROM         nexusGarments_StoreJobworkInwardLog INNER JOIN nexusGarments_StoreJobPO ON nexusGarments_StoreJobworkInwardLog.ProcessID = nexusGarments_StoreJobPO.id where  nexusGarments_StoreJobworkInwardLog.id=" + m_dataReader1["id"].ToString() + " and nexusGarments_StoreJobworkInwardLog.invoiceno='" + sd[1] + "' and nexusGarments_StoreJobPO.ponumber='" + sd[0] + "')"))
                        {
                            UpdateQuery("Update nexusGarments_StoreJobworkInwardLog set isapproved='True',Approvedby="+Sess.id+" where id="+m_dataReader1["id"].ToString());
                        }
                    }
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }



        internal void LoadStoreJobInwardApproval(DataSet ds)
        {
            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT    DISTINCT nexusGarments_StoreJobworkInwardLog.isapproved as [Select],nexusGarments_StoreJobPO.ponumber +' - '+  nexusGarments_StoreJobworkInwardLog.invoiceno as [DC Number - Invoice Number], nexusGarments_OutSourceCompany.Company as Company, nexusGarments_OutSourceProcess.OutSourceProcess as Process FROM         nexusGarments_StoreJobPO INNER JOIN nexusGarments_OutSourceCompany ON nexusGarments_StoreJobPO.Company = nexusGarments_OutSourceCompany.id INNER JOIN nexusGarments_StoreJobworkInwardLog ON nexusGarments_StoreJobPO.id = nexusGarments_StoreJobworkInwardLog.ProcessID INNER JOIN nexusGarments_OutSourceProcess ON nexusGarments_StoreJobPO.JProcessID = nexusGarments_OutSourceProcess.id where nexusGarments_StoreJobworkInwardLog.isapproved='false'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT     nexusGarments_StoreJobPO.ponumber +' - '+nexusGarments_StoreJobworkInwardLog.invoiceno as [DC Number - Invoice Number], nexusGarments_Master_Accessories.accName as Accessories,  nexusGarments_Master_AccSize.size as Size, nexusGarments_PrintingColor.ColorName as Color, nexusGarments_StoreJobworkInwardLog.InwardQuantity FROM         nexusGarments_StoreJobworkInwardLog INNER JOIN nexusGarments_StoreJobPO ON nexusGarments_StoreJobworkInwardLog.processid = nexusGarments_StoreJobPO.id INNER JOIN nexusGarments_PattenResource ON nexusGarments_StoreJobPO.mapid = nexusGarments_PattenResource.id INNER JOIN nexusGarments_Master_Accessories ON nexusGarments_PattenResource.accid = nexusGarments_Master_Accessories.id INNER JOIN nexusGarments_Master_AccSize ON nexusGarments_PattenResource.sizeid = nexusGarments_Master_AccSize.sizeid INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PattenResource.colorid = nexusGarments_PrintingColor.id where nexusGarments_StoreJobworkInwardLog.isapproved = 'False'", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_StoreJobPO");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_StoreJobworkInwardLog");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                ds.Relations.Add("DeliverInformation",
                    ds.Tables["nexusGarments_StoreJobPO"].Columns["DC Number - Invoice Number"],
                    ds.Tables["nexusGarments_StoreJobworkInwardLog"].Columns["DC Number - Invoice Number"]);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }
        }


        
        internal void LoadStorePoInwardApproval(DataSet ds)
        {
            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT DISTINCT nexusGarments_StoreInwardLog.isapproved as [Select],nexusGarments_StorePO.ponumber as [PO Number], nexusGarments_Master_StoreSupplier.Supplier as [Supplier], nexusGarments_StoreInwardLog.invoiceno as [Invoice Number] FROM         nexusGarments_StoreInwardLog INNER JOIN nexusGarments_StorePO ON nexusGarments_StoreInwardLog.ProcessID = nexusGarments_StorePO.id INNER JOIN nexusGarments_Master_StoreSupplier ON nexusGarments_StorePO.supplier = nexusGarments_Master_StoreSupplier.id WHERE     (nexusGarments_StoreInwardLog.IsApproved = 'False')";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT     nexusGarments_Master_Accessories.accName as [Accessories], nexusGarments_Master_AccSize.size as Size, nexusGarments_PrintingColor.ColorName as Color,  nexusGarments_StoreInwardLog.inwardQuantity as [Inward Quantity], nexusGarments_StorePO.ponumber as [PO Number], nexusGarments_StoreInwardLog.invoiceno as [Invoice Number] FROM nexusGarments_StorePO INNER JOIN nexusGarments_StoreInwardLog ON nexusGarments_StorePO.id = nexusGarments_StoreInwardLog.ProcessID INNER JOIN nexusGarments_PattenResource ON nexusGarments_StorePO.mapid = nexusGarments_PattenResource.id INNER JOIN nexusGarments_Master_Accessories ON nexusGarments_PattenResource.accid = nexusGarments_Master_Accessories.id INNER JOIN nexusGarments_Master_AccSize ON nexusGarments_PattenResource.sizeid = nexusGarments_Master_AccSize.sizeid INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PattenResource.colorid = nexusGarments_PrintingColor.id where nexusGarments_StoreInwardLog.isapproved='False'", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_StoreInwardLog");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_StorePO");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[2];

                DataColumn[] da2 = new DataColumn[2];

                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"];
                da1[1] = ds.Tables["nexusGarments_StoreInwardLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_StorePO"].Columns["PO Number"];
                da2[1] = ds.Tables["nexusGarments_StorePO"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }
        }




        internal void LoadDyingColor(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Dying Color");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,DyingColor from nexus_Master_DyingColor";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["DyingColor"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadKnittingFabric(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Fabric");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select fabricId,fabricName from nexusGarments_Master_fabric";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["fabricId"].ToString().Trim(), m_dataReader["fabricName"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void loadDevDll(string sQuery, DevExpress.XtraEditors.ComboBoxEdit Devddl, string field)
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = sQuery;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    Devddl.Properties.Items.Add(m_dataReader[field].ToString());


                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void loadvisitorslog(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Log ID");
            dt.Columns.Add("Visitor Name");
            dt.Columns.Add("No of person");
            dt.Columns.Add("Department");
            dt.Columns.Add("Purpose");
            dt.Columns.Add("Company");
            dt.Columns.Add("Date Time");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select * from nexusGarments_VisitorsLog where islogout='0'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString(), m_dataReader["VisitorName"].ToString(), m_dataReader["NoPerson"].ToString(), m_dataReader["MeetSection"].ToString(), m_dataReader["PurposeDis"].ToString(), m_dataReader["FromCompanyDis"].ToString(), m_dataReader["InwardDateTime"].ToString());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }

        }

        internal bool LoadStaffMoventInfo(string sQuery, DevExpress.XtraEditors.LabelControl lblStaffName, DevExpress.XtraEditors.LabelControl lblPosition, DevExpress.XtraEditors.LabelControl lblDept)
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = sQuery;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    lblStaffName.Text = m_dataReader["Firstname"].ToString().Trim() + " " + m_dataReader["Lastname"].ToString().Trim();
                    lblPosition.Text = m_dataReader["PostDis"].ToString();
                    lblDept.Text = m_dataReader["DeptDis"].ToString();
                    return true;

                }
                else { 
                    return false;
                }
               
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                CloseDB(1);
            }
            return false;
        }

        internal void LoadStaffMoventGridInfo(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Staff ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Department");
            dt.Columns.Add("Position");
            dt.Columns.Add("Out Time");
           

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarmrnts_Master_Staff.staffid as staffid, nexusGarmrnts_Master_Staff.FirstName as Fstaffname,nexusGarmrnts_Master_Staff.LastName as Lstaffname, nexusGarmrnts_Master_Staff.DeptDis as dept, nexusGarments_StaffMovementLog.Outtime as Outtime,nexusGarmrnts_Master_Staff.PostDis as position FROM nexusGarments_StaffMovementLog Left JOIN nexusGarmrnts_Master_Staff ON nexusGarments_StaffMovementLog.Staffid = nexusGarmrnts_Master_Staff.Staffid where nexusGarments_StaffMovementLog.isout = '0'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["staffid"].ToString().Trim(),m_dataReader["Fstaffname"].ToString().Trim() + " " + m_dataReader["Lstaffname"].ToString().Trim(), m_dataReader["dept"].ToString(), m_dataReader["position"].ToString(), m_dataReader["Outtime"].ToString().Substring(0,10));

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }

        }

        internal void LoadInward(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Dc No");
            dt.Columns.Add("Department");
            dt.Columns.Add("Company");
            dt.Columns.Add("Process");
            dt.Columns.Add("Del On");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_Dept.DeptName, nexusGarments_Master_Company.CompanyName, nexusGarments_MaterialLog.DCNo,nexusGarments_MaterialLog.Process, nexusGarments_MaterialLog.OutwardDate FROM nexusGarments_MaterialLog LEFT JOIN nexusGarments_Master_Dept ON nexusGarments_MaterialLog.DelFrom = nexusGarments_Master_Dept.id LEFT JOIN nexusGarments_Master_Company ON nexusGarments_Master_Company.id = nexusGarments_MaterialLog.DelTo where nexusGarments_MaterialLog.isinward <> '1' and isoutward = '1';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["DCNo"].ToString().Trim(), m_dataReader["DeptName"].ToString().Trim(), m_dataReader["CompanyName"].ToString().Trim(), m_dataReader["Process"].ToString().Trim(), m_dataReader["OutwardDate"].ToString().Trim().Substring(0,10));

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }

            
        }

        internal bool  LoadInwardDetails(string sQuery, DevExpress.XtraEditors.LabelControl lblCompany, DevExpress.XtraEditors.LabelControl lblProcess, DevExpress.XtraEditors.LabelControl lblDeliveryTo)
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = sQuery;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    lblCompany.Text = m_dataReader["CompanyName"].ToString().Trim();
                    lblProcess.Text = m_dataReader["Process"].ToString().Trim();
                    lblDeliveryTo.Text = m_dataReader["DeptName"].ToString().Trim();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return false;
        }

        internal void loadInwardGrid(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Dc No");
            dt.Columns.Add("Department");
            dt.Columns.Add("Company");
            dt.Columns.Add("Process");
            dt.Columns.Add("Inward On");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_Dept.DeptName, nexusGarments_Master_Company.CompanyName, nexusGarments_MaterialLog.DCNo,nexusGarments_MaterialLog.Process, nexusGarments_MaterialLog.InwardDate FROM nexusGarments_MaterialLog LEFT JOIN nexusGarments_Master_Dept ON nexusGarments_MaterialLog.DelFrom = nexusGarments_Master_Dept.id LEFT JOIN nexusGarments_Master_Company ON nexusGarments_Master_Company.id = nexusGarments_MaterialLog.DelTo where nexusGarments_MaterialLog.isinward='1' and nexusGarments_MaterialLog.inwarddate < getdate() and nexusGarments_MaterialLog.inwarddate > getdate()-1 ;";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["DCNo"].ToString().Trim(), m_dataReader["DeptName"].ToString().Trim(), m_dataReader["CompanyName"].ToString().Trim(), m_dataReader["Process"].ToString().Trim(), m_dataReader["InwardDate"].ToString());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void loadInwardGridDs(DataSet ds)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Clear();
            dt1.Rows.Clear();
            dt1.Columns.Add("Dc No");
            dt1.Columns.Add("Department");
            dt1.Columns.Add("Company");
            dt1.Columns.Add("Process");
            dt1.Columns.Add("Inward On");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_Dept.DeptName, nexusGarments_Master_Company.CompanyName, nexusGarments_MaterialLog.DCNo,nexusGarments_MaterialLog.Process, nexusGarments_MaterialLog.InwardDate FROM nexusGarments_MaterialLog LEFT JOIN nexusGarments_Master_Dept ON nexusGarments_MaterialLog.DelFrom = nexusGarments_Master_Dept.id LEFT JOIN nexusGarments_Master_Company ON nexusGarments_Master_Company.id = nexusGarments_MaterialLog.DelTo where nexusGarments_MaterialLog.isinward='1' and nexusGarments_MaterialLog.inwarddate < getdate() and nexusGarments_MaterialLog.inwarddate > getdate()-1 ;";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt1.Rows.Add(m_dataReader["DCNo"].ToString().Trim(), m_dataReader["DeptName"].ToString().Trim(), m_dataReader["CompanyName"].ToString().Trim(), m_dataReader["Process"].ToString().Trim(), m_dataReader["InwardDate"].ToString());

                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadOutward(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Dc No");
            dt.Columns.Add("Department");
            dt.Columns.Add("Company");
            dt.Columns.Add("Process");
            dt.Columns.Add("Del On");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_Dept.DeptName, nexusGarments_Master_Company.CompanyName, nexusGarments_MaterialLog.DCNo,nexusGarments_MaterialLog.Process, nexusGarments_MaterialLog.OutwardDate FROM nexusGarments_MaterialLog LEFT JOIN nexusGarments_Master_Dept ON nexusGarments_MaterialLog.DelFrom = nexusGarments_Master_Dept.id LEFT JOIN nexusGarments_Master_Company ON nexusGarments_Master_Company.id = nexusGarments_MaterialLog.DelTo where nexusGarments_MaterialLog.isinward = '0' and isoutward = '0';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["DCNo"].ToString().Trim(), m_dataReader["DeptName"].ToString().Trim(), m_dataReader["CompanyName"].ToString().Trim(), m_dataReader["Process"].ToString().Trim(), m_dataReader["OutwardDate"].ToString().Trim().Substring(0, 10));

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }

        }

        internal void loadOutwardGrid(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Dc No");
            dt.Columns.Add("Department");
            dt.Columns.Add("Company");
            dt.Columns.Add("Process");
            dt.Columns.Add("Outward On");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_Dept.DeptName, nexusGarments_Master_Company.CompanyName, nexusGarments_MaterialLog.DCNo,nexusGarments_MaterialLog.Process, nexusGarments_MaterialLog.outwardDate FROM nexusGarments_MaterialLog LEFT JOIN nexusGarments_Master_Dept ON nexusGarments_MaterialLog.DelFrom = nexusGarments_Master_Dept.id LEFT JOIN nexusGarments_Master_Company ON nexusGarments_Master_Company.id = nexusGarments_MaterialLog.DelTo where nexusGarments_MaterialLog.isoutward='1' and isinward='0' and nexusGarments_MaterialLog.outwarddate < getdate() and nexusGarments_MaterialLog.outwarddate > getdate()-1 ;";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["DCNo"].ToString().Trim(), m_dataReader["DeptName"].ToString().Trim(), m_dataReader["CompanyName"].ToString().Trim(), m_dataReader["Process"].ToString().Trim(), m_dataReader["outwardDate"].ToString());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadYarnDCInfo(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();

            dt.Columns.Add("PO Number");
            
            dt.Columns.Add("Style"); 
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Process ID");
            dt.Columns.Add("PO Date", System.Type.GetType("System.DateTime"));
           

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT DISTINCT nexusGarments_YarnPurchase.dcno,nexusGarments_YarnPurchase.podate, nexusGarmants_Master_Process.ProcessID, nexusGarments_Master_Style.ProcessStyle,nexusGarments_Master_YarnSupplier.Supplier FROM nexusGarmants_Master_Process INNER JOIN nexusGarments_YarnPurchase ON nexusGarmants_Master_Process.id = nexusGarments_YarnPurchase.ProcessId INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id where nexusGarmants_Master_Process.isclosed <>'True'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["dcno"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["Supplier"].ToString().Trim(), m_dataReader["ProcessID"].ToString().Trim(), m_dataReader["podate"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void loadYarnPurchaceGrid(DataTable dt, string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Counts");
           
            dt.Columns.Add("Mill");
            
            dt.Columns.Add("Order Quantity");
            dt.Columns.Add("Avil Quantity");


            dt.Columns.Add("Bags", System.Type.GetType("System.Double"));
            dt.Columns.Add("Gross Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Net Weight", System.Type.GetType("System.Double"));


            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_YarnPurchase.id,nexusGarments_YarnPurchase.AvilQuantity,nexusGarments_Master_YarnSupplier.Supplier,nexusGarments_YarnPurchase.orderquantity, nexusGarmrnts_Master_Counts.Counts, nexusGarments_Master_Style.ProcessStyle FROM nexusGarments_YarnPurchase INNER JOIN  nexusGarments_Master_YarnSupplier AS nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarmrnts_Master_Counts  ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id  INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id where nexusGarments_YarnPurchase.dcno='" + dcno + "'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["counts"].ToString().Trim(),  m_dataReader["Supplier"].ToString().Trim(), m_dataReader["orderquantity"].ToString().Trim(), m_dataReader["AvilQuantity"].ToString().Trim(),  0, 0, 0);

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }
       
        internal void LoadDyingBaseColor(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Dying Base Color");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,DesignName from nexusGarments_Dyebasecolor";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dyebasecolor"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadKnittingLke(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Counts");
            dt.Columns.Add("Color");
            dt.Columns.Add("Bags", System.Type.GetType("System.Double"));
            dt.Columns.Add("Weight", System.Type.GetType("System.Double"));


            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_Color.Color, nexusGarmrnts_Master_Counts.Counts, nexusGarments_Master_Style.ProcessStyle, nexusGarments_YarnPurchase.id,nexusGarments_YarnPurchase.avilquantity, nexusGarments_YarnPurchase.bags FROM nexusGarments_Master_Color INNER JOIN nexusGarments_Master_Style ON nexusGarments_Master_Color.id = nexusGarments_Master_Style.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_Master_Color.id = nexusGarments_YarnPurchase.colorId AND nexusGarments_Master_Style.id = nexusGarments_YarnPurchase.StyleId INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id where nexusGarments_YarnPurchase.isclosed='true'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(),m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["counts"].ToString().Trim(), m_dataReader["color"].ToString().Trim(), m_dataReader["bags"].ToString().Trim(), m_dataReader["avilquantity"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }

        internal void LoadKnittingLkepo (DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Style");
            dt.Columns.Add("Knitting Company");
            dt.Columns.Add("Counts");
            dt.Columns.Add("Bags");
            dt.Columns.Add("Avail Quantity");
            dt.Columns.Add("gg");
            dt.Columns.Add("ll");
            dt.Columns.Add("dia");
            dt.Columns.Add("gsm");
            dt.Columns.Add("Req weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("supplier");
            dt.Columns.Add("delweight", System.Type.GetType("System.Double"));
            dt.Columns.Add("knitid");
            //dt.Columns.Add("id");
           
           
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT  nexusGarments_Master_YarnSupplier.Supplier,nexusGarments_KnittingProgram.id AS knitid, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll, nexusGarments_KnittingProgram.reqWeight, nexusGarments_YarnSource.recweight - nexusGarments_YarnSource.delweight AS avilquantity,  nexusGarments_YarnSource.recbags - nexusGarments_YarnSource.delbags AS bags, nexusGarments_Master_KnittingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_YarnSource.id, nexusGarments_YarnPurchase.SupplierId, nexusGarments_Master_Style.ProcessStyle,  nexusGarmrnts_Master_Counts.Counts, nexusGarments_KnittingProgram.delweight FROM   nexusGarments_KnittingProgram INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id LEFT OUTER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id where  nexusGarments_YarnSource.recweight - nexusGarments_YarnSource.delweight > 0 and nexusGarments_KnittingProgram.isprocessclosed <> 'True' and  nexusGarments_KnittingProgram.reqWeight  -  nexusGarments_KnittingProgram.delweight <> 0 order by nexusGarments_KnittingProgram.id asc";
            try
            { string pid = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                   
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    if (pid != m_dataReader["knitid"].ToString().Trim())
                    {
                        pid = m_dataReader["knitid"].ToString().Trim();
                        dt.Rows.Add(m_dataReader["knitid"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["counts"].ToString().Trim(), m_dataReader["bags"].ToString().Trim(), m_dataReader["avilquantity"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["gsm"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim(), m_dataReader["supplier"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["id"].ToString().Trim());
                    }

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadKnittingEditLkepo(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Style");
            dt.Columns.Add("Knitting Company");
            dt.Columns.Add("Counts");
            dt.Columns.Add("Bags");
            dt.Columns.Add("Avail Quantity");
            dt.Columns.Add("gg");
            dt.Columns.Add("ll");
            dt.Columns.Add("dia");
            dt.Columns.Add("gsm");
            dt.Columns.Add("Req weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("supplier");
            dt.Columns.Add("delweight", System.Type.GetType("System.Double"));
            dt.Columns.Add("knitid");
            //dt.Columns.Add("id");


            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT  nexusGarments_Master_YarnSupplier.Supplier,nexusGarments_KnittingProgram.id AS knitid, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll, nexusGarments_KnittingProgram.reqWeight, nexusGarments_YarnSource.recweight - nexusGarments_YarnSource.delweight AS avilquantity,  nexusGarments_YarnSource.recbags - nexusGarments_YarnSource.delbags AS bags, nexusGarments_Master_KnittingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_YarnSource.id, nexusGarments_YarnPurchase.SupplierId, nexusGarments_Master_Style.ProcessStyle,  nexusGarmrnts_Master_Counts.Counts, nexusGarments_KnittingProgram.delweight FROM   nexusGarments_KnittingProgram INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id LEFT OUTER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id where   nexusGarments_KnittingProgram.isprocessclosed <> 'True'  order by nexusGarments_KnittingProgram.id asc";
            try
            {
                string pid = "";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    if (pid != m_dataReader["knitid"].ToString().Trim())
                    {
                        pid = m_dataReader["knitid"].ToString().Trim();
                        dt.Rows.Add(m_dataReader["knitid"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["counts"].ToString().Trim(), m_dataReader["bags"].ToString().Trim(), m_dataReader["avilquantity"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["gsm"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim(), m_dataReader["supplier"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["knitid"].ToString().Trim());
                    }
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadKnittingOutWardGrid(DataTable Knittingdt, string p)
        {
            
        }

        internal void loadKnittingDelivery(DataTable dt,string pono)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Company");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Gsm");
            dt.Columns.Add("GG");
            dt.Columns.Add("LL");
            dt.Columns.Add("Req Weight", System.Type.GetType("System.Double"));

            dt.Columns.Add("Deliv Weight", System.Type.GetType("System.Double"));

            dt.Columns.Add("Deliv Bags", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_fabric.fabricName, nexusGarments_Master_KnittingCompany.Company, nexusGarments_Map_Process_Fabric.id,nexusGarments_Map_Process_Fabric.dia,nexusGarments_Map_Process_Fabric.gsm, nexusGarments_Map_Process_Fabric.gg, nexusGarments_Map_Process_Fabric.ll, nexusGarments_Map_Process_Fabric.reqWeight, nexusGarments_Map_Process_Fabric.poid FROM nexusGarments_Map_Process_Fabric INNER JOIN nexusGarments_Master_fabric ON nexusGarments_Map_Process_Fabric.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_Map_Process_Fabric.Companyid = nexusGarments_Master_KnittingCompany.id where nexusGarments_Map_Process_Fabric.id  = '" + pono + "'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["gsm"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim(), m_dataReader["reqWeight"].ToString().Trim(), "", "");

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }

        internal void LoadDyingComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,company from nexusGarmrnts_Master_DyingCompany";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadComptComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,company from nexusGarments_Master_CompactingCompany";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadKnittingDCLkepo(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("DC NO");
            dt.Columns.Add("Company");
            dt.Columns.Add("Bags");
            dt.Columns.Add("Weight");

           
            dt.Columns.Add("mapid");
            dt.Columns.Add("gg");
            dt.Columns.Add("ll");
            dt.Columns.Add("gsm");
            dt.Columns.Add("dia");
            dt.Columns.Add("deldate");
            dt.Columns.Add("recWeights", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_KnittingProcess.recWeight,nexusGarments_Master_KnittingCompany.Company, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm,nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll, nexusGarments_KnittingProcess.id, nexusGarments_KnittingProcess.DCNo,nexusGarments_KnittingProcess.delBags, nexusGarments_KnittingProcess.delWeight, nexusGarments_KnittingProcess.deldate,nexusGarments_KnittingProcess.mapid FROM nexusGarments_KnittingProgram INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_KnittingProgram.id = nexusGarments_KnittingProcess.mapid  where nexusGarments_KnittingProcess.isclosed = 'False' and  nexusGarments_knittingprogram.isprocessclosed='False'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["delBags"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim(), m_dataReader["gsm"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadKnittingDC(DataTable dt,string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("DC NO");
            dt.Columns.Add("Company");
            dt.Columns.Add("Bags");
            dt.Columns.Add("Weight");


            dt.Columns.Add("mapid");
            dt.Columns.Add("gg");
            dt.Columns.Add("ll");
            dt.Columns.Add("gsm");
            dt.Columns.Add("dia");
            dt.Columns.Add("deldate");
            dt.Columns.Add("recWeights", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_KnittingProcess.recWeight,nexusGarments_Master_KnittingCompany.Company, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm,nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll, nexusGarments_KnittingProcess.id, nexusGarments_KnittingProcess.DCNo,nexusGarments_KnittingProcess.delBags, nexusGarments_KnittingProcess.delWeight, nexusGarments_KnittingProcess.deldate,nexusGarments_KnittingProcess.mapid FROM nexusGarments_KnittingProgram INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_KnittingProgram.id = nexusGarments_KnittingProcess.mapid  where nexusGarments_KnittingProcess.dcno='"+dcno+"'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["delBags"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim(), m_dataReader["gsm"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void loadKnittingInward(DataTable dt, string id)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
           
            dt.Columns.Add("GG");
            dt.Columns.Add("LL");
            dt.Columns.Add("GSM");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Bage");
            dt.Columns.Add("Weight");

            dt.Columns.Add("Res Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Res Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Yarn Retund", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_KnittingProcess.dcno,nexusGarments_KnittingProcess.id,nexusGarments_KnittingProcess.delBags,nexusGarments_KnittingProcess.delweight, nexusGarments_KnittingProcess.deldate, nexusGarments_Map_Process_Fabric.ll,nexusGarments_Map_Process_Fabric.gg, nexusGarments_Map_Process_Fabric.gsm, nexusGarments_Map_Process_Fabric.dia,nexusGarments_Master_KnittingCompany_1.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle FROM nexusGarments_KnittingProcess INNER JOIN nexusGarments_Map_Process_Fabric ON nexusGarments_KnittingProcess.mapid = nexusGarments_Map_Process_Fabric.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_Map_Process_Fabric.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_KnittingCompany AS nexusGarments_Master_KnittingCompany_1 ON  nexusGarments_Map_Process_Fabric.Companyid = nexusGarments_Master_KnittingCompany_1.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_Map_Process_Fabric.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id where  nexusGarments_KnittingProcess.id='" +id + "'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim(), m_dataReader["gsm"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(),m_dataReader["delBags"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(),"","","" );

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadHeatSettingDel(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Company");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Rolls");
            dt.Columns.Add("Weight");
            dt.Columns.Add("F GSM");
            dt.Columns.Add("H GSM");


            dt.Columns.Add("ReqWeight", System.Type.GetType("System.Double"));

            dt.Columns.Add("delWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("delrolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("recWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recrolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("kid");
            dt.Columns.Add("pro");
            //dt.Columns.Add("pid");
           m_dataReader = null;
           m_odbcCommand.CommandText = "SELECT     nexusGarments_KnittingProgram.id AS pid, nexusGarments_ProcessWeight.id AS kid,  nexusGarments_ProcessWeight.recweight - nexusGarments_ProcessWeight.desweight AS lotweight,  nexusGarments_ProcessWeight.recrolls - nexusGarments_ProcessWeight.desrolls AS lotrolls, nexusGarments_Master_fabric.fabricName,  nexusGarments_Master_Style.ProcessStyle, nexusGarments_HeatSettingProgram.FGSM, nexusGarments_HeatSettingProgram.id, nexusGarments_HeatSettingProgram.delweight, nexusGarments_HeatSettingProgram.recrolls, nexusGarments_HeatSettingProgram.recweight, nexusGarments_HeatSettingProgram.delrolls, nexusGarments_HeatSettingProgram.ReqWeight,  nexusGarments_Master_HeatSettingCompany.Company AS heatsettingcomp, nexusGarments_HeatSettingProgram.HGSM, nexusGarments_HeatSettingProgram.dia,  'K' AS pro FROM         nexusGarments_HeatSettingProgram INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_HeatSettingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_HeatSettingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_HeatSettingCompany ON nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_HeatSettingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_KnittingProgram.Ksource AND  nexusGarments_HeatSettingProgram.fabricid = nexusGarments_KnittingProgram.Fabricid WHERE     (nexusGarments_ProcessWeight.IsKwashing = 'False') AND (nexusGarments_ProcessWeight.isHeatSetting = 'true') AND  (nexusGarments_ProcessWeight.IsProcessClosed <> 'True') AND (nexusGarments_ProcessWeight.recweight - nexusGarments_ProcessWeight.desweight > 0)";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["processstyle"].ToString().Trim(), m_dataReader["heatsettingcomp"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["lotrolls"].ToString().Trim(), m_dataReader["lotWeight"].ToString().Trim(), m_dataReader["hgsm"].ToString().Trim(), m_dataReader["FGSM"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["kid"].ToString().Trim(), m_dataReader["pro"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            
        }

        internal void LoadHeatSettingDC(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();

            dt.Columns.Add("id");
            dt.Columns.Add("DC No");
            dt.Columns.Add("Company");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Style");
            dt.Columns.Add("mapid");
            dt.Columns.Add("Del Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("F GSM");
            dt.Columns.Add("H GSM");
            dt.Columns.Add("Dia");


            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT  nexusGarments_Master_fabric.fabricName,nexusGarments_HeatSettingProcess.recWeight, nexusGarments_HeatSettingProcess.recRolls, nexusGarments_HeatSettingProcess.delWeight,nexusGarments_HeatSettingProcess.delrolls, nexusGarments_HeatSettingProcess.dcno, nexusGarments_HeatSettingProcess.mapid,    nexusGarments_HeatSettingProcess.id, nexusGarments_heatsettingprogram.dia, nexusGarments_Master_HeatSettingCompany.Company AS heatsetting,  nexusGarments_Master_Style.ProcessStyle, nexusGarments_heatsettingprogram.hgsm, nexusGarments_HeatSettingProgram.FGSM FROM nexusGarments_HeatSettingProcess INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id   INNER JOIN nexusGarments_Master_HeatSettingCompany ON nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_heatsettingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_heatsettingprogram.Fabricid = nexusGarments_Master_fabric.fabricId where nexusGarments_heatsettingprocess.isclosed ='False' and nexusGarments_heatsettingprocess.isapproved ='True' ";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["heatsetting"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["FGSM"].ToString().Trim(), m_dataReader["hgsm"].ToString().Trim(), m_dataReader["dia"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadDyingReadyProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Dying Company");
            
            dt.Columns.Add("Dying Color");
            dt.Columns.Add("inlotrolls");
            dt.Columns.Add("inlotweight");

            dt.Columns.Add("delWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("delRolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("recWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recRolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("reqWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("isheatsetting");
            dt.Columns.Add("mapid");
            dt.Columns.Add("comptComp");
            dt.Columns.Add("ComptMethod");
            dt.Columns.Add("pro");
            dt.Columns.Add("pid");


            m_dataReader = null;
            //SELECT     nexusGarments_KnittingProgram.isHeatSetting, nexusGarments_DyingProgram.id, nexusGarmrnts_Master_DyingCompany.Company,  nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexus_Master_DyingColor.DyingColor,nexusGarments_Dyebasecolor.dyebasecolor, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls, nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProgram.heatrecweight - nexusGarments_KnittingProgram.heatdelweight AS Expr1, nexusGarments_KnittingProgram.heatrecrolls - nexusGarments_KnittingProgram.heatdelrolls AS Expr2, nexusGarments_KnittingProgram.id AS Expr3,  nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod FROM       nexusGarments_DyingProgram   INNER JOIN   nexus_Master_DyingColor ON nexus_Master_DyingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN  nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN  nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_KnittingProgram.id = nexusGarments_HeatSettingProgram.kid INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN   nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id WHERE     (nexusGarments_KnittingProgram.isHeatSetting = 'True') AND (nexusGarments_HeatSettingProgram.recweight - nexusGarments_HeatSettingProgram.desweight > 0)   AND (nexusGarments_KnittingProgram.IsProcessClosed <> 'True') UNION SELECT     nexusGarments_KnittingProgram.isHeatSetting, nexusGarments_DyingProgram.id, nexusGarmrnts_Master_DyingCompany.Company,nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexus_Master_DyingColor.DyingColor,nexusGarments_Dyebasecolor.dyebasecolor, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls, nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProgram.recweight - nexusGarments_KnittingProgram.desweight AS Expr1,   nexusGarments_KnittingProgram.recrolls - nexusGarments_KnittingProgram.desrolls AS Expr2, nexusGarments_KnittingProgram.id AS Expr3, nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod FROM        nexusGarments_DyingProgram  INNER JOIN    nexus_Master_DyingColor ON nexus_Master_DyingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN   nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id WHERE     (nexusGarments_KnittingProgram.isHeatSetting = 'False') AND (nexusGarments_KnittingProgram.recweight - nexusGarments_KnittingProgram.desweight > 0) AND  (nexusGarments_KnittingProgram.IsProcessClosed <> 'True')
            m_odbcCommand.CommandText = "SELECT     nexusGarments_HeatSettingProgram.id AS pid, nexusGarments_ProcessWeight.isHeatSetting, nexusGarments_DyingProgram.id,  nexusGarmrnts_Master_DyingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_DyingProgram.dia,  nexusGarments_PrintingColor.ColorName, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls,  nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,  nexusGarments_Master_Style.ProcessStyle,nexusGarments_ProcessWeight.HeatRecRolls - nexusGarments_ProcessWeight.HeatDelRolls AS Expr1,  nexusGarments_ProcessWeight.HeatRecweight - nexusGarments_ProcessWeight.Heatdelweight AS Expr2, nexusGarments_ProcessWeight.id AS Expr3,  nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod, 'H' AS pro FROM         nexusGarments_DyingProgram INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PrintingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_DyingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_DyingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_DyingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_HeatSettingProgram.Ksource AND  nexusGarments_Master_fabric.fabricId = nexusGarments_HeatSettingProgram.fabricid AND  nexusGarments_Master_Style.id = nexusGarments_HeatSettingProgram.styleid AND  nexusGarments_DyingProgram.fabricid = nexusGarments_HeatSettingProgram.fabricid WHERE     (nexusGarments_ProcessWeight.isHeatSetting = 'True') AND (nexusGarments_ProcessWeight.HeatRecWeight - nexusGarments_ProcessWeight.HeatDelWeight > 0)  AND (nexusGarments_ProcessWeight.IsProcessClosed <> 'True')  UNION SELECT     nexusGarments_KnittingProgram.id AS pid, nexusGarments_ProcessWeight.isHeatSetting, nexusGarments_DyingProgram.id,  nexusGarmrnts_Master_DyingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_DyingProgram.dia,  nexusGarments_PrintingColor.ColorName, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls,  nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,  nexusGarments_Master_Style.ProcessStyle,nexusGarments_ProcessWeight.recrolls - nexusGarments_ProcessWeight.desrolls AS Expr1, nexusGarments_ProcessWeight.recweight - nexusGarments_ProcessWeight.desweight AS Expr2,   nexusGarments_ProcessWeight.id AS Expr3,  nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod, 'K' AS pro FROM         nexusGarments_DyingProgram INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PrintingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_DyingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_DyingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_DyingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN  nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id INNER JOIN  nexusGarments_KnittingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_KnittingProgram.Ksource AND  nexusGarments_DyingProgram.fabricid = nexusGarments_KnittingProgram.Fabricid WHERE     (nexusGarments_ProcessWeight.isHeatSetting = 'False') AND (nexusGarments_ProcessWeight.IsKwashing = 'False') AND (nexusGarments_ProcessWeight.recweight - nexusGarments_ProcessWeight.desWeight > 0) AND (nexusGarments_ProcessWeight.IsProcessClosed <> 'True') AND (nexusGarments_KnittingProgram.recweight > 0)";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["ColorName"].ToString().Trim(), m_dataReader["expr1"].ToString().Trim(), m_dataReader["expr2"].ToString().Trim(), m_dataReader["delWeight"].ToString().Trim(), m_dataReader["delRolls"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim(), m_dataReader["recRolls"].ToString().Trim(), m_dataReader["reqWeight"].ToString().Trim(), m_dataReader["isheatsetting"].ToString().Trim(), m_dataReader["expr3"].ToString().Trim(), m_dataReader["expr4"].ToString().Trim(), m_dataReader["CompMethod"].ToString().Trim(), m_dataReader["pro"].ToString().Trim(), m_dataReader["pid"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }

        internal void LoadPrintingReadyProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Print Company");
            dt.Columns.Add("Design");
            dt.Columns.Add("Color");
            dt.Columns.Add("inlotrolls");
            dt.Columns.Add("inlotweight");

            dt.Columns.Add("delWeight");
            dt.Columns.Add("delRolls");
            dt.Columns.Add("recWeight");
            dt.Columns.Add("recRolls");
            dt.Columns.Add("reqWeight");
            
            dt.Columns.Add("mapid");
            dt.Columns.Add("pro");
            dt.Columns.Add("did");

            m_dataReader = null;
            //SELECT     nexusGarments_KnittingProgram.isHeatSetting, nexusGarments_DyingProgram.id, nexusGarmrnts_Master_DyingCompany.Company,  nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexus_Master_DyingColor.DyingColor,nexusGarments_Dyebasecolor.dyebasecolor, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls, nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProgram.heatrecweight - nexusGarments_KnittingProgram.heatdelweight AS Expr1, nexusGarments_KnittingProgram.heatrecrolls - nexusGarments_KnittingProgram.heatdelrolls AS Expr2, nexusGarments_KnittingProgram.id AS Expr3,  nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod FROM       nexusGarments_DyingProgram   INNER JOIN   nexus_Master_DyingColor ON nexus_Master_DyingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN  nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN  nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_KnittingProgram.id = nexusGarments_HeatSettingProgram.kid INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN   nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id WHERE     (nexusGarments_KnittingProgram.isHeatSetting = 'True') AND (nexusGarments_HeatSettingProgram.recweight - nexusGarments_HeatSettingProgram.desweight > 0)   AND (nexusGarments_KnittingProgram.IsProcessClosed <> 'True') UNION SELECT     nexusGarments_KnittingProgram.isHeatSetting, nexusGarments_DyingProgram.id, nexusGarmrnts_Master_DyingCompany.Company,nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexus_Master_DyingColor.DyingColor,nexusGarments_Dyebasecolor.dyebasecolor, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls, nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProgram.recweight - nexusGarments_KnittingProgram.desweight AS Expr1,   nexusGarments_KnittingProgram.recrolls - nexusGarments_KnittingProgram.desrolls AS Expr2, nexusGarments_KnittingProgram.id AS Expr3, nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod FROM        nexusGarments_DyingProgram  INNER JOIN    nexus_Master_DyingColor ON nexus_Master_DyingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN   nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id WHERE     (nexusGarments_KnittingProgram.isHeatSetting = 'False') AND (nexusGarments_KnittingProgram.recweight - nexusGarments_KnittingProgram.desweight > 0) AND  (nexusGarments_KnittingProgram.IsProcessClosed <> 'True')


//            SELECT     nexusGarments_PrintingProgram.id, nexusGarments_PrintingProgram.Ksource AS kid, nexusGarments_PrintingDesign.DesignName, 
//                      nexusGarments_PrintingColor.ColorName, nexusGarments_PrintingProgram.reqWeight, nexusGarments_PrintingProgram.delWeight, 
//                      nexusGarments_PrintingProgram.delRolls, nexusGarments_PrintingProgram.recWeight, nexusGarments_PrintingProgram.recRolls, 
//                      nexusGarments_ProcessWeight.recrolls - nexusGarments_ProcessWeight.desrolls AS availrolls, 
//                      nexusGarments_ProcessWeight.recweight - nexusGarments_ProcessWeight.desWeight AS availweight, nexusGarments_Master_fabric.fabricName, 
//                      nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingCompany.Company, nexusGarments_PrintingProgram.dia, '1' AS pro, '1' AS did
//FROM         nexusGarments_PrintingProgram INNER JOIN
//                      nexusGarments_ProcessWeight ON nexusGarments_PrintingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN
//                      nexusGarments_Master_Style ON nexusGarments_PrintingProgram.StyleID = nexusGarments_Master_Style.id INNER JOIN
//                      nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.FabricID = nexusGarments_Master_fabric.fabricId INNER JOIN
//                      nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN
//                      nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN
//                      nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id
//WHERE     (nexusGarments_ProcessWeight.IsKwashing = 'False') AND (nexusGarments_ProcessWeight.isHeatSetting = 'False') AND 
//                      (nexusGarments_ProcessWeight.isDying = 'False') AND (nexusGarments_ProcessWeight.isPrinting = 'True') AND 
//                      (nexusGarments_ProcessWeight.recweight - nexusGarments_ProcessWeight.desWeight > 0)
//UNION
//SELECT     nexusGarments_PrintingProgram.id, nexusGarments_PrintingProgram.Ksource AS kid, nexusGarments_PrintingDesign.DesignName, 
//                      nexusGarments_PrintingColor.ColorName, nexusGarments_PrintingProgram.reqWeight, nexusGarments_PrintingProgram.delWeight, 
//                      nexusGarments_PrintingProgram.delRolls, nexusGarments_PrintingProgram.recWeight, nexusGarments_PrintingProgram.recRolls, 
//                      nexusGarments_ProcessWeight.KWRecRolls - nexusGarments_ProcessWeight.KWDelRolls AS availrolls, 
//                      nexusGarments_ProcessWeight.KWRecWeight - nexusGarments_ProcessWeight.KWDelWeight AS availweight, nexusGarments_Master_fabric.fabricName, 
//                      nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingCompany.Company, nexusGarments_PrintingProgram.dia, '2' AS pro, '1' AS did
//FROM         nexusGarments_PrintingProgram INNER JOIN
//                      nexusGarments_ProcessWeight ON nexusGarments_PrintingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN
//                      nexusGarments_Master_Style ON nexusGarments_PrintingProgram.StyleID = nexusGarments_Master_Style.id INNER JOIN
//                      nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.FabricID = nexusGarments_Master_fabric.fabricId INNER JOIN
//                      nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN
//                      nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN
//                      nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id
//WHERE     (nexusGarments_ProcessWeight.IsKwashing = 'True') AND (nexusGarments_ProcessWeight.isHeatSetting = 'False') AND 
//                      (nexusGarments_ProcessWeight.isDying = 'False') AND (nexusGarments_ProcessWeight.isPrinting = 'True') AND 
//                      (nexusGarments_ProcessWeight.KWRecWeight - nexusGarments_ProcessWeight.KWDelWeight > 0)
//UNION
//SELECT     nexusGarments_PrintingProgram.id, nexusGarments_PrintingProgram.Ksource AS kid, nexusGarments_PrintingDesign.DesignName, 
//                      nexusGarments_PrintingColor.ColorName, nexusGarments_PrintingProgram.reqWeight, nexusGarments_PrintingProgram.delWeight, 
//                      nexusGarments_PrintingProgram.delRolls, nexusGarments_PrintingProgram.recWeight, nexusGarments_PrintingProgram.recRolls, 
//                      nexusGarments_ProcessWeight.HeatRecRolls - nexusGarments_ProcessWeight.HeatDelRolls AS availrolls, 
//                      nexusGarments_ProcessWeight.HeatRecWeight - nexusGarments_ProcessWeight.HeatDelWeight AS availweight, nexusGarments_Master_fabric.fabricName, 
//                      nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingCompany.Company, nexusGarments_PrintingProgram.dia, '3' AS pro, '1' AS did
//FROM         nexusGarments_PrintingProgram INNER JOIN
//                      nexusGarments_ProcessWeight ON nexusGarments_PrintingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN
//                      nexusGarments_Master_Style ON nexusGarments_PrintingProgram.StyleID = nexusGarments_Master_Style.id INNER JOIN
//                      nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.FabricID = nexusGarments_Master_fabric.fabricId INNER JOIN
//                      nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN
//                      nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN
//                      nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id
//WHERE     (nexusGarments_ProcessWeight.isHeatSetting = 'True') AND (nexusGarments_ProcessWeight.isDying = 'False') AND 
//                      (nexusGarments_ProcessWeight.isPrinting = 'True') AND (nexusGarments_ProcessWeight.HeatRecWeight - nexusGarments_ProcessWeight.HeatDelWeight > 0)
//UNION

            m_odbcCommand.CommandText = "SELECT     nexusGarments_PrintingProgram.id, nexusGarments_PrintingProgram.Ksource AS kid, nexusGarments_PrintingDesign.DesignName,  nexusGarments_PrintingColor.ColorName, nexusGarments_PrintingProgram.reqWeight, nexusGarments_PrintingProgram.delWeight,  nexusGarments_PrintingProgram.delRolls, nexusGarments_PrintingProgram.recWeight, nexusGarments_PrintingProgram.recRolls,  nexusGarments_ProcessWeight.DyeRecRolls - nexusGarments_ProcessWeight.DyeDelRolls AS availrolls,  nexusGarments_ProcessWeight.DyeRecWeight - nexusGarments_ProcessWeight.DyeDelWeight AS availweight, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingCompany.Company, nexusGarments_PrintingProgram.dia, '4' AS pro,  nexusGarments_DyingProgram.id AS did FROM         nexusGarments_PrintingProgram INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_PrintingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_PrintingProgram.StyleID = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.FabricID = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id INNER JOIN nexusGarments_DyingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_DyingProgram.Ksource AND  nexusGarments_Master_Style.id = nexusGarments_DyingProgram.styleid AND nexusGarments_Master_fabric.fabricId = nexusGarments_DyingProgram.fabricid AND  nexusGarments_PrintingColor.id = nexusGarments_DyingProgram.dyingColor WHERE     (nexusGarments_ProcessWeight.isDying = 'True') AND (nexusGarments_ProcessWeight.isPrinting = 'True') AND (nexusGarments_ProcessWeight.DyeRecWeight - nexusGarments_ProcessWeight.DyeDelWeight > 0) AND (nexusGarments_DyingProgram.recWeight > 0)";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["designname"].ToString().Trim(), m_dataReader["ColorName"].ToString().Trim(), m_dataReader["availRolls"].ToString().Trim(), m_dataReader["availWeight"].ToString().Trim(), m_dataReader["delWeight"].ToString().Trim(), m_dataReader["delRolls"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim(), m_dataReader["recRolls"].ToString().Trim(), m_dataReader["reqWeight"].ToString().Trim(), m_dataReader["kid"].ToString().Trim(), m_dataReader["pro"].ToString().Trim(), m_dataReader["did"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadEXProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Process");


            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     * from nexusGarments_LotEXProcess ";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["Process"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadEXCompany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");


            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     id,Company from nexusGarments_LotExCompany ";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["Company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }





        internal void LoadDyingExProcessReadyProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
           
            
            dt.Columns.Add("Color");
            dt.Columns.Add("inlotrolls");
            dt.Columns.Add("inlotweight");

           
            
          

            m_dataReader = null;
            //SELECT     nexusGarments_KnittingProgram.isHeatSetting, nexusGarments_DyingProgram.id, nexusGarmrnts_Master_DyingCompany.Company,  nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexus_Master_DyingColor.DyingColor,nexusGarments_Dyebasecolor.dyebasecolor, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls, nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProgram.heatrecweight - nexusGarments_KnittingProgram.heatdelweight AS Expr1, nexusGarments_KnittingProgram.heatrecrolls - nexusGarments_KnittingProgram.heatdelrolls AS Expr2, nexusGarments_KnittingProgram.id AS Expr3,  nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod FROM       nexusGarments_DyingProgram   INNER JOIN   nexus_Master_DyingColor ON nexus_Master_DyingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN  nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN  nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_KnittingProgram.id = nexusGarments_HeatSettingProgram.kid INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN   nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id WHERE     (nexusGarments_KnittingProgram.isHeatSetting = 'True') AND (nexusGarments_HeatSettingProgram.recweight - nexusGarments_HeatSettingProgram.desweight > 0)   AND (nexusGarments_KnittingProgram.IsProcessClosed <> 'True') UNION SELECT     nexusGarments_KnittingProgram.isHeatSetting, nexusGarments_DyingProgram.id, nexusGarmrnts_Master_DyingCompany.Company,nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexus_Master_DyingColor.DyingColor,nexusGarments_Dyebasecolor.dyebasecolor, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls, nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProgram.recweight - nexusGarments_KnittingProgram.desweight AS Expr1,   nexusGarments_KnittingProgram.recrolls - nexusGarments_KnittingProgram.desrolls AS Expr2, nexusGarments_KnittingProgram.id AS Expr3, nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod FROM        nexusGarments_DyingProgram  INNER JOIN    nexus_Master_DyingColor ON nexus_Master_DyingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN   nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id WHERE     (nexusGarments_KnittingProgram.isHeatSetting = 'False') AND (nexusGarments_KnittingProgram.recweight - nexusGarments_KnittingProgram.desweight > 0) AND  (nexusGarments_KnittingProgram.IsProcessClosed <> 'True')
            m_odbcCommand.CommandText = "SELECT     nexusGarments_DyingProgram.id, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingColor.ColorName, nexusGarments_DyingProgram.recWeight - nexusGarments_DyingProgram.desWeight as inlotweight, nexusGarments_DyingProgram.recRolls - nexusGarments_DyingProgram.desRolls as inlotrolls FROM         nexusGarments_DyingProgram INNER JOIN nexusGarments_Master_fabric ON nexusGarments_DyingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_DyingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_DyingProgram.Ksource = nexusGarments_ProcessWeight.id where nexusGarments_DyingProgram.recWeight - nexusGarments_DyingProgram.desWeight > 0";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(),  m_dataReader["ColorName"].ToString().Trim(), m_dataReader["inlotweight"].ToString().Trim(), m_dataReader["inlotrolls"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadPrintingEditProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Print Company");
            dt.Columns.Add("Design");
            dt.Columns.Add("Color");
            dt.Columns.Add("inlotrolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("inlotweight", System.Type.GetType("System.Double"));

            dt.Columns.Add("delWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("delRolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("recWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recRolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("reqWeight", System.Type.GetType("System.Double"));

            dt.Columns.Add("mapid");
            dt.Columns.Add("pro");

            m_dataReader = null;
            //SELECT     nexusGarments_KnittingProgram.isHeatSetting, nexusGarments_DyingProgram.id, nexusGarmrnts_Master_DyingCompany.Company,  nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexus_Master_DyingColor.DyingColor,nexusGarments_Dyebasecolor.dyebasecolor, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls, nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProgram.heatrecweight - nexusGarments_KnittingProgram.heatdelweight AS Expr1, nexusGarments_KnittingProgram.heatrecrolls - nexusGarments_KnittingProgram.heatdelrolls AS Expr2, nexusGarments_KnittingProgram.id AS Expr3,  nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod FROM       nexusGarments_DyingProgram   INNER JOIN   nexus_Master_DyingColor ON nexus_Master_DyingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN  nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN  nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_KnittingProgram.id = nexusGarments_HeatSettingProgram.kid INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN   nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id WHERE     (nexusGarments_KnittingProgram.isHeatSetting = 'True') AND (nexusGarments_HeatSettingProgram.recweight - nexusGarments_HeatSettingProgram.desweight > 0)   AND (nexusGarments_KnittingProgram.IsProcessClosed <> 'True') UNION SELECT     nexusGarments_KnittingProgram.isHeatSetting, nexusGarments_DyingProgram.id, nexusGarmrnts_Master_DyingCompany.Company,nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexus_Master_DyingColor.DyingColor,nexusGarments_Dyebasecolor.dyebasecolor, nexusGarments_DyingProgram.delWeight, nexusGarments_DyingProgram.delRolls, nexusGarments_DyingProgram.recWeight, nexusGarments_DyingProgram.recRolls, nexusGarments_DyingProgram.reqWeight,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProgram.recweight - nexusGarments_KnittingProgram.desweight AS Expr1,   nexusGarments_KnittingProgram.recrolls - nexusGarments_KnittingProgram.desrolls AS Expr2, nexusGarments_KnittingProgram.id AS Expr3, nexusGarments_Master_CompactingCompany.Company AS Expr4, nexusGarments_Master_CompactingMethod.CompMethod FROM        nexusGarments_DyingProgram  INNER JOIN    nexus_Master_DyingColor ON nexus_Master_DyingColor.id = nexusGarments_DyingProgram.dyingColor INNER JOIN nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN   nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id WHERE     (nexusGarments_KnittingProgram.isHeatSetting = 'False') AND (nexusGarments_KnittingProgram.recweight - nexusGarments_KnittingProgram.desweight > 0) AND  (nexusGarments_KnittingProgram.IsProcessClosed <> 'True')
            m_odbcCommand.CommandText = "SELECT     nexusGarments_PrintingProgram.id, nexusGarments_PrintingProgram.ksource as kid, nexusGarments_PrintingDesign.DesignName, nexusGarments_PrintingColor.ColorName,  nexusGarments_PrintingProgram.reqWeight, nexusGarments_PrintingProgram.delWeight, nexusGarments_PrintingProgram.delRolls,  nexusGarments_PrintingProgram.recWeight, nexusGarments_PrintingProgram.recRolls,  nexusGarments_processweight.recrolls - nexusGarments_processweight.desrolls AS availrolls,  nexusGarments_processweight.recweight - nexusGarments_processweight.desweight AS availweight, nexusGarments_Master_fabric.fabricName,  nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingCompany.Company, nexusGarments_printingprogram.dia,'1' as pro FROM nexusGarments_PrintingProgram INNER JOIN nexusGarments_processweight ON nexusGarments_PrintingProgram.ksource = nexusGarments_processweight.id  INNER JOIN  nexusGarments_Master_Style ON nexusGarments_printingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_printingprogram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN  nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN   nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN   nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id WHERE     (nexusGarments_processweight.IsKwashing = 'False') AND (nexusGarments_processweight.isHeatSetting = 'False') AND  (nexusGarments_processweight.isDying = 'False') AND (nexusGarments_processweight.isPrinting = 'True') and nexusGarments_processweight.recweight - nexusGarments_processweight.desweight > 0 union SELECT     nexusGarments_PrintingProgram.id, nexusGarments_PrintingProgram.ksource as kid, nexusGarments_PrintingDesign.DesignName, nexusGarments_PrintingColor.ColorName,  nexusGarments_PrintingProgram.reqWeight, nexusGarments_PrintingProgram.delWeight, nexusGarments_PrintingProgram.delRolls,  nexusGarments_PrintingProgram.recWeight, nexusGarments_PrintingProgram.recRolls,  nexusGarments_processweight.KWrecrolls - nexusGarments_processweight.KWDelrolls AS availrolls,  nexusGarments_processweight.KWrecweight - nexusGarments_processweight.KWDelweight AS availweight, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingCompany.Company, nexusGarments_printingprogram.dia,'2' as pro FROM  nexusGarments_PrintingProgram INNER JOIN nexusGarments_processweight ON nexusGarments_PrintingProgram.ksource = nexusGarments_processweight.id  INNER JOIN  nexusGarments_Master_Style ON nexusGarments_printingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_printingprogram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN  nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN   nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN   nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id  WHERE     (nexusGarments_processweight.IsKwashing = 'True') AND (nexusGarments_processweight.isHeatSetting = 'False') AND   (nexusGarments_processweight.isDying = 'False') AND (nexusGarments_processweight.isPrinting = 'True') and nexusGarments_processweight.KWrecweight - nexusGarments_processweight.KWDelweight > 0 union SELECT     nexusGarments_PrintingProgram.id, nexusGarments_PrintingProgram.ksource as kid, nexusGarments_PrintingDesign.DesignName, nexusGarments_PrintingColor.ColorName,   nexusGarments_PrintingProgram.reqWeight, nexusGarments_PrintingProgram.delWeight, nexusGarments_PrintingProgram.delRolls,   nexusGarments_PrintingProgram.recWeight, nexusGarments_PrintingProgram.recRolls,  nexusGarments_processweight.heatrecrolls - nexusGarments_processweight.heatDelrolls AS availrolls, nexusGarments_processweight.heatrecweight - nexusGarments_processweight.heatDelweight AS availweight, nexusGarments_Master_fabric.fabricName,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingCompany.Company, nexusGarments_printingprogram.dia,'3' as pro FROM nexusGarments_PrintingProgram INNER JOIN nexusGarments_processweight ON nexusGarments_PrintingProgram.ksource = nexusGarments_processweight.id  INNER JOIN  nexusGarments_Master_Style ON nexusGarments_printingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_printingprogram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN  nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN   nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN   nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id  WHERE     (nexusGarments_processweight.isHeatSetting = 'True') AND   (nexusGarments_processweight.isDying = 'False') AND (nexusGarments_processweight.isPrinting = 'True') and nexusGarments_processweight.heatrecweight - nexusGarments_processweight.heatDelweight > 0 union SELECT     nexusGarments_PrintingProgram.id, nexusGarments_PrintingProgram.ksource as kid, nexusGarments_PrintingDesign.DesignName, nexusGarments_PrintingColor.ColorName,   nexusGarments_PrintingProgram.reqWeight, nexusGarments_PrintingProgram.delWeight, nexusGarments_PrintingProgram.delRolls,   nexusGarments_PrintingProgram.recWeight, nexusGarments_PrintingProgram.recRolls,  nexusGarments_processweight.dyerecrolls - nexusGarments_processweight.dyeDelrolls AS availrolls,nexusGarments_processweight.dyerecweight - nexusGarments_processweight.dyeDelweight AS availweight, nexusGarments_Master_fabric.fabricName,   nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingCompany.Company, nexusGarments_printingprogram.dia,'4' as pro FROM         nexusGarments_PrintingProgram INNER JOIN nexusGarments_processweight ON nexusGarments_PrintingProgram.ksource = nexusGarments_processweight.id  INNER JOIN  nexusGarments_Master_Style ON nexusGarments_printingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_printingprogram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN  nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN   nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN   nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id   INNER JOIN  nexusGarments_DyingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_DyingProgram.Ksource AND  nexusGarments_Master_Style.id = nexusGarments_DyingProgram.styleid AND nexusGarments_Master_fabric.fabricId = nexusGarments_DyingProgram.fabricid AND   nexusGarments_PrintingColor.id = nexusGarments_DyingProgram.dyingColor WHERE (nexusGarments_processweight.isDying = 'True') AND (nexusGarments_processweight.isPrinting = 'True') and nexusGarments_processweight.dyerecweight - nexusGarments_processweight.dyeDelweight  >0 and nexusGarments_DyingProgram.recWeight > 0";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["designname"].ToString().Trim(), m_dataReader["ColorName"].ToString().Trim(), m_dataReader["availRolls"].ToString().Trim(), m_dataReader["availWeight"].ToString().Trim(), m_dataReader["delWeight"].ToString().Trim(), m_dataReader["delRolls"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim(), m_dataReader["recRolls"].ToString().Trim(), m_dataReader["reqWeight"].ToString().Trim(), m_dataReader["kid"].ToString().Trim(), m_dataReader["pro"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadDyingInwardProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("DC NO");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Del Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recrolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("recweight", System.Type.GetType("System.Double"));
            
            dt.Columns.Add("Dying Color");
            dt.Columns.Add("Dying Company");
dt.Columns.Add("mapid");
            dt.Columns.Add("Comp Company");
            dt.Columns.Add("Comp method");

            

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_DyingProcess.id, nexusGarments_DyingProcess.mapid, nexusGarments_DyingProcess.dcno, nexusGarments_DyingProcess.delrolls,  nexusGarments_DyingProcess.delWeight, nexusGarments_DyingProcess.recRolls, nexusGarments_DyingProcess.recWeight,   nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle, nexusGarments_PrintingColor.ColorName,  nexusGarments_Master_CompactingCompany.Company AS compcom, nexusGarments_Master_CompactingMethod.CompMethod,   nexusGarmrnts_Master_DyingCompany.Company, nexusGarments_DyingProgram.dia FROM nexusGarments_DyingProcess INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id INNER JOIN   nexusGarments_Master_Style ON nexusGarments_DyingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_DyingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN   nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id WHERE     (nexusGarments_DyingProcess.isclosed = 'false') and (nexusGarments_DyingProcess.isapproved = 'true')";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["ColorName"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), m_dataReader["compcom"].ToString().Trim(), m_dataReader["compmethod"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }      
        }

        internal void LoadPrintingInwardProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("DC NO");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Del Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recrolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("recweight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Design");
            dt.Columns.Add("Color");
            dt.Columns.Add("Company");
            dt.Columns.Add("mapid");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_PrintingProcess.id, nexusGarments_PrintingProcess.mapid, nexusGarments_PrintingProcess.dcno, nexusGarments_PrintingProcess.delrolls,  nexusGarments_PrintingProcess.delWeight, nexusGarments_PrintingProcess.recRolls, nexusGarments_PrintingProcess.recWeight, nexusGarments_Master_Style.ProcessStyle, nexusGarments_Master_fabric.fabricName, nexusGarments_PrintingDesign.DesignName,  nexusGarments_PrintingCompany.Company, nexusGarments_PrintingColor.ColorName, nexusGarments_PrintingProgram.dia FROM nexusGarments_PrintingProcess INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id INNER JOIN   nexusGarments_Master_Style ON nexusGarments_PrintingProgram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id WHERE     (nexusGarments_PrintingProcess.isclosed = 'False') and (nexusGarments_PrintingProcess.isapproved = 'True')";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["designname"].ToString().Trim(), m_dataReader["colorname"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal string genKnittingDC()
        {
           
            m_dataReader = null;
            m_odbcCommand.CommandText = "select [value] from nexusGarments_dcgen where processid='101';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dcno = m_dataReader["value"].ToString();
                    return dcno;
                }
                

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return dcno;
        }

        internal string genHeatSettingDC()
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = "select [value] from nexusGarments_dcgen where processid='102';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dcno = m_dataReader["value"].ToString();
                    return dcno;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return dcno;
        }

        internal string genYPDC()
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = "select [value] from nexusGarments_dcgen where processid='100';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dcno = m_dataReader["value"].ToString();
                    return dcno;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return dcno;
        }

        internal string gendyeSettingDC()
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = "select [value] from nexusGarments_dcgen where processid='103';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dcno = m_dataReader["value"].ToString();
                    return dcno;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return dcno;
        }



        


        internal string genPrintingDC()
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = "select [value] from nexusGarments_dcgen where processid='105';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dcno = m_dataReader["value"].ToString();
                    return dcno;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return dcno;
        }



        internal string genDyingEXDC()
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = "select [value] from nexusGarments_dcgen where processid='107';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dcno = m_dataReader["value"].ToString();
                    return dcno;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return dcno;
        }

        internal string genkwashingSettingDC()
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = "select [value] from nexusGarments_dcgen where processid='104';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dcno = m_dataReader["value"].ToString();
                    return dcno;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return dcno;
        }

        internal void LoadKnittingProgress(DataTable dt ,string poid)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Req / Rec");
            dt.Columns.Add("Completed");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_KnittingProgram.recweight, nexusGarments_KnittingProgram.reqweight,(nexusGarments_KnittingProgram.recweight*100)/nexusGarments_KnittingProgram.reqweight as prec  , nexusGarments_Master_Style.ProcessStyle,nexusGarments_Master_fabric.fabricName FROM nexusGarments_KnittingProgram INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId where nexusGarments_KnittingProgram.processid='"+poid+"'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                   
                    dt.Rows.Add(m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim()+ "/"+ m_dataReader["recweight"].ToString().Trim(), Convert.ToInt32( Convert.ToDouble(m_dataReader["prec"].ToString().Trim())));
                    
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadHeatSettingProgress(DataTable dt,string poid)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Req / Rec");
            dt.Columns.Add("Completed");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_Master_Style.ProcessStyle, nexusGarments_Master_fabric.fabricName, nexusGarments_HeatSettingProgram.ReqWeight,nexusGarments_HeatSettingProgram.recweight,(nexusGarments_HeatSettingProgram.recweight*100/nexusGarments_HeatSettingProgram.ReqWeight) as prec FROM nexusGarments_HeatSettingProgram INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_HeatSettingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id  where nexusGarments_KnittingProgram.processid='"+poid+"'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim() + "/" + m_dataReader["recweight"].ToString().Trim(),Convert.ToInt32(Convert.ToDouble( m_dataReader["prec"].ToString().Trim())));

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadDyingProgress(DataTable dt,string poid)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Req / Rec");
            dt.Columns.Add("Completed");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_Master_Style.ProcessStyle, nexusGarments_Master_fabric.fabricName, nexusGarments_DyingProgram.reqWeight, nexusGarments_DyingProgram.recWeight , (nexusGarments_DyingProgram.recWeight*100)/nexusGarments_DyingProgram.reqWeight as prec FROM  nexusGarments_DyingProgram INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN    nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN   nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN   nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId  where nexusGarments_KnittingProgram.processid='"+poid+"'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim() + "/" + m_dataReader["recweight"].ToString().Trim(),Convert.ToInt32( Convert.ToDouble(m_dataReader["prec"].ToString().Trim())));

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LaodKnittingRelation(DataSet ds, string  poid)
        {
            try
            {
                string KnittingMasterQuery = "SELECT nexusGarments_KnittingProgram.id,rtrim(nexusGarments_Master_Style.ProcessStyle) as Style,rtrim (nexusGarments_Master_fabric.fabricName) as Fabric, rtrim( nexusGarments_Master_KnittingCompany.Company) as [Knitting Company],  nexusGarments_KnittingProgram.dia as Dia, nexusGarments_KnittingProgram.gsm as GSM,nexusGarments_KnittingProgram.gg as GG, nexusGarments_KnittingProgram.ll as LL, nexusGarments_KnittingProgram.delweight as Deliver, nexusGarments_KnittingProgram.recweight as Resived,((nexusGarments_KnittingProgram.recweight*100) /nexusGarments_KnittingProgram.delweight ) as Precentage FROM nexusGarments_KnittingProgram INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id where nexusGarments_KnittingProgram.delweight<>0 and   nexusGarments_KnittingProgram.processid='"+poid+"'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT id,mapid,DCNo AS [DC Number], delBags AS [Del Bags], [delWeight] as [Del Weight], recWeight AS [Rec weight], recrolls AS [Rec Bags], deldate AS [Delivered On] FROM nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid="+poid+")", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_KnittingProgram");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_KnittingProcess");
                oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                ds.Relations.Add("DeliverInformation",
                    ds.Tables["nexusGarments_KnittingProgram"].Columns["id"],
                    ds.Tables["nexusGarments_KnittingProcess"].Columns["mapid"]);
              
                ds.Relations.Add("Inward Info",
                    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }
        

        }

        internal void LoadHeatSettingRelation(DataSet ds,string poid)
        {
            try
            {
                string KnittingMasterQuery = "SELECT     nexusGarments_HeatSettingProgram.id, RTRIM(nexusGarments_Master_fabric.fabricName) AS Fabric, nexusGarments_HeatSettingProgram.FGSM, RTRIM(nexusGarments_Master_Style.ProcessStyle) AS Style, RTRIM(nexusGarments_Master_HeatSettingCompany.Company) AS [HS Company],  nexusGarments_HeatSettingProgram.recweight, nexusGarments_HeatSettingProgram.ReqWeight, nexusGarments_HeatSettingProgram.recweight * 100 / nexusGarments_HeatSettingProgram.ReqWeight AS Completed FROM  nexusGarments_HeatSettingProgram INNER JOIN   nexusGarments_KnittingProgram ON nexusGarments_HeatSettingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN  nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_HeatSettingCompany ON nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id  where nexusGarments_KnittingProgram.processid='"+poid+"'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT id,mapid,DCNo AS [DC Number], delrolls AS [Del Bags], [delWeight] as [Del Weight], recWeight AS [Rec weight], recrolls AS [Rec Rolls], deldate AS [Delivered On] FROM nexusGarments_HeatSettingProcess where mapid in (select id from nexusGarments_heatsettingprogram where kid in(select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,invoiceno as [Invoice Number] ,recrolls as [Rec Rolls] , recweight as [Rec weight],recdate as [Rec Date] FROM  nexusGarments_HeatSettingLog where dcid in(select id from nexusGarments_heatsettingprocess where mapid in(select id from nexusGarments_heatsettingprogram where kid in(select id from nexusGarments_knittingProgram where processid=" + poid + ")))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_HeatSettingProgram");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_HeatSettingProcess");
                oleDbDataAdapter3.Fill(ds, "nexusGarments_HeatSettingLog");
                ds.Relations.Add("DeliverInformation",
                    ds.Tables["nexusGarments_HeatSettingProgram"].Columns["id"],
                    ds.Tables["nexusGarments_HeatSettingProcess"].Columns["mapid"]);
                //ds.Tables["nexusGarments_KnittingProcess"].Columns["mapid"].
                ds.Relations.Add("Inward Info",
                    ds.Tables["nexusGarments_HeatSettingProcess"].Columns["id"],
                    ds.Tables["nexusGarments_HeatSettingLog"].Columns["dcid"]);
            }
            catch { 
            }
        }

        internal void LoadDyingRelationRelation(DataSet ds, string poid)
        {
            try
            {
                string KnittingMasterQuery = "SELECT     nexusGarments_Master_Style.ProcessStyle as Style, nexusGarmrnts_Master_DyingCompany.Company,  nexusGarments_Dyebasecolor.dyebasecolor as [Dye Base Color], nexus_Master_DyingColor.DyingColor as [Color], nexusGarments_DyingProgram.RecWeight, nexusGarments_DyingProgram.ReqWeight,(nexusGarments_DyingProgram.recWeight*100/nexusGarments_DyingProgram.reqWeight) as Completed,nexusGarments_DyingProgram.id FROM nexusGarments_DyingProgram INNER JOIN nexus_Master_DyingColor ON nexusGarments_DyingProgram.dyingColor = nexus_Master_DyingColor.id INNER JOIN nexusGarments_Dyebasecolor ON nexusGarments_DyingProgram.dyingBaseColor = nexusGarments_Dyebasecolor.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_DyingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id  where nexusGarments_KnittingProgram.processid='"+poid+"'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT id,mapid,DCNo AS [DC Number], delrolls AS [Del Bags], [delWeight] as [Del Weight], recWeight AS [Rec weight], recrolls AS [Rec Rolls], deldate AS [Delivered On] FROM nexusGarments_DyingProcess where mapid in(select id from nexusGarments_dyingprogram where kid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,invoiceno as [Invoice Number] ,recrolls as [Rec Rolls] , recweight as [Rec weight],recdate as [Rec Date] FROM  nexusGarments_DyingLog where dcid in  (select id from nexusGarments_DyingProcess where mapid in(select id from nexusGarments_dyingprogram where kid in (select id from nexusGarments_knittingProgram where processid=" + poid + ")))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_DyingProgram");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_DyingProcess");
                oleDbDataAdapter3.Fill(ds, "nexusGarments_DyingLog");
                ds.Relations.Add("DeliverInformation",
                ds.Tables["nexusGarments_DyingProgram"].Columns["id"],
                ds.Tables["nexusGarments_DyingProcess"].Columns["mapid"]);
                //ds.Tables["nexusGarments_KnittingProcess"].Columns.
                ds.Relations.Add("Inward Info",
                ds.Tables["nexusGarments_DyingProcess"].Columns["id"],
                ds.Tables["nexusGarments_DyingLog"].Columns["dcid"]);
            }
            catch { }
        }

        internal void LaodKnittingProcessTracking(DataSet ds,string dcno)
        {
            try
            {
                string KnittingMasterQuery = "SELECT  rtrim(nexusGarments_Master_fabric.fabricName) as Fabric, rtrim(nexusGarments_Master_KnittingCompany.Company) as  Company, rtrim(nexusGarments_Master_Style.ProcessStyle) as Style,nexusGarments_KnittingProcess.delWeight as  DelWeight, rtrim(nexusGarments_KnittingProcess.delBags)as  DelBags, rtrim( nexusGarments_KnittingProcess.recWeight)as RecWeight, rtrim(nexusGarments_KnittingProcess.recrolls) as RecRolls, nexusGarments_KnittingProcess.deldate as DeliveryDate, nexusGarments_KnittingProcess.id FROM  nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id where nexusGarments_KnittingProcess.dcno='" + dcno + "'";

                //, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll,
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where dcno='"+dcno+"')", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_KnittingProcess");
                
                oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                //ds.Tables["nexusGarments_KnittingProcess"].Columns.
                ds.Relations.Add("Inward Info",
                    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }


        }

        internal void LoadKnittingdcPrintinfo(DataTable dt, string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("dcno");
           
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT  nexusGarments_Master_fabric.fabricName, nexusGarments_Master_KnittingCompany.Company, nexusGarments_Master_Style.ProcessStyle,nexusGarments_KnittingProcess.delWeight, nexusGarments_KnittingProcess.DCNo, nexusGarments_KnittingProcess.delBags,  nexusGarments_KnittingProcess.recWeight, nexusGarments_KnittingProcess.recrolls, nexusGarments_KnittingProcess.deldate, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll, nexusGarments_KnittingProcess.id FROM  nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id where nexusGarments_KnittingProcess.dcno='"+dcno+"'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim()  );

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LaodHeatSettingProcessTracking(DataSet ds, string dcno)
        {
            try
            {
                string KnittingMasterQuery = "SELECT  rtrim(nexusGarments_Master_fabric.fabricName) as Fabric, rtrim(nexusGarments_Master_KnittingCompany.Company) as  Company, rtrim(nexusGarments_Master_Style.ProcessStyle) as Style,nexusGarments_HeatSettingProcess.delWeight as  DelWeight, rtrim(nexusGarments_HeatSettingProcess.delrolls)as  DelRolls, rtrim( nexusGarments_HeatSettingProcess.recWeight)as RecWeight, rtrim(nexusGarments_HeatSettingProcess.recrolls) as RecRolls, nexusGarments_HeatSettingProcess.deldate as DeliveryDate, nexusGarments_HeatSettingProcess.id FROM  nexusGarments_HeatSettingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id where nexusGarments_HeatSettingProcess.dcno='" + dcno + "'";

                //, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll,
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight],recdate as [Rec Date] FROM  nexusGarments_HeatSettingLog where dcid in( select id from nexusGarments_HeatSettingProcess where dcno='" + dcno + "')", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_HeatSettingProcess");

                oleDbDataAdapter3.Fill(ds, "nexusGarments_HeatSettingLog");
                //ds.Tables["nexusGarments_KnittingProcess"].Columns.
                ds.Relations.Add("Inward Info",
                    ds.Tables["nexusGarments_HeatSettingProcess"].Columns["id"],
                    ds.Tables["nexusGarments_HeatSettingLog"].Columns["dcid"]);
            }
            catch
            {
            }
        }

        internal void LaodDyingProcessTracking(DataSet ds, string dcno)
        {
            try
            {
                string KnittingMasterQuery = "SELECT  rtrim(nexusGarments_Master_fabric.fabricName) as Fabric, rtrim(nexusGarments_Master_KnittingCompany.Company) as  Company, rtrim(nexusGarments_Master_Style.ProcessStyle) as Style,nexusGarments_DyingProcess.delWeight as  DelWeight, rtrim(nexusGarments_DyingProcess.delrolls)as  DelRolls, rtrim( nexusGarments_DyingProcess.recWeight)as RecWeight, rtrim(nexusGarments_DyingProcess.recrolls) as RecRolls, nexusGarments_DyingProcess.deldate as DeliveryDate, nexusGarments_DyingProcess.id FROM  nexusGarments_DyingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id where nexusGarments_DyingProcess.dcno='" + dcno + "'";

                //, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll,
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight],recdate as [Rec Date] FROM  nexusGarments_DyingLog where dcid in( select id from nexusGarments_DyingProcess where dcno='" + dcno + "')", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_DyingProcess");

                oleDbDataAdapter3.Fill(ds, "nexusGarments_DyingLog");
                //ds.Tables["nexusGarments_KnittingProcess"].Columns.
                ds.Relations.Add("Inward Info",
                    ds.Tables["nexusGarments_DyingProcess"].Columns["id"],
                    ds.Tables["nexusGarments_DyingLog"].Columns["dcid"]);
            }
            catch
            {
            }
        }

        internal void LoadKnittingComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,Company from nexusGarments_Master_KnittingCompany";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadWashingComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,Company from nexusGarments_KWashingCompany";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadStoreComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,supplier as Company from nexusGarments_Master_StoreSupplier";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        //internal void LoadDyingComapany(DataTable dt)
        //{
        //    dt.Columns.Clear();
        //    dt.Rows.Clear();
        //    dt.Columns.Add("id");
        //    dt.Columns.Add("Company");

        //    m_dataReader = null;
        //    m_odbcCommand.CommandText = "Select id,Company from nexusGarments_HeatSettingProgram";
        //    try
        //    {
        //        OpenDB();
        //        m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (m_dataReader.Read())
        //        {
        //            // lblMake_new.Text = m_dataReader["make"].ToString();
        //            //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
        //            // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

        //            //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

        //            dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

        //        }

        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    finally
        //    {
        //        CloseDB(1);
        //    }
        //}


        internal void LoadHeatSettingComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,Company from nexusGarments_Master_HeatSettingCompany";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void KnittingReport(DataTable dt)
        {
            bool isset=false;
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Date", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Style");
            dt.Columns.Add("Counts");
            dt.Columns.Add("Yarn Supplier");
            dt.Columns.Add("Knitting Company");
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Balance", System.Type.GetType("System.Double"));
            dt.Columns.Add("Prec");
            dt.Columns.Add("Closed", System.Type.GetType("System.Boolean"));
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_KnittingProcess.recweight,nexusGarments_KnittingProcess.isclosed, nexusGarments_KnittingProcess.DCNo, nexusGarments_KnittingProcess.deldate,  nexusGarments_Master_Style.ProcessStyle, nexusGarmrnts_Master_Counts.Counts, nexusGarments_Master_YarnSupplier.Supplier, nexusGarments_Master_KnittingCompany.Company, nexusGarments_KnittingProcess.delWeight, nexusGarments_KnittingProcess.delBags,  nexusGarments_Master_KnittingCompany.id FROM         nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_KnittingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id ";
            
            
 //           if (Company != "")
 //           {
 //               isset = true;
 //m_odbcCommand.CommandText = m_odbcCommand.CommandText + "  nexusGarments_KnittingProgram.companyid =" +Company;
 //           }


 //           if (fromdate != "")
 //           {
 //               if (isset)
 //               {
 //                   m_odbcCommand.CommandText = m_odbcCommand.CommandText + " and ";
 //               }
 //               m_odbcCommand.CommandText = m_odbcCommand.CommandText + "  nexusGarments_KnittingProcess.deldate >=" + fromdate + "'";
 //               isset = true;
 //           }


 //           if (todate != "")
 //           {
 //               if (isset)
 //               {
 //                   m_odbcCommand.CommandText = m_odbcCommand.CommandText + " and ";
 //               }
 //               m_odbcCommand.CommandText = m_odbcCommand.CommandText + "  nexusGarments_KnittingProcess.deldate <=" + todate + "'";
 //               isset = true;
 //           }
            string id = "";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    if (id != m_dataReader["dcno"].ToString().Trim())
                    {
                        id = m_dataReader["dcno"].ToString().Trim();
                        dt.Rows.Add(m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim().Substring(0, 10), m_dataReader["processstyle"].ToString().Trim(), m_dataReader["counts"].ToString().Trim(), m_dataReader["supplier"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) - Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()), Convert.ToInt32(Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()) * 100 / Convert.ToDouble(m_dataReader["delweight"].ToString().Trim())), m_dataReader["isClosed"].ToString().Trim());
                    }

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }

        internal void LoadknittingReadytoClose(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Company");

            dt.Columns.Add("Deliverd", System.Type.GetType("System.Double"));
            dt.Columns.Add("Recieved", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Yarn", System.Type.GetType("System.Double"));
            dt.Columns.Add("Status");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_KnittingProcess.id,nexusGarments_KnittingProcess.isclosed,nexusGarments_KnittingProcess.delWeight,nexusGarments_KnittingProcess.recyarn, nexusGarments_KnittingProcess.recWeight, nexusGarments_KnittingProcess.DCNo,nexusGarments_Master_fabric.fabricName, nexusGarments_Master_KnittingCompany.Company, nexusGarments_KnittingProgram.dia FROM nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id where nexusGarments_KnittingProcess.isclosed = 'False'";

           
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    string status="Open";
                    if (m_dataReader["isclosed"].ToString().Trim() == "True")
                    {
                        status = "Close";
                    }
                    else {
                        status = "Open";
                    }
                    if((Convert.ToDouble(m_dataReader["recweight"].ToString().Trim())) != 0  && status !="Close" )
                    {
                        double se = ((Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) * 100) / (Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()))) - 100;
                        if (se < 2 )

                        {
                            status = "Ready to Close";
                        }
                    }
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["isclosed"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["recyarn"].ToString().Trim(), status);

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }

        internal void LoadHeatSettingReadytoClose(DataTable dt)
        {

            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Company");

            dt.Columns.Add("Deliverd", System.Type.GetType("System.Double"));
            dt.Columns.Add("Recieved", System.Type.GetType("System.Double"));
            
            dt.Columns.Add("Status");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_HeatSettingProcess.isClosed, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle,  nexusGarments_HeatSettingProcess.mapid, nexusGarments_HeatSettingProcess.dcno, nexusGarments_HeatSettingProcess.delWeight,  nexusGarments_HeatSettingProcess.recWeight, nexusGarments_Master_HeatSettingCompany.Company, nexusGarments_HeatSettingProgram.dia,  nexusGarments_HeatSettingProcess.id FROM  nexusGarments_HeatSettingProcess INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id INNER JOIN nexusGarments_Master_HeatSettingCompany ON  nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id  INNER JOIN nexusGarments_Master_fabric ON nexusGarments_HeatSettingProgram.Fabricid = nexusGarments_Master_fabric.fabricid inner join nexusGarments_Master_Style ON nexusGarments_HeatSettingProgram.StyleId = nexusGarments_Master_Style.id WHERE     (nexusGarments_HeatSettingProcess.isClosed = 'False') ";


            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    string status = "Open";
                    if (m_dataReader["isclosed"].ToString().Trim() == "True")
                    {
                        status = "Close";
                    }
                    else
                    {
                        status = "Open";
                    }
                    if ((Convert.ToDouble(m_dataReader["recweight"].ToString().Trim())) != 0 && status != "Close")
                    {
                        double se = ((Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) * 100) / (Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()))) - 100;
                        if (se < 11)
                        {
                            status = "Ready to Close";
                        }
                    }
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["isclosed"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), status);

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadDyingReadytoClose(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Company");

            dt.Columns.Add("Deliverd", System.Type.GetType("System.Double"));
            dt.Columns.Add("Recieved", System.Type.GetType("System.Double"));

            dt.Columns.Add("Status");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_DyingProcess.mapid, nexusGarments_PrintingColor.ColorName AS DyingColor, nexusGarments_Master_Style.ProcessStyle,  nexusGarments_Master_fabric.fabricName, nexusGarments_DyingProcess.dcno, nexusGarments_DyingProcess.delWeight,  nexusGarments_DyingProcess.recWeight, nexusGarmrnts_Master_DyingCompany.Company, nexusGarments_DyingProgram.dia,  nexusGarments_DyingProcess.id, nexusGarments_DyingProcess.isclosed FROM        nexusGarments_DyingProgram  INNER JOIN  nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id  INNER JOIN nexusGarments_Master_Style ON nexusGarments_DyingProgram.StyleId = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_DyingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_DyingProcess ON nexusGarments_DyingProgram.id = nexusGarments_DyingProcess.mapid WHERE     (nexusGarments_DyingProcess.isclosed = 'False')";


            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    string status = "Open";
                    if (m_dataReader["isclosed"].ToString().Trim() == "True")
                    {
                        status = "Close";
                    }
                    else
                    {
                        status = "Open";
                    }
                    if ((Convert.ToDouble(m_dataReader["recweight"].ToString().Trim())) != 0 && status != "Close")
                    {
                        double se = ((Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) * 100) / (Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()))) - 100;
                        if (se < 3)
                        {
                            status = "Ready to Close";
                        }
                    }
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["isclosed"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), status);

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }

        internal void LoadPrintingReadytoClose(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Company");

            dt.Columns.Add("Deliverd", System.Type.GetType("System.Double"));
            dt.Columns.Add("Recieved", System.Type.GetType("System.Double"));

            dt.Columns.Add("Status");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_PrintingProcess.isclosed,nexusGarments_PrintingProcess.id, nexusGarments_PrintingProcess.dcno, nexusGarments_Master_fabric.fabricName, nexusGarments_PrintingCompany.Company,   nexusGarments_PrintingProcess.delWeight, nexusGarments_PrintingProcess.recWeight FROM         nexusGarments_PrintingProcess INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id INNER JOIN   nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id  INNER JOIN nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.Fabricid = nexusGarments_Master_fabric.fabricId where nexusGarments_PrintingProcess.isclosed='False'";


            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    string status = "Open";
                    if (m_dataReader["isclosed"].ToString().Trim() == "True")
                    {
                        status = "Close";
                    }
                    else
                    {
                        status = "Open";
                    }
                    if ((Convert.ToDouble(m_dataReader["recweight"].ToString().Trim())) != 0 && status != "Close")
                    {
                        double se = ((Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) * 100) / (Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()))) - 100;
                        if (se < 1)
                        {
                            status = "Ready to Close";
                        }
                    }
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["isclosed"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), status);

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadHeatSettiingReport(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Date", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Company");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("F GSM");
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Balance");
            dt.Columns.Add("Prec %");
            dt.Columns.Add("Closed", System.Type.GetType("System.Boolean"));
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_HeatSettingProcess.isclosed,nexusGarments_HeatSettingProgram.fgsm,nexusGarments_HeatSettingProcess.dcno, nexusGarments_HeatSettingProcess.deldate, nexusGarments_Master_HeatSettingCompany.Company,nexusGarments_Master_fabric.fabricName, nexusGarments_HeatSettingProcess.delWeight, nexusGarments_HeatSettingProcess.recWeight FROM  nexusGarments_HeatSettingProcess INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_HeatSettingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_HeatSettingCompany ON nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_HeatSettingProgram.StyleId = nexusGarments_Master_Style.id";

           

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim().Substring(0, 10), m_dataReader["company"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["fgsm"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) - Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()), Convert.ToInt32(Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()) * 100 / Convert.ToDouble(m_dataReader["delweight"].ToString().Trim())), m_dataReader["isclosed"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }

        internal void LoadDyingReport(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Date", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Company");
            dt.Columns.Add("Color");

            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Balance");
            dt.Columns.Add("Prec %");
            dt.Columns.Add("Closed", System.Type.GetType("System.Boolean"));
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_DyingProcess.isclosed,nexusGarments_DyingProcess.dcno, nexusGarments_DyingProcess.deldate, nexusGarmrnts_Master_DyingCompany.Company,nexusGarments_PrintingColor.ColorName, nexusGarments_DyingProcess.delWeight, nexusGarments_DyingProcess.recweight FROM nexusGarments_DyingProcess INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id INNER JOIN   nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id";



            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim().Substring(0, 10), m_dataReader["company"].ToString().Trim(), m_dataReader["ColorName"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) - Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()), Convert.ToInt32(Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()) * 100 / Convert.ToDouble(m_dataReader["delweight"].ToString().Trim())), m_dataReader["isclosed"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }


        internal void LoadPrintingReport(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Date", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Company");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Design");
            dt.Columns.Add("Color");

            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Balance", System.Type.GetType("System.Double"));
            dt.Columns.Add("Prec %");
            dt.Columns.Add("Closed", System.Type.GetType("System.Boolean"));
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_PrintingProcess.isclosed,nexusGarments_PrintingProcess.dcno, nexusGarments_PrintingProcess.deldate, nexusGarments_PrintingDesign.DesignName,  nexusGarments_PrintingColor.ColorName, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle,  nexusGarments_PrintingProcess.delWeight, nexusGarments_PrintingProcess.recweight, nexusGarments_PrintingCompany.Company  FROM         nexusGarments_PrintingProcess INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id   INNER JOIN nexusGarments_Master_Style ON nexusGarments_PrintingProgram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id";



            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim().Substring(0, 10), m_dataReader["company"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["designname"].ToString().Trim(), m_dataReader["colorname"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) - Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()), Convert.ToInt32(Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()) * 100 / Convert.ToDouble(m_dataReader["delweight"].ToString().Trim())), m_dataReader["isclosed"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadKWashingReport(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Date", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Style");
            dt.Columns.Add("Company");
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Balance", System.Type.GetType("System.Double"));
            dt.Columns.Add("Prec %");
            dt.Columns.Add("Closed", System.Type.GetType("System.Boolean"));
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_KWashingProcess.isclosed,nexusGarments_KWashingProcess.dcno, nexusGarments_KWashingProcess.deldate, nexusGarments_Master_fabric.fabricName,  nexusGarments_Master_Style.ProcessStyle, nexusGarments_KWashingCompany.Company, nexusGarments_KWashingProcess.delWeight, nexusGarments_KWashingProcess.recWeight   FROM nexusGarments_KWashingProcess INNER JOIN nexusGarments_KWashingProgram ON nexusGarments_KWashingProcess.mapid = nexusGarments_KWashingProgram.id INNER JOIN  nexusGarments_KWashingCompany ON nexusGarments_KWashingProgram.dyingComp = nexusGarments_KWashingCompany.id INNER JOIN   nexusGarments_Master_Style ON nexusGarments_KWashingProgram.StyleId = nexusGarments_Master_Style.id INNER JOIN   nexusGarments_Master_fabric ON nexusGarments_KWashingProgram.Fabricid = nexusGarments_Master_fabric.fabricId";



            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim().Substring(0, 10), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["processStyle"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) - Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()), Convert.ToInt32(Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()) * 100 / Convert.ToDouble(m_dataReader["delweight"].ToString().Trim())), m_dataReader["isclosed"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadWashingReport(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Date", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Style");
            dt.Columns.Add("Company");
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Balance", System.Type.GetType("System.Double"));
            dt.Columns.Add("Prec %");
            dt.Columns.Add("Closed", System.Type.GetType("System.Boolean"));
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_WashingProcess.isclosed,nexusGarments_WashingProcess.deldate,nexusGarments_WashingProcess.dcno, nexusGarments_KWashingCompany.Company, nexusGarments_Master_Style.ProcessStyle,nexusGarments_Master_fabric.fabricName, nexusGarments_WashingProcess.delWeight, nexusGarments_WashingProcess.recWeight FROM         nexusGarments_WashingProcess INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN   nexusGarments_KWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_WashingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN   nexusGarments_Master_Style ON nexusGarments_WashingProgram.styleid = nexusGarments_Master_Style.id";



            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim().Substring(0, 10), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["processStyle"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) - Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()), Convert.ToInt32(Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()) * 100 / Convert.ToDouble(m_dataReader["delweight"].ToString().Trim())), m_dataReader["isclosed"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadYarnApproval(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("dcid");
            dt.Columns.Add("id");
           
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Process Style");
            dt.Columns.Add("Invoice No");
            dt.Columns.Add("Order Quantity", System.Type.GetType("System.Double"));
            dt.Columns.Add("Gross Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Net Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Bags");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_YarnInward.invoiceno,nexusGarments_YarnInward.DcId, nexusGarments_YarnInward.id, nexusGarments_YarnPurchase.dcno,nexusGarments_Master_YarnSupplier.Supplier, nexusGarments_Master_Style.ProcessStyle, nexusGarments_YarnPurchase.OrderQuantity, nexusGarments_YarnInward.GrossWeight, nexusGarments_YarnInward.NetWeight, nexusGarments_YarnInward.bags FROM nexusGarments_YarnInward INNER JOIN  nexusGarments_YarnPurchase ON nexusGarments_YarnInward.DcId = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id where nexusGarments_YarnInward.isapproved='false'";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["dcid"].ToString().Trim(), m_dataReader["id"].ToString().Trim(), "False", m_dataReader["Supplier"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["OrderQuantity"].ToString().Trim(), m_dataReader["GrossWeight"].ToString().Trim(), m_dataReader["NetWeight"].ToString().Trim(), m_dataReader["bags"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



    
        internal void LoadHeatApproval(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("dcid");
            dt.Columns.Add("mapid");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Invoice");
            dt.Columns.Add("DC Number");

            dt.Columns.Add("Company");
            dt.Columns.Add("FGSM");
            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
           
            

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_HeatSettingLog.id, nexusGarments_HeatSettingLog.dcid, nexusGarments_HeatSettingProcess.mapid, nexusGarments_HeatSettingLog.invoiceno,  nexusGarments_HeatSettingLog.recrolls, nexusGarments_HeatSettingLog.recweight, nexusGarments_HeatSettingProcess.dcno,  nexusGarments_Master_HeatSettingCompany.Company, nexusGarments_HeatSettingProgram.FGSM FROM nexusGarments_HeatSettingProgram INNER JOIN nexusGarments_HeatSettingProcess ON nexusGarments_HeatSettingProgram.id = nexusGarments_HeatSettingProcess.mapid INNER JOIN nexusGarments_Master_HeatSettingCompany ON  nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id INNER JOIN nexusGarments_HeatSettingLog ON nexusGarments_HeatSettingProcess.id = nexusGarments_HeatSettingLog.dcid where nexusGarments_HeatSettingLog.isapproved='False' ";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcid"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), "False", m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["FGSM"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadDyingApproval(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("dcid");
            dt.Columns.Add("mapid");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Invoice");
            dt.Columns.Add("DC Number");

            dt.Columns.Add("Company");
            dt.Columns.Add("Color");
            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_DyingProcess.dcno,nexusGarments_DyingLog.id, nexusGarments_DyingProcess.mapid, nexusGarments_DyingLog.invoiceno, nexusGarments_PrintingColor.colorname as DyingColor, nexusGarmrnts_Master_DyingCompany.Company, nexusGarments_DyingLog.recrolls, nexusGarments_DyingLog.recweight, nexusGarments_DyingLog.dcid FROM  nexusGarments_DyingProgram INNER JOIN  nexusGarments_DyingProcess ON nexusGarments_DyingProgram.id = nexusGarments_DyingProcess.mapid INNER JOIN nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id  INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN   nexusGarments_DyingLog ON nexusGarments_DyingProcess.id = nexusGarments_DyingLog.dcid  where nexusGarments_DyingLog.isapproved='False'";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcid"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), "False", m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["DyingColor"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadEXProcessApproval(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("dcid");
            dt.Columns.Add("mapid");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Invoice");
            dt.Columns.Add("DC Number");

            dt.Columns.Add("Company");
            dt.Columns.Add("Process");
            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_DyeEXInwardLog.id, nexusGarments_DyeEXInwardLog.dcid, nexusGarments_DyingExDel.mapid, nexusGarments_DyingExDel.dcno, nexusGarments_DyeEXInwardLog.invoiceno, nexusGarments_LotExCompany.Company, nexusGarments_LotEXProcess.Process,  nexusGarments_DyeEXInwardLog.recrolls, nexusGarments_DyeEXInwardLog.recweight FROM         nexusGarments_DyeEXInwardLog INNER JOIN nexusGarments_DyingExDel ON nexusGarments_DyeEXInwardLog.dcid = nexusGarments_DyingExDel.id INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingExDel.mapid = nexusGarments_DyingProgram.id INNER JOIN nexusGarments_LotExCompany ON nexusGarments_DyingExDel.Companyid = nexusGarments_LotExCompany.id INNER JOIN nexusGarments_LotEXProcess ON nexusGarments_DyingExDel.ProcessID = nexusGarments_LotEXProcess.id where nexusGarments_DyeEXInwardLog.isapproved='False'";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcid"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), "False", m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["Process"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadYarnDyingApproval(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("dcid");
            dt.Columns.Add("mapid");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Invoice");
            dt.Columns.Add("DC Number");

            dt.Columns.Add("Company");
            dt.Columns.Add("Color");
            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_YarnDyingProcess.dcno, nexusGarments_YarnDyingLog.invoiceno, nexusGarments_YarnDyingProcess.id,  nexusGarments_YarnDyingProcess.mapid, nexusGarments_YarnDyingCompany.Company, nexusGarments_PrintingColor.ColorName,  nexusGarments_YarnDyingLog.recrolls, nexusGarments_YarnDyingLog.recweight, nexusGarments_YarnDyingLog.recdate,  nexusGarments_YarnDyingLog.id AS Expr1 FROM         nexusGarments_YarnDyingLog INNER JOIN nexusGarments_YarnDyingProcess ON nexusGarments_YarnDyingLog.dcid = nexusGarments_YarnDyingProcess.id INNER JOIN nexusGarments_YarnDyingProgram ON nexusGarments_YarnDyingProcess.mapid = nexusGarments_YarnDyingProgram.id INNER JOIN nexusGarments_YarnDyingCompany ON nexusGarments_YarnDyingProgram.companyid = nexusGarments_YarnDyingCompany.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_YarnDyingProgram.colorid = nexusGarments_PrintingColor.id where  nexusGarments_YarnDyingLog.isapproved='False'";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["Expr1"].ToString().Trim(), m_dataReader["id"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), "False", m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["ColorName"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadPrintApproval(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("dcid");
            dt.Columns.Add("mapid");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Invoice");
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Company");
            dt.Columns.Add("Color");
            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_PrintingLog.id, nexusGarments_PrintingLog.dcid, nexusGarments_PrintingProcess.mapid, nexusGarments_PrintingLog.invoiceno,nexusGarments_PrintingProcess.dcno, nexusGarments_PrintingCompany.Company, nexusGarments_PrintingColor.ColorName, nexusGarments_PrintingLog.recrolls, nexusGarments_PrintingLog.recweight FROM  nexusGarments_PrintingLog INNER JOIN  nexusGarments_PrintingProcess ON nexusGarments_PrintingLog.dcid = nexusGarments_PrintingProcess.id INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id INNER JOIN nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id INNER JOIN  nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id where nexusGarments_PrintingLog.isapproved='False'";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["Expr1"].ToString().Trim(), m_dataReader["id"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), "False", m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["Colorname"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadKwashingApproval(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("dcid");
            dt.Columns.Add("mapid");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Invoice");
            dt.Columns.Add("DC Number");

            dt.Columns.Add("Company");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_KWashingLog.id, nexusGarments_KWashingLog.dcid, nexusGarments_KWashingProcess.mapid, nexusGarments_KWashingLog.invoiceno,    nexusGarments_KWashingProcess.dcno, nexusGarments_KWashingCompany.Company, nexusGarments_KWashingLog.recrolls,   nexusGarments_KWashingLog.recweight FROM         nexusGarments_KWashingLog INNER JOIN nexusGarments_KWashingProcess ON nexusGarments_KWashingLog.dcid = nexusGarments_KWashingProcess.id INNER JOIN  nexusGarments_KWashingProgram ON nexusGarments_KWashingProcess.mapid = nexusGarments_KWashingProgram.id INNER JOIN nexusGarments_KWashingCompany ON nexusGarments_KWashingProgram.dyingComp = nexusGarments_KWashingCompany.id WHERE     (nexusGarments_KWashingLog.isApproved = 'False')";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcid"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), "False", m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(),  m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadwashingApproval(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("dcid");
            dt.Columns.Add("mapid");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Invoice");
            dt.Columns.Add("DC Number");

            dt.Columns.Add("Company");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_WashingLog.id, nexusGarments_WashingLog.dcid, nexusGarments_WashingProcess.mapid, nexusGarments_WashingLog.invoiceno,    nexusGarments_WashingProcess.dcno, nexusGarments_kWashingCompany.Company, nexusGarments_WashingLog.recrolls,   nexusGarments_WashingLog.recweight FROM         nexusGarments_WashingLog INNER JOIN nexusGarments_WashingProcess ON nexusGarments_WashingLog.dcid = nexusGarments_WashingProcess.id INNER JOIN  nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_kWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id WHERE     (nexusGarments_WashingLog.isApproved = 'False')";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcid"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim(), "False", m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadApprovalInward(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("kpsid");
            dt.Columns.Add("kpid");
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Invoice Number");
            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Yarn", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Date");
            dt.Columns.Add("Fabric");

         // dt.Columns.Add("Invoice");
            
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_YarnInwardLog.img as imag,nexusGarments_KnittingProcess.id as kpsid,nexusGarments_KnittingProgram.id as kpid ,nexusGarments_YarnInwardLog.recyarn,nexusGarments_YarnInwardLog.id,nexusGarments_YarnInwardLog.recdate, nexusGarments_YarnInwardLog.recrolls, nexusGarments_YarnInwardLog.recweight, nexusGarments_KnittingProcess.DCNo,nexusGarments_YarnInwardLog.invoiceno, nexusGarments_Master_fabric.fabricName FROM nexusGarments_YarnInwardLog INNER JOIN  nexusGarments_KnittingProcess ON nexusGarments_YarnInwardLog.dcid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId WHERE  (nexusGarments_YarnInwardLog.isApproved = 'False') ";

            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    //byte[] imageData = (byte[])m_dataReader["imag"];

                    ////Initialize image variable
                    //Image newImage;
                    ////Read image data into a memory stream
                    //using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    //{
                    //    ms.Write(imageData, 0, imageData.Length);

                    //    //Set image variable value using memory stream.
                    //    newImage = Image.FromStream(ms, true);
                    //}


                    dt.Rows.Add(m_dataReader["kpsid"].ToString().Trim(), m_dataReader["kpid"].ToString().Trim(), m_dataReader["id"].ToString().Trim(), "False", m_dataReader["dcno"].ToString().Trim(), m_dataReader["invoiceno"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["recyarn"].ToString().Trim(), m_dataReader["recdate"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim());

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }

        internal void LoadYarnSupplier(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Supplier");
          
            m_dataReader = null;
            m_odbcCommand.CommandText = "select * from nexusGarments_Master_YarnSupplier";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["supplier"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadYarntype(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Type");

            m_dataReader = null;
            m_odbcCommand.CommandText = "select * from nexusGarments_Master_YarnType";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["yarnType"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadYarnCounts(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Counts");

            m_dataReader = null;
            m_odbcCommand.CommandText = "select * from nexusGarmrnts_Master_Counts";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["Counts"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadStyleDT(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");

            m_dataReader = null;
            m_odbcCommand.CommandText = "select * from nexusGarments_Master_Style";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadYarnColor(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Color");

            m_dataReader = null;
            m_odbcCommand.CommandText = "select * from nexusMaster_Master_YarnColor";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["color"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadStyle(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");

            m_dataReader = null;
            m_odbcCommand.CommandText = "select * from nexusGarments_Master_Style";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal bool LoadComapnyInfo(string sQuery, DevExpress.XtraEditors.TextEdit txtAddress1, DevExpress.XtraEditors.TextEdit txtAddress2, DevExpress.XtraEditors.TextEdit txtArea, DevExpress.XtraEditors.TextEdit txtCity, DevExpress.XtraEditors.TextEdit txtState, DevExpress.XtraEditors.TextEdit txtPhone, DevExpress.XtraEditors.TextEdit txtFax, DevExpress.XtraEditors.TextEdit txtMail)
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = sQuery;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    txtAddress1.Text = m_dataReader["Address1"].ToString().Trim();
                    txtAddress2.Text = m_dataReader["address2"].ToString().Trim();
                    txtArea.Text = m_dataReader["area"].ToString().Trim();
                    txtCity.Text = m_dataReader["City"].ToString().Trim();
                    txtFax.Text = m_dataReader["fax"].ToString().Trim();
                    txtMail.Text = m_dataReader["mail"].ToString().Trim();
                    txtPhone.Text = m_dataReader["Phone"].ToString().Trim();
                    txtState.Text = m_dataReader["State"].ToString().Trim();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return false;
        }

        internal void LoadHeatComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,company from nexusGarments_Master_HeatSettingCompany";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadPrintingComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Company");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,company from nexusGarments_PrintingCompany";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadYarntComapany(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Supplier");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,Supplier from nexusGarments_Master_YarnSupplier";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["supplier"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        //internal void LoadKnittingComapany(DataTable dt)
        //{
        //    dt.Columns.Clear();
        //    dt.Rows.Clear();
        //    dt.Columns.Add("id");
        //    dt.Columns.Add("Company");

        //    m_dataReader = null;
        //    m_odbcCommand.CommandText = "Select id,company from nexusGarments_Master_KnittingCompany";
        //    try
        //    {
        //        OpenDB();
        //        m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (m_dataReader.Read())
        //        {
        //            // lblMake_new.Text = m_dataReader["make"].ToString();
        //            //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
        //            // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

        //            //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

        //            dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["company"].ToString().Trim());

        //        }

        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    finally
        //    {
        //        CloseDB(1);
        //    }
        //}

        internal void LoadKWashingProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Washing Company");
            dt.Columns.Add("inlotweight", System.Type.GetType("System.Double"));
            dt.Columns.Add("inlotrolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("delWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("delRolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("recWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recRolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("reqWeight", System.Type.GetType("System.Double"));

            dt.Columns.Add("mapid");
            


            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT    nexusGarments_KWashingProgram.id,nexusGarments_KWashingProgram.ksource, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle, nexusGarments_KWashingCompany.Company,nexusGarments_KWashingProgram.dia, nexusGarments_KWashingProgram.reqWeight,nexusGarments_ProcessWeight.recweight - nexusGarments_ProcessWeight.desWeight as inlotweight, nexusGarments_ProcessWeight.recrolls - nexusGarments_ProcessWeight.desrolls as inlotrolls, nexusGarments_KWashingProgram.delWeight, nexusGarments_KWashingProgram.delRolls, nexusGarments_KWashingProgram.recWeight , nexusGarments_KWashingProgram.recRolls FROM   nexusGarments_KWashingProgram INNER JOIN  nexusGarments_ProcessWeight ON nexusGarments_KWashingProgram.ksource = nexusGarments_ProcessWeight.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_KWashingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN  nexusGarments_Master_Style ON nexusGarments_KWashingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_KWashingCompany ON nexusGarments_KWashingProgram.dyingComp = nexusGarments_KWashingCompany.id";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    //m_dataReader1=null



                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(),  m_dataReader["inlotweight"].ToString().Trim(), m_dataReader["inlotrolls"].ToString().Trim(),  m_dataReader["delweight"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim(), m_dataReader["ksource"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }   
        }



        internal void LoadKWashingEditProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Washing Company");
            dt.Columns.Add("inlotweight", System.Type.GetType("System.Double"));
            dt.Columns.Add("inlotrolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("delWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("delRolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("recWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recRolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("reqWeight", System.Type.GetType("System.Double"));

            dt.Columns.Add("mapid");



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT    nexusGarments_KWashingProgram.id,nexusGarments_KWashingProgram.ksource, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle, nexusGarments_KWashingCompany.Company,nexusGarments_KWashingProgram.dia, nexusGarments_KWashingProgram.reqWeight,nexusGarments_ProcessWeight.recweight - nexusGarments_ProcessWeight.desWeight as inlotweight, nexusGarments_ProcessWeight.recrolls - nexusGarments_ProcessWeight.desrolls as inlotrolls, nexusGarments_KWashingProgram.delWeight, nexusGarments_KWashingProgram.delRolls, nexusGarments_KWashingProgram.recWeight , nexusGarments_KWashingProgram.recRolls FROM   nexusGarments_KWashingProgram INNER JOIN  nexusGarments_ProcessWeight ON nexusGarments_KWashingProgram.ksource = nexusGarments_ProcessWeight.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_KWashingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN  nexusGarments_Master_Style ON nexusGarments_KWashingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_KWashingCompany ON nexusGarments_KWashingProgram.dyingComp = nexusGarments_KWashingCompany.id";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    //m_dataReader1=null



                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["inlotweight"].ToString().Trim(), m_dataReader["inlotrolls"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim(), m_dataReader["ksource"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadKwashingInwardProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("DC NO");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Del Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recrolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("recweight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Washing Company");
            dt.Columns.Add("mapid");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_kWashingProcess.id, nexusGarments_kWashingProcess.dcno, nexusGarments_Master_Style.ProcessStyle,nexusGarments_Master_fabric.fabricName, nexusGarments_kwashingprogram.dia, nexusGarments_kWashingProcess.delrolls,nexusGarments_kWashingProcess.delWeight, nexusGarments_kWashingProcess.recRolls, nexusGarments_kWashingProcess.recWeight,nexusGarments_kWashingCompany.Company, nexusGarments_kWashingProcess.mapid FROM nexusGarments_kWashingProcess INNER JOIN  nexusGarments_kWashingProgram ON nexusGarments_kWashingProcess.mapid = nexusGarments_kWashingProgram.id INNER JOIN   nexusGarments_Master_Style ON nexusGarments_kwashingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_KWashingprogram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_kWashingCompany ON nexusGarments_kWashingProgram.dyingComp = nexusGarments_KWashingCompany.id where nexusGarments_kWashingProcess.isclosed ='False'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(),  m_dataReader["company"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadKwashingInwardProcess(DataTable dt,string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("DC NO");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Del Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recrolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("recweight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Washing Company");
            dt.Columns.Add("mapid");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_kWashingProcess.id, nexusGarments_kWashingProcess.dcno, nexusGarments_Master_Style.ProcessStyle,nexusGarments_Master_fabric.fabricName, nexusGarments_kwashingprogram.dia, nexusGarments_kWashingProcess.delrolls,nexusGarments_kWashingProcess.delWeight, nexusGarments_kWashingProcess.recRolls, nexusGarments_kWashingProcess.recWeight,nexusGarments_kWashingCompany.Company, nexusGarments_kWashingProcess.mapid FROM nexusGarments_kWashingProcess INNER JOIN  nexusGarments_kWashingProgram ON nexusGarments_kWashingProcess.mapid = nexusGarments_kWashingProgram.id INNER JOIN   nexusGarments_Master_Style ON nexusGarments_kwashingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_KWashingprogram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_kWashingCompany ON nexusGarments_kWashingProgram.dyingComp = nexusGarments_KWashingCompany.id where nexusGarments_kWashingProcess.dcno="+dcno+"'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadwashingInwardProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("DC NO");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Del Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recrolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("recweight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Washing Company");
            dt.Columns.Add("mapid");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_WashingProcess.id, nexusGarments_WashingProcess.dcno, nexusGarments_Master_Style.ProcessStyle,nexusGarments_Master_fabric.fabricName, nexusGarments_WashingProgram.dia, nexusGarments_WashingProcess.delrolls,nexusGarments_WashingProcess.delWeight, nexusGarments_WashingProcess.recRolls, nexusGarments_WashingProcess.recWeight,nexusGarments_kWashingCompany.Company, nexusGarments_WashingProcess.mapid FROM         nexusGarments_WashingProcess INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_WashingProgram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_WashingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_KWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id WHERE     (nexusGarments_WashingProcess.isclosed = 'False') and (nexusGarments_WashingProcess.isapproved = 'True')";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadWashingProcessEDIT(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Washing Company");

            dt.Columns.Add("inlotrolls");
            dt.Columns.Add("Avail Weight", System.Type.GetType("System.Double"));

            dt.Columns.Add("delWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("delRolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("recWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recRolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("reqWeight", System.Type.GetType("System.Double"));

            dt.Columns.Add("mapid");



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_Style.ProcessStyle,nexusGarments_WashingProgram.id, nexusGarments_Master_fabric.fabricName, nexusGarments_washingprogram.dia,  nexusGarments_KWashingCompany.Company,nexusGarments_processweight.PrintRecRolls - nexusGarments_processweight.PrintDelRolls as Availrolls,nexusGarments_processweight.PrintRecWeight - nexusGarments_processweight.PrintDelWeight as AvailWeight, nexusGarments_WashingProgram.delWeight,nexusGarments_WashingProgram.delRolls,nexusGarments_WashingProgram.reqweight, nexusGarments_WashingProgram.recWeight, nexusGarments_WashingProgram.recRolls,  nexusGarments_WashingProgram.kid FROM  nexusGarments_WashingProgram INNER JOIN nexusGarments_processweight ON nexusGarments_WashingProgram.ksource = nexusGarments_processweight.id INNER JOIN   nexusGarments_KWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_washingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_washingprogram.Fabricid = nexusGarments_Master_fabric.fabricId where  nexusGarments_processweight.isprocessclosed <> 'True'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["Availrolls"].ToString().Trim(), m_dataReader["availweight"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim(), m_dataReader["kid"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadWashingProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Style");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Washing Company");

            dt.Columns.Add("inlotrolls");
            dt.Columns.Add("Avail Weight", System.Type.GetType("System.Double"));

            dt.Columns.Add("delWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("delRolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("recWeight", System.Type.GetType("System.Double"));
            dt.Columns.Add("recRolls", System.Type.GetType("System.Double"));

            dt.Columns.Add("reqWeight", System.Type.GetType("System.Double"));

            dt.Columns.Add("mapid");



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_Master_Style.ProcessStyle,nexusGarments_WashingProgram.id, nexusGarments_Master_fabric.fabricName, nexusGarments_washingprogram.dia,  nexusGarments_KWashingCompany.Company,nexusGarments_processweight.PrintRecRolls - nexusGarments_processweight.PrintDelRolls as Availrolls,nexusGarments_processweight.PrintRecWeight - nexusGarments_processweight.PrintDelWeight as AvailWeight, nexusGarments_WashingProgram.delWeight,nexusGarments_WashingProgram.delRolls,nexusGarments_WashingProgram.reqweight, nexusGarments_WashingProgram.recWeight, nexusGarments_WashingProgram.recRolls,  nexusGarments_WashingProgram.kid FROM  nexusGarments_WashingProgram INNER JOIN nexusGarments_processweight ON nexusGarments_WashingProgram.ksource = nexusGarments_processweight.id INNER JOIN   nexusGarments_KWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_washingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_washingprogram.Fabricid = nexusGarments_Master_fabric.fabricId where nexusGarments_processweight.PrintRecWeight - nexusGarments_processweight.PrintDelWeight > 0 and nexusGarments_processweight.isprocessclosed <> 'True'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["Availrolls"].ToString().Trim(), m_dataReader["availweight"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["recWeight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["reqweight"].ToString().Trim(), m_dataReader["kid"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadWashingReadytoClose(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Company");

            dt.Columns.Add("Deliverd", System.Type.GetType("System.Double"));
            dt.Columns.Add("Recieved", System.Type.GetType("System.Double"));

            dt.Columns.Add("Status");
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_WashingProcess.id, nexusGarments_WashingProcess.isclosed, nexusGarments_WashingProcess.dcno,nexusGarments_Master_fabric.fabricName, nexusGarments_KWashingCompany.Company, nexusGarments_WashingProcess.delWeight,nexusGarments_WashingProcess.recWeight FROM nexusGarments_WashingProcess INNER JOIN   nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_KWashingCompany ON    nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_WashingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId where nexusGarments_WashingProcess.isclosed='False'";


            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    string status = "Open";
                    if (m_dataReader["isclosed"].ToString().Trim() == "True")
                    {
                        status = "Close";
                    }
                    else
                    {
                        status = "Open";
                    }
                    if ((Convert.ToDouble(m_dataReader["recweight"].ToString().Trim())) != 0 && status != "Close")
                    {
                        double se = ((Convert.ToDouble(m_dataReader["delweight"].ToString().Trim()) * 100) / (Convert.ToDouble(m_dataReader["recweight"].ToString().Trim()))) - 100;
                        if (se < 1)
                        {
                            status = "Ready to Close";
                        }
                    }
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["isclosed"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), status);

                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadKnittingDC(DataTable knittinginward, string p, DevExpress.XtraEditors.LabelControl lblBags,
            DevExpress.XtraEditors.LabelControl lblCompany, DevExpress.XtraEditors.LabelControl lblWeight,
            DevExpress.XtraEditors.LabelControl lblDeliveryDate, DevExpress.XtraEditors.LabelControl lblRecWeight, DevExpress.XtraEditors.LabelControl lblid)
        {
            knittinginward.Columns.Clear();
            knittinginward.Rows.Clear();

            knittinginward.Columns.Add("GG");
            knittinginward.Columns.Add("LL");
            knittinginward.Columns.Add("GSM");
            knittinginward.Columns.Add("Dia");
            knittinginward.Columns.Add("Rec Rolls", System.Type.GetType("System.Decimal"));
            knittinginward.Columns.Add("Rec Weight", System.Type.GetType("System.Decimal"));
            knittinginward.Columns.Add("Yarn Retund", System.Type.GetType("System.Decimal"));
    

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_KnittingProcess.recWeight,nexusGarments_Master_KnittingCompany.Company, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm,nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll, nexusGarments_KnittingProcess.id, nexusGarments_KnittingProcess.DCNo,nexusGarments_KnittingProcess.delBags, nexusGarments_KnittingProcess.delWeight, nexusGarments_KnittingProcess.deldate,nexusGarments_KnittingProcess.mapid FROM nexusGarments_KnittingProgram INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_KnittingProgram.id = nexusGarments_KnittingProcess.mapid  where nexusGarments_KnittingProcess.dcno='" + p + "'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    lblBags.Text = m_dataReader["delBags"].ToString().Trim();
                    lblCompany.Text = m_dataReader["Company"].ToString().Trim();
                    lblWeight.Text = m_dataReader["delweight"].ToString().Trim();
                    lblDeliveryDate.Text = m_dataReader["deldate"].ToString().Trim();
                    lblRecWeight.Text = m_dataReader["recweight"].ToString().Trim();
                    lblid.Text = m_dataReader["id"].ToString().Trim();
                    //m_dataReader["id"].ToString().Trim(),
                    // m_dataReader["mapid"].ToString().Trim(),
                    knittinginward.Rows.Add(m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim(), m_dataReader["gsm"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), 0, 0, 0);

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void loadpopupmessage(string sQuery, System.Windows.Forms.TextBox txtMessage, DevExpress.XtraEditors.LabelControl lblCode, DevExpress.XtraEditors.LabelControl lblid)
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = sQuery;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    //Devddl.Properties.Items.Add(m_dataReader[field].ToString());
                    txtMessage.Text = m_dataReader["dismessage"].ToString();
                    lblCode.Text = m_dataReader["mcode"].ToString();
                    lblid.Text = m_dataReader["id"].ToString();

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadKwashingInwardProcess(DataTable dt, string dcno, DevExpress.XtraEditors.LabelControl lblBags, 
            DevExpress.XtraEditors.LabelControl lblCompany, DevExpress.XtraEditors.LabelControl lblDeliveryDate, 
            DevExpress.XtraEditors.LabelControl lblRecRolls, DevExpress.XtraEditors.LabelControl lblRecWeight,
            DevExpress.XtraEditors.LabelControl lblWeight, DevExpress.XtraEditors.LabelControl lblid)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Decimal"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_kWashingProcess.deldate,nexusGarments_kWashingProcess.id, nexusGarments_kWashingProcess.dcno, nexusGarments_Master_Style.ProcessStyle,nexusGarments_Master_fabric.fabricName, nexusGarments_kwashingprogram.dia, nexusGarments_kWashingProcess.delrolls,nexusGarments_kWashingProcess.delWeight, nexusGarments_kWashingProcess.recRolls, nexusGarments_kWashingProcess.recWeight,nexusGarments_kWashingCompany.Company, nexusGarments_kWashingProcess.mapid FROM nexusGarments_kWashingProcess INNER JOIN  nexusGarments_kWashingProgram ON nexusGarments_kWashingProcess.mapid = nexusGarments_kWashingProgram.id INNER JOIN   nexusGarments_Master_Style ON nexusGarments_kwashingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_KWashingprogram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_kWashingCompany ON nexusGarments_kWashingProgram.dyingComp = nexusGarments_KWashingCompany.id where nexusGarments_kWashingProcess.dcno='" + dcno + "'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {

                        lblBags.Text = m_dataReader["delrolls"].ToString().Trim();
                        lblCompany.Text = m_dataReader["company"].ToString().Trim();
                        lblDeliveryDate.Text = m_dataReader["deldate"].ToString().Trim();
                        lblRecRolls.Text=m_dataReader["recrolls"].ToString().Trim();
                        lblRecWeight.Text = m_dataReader["recweight"].ToString().Trim();
                        lblid.Text = m_dataReader["id"].ToString().Trim();
                        lblWeight.Text = m_dataReader["delweight"].ToString().Trim();
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    //m_dataReader["id"].ToString().Trim(), m_dataReader["dcno"].ToString().Trim(), m_dataReader["ProcessStyle"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["company"].ToString().Trim(), m_dataReader["mapid"].ToString().Trim()
                        dt.Rows.Add(m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(),0 ,0);

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadHeatSettingDC(DataTable dt, string dcno, DevExpress.XtraEditors.LabelControl lblCompany,
            DevExpress.XtraEditors.LabelControl lblDeliveryDate, DevExpress.XtraEditors.LabelControl lblDelRolls,
            DevExpress.XtraEditors.LabelControl lblDelWeight, DevExpress.XtraEditors.LabelControl lblrecBags,
            DevExpress.XtraEditors.LabelControl lblRecWeight, DevExpress.XtraEditors.LabelControl lblStyle, DevExpress.XtraEditors.LabelControl lblid)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();

            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("F GSM");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Decimal"));


            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT  nexusGarments_HeatSettingProcess.deldate,nexusGarments_Master_fabric.fabricName,nexusGarments_HeatSettingProcess.recWeight, nexusGarments_HeatSettingProcess.recRolls, nexusGarments_HeatSettingProcess.delWeight,nexusGarments_HeatSettingProcess.delrolls, nexusGarments_HeatSettingProcess.dcno, nexusGarments_HeatSettingProcess.mapid,    nexusGarments_HeatSettingProcess.id, nexusGarments_heatsettingprogram.dia, nexusGarments_Master_HeatSettingCompany.Company AS heatsetting,  nexusGarments_Master_Style.ProcessStyle, nexusGarments_heatsettingprogram.hgsm, nexusGarments_HeatSettingProgram.FGSM FROM nexusGarments_HeatSettingProcess INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id   INNER JOIN nexusGarments_Master_HeatSettingCompany ON nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_heatsettingprogram.StyleId = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_heatsettingprogram.Fabricid = nexusGarments_Master_fabric.fabricId where nexusGarments_heatsettingprocess.dcno='" + dcno + "'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    lblCompany.Text = m_dataReader["heatsetting"].ToString();
                    lblDeliveryDate.Text = m_dataReader["deldate"].ToString();
                    lblDelRolls.Text = m_dataReader["delrolls"].ToString();
                    lblDelWeight.Text = m_dataReader["delweight"].ToString();
                    lblrecBags.Text = m_dataReader["recrolls"].ToString();
                    lblRecWeight.Text = m_dataReader["recweight"].ToString();
                    lblStyle.Text = m_dataReader["ProcessStyle"].ToString();
                    lblid.Text = m_dataReader["id"].ToString();
                    dt.Rows.Add( m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["FGSM"].ToString().Trim(),0,0);

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadDyingInwardProcess(DataTable dt, string dcno, DevExpress.XtraEditors.LabelControl lblCompany, 
            DevExpress.XtraEditors.LabelControl lblDeliveryDate, DevExpress.XtraEditors.LabelControl lblRecRolls,
            DevExpress.XtraEditors.LabelControl lblRecWeight, DevExpress.XtraEditors.LabelControl lblWeight,
            DevExpress.XtraEditors.LabelControl lblid, DevExpress.XtraEditors.LabelControl lblBags)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();

            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Color");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Decimal"));



            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT  nexusGarments_DyingProcess.deldate,nexusGarments_Master_fabric.fabricName, nexus_Master_DyingColor.DyingColor, nexusGarments_Master_Style.ProcessStyle, nexusGarments_DyingProcess.dcno, nexusGarments_DyingProcess.id, nexusGarments_DyingProcess.delrolls, nexusGarments_DyingProcess.delWeight,  nexusGarments_DyingProcess.recRolls, nexusGarments_DyingProcess.recWeight, nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm,  nexusGarments_DyingProcess.mapid, nexusGarmrnts_Master_DyingCompany.Company FROM nexusGarments_KnittingProgram INNER JOIN nexusGarments_DyingProgram ON nexusGarments_KnittingProgram.id = nexusGarments_DyingProgram.kid INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN  nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN   nexus_Master_DyingColor ON nexusGarments_DyingProgram.dyingColor = nexus_Master_DyingColor.id INNER JOIN nexusGarments_DyingProcess ON nexusGarments_DyingProgram.id = nexusGarments_DyingProcess.mapid  INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id where nexusGarments_DyingProcess.dcno='"+dcno+"'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    lblCompany.Text = m_dataReader["Company"].ToString().Trim();
                    lblDeliveryDate.Text = m_dataReader["deldate"].ToString().Trim();
                    lblRecRolls.Text = m_dataReader["recrolls"].ToString().Trim();
                    lblRecWeight.Text = m_dataReader["recweight"].ToString().Trim();
                    lblWeight.Text = m_dataReader["delweight"].ToString().Trim();
                    lblid.Text = m_dataReader["id"].ToString().Trim();
                    lblBags.Text = m_dataReader["delrolls"].ToString().Trim();
                    dt.Rows.Add( m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["dyingcolor"].ToString().Trim(), 0,0);

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }     
        }

        internal void LoadPrintingInwardProcess(DataTable dt, string dcno, DevExpress.XtraEditors.LabelControl lblBags, 
            DevExpress.XtraEditors.LabelControl lblCompany, DevExpress.XtraEditors.LabelControl lblDeliveryDate, 
            DevExpress.XtraEditors.LabelControl lblRecRolls, DevExpress.XtraEditors.LabelControl lblRecWeight,
            DevExpress.XtraEditors.LabelControl lblWeight, DevExpress.XtraEditors.LabelControl lblid)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();

            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Color");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Decimal"));
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_PrintingProcess.deldate,nexusGarments_PrintingProcess.id, nexusGarments_PrintingProcess.mapid, nexusGarments_PrintingProcess.dcno, nexusGarments_PrintingProcess.delrolls,  nexusGarments_PrintingProcess.delWeight, nexusGarments_PrintingProcess.recRolls, nexusGarments_PrintingProcess.recWeight,     nexusGarments_Master_Style.ProcessStyle, nexusGarments_Master_fabric.fabricName, nexusGarments_PrintingDesign.DesignName,   nexusGarments_PrintingCompany.Company, nexusGarments_PrintingColor.ColorName,nexusGarments_knittingprogram.dia FROM  nexusGarments_PrintingProcess INNER JOIN   nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id INNER JOIN   nexusGarments_KnittingProgram ON nexusGarments_PrintingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN   nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN    nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN   nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN    nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id INNER JOIN   nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN  nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id where nexusGarments_PrintingProcess.dcno='" + dcno + "'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    lblCompany.Text = m_dataReader["Company"].ToString().Trim();
                    lblDeliveryDate.Text = m_dataReader["deldate"].ToString().Trim();
                    lblRecRolls.Text = m_dataReader["recrolls"].ToString().Trim();
                    lblRecWeight.Text = m_dataReader["recweight"].ToString().Trim();
                    lblWeight.Text = m_dataReader["delweight"].ToString().Trim();
                    lblid.Text = m_dataReader["id"].ToString().Trim();
                    lblBags.Text = m_dataReader["delrolls"].ToString().Trim();
                    dt.Rows.Add(m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["ColorName"].ToString().Trim(), 0, 0);

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadwashingInwardProcess(DataTable dt, string dcno, DevExpress.XtraEditors.LabelControl lblBags, 
            DevExpress.XtraEditors.LabelControl lblCompany, DevExpress.XtraEditors.LabelControl lblDeliveryDate, 
            DevExpress.XtraEditors.LabelControl lblRecRolls, DevExpress.XtraEditors.LabelControl lblRecWeight, 
            DevExpress.XtraEditors.LabelControl lblWeight, DevExpress.XtraEditors.LabelControl lblid)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Decimal"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_WashingProcess.deldate,nexusGarments_WashingProcess.id, nexusGarments_WashingProcess.dcno, nexusGarments_Master_Style.ProcessStyle,nexusGarments_Master_fabric.fabricName, nexusGarments_KnittingProgram.dia, nexusGarments_WashingProcess.delrolls,nexusGarments_WashingProcess.delWeight, nexusGarments_WashingProcess.recRolls, nexusGarments_WashingProcess.recWeight,nexusGarments_kWashingCompany.Company, nexusGarments_WashingProcess.mapid FROM nexusGarments_WashingProcess INNER JOIN  nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_WashingProgram.kid = nexusGarments_KnittingProgram.id INNER JOIN   nexusGarments_YarnPurchase ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnPurchase.id INNER JOIN  nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_kWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id where nexusGarments_WashingProcess.dcno='" + dcno + "'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    lblBags.Text = m_dataReader["recrolls"].ToString();
                    lblCompany.Text = m_dataReader["Company"].ToString();
                    lblDeliveryDate.Text = m_dataReader["deldate"].ToString();
                    lblRecRolls.Text = m_dataReader["recrolls"].ToString();
                    lblRecWeight.Text = m_dataReader["recweight"].ToString();
                    lblWeight.Text = m_dataReader["delweight"].ToString();
                    lblid.Text = m_dataReader["id"].ToString();
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add( m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), 0,0);

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void loaddcreprint(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));

            dt.Columns.Add("DC Number");//, System.Type.GetType("System.Decimal")
            dt.Columns.Add("Date");
            dt.Columns.Add("Company");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Del Weight");
            dt.Columns.Add("Del Bags");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_KnittingProcess.id, nexusGarments_KnittingProcess.DCNo, nexusGarments_KnittingProcess.delWeight, nexusGarments_KnittingProcess.delBags,  nexusGarments_KnittingProcess.deldate, nexusGarments_Master_YarnSupplier.Supplier, nexusGarments_Master_KnittingCompany.Company,  nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll,  nexusGarments_Master_fabric.fabricName, nexusGarmrnts_Master_Counts.Counts, nexusGarments_Master_KnittingCompany.Address1 FROM  nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id where nexusGarments_KnittingProcess.isapproved='true' ORDER BY nexusGarments_KnittingProcess.id DESC";
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    if (id != m_dataReader["id"].ToString().Trim())
                    {
                        id = m_dataReader["id"].ToString().Trim();
                        dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "False", m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["delbags"].ToString().Trim());
                    }

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void loaddcKwashingreprint(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));

            dt.Columns.Add("DC Number");//, System.Type.GetType("System.Decimal")
            dt.Columns.Add("Date");
            dt.Columns.Add("Company");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Del Weight");
            dt.Columns.Add("Del Bags");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_WashingProcess.deldate, nexusGarments_WashingProcess.id, nexusGarments_WashingProcess.dcno,  nexusGarments_KWashingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle, nexusGarments_WashingProcess.delWeight, nexusGarments_WashingProcess.delrolls, nexusGarments_Master_YarnSupplier.Supplier,  nexusGarmrnts_Master_Counts.Counts FROM         nexusGarments_WashingProcess INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_KWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_WashingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_WashingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_WashingProgram.ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_KnittingProgram.Ksource INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id where nexusGarments_WashingProcess.isapproved='True' ORDER BY nexusGarments_WashingProcess.id DESC";
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    if (id != m_dataReader["id"].ToString().Trim())
                    {
                        id = m_dataReader["id"].ToString().Trim();
                        dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "False", m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim());
                    }

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void loaddcDyingreprint(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));

            dt.Columns.Add("DC Number");//, System.Type.GetType("System.Decimal")
            dt.Columns.Add("Date");
            dt.Columns.Add("Company");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Color");
            dt.Columns.Add("Del Weight");
            dt.Columns.Add("Del Bags");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_DyingProgram.dia, nexusGarments_DyingProcess.deldate, nexusGarments_DyingProcess.id, nexusGarments_DyingProcess.dcno,  nexusGarmrnts_Master_DyingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle,  nexusGarments_DyingProcess.delWeight, nexusGarments_DyingProcess.delrolls, nexusGarments_Master_YarnSupplier.Supplier,  nexusGarmrnts_Master_Counts.Counts, nexusGarments_PrintingColor.ColorName FROM         nexusGarments_DyingProcess INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_DyingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_DyingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_DyingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_KnittingProgram.Ksource INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id where nexusGarments_DyingProcess.isapproved='True' ORDER BY nexusGarments_DyingProcess.id DESC";
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    if (id != m_dataReader["id"].ToString().Trim())
                    {
                        id = m_dataReader["id"].ToString().Trim();
                        dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "False", m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(),m_dataReader["colorname"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim());
                    }

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void loaddcPrintingreprint(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));

            dt.Columns.Add("DC Number");//, System.Type.GetType("System.Decimal")
            dt.Columns.Add("Date");
            dt.Columns.Add("Company");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Color");
            dt.Columns.Add("Del Weight");
            dt.Columns.Add("Del Bags");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_Printingprogram.dia, nexusGarments_Printingprocess.deldate, nexusGarments_Printingprocess.id, nexusGarments_Printingprocess.dcno,  nexusGarments_PrintingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle,  nexusGarments_Printingprocess.delWeight, nexusGarments_Printingprocess.delrolls, nexusGarments_PrintingColor.ColorName FROM         nexusGarments_Printingprocess INNER JOIN nexusGarments_Printingprogram ON nexusGarments_Printingprocess.mapid = nexusGarments_Printingprogram.id INNER JOIN nexusGarments_PrintingCompany ON nexusGarments_Printingprogram.printingComp = nexusGarments_PrintingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_Printingprogram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_Printingprogram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_Printingprogram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_KnittingProgram.Ksource INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_Printingprogram.Colorid = nexusGarments_PrintingColor.id where nexusGarments_Printingprocess.isapproved='true' ORDER BY nexusGarments_Printingprocess.id DESC";
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    if (id != m_dataReader["id"].ToString().Trim())
                    {
                        id = m_dataReader["id"].ToString().Trim();
                        dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "False", m_dataReader["dcno"].ToString().Trim(), m_dataReader["deldate"].ToString().Trim(), m_dataReader["Company"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["colorname"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["delrolls"].ToString().Trim());
                    }

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void knittingdc(string dt)
        {
             
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT    nexusGarments_Master_Style.ProcessStyle, nexusGarments_KnittingProcess.id, nexusGarments_KnittingProcess.DCNo, nexusGarments_KnittingProcess.delWeight, nexusGarments_KnittingProcess.delBags,  nexusGarments_KnittingProcess.deldate, nexusGarments_Master_YarnSupplier.Supplier, nexusGarments_Master_KnittingCompany.Company,  nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll,  nexusGarments_Master_fabric.fabricName, nexusGarmrnts_Master_Counts.Counts, nexusGarments_Master_KnittingCompany.Address1 ,(SELECT Empname FROM nexusGarments_Master_Employee where id=(select preparedby from nexusGarments_KnittingProcess where id=" + dt + ")) as preparedby,(SELECT EmpSignature FROM nexusGarments_Master_Employee where id=(select preparedby from nexusGarments_KnittingProcess where id=" + dt + ")) as preparedSignature,(SELECT Empname FROM nexusGarments_Master_Employee where id=(select ApprovedBy from nexusGarments_KnittingProcess where id=" + dt + ")) as approvedby,(SELECT EmpSignature FROM nexusGarments_Master_Employee where id=(select approvedby from nexusGarments_KnittingProcess where id=" + dt + ")) as approvedSignature FROM  nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id  INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id where nexusGarments_KnittingProcess.id='" + dt + "' ORDER BY nexusGarments_KnittingProcess.id DESC";
            try
            {


                supplier = "Supplier";
                counts = "Counts";
               
                //Report.KnittingDC sd = new NaveenaAccounts.Report.KnittingDC();
               
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    supplier = supplier +","+ m_dataReader["supplier"].ToString().Trim();
                    counts = counts +","+ m_dataReader["counts"].ToString().Trim();
                    delbags = m_dataReader["delBags"].ToString().Trim();
                    delweight = m_dataReader["delweight"].ToString().Trim();
                    dia = m_dataReader["dia"].ToString().Trim();
                    ll = m_dataReader["ll"].ToString().Trim();
                    gg = m_dataReader["gg"].ToString().Trim();
                    company = m_dataReader["Company"].ToString().Trim();
                    gsm = m_dataReader["gsm"].ToString().Trim();
                    style = m_dataReader["processstyle"].ToString().Trim();
                    fabric= m_dataReader["fabricname"].ToString().Trim();
                    dcno = m_dataReader["dcno"].ToString().Trim();
                    preparedby = m_dataReader["preparedby"].ToString().Trim();
                    Approved = m_dataReader["approvedby"].ToString().Trim();

                    byte[] Presign = (byte[])m_dataReader["preparedSignature"];
                    byte[] appSign = (byte[])m_dataReader["approvedSignature"];

                    //Initialize image variable
                    
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(Presign, 0, Presign.Length))
                    {
                        ms.Write(Presign, 0, Presign.Length);

                        //Set image variable value using memory stream.
                        PreparedSign = Image.FromStream(ms, true);
                    }


                    using (MemoryStream ms = new MemoryStream(appSign, 0, appSign.Length))
                    {
                        ms.Write(appSign, 0, appSign.Length);
                        
                        //Set image variable value using memory stream.
                        ApprovedSign = Image.FromStream(ms, true);
                    }
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                 
                
                }
                //sd.barcodeDCNo.Text = dcno;
                //sd.lblbags.Text = delbags;
                //sd.lblCompany.Text = company;
                //sd.lblCounts.Text = counts;
                //sd.lblDia.Text = dia;
                //sd.lblFabric.Text = fabric;
                //sd.lblGG.Text = gg;
                //sd.lblGSM.Text = gsm;
                //sd.lblLL.Text = ll;
                //sd.lblMill.Text = supplier;
                //sd.lblStyle.Text = style; 
                //sd.lblWeight.Text = delweight;
                //sd.lblPreparedBy.Text = preparedby;
                //sd.lblApprovedBy.Text = Approved;
                //sd.ptrApprovedBySign.Image = ApprovedSign;
                //sd.ptrPreparedBySign.Image = PreparedSign;
                //sd.ShowPreview();
              
               
            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void washingdc(string dt)
        {

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_knittingprogram.dia,nexusGarments_PrintingColor.ColorName,   nexusGarments_WashingProcess.deldate, nexusGarments_WashingProcess.id, nexusGarments_WashingProcess.dcno, nexusGarments_KWashingCompany.Company,  nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle, nexusGarments_WashingProcess.delWeight,  nexusGarments_WashingProcess.delrolls, nexusGarments_Master_YarnSupplier.Supplier, nexusGarmrnts_Master_Counts.Counts ,(SELECT Empname FROM nexusGarments_Master_Employee where id=(select preparedby from nexusGarments_WashingProcess where dcno=" + dt + ")) as preparedby,(SELECT EmpSignature FROM nexusGarments_Master_Employee where id=(select preparedby from nexusGarments_WashingProcess where dcno=" + dt + ")) as preparedSignature,(SELECT Empname FROM nexusGarments_Master_Employee where id=(select ApprovedBy from nexusGarments_WashingProcess where dcno=" + dt + ")) as approvedby,(SELECT EmpSignature FROM nexusGarments_Master_Employee where id=(select approvedby from nexusGarments_WashingProcess where dcno=" + dt + ")) as approvedSignature FROM         nexusGarments_WashingProcess INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_WashingProgram.Colorid = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_KWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_WashingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_WashingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_WashingProgram.ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_KnittingProgram.Ksource INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id where nexusGarments_WashingProcess.dcno=" + dt + " ORDER BY nexusGarments_Master_YarnSupplier.supplier DESC";
            try
            {


                supplier = "";string suppt = "";
                string Fcolor = "";
                counts = ""; string countst = "";
                //Report.DyingDC sd = new NaveenaAccounts.Report.DyingDC();
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    if (suppt != m_dataReader["supplier"].ToString().Trim())
                    {
                        suppt = m_dataReader["supplier"].ToString().Trim();
                        supplier = supplier + "," + m_dataReader["supplier"].ToString().Trim();
                    }
                    if (countst != m_dataReader["counts"].ToString().Trim())
                    {
                        countst = m_dataReader["counts"].ToString().Trim();
                        counts = counts + "," + m_dataReader["counts"].ToString().Trim();
                    }
                    delbags = m_dataReader["delrolls"].ToString().Trim();
                    delweight = m_dataReader["delweight"].ToString().Trim();
                    dia = m_dataReader["dia"].ToString().Trim();
                  
                    company = m_dataReader["Company"].ToString().Trim();
                    Fcolor = m_dataReader["colorname"].ToString().Trim();
                    style = m_dataReader["processstyle"].ToString().Trim();
                    fabric = m_dataReader["fabricname"].ToString().Trim();
                    dcno = m_dataReader["dcno"].ToString().Trim();
                   
                    preparedby = m_dataReader["preparedby"].ToString().Trim();
                    Approved = m_dataReader["approvedby"].ToString().Trim();

                    byte[] Presign = (byte[])m_dataReader["preparedSignature"];
                    byte[] appSign = (byte[])m_dataReader["approvedSignature"];

                    //Initialize image variable

                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(Presign, 0, Presign.Length))
                    {
                        ms.Write(Presign, 0, Presign.Length);

                        //Set image variable value using memory stream.
                        PreparedSign = Image.FromStream(ms, true);
                    }


                    using (MemoryStream ms = new MemoryStream(appSign, 0, appSign.Length))
                    {
                        ms.Write(appSign, 0, appSign.Length);

                        //Set image variable value using memory stream.
                        ApprovedSign = Image.FromStream(ms, true);
                    }
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();


                }
                //XtraMessageBox 
                //sd.barcodeDCNo.Text = dt;
                //sd.lblRolls.Text = delbags;
                //sd.lblCompany.Text = company;
                //sd.lblSupplier.Text = supplier;
                //sd.lblCounts.Text = counts;
                //sd.lblDia.Text = dia;
                //sd.lblFabric.Text = fabric;
                //sd.lblColor.Text = Fcolor;
                //sd.lblStyle.Text = style;
                //sd.lblWeight.Text = delweight;
                //sd.lblPreparedBy.Text = preparedby;
                //sd.lblApprovedBy.Text = Approved;
                //sd.ptrApprovedBySign.Image = ApprovedSign;
                //sd.ptrPreparedBySign.Image = PreparedSign;
                //sd.lblLabTipCap.Visible = false;
                //sd.lblTooltip.Visible = false;
                //sd.ShowPreview();

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void Dyingdc(string dt)
        {

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_DyingProgram.labtip,nexusGarments_DyingProgram.dia, nexusGarments_DyingProcess.deldate, nexusGarments_DyingProcess.id, nexusGarments_DyingProcess.dcno,  nexusGarmrnts_Master_DyingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle,  nexusGarments_DyingProcess.delWeight, nexusGarments_DyingProcess.delrolls, nexusGarments_Master_YarnSupplier.Supplier,  nexusGarmrnts_Master_Counts.Counts, nexusGarments_PrintingColor.ColorName,(SELECT Empname FROM nexusGarments_Master_Employee where id=(select preparedby from nexusGarments_DyingProcess where dcno=" + dt + ")) as preparedby,(SELECT EmpSignature FROM nexusGarments_Master_Employee where id=(select preparedby from nexusGarments_DyingProcess where dcno=" + dt + ")) as preparedSignature,(SELECT Empname FROM nexusGarments_Master_Employee where id=(select ApprovedBy from nexusGarments_DyingProcess where dcno=" + dt + ")) as approvedby,(SELECT EmpSignature FROM nexusGarments_Master_Employee where id=(select approvedby from nexusGarments_DyingProcess where dcno=" + dt + ")) as approvedSignature  FROM         nexusGarments_DyingProcess INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_DyingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_DyingProgram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_DyingProgram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_KnittingProgram.Ksource INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id where nexusGarments_dyingProcess.dcno=" + dt + " ORDER BY nexusGarments_Master_YarnSupplier.supplier DESC";
            try
            {

                string Fcolor = "";
                supplier = ""; string suppt = "";
                counts = ""; string countst = "";
                //Report.DyingDC sd = new NaveenaAccounts.Report.DyingDC();
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    if (suppt != m_dataReader["supplier"].ToString().Trim())
                    {
                        suppt = m_dataReader["supplier"].ToString().Trim();
                        supplier = supplier + "," + m_dataReader["supplier"].ToString().Trim();
                    }
                    if (countst != m_dataReader["counts"].ToString().Trim())
                    {
                        countst = m_dataReader["counts"].ToString().Trim();
                        counts = counts + "," + m_dataReader["counts"].ToString().Trim();
                    }
                    delbags = m_dataReader["delrolls"].ToString().Trim();
                    delweight = m_dataReader["delweight"].ToString().Trim();
                    dia = m_dataReader["dia"].ToString().Trim();
                    Fcolor = m_dataReader["Colorname"].ToString().Trim();
                    company = m_dataReader["Company"].ToString().Trim();
                    deldate = m_dataReader["deldate"].ToString().Trim().Substring(0,10);
                    style = m_dataReader["processstyle"].ToString().Trim();
                    fabric = m_dataReader["fabricname"].ToString().Trim();
                    dcno = m_dataReader["dcno"].ToString().Trim();
                    lbltip = m_dataReader["labtip"].ToString().Trim();
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();
                    preparedby = m_dataReader["preparedby"].ToString().Trim();
                    Approved = m_dataReader["approvedby"].ToString().Trim();

                    byte[] Presign = (byte[])m_dataReader["preparedSignature"];
                    byte[] appSign = (byte[])m_dataReader["approvedSignature"];

                    //Initialize image variable

                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(Presign, 0, Presign.Length))
                    {
                        ms.Write(Presign, 0, Presign.Length);

                        //Set image variable value using memory stream.
                        PreparedSign = Image.FromStream(ms, true);
                    }


                    using (MemoryStream ms = new MemoryStream(appSign, 0, appSign.Length))
                    {
                        ms.Write(appSign, 0, appSign.Length);

                        //Set image variable value using memory stream.
                        ApprovedSign = Image.FromStream(ms, true);
                    }
                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();


                }
                //sd.barcodeDCNo.Text = dcno;
                //sd.lblRolls.Text = delbags;
                //sd.lblCompany.Text = company;
                //sd.lblDate.Text = deldate;
                //sd.lblSupplier.Text = supplier;
                //sd.lblCounts.Text = counts;
                //sd.lblDia.Text = dia;
                //sd.lblFabric.Text = fabric;
                //sd.lblColor.Text = Fcolor;
                //sd.lblStyle.Text = style;
                //sd.lblWeight.Text = delweight;
                //sd.lblTooltip.Text = lbltip;
                //sd.lblPreparedBy.Text = preparedby;
                //sd.lblApprovedBy.Text = Approved;
                //sd.ptrApprovedBySign.Image = ApprovedSign;
                //sd.ptrPreparedBySign.Image = PreparedSign;
                //sd.ShowPreview();

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void Printingdc(string dt)
        {

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT    nexusGarmrnts_Master_Counts.Counts,nexusGarments_Master_YarnSupplier.Supplier, nexusGarments_Printingprogram.dia, nexusGarments_Printingprocess.deldate, nexusGarments_Printingprocess.id, nexusGarments_Printingprocess.dcno,  nexusGarments_PrintingCompany.Company, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_Style.ProcessStyle,  nexusGarments_Printingprocess.delWeight, nexusGarments_Printingprocess.delrolls, nexusGarments_PrintingColor.ColorName,(SELECT Empname FROM nexusGarments_Master_Employee where id=(select preparedby from nexusGarments_Printingprocess where dcno=" + dt + ")) as preparedby,(SELECT EmpSignature FROM nexusGarments_Master_Employee where id=(select preparedby from nexusGarments_Printingprocess where dcno=" + dt + ")) as preparedSignature,(SELECT Empname FROM nexusGarments_Master_Employee where id=(select ApprovedBy from nexusGarments_Printingprocess where dcno=" + dt + ")) as approvedby,(SELECT EmpSignature FROM nexusGarments_Master_Employee where id=(select approvedby from nexusGarments_Printingprocess where dcno=" + dt + ")) as approvedSignature FROM         nexusGarments_Printingprocess INNER JOIN nexusGarments_Printingprogram ON nexusGarments_Printingprocess.mapid = nexusGarments_Printingprogram.id INNER JOIN nexusGarments_PrintingCompany ON nexusGarments_Printingprogram.printingComp = nexusGarments_PrintingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_Printingprogram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_Master_Style ON nexusGarments_Printingprogram.styleid = nexusGarments_Master_Style.id INNER JOIN nexusGarments_ProcessWeight ON nexusGarments_Printingprogram.Ksource = nexusGarments_ProcessWeight.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_ProcessWeight.id = nexusGarments_KnittingProgram.Ksource INNER JOIN nexusGarments_YarnSource ON nexusGarments_KnittingProgram.poid = nexusGarments_YarnSource.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnSource.id = nexusGarments_YarnPurchase.Ysource INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarmrnts_Master_Counts ON nexusGarments_YarnPurchase.CountsId = nexusGarmrnts_Master_Counts.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_Printingprogram.Colorid = nexusGarments_PrintingColor.id where nexusGarments_printingProcess.dcno=" + dt + " ORDER BY nexusGarments_Master_YarnSupplier.Supplier DESC";
            try
            {

                string Fcolor = "";
                supplier = ""; string suppt = "";
                counts = ""; string countst = "";
               // Report.DyingDC sd = new NaveenaAccounts.Report.DyingDC();
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    if (suppt != m_dataReader["supplier"].ToString().Trim())
                    {
                        suppt = m_dataReader["supplier"].ToString().Trim();
                        supplier = supplier + "," + m_dataReader["supplier"].ToString().Trim();
                    }
                    if (countst != m_dataReader["counts"].ToString().Trim())
                    {
                        countst = m_dataReader["counts"].ToString().Trim();
                        counts = counts + "," + m_dataReader["counts"].ToString().Trim();
                    }
                    delbags = m_dataReader["delrolls"].ToString().Trim();
                    delweight = m_dataReader["delweight"].ToString().Trim();
                    dia = m_dataReader["dia"].ToString().Trim();
                    Fcolor = m_dataReader["Colorname"].ToString().Trim();
                    company = m_dataReader["Company"].ToString().Trim();
                    deldate = m_dataReader["deldate"].ToString().Trim();
                    style = m_dataReader["processstyle"].ToString().Trim();
                    fabric = m_dataReader["fabricname"].ToString().Trim();
                    dcno = m_dataReader["dcno"].ToString().Trim();
                    preparedby = m_dataReader["preparedby"].ToString().Trim();
                    Approved = m_dataReader["approvedby"].ToString().Trim();

                    byte[] Presign = (byte[])m_dataReader["preparedSignature"];
                    byte[] appSign = (byte[])m_dataReader["approvedSignature"];

                    //Initialize image variable

                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(Presign, 0, Presign.Length))
                    {
                        ms.Write(Presign, 0, Presign.Length);

                        //Set image variable value using memory stream.
                        PreparedSign = Image.FromStream(ms, true);
                    }


                    using (MemoryStream ms = new MemoryStream(appSign, 0, appSign.Length))
                    {
                        ms.Write(appSign, 0, appSign.Length);

                        //Set image variable value using memory stream.
                        ApprovedSign = Image.FromStream(ms, true);
                    }
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();


                }
                //sd.barcodeDCNo.Text = dcno;
                //sd.lblPRocess.Text = "Printing Delivery";
                //sd.lblRolls.Text = delbags;
                //sd.lblCompany.Text = company;
                //sd.lblSupplier.Text = supplier;
                //sd.lblCounts.Text = counts;
                //sd.lblDia.Text = dia;
                //sd.lblDate.Text = deldate;
                //sd.lblFabric.Text = fabric;
                //sd.lblColor.Text = Fcolor;
                //sd.lblStyle.Text = style;
                //sd.lblWeight.Text = delweight;
                //sd.lblPreparedBy.Text = preparedby;
                //sd.lblApprovedBy.Text = Approved;
                //sd.ptrApprovedBySign.Image = ApprovedSign;
                //sd.ptrPreparedBySign.Image = PreparedSign;
                //sd.ShowPreview();

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadInvoicePicture(string p, DevExpress.XtraEditors.PictureEdit pictureEdit1)
        {
           

            m_dataReader = null;
            m_odbcCommand.CommandText = "select img from nexusGarments_YarnInwardLog where id=" + p;
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    byte[] imageData = (byte[])m_dataReader["img"];

                    //Initialize image variable
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    pictureEdit1.Image = newImage;

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadHeatPicture(string p, DevExpress.XtraEditors.PictureEdit pictureEdit1)
        {


            m_dataReader = null;
            m_odbcCommand.CommandText = "select img from nexusGarments_HeatSettingLog where id=" + p;
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    byte[] imageData = (byte[])m_dataReader["img"];

                    //Initialize image variable
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    pictureEdit1.Image = newImage;

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadWashingPicture(string p, DevExpress.XtraEditors.PictureEdit pictureEdit1)
        {


            m_dataReader = null;
            m_odbcCommand.CommandText = "select img from nexusGarments_washingLog where id=" + p;
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    byte[] imageData = (byte[])m_dataReader["img"];

                    //Initialize image variable
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    pictureEdit1.Image = newImage;

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadPrintPicture(string p, DevExpress.XtraEditors.PictureEdit pictureEdit1)
        {


            m_dataReader = null;
            m_odbcCommand.CommandText = "select img from nexusGarments_PrintingLog where id=" + p;
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    byte[] imageData = (byte[])m_dataReader["img"];

                    //Initialize image variable
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    pictureEdit1.Image = newImage;

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }




        internal void LoadDyePicture(string p, DevExpress.XtraEditors.PictureEdit pictureEdit1)
        {


            m_dataReader = null;
            m_odbcCommand.CommandText = "select img from nexusGarments_DyingLog where id=" + p;
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    byte[] imageData = (byte[])m_dataReader["img"];

                    //Initialize image variable
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    pictureEdit1.Image = newImage;

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void LoadYarnInvoicePicture(string p, DevExpress.XtraEditors.PictureEdit pictureEdit1)
        {


            m_dataReader = null;
            m_odbcCommand.CommandText = "select img from nexusGarments_YarnInward where id=" + p;
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    byte[] imageData = (byte[])m_dataReader["img"];

                    //Initialize image variable
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    pictureEdit1.Image = newImage;

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }




        internal void PrintKnittDC()
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "select id from nexusGarments_KnittingProcess where isApproved='True' and isprinted='False'";
            try
            {

                //string id = "0";
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {
                    
                    knittingdc(m_dataReader1["id"].ToString().Trim());
                   
                    UpdateQuery("update nexusGarments_KnittingProcess  set isprinted='True' where id="+m_dataReader1["id"].ToString().Trim());
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }  
        }

        internal void printHeatSettingDC()
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "select id from nexusGarments_HeatSettingProcess where isApproved='True' and isprinted='False'";
            try
            {

                //string id = "0";
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {
                  
                    //(m_dataReader1["id"].ToString().Trim());

                    UpdateQuery("update nexusGarments_HeatSettingProcess  set isprinted='True' where id=" + m_dataReader1["id"].ToString().Trim());
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }  
        }

        internal void printDyingDC()
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "select dcno from nexusGarments_DyingProcess where isApproved='True' and isprinted='False'";
            try
            {

                //string id = "0";
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {
                    
                    Dyingdc(m_dataReader1["dcno"].ToString().Trim());

                    UpdateQuery("update nexusGarments_DyingProcess  set isprinted='True' where dcno=" + m_dataReader1["dcno"].ToString().Trim());
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }  
        }

        internal void printPrintingingDC()
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "select dcno from nexusGarments_PrintingProcess where isApproved='True' and isprinted='False'";
            try
            {

                //string id = "0";
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {

                    Printingdc(m_dataReader1["dcno"].ToString().Trim());

                    UpdateQuery("update nexusGarments_PrintingProcess  set isprinted='True' where dcno=" + m_dataReader1["dcno"].ToString().Trim());
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }  
        }

        internal void printWashingDC()
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "select dcno from nexusGarments_washingProcess where isApproved='True' and isprinted='False'";
            try
            {
                
                //string id = "0";
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {

                    washingdc(m_dataReader1["dcno"].ToString().Trim());

                    UpdateQuery("update nexusGarments_washingProcess  set isprinted='True' where dcno=" + m_dataReader1["dcno"].ToString().Trim());
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }  
        }

        internal void LoadPOInvoicePicture(string p, string p2, DevExpress.XtraEditors.PictureEdit pictureEdit1)
        {
            string[] sd = p.Split('-');
            sd[0] = sd[0].Trim();

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_StoreInwardLog.img    from     nexusGarments_StoreInwardLog INNER JOIN nexusGarments_storePO ON nexusGarments_StoreInwardLog.processid = nexusGarments_storePO.id  where nexusGarments_StoreInwardLog.invoiceno='" + p2 + "' and nexusGarments_storePO.ponumber='" + p + "'";
            try
            {

                string id = "0";
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    byte[] imageData = (byte[])m_dataReader["img"];

                    //Initialize image variable
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    pictureEdit1.Image = newImage;

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }


        internal void ApproveStorePo(string p, string p_2)
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "SELECT     nexusGarments_StoreInwardLog.id  FROM         nexusGarments_StorePO INNER JOIN nexusGarments_StoreInwardLog ON nexusGarments_StorePO.id = nexusGarments_StoreInwardLog.ProcessID where nexusGarments_StoreInwardLog.invoiceno='" + p_2 + "' and nexusGarments_StorePO.ponumber=" + p + " ";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    if (UpdateQuery("update nexusGarments_StorePO set recquantity = recquantity + (select inwardQuantity from nexusGarments_StoreInwardLog where id=" + m_dataReader1["id"].ToString() + ") where id=(select ProcessID from nexusGarments_StoreInwardLog where id=" + m_dataReader1["id"].ToString() + ");Update nexusGarments_PattenResource set recQuantity=recQuantity+(select inwardQuantity from nexusGarments_StoreInwardLog where id=" + m_dataReader1["id"].ToString() + "),recPic=recPic+(select (select inwardQuantity from nexusGarments_StoreInwardLog where id=" + m_dataReader1["id"].ToString() + ") * (SELECT     nexusGarments_Master_Accessories.PPU FROM nexusGarments_PattenResource INNER JOIN nexusGarments_Master_Accessories ON nexusGarments_PattenResource.accid = nexusGarments_Master_Accessories.id where nexusGarments_PattenResource.id=(select mapid from nexusGarments_StorePO where id=(select processid from nexusGarments_StoreInwardLog where id=" + m_dataReader1["id"].ToString() + ") ) )) where id=(select mapid from nexusGarments_StorePO where id=(select processid from nexusGarments_StoreInwardLog where id=" + m_dataReader1["id"].ToString() + ") )"))
                    {
                        if (UpdateQuery("update nexusgarments_storeinwardlog set isapproved='true' where id=" + m_dataReader1["id"].ToString()))
                        {
                            //if("dfd"=="dfd")
                            //{}
                        }
                    }
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }


        internal void RejectStorePo(string p, string p_2)
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "SELECT     nexusGarments_StoreInwardLog.id  FROM         nexusGarments_StorePO INNER JOIN nexusGarments_StoreInwardLog ON nexusGarments_StorePO.id = nexusGarments_StoreInwardLog.ProcessID where nexusGarments_StoreInwardLog.invoiceno='" + p_2 + "' and nexusGarments_StorePO.ponumber=" + p + " ";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    UpdateQuery("delete from nexusGarments_StoreInwardLog where id=" + m_dataReader1["make"].ToString());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }



        internal void LoadStorePoApproval(DataSet ds)
        {
             
            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT DISTINCT nexusGarments_StoreInwardLog.isapproved as [Select],nexusGarments_StorePO.ponumber as [PO Number], nexusGarments_Master_StoreSupplier.Supplier as [Supplier], nexusGarments_StoreInwardLog.invoiceno as [Invoice Number] FROM         nexusGarments_StoreInwardLog INNER JOIN nexusGarments_StorePO ON nexusGarments_StoreInwardLog.ProcessID = nexusGarments_StorePO.id INNER JOIN nexusGarments_Master_StoreSupplier ON nexusGarments_StorePO.supplier = nexusGarments_Master_StoreSupplier.id WHERE     (nexusGarments_StoreInwardLog.IsApproved = 'False')";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, connString);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT     nexusGarments_Master_Accessories.accName as [Accessories], nexusGarments_Master_AccSize.size as Size, nexusGarments_PrintingColor.ColorName as Color,  nexusGarments_StoreInwardLog.inwardQuantity as [Inward Quantity], nexusGarments_StorePO.ponumber as [PO Number], nexusGarments_StoreInwardLog.invoiceno as [Invoice Number] FROM nexusGarments_StorePO INNER JOIN nexusGarments_StoreInwardLog ON nexusGarments_StorePO.id = nexusGarments_StoreInwardLog.ProcessID INNER JOIN nexusGarments_PattenResource ON nexusGarments_StorePO.mapid = nexusGarments_PattenResource.id INNER JOIN nexusGarments_Master_Accessories ON nexusGarments_PattenResource.accid = nexusGarments_Master_Accessories.id INNER JOIN nexusGarments_Master_AccSize ON nexusGarments_PattenResource.sizeid = nexusGarments_Master_AccSize.sizeid INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PattenResource.colorid = nexusGarments_PrintingColor.id where nexusGarments_StoreInwardLog.isapproved='False'", connString);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_StoreInwardLog");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_StorePO");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[2];

                DataColumn[] da2 = new DataColumn[2];
               
                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"];
                da1[1] = ds.Tables["nexusGarments_StoreInwardLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_StorePO"].Columns["PO Number"];
                da2[1] = ds.Tables["nexusGarments_StorePO"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2); 

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }
       
        }




        internal void LoadYarnDyingApproval(DataSet ds)
        {

            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT    DISTINCT nexusGarments_YarnDyingLog.isapproved as [Select],nexusGarments_YarnDyingProcess.dcno as [DC Number], nexusGarments_YarnDyingLog.invoiceno as [Invoice Number], nexusGarments_YarnDyingCompany.Company as [Dying Company] FROM         nexusGarments_YarnDyingLog INNER JOIN nexusGarments_YarnDyingProcess ON nexusGarments_YarnDyingLog.dcid = nexusGarments_YarnDyingProcess.id INNER JOIN nexusGarments_YarnDyingProgram ON nexusGarments_YarnDyingProcess.mapid = nexusGarments_YarnDyingProgram.id INNER JOIN nexusGarments_YarnDyingCompany ON nexusGarments_YarnDyingProgram.companyid = nexusGarments_YarnDyingCompany.id where nexusGarments_YarnDyingLog.isapproved='False'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT     nexusGarments_YarnDyingProcess.dcno as [DC Number], nexusGarments_YarnDyingLog.invoiceno as [Invoice Number], nexusGarments_Master_YarnType.yarnType as [Yarn Type],  nexusGarments_PrintingColor.ColorName as [Color], nexusGarments_Master_Style.ProcessStyle as [Style], nexusGarments_YarnDyingCompany.Company as [Company],  nexusGarments_YarnDyingLog.recrolls as [Received Bags], nexusGarments_YarnDyingLog.recweight as [Received Weight] FROM        nexusGarments_YarnDyingProcess  INNER JOIN nexusGarments_YarnDyingLog ON nexusGarments_YarnDyingLog.dcid = nexusGarments_YarnDyingProcess.id INNER JOIN nexusGarments_YarnDyingProgram ON nexusGarments_YarnDyingProcess.mapid = nexusGarments_YarnDyingProgram.id INNER JOIN nexusGarments_YarnDyingCompany ON nexusGarments_YarnDyingProgram.companyid = nexusGarments_YarnDyingCompany.id INNER JOIN nexusGarments_PrintingColor ON nexusGarments_YarnDyingProgram.colorid = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_YarnDyingProgram.mapid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_YarnType ON nexusGarments_YarnPurchase.typeid = nexusGarments_Master_YarnType.id INNER JOIN nexusGarmants_Master_Process ON nexusGarments_YarnPurchase.ProcessId = nexusGarmants_Master_Process.id INNER JOIN nexusGarments_Master_Style ON nexusGarments_YarnPurchase.StyleId = nexusGarments_Master_Style.id AND  nexusGarmants_Master_Process.StyleID = nexusGarments_Master_Style.id WHERE     (nexusGarments_YarnDyingLog.isApproved = 'False')", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_YarnDyingLog");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_YarnDyingProcess");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[2];

                DataColumn[] da2 = new DataColumn[2];

                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_YarnDyingLog"].Columns["DC Number"];
                da1[1] = ds.Tables["nexusGarments_YarnDyingLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_YarnDyingProcess"].Columns["DC Number"];
                da2[1] = ds.Tables["nexusGarments_YarnDyingProcess"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }

        }




        internal void ApproveYarnDyingDC(string dcNumber, string invoicenumber)
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = " SELECT     nexusGarments_YarnDyingLog.id, nexusGarments_YarnDyingLog.recrolls, nexusGarments_YarnDyingLog.recweight,  nexusGarments_YarnDyingProgram.id as mapid FROM         nexusGarments_YarnDyingLog INNER JOIN nexusGarments_YarnDyingProcess ON nexusGarments_YarnDyingLog.dcid = nexusGarments_YarnDyingProcess.id INNER JOIN nexusGarments_YarnDyingProgram ON nexusGarments_YarnDyingProcess.mapid = nexusGarments_YarnDyingProgram.id where nexusGarments_YarnDyingProcess.dcno = '" + dcNumber + "' and nexusGarments_YarnDyingLog.invoiceno='" + invoicenumber + "'";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {
                    

                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("Update nexusGarments_YarnDyingProcess set recWeight=recWeight+'{0}', recRolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["id"].ToString());
                    if (UpdateQuery(m_sbQueryText.ToString()))
                    {
                        m_sbQueryText.Length = 0;
                        m_sbQueryText.AppendFormat("Update nexusGarments_YarnDyingProgram set recWeight=recWeight+'{0}', recbags=recbags+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                        if (UpdateQuery(m_sbQueryText.ToString()))
                        {
                            m_sbQueryText.Length = 0;
                            m_sbQueryText.AppendFormat("Update nexusGarments_YarnDyingLog set  isapproved='True',approvedby='{1}' where id='{0}'", m_dataReader1["id"].ToString(), Sess.id.ToString());
                            if (UpdateQuery(m_sbQueryText.ToString()))
                            {

                            }
                            m_sbQueryText.Length = 0;
                            m_sbQueryText.AppendFormat("Update nexusGarments_YarnSource set recweight=recweight+{0},recbags=recbags+{2} where id= (select Ysource from nexusGarments_YarnDyingProgram where id='{1}')", m_dataReader1["recweight"].ToString(), m_dataReader1["mapid"].ToString(), m_dataReader1["recrolls"].ToString());
                            if (UpdateQuery(m_sbQueryText.ToString()))
                            {

                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }

        internal void LoadKnittingApproval(DataSet ds)
        {

            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT    DISTINCT nexusGarments_YarnInwardLog.isapproved as [Select],nexusGarments_KnittingProcess.DCNo as [DC Number], nexusGarments_YarnInwardLog.invoiceno as [Invoice Number], nexusGarments_Master_KnittingCompany.Company as [Knitting Company] FROM         nexusGarments_YarnInwardLog INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_YarnInwardLog.dcid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id where nexusGarments_YarnInwardLog.isapproved='False'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT  nexusGarments_KnittingProcess.DCNo AS [DC Number], nexusGarments_YarnInwardLog.invoiceno AS [Invoice Number], nexusGarments_KnittingProgram.dia as [Dia],  nexusGarments_KnittingProgram.gsm as GSM, nexusGarments_KnittingProgram.gg as GG, nexusGarments_KnittingProgram.ll as LL, nexusGarments_YarnInwardLog.recrolls Rolls,  nexusGarments_YarnInwardLog.recweight as Weight, nexusGarments_YarnInwardLog.recyarn as Yarn FROM    nexusGarments_KnittingProcess      INNER JOIN nexusGarments_YarnInwardLog ON nexusGarments_YarnInwardLog.dcid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id WHERE     (nexusGarments_YarnInwardLog.isApproved = 'False')", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_YarnInwardLog");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_KnittingProcess");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[2];

                DataColumn[] da2 = new DataColumn[2];

                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_YarnInwardLog"].Columns["DC Number"];
                da1[1] = ds.Tables["nexusGarments_YarnInwardLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_KnittingProcess"].Columns["DC Number"];
                da2[1] = ds.Tables["nexusGarments_KnittingProcess"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }

        }


        internal void KnittingApproval(string DCNO, string Invoiceno)
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "SELECT     nexusGarments_YarnInwardLog.id, nexusGarments_KnittingProcess.id AS kpsid, nexusGarments_KnittingProgram.id AS kpid,  nexusGarments_YarnInwardLog.recrolls, nexusGarments_YarnInwardLog.recweight, nexusGarments_YarnInwardLog.recyarn FROM         nexusGarments_YarnInwardLog INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_YarnInwardLog.dcid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id WHERE     (nexusGarments_KnittingProcess.DCNo = '" + DCNO + "') AND (nexusGarments_YarnInwardLog.invoiceno = '" + Invoiceno + "')";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {


                    #region old


                    

                            //select id from nexusGarments_knittingprocess where recweight+0 > delweight and id =106

                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("Update nexusGarments_KnittingProcess set recWeight=recWeight+'{0}', recrolls=recrolls+'{1}',recyarn=recyarn+'{2}' where id='{3}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["recyarn"].ToString(), m_dataReader1["kpsid"].ToString());
                    if (UpdateQuery(m_sbQueryText.ToString()))
                    {

                        m_sbQueryText.Length = 0;
                        m_sbQueryText.AppendFormat("Update nexusGarments_KnittingProgram set recWeight=recWeight+'{0}', recrolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["kpid"].ToString());
                        if (UpdateQuery(m_sbQueryText.ToString()))
                        {

                            m_sbQueryText.Length = 0;
                            m_sbQueryText.AppendFormat("Update nexusGarments_processweight set recWeight=recWeight+'{0}', recrolls=recrolls+'{1}' where id=(select ksource from nexusGarments_KnittingProgram where id='{2}')", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["kpid"].ToString());
                            if (UpdateQuery(m_sbQueryText.ToString()))
                            {

                                m_sbQueryText.Length = 0;
                                m_sbQueryText.AppendFormat("Update nexusGarments_YarnInwardLog set  isapproved='True',approvedby='{1}' where id='{0}'", m_dataReader1["id"].ToString(), Sess.id.ToString());
                                if (UpdateQuery(m_sbQueryText.ToString()))
                                {

                                }
                            }
                        }
                    }
                       
                    




                    #endregion 
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }



        internal void LoadHeatSettingApproval(DataSet ds)
        {

            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT  DISTINCT  nexusGarments_HeatSettingLog.isapproved as [Select], nexusGarments_Master_HeatSettingCompany.Company as [Company], nexusGarments_HeatSettingProcess.dcno as [DC Number], nexusGarments_HeatSettingLog.invoiceno as [Invoice Number] FROM         nexusGarments_HeatSettingLog INNER JOIN nexusGarments_HeatSettingProcess ON nexusGarments_HeatSettingLog.dcid = nexusGarments_HeatSettingProcess.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id INNER JOIN nexusGarments_Master_HeatSettingCompany ON nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id where  nexusGarments_HeatSettingLog.isapproved = 'False'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT  nexusGarments_HeatSettingProcess.dcno as [DC Number], nexusGarments_HeatSettingLog.invoiceno as [Invoice Number],  nexusGarments_Master_fabric.fabricName as [Fabric],  nexusGarments_HeatSettingProgram.HGSM, nexusGarments_HeatSettingProgram.FGSM, nexusGarments_HeatSettingProgram.dia as Dia,nexusGarments_HeatSettingLog.recrolls as [Rec Rolls], nexusGarments_HeatSettingLog.recweight as [Rec Weight] FROM        nexusGarments_HeatSettingProcess  INNER JOIN  nexusGarments_HeatSettingLog ON nexusGarments_HeatSettingLog.dcid = nexusGarments_HeatSettingProcess.id INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_HeatSettingProgram.fabricid = nexusGarments_Master_fabric.fabricId WHERE     (nexusGarments_HeatSettingLog.isApproved = 'False')", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_HeatSettingLog");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_HeatSettingProcess");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[2];

                DataColumn[] da2 = new DataColumn[2];

                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_HeatSettingLog"].Columns["DC Number"];
                da1[1] = ds.Tables["nexusGarments_HeatSettingLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_HeatSettingProcess"].Columns["DC Number"];
                da2[1] = ds.Tables["nexusGarments_HeatSettingProcess"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }

        }



        internal void HeatSettingApproval(string DCNO, string Invoiceno)
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "SELECT     nexusGarments_HeatSettingLog.id, nexusGarments_HeatSettingLog.dcid, nexusGarments_HeatSettingProcess.mapid, nexusGarments_HeatSettingLog.recrolls,  nexusGarments_HeatSettingLog.recweight FROM         nexusGarments_HeatSettingLog INNER JOIN nexusGarments_HeatSettingProcess ON nexusGarments_HeatSettingLog.dcid = nexusGarments_HeatSettingProcess.id  where nexusGarments_HeatSettingLog.invoiceno='"+Invoiceno+"' and nexusGarments_HeatSettingProcess.dcno='"+DCNO+"'";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {


                    #region old




                    //select id from nexusGarments_knittingprocess where recweight+0 > delweight and id =106

                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("Update nexusGarments_HeatSettingProcess set recWeight=recWeight+'{0}', recRolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["dcid"].ToString());
                    if (UpdateQuery(m_sbQueryText.ToString()))
                    {

                        m_sbQueryText.Length = 0;
                        m_sbQueryText.AppendFormat("Update nexusGarments_HeatSettingProgram set recWeight=recWeight+'{0}', recrolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                        if (UpdateQuery(m_sbQueryText.ToString()))
                        {

                            m_sbQueryText.Length = 0;
                            m_sbQueryText.AppendFormat("Update nexusGarments_processweight set HeatRecWeight=HeatRecWeight+'{0}', HeatRecRolls=HeatRecRolls+'{1}' where id=(select ksource from nexusGarments_HeatSettingProgram where id='{2}')", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                            if (UpdateQuery(m_sbQueryText.ToString()))
                            {
                                m_sbQueryText.Length = 0;
                                m_sbQueryText.AppendFormat("Update nexusGarments_HeatSettingLog set  isapproved='True',approvedby='{1}' where id='{0}'", m_dataReader1["id"].ToString(), Sess.id.ToString());
                                if (UpdateQuery(m_sbQueryText.ToString()))
                                {
                                }
                            }

                        }
                    }





                    #endregion
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }

        internal void LoadDyingApproval(DataSet ds)
        {

            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT    DISTINCT nexusGarments_DyingLog.isapproved as [Select], nexusGarments_DyingProcess.dcno as [DC Number], nexusGarments_DyingLog.invoiceno as [Invoice Number], nexusGarmrnts_Master_DyingCompany.Company as [Dying Company],  nexusGarments_Master_CompactingCompany.Company AS [Compacting Company], nexusGarments_Master_CompactingMethod.CompMethod as [Compacting Method] FROM         nexusGarments_DyingLog INNER JOIN nexusGarments_DyingProcess ON nexusGarments_DyingLog.dcid = nexusGarments_DyingProcess.id INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id INNER JOIN nexusGarmrnts_Master_DyingCompany ON nexusGarments_DyingProgram.dyingComp = nexusGarmrnts_Master_DyingCompany.id INNER JOIN nexusGarments_Master_CompactingCompany ON nexusGarments_DyingProgram.ComptCompany = nexusGarments_Master_CompactingCompany.id INNER JOIN nexusGarments_Master_CompactingMethod ON nexusGarments_DyingProgram.ComptMethod = nexusGarments_Master_CompactingMethod.id where nexusGarments_DyingLog.isapproved='False'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT     nexusGarments_DyingProcess.dcno as [DC Number], nexusGarments_DyingLog.invoiceno as [Invoice Number], nexusGarments_Master_fabric.fabricName as [Fabric], nexusGarments_PrintingColor.ColorName as [Color],  nexusGarments_DyingProgram.dia as [Dia],  nexusGarments_DyingLog.recrolls as [Rec Rolls], nexusGarments_DyingLog.recweight as [Rec Weight] FROM         nexusGarments_DyingProcess INNER JOIN nexusGarments_DyingLog ON nexusGarments_DyingProcess.id = nexusGarments_DyingLog.dcid INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_DyingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id where nexusGarments_DyingLog.isapproved='False'", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_DyingLog");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_DyingProcess");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[2];

                DataColumn[] da2 = new DataColumn[2];

                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_DyingLog"].Columns["DC Number"];
                da1[1] = ds.Tables["nexusGarments_DyingLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_DyingProcess"].Columns["DC Number"];
                da2[1] = ds.Tables["nexusGarments_DyingProcess"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }

        }

        internal void LoadDyingApproval(string DCNO, string Invoiceno)
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "SELECT     nexusGarments_DyingLog.id, nexusGarments_DyingLog.dcid, nexusGarments_DyingProcess.mapid, nexusGarments_DyingLog.recrolls,  nexusGarments_DyingLog.recweight FROM         nexusGarments_DyingLog INNER JOIN nexusGarments_DyingProcess ON nexusGarments_DyingLog.dcid = nexusGarments_DyingProcess.id   where nexusGarments_DyingLog.invoiceno='" + Invoiceno + "' and nexusGarments_DyingProcess.dcno='" + DCNO + "'";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {

                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("Update nexusGarments_DyingProcess set recWeight=recWeight+'{0}', recRolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["dcid"].ToString());
                    if (UpdateQuery(m_sbQueryText.ToString()))
                    {

                        m_sbQueryText.Length = 0;
                        m_sbQueryText.AppendFormat("Update nexusGarments_DyingProgram set recWeight=recWeight+'{0}', recrolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                        if (UpdateQuery(m_sbQueryText.ToString()))
                        {
                            m_sbQueryText.Length = 0;
                            m_sbQueryText.AppendFormat("Update nexusGarments_processweight set DyeRecWeight=DyeRecWeight+'{0}', DyeRecRolls=DyeRecRolls+'{1}' where id=(select ksource from nexusGarments_DyingProgram where id='{2}')", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                            if (UpdateQuery(m_sbQueryText.ToString()))
                            {
                                m_sbQueryText.Length = 0;
                                m_sbQueryText.AppendFormat("Update nexusGarments_DyingLog set  isapproved='True',approvedby='{1}' where id='{0}'", m_dataReader1["id"].ToString(), Sess.id.ToString());
                                if (UpdateQuery(m_sbQueryText.ToString()))
                                {

                                }
                            }
                        }
                    }
                    
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }

        internal void LoadPrintingApproval(DataSet ds)
        {

            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT  DISTINCT nexusGarments_PrintingLog.isapproved as [Select] ,  nexusGarments_PrintingProcess.dcno as [DC Number], nexusGarments_PrintingLog.invoiceno as [Invoice Number], nexusGarments_PrintingCompany.Company FROM         nexusGarments_PrintingLog INNER JOIN nexusGarments_PrintingProcess ON nexusGarments_PrintingLog.dcid = nexusGarments_PrintingProcess.id INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id INNER JOIN nexusGarments_PrintingCompany ON nexusGarments_PrintingProgram.PrintingComp = nexusGarments_PrintingCompany.id where nexusGarments_PrintingLog.isapproved='False'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT  nexusGarments_PrintingProcess.dcno AS [DC Number], nexusGarments_PrintingLog.invoiceno AS [Invoice Number], nexusGarments_Master_fabric.fabricName as Fabric,  nexusGarments_PrintingColor.ColorName as Color, nexusGarments_PrintingDesign.DesignName as Design, nexusGarments_PrintingLog.recrolls as [Received Rolls],  nexusGarments_PrintingLog.recweight as [Received Weight] FROM   nexusGarments_PrintingProcess INNER JOIN nexusGarments_PrintingLog ON nexusGarments_PrintingLog.dcid = nexusGarments_PrintingProcess.id INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.FabricID = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id WHERE     (nexusGarments_PrintingLog.isApproved = 'False')", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_PrintingLog");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_PrintingProcess");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[2];

                DataColumn[] da2 = new DataColumn[2];

                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_PrintingLog"].Columns["DC Number"];
                da1[1] = ds.Tables["nexusGarments_PrintingLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_PrintingProcess"].Columns["DC Number"];
                da2[1] = ds.Tables["nexusGarments_PrintingProcess"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }

        }

        internal void LoadPrintingApproval(string DCNO, string Invoiceno)
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "SELECT     nexusGarments_PrintingLog.id, nexusGarments_PrintingLog.dcid, nexusGarments_PrintingProcess.mapid, nexusGarments_PrintingLog.recrolls,  nexusGarments_PrintingLog.recweight FROM         nexusGarments_PrintingLog INNER JOIN nexusGarments_PrintingProcess ON nexusGarments_PrintingLog.dcid = nexusGarments_PrintingProcess.id    where nexusGarments_PrintingLog.invoiceno='" + Invoiceno + "' and nexusGarments_PrintingProcess.dcno='" + DCNO + "'";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {

                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("Update nexusGarments_PrintingProcess set recWeight=recWeight+'{0}', recRolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["dcid"].ToString());
                    if (UpdateQuery(m_sbQueryText.ToString()))
                    {

                        m_sbQueryText.Length = 0;
                        m_sbQueryText.AppendFormat("Update nexusGarments_PrintingProgram set recWeight=recWeight+'{0}', recrolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                        if (UpdateQuery(m_sbQueryText.ToString()))
                        {
                            m_sbQueryText.Length = 0;
                            m_sbQueryText.AppendFormat("Update nexusGarments_processweight set PrintRecWeight=PrintRecWeight+'{0}', PrintRecRolls=PrintRecRolls+'{1}' where id=(select ksource from nexusGarments_PrintingProgram where id='{2}')", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                            if (UpdateQuery(m_sbQueryText.ToString()))
                            {
                                m_sbQueryText.Length = 0;
                                m_sbQueryText.AppendFormat("Update nexusGarments_PrintingLog set  isapproved='True',approvedby='{1}' where id='{0}'", m_dataReader1["id"].ToString(), Sess.id.ToString());
                                if (UpdateQuery(m_sbQueryText.ToString()))
                                {

                                }
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }





        internal void LoadWashingApproval(DataSet ds)
        {

            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT  DISTINCT nexusGarments_WashingLog.isapproved as [Select] ,  nexusGarments_WashingProcess.dcno as [DC Number], nexusGarments_WashingLog.invoiceno as [Invoice Number], nexusGarments_KWashingCompany.Company FROM         nexusGarments_WashingLog INNER JOIN nexusGarments_WashingProcess ON nexusGarments_WashingLog.dcid = nexusGarments_WashingProcess.id INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_KWashingCompany ON nexusGarments_WashingProgram.dyingComp = nexusGarments_KWashingCompany.id where nexusGarments_WashingLog.isapproved='False'";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT  nexusGarments_WashingProcess.dcno AS [DC Number], nexusGarments_WashingLog.invoiceno AS [Invoice Number], nexusGarments_Master_fabric.fabricName as Fabric,  nexusGarments_printingColor.ColorName as Color, nexusGarments_WashingLog.recrolls as [Received Rolls],  nexusGarments_WashingLog.recweight as [Received Weight] FROM   nexusGarments_WashingProcess INNER JOIN nexusGarments_WashingLog ON nexusGarments_WashingLog.dcid = nexusGarments_WashingProcess.id INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_WashingProgram.FabricID = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_printingColor ON nexusGarments_WashingProgram.ColorID = nexusGarments_printingColor.id  WHERE     (nexusGarments_WashingLog.isApproved = 'False')", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_WashingLog");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_WashingProcess");
                //oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[2];

                DataColumn[] da2 = new DataColumn[2];

                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_WashingLog"].Columns["DC Number"];
                da1[1] = ds.Tables["nexusGarments_WashingLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_WashingProcess"].Columns["DC Number"];
                da2[1] = ds.Tables["nexusGarments_WashingProcess"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {

            }

        }


        internal string AccountsRefGen()
        {
            m_dataReader = null;
            m_odbcCommand.CommandText = "select [value] from nexusGarments_Accounts_RefGen where processid='0';";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (m_dataReader.Read())
                {
                   
                    dcno = m_dataReader["value"].ToString();
                    return dcno;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
            return dcno;
        }


        internal void LoadKnittingBill(DataSet ds,string dcno)
        {

            ds.Clear();
            try
            {

                string Query0 = "SELECT DISTINCT  RTRIM(nexusGarments_KnittingProcess.DCNo) AS [DC Number],  nexusGarments_KnittingProcess.deldate AS Date, nexusGarments_Master_Style.ProcessStyle AS [Style No],   (SELECT     SUM(delweight) AS Expr1 FROM          nexusGarments_KnittingYarnDel WHERE      (dcno = nexusGarments_KnittingProcess.DCNo)) AS [Del Yarn], (SELECT     SUM(recWeight) AS Expr1 FROM          nexusGarments_KnittingProcess WHERE      (DCNo = nexusGarments_KnittingYarnDel.dcno)) AS [Rec Weight], (SELECT     SUM(nexusGarments_KnitttingYarnInward.RecWeight) AS Expr1 FROM          nexusGarments_KnitttingYarnInward INNER JOIN nexusGarments_KnittingYarnDel ON nexusGarments_KnitttingYarnInward.mapid = nexusGarments_KnittingYarnDel.id WHERE      (nexusGarments_KnittingYarnDel.dcno = nexusGarments_KnittingProcess.DCNo)) AS [Rec Yarn],0.00 as TDS,0 as [Bill Weight],0 as [Ammount],0 as [Debit Weight], 0 as [Debit Ammount]  FROM         nexusGarments_KnittingProgram INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarmants_Master_Process ON nexusGarments_KnittingProgram.ProcessId = nexusGarmants_Master_Process.id INNER JOIN nexusGarments_Master_Style ON nexusGarmants_Master_Process.StyleID = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_KnittingYarnDel ON nexusGarments_KnittingYarnDel.dcno = nexusGarments_KnittingProcess.DCNo where nexusGarments_KnittingProcess.DCNo in ("+dcno+")";
                    //"SELECT DISTINCT  nexusGarments_KnittingProcess.isclosed AS [Select], RTRIM(nexusGarments_KnittingProcess.DCNo) AS [DC Number],  nexusGarments_KnittingProcess.deldate AS Date, nexusGarments_Master_Style.ProcessStyle AS [Style No], nexusGarments_Master_KnittingCompany.Company,  '0' AS [Program Weight],  ( SELECT    sum( delweight) FROM         nexusGarments_KnittingYarnDel where dcno = nexusGarments_KnittingProcess.DCNo ) AS [Del Yarn],  (select sum(recweight) from nexusGarments_KnittingProcess where dcno = nexusGarments_KnittingYarnDel.dcno) AS [Rec Weight],  (SELECT     sum(nexusGarments_KnitttingYarnInward.RecWeight) FROM         nexusGarments_KnitttingYarnInward INNER JOIN nexusGarments_KnittingYarnDel ON nexusGarments_KnitttingYarnInward.mapid = nexusGarments_KnittingYarnDel.id  where nexusGarments_KnittingYarnDel.dcno =  nexusGarments_KnittingProcess.DCNo) AS [Rec Yarn], '0' AS Balance, 0.00 AS Prec, '' AS Status,  nexusGarments_KnittingProcess.jobworkno AS [Job Work No] FROM         nexusGarments_KnittingProgram INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarmants_Master_Process ON nexusGarments_KnittingProgram.ProcessId = nexusGarmants_Master_Process.id INNER JOIN nexusGarments_Master_Style ON nexusGarmants_Master_Process.StyleID = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id inner join nexusGarments_KnittingYarnDel on nexusGarments_KnittingYarnDel.dcno = nexusGarments_KnittingProcess.dcno";
                //"SELECT DISTINCT nexusGarments_KnittingProcess.isclosed as [Select],rtrim( nexusGarments_KnittingProcess.DCNo) as [DC Number], nexusGarments_KnittingProcess.deldate AS [Date], nexusGarments_Master_Style.ProcessStyle as [Style No],  nexusGarments_Master_KnittingCompany.Company,  '0' AS [Program Weight], 0.00 AS [Del Yarn], '0' AS [Rec Weight], '0' AS [Rec Yarn],'0' as Balance , 0.00 AS Prec,'' as [Status],nexusGarments_KnittingProcess.jobworkno as [Job Work No] FROM   nexusGarments_KnittingProgram       INNER JOIN  nexusGarments_KnittingProcess    ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarmants_Master_Process ON nexusGarments_KnittingProgram.ProcessId = nexusGarmants_Master_Process.id INNER JOIN nexusGarments_Master_Style ON nexusGarmants_Master_Process.StyleID = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id ";


                string Query1 = "SELECT DISTINCT  rtrim(nexusGarments_KnittingProcess.DCNo) as [DC Number], nexusGarments_Master_Style.ProcessStyle,  nexusGarments_Master_KnittingCompany.Company, CONVERT(varchar(12), nexusGarments_KnittingProcess.deldate, 109) AS [Delivery Date],  nexusGarments_KnittingProcess.delBags, nexusGarments_KnittingProcess.delWeight FROM         nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarmants_Master_Process ON nexusGarments_KnittingProgram.ProcessId = nexusGarmants_Master_Process.id INNER JOIN nexusGarments_Master_Style ON nexusGarmants_Master_Process.StyleID = nexusGarments_Master_Style.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id   where nexusGarments_KnittingProcess.DCNo in ("+dcno+")";


                string Query2 = "  SELECT DISTINCT  RTRIM(kp.DCNo) AS [DC Number], nexusGarments_Master_YarnSupplier.Supplier, nexusGarments_Master_YarnType.yarnType,  nexusGarments_KnittingYarnDel.delbags, CONVERT(varchar(12), nexusGarments_KnittingYarnDel.delweight, 109) AS [Del Weight], 'NULL' AS Color FROM         nexusGarments_KnittingProcess AS kp INNER JOIN nexusGarments_KnittingYarnDel ON kp.DCNo = nexusGarments_KnittingYarnDel.dcno INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingYarnDel.Yid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarments_Master_YarnType ON nexusGarments_YarnPurchase.typeid = nexusGarments_Master_YarnType.id WHERE     (nexusGarments_KnittingYarnDel.Type = 'UD')  and kp.DCNo in (" + dcno + ") UNION SELECT DISTINCT  RTRIM(nexusGarments_KnittingProcess.DCNo) AS [DC Number], kp.Supplier, nexusGarments_Master_YarnType.yarnType,  nexusGarments_KnittingYarnDel.delbags, CONVERT(varchar(12), nexusGarments_KnittingYarnDel.delweight, 109) AS [Del Weight],  nexusGarments_PrintingColor.ColorName as [Color] FROM         nexusGarments_Master_YarnSupplier as kp INNER JOIN nexusGarments_YarnPurchase ON kp.id = nexusGarments_YarnPurchase.SupplierId INNER JOIN nexusGarments_Master_YarnType ON nexusGarments_YarnPurchase.typeid = nexusGarments_Master_YarnType.id INNER JOIN nexusGarments_YarnDyingProgram ON nexusGarments_YarnPurchase.id = nexusGarments_YarnDyingProgram.mapid INNER JOIN nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingYarnDel ON nexusGarments_KnittingProcess.DCNo = nexusGarments_KnittingYarnDel.dcno ON  nexusGarments_YarnDyingProgram.id = nexusGarments_KnittingYarnDel.Yid INNER JOIN nexusGarments_PrintingColor ON nexusGarments_YarnDyingProgram.colorid = nexusGarments_PrintingColor.id WHERE     (nexusGarments_KnittingYarnDel.Type = 'D')  and nexusGarments_KnittingProcess.DCNo in (" + dcno + ")";


                string Query3 = "SELECT  DISTINCT  rtrim( nexusGarments_KnittingProcess.DCNo) as [DC Number], nexusGarments_YarnInwardLog.invoiceno, nexusGarments_Master_fabric.fabricName,  nexusGarments_YarnInwardLog.recrolls, nexusGarments_YarnInwardLog.recweight, convert (varchar(12),nexusGarments_YarnInwardLog.recdate,109) FROM         nexusGarments_YarnInwardLog INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_YarnInwardLog.dcid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId where  nexusGarments_KnittingProcess.DCNo in (" + dcno + ")";




                string Query4 = "SELECT DISTINCT   rtrim(nexusGarments_KnittingProcess.DCNo) as [DC Number], nexusGarments_KnitttingYarnInward.invoiceno, nexusGarments_Master_YarnSupplier.Supplier,  nexusGarments_Master_YarnType.yarnType, nexusGarments_KnitttingYarnInward.RecBags, nexusGarments_KnitttingYarnInward.RecWeight FROM         nexusGarments_KnitttingYarnInward INNER JOIN nexusGarments_KnittingYarnDel ON nexusGarments_KnitttingYarnInward.mapid = nexusGarments_KnittingYarnDel.id INNER JOIN nexusGarments_YarnPurchase ON nexusGarments_KnittingYarnDel.Yid = nexusGarments_YarnPurchase.id INNER JOIN nexusGarments_Master_YarnSupplier ON nexusGarments_YarnPurchase.SupplierId = nexusGarments_Master_YarnSupplier.id INNER JOIN nexusGarments_Master_YarnType ON nexusGarments_YarnPurchase.typeid = nexusGarments_Master_YarnType.id INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_KnittingYarnDel.dcno = nexusGarments_KnittingProcess.dcno  where nexusGarments_KnittingProcess.DCNo in ("+dcno+")";



                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter0 = new System.Data.OleDb.OleDbDataAdapter(Query0, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(Query1, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter(Query2, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter(Query3, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter4 = new System.Data.OleDb.OleDbDataAdapter(Query4, conns);
                oleDbDataAdapter0.Fill(ds, "nexusGarments_KnittingProgram");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_KnittingProcess");
                oleDbDataAdapter2.Fill(ds, "kp");
                oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                oleDbDataAdapter4.Fill(ds, "nexusGarments_KnitttingYarnInward");
                DataColumn[] da0 = new DataColumn[1];
                DataColumn[] da1 = new DataColumn[1];
                DataColumn[] da2 = new DataColumn[1];
                DataColumn[] da3 = new DataColumn[1];
                DataColumn[] da4 = new DataColumn[1];

                //da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da0[0] = ds.Tables["nexusGarments_KnittingProgram"].Columns["DC Number"];

                da1[0] = ds.Tables["nexusGarments_KnittingProcess"].Columns["DC Number"];
                //da1[1] = ds.Tables["nexusGarments_HeatSettingLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["kp"].Columns["DC Number"];


                da3[0] = ds.Tables["nexusGarments_YarnInwardLog"].Columns["DC Number"];
                da4[0] = ds.Tables["nexusGarments_KnitttingYarnInward"].Columns["DC Number"];
                //da2[1] = ds.Tables["nexusGarments_HeatSettingProcess"].Columns["Invoice Number"];


                ds.Relations.Add("Program", da0, da1);
                ds.Relations.Add("Yarn Delivery", da0, da2);
                ds.Relations.Add("Fabric Inward", da0, da3);
                ds.Relations.Add("Yarn Inward", da0, da4);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }

        }


        internal void LoadWashingApproval(string DCNO, string Invoiceno)
        {
            m_dataReader1 = null;
            m_odbcCommand_1.CommandText = "SELECT     nexusGarments_WashingLog.id, nexusGarments_WashingLog.dcid, nexusGarments_WashingProcess.mapid, nexusGarments_WashingLog.recrolls,  nexusGarments_WashingLog.recweight FROM         nexusGarments_WashingLog INNER JOIN nexusGarments_WashingProcess ON nexusGarments_WashingLog.dcid = nexusGarments_WashingProcess.id    where nexusGarments_WashingLog.invoiceno='" + Invoiceno + "' and nexusGarments_WashingProcess.dcno='" + DCNO + "'";
            try
            {
                OpenDB1();
                m_dataReader1 = m_odbcCommand_1.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader1.Read())
                {

                    m_sbQueryText.Length = 0;
                    m_sbQueryText.AppendFormat("Update nexusGarments_WashingProcess set recWeight=recWeight+'{0}', recRolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["dcid"].ToString());
                    if (UpdateQuery(m_sbQueryText.ToString()))
                    {

                        m_sbQueryText.Length = 0;
                        m_sbQueryText.AppendFormat("Update nexusGarments_WashingProgram set recWeight=recWeight+'{0}', recrolls=recrolls+'{1}' where id='{2}'", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                        if (UpdateQuery(m_sbQueryText.ToString()))
                        {
                            m_sbQueryText.Length = 0;
                            m_sbQueryText.AppendFormat("Update nexusGarments_processweight set WRecWeight=WRecWeight+'{0}', WRecRolls=WRecRolls+'{1}' where id=(select ksource from nexusGarments_WashingProgram where id='{2}')", m_dataReader1["recweight"].ToString(), m_dataReader1["recrolls"].ToString(), m_dataReader1["mapid"].ToString());
                            if (UpdateQuery(m_sbQueryText.ToString()))
                            {
                                m_sbQueryText.Length = 0;
                                m_sbQueryText.AppendFormat("Update nexusGarments_WashingLog set  isapproved='True',approvedby='{1}' where id='{0}'", m_dataReader1["id"].ToString(), Sess.id.ToString());
                                if (UpdateQuery(m_sbQueryText.ToString()))
                                {

                                }
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB1(1);
            }
        }

        internal void LoadBank(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Buyer");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,BankName from nexusGarments_Master_Bank";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["BankName"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadCompanyProcess(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Process");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,Process from nexusGarments_Master_CompanyProcess";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["Process"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadChequeDetails(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Bank");
            dt.Columns.Add("From");
            dt.Columns.Add("To");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     NexusGarments_Master_ChequeBook.id, nexusGarments_Master_Bank.BankName, NexusGarments_Master_ChequeBook.NoFrom,  NexusGarments_Master_ChequeBook.NoTo FROM         NexusGarments_Master_ChequeBook INNER JOIN nexusGarments_Master_Bank ON NexusGarments_Master_ChequeBook.BankID = nexusGarments_Master_Bank.id where NexusGarments_Master_ChequeBook.isclosed='False'";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["BankName"].ToString().Trim(), m_dataReader["NoFrom"].ToString().Trim(), m_dataReader["NoTo"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadKnittingBillDC(DevExpress.XtraEditors.CheckedComboBoxEdit chkDCNo, string query)
        {
            chkDCNo.Properties.Items.Clear();
            m_dataReader = null;
            m_odbcCommand.CommandText = query;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();
                    chkDCNo.Properties.Items.Add(m_dataReader["dcno"].ToString().Trim());
                   

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadRefDC(DevExpress.XtraEditors.CheckedComboBoxEdit chkDCNo, string query)
        {
            chkDCNo.Properties.Items.Clear();
            m_dataReader = null;
            m_odbcCommand.CommandText = query;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                     chkDCNo.Properties.Items.Add(m_dataReader["refno"].ToString().Trim());


                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadKnittingChequeDetails(DataTable dt,string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC No");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            //dt.Columns.Add("Gsm");
            //dt.Columns.Add("GG");
            //dt.Columns.Add("LL");

            dt.Columns.Add("Del Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Est Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("Bill Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("TDS %", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Ammount", System.Type.GetType("System.Double"));
        
            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT nexusGarments_KnittingProcess.delweight,nexusGarments_KnittingProcess.id,nexusGarments_KnittingProcess.DCNo, nexusGarments_Master_KnittingCompany.Company, nexusGarments_Master_fabric.fabricName,  nexusGarments_KnittingProgram.dia, nexusGarments_KnittingProgram.gsm, nexusGarments_KnittingProgram.gg, nexusGarments_KnittingProgram.ll,  nexusGarments_KnittingProcess.recrolls,nexusGarments_KnittingProcess.recWeight,nexusGarments_KnittingProgram.uprice as estprice FROM         nexusGarments_KnittingProcess INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id INNER JOIN nexusGarments_Master_KnittingCompany ON nexusGarments_KnittingProgram.Companyid = nexusGarments_Master_KnittingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_KnittingProgram.Fabricid = nexusGarments_Master_fabric.fabricId where dcno in(" + dcno + ")";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    //, m_dataReader["gsm"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim()
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "True", m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["delweight"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["estprice"].ToString().Trim(), "0", "0", "0", "0");

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadHeatSettingChequeDetails(DataTable dt, string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC No");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Dia");
            dt.Columns.Add("HGSM");
            dt.Columns.Add("FGSM");
            //dt.Columns.Add("Gsm");
            //dt.Columns.Add("GG");
            //dt.Columns.Add("LL");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Est Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("Bill Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("TDS %", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Ammount", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_HeatSettingProgram.dia,nexusGarments_HeatSettingProcess.id, nexusGarments_HeatSettingProcess.dcno, nexusGarments_Master_fabric.fabricName, nexusGarments_Master_HeatSettingCompany.Company, nexusGarments_HeatSettingProgram.uprice ,nexusGarments_HeatSettingProgram.HGSM, nexusGarments_HeatSettingProgram.FGSM, nexusGarments_HeatSettingProcess.delrolls,  nexusGarments_HeatSettingProcess.delWeight, nexusGarments_HeatSettingProcess.recRolls, nexusGarments_HeatSettingProcess.recWeight FROM         nexusGarments_HeatSettingProcess INNER JOIN nexusGarments_HeatSettingProgram ON nexusGarments_HeatSettingProcess.mapid = nexusGarments_HeatSettingProgram.id INNER JOIN nexusGarments_Master_HeatSettingCompany ON  nexusGarments_HeatSettingProgram.heatsettingCompany = nexusGarments_Master_HeatSettingCompany.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_HeatSettingProgram.fabricid = nexusGarments_Master_fabric.fabricId WHERE     (nexusGarments_HeatSettingProcess.isClosed = 'True')and nexusGarments_HeatSettingProcess.dcno in(" + dcno + ")";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    //, m_dataReader["gsm"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim()
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "True", m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["hgsm"].ToString().Trim(), m_dataReader["fgsm"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["uprice"].ToString().Trim(), "0", "0", "0", "0");

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadDyingChequeDetails(DataTable dt, string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC No");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Color");
            dt.Columns.Add("Dia");
           
            //dt.Columns.Add("Gsm");
            //dt.Columns.Add("GG");
            //dt.Columns.Add("LL");

            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Est Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("Bill Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("TDS %", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Ammount", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_DyingProgram.dia, nexusGarments_DyingProcess.id, nexusGarments_DyingProcess.dcno, nexusGarments_Master_fabric.fabricName,  nexusGarments_PrintingColor.ColorName, nexusGarments_DyingProcess.delrolls, nexusGarments_DyingProcess.delWeight, (select sum(recrolls) from nexusgarments_dyinglog where dcid=nexusGarments_DyingProcess.id) as recrolls,  (select sum(recweight) from nexusgarments_dyinglog where dcid=nexusGarments_DyingProcess.id) as recweight, nexusGarments_DyingProgram.UPrice FROM         nexusGarments_DyingProcess INNER JOIN nexusGarments_DyingProgram ON nexusGarments_DyingProcess.mapid = nexusGarments_DyingProgram.id INNER JOIN  nexusGarments_Master_fabric ON nexusGarments_DyingProgram.fabricid = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_PrintingColor ON nexusGarments_DyingProgram.dyingColor = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_DyingLog ON nexusGarments_DyingProcess.id = nexusGarments_DyingLog.dcid WHERE     (nexusGarments_DyingProcess.isclosed = 'True') and nexusGarments_dyingProcess.dcno in(" + dcno + ")";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    //, m_dataReader["gsm"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim()
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "True", m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["colorname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["uprice"].ToString().Trim(), "0", "0", "0", "0");

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadPrintingChequeDetails(DataTable dt, string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC No");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Color");
            dt.Columns.Add("Design");
            dt.Columns.Add("Dia");
            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Est Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("Bill Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("TDS %", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Ammount", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_PrintingProgram.dia, nexusGarments_PrintingProcess.id, nexusGarments_PrintingProcess.dcno, nexusGarments_Master_fabric.fabricName,  nexusGarments_PrintingColor.ColorName, nexusGarments_PrintingDesign.DesignName, nexusGarments_PrintingProcess.delrolls,  nexusGarments_PrintingProcess.delWeight, (select sum(printrecrolls) from nexusgarments_printinglog where dcid=nexusGarments_PrintingProcess.id) asrecRolls, (select sum(printrecweight) from nexusgarments_printinglog where dcid=nexusGarments_PrintingProcess.id) as recWeight,  nexusGarments_PrintingProgram.UPrice FROM         nexusGarments_PrintingProcess INNER JOIN nexusGarments_PrintingProgram ON nexusGarments_PrintingProcess.mapid = nexusGarments_PrintingProgram.id INNER JOIN nexusGarments_Master_fabric ON nexusGarments_PrintingProgram.FabricID = nexusGarments_Master_fabric.fabricId INNER JOIN nexusGarments_PrintingColor ON nexusGarments_PrintingProgram.ColorID = nexusGarments_PrintingColor.id INNER JOIN nexusGarments_PrintingDesign ON nexusGarments_PrintingProgram.DesignID = nexusGarments_PrintingDesign.id WHERE     (nexusGarments_PrintingProcess.isclosed = 'True') and nexusGarments_printingProcess.dcno in(" + dcno + ")";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    //, m_dataReader["gsm"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim()
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "True", m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["colorname"].ToString().Trim(), m_dataReader["DesignName"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["uprice"].ToString().Trim(), "0", "0", "0", "0");

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadWashingChequeDetails(DataTable dt, string dcno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC No");
            dt.Columns.Add("Fabric");
            dt.Columns.Add("Color");
            dt.Columns.Add("Dia");
           
            dt.Columns.Add("Rec Rolls", System.Type.GetType("System.Double"));
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Est Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("Bill Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("TDS %", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Ammount", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT     nexusGarments_WashingProgram.dia,nexusGarments_WashingProcess.id, nexusGarments_WashingProcess.dcno, nexusGarments_Master_fabric.fabricName,  nexusGarments_printingColor.ColorName ,        nexusGarments_WashingProcess.delrolls, nexusGarments_WashingProcess.delWeight, nexusGarments_WashingProcess.recRolls,  nexusGarments_WashingProcess.recWeight, nexusGarments_WashingProgram.UPrice FROM         nexusGarments_WashingProcess INNER JOIN nexusGarments_WashingProgram ON nexusGarments_WashingProcess.mapid = nexusGarments_WashingProgram.id INNER JOIN nexusGarments_Master_fabric  ON nexusGarments_WashingProgram.FabricID = nexusGarments_Master_fabric.fabricId  INNER JOIN nexusGarments_PrintingColor ON nexusGarments_WashingProgram.ColorID = nexusGarments_printingcolor.id  WHERE     (nexusGarments_washingProcess.isClosed = 'True')and nexusGarments_washingProcess.dcno in(" + dcno + ")";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    //, m_dataReader["gsm"].ToString().Trim(), m_dataReader["gg"].ToString().Trim(), m_dataReader["ll"].ToString().Trim()
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "True", m_dataReader["dcno"].ToString().Trim(), m_dataReader["fabricname"].ToString().Trim(), m_dataReader["colorname"].ToString().Trim(), m_dataReader["dia"].ToString().Trim(), m_dataReader["recrolls"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["uprice"].ToString().Trim(), "0", "0", "0", "0");

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadTaxInfo(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Tax");
            dt.Columns.Add("Precentage");
            dt.Columns.Add("Note");
           
        }




        internal void LoadTax(DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Tax");

            m_dataReader = null;
            m_odbcCommand.CommandText = "Select id,taxes from nexusGarments_Master_Tax";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["taxes"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void LoadKnittingBillingGrid(DataTable dt,string refno)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select");
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Bill No");
            dt.Columns.Add("Recived Weight");
            dt.Columns.Add("Debit Weight");
            dt.Columns.Add("Bill Price");
            dt.Columns.Add("Debit Ammount");
            dt.Columns.Add("Billing Ammount");

            m_dataReader = null;
            m_odbcCommand.CommandText = "SELECT    nexusGarments_Accounts_LOTBill_1.id, nexusGarments_KnittingProcess.DCNo, nexusGarments_Accounts_LOTBill_1.billno, (SELECT SUM(nexusGarments_Accounts_BillTax.Prec) AS Expr1 FROM nexusGarments_Accounts_LOTBill INNER JOIN nexusGarments_Accounts_BillTax ON nexusGarments_Accounts_LOTBill.id = nexusGarments_Accounts_BillTax.id WHERE      (nexusGarments_Accounts_LOTBill.Refno = " + refno + ")) AS sumprecentage, nexusGarments_KnittingProcess.recWeight,  nexusGarments_Accounts_LOTBill_1.debitweight, nexusGarments_Accounts_LOTBill_1.BillUPrice, nexusGarments_Accounts_LOTBill_1.DebitAmmount, (nexusGarments_KnittingProcess.recWeight*nexusGarments_Accounts_LOTBill_1.BillUPrice) -  nexusGarments_Accounts_LOTBill_1.DebitAmmount as [BillingPrice] FROM         nexusGarments_Accounts_LOTBill AS nexusGarments_Accounts_LOTBill_1 INNER JOIN nexusGarments_KnittingProcess ON nexusGarments_Accounts_LOTBill_1.mapid = nexusGarments_KnittingProcess.id INNER JOIN nexusGarments_KnittingProgram ON nexusGarments_KnittingProcess.mapid = nexusGarments_KnittingProgram.id where nexusGarments_Accounts_LOTBill_1.refno = " + refno + "";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    // lblMake_new.Text = m_dataReader["make"].ToString();
                    //  ddlHour.Items.Add(m_dataReader["cfrom"].ToString());
                    // lblSubject.Text = m_dataReader[sel.Trim().ToString()].ToString();

                    //lblgtotal.Text = m_dataReader["name"].ToString() + " " + m_dataReader["lname"].ToString();

                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), m_dataReader["taxes"].ToString().Trim());

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadBillingGrid(string p, DataTable dt)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("id");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DC Number");
            dt.Columns.Add("Bill No");
            dt.Columns.Add("Rec Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Dbt Weight", System.Type.GetType("System.Double"));
            dt.Columns.Add("Bill Price", System.Type.GetType("System.Double"));
            dt.Columns.Add("Dbt Ammount", System.Type.GetType("System.Double"));
            dt.Columns.Add("Billing Ammount", System.Type.GetType("System.Double"));
            dt.Columns.Add("bm", System.Type.GetType("System.Double"));

            m_dataReader = null;
            m_odbcCommand.CommandText = p;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    dt.Rows.Add(m_dataReader["id"].ToString().Trim(), "True", m_dataReader["dcno"].ToString().Trim(), m_dataReader["billno"].ToString().Trim(), m_dataReader["recweight"].ToString().Trim(), m_dataReader["debitweight"].ToString().Trim(), m_dataReader["billuprice"].ToString().Trim(), m_dataReader["debitAmmount"].ToString().Trim(),Math.Round(Convert.ToDecimal( m_dataReader["billingprice"].ToString().Trim())),Math.Truncate(Convert.ToDecimal( m_dataReader["billingprice"].ToString().Trim())));

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal void Loadchequeno(DevExpress.XtraEditors.TextEdit txtNo, string p)
        {
           
            m_dataReader = null;
            m_odbcCommand.CommandText = p;
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                     txtNo.Text =  m_dataReader["nextno"].ToString().Trim();
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }



        internal void LoadUnPaidSalary(DataSet ds)
        {

            ds.Clear();
            try
            {
                string KnittingMasterQuery = "SELECT DISTINCT nexusGarments_Master_Employee.Empid, nexusGarments_Master_Employee.Empname as Name, nexusGarments_Master_Designation.Designation,  nexusGarments_Master_Grade.Grade, (SELECT    round(SUM(Salary),0) AS Expr1 FROM          nexusGarments_EmpAllogation WHERE      (empid = nexusGarments_Master_Employee.id)) AS salary FROM         nexusGarments_EmpAllogation AS nexusGarments_EmpAllogation_1 INNER JOIN nexusGarments_Master_Employee ON nexusGarments_EmpAllogation_1.empid = nexusGarments_Master_Employee.id INNER JOIN nexusGarments_Master_Grade ON nexusGarments_Master_Employee.grade = nexusGarments_Master_Grade.id INNER JOIN nexusGarments_Master_Designation ON nexusGarments_Master_Employee.Designation = nexusGarments_Master_Designation.id WHERE     (nexusGarments_EmpAllogation_1.OutwardDateTime <> '') AND (nexusGarments_EmpAllogation_1.IsPaid = 'False')";


                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter(KnittingMasterQuery, conns);
                System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter("SELECT DISTINCT  nexusGarments_Master_Employee.Empid, nexusGarments_Units.UnitName as Unit, nexusGarments_Master_Shift.Shiftid as Shift, nexusGarments_EmpAllogation.AllogDate ,  nexusGarments_EmpAllogation.InwardDateTime, nexusGarments_EmpAllogation.OutwardDateTime, nexusGarments_EmpAllogation.IntervalMin,  nexusGarments_EmpAllogation.Salary FROM       nexusGarments_Master_Employee   INNER JOIN nexusGarments_EmpAllogation ON nexusGarments_EmpAllogation.empid = nexusGarments_Master_Employee.id INNER JOIN nexusGarments_Units ON nexusGarments_EmpAllogation.unitid = nexusGarments_Units.id INNER JOIN nexusGarments_Master_Shift ON nexusGarments_EmpAllogation.shiftid = nexusGarments_Master_Shift.id WHERE     (nexusGarments_EmpAllogation.OutwardDateTime <> '') AND (nexusGarments_EmpAllogation.IsPaid = 'False')", conns);
                // System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter3 = new System.Data.OleDb.OleDbDataAdapter("SELECT  dcid,recrolls as [Rec Rolls] , recweight as [Rec weight], recyarn as [Returned Yarn],recdate as [Rec Date] FROM  nexusGarments_YarnInwardLog where dcid in( select id from nexusGarments_KnittingProcess where mapid in (select id from nexusGarments_knittingProgram where processid=" + poid + "))", "Data Source=TAGGIN-PC\\TAGGINDEV;Initial Catalog=NexusGarments;Persist Security Info=True;User ID=sa;Password=openforsk;Provider=SQLOLEDB;");
                oleDbDataAdapter1.Fill(ds, "nexusGarments_EmpAllogation_1");
                oleDbDataAdapter2.Fill(ds, "nexusGarments_Master_Employee");
                ////oleDbDataAdapter3.Fill(ds, "nexusGarments_YarnInwardLog");
                DataColumn[] da1 = new DataColumn[1];

                DataColumn[] da2 = new DataColumn[1];

                ////da1 = (ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"] , ds.Tables["nexusGarments_StoreInwardLog"].Columns["PO Number"]);
                da1[0] = ds.Tables["nexusGarments_EmpAllogation_1"].Columns["Empid"];
                //da1[1] = ds.Tables["nexusGarments_YarnDyingLog"].Columns["Invoice Number"];
                da2[0] = ds.Tables["nexusGarments_Master_Employee"].Columns["Empid"];
                //da2[1] = ds.Tables["nexusGarments_YarnDyingProcess"].Columns["Invoice Number"];
                ds.Relations.Add("DeliverInformation", da1, da2);

                //ds.Relations.Add("Inward Info",
                //    ds.Tables["nexusGarments_KnittingProcess"].Columns["id"],
                //    ds.Tables["nexusGarments_YarnInwardLog"].Columns["dcid"]);
            }
            catch
            {
            }

        }


        internal void LoadStorePODetails(DataTable dt, string p)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("PO Number");
            dt.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Date");
   
            dt.Columns.Add("Ordered Quantity", System.Type.GetType("System.Double"));
            dt.Columns.Add("Received Quantity", System.Type.GetType("System.Double"));
            dt.Columns.Add("Billing Units", System.Type.GetType("System.Double"));
            dt.Columns.Add("Billing Amount", System.Type.GetType("System.Double"));
            dt.Columns.Add("TDS", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Units", System.Type.GetType("System.Double"));
            dt.Columns.Add("Debit Amount", System.Type.GetType("System.Double"));
            
            m_dataReader = null;
            m_odbcCommand.CommandText = p;
            m_odbcCommand.CommandText = "SELECT  distinct   ponumber, orderon FROM         nexusGarments_StorePO where ponumber  in(" + p + ")";
            try
            {
                OpenDB();
                m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (m_dataReader.Read())
                {
                    dt.Rows.Add(m_dataReader["ponumber"].ToString().Trim(), "false", m_dataReader["orderon"].ToString().Trim(), 0, 0, 0, 0, 0, 0, 0);
                }
                

            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseDB(1);
            }
        }

        internal string[] loadNetQuantity(string p)
        {
            //assign  variables  
            m_dataReader = null;
            m_odbcCommand.CommandText = p;

            //open the DB and read the data
            OpenDB();
            m_dataReader = m_odbcCommand.ExecuteReader(CommandBehavior.CloseConnection);

            //intialize variables
            string[] stringList = new string[m_dataReader.FieldCount];

            //create single dimensional string array to hold DB values
            if (m_dataReader.Read())
            {
                int i = 0;
                while (i < m_dataReader.FieldCount)
                {
                    stringList[i] = m_dataReader[i].ToString();
                    i++;
                }
            }

            //close the connection and return the data
            m_dataReader.Close();
            return stringList;


        }

       
    }
}

using System;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Web;
using System.IO;
using System.IO.Compression;


namespace PLABSM
{
    public enum EncryptMode
    {
        Live,
        Local
    }

    public enum DbProvider
    {
        MSSql
    }

    public enum ConnectionModes
    {
        WebDB,
        LocalDB
    }

    public class DAL
    {
        String _localPath = "\\bin\\";
        String _settingsFileName = "DBSettings.xml";
        ConnectionModes _ConnectionMode = ConnectionModes.WebDB;
        

      
        public ConnectionModes ConnectionMode
        {
            get { return _ConnectionMode; }
            set { _ConnectionMode = value; }
        }


        public String SettingsFileName
        {
            get { return _settingsFileName; }
            set { _settingsFileName = value; }
        }

        public String LocalPath
        {
            get { return _localPath; }
            set { _localPath = value; }
        }



        public DAL()
        {

        }

        #region Common Methods


        public DataSet SelectSP(String SpName, String XmlParams, DbProvider SelectedDataBase, String SelectedFinYear = "", String ConnectionTag = "")
        {
            if (SelectedDataBase == DbProvider.MSSql)
                return MSSelectSP(SpName, XmlParams, SelectedFinYear, ConnectionTag);

            //else if (SelectedDataBase == DbProvider.MySql)
            //    return MySQLSelectSP(SpName, XmlParams);

            return new DataSet();
        }
        public String insertSP(String SpName, String XmlParams, DbProvider SelectedDataBase, String SelectedFinYear =  "", String ConnectionTag = "")
        {
            if (SelectedDataBase == DbProvider.MSSql)
                return MSinsertSP(SpName, XmlParams, SelectedFinYear, ConnectionTag);

            //else if (SelectedDataBase == DbProvider.MySql)
            //    return MyInsertSP(SpName, XmlParams);

            return string.Empty;
        }
        public void MSExecQuery(String SqlString, int? SelectedFinYear = -1, String ConnertionTag = "")
        {

            SqlConnection connection = new SqlConnection();
            SqlCommand cmd = new SqlCommand();


            connection = MSCreateConnection(ConnertionTag);

            String CurScript = "";
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;
            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                String[] strarr = SqlString.Split(new string[] { "GO\r\n", "go\r\n", "go\n", "GO\n" }, StringSplitOptions.None);

                for (int i = 0; i < strarr.Length; i++)
                {
                    CurScript = "";
                    if (strarr[i].Trim() != string.Empty)
                    {
                        CurScript = strarr[i].Trim().Replace("\r\nGO", "").Replace("\r\ngo", "");
                        command.CommandText = strarr[i].Trim();
                        int Rslt = command.ExecuteNonQuery();
                    }
                }


                transaction.Commit();
                Console.WriteLine("Both records are written to database.");
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message + " " + CurScript);
            }


        }


        #endregion

        #region MSSql Region
        public DataSet MSSelectQuery(String SpName, String SelectedFinYear = "")
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                con = MSCreateConnection(SelectedFinYear);
                cmd = con.CreateCommand();
                cmd.CommandTimeout = 100000000;
                cmd.CommandText = SpName;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("RslDS");
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                throw new Exception("PLError " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("PLError " + ex.Message + " PLError:- 002");
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
        }
        private DataSet MSSelectSP(String SpName, String XmlParams, String SelectedFinYear = "",String ConnectionTag = "")
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                con = MSCreateConnection( ConnectionTag = "",SelectedFinYear);
                cmd = con.CreateCommand();
                cmd.CommandTimeout = 100000000;
                cmd.CommandText = SpName;
                cmd.CommandType = CommandType.StoredProcedure;

                if (XmlParams.Trim() != String.Empty)
                    this.GetSqlParams(XmlParams, cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("RslDS");
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                throw new Exception("PLError " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("PLError " + ex.Message + " PLError:- 002");
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
        }


        private String MSinsertSP(String SpName, String XmlParams, String SelectedFinYear = "", String ConnectionTag="")
        {
            String status = "0";
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                con = MSCreateConnection(ConnectionTag);
                cmd = new SqlCommand(SpName, con);
                cmd.CommandTimeout = 10000;
                if (XmlParams.Trim() != String.Empty)
                    this.GetSqlParams(XmlParams, cmd);

                cmd.CommandType = CommandType.StoredProcedure;
                status = cmd.ExecuteNonQuery().ToString();
            }
            catch (SqlException ex)
            {
                status = "PLError " + ex.Message;
            }
            catch (Exception ex)
            {
                status = "PLError " + ex.Message;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
            return status;
        }

        public int MSinsertQuery(String SqlQuery, String SelectedFinYear = "")
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            try
            {
                con = MSCreateConnection(SelectedFinYear);
                cmd = con.CreateCommand();
                cmd.CommandTimeout = 100000000;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlQuery;
                Object count = cmd.ExecuteScalar();

                if (count != null)
                    return Convert.ToInt32(count.ToString());
                else
                    return -2;

            }
            catch (SqlException ex)
            {
                throw new Exception("PLError " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("PLError " + ex.Message + " PLError:- 002");
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
        }


        private void GetSqlParams(String XmlParams, SqlCommand cmd)
        {

            try
            {
                XmlDocument xmlDom = new XmlDocument();
                xmlDom.LoadXml(XmlParams);
                foreach (XmlElement parEle in xmlDom)
                {
                    String Paramater = string.Empty;
                    object value = DBNull.Value;
                    String dataType = string.Empty;
                    foreach (XmlElement ele in parEle)
                    {
                        try
                        {
                            Paramater = "@" + ele.Name.Trim().Replace("@", "");
                            value = ele.InnerText;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + "Paramater name Not Correct PLError:- 003");
                        }

                        if (value != null && value.ToString().Trim() != string.Empty && value.ToString().Trim() != "-1")
                            value = value.ToString().Replace("'", "`");
                        else if (value.ToString().Trim() == string.Empty || value.ToString().Trim() == "-1")
                            value = DBNull.Value;


                        cmd.Parameters.Add(new SqlParameter(Paramater, value));
                    }
                    break;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message + "Level :- 004");
            }
        }

        private SqlConnection MSCreateConnection( String ConnectionTag  ,String SelectedFinYear = "" )
        {
            // int checkPoint  = 0 ;
            String ConnectionString = String.Empty;
            try
            {
                ConnectionString = getConnectionStr(DbProvider.MSSql, ConnectionTag, SelectedFinYear);
                SqlConnection connection = new SqlConnection();
                try
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("PLError Could not Open MS Sql Connection PLError:- 004");
                }

                return connection;
            }
            catch (Exception ex)
            {
                //throw new Exception("PLError" + ex.Message + "Connection String Not Found 005 :---> [ ]");
                throw new Exception(ex.Message);
            }
        }

        #endregion


        private String getConnectionStr(DbProvider DbProvider, String ConnectionTag, String SelectedFinYear ="")
        {

            String ConProv = "MsSqlCon";

            if(ConnectionTag != "")
                ConProv = ConnectionTag;

            if (SelectedFinYear != "")
                ConProv += SelectedFinYear.ToString();



            System.Xml.XmlDocument obj = new XmlDocument();

            string appPath = string.Empty;

            try
            {
                if (this._ConnectionMode == ConnectionModes.LocalDB)
                    appPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\" + this._settingsFileName;
                else
                    appPath = HttpContext.Current.Server.MapPath("~") + this._localPath + "\\" + this._settingsFileName;
            }
            catch
            {
                appPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\" + this._settingsFileName;
            }

            try
            {
                string XmlString = File.ReadAllText(appPath);


                obj.LoadXml(XmlString);
                String InnerTag = obj.GetElementsByTagName(ConProv)[0].InnerText;
				//   String InnerTagD = PLEncryptor.RSA.RSADecryption(InnerTag);
				var InnerTagD = InnerTag;


                if (InnerTagD.Trim() != "")
                    InnerTag = InnerTagD;

                return InnerTag;


            }
            catch (Exception ex)
            {
                throw new Exception("Connection String Error");
            }
        }

        


    }
}
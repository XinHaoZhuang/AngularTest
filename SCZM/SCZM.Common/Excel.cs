using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCZM.Common
{
    public class Excel
    {
        public static DataSet ReadExcel(string sheetName, string strFileName, string strFileType)
        {
            string filepath = strFileName;
            string strSql = "select * from [" + sheetName + "$] ";

            DataSet ds = new DataSet();
            string strConn = "";
            //if (strFileType == "xls")
            //{
            //    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
            //}
            //else
            //{
                strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + filepath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1';";
            //}
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                if (conn.State == ConnectionState.Open) { conn.Close(); }
                conn.Open();
                OleDbDataAdapter mycommand = new OleDbDataAdapter(strSql, conn);
                mycommand.Fill(ds, "myexcle");
                conn.Close();

            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open) { conn.Close(); }
                throw;
            }

            return ds;
        }
    }
}

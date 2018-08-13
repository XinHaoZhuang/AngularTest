using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
using System.Data.OleDb;

namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Common 公共方法
    /// </summary>
    public partial class sys_Common
    {
        public sys_Common()
        { }
        /// <summary>
        /// 获得最新申请单号
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="signName">标识</param>
        /// <returns></returns>
        public string GetNewApplyNo(string tableName,string signName)
        {
            string newApplyNo = "";
            string beforeNo = signName + DateTime.Now.ToString("yyyyMMdd");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(ApplyNo) from " + tableName + " where ApplyNo like '" + beforeNo + "%'");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                string maxNo = dt.Rows[0][0].ToString();
                string afterNo = "00" + (Convert.ToInt32(maxNo.Substring(maxNo.Length - 3)) + 1).ToString();
                newApplyNo = beforeNo + afterNo.Substring(afterNo.Length - 3);
            }
            else
            {
                newApplyNo = beforeNo + "001";
            }
            return newApplyNo;
        }
    }
}

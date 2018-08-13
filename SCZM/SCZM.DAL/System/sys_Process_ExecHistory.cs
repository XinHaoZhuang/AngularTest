using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Process_ExecHistory。

    /// </summary>
    public partial class sys_Process_ExecHistory
    {
        public sys_Process_ExecHistory()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Process_ExecHistory");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Process_ExecHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Process_ExecHistory(");
            strSql.Append("ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel)");
            strSql.Append(" values (");
            strSql.Append("@ProcessId,@BillSign,@BillId,@OrderId,@PostId,@PostName,@AuditorId,@AuditorName,@AuditorTime,@FlagAudit,@AuditorOpinion,@FlagDel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProcessId", SqlDbType.Int,4),
					new SqlParameter("@BillSign", SqlDbType.VarChar,50),
					new SqlParameter("@BillId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@PostId", SqlDbType.Int,4),
					new SqlParameter("@PostName", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditorId", SqlDbType.NChar,10),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,20),
					new SqlParameter("@AuditorTime", SqlDbType.DateTime),
					new SqlParameter("@FlagAudit", SqlDbType.Int,4),
					new SqlParameter("@AuditorOpinion", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
            parameters[0].Value = model.ProcessId;
            parameters[1].Value = model.BillSign;
            parameters[2].Value = model.BillId;
            parameters[3].Value = model.OrderId;
            parameters[4].Value = model.PostId;
            parameters[5].Value = model.PostName;
            parameters[6].Value = model.AuditorId;
            parameters[7].Value = model.AuditorName;
            parameters[8].Value = model.AuditorTime;
            parameters[9].Value = model.FlagAudit;
            parameters[10].Value = model.AuditorOpinion;
            parameters[11].Value = model.FlagDel;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据

        /// </summary>
        public int Update(SCZM.Model.System.sys_Process_ExecHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process_ExecHistory set ");
            strSql.Append("ProcessId=@ProcessId,");
            strSql.Append("BillSign=@BillSign,");
            strSql.Append("BillId=@BillId,");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("PostId=@PostId,");
            strSql.Append("PostName=@PostName,");
            strSql.Append("AuditorId=@AuditorId,");
            strSql.Append("AuditorName=@AuditorName,");
            strSql.Append("AuditorTime=@AuditorTime,");
            strSql.Append("FlagAudit=@FlagAudit,");
            strSql.Append("AuditorOpinion=@AuditorOpinion,");
            strSql.Append("FlagDel=@FlagDel");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProcessId", SqlDbType.Int,4),
					new SqlParameter("@BillSign", SqlDbType.VarChar,50),
					new SqlParameter("@BillId", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@PostId", SqlDbType.Int,4),
					new SqlParameter("@PostName", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditorId", SqlDbType.NChar,10),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar,20),
					new SqlParameter("@AuditorTime", SqlDbType.DateTime),
					new SqlParameter("@FlagAudit", SqlDbType.Int,4),
					new SqlParameter("@AuditorOpinion", SqlDbType.NVarChar,100),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ProcessId;
            parameters[1].Value = model.BillSign;
            parameters[2].Value = model.BillId;
            parameters[3].Value = model.OrderId;
            parameters[4].Value = model.PostId;
            parameters[5].Value = model.PostName;
            parameters[6].Value = model.AuditorId;
            parameters[7].Value = model.AuditorName;
            parameters[8].Value = model.AuditorTime;
            parameters[9].Value = model.FlagAudit;
            parameters[10].Value = model.AuditorOpinion;
            parameters[11].Value = model.FlagDel;
            parameters[12].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process_ExecHistory set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDList)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process_ExecHistory set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Process_ExecHistory GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel from sys_Process_ExecHistory ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Process_ExecHistory model = new SCZM.Model.System.sys_Process_ExecHistory();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体 子方法 从DataRow中

        /// </summary>
        public SCZM.Model.System.sys_Process_ExecHistory DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Process_ExecHistory model = new SCZM.Model.System.sys_Process_ExecHistory();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ProcessId"] != null && row["ProcessId"].ToString() != "")
                {
                    model.ProcessId = int.Parse(row["ProcessId"].ToString());
                }
                if (row["BillSign"] != null)
                {
                    model.BillSign = row["BillSign"].ToString();
                }
                if (row["BillId"] != null && row["BillId"].ToString() != "")
                {
                    model.BillId = int.Parse(row["BillId"].ToString());
                }
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = int.Parse(row["OrderId"].ToString());
                }
                if (row["PostId"] != null && row["PostId"].ToString() != "")
                {
                    model.PostId = int.Parse(row["PostId"].ToString());
                }
                if (row["PostName"] != null)
                {
                    model.PostName = row["PostName"].ToString();
                }
                if (row["AuditorId"] != null)
                {
                    model.AuditorId = row["AuditorId"].ToString();
                }
                if (row["AuditorName"] != null)
                {
                    model.AuditorName = row["AuditorName"].ToString();
                }
                if (row["AuditorTime"] != null && row["AuditorTime"].ToString() != "")
                {
                    model.AuditorTime = DateTime.Parse(row["AuditorTime"].ToString());
                }
                if (row["FlagAudit"] != null && row["FlagAudit"].ToString() != "")
                {
                    model.FlagAudit = int.Parse(row["FlagAudit"].ToString());
                }
                if (row["AuditorOpinion"] != null)
                {
                    model.AuditorOpinion = row["AuditorOpinion"].ToString();
                }
                if (row["FlagDel"] != null && row["FlagDel"].ToString() != "")
                {
                    if ((row["FlagDel"].ToString() == "1") || (row["FlagDel"].ToString().ToLower() == "true"))
                    {
                        model.FlagDel = true;
                    }
                    else
                    {
                        model.FlagDel = false;
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel ");
            strSql.Append("FROM sys_Process_ExecHistory");
            strSql.Append(" where FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表 通过ID
        /// </summary>
        public DataSet GetList(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel ");
            strSql.Append("FROM sys_Process_ExecHistory");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表 通过Param
        /// </summary>
        public DataSet GetList(string strWhere, List<SqlParameter> parameterList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ProcessId,BillSign,BillId,OrderId,PostId,PostName,AuditorId,AuditorName,AuditorTime,FlagAudit,AuditorOpinion,FlagDel ");
            strSql.Append("FROM sys_Process_ExecHistory");
            strSql.Append(" where FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameterList);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM sys_Process_ExecHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_Process_ExecHistory T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "sys_Process_ExecHistory";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  基本方法
        #region  扩展方法
        #endregion  扩展方法
    }
}


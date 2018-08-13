using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Process_BillSet。

    /// </summary>
    public partial class sys_Process_BillSet
    {
        public sys_Process_BillSet()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Process_BillSet");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据

        /// </summary>
        public int Add(SCZM.Model.System.sys_Process_BillSet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Process_BillSet(");
            strSql.Append("BillSign,TableName,BillName,ProcessId,SupId,SortId,FlagHistory,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@BillSign,@TableName,@BillName,@ProcessId,@SupId,@SortId,@FlagHistory,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BillSign", SqlDbType.VarChar,50),
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
					new SqlParameter("@BillName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessId", SqlDbType.Int,4),
					new SqlParameter("@SupId", SqlDbType.Int,4),
					new SqlParameter("@SortId", SqlDbType.Int,4),
					new SqlParameter("@FlagHistory", SqlDbType.Bit,1),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.BillSign;
            parameters[1].Value = model.TableName;
            parameters[2].Value = model.BillName;
            parameters[3].Value = model.ProcessId;
            parameters[4].Value = model.SupId;
            parameters[5].Value = model.SortId;
            parameters[6].Value = model.FlagHistory;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;

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
        public int Update(SCZM.Model.System.sys_Process_BillSet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process_BillSet set ");
            strSql.Append("BillSign=@BillSign,");
            strSql.Append("TableName=@TableName,");
            strSql.Append("BillName=@BillName,");
            strSql.Append("ProcessId=@ProcessId,");
            strSql.Append("SupId=@SupId,");
            strSql.Append("SortId=@SortId,");
            strSql.Append("FlagHistory=@FlagHistory,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BillSign", SqlDbType.VarChar,50),
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
					new SqlParameter("@BillName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessId", SqlDbType.Int,4),
					new SqlParameter("@SupId", SqlDbType.Int,4),
					new SqlParameter("@SortId", SqlDbType.Int,4),
					new SqlParameter("@FlagHistory", SqlDbType.Bit,1),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.BillSign;
            parameters[1].Value = model.TableName;
            parameters[2].Value = model.BillName;
            parameters[3].Value = model.ProcessId;
            parameters[4].Value = model.SupId;
            parameters[5].Value = model.SortId;
            parameters[6].Value = model.FlagHistory;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaName;
            parameters[9].Value = model.OperaTime;
            parameters[10].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Process_BillSet set FlagDel=1 ");
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
            strSql.Append("update sys_Process_BillSet set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_Process_BillSet GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,BillSign,TableName,BillName,ProcessId,SupId,SortId,FlagHistory,FlagDel,OperaName,OperaTime from sys_Process_BillSet ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Process_BillSet model = new SCZM.Model.System.sys_Process_BillSet();
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
        public SCZM.Model.System.sys_Process_BillSet DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Process_BillSet model = new SCZM.Model.System.sys_Process_BillSet();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["BillSign"] != null)
                {
                    model.BillSign = row["BillSign"].ToString();
                }
                if (row["TableName"] != null)
                {
                    model.TableName = row["TableName"].ToString();
                }
                if (row["BillName"] != null)
                {
                    model.BillName = row["BillName"].ToString();
                }
                if (row["ProcessId"] != null && row["ProcessId"].ToString() != "")
                {
                    model.ProcessId = int.Parse(row["ProcessId"].ToString());
                }
                if (row["SupId"] != null && row["SupId"].ToString() != "")
                {
                    model.SupId = int.Parse(row["SupId"].ToString());
                }
                if (row["SortId"] != null && row["SortId"].ToString() != "")
                {
                    model.SortId = int.Parse(row["SortId"].ToString());
                }
                if (row["FlagHistory"] != null && row["FlagHistory"].ToString() != "")
                {
                    if ((row["FlagHistory"].ToString() == "1") || (row["FlagHistory"].ToString().ToLower() == "true"))
                    {
                        model.FlagHistory = true;
                    }
                    else
                    {
                        model.FlagHistory = false;
                    }
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
                if (row["OperaName"] != null)
                {
                    model.OperaName = row["OperaName"].ToString();
                }
                if (row["OperaTime"] != null && row["OperaTime"].ToString() != "")
                {
                    model.OperaTime = DateTime.Parse(row["OperaTime"].ToString());
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
            strSql.Append("select ID,BillSign,TableName,BillName,ProcessId,SupId,SortId,FlagHistory,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Process_BillSet");
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
            strSql.Append("select ID,BillSign,TableName,BillName,ProcessId,SupId,SortId,FlagHistory,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Process_BillSet");
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
            strSql.Append("select ID,BillSign,TableName,BillName,ProcessId,SupId,SortId,FlagHistory,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Process_BillSet");
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
            strSql.Append("select count(1) FROM sys_Process_BillSet ");
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
            strSql.Append(")AS Row, T.*  from sys_Process_BillSet T ");
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
            parameters[0].Value = "sys_Process_BillSet";
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
        /// <summary>
        /// 获得数据列表-带审批流信息 通过SQL语句
        /// </summary>
        public DataSet GetMxList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.BillSign,a.BillName,a.ProcessId,a.SupId,a.SortId,a.FlagHistory,a.OperaName,a.OperaTime,b.ProcessName,b.Memo ");
            strSql.Append("FROM sys_Process_BillSet a left join sys_Process b on a.ProcessId=b.ID and b.FlagDel=0 and b.FlagUse=1 ");
            strSql.Append(" where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by SortId");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 是否存在单据的审批流
        /// </summary>
        public bool ExistsBillProc(string billSign)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Process_BillSet");
            strSql.Append(" where FlagDel=0 and BillSign=@billSign ");
            SqlParameter[] parameters = {
					new SqlParameter("@billSign", SqlDbType.VarChar)};
            parameters[0].Value = billSign;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        #endregion  扩展方法
    }
}


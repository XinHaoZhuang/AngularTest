using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SCZM.DBUtility;//Please add references
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类:sys_OperaLog
    /// </summary>
    public partial class sys_OperaLog
    {
        public sys_OperaLog()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录

        /// </summary>
        public bool Exists(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_OperaLog");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据

        /// </summary>
        public long Add(SCZM.Model.System.sys_OperaLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_OperaLog(");
            strSql.Append("PerId,PerName,PerAccount,MenuId,OperaType,Memo,OperaTime,LoginIP)");
            strSql.Append(" values (");
            strSql.Append("@PerId,@PerName,@PerAccount,@MenuId,@OperaType,@Memo,@OperaTime,@LoginIP)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PerId", SqlDbType.Int,4),
					new SqlParameter("@PerName", SqlDbType.NVarChar,20),
					new SqlParameter("@PerAccount", SqlDbType.NVarChar,20),
					new SqlParameter("@MenuId", SqlDbType.Int,4),
					new SqlParameter("@OperaType", SqlDbType.VarChar,20),
					new SqlParameter("@Memo", SqlDbType.NVarChar,200),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@LoginIP", SqlDbType.VarChar,20)};
            parameters[0].Value = model.PerId;
            parameters[1].Value = model.PerName;
            parameters[2].Value = model.PerAccount;
            parameters[3].Value = model.MenuId;
            parameters[4].Value = model.OperaType;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.OperaTime;
            parameters[7].Value = model.LoginIP;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// 更新一条数据

        /// </summary>
        public int Update(SCZM.Model.System.sys_OperaLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_OperaLog set ");
            strSql.Append("PerId=@PerId,");
            strSql.Append("PerName=@PerName,");
            strSql.Append("PerAccount=@PerAccount,");
            strSql.Append("MenuId=@MenuId,");
            strSql.Append("OperaType=@OperaType,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("LoginIP=@LoginIP");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@PerId", SqlDbType.Int,4),
					new SqlParameter("@PerName", SqlDbType.NVarChar,20),
					new SqlParameter("@PerAccount", SqlDbType.NVarChar,20),
					new SqlParameter("@MenuId", SqlDbType.Int,4),
					new SqlParameter("@OperaType", SqlDbType.VarChar,20),
					new SqlParameter("@Memo", SqlDbType.NVarChar,200),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@LoginIP", SqlDbType.VarChar,20),
					new SqlParameter("@ID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.PerId;
            parameters[1].Value = model.PerName;
            parameters[2].Value = model.PerAccount;
            parameters[3].Value = model.MenuId;
            parameters[4].Value = model.OperaType;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.OperaTime;
            parameters[7].Value = model.LoginIP;
            parameters[8].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除一条数据

        /// </summary>
        public int Delete(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_OperaLog ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_OperaLog ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_OperaLog GetModel(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,PerId,PerName,PerAccount,MenuId,OperaType,Memo,OperaTime,LoginIP from sys_OperaLog ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
			};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_OperaLog model = new SCZM.Model.System.sys_OperaLog();
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
        /// 得到一个对象实体

        /// </summary>
        public SCZM.Model.System.sys_OperaLog DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_OperaLog model = new SCZM.Model.System.sys_OperaLog();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = long.Parse(row["ID"].ToString());
                }
                if (row["PerId"] != null && row["PerId"].ToString() != "")
                {
                    model.PerId = int.Parse(row["PerId"].ToString());
                }
                if (row["PerName"] != null)
                {
                    model.PerName = row["PerName"].ToString();
                }
                if (row["PerAccount"] != null)
                {
                    model.PerAccount = row["PerAccount"].ToString();
                }
                if (row["MenuId"] != null && row["MenuId"].ToString() != "")
                {
                    model.MenuId = int.Parse(row["MenuId"].ToString());
                }
                if (row["OperaType"] != null)
                {
                    model.OperaType = row["OperaType"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["OperaTime"] != null && row["OperaTime"].ToString() != "")
                {
                    model.OperaTime = DateTime.Parse(row["OperaTime"].ToString());
                }
                if (row["LoginIP"] != null)
                {
                    model.LoginIP = row["LoginIP"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PerId,PerName,PerAccount,MenuId,OperaType,Memo,OperaTime,LoginIP ");
            strSql.Append(" FROM sys_OperaLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by OperaTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据

        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,PerId,PerName,PerAccount,MenuId,OperaType,Memo,OperaTime,LoginIP ");
            strSql.Append(" FROM sys_OperaLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM sys_OperaLog ");
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
            strSql.Append(")AS Row, T.*  from sys_OperaLog T ");
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
            parameters[0].Value = "sys_OperaLog";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 获得带模块名的数据列表

        /// </summary>
        public DataSet GetList_Menu(string strWhere, List<SqlParameter> parameterList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.PerName,a.PerAccount,b.MenuName,a.Memo,a.OperaTime,a.LoginIP from sys_OperaLog a ");
            strSql.Append("left join  sys_Menu b on a.MenuId=b.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by a.OperaTime desc ");
            return DbHelperSQL.Query(strSql.ToString(),parameterList);
        }
        /// <summary>
        /// 根据筛选条件批量删除数据

        /// </summary>
        public int DeleteListByFilter(string strWhere, List<SqlParameter> parameterList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_OperaLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameterList.ToArray());
            return rows;
            
        }
        #endregion  ExtensionMethod
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.System
{
    /// <summary>
    /// 数据访问类sys_Post。

    /// </summary>
    public partial class sys_Post
    {
        public sys_Post()
        { }
        #region  基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Post");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.System.sys_Post model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_Post(");
            strSql.Append("DepId,PostName,Memo,SortId,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@DepId,@PostName,@Memo,@SortId,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DepId", SqlDbType.Int,4),
					new SqlParameter("@PostName", SqlDbType.NVarChar,10),
					new SqlParameter("@Memo", SqlDbType.NVarChar,100),
					new SqlParameter("@SortId", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.DepId;
            parameters[1].Value = model.PostName;
            parameters[2].Value = model.Memo;
            parameters[3].Value = model.SortId;
            parameters[4].Value = model.FlagDel;
            parameters[5].Value = model.OperaName;
            parameters[6].Value = model.OperaTime;

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
        public int Update(SCZM.Model.System.sys_Post model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Post set ");
            strSql.Append("DepId=@DepId,");
            strSql.Append("PostName=@PostName,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("SortId=@SortId,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepId", SqlDbType.Int,4),
					new SqlParameter("@PostName", SqlDbType.NVarChar,10),
					new SqlParameter("@Memo", SqlDbType.NVarChar,100),
					new SqlParameter("@SortId", SqlDbType.Int,4),
					new SqlParameter("@FlagDel", SqlDbType.Bit,1),
					new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
					new SqlParameter("@OperaTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.DepId;
            parameters[1].Value = model.PostName;
            parameters[2].Value = model.Memo;
            parameters[3].Value = model.SortId;
            parameters[4].Value = model.FlagDel;
            parameters[5].Value = model.OperaName;
            parameters[6].Value = model.OperaTime;
            parameters[7].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_Post set FlagDel=1 ");
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
            strSql.Append("update sys_Post set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.System.sys_Post GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,DepId,PostName,Memo,SortId,FlagDel,OperaName,OperaTime from sys_Post ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.System.sys_Post model = new SCZM.Model.System.sys_Post();
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
        public SCZM.Model.System.sys_Post DataRowToModel(DataRow row)
        {
            SCZM.Model.System.sys_Post model = new SCZM.Model.System.sys_Post();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["DepId"] != null && row["DepId"].ToString() != "")
                {
                    model.DepId = int.Parse(row["DepId"].ToString());
                }
                if (row["PostName"] != null)
                {
                    model.PostName = row["PostName"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["SortId"] != null && row["SortId"].ToString() != "")
                {
                    model.SortId = int.Parse(row["SortId"].ToString());
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
            strSql.Append("select ID,DepId,PostName,Memo,SortId,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Post");
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
            strSql.Append("select ID,DepId,PostName,Memo,SortId,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Post");
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
            strSql.Append("select ID,DepId,PostName,Memo,SortId,FlagDel,OperaName,OperaTime ");
            strSql.Append("FROM sys_Post");
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
            strSql.Append("select count(1) FROM sys_Post ");
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
            strSql.Append(")AS Row, T.*  from sys_Post T ");
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
            parameters[0].Value = "sys_Post";
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
        /// 查询岗位名是否存在

        /// </summary>
        public bool ExistsPostName(string postName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_Post ");
            strSql.Append("where FlagDel=0 and PostName=@postName");
            SqlParameter[] parameters = {
                    new SqlParameter("@PostName", SqlDbType.NVarChar)};
            parameters[0].Value = postName;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更改审批流表岗位名称

        /// </summary>
        /// <param name="oldPostName"></param>
        /// <param name="newPostName"></param>
        /// <returns></returns>
        public bool UpdatePostName(string oldPostName, string newPostName)
        {
            try
            {
                string strSql = "update sys_Process set Memo=substring(replace('→ '+Memo+' →','→ ' + @oldPostName + ' →','→ ' + @newPostName + ' →'),3,LEN(replace('→ '+Memo+' →','→ ' + @oldPostName + ' →','→ ' + @newPostName + ' →'))-4) ";
                strSql += "where '→ '+Memo+' →' like '%→ '+@oldPostName+' →%'";
                SqlParameter[] parameters = {
                    new SqlParameter("@oldPostName", SqlDbType.VarChar,50),
					new SqlParameter("@newPostName", SqlDbType.VarChar,50)};
                parameters[0].Value = oldPostName;
                parameters[1].Value = newPostName;
                DbHelperSQL.ExecuteSql(strSql, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获得数据列表 通过SQL语句
        /// </summary>
        public DataSet GetListAll(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,c.DepName,a.PostName,a.Memo,a.OperaName,a.OperaTime,(case when b.PostId is null then 0 else 1 end) as FlagUse ");
            strSql.Append("from sys_Post a ");
            strSql.Append("left join( ");
            strSql.Append("select distinct PostId from sys_Person where FlagDel=0 ");
            strSql.Append("union ");
            strSql.Append("select distinct b.PostId from sys_Process a ");
            strSql.Append("left join sys_ProcessDetail b on a.ID=b.ProcessId and b.FlagDel=0 where a.FlagDel=0 ");
            strSql.Append(") b on a.ID=b.PostId ");
            strSql.Append("left join sys_Department c on a.DepId=c.ID ");
            strSql.Append(" where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.DepId,a.SortId");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表 通过ID
        /// </summary>
        public DataSet GetInfo(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.DepId,a.PostName,a.Memo,a.SortId,a.OperaName,a.OperaTime,b.PerNameStr,c.ProcessNameStr ");
            strSql.Append("from sys_Post a ");
            strSql.Append("left join (select STUFF(( ");
            strSql.Append("select '，'+PerName from sys_Person where FlagDel=0 and PostId=" + ID.ToString() + " ");
            strSql.Append("for xml path('')),1,1,'') as PerNameStr )b on 1=1 ");
            strSql.Append("left join (select STUFF(( ");
            strSql.Append("select '，'+ProcessName from sys_Process where FlagDel=0 and ID in(select ProcessId from sys_ProcessDetail where FlagDel=0 and PostId=" + ID.ToString() + ") ");
            strSql.Append("for xml path('')),1,1,'') as ProcessNameStr )c on 1=1 ");
            strSql.Append(" where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 校验岗位是否使用 0未使用 1已使用
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public int CheckPostUse(string postIdStr)
        {
            int flagUse = 0;
            StringBuilder strSql = new StringBuilder();
            string procName = "p_sys_CheckPostUse";
            SqlParameter[] parameters = {
					new SqlParameter("@postIdStr", SqlDbType.VarChar)
                                        };
            parameters[0].Value = postIdStr;
            DataTable dt = DbHelperSQL.RunProcedure(procName, parameters, "dt").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                flagUse = Convert.ToInt32(dt.Rows[0][0]);
            }
            return flagUse;
        }
        #endregion  扩展方法
    }
}


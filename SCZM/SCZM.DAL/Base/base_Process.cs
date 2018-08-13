using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Base
{
    /// <summary>
    /// 数据访问类base_Process。
    /// </summary>
    public partial class base_Process
    {
        public base_Process()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_Process");
            strSql.Append(" where FlagDel=0 and  " + FieldName + "=@" + FieldName + " and ID<>@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@" + FieldName, SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = FieldValue;
            parameters[1].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Base.base_Process model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into base_Process(");
            strSql.Append("ClassId,ProcessName,FlagDel,OperaId,OperaName,OperaTime,SortId)");
            strSql.Append(" values (");
            strSql.Append("@ClassId,@ProcessName,@FlagDel,@OperaId,@OperaName,@OperaTime,@SortId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@ClassId", SqlDbType.Int,4),
				new SqlParameter("@ProcessName", SqlDbType.NVarChar,50),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@SortId",SqlDbType.Int,4)};
            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.ProcessName;
            parameters[2].Value = model.FlagDel;
            parameters[3].Value = model.OperaId;
            parameters[4].Value = model.OperaName;
            parameters[5].Value = model.OperaTime;
            parameters[6].Value = model.SortId;

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
        public int Update(SCZM.Model.Base.base_Process model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update base_Process set ");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("ProcessName=@ProcessName,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("SortId=@SortId");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ClassId", SqlDbType.Int,4),
				new SqlParameter("@ProcessName", SqlDbType.NVarChar,50),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@SortId", SqlDbType.Int,4),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.ProcessName;
            parameters[2].Value = model.FlagDel;
            parameters[3].Value = model.OperaId;
            parameters[4].Value = model.OperaName;
            parameters[5].Value = model.OperaTime;
            parameters[6].Value = model.SortId;
            parameters[7].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Base.base_Process GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,SortId,ClassId,ProcessName,FlagDel,OperaId,OperaName,OperaTime from base_Process ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Base.base_Process model = new SCZM.Model.Base.base_Process();
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
        public SCZM.Model.Base.base_Process DataRowToModel(DataRow row)
        {
            SCZM.Model.Base.base_Process model = new SCZM.Model.Base.base_Process();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["SortId"] != null && row["SortId"].ToString() != "") {
                    model.SortId = int.Parse(row["SortId"].ToString());
                }
                if (row["ClassId"] != null && row["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if (row["ProcessName"] != null)
                {
                    model.ProcessName = row["ProcessName"].ToString();
                }
                if (row["FlagDel"] != null && row["FlagDel"].ToString() != "")
                {
                    model.FlagDel = int.Parse(row["FlagDel"].ToString());
                }
                if (row["OperaId"] != null && row["OperaId"].ToString() != "")
                {
                    model.OperaId = int.Parse(row["OperaId"].ToString());
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

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update base_Process set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,b.SortId,a.ClassId,a.ProcessName,b.ProcessClassName,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_Process a ");
            strSql.Append("left join base_ProcessClass b on b.FlagDel=0 and a.ClassId=b.ID ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by b.SortId,a.SortId asc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.SortId,a.ClassId,a.ProcessName,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_Process a ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetComboList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as ProcessId,a.ProcessName,b.ProcessClassName+'——'+a.ProcessName as ClassProcessName ");
            strSql.Append("FROM base_Process a ");
            strSql.Append("left join base_ProcessClass b on a.ClassId=b.ID and b.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.ID");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  扩展方法
    }
}


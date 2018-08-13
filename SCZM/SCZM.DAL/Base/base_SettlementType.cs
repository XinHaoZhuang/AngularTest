using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Base
{
    /// <summary>
    /// 数据访问类base_SettlementType。
    /// </summary>
    public partial class base_SettlementType
    {
        public base_SettlementType()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_SettlementType");
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
        public int Add(SCZM.Model.Base.base_SettlementType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into base_SettlementType(");
            strSql.Append("SettlementTypeName,Memo,SortId,FlagDel,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@SettlementTypeName,@Memo,@SortId,@FlagDel,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@SettlementTypeName", SqlDbType.NVarChar,10),
				new SqlParameter("@Memo", SqlDbType.NVarChar,100),
				new SqlParameter("@SortId", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.SettlementTypeName;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.SortId;
            parameters[3].Value = model.FlagDel;
            parameters[4].Value = model.OperaName;
            parameters[5].Value = model.OperaTime;

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
        public int Update(SCZM.Model.Base.base_SettlementType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update base_SettlementType set ");
            strSql.Append("SettlementTypeName=@SettlementTypeName,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("SortId=@SortId,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@SettlementTypeName", SqlDbType.NVarChar,10),
				new SqlParameter("@Memo", SqlDbType.NVarChar,100),
				new SqlParameter("@SortId", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.SettlementTypeName;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.SortId;
            parameters[3].Value = model.FlagDel;
            parameters[4].Value = model.OperaName;
            parameters[5].Value = model.OperaTime;
            parameters[6].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Base.base_SettlementType GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,SettlementTypeName,Memo,SortId,FlagDel,OperaName,OperaTime from base_SettlementType ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Base.base_SettlementType model = new SCZM.Model.Base.base_SettlementType();
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
        public SCZM.Model.Base.base_SettlementType DataRowToModel(DataRow row)
        {
            SCZM.Model.Base.base_SettlementType model = new SCZM.Model.Base.base_SettlementType();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["SettlementTypeName"] != null)
                {
                    model.SettlementTypeName = row["SettlementTypeName"].ToString();
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

        #endregion  基本方法
        #region  扩展方法
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update base_SettlementType set FlagDel=1 ");
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
            strSql.Append("select a.ID,a.SettlementTypeName,a.Memo,a.SortId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_SettlementType a ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.SettlementTypeName,a.Memo,a.SortId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_SettlementType a ");
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
            strSql.Append("select a.ID as SettlementTypeId,a.SettlementTypeName ");
            strSql.Append("FROM base_SettlementType a ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.SortId,a.ID");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  扩展方法
    }
}


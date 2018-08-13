using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Base
{
    /// <summary>
    /// 数据访问类base_Accessories。
    /// </summary>
    public partial class base_Accessories
    {
        public base_Accessories()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_Accessories");
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
        public int Add(SCZM.Model.Base.base_Accessories model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into base_Accessories(");
            strSql.Append("AccessoriesName,FlagDel,OperaId,OperaName,OperaTime,Unit)");
            strSql.Append(" values (");
            strSql.Append("@AccessoriesName,@FlagDel,@OperaId,@OperaName,@OperaTime,@Unit)");
            SqlParameter[] parameters = {
				new SqlParameter("@AccessoriesName", SqlDbType.NVarChar,20),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@Unit",SqlDbType.NVarChar)};
            parameters[0].Value = model.AccessoriesName;
            parameters[1].Value = model.FlagDel;
            parameters[2].Value = model.OperaId;
            parameters[3].Value = model.OperaName;
            parameters[4].Value = model.OperaTime;
            parameters[5].Value = model.Unit;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Base.base_Accessories model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update base_Accessories set ");
            strSql.Append("AccessoriesName=@AccessoriesName,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("Unit=@Unit");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@AccessoriesName", SqlDbType.NVarChar,20),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@Unit",SqlDbType.NVarChar),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.AccessoriesName;
            parameters[1].Value = model.FlagDel;
            parameters[2].Value = model.OperaId;
            parameters[3].Value = model.OperaName;
            parameters[4].Value = model.OperaTime;
            parameters[5].Value = model.Unit;
            parameters[6].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Base.base_Accessories GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,AccessoriesName,Unit,FlagDel,OperaId,OperaName,OperaTime from base_Accessories ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Base.base_Accessories model = new SCZM.Model.Base.base_Accessories();
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
        public SCZM.Model.Base.base_Accessories DataRowToModel(DataRow row)
        {
            SCZM.Model.Base.base_Accessories model = new SCZM.Model.Base.base_Accessories();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["AccessoriesName"] != null)
                {
                    model.AccessoriesName = row["AccessoriesName"].ToString();
                }
                if (row["Unit"] != null && row["Unit"].ToString() != "")
                {
                    model.Unit = row["Unit"].ToString();
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
            strSql.Append("update base_Accessories set FlagDel=1 ");
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
            strSql.Append("select a.ID,a.AccessoriesName,a.Unit,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_Accessories a ");
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
            strSql.Append("select a.ID,a.AccessoriesName,a.Unit,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_Accessories a ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        public DataSet GetAccessoriesCombo(string strWhere) {
            string strSql = "select a.ID as AccessoriesId,a.AccessoriesName,a.Unit,a.AccessoriesName+'('+a.Unit+')' as 'AccessoriesName--Unit' from base_Accessories a where a.FlagDel=0 ";
            if(strWhere!=""){
                strSql+=strWhere;
            }            
            strSql+= " order by a.AccessoriesName ";
            return DbHelperSQL.Query(strSql);
        }
        #endregion  扩展方法
    }
}


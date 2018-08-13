using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Base
{
    /// <summary>
    /// 数据访问类base_Procedure。
    /// </summary>
    public partial class base_Procedure
    {
        public base_Procedure()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_Procedure");
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
        public int Add(SCZM.Model.Base.base_Procedure model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into base_Procedure(");
            strSql.Append("ProcedureName,SupId,SupList,SortId,Memo,FlagDel,OperaId,OperaName,OperaTime)");
            strSql.Append(" values (");
            strSql.Append("@ProcedureName,@SupId,@SupList,@SortId,@Memo,@FlagDel,@OperaId,@OperaName,@OperaTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@ProcedureName", SqlDbType.NVarChar,50),
				new SqlParameter("@SupId", SqlDbType.Int,4),
				new SqlParameter("@SupList", SqlDbType.VarChar,100),
				new SqlParameter("@SortId", SqlDbType.Int,4),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,50),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ProcedureName;
            parameters[1].Value = model.SupId;
            parameters[2].Value = model.SupList;
            parameters[3].Value = model.SortId;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaId;
            parameters[7].Value = model.OperaName;
            parameters[8].Value = model.OperaTime;

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
        public int Update(SCZM.Model.Base.base_Procedure model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update base_Procedure set ");
            strSql.Append("ProcedureName=@ProcedureName,");
            strSql.Append("SupId=@SupId,");
            strSql.Append("SupList=@SupList,");
            strSql.Append("SortId=@SortId,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ProcedureName", SqlDbType.NVarChar,50),
				new SqlParameter("@SupId", SqlDbType.Int,4),
				new SqlParameter("@SupList", SqlDbType.VarChar,100),
				new SqlParameter("@SortId", SqlDbType.Int,4),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,50),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ProcedureName;
            parameters[1].Value = model.SupId;
            parameters[2].Value = model.SupList;
            parameters[3].Value = model.SortId;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.FlagDel;
            parameters[6].Value = model.OperaId;
            parameters[7].Value = model.OperaName;
            parameters[8].Value = model.OperaTime;
            parameters[9].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Base.base_Procedure GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ProcedureName,SupId,SupList,SortId,Memo,FlagDel,OperaId,OperaName,OperaTime from base_Procedure ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Base.base_Procedure model = new SCZM.Model.Base.base_Procedure();
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
        public SCZM.Model.Base.base_Procedure DataRowToModel(DataRow row)
        {
            SCZM.Model.Base.base_Procedure model = new SCZM.Model.Base.base_Procedure();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ProcedureName"] != null)
                {
                    model.ProcedureName = row["ProcedureName"].ToString();
                }
                if (row["SupId"] != null && row["SupId"].ToString() != "")
                {
                    model.SupId = int.Parse(row["SupId"].ToString());
                }
                if (row["SupList"] != null)
                {
                    model.SupList = row["SupList"].ToString();
                }
                if (row["SortId"] != null && row["SortId"].ToString() != "")
                {
                    model.SortId = int.Parse(row["SortId"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
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
            strSql.Append("update base_Procedure set FlagDel=1 ");
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
            strSql.Append("select a.ID,a.ProcedureName,a.SupId,a.SupList,a.SortId,a.Memo,a.OperaId,a.OperaName,a.OperaTime,b.NumType ");
            strSql.Append("FROM base_Procedure a ");
            strSql.Append("left join base_ProcedureMachineNat b on a.ID=b.ProcedureId and b.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.SortId asc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere, string MachineModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ProcedureName,a.SupId,a.SupList,a.SortId,a.Memo,a.OperaId,a.OperaName,a.OperaTime,b.NumType,b.MachineLevelNat ");
            strSql.Append("FROM base_Procedure a ");
            strSql.Append("left join (select *,case (select MachineLevel from base_MachineModel where MachineModel='" + MachineModel + "' and FlagDel=0) when 1 then MachineLevel10 when 2 then MachineLevel20 when 3 then MachineLevel30 when 4 then MachineLevel40 when 5 then MachineLevel50 else 0 end as MachineLevelNat from base_ProcedureMachineNat where FlagDel=0) b on a.ID=b.ProcedureId ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.SortId asc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ProcedureName,a.SupId,a.SupList,a.SortId,a.Memo,a.OperaId,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_Procedure a ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        public string GetSupList(int Id) {
            string strSql = "select SupList from base_Procedure where FlagDel=0 and ID=" + Id;
            object obj=DbHelperSQL.GetSingle(strSql);
            return obj == null ? "" : obj.ToString();
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetComboList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as ProcedureId,a.ProcedureName ");
            strSql.Append("FROM base_Procedure a ");
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


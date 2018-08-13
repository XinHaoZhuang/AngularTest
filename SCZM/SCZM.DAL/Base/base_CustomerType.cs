using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Base
{
	/// <summary>
	/// 数据访问类base_CustomerType。
	/// </summary>
	public partial class base_CustomerType
	{
		public base_CustomerType()
		{}
		#region  基本方法
		/// <summary>
		/// 某列是否存在
		/// </summary>
		public bool Exists(string FieldName,string FieldValue,int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from base_CustomerType");
			strSql.Append(" where FlagDel=0 and  " + FieldName + "=@" + FieldName + " and ID<>@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@" + FieldName, SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = FieldValue;
			parameters[1].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SCZM.Model.Base.base_CustomerType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into base_CustomerType(");
			strSql.Append("CustTypeName,FlagRegister,FlagDel,OperaId,OperaName,OperaTime)");
			strSql.Append(" values (");
			strSql.Append("@CustTypeName,@FlagRegister,@FlagDel,@OperaId,@OperaName,@OperaTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
				new SqlParameter("@CustTypeName", SqlDbType.NVarChar,50),
				new SqlParameter("@FlagRegister", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
			parameters[0].Value = model.CustTypeName;
			parameters[1].Value = model.FlagRegister;
			parameters[2].Value = model.FlagDel;
			parameters[3].Value = model.OperaId;
			parameters[4].Value = model.OperaName;
			parameters[5].Value = model.OperaTime;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public int Update(SCZM.Model.Base.base_CustomerType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update base_CustomerType set ");
			strSql.Append("CustTypeName=@CustTypeName,");
			strSql.Append("FlagRegister=@FlagRegister,");
			strSql.Append("FlagDel=@FlagDel,");
			strSql.Append("OperaId=@OperaId,");
			strSql.Append("OperaName=@OperaName,");
			strSql.Append("OperaTime=@OperaTime");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@CustTypeName", SqlDbType.NVarChar,50),
				new SqlParameter("@FlagRegister", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.CustTypeName;
			parameters[1].Value = model.FlagRegister;
			parameters[2].Value = model.FlagDel;
			parameters[3].Value = model.OperaId;
			parameters[4].Value = model.OperaName;
			parameters[5].Value = model.OperaTime;
			parameters[6].Value = model.ID;

			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SCZM.Model.Base.base_CustomerType GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CustTypeName,FlagRegister,FlagDel,OperaId,OperaName,OperaTime from base_CustomerType ");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			SCZM.Model.Base.base_CustomerType model=new SCZM.Model.Base.base_CustomerType();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public SCZM.Model.Base.base_CustomerType DataRowToModel(DataRow row)
		{
			SCZM.Model.Base.base_CustomerType model=new SCZM.Model.Base.base_CustomerType();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CustTypeName"]!=null)
				{
					model.CustTypeName=row["CustTypeName"].ToString();
				}
				if(row["FlagRegister"]!=null && row["FlagRegister"].ToString()!="")
				{
					model.FlagRegister=int.Parse(row["FlagRegister"].ToString());
				}
				if(row["FlagDel"]!=null && row["FlagDel"].ToString()!="")
				{
					model.FlagDel=int.Parse(row["FlagDel"].ToString());
				}
				if(row["OperaId"]!=null && row["OperaId"].ToString()!="")
				{
					model.OperaId=int.Parse(row["OperaId"].ToString());
				}
				if(row["OperaName"]!=null)
				{
					model.OperaName=row["OperaName"].ToString();
				}
				if(row["OperaTime"]!=null && row["OperaTime"].ToString()!="")
				{
					model.OperaTime=DateTime.Parse(row["OperaTime"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update base_CustomerType set FlagDel=1 ");
			strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows;
		}

		/// <summary>
		/// 获得数据列表 通过Where条件
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select a.ID,a.CustTypeName,a.FlagRegister,a.OperaId,a.OperaName,a.OperaTime ");
			strSql.Append("FROM base_CustomerType a ");
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select a.ID,a.CustTypeName,a.FlagRegister,a.OperaId,a.OperaName,a.OperaTime ");
			strSql.Append("FROM base_CustomerType a ");
			strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}
        public DataSet GetCustomerTypeCombo(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as CustTypeId,a.CustTypeName,a.FlagRegister from base_CustomerType a where a.FlagDel=0 ");
            if (strWhere != "") {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
		#endregion  扩展方法
	}
}


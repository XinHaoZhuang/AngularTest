using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Base
{
	/// <summary>
	/// 数据访问类base_PersonAssess。
	/// </summary>
	public partial class base_PersonAssess
	{
		public base_PersonAssess()
		{}
		#region  基本方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SCZM.Model.Base.base_PersonAssess model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into base_PersonAssess(");
			strSql.Append("PersonId,Assess,CreateDate,FlagDel,OperaId,OperaName,OperaTime)");
			strSql.Append(" values (");
			strSql.Append("@PersonId,@Assess,@CreateDate,@FlagDel,@OperaId,@OperaName,@OperaTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
				new SqlParameter("@PersonId", SqlDbType.Int,4),
				new SqlParameter("@Assess", SqlDbType.Decimal,9),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,50),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
			parameters[0].Value = model.PersonId;
			parameters[1].Value = model.Assess;
			parameters[2].Value = model.CreateDate;
			parameters[3].Value = model.FlagDel;
			parameters[4].Value = model.OperaId;
			parameters[5].Value = model.OperaName;
			parameters[6].Value = model.OperaTime;

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
		public int Update(SCZM.Model.Base.base_PersonAssess model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update base_PersonAssess set ");
			strSql.Append("PersonId=@PersonId,");
			strSql.Append("Assess=@Assess,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("FlagDel=@FlagDel,");
			strSql.Append("OperaId=@OperaId,");
			strSql.Append("OperaName=@OperaName,");
			strSql.Append("OperaTime=@OperaTime");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@PersonId", SqlDbType.Int,4),
				new SqlParameter("@Assess", SqlDbType.Decimal,9),
				new SqlParameter("@CreateDate", SqlDbType.DateTime),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,50),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.PersonId;
			parameters[1].Value = model.Assess;
			parameters[2].Value = model.CreateDate;
			parameters[3].Value = model.FlagDel;
			parameters[4].Value = model.OperaId;
			parameters[5].Value = model.OperaName;
			parameters[6].Value = model.OperaTime;
			parameters[7].Value = model.ID;

			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SCZM.Model.Base.base_PersonAssess GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,PersonId,Assess,CreateDate,FlagDel,OperaId,OperaName,OperaTime from base_PersonAssess ");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			SCZM.Model.Base.base_PersonAssess model=new SCZM.Model.Base.base_PersonAssess();
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
		public SCZM.Model.Base.base_PersonAssess DataRowToModel(DataRow row)
		{
			SCZM.Model.Base.base_PersonAssess model=new SCZM.Model.Base.base_PersonAssess();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["PersonId"]!=null && row["PersonId"].ToString()!="")
				{
					model.PersonId=int.Parse(row["PersonId"].ToString());
				}
				if(row["Assess"]!=null && row["Assess"].ToString()!="")
				{
					model.Assess=decimal.Parse(row["Assess"].ToString());
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
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
			strSql.Append("update base_PersonAssess set FlagDel=1 ");
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
            //strSql.Append("select a.ID,a.PersonId,a.Assess,a.CreateDate,a.OperaId,a.OperaName,a.OperaTime ");
            //strSql.Append("FROM base_PersonAssess a ");
            //strSql.Append("where a.FlagDel=0");
            strSql.Append("select a.ID as PersonId,a.PerName as PersonName,b.ID,b.Assess,b.CreateDate,b.OperaName,b.OperaTime,c.DepName ");
            strSql.Append("from sys_Person a ");
            strSql.Append("left join (select * from base_PersonAssess where FlagDel=0 and ID in(select MAX(ID) from base_PersonAssess where FlagDel=0 group by PersonId)) b on a.ID=b.PersonId ");
            strSql.Append("left join sys_Department c on a.DepId=c.ID and c.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 ");
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
			strSql.Append("select a.ID,a.PersonId,a.Assess,a.CreateDate,a.OperaId,a.OperaName,a.OperaTime,b.PerName as PersonName ");
			strSql.Append("FROM base_PersonAssess a ");
            strSql.Append("left join sys_Person b on a.PersonId=b.ID ");
			strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}
        public DataSet GetHistory(int ID,int PersonId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.PersonId,a.Assess,a.CreateDate,a.OperaId,a.OperaName,a.OperaTime,b.PerName as PersonName ");
            strSql.Append("FROM base_PersonAssess a ");
            strSql.Append("left join sys_Person b on a.PersonId=b.ID ");
            strSql.Append("where a.FlagDel=0 and a.ID<>@ID and a.PersonId=@PersonId ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4),
                new SqlParameter("@PersonId",SqlDbType.Int,4)};
            parameters[0].Value = ID;
            parameters[1].Value = PersonId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
		#endregion  扩展方法
	}
}


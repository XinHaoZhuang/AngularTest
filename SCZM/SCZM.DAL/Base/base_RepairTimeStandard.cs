using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Base
{
	/// <summary>
	/// 数据访问类base_RepairTimeStandard。
	/// </summary>
	public partial class base_RepairTimeStandard
	{
		public base_RepairTimeStandard()
		{}
		#region  基本方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SCZM.Model.Base.base_RepairTimeStandard model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into base_RepairTimeStandard(");
			strSql.Append("ProcedureId,MachineLevel10,MachineLevel20,MachineLevel30,MachineLevel40,MachineLevel50,NumType,FlagDel,OperaId,OperaName,OperaTime)");
			strSql.Append(" values (");
			strSql.Append("@ProcedureId,@MachineLevel10,@MachineLevel20,@MachineLevel30,@MachineLevel40,@MachineLevel50,@NumType,@FlagDel,@OperaId,@OperaName,@OperaTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
				new SqlParameter("@ProcedureId", SqlDbType.Int,4),
				new SqlParameter("@MachineLevel10", SqlDbType.Decimal,9),
				new SqlParameter("@MachineLevel20", SqlDbType.Decimal,9),
				new SqlParameter("@MachineLevel30", SqlDbType.Decimal,9),
				new SqlParameter("@MachineLevel40", SqlDbType.Decimal,9),
				new SqlParameter("@MachineLevel50", SqlDbType.Decimal,9),
				new SqlParameter("@NumType", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ProcedureId;
			parameters[1].Value = model.MachineLevel10;
			parameters[2].Value = model.MachineLevel20;
			parameters[3].Value = model.MachineLevel30;
			parameters[4].Value = model.MachineLevel40;
			parameters[5].Value = model.MachineLevel50;
			parameters[6].Value = model.NumType;
			parameters[7].Value = model.FlagDel;
			parameters[8].Value = model.OperaId;
			parameters[9].Value = model.OperaName;
			parameters[10].Value = model.OperaTime;

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
		public int Update(SCZM.Model.Base.base_RepairTimeStandard model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update base_RepairTimeStandard set ");
			strSql.Append("ProcedureId=@ProcedureId,");
			strSql.Append("MachineLevel10=@MachineLevel10,");
			strSql.Append("MachineLevel20=@MachineLevel20,");
			strSql.Append("MachineLevel30=@MachineLevel30,");
			strSql.Append("MachineLevel40=@MachineLevel40,");
			strSql.Append("MachineLevel50=@MachineLevel50,");
			strSql.Append("NumType=@NumType,");
			strSql.Append("FlagDel=@FlagDel,");
			strSql.Append("OperaId=@OperaId,");
			strSql.Append("OperaName=@OperaName,");
			strSql.Append("OperaTime=@OperaTime");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ProcedureId", SqlDbType.Int,4),
				new SqlParameter("@MachineLevel10", SqlDbType.Decimal,9),
				new SqlParameter("@MachineLevel20", SqlDbType.Decimal,9),
				new SqlParameter("@MachineLevel30", SqlDbType.Decimal,9),
				new SqlParameter("@MachineLevel40", SqlDbType.Decimal,9),
				new SqlParameter("@MachineLevel50", SqlDbType.Decimal,9),
				new SqlParameter("@NumType", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ProcedureId;
			parameters[1].Value = model.MachineLevel10;
			parameters[2].Value = model.MachineLevel20;
			parameters[3].Value = model.MachineLevel30;
			parameters[4].Value = model.MachineLevel40;
			parameters[5].Value = model.MachineLevel50;
			parameters[6].Value = model.NumType;
			parameters[7].Value = model.FlagDel;
			parameters[8].Value = model.OperaId;
			parameters[9].Value = model.OperaName;
			parameters[10].Value = model.OperaTime;
			parameters[11].Value = model.ID;

			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SCZM.Model.Base.base_RepairTimeStandard GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,ProcedureId,MachineLevel10,MachineLevel20,MachineLevel30,MachineLevel40,MachineLevel50,NumType,FlagDel,OperaId,OperaName,OperaTime from base_RepairTimeStandard ");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			SCZM.Model.Base.base_RepairTimeStandard model=new SCZM.Model.Base.base_RepairTimeStandard();
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
		public SCZM.Model.Base.base_RepairTimeStandard DataRowToModel(DataRow row)
		{
			SCZM.Model.Base.base_RepairTimeStandard model=new SCZM.Model.Base.base_RepairTimeStandard();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["ProcedureId"]!=null && row["ProcedureId"].ToString()!="")
				{
					model.ProcedureId=int.Parse(row["ProcedureId"].ToString());
				}
				if(row["MachineLevel10"]!=null && row["MachineLevel10"].ToString()!="")
				{
					model.MachineLevel10=decimal.Parse(row["MachineLevel10"].ToString());
				}
				if(row["MachineLevel20"]!=null && row["MachineLevel20"].ToString()!="")
				{
					model.MachineLevel20=decimal.Parse(row["MachineLevel20"].ToString());
				}
				if(row["MachineLevel30"]!=null && row["MachineLevel30"].ToString()!="")
				{
					model.MachineLevel30=decimal.Parse(row["MachineLevel30"].ToString());
				}
				if(row["MachineLevel40"]!=null && row["MachineLevel40"].ToString()!="")
				{
					model.MachineLevel40=decimal.Parse(row["MachineLevel40"].ToString());
				}
				if(row["MachineLevel50"]!=null && row["MachineLevel50"].ToString()!="")
				{
					model.MachineLevel50=decimal.Parse(row["MachineLevel50"].ToString());
				}
				if(row["NumType"]!=null && row["NumType"].ToString()!="")
				{
					model.NumType=int.Parse(row["NumType"].ToString());
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
			strSql.Append("update base_RepairTimeStandard set FlagDel=1 ");
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
            //strSql.Append("select a.ID,a.ProcedureId,a.MachineLevel10,a.MachineLevel20,a.MachineLevel30,a.MachineLevel40,a.MachineLevel50,a.NumType,a.OperaId,a.OperaName,a.OperaTime ");
            //strSql.Append("FROM base_RepairTimeStandard a ");
            //strSql.Append("where a.FlagDel=0");
            strSql.Append("select a.ID as ProcedureId,a.ProcedureName,a.SupId,a.SupList,a.SortId,b.ID,b.MachineLevel10,b.MachineLevel20,b.MachineLevel30,b.MachineLevel40,b.MachineLevel50,b.NumType,b.OperaId,b.OperaName,b.OperaTime ");
            strSql.Append("FROM base_Procedure a ");
            strSql.Append("left join base_RepairTimeStandard b on b.ProcedureId=a.ID and b.FlagDel=0 ");
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
			strSql.Append("select a.ID,a.ProcedureId,b.ProcedureName,a.MachineLevel10,a.MachineLevel20,a.MachineLevel30,a.MachineLevel40,a.MachineLevel50,a.NumType,a.OperaId,a.OperaName,a.OperaTime ");
			strSql.Append("FROM base_RepairTimeStandard a ");
            strSql.Append("left join base_Procedure b on a.ProcedureId=b.ID and b.FlagDel=0 ");
			strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}
		#endregion  扩展方法
	}
}


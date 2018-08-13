using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Base
{
	/// <summary>
	/// 数据访问类base_CustomerInformation。
	/// </summary>
	public partial class base_CustomerInformation
	{
		public base_CustomerInformation()
		{}
		#region  基本方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SCZM.Model.Base.base_CustomerInformation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into base_CustomerInformation(");
			strSql.Append("CustCode,CustName,CustType,MachineModelId,MachineModel,MachineCode,MachineState,Part,SMR,Linkman,LinkPhone,FlagDel,OperaName,OperaTime)");
			strSql.Append(" values (");
			strSql.Append("@CustCode,@CustName,@CustType,@MachineModelId,@MachineModel,@MachineCode,@MachineState,@Part,@SMR,@Linkman,@LinkPhone,@FlagDel,@OperaName,@OperaTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
				new SqlParameter("@CustCode", SqlDbType.VarChar,50),
				new SqlParameter("@CustName", SqlDbType.NVarChar,50),
				new SqlParameter("@CustType", SqlDbType.NVarChar,20),
				new SqlParameter("@MachineModelId", SqlDbType.Int,4),
				new SqlParameter("@MachineModel", SqlDbType.NVarChar,20),
				new SqlParameter("@MachineCode", SqlDbType.NVarChar,20),
				new SqlParameter("@MachineState", SqlDbType.NVarChar,20),
				new SqlParameter("@Part", SqlDbType.NVarChar,20),
				new SqlParameter("@SMR", SqlDbType.Decimal,9),
				new SqlParameter("@Linkman", SqlDbType.NVarChar,20),
				new SqlParameter("@LinkPhone", SqlDbType.VarChar,20),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
			parameters[0].Value = model.CustCode;
			parameters[1].Value = model.CustName;
			parameters[2].Value = model.CustType;
			parameters[3].Value = model.MachineModelId;
			parameters[4].Value = model.MachineModel;
			parameters[5].Value = model.MachineCode;
			parameters[6].Value = model.MachineState;
			parameters[7].Value = model.Part;
			parameters[8].Value = model.SMR;
			parameters[9].Value = model.Linkman;
			parameters[10].Value = model.LinkPhone;
			parameters[11].Value = model.FlagDel;
			parameters[12].Value = model.OperaName;
			parameters[13].Value = model.OperaTime;

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
		public int Update(SCZM.Model.Base.base_CustomerInformation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update base_CustomerInformation set ");
			strSql.Append("CustCode=@CustCode,");
			strSql.Append("CustName=@CustName,");
			strSql.Append("CustType=@CustType,");
			strSql.Append("MachineModelId=@MachineModelId,");
			strSql.Append("MachineModel=@MachineModel,");
			strSql.Append("MachineCode=@MachineCode,");
			strSql.Append("MachineState=@MachineState,");
			strSql.Append("Part=@Part,");
			strSql.Append("SMR=@SMR,");
			strSql.Append("Linkman=@Linkman,");
			strSql.Append("LinkPhone=@LinkPhone,");
			strSql.Append("FlagDel=@FlagDel,");
			strSql.Append("OperaName=@OperaName,");
			strSql.Append("OperaTime=@OperaTime");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@CustCode", SqlDbType.VarChar,50),
				new SqlParameter("@CustName", SqlDbType.NVarChar,50),
				new SqlParameter("@CustType", SqlDbType.NVarChar,20),
				new SqlParameter("@MachineModelId", SqlDbType.Int,4),
				new SqlParameter("@MachineModel", SqlDbType.NVarChar,20),
				new SqlParameter("@MachineCode", SqlDbType.NVarChar,20),
				new SqlParameter("@MachineState", SqlDbType.NVarChar,20),
				new SqlParameter("@Part", SqlDbType.NVarChar,20),
				new SqlParameter("@SMR", SqlDbType.Decimal,9),
				new SqlParameter("@Linkman", SqlDbType.NVarChar,20),
				new SqlParameter("@LinkPhone", SqlDbType.VarChar,20),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.CustCode;
			parameters[1].Value = model.CustName;
			parameters[2].Value = model.CustType;
			parameters[3].Value = model.MachineModelId;
			parameters[4].Value = model.MachineModel;
			parameters[5].Value = model.MachineCode;
			parameters[6].Value = model.MachineState;
			parameters[7].Value = model.Part;
			parameters[8].Value = model.SMR;
			parameters[9].Value = model.Linkman;
			parameters[10].Value = model.LinkPhone;
			parameters[11].Value = model.FlagDel;
			parameters[12].Value = model.OperaName;
			parameters[13].Value = model.OperaTime;
			parameters[14].Value = model.ID;

			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SCZM.Model.Base.base_CustomerInformation GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CustCode,CustName,CustType,MachineModelId,MachineModel,MachineCode,MachineState,Part,SMR,Linkman,LinkPhone,FlagDel,OperaName,OperaTime from base_CustomerInformation ");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			SCZM.Model.Base.base_CustomerInformation model=new SCZM.Model.Base.base_CustomerInformation();
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
		public SCZM.Model.Base.base_CustomerInformation DataRowToModel(DataRow row)
		{
			SCZM.Model.Base.base_CustomerInformation model=new SCZM.Model.Base.base_CustomerInformation();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CustCode"]!=null)
				{
					model.CustCode=row["CustCode"].ToString();
				}
				if(row["CustName"]!=null)
				{
					model.CustName=row["CustName"].ToString();
				}
				if(row["CustType"]!=null)
				{
					model.CustType=row["CustType"].ToString();
				}
				if(row["MachineModelId"]!=null && row["MachineModelId"].ToString()!="")
				{
					model.MachineModelId=int.Parse(row["MachineModelId"].ToString());
				}
				if(row["MachineModel"]!=null)
				{
					model.MachineModel=row["MachineModel"].ToString();
				}
				if(row["MachineCode"]!=null)
				{
					model.MachineCode=row["MachineCode"].ToString();
				}
				if(row["MachineState"]!=null)
				{
					model.MachineState=row["MachineState"].ToString();
				}
				if(row["Part"]!=null)
				{
					model.Part=row["Part"].ToString();
				}
				if(row["SMR"]!=null && row["SMR"].ToString()!="")
				{
					model.SMR=decimal.Parse(row["SMR"].ToString());
				}
				if(row["Linkman"]!=null)
				{
					model.Linkman=row["Linkman"].ToString();
				}
				if(row["LinkPhone"]!=null)
				{
					model.LinkPhone=row["LinkPhone"].ToString();
				}
				if(row["FlagDel"]!=null && row["FlagDel"].ToString()!="")
				{
					model.FlagDel=int.Parse(row["FlagDel"].ToString());
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
			strSql.Append("update base_CustomerInformation set FlagDel=1 ");
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
			strSql.Append("select a.ID,a.CustCode,a.CustName,a.CustType,a.MachineModelId,a.MachineModel,a.MachineCode,a.MachineState,a.Part,a.SMR,a.Linkman,a.LinkPhone,a.OperaName,a.OperaTime ");
			strSql.Append("FROM base_CustomerInformation a ");
			strSql.Append("where a.FlagDel=0");
			if (strWhere.Trim() != "")
			{
				strSql.Append(strWhere);
			}
            strSql.Append(" order by a.OperaTime desc");
			return DbHelperSQL.Query(strSql.ToString());
		}
        public DataSet GetComboList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 40  a.ID,a.CustCode,a.CustName,a.CustType,a.MachineModelId,a.MachineModel,a.MachineCode,a.MachineState,a.Part,a.SMR,a.Linkman,a.LinkPhone,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_CustomerInformation a ");
            strSql.Append("where a.FlagDel=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList_Import(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.CustCode,a.CustName,a.CustType,a.MachineModelId,a.MachineModel,a.MachineCode,a.MachineState,a.Part,a.SMR,a.Linkman,a.LinkPhone,a.OperaName,a.OperaTime ");
            strSql.Append("FROM base_CustomerInformation_Import a ");
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
			strSql.Append("select a.ID,a.CustCode,a.CustName,a.CustType,a.MachineModelId,a.MachineModel,a.MachineCode,a.MachineState,a.Part,a.SMR,a.Linkman,a.LinkPhone,a.OperaName,a.OperaTime ");
			strSql.Append("FROM base_CustomerInformation a ");
			strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}
        public int ImportData(string rows) {
            if (rows != "")
            {
                string strSql = "insert into base_CustomerInformation_Import(CustCode,CustName,MachineModel,MachineCode,Part,SMR,MachineState,Linkman,LinkPhone,CustType)values" + rows;
                return DbHelperSQL.ExecuteSql(strSql);
            }
            else {
                return 0;
            }
        }
        public void truncateTable() {
            string strSql = "truncate table base_CustomerInformation_Import";
            DbHelperSQL.ExecuteSql(strSql);
        }
        public int DelMachineCode_Null() {
            string strSql = "delete from base_CustomerInformation_Import where MachineCode is null;";
            return DbHelperSQL.ExecuteSql(strSql);
        }
        public int DelList_Import(string IdStr)
        {
            string strSql = "delete from base_CustomerInformation_Import where ID in(" + IdStr + ");";
            return DbHelperSQL.ExecuteSql(strSql);
        }
        public DataSet chooseMachineCode() {
            string strSql = "select * from base_CustomerInformation_Import where MachineCode in(select a.MachineCode from base_CustomerInformation_Import a where not exists(select MachineCode from base_CustomerInformation b where a.MachineCode=b.MachineCode)group by MachineCode having COUNT(1)>1) order by MachineCode";
            return DbHelperSQL.Query(strSql);
        }
        public int InsertInformation()
        { 
            string strSql=@"insert into base_CustomerInformation(CustCode,CustName,CustType,MachineModelId,MachineModel,MachineCode,MachineState,Part,SMR,Linkman,LinkPhone)
select CustCode,CustName,CustType,MachineModelId,MachineModel,MachineCode,MachineState,Part,SMR,Linkman,LinkPhone from base_CustomerInformation_Import a
where not exists(select MachineCode from base_CustomerInformation b where a.MachineCode=b.MachineCode)";
            return DbHelperSQL.ExecuteSql(strSql);
        }
        public DataSet GetMachineLevel_Undo() {
            string strSql = @"select distinct a.MachineModel from base_CustomerInformation a
left join base_MachineModel b on a.MachineModel=b.MachineModel
where b.MachineModel is null";
            return DbHelperSQL.Query(strSql);
        }
        public int setLevel(string rows) {
            if (rows != "")
            {
                string strSql = "insert into base_MachineModel(MachineModel,MachineLevel)values" + rows;
                return DbHelperSQL.ExecuteSql(strSql);
            }
            else
            {
                return 0;
            }
        }
        public int setMachineId()
        {
            string strSql = "update a set a.MachineModelId=b.ID from base_CustomerInformation a,base_MachineModel b where a.MachineModel=b.MachineModel and a.MachineModelId is null";
           return DbHelperSQL.ExecuteSql(strSql);
        }
		#endregion  扩展方法
	}
}


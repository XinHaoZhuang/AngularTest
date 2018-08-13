using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Proj
{
	/// <summary>
	/// ���ݷ�����proj_SalesInvoice��
	/// </summary>
	public partial class proj_SalesInvoice
	{
		public proj_SalesInvoice()
		{}
		#region  ��������
		/// <summary>
		/// ĳ���Ƿ����
		/// </summary>
		public bool Exists(string FieldName,string FieldValue,int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from proj_SalesInvoice");
			strSql.Append(" where FlagDel=0 and  " + FieldName + "=@" + FieldName + " and ID<>@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@" + FieldName, SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = FieldValue;
			parameters[1].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(SCZM.Model.Proj.proj_SalesInvoice model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into proj_SalesInvoice(");
			strSql.Append("ContractId,InvoiceCode,InvoiceDate,InvoiceNat,TaxRate,Memo,AttachmentId_Invoice,FlagDel,OperaId,OperaName,OperaTime)");
			strSql.Append(" values (");
			strSql.Append("@ContractId,@InvoiceCode,@InvoiceDate,@InvoiceNat,@TaxRate,@Memo,@AttachmentId_Invoice,@FlagDel,@OperaId,@OperaName,@OperaTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
				new SqlParameter("@ContractId", SqlDbType.Int,4),
				new SqlParameter("@InvoiceCode", SqlDbType.VarChar,10),
				new SqlParameter("@InvoiceDate", SqlDbType.SmallDateTime),
				new SqlParameter("@InvoiceNat", SqlDbType.Decimal,9),
                new SqlParameter("@TaxRate",SqlDbType.Decimal,5),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@AttachmentId_Invoice", SqlDbType.VarChar,50),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ContractId;
			parameters[1].Value = model.InvoiceCode;
			parameters[2].Value = model.InvoiceDate;
			parameters[3].Value = model.InvoiceNat;
            parameters[4].Value = model.TaxRate;
			parameters[5].Value = model.Memo;
			parameters[6].Value = model.AttachmentId_Invoice;
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
		/// ����һ������
		/// </summary>
		public int Update(SCZM.Model.Proj.proj_SalesInvoice model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update proj_SalesInvoice set ");
			strSql.Append("ContractId=@ContractId,");
			strSql.Append("InvoiceCode=@InvoiceCode,");
			strSql.Append("InvoiceDate=@InvoiceDate,");
			strSql.Append("InvoiceNat=@InvoiceNat,");
            strSql.Append("TaxRate=@TaxRate,");
			strSql.Append("Memo=@Memo,");
			strSql.Append("AttachmentId_Invoice=@AttachmentId_Invoice,");
			strSql.Append("FlagDel=@FlagDel,");
			strSql.Append("OperaId=@OperaId,");
			strSql.Append("OperaName=@OperaName,");
			strSql.Append("OperaTime=@OperaTime");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ContractId", SqlDbType.Int,4),
				new SqlParameter("@InvoiceCode", SqlDbType.VarChar,10),
				new SqlParameter("@InvoiceDate", SqlDbType.SmallDateTime),
				new SqlParameter("@InvoiceNat", SqlDbType.Decimal,9),
                new SqlParameter("@TaxRate",SqlDbType.Decimal,5),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@AttachmentId_Invoice", SqlDbType.VarChar,50),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ContractId;
			parameters[1].Value = model.InvoiceCode;
			parameters[2].Value = model.InvoiceDate;
			parameters[3].Value = model.InvoiceNat;
            parameters[4].Value = model.TaxRate;
			parameters[5].Value = model.Memo;
			parameters[6].Value = model.AttachmentId_Invoice;
			parameters[7].Value = model.FlagDel;
			parameters[8].Value = model.OperaId;
			parameters[9].Value = model.OperaName;
			parameters[10].Value = model.OperaTime;
			parameters[11].Value = model.ID;

			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			return rows;
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public SCZM.Model.Proj.proj_SalesInvoice GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,ContractId,InvoiceCode,InvoiceDate,InvoiceNat,TaxRate,Memo,AttachmentId_Invoice,FlagDel,OperaId,OperaName,OperaTime from proj_SalesInvoice ");
			strSql.Append(" where FlagDel=0 and ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			SCZM.Model.Proj.proj_SalesInvoice model=new SCZM.Model.Proj.proj_SalesInvoice();
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
		/// �õ�һ������ʵ�� �ӷ��� ��DataRow��
		/// </summary>
		public SCZM.Model.Proj.proj_SalesInvoice DataRowToModel(DataRow row)
		{
			SCZM.Model.Proj.proj_SalesInvoice model=new SCZM.Model.Proj.proj_SalesInvoice();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["ContractId"]!=null && row["ContractId"].ToString()!="")
				{
					model.ContractId=int.Parse(row["ContractId"].ToString());
				}
				if(row["InvoiceCode"]!=null)
				{
					model.InvoiceCode=row["InvoiceCode"].ToString();
				}
				if(row["InvoiceDate"]!=null && row["InvoiceDate"].ToString()!="")
				{
					model.InvoiceDate=DateTime.Parse(row["InvoiceDate"].ToString());
				}
				if(row["InvoiceNat"]!=null && row["InvoiceNat"].ToString()!="")
				{
					model.InvoiceNat=decimal.Parse(row["InvoiceNat"].ToString());
				}
                if (row["TaxRate"] != null && row["TaxRate"].ToString() != "") {
                    model.TaxRate = decimal.Parse(row["TaxRate"].ToString());
                }
				if(row["Memo"]!=null)
				{
					model.Memo=row["Memo"].ToString();
				}
				if(row["AttachmentId_Invoice"]!=null)
				{
					model.AttachmentId_Invoice=row["AttachmentId_Invoice"].ToString();
				}
				if(row["FlagDel"]!=null && row["FlagDel"].ToString()!="")
				{
					if((row["FlagDel"].ToString()=="1")||(row["FlagDel"].ToString().ToLower()=="true"))
					{
						model.FlagDel=true;
					}
					else
					{
						model.FlagDel=false;
					}
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

		#endregion  ��������
		#region  ��չ����
		/// <summary>
		/// ����ɾ������
		/// </summary>
		public int DeleteList(string IDList)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update proj_SalesInvoice set FlagDel=1 ");
			strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows;
		}

		/// <summary>
		/// ��������б� ͨ��Where����
		/// </summary>
		public DataSet GetList(string strWhere,int operaId)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select a.ID,a.InvoiceCode,c.ProjName,b.ContractCode,d.CustName,a.InvoiceDate,a.InvoiceNat,a.TaxRate,a.OperaName,a.OperaTime ");
			strSql.Append("FROM proj_SalesInvoice a ");
            strSql.Append("left join proj_Contract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Customer d on c.CustId=d.ID ");
            strSql.Append("where a.FlagDel=0 and c.ProjManagerId in(select e.CtrlPerId from v_sys_PersonCtrl e where e.PerId=" + operaId.ToString() + ") ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(strWhere);
			}
            strSql.Append(" order by a.OperaTime desc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���������ϸ ͨ��ID
		/// </summary>
		public DataSet GetDetail(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select a.ContractId,c.ProjName,d.CustName,a.InvoiceCode,a.InvoiceDate,a.InvoiceNat,a.TaxRate,a.Memo,a.AttachmentId_Invoice,a.OperaName,a.OperaTime,b.ContractCode,e.CompanyName,b.ContractDate,b.ContractNat,f.PerName as SvcManagerName,g.PerName as ProjManagerName ");
			strSql.Append("FROM proj_SalesInvoice a ");
            strSql.Append("left join proj_Contract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Customer d on c.CustId=d.ID ");
            strSql.Append("left join base_Company e on b.CompanyId=d.ID ");
            strSql.Append("left join sys_Person f on c.SvcManagerId=f.ID ");
            strSql.Append("left join sys_Person g on c.ProjManagerId=g.ID  ");
			strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
			SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			string AttachmentId = "";
			if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
			{
				AttachmentId = (ds.Tables[0].Rows[0]["AttachmentId_Invoice"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_Invoice"].ToString() + ",") 
					;
			}
			DataTable dt = new System.sys_Attachment().GetAttachment(AttachmentId);
			dt.TableName = "attachment";
			ds.Tables.Add(dt.Copy());
			return ds;
		}
		#endregion  ��չ����
	}
}


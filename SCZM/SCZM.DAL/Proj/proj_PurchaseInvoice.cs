using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Proj
{
    /// <summary>
    /// 数据访问类proj_PurchaseInvoice。
    /// </summary>
    public partial class proj_PurchaseInvoice
    {
        public proj_PurchaseInvoice()
        { }
        #region  基本方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCZM.Model.Proj.proj_PurchaseInvoice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into proj_PurchaseInvoice(");
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
        public int Update(SCZM.Model.Proj.proj_PurchaseInvoice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update proj_PurchaseInvoice set ");
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

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Proj.proj_PurchaseInvoice GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ContractId,InvoiceCode,InvoiceDate,InvoiceNat,TaxRate,Memo,AttachmentId_Invoice,FlagDel,OperaId,OperaName,OperaTime from proj_PurchaseInvoice ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Proj.proj_PurchaseInvoice model = new SCZM.Model.Proj.proj_PurchaseInvoice();
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
        public SCZM.Model.Proj.proj_PurchaseInvoice DataRowToModel(DataRow row)
        {
            SCZM.Model.Proj.proj_PurchaseInvoice model = new SCZM.Model.Proj.proj_PurchaseInvoice();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ContractId"] != null && row["ContractId"].ToString() != "")
                {
                    model.ContractId = int.Parse(row["ContractId"].ToString());
                }
                if (row["InvoiceCode"] != null)
                {
                    model.InvoiceCode = row["InvoiceCode"].ToString();
                }
                if (row["InvoiceDate"] != null && row["InvoiceDate"].ToString() != "")
                {
                    model.InvoiceDate = DateTime.Parse(row["InvoiceDate"].ToString());
                }
                if (row["InvoiceNat"] != null && row["InvoiceNat"].ToString() != "")
                {
                    model.InvoiceNat = decimal.Parse(row["InvoiceNat"].ToString());
                }
                if (row["TaxRate"] != null && row["TaxRate"].ToString() != "") {
                    model.TaxRate = decimal.Parse(row["TaxRate"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["AttachmentId_Invoice"] != null)
                {
                    model.AttachmentId_Invoice = row["AttachmentId_Invoice"].ToString();
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
            strSql.Append("update proj_PurchaseInvoice set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere, int operaId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,c.ProjName,d.PartnerName,a.InvoiceCode,a.InvoiceDate,a.InvoiceNat,a.TaxRate,a.OperaName,a.OperaTime ");
            strSql.Append("FROM proj_PurchaseInvoice a ");
            strSql.Append("left join proj_PartnerContract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Partner d on b.PartnerId=d.ID ");
            strSql.Append("where a.FlagDel=0 and c.ProjManagerId in(select e.CtrlPerId from v_sys_PersonCtrl e where e.PerId=" + operaId.ToString() + ")");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ContractId,c.ProjName,d.PartnerName,b.ContractCode,e.CompanyName,b.ContractDate,b.ContractNat,a.InvoiceCode,a.InvoiceDate,a.InvoiceNat,a.TaxRate,a.Memo,a.AttachmentId_Invoice,f.PerName as SvcManagerName,g.PerName as ProjManagerName,a.OperaName,a.OperaTime ");
            strSql.Append("FROM proj_PurchaseInvoice a ");
            strSql.Append("left join proj_PartnerContract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Partner d on b.PartnerId=d.ID ");
            strSql.Append("left join base_Company e on b.CompanyId=e.ID ");
            strSql.Append("left join sys_Person f on c.SvcManagerId=f.ID ");
            strSql.Append("left join sys_Person g on c.ProjManagerId=g.ID ");
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
        /// <summary>
        /// 获得参照数据列表
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="operaId">操作人</param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere, int operaId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as InvoiceId,a.InvoiceCode,a.InvoiceNat,a.TaxRate,a.ContractId,a.InvoiceCode+'('+dbo.ClearZero(cast(InvoiceNat as varchar))+'元)' as InvoiceList,a.FlagDel  ");
            strSql.Append("FROM proj_PurchaseInvoice a ");
            strSql.Append("left join proj_PartnerContract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("where a.FlagDel=0 and b.BillState=1 and c.ProjManagerId in(select e.CtrlPerId from v_sys_PersonCtrl e where e.PerId=" + operaId.ToString() + ") ");
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


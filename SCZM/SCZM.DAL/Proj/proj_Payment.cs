using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;
namespace SCZM.DAL.Proj
{
    /// <summary>
    /// 数据访问类proj_Payment。
    /// </summary>
    public partial class proj_Payment
    {
        public proj_Payment()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from proj_Payment");
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
        public int Add(SCZM.Model.Proj.proj_Payment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into proj_Payment(");
            strSql.Append("ContractId,PayDate,PayNat,FlagInvoice,InvoiceId,InvoiceCode,Memo,BillSign,AuditNextId,BillState,FlagDel,DepId,OperaId,OperaName,OperaTime,AuditEndTime)");
            strSql.Append(" values (");
            strSql.Append("@ContractId,@PayDate,@PayNat,@FlagInvoice,@InvoiceId,@InvoiceCode,@Memo,@BillSign,@AuditNextId,@BillState,@FlagDel,@DepId,@OperaId,@OperaName,@OperaTime,@AuditEndTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@ContractId", SqlDbType.Int,4),
				new SqlParameter("@PayDate", SqlDbType.SmallDateTime),
				new SqlParameter("@PayNat", SqlDbType.Decimal,9),
				new SqlParameter("@FlagInvoice", SqlDbType.Int,4),
				new SqlParameter("@InvoiceId", SqlDbType.VarChar,50),
				new SqlParameter("@InvoiceCode", SqlDbType.VarChar,200),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@BillSign", SqlDbType.VarChar,50),
				new SqlParameter("@AuditNextId", SqlDbType.Int,4),
				new SqlParameter("@BillState", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@DepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@AuditEndTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ContractId;
            parameters[1].Value = model.PayDate;
            parameters[2].Value = model.PayNat;
            parameters[3].Value = model.FlagInvoice;
            parameters[4].Value = model.InvoiceId;
            parameters[5].Value = model.InvoiceCode;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.BillSign;
            parameters[8].Value = model.AuditNextId;
            parameters[9].Value = model.BillState;
            parameters[10].Value = model.FlagDel;
            parameters[11].Value = model.DepId;
            parameters[12].Value = model.OperaId;
            parameters[13].Value = model.OperaName;
            parameters[14].Value = model.OperaTime;
            parameters[15].Value = model.AuditEndTime;

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
        public int Update(SCZM.Model.Proj.proj_Payment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update proj_Payment set ");
            strSql.Append("ContractId=@ContractId,");
            strSql.Append("PayDate=@PayDate,");
            strSql.Append("PayNat=@PayNat,");
            strSql.Append("FlagInvoice=@FlagInvoice,");
            strSql.Append("InvoiceId=@InvoiceId,");
            strSql.Append("InvoiceCode=@InvoiceCode,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("BillSign=@BillSign,");
            strSql.Append("AuditNextId=@AuditNextId,");
            strSql.Append("BillState=@BillState,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("DepId=@DepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("AuditEndTime=@AuditEndTime");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ContractId", SqlDbType.Int,4),
				new SqlParameter("@PayDate", SqlDbType.SmallDateTime),
				new SqlParameter("@PayNat", SqlDbType.Decimal,9),
				new SqlParameter("@FlagInvoice", SqlDbType.Int,4),
				new SqlParameter("@InvoiceId", SqlDbType.VarChar,50),
				new SqlParameter("@InvoiceCode", SqlDbType.VarChar,200),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@BillSign", SqlDbType.VarChar,50),
				new SqlParameter("@AuditNextId", SqlDbType.Int,4),
				new SqlParameter("@BillState", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@DepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@AuditEndTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.ContractId;
            parameters[1].Value = model.PayDate;
            parameters[2].Value = model.PayNat;
            parameters[3].Value = model.FlagInvoice;
            parameters[4].Value = model.InvoiceId;
            parameters[5].Value = model.InvoiceCode;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.BillSign;
            parameters[8].Value = model.AuditNextId;
            parameters[9].Value = model.BillState;
            parameters[10].Value = model.FlagDel;
            parameters[11].Value = model.DepId;
            parameters[12].Value = model.OperaId;
            parameters[13].Value = model.OperaName;
            parameters[14].Value = model.OperaTime;
            parameters[15].Value = model.AuditEndTime;
            parameters[16].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SCZM.Model.Proj.proj_Payment GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ContractId,PayDate,PayNat,FlagInvoice,InvoiceId,InvoiceCode,Memo,BillSign,AuditNextId,BillState,FlagDel,DepId,OperaId,OperaName,OperaTime,AuditEndTime from proj_Payment ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Proj.proj_Payment model = new SCZM.Model.Proj.proj_Payment();
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
        public SCZM.Model.Proj.proj_Payment DataRowToModel(DataRow row)
        {
            SCZM.Model.Proj.proj_Payment model = new SCZM.Model.Proj.proj_Payment();
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
                if (row["PayDate"] != null && row["PayDate"].ToString() != "")
                {
                    model.PayDate = DateTime.Parse(row["PayDate"].ToString());
                }
                if (row["PayNat"] != null && row["PayNat"].ToString() != "")
                {
                    model.PayNat = decimal.Parse(row["PayNat"].ToString());
                }
                if (row["FlagInvoice"] != null && row["FlagInvoice"].ToString() != "")
                {
                    model.FlagInvoice = int.Parse(row["FlagInvoice"].ToString());
                }
                if (row["InvoiceId"] != null)
                {
                    model.InvoiceId = row["InvoiceId"].ToString();
                }
                if (row["InvoiceCode"] != null)
                {
                    model.InvoiceCode = row["InvoiceCode"].ToString();
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["BillSign"] != null)
                {
                    model.BillSign = row["BillSign"].ToString();
                }
                if (row["AuditNextId"] != null && row["AuditNextId"].ToString() != "")
                {
                    model.AuditNextId = int.Parse(row["AuditNextId"].ToString());
                }
                if (row["BillState"] != null && row["BillState"].ToString() != "")
                {
                    model.BillState = int.Parse(row["BillState"].ToString());
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
                if (row["DepId"] != null && row["DepId"].ToString() != "")
                {
                    model.DepId = int.Parse(row["DepId"].ToString());
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
                if (row["AuditEndTime"] != null && row["AuditEndTime"].ToString() != "")
                {
                    model.AuditEndTime = DateTime.Parse(row["AuditEndTime"].ToString());
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
            strSql.Append("update proj_Payment set FlagDel=1 ");
            strSql.Append(" where FlagDel=0 and ID in(" + IDList + ")");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 获取申请页面的数据列表 通过存储过程
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="perId">查询人ID</param>
        /// <param name="billState">单据状态0全部 1审批完成 2 审批未完成 3审批中 4审批不同意 5未提交 6已提交</param>
        /// <returns></returns>
        public DataSet GetApplyList(string strWhere, int perId, int billState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ContractId,a.PayDate,a.PayNat,a.FlagInvoice,a.BillState,a.OperaName,a.OperaTime,b.ContractCode,c.ProjName,d.PartnerName,e.PartnerTypeName ");
            strSql.Append("FROM proj_Payment a ");
            strSql.Append("left join proj_PartnerContract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Partner d on b.PartnerId=d.ID ");
            strSql.Append("left join base_PartnerType e on d.PartnerTypeId=e.ID ");
            string strOrder = "a.OperaTime desc";
            string procName = "p_sys_GetApplyList";
            SqlParameter[] parameters = {
				new SqlParameter("@strSql", SqlDbType.VarChar),
				new SqlParameter("@strWhere", SqlDbType.VarChar),
				new SqlParameter("@strOrder", SqlDbType.VarChar),
				new SqlParameter("@perId", SqlDbType.Int,4),
				new SqlParameter("@billState", SqlDbType.Int,4)};
            parameters[0].Value = strSql.ToString();
            parameters[1].Value = strWhere;
            parameters[2].Value = strOrder;
            parameters[3].Value = perId;
            parameters[4].Value = billState;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }

        /// <summary>
        /// 获取审核页面的数据列表 通过存储过程
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="perId">查询人ID</param>
        /// <param name="billState">单据状态0全部 1审批完成 2 审批未完成 3审批中 4审批不同意 5未提交 6已提交</param>
        /// <param name="auditState">审核状态 0全部 1已审核 2 未审核	3 审批同意	4 审批不同意</param>
        /// <returns></returns>
        public DataSet GetAuditList(string strWhere, int perId, int billState, int auditState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ContractId,a.PayDate,a.PayNat,a.FlagInvoice,a.BillState,a.OperaName,a.OperaTime,b.ContractCode,c.ProjName,d.PartnerName,e.PartnerTypeName ");
            strSql.Append("FROM proj_Payment a ");
            strSql.Append("left join proj_PartnerContract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Partner d on b.PartnerId=d.ID ");
            strSql.Append("left join base_PartnerType e on d.PartnerTypeId=e.ID ");
            string strOrder = "a.OperaTime desc";
            string procName = "p_sys_GetAuditList";
            SqlParameter[] parameters = {
				new SqlParameter("@strSql", SqlDbType.VarChar),
				new SqlParameter("@strWhere", SqlDbType.VarChar),
				new SqlParameter("@strOrder", SqlDbType.VarChar),
				new SqlParameter("@perId", SqlDbType.Int,4),
				new SqlParameter("@billState", SqlDbType.Int,4),
				new SqlParameter("@auditState", SqlDbType.Int,4)};
            parameters[0].Value = strSql.ToString();
            parameters[1].Value = strWhere;
            parameters[2].Value = strOrder;
            parameters[3].Value = perId;
            parameters[4].Value = billState;
            parameters[5].Value = auditState;
            return DbHelperSQL.RunProcedure(procName, parameters, "dt");
        }

        /// <summary>
        /// 获得数据明细 通过ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ContractId,a.PayDate,a.PayNat,a.FlagInvoice,a.InvoiceId,a.InvoiceCode,a.Memo,a.BillState,a.BillSign,a.OperaName,a.OperaTime,b.ProjId,b.ContractCode,g.PartnerName,b.ContractDate,b.ContractNat,c.ProjName,d.CompanyName,e.PerName as ProjManagerName,f.PerName as SvcManagerName ");
            strSql.Append("FROM proj_Payment a ");
            strSql.Append("left join proj_PartnerContract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Partner g on g.ID=b.PartnerId ");
            strSql.Append("left join base_Company d on b.CompanyId=d.ID ");
            strSql.Append("left join sys_Person e on c.ProjManagerId=e.ID ");
            strSql.Append("left join sys_Person f on c.SvcManagerId=f.ID ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获取付款审批单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet GetPaymentList(int ID) {
            string procName = "p_proj_GetPaymentList";
            SqlParameter[] parameters = { 
                        new SqlParameter("@ID",SqlDbType.Int,4)
                                        };
            parameters[0].Value = ID;
            return DbHelperSQL.RunProcedure(procName,parameters,"dt");
        }
        /// <summary>
        /// 付款审批单所需的付款单位信息列表
        /// </summary>
        /// <param name="ID">项目id</param>
        /// <returns></returns>
        public DataSet GetPrintList(int ID) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.ContractId,a.PayDate,a.PayNat,a.FlagInvoice,a.BillState,a.OperaName,a.OperaTime,b.ContractCode,c.ProjName,d.PartnerName,e.PartnerTypeName ");
            strSql.Append("FROM proj_Payment a ");
            strSql.Append("left join proj_PartnerContract b on a.ContractId=b.ID ");
            strSql.Append("left join proj_Project c on b.ProjId=c.ID ");
            strSql.Append("left join base_Partner d on b.PartnerId=d.ID ");
            strSql.Append("left join base_PartnerType e on d.PartnerTypeId=e.ID ");
            strSql.Append("where c.ID=@ID and a.FlagDel=0 and a.BillState=1 ");
            //strSql.Append("where c.ID=@ID and a.FlagDel=0 and (a.BillState=1 or a.BillState=3) ");
            strSql.Append("order by a.OperaTime desc ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4),
                                        };
            parameters[0].Value = ID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion  扩展方法
    }
}


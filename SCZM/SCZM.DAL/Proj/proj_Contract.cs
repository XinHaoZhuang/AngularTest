using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用
namespace SCZM.DAL.Proj
{
    /// <summary>
    /// 数据访问类proj_Contract。
    /// </summary>
    public partial class proj_Contract
    {
        public proj_Contract()
        { }
        #region  基本方法
        
        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(SCZM.Model.Proj.proj_Contract model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into proj_Contract(");
            strSql.Append("ContractCode,ProjId,CompanyId,ContractDate,ContractNat,ProjType,StartDate,CompleteDate,Timescale,Memo,AttachmentId_Contract,AttachmentId_ControlCard,BillSign,AuditNextId,BillState,FlagDel,DepId,OperaId,OperaName,OperaTime,AuditEndTime)");
            strSql.Append(" values (");
            strSql.Append("@ContractCode,@ProjId,@CompanyId,@ContractDate,@ContractNat,@ProjType,@StartDate,@CompleteDate,@Timescale,@Memo,@AttachmentId_Contract,@AttachmentId_ControlCard,@BillSign,@AuditNextId,@BillState,@FlagDel,@DepId,@OperaId,@OperaName,@OperaTime,@AuditEndTime)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@ContractCode", SqlDbType.VarChar,30),
				new SqlParameter("@ProjId", SqlDbType.Int,4),
				new SqlParameter("@CompanyId", SqlDbType.Int,4),
				new SqlParameter("@ContractDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ContractNat", SqlDbType.Decimal,9),
				new SqlParameter("@ProjType", SqlDbType.Int,4),
				new SqlParameter("@StartDate", SqlDbType.SmallDateTime),
				new SqlParameter("@CompleteDate", SqlDbType.SmallDateTime),
				new SqlParameter("@Timescale", SqlDbType.Int,4),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@AttachmentId_Contract", SqlDbType.VarChar,50),
				new SqlParameter("@AttachmentId_ControlCard", SqlDbType.VarChar,50),
				new SqlParameter("@BillSign", SqlDbType.VarChar,50),
				new SqlParameter("@AuditNextId", SqlDbType.Int,4),
				new SqlParameter("@BillState", SqlDbType.Int,4),
				new SqlParameter("@FlagDel", SqlDbType.Bit,1),
				new SqlParameter("@DepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,20),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
				new SqlParameter("@AuditEndTime", SqlDbType.DateTime),
				new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.ContractCode;
            parameters[1].Value = model.ProjId;
            parameters[2].Value = model.CompanyId;
            parameters[3].Value = model.ContractDate;
            parameters[4].Value = model.ContractNat;
            parameters[5].Value = model.ProjType;
            parameters[6].Value = model.StartDate;
            parameters[7].Value = model.CompleteDate;
            parameters[8].Value = model.Timescale;
            parameters[9].Value = model.Memo;
            parameters[10].Value = model.AttachmentId_Contract;
            parameters[11].Value = model.AttachmentId_ControlCard;
            parameters[12].Value = model.BillSign;
            parameters[13].Value = model.AuditNextId;
            parameters[14].Value = model.BillState;
            parameters[15].Value = model.FlagDel;
            parameters[16].Value = model.DepId;
            parameters[17].Value = model.OperaId;
            parameters[18].Value = model.OperaName;
            parameters[19].Value = model.OperaTime;
            parameters[20].Value = model.AuditEndTime;
            parameters[21].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2;
            if (model.proj_ContractDetails != null)
            {
                foreach (SCZM.Model.Proj.proj_ContractDetail models in model.proj_ContractDetails)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into proj_ContractDetail(");
                    strSql2.Append("ContractId,Progress,ReceiveDate,ReceiveRate,ReceiveNat,Memo,FlagDel)");
                    strSql2.Append(" values (");
                    strSql2.Append("@ContractId,@Progress,@ReceiveDate,@ReceiveRate,@ReceiveNat,@Memo,@FlagDel)");
                    SqlParameter[] parameters2 = {
						new SqlParameter("@ContractId", SqlDbType.Int,4),
						new SqlParameter("@Progress", SqlDbType.NVarChar,20),
						new SqlParameter("@ReceiveDate", SqlDbType.SmallDateTime),
						new SqlParameter("@ReceiveRate", SqlDbType.Decimal,9),
						new SqlParameter("@ReceiveNat", SqlDbType.Decimal,9),
						new SqlParameter("@Memo", SqlDbType.NVarChar,100),
						new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.Progress;
                    parameters2[2].Value = models.ReceiveDate;
                    parameters2[3].Value = models.ReceiveRate;
                    parameters2[4].Value = models.ReceiveNat;
                    parameters2[5].Value = models.Memo;
                    parameters2[6].Value = models.FlagDel;

                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[21].Value;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Proj.proj_Contract model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update proj_Contract set ");
            strSql.Append("ContractCode=@ContractCode,");
            strSql.Append("ProjId=@ProjId,");
            strSql.Append("CompanyId=@CompanyId,");
            strSql.Append("ContractDate=@ContractDate,");
            strSql.Append("ContractNat=@ContractNat,");
            strSql.Append("ProjType=@ProjType,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("CompleteDate=@CompleteDate,");
            strSql.Append("Timescale=@Timescale,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("AttachmentId_Contract=@AttachmentId_Contract,");
            strSql.Append("AttachmentId_ControlCard=@AttachmentId_ControlCard,");
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
				new SqlParameter("@ContractCode", SqlDbType.VarChar,30),
				new SqlParameter("@ProjId", SqlDbType.Int,4),
				new SqlParameter("@CompanyId", SqlDbType.Int,4),
				new SqlParameter("@ContractDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ContractNat", SqlDbType.Decimal,9),
				new SqlParameter("@ProjType", SqlDbType.Int,4),
				new SqlParameter("@StartDate", SqlDbType.SmallDateTime),
				new SqlParameter("@CompleteDate", SqlDbType.SmallDateTime),
				new SqlParameter("@Timescale", SqlDbType.Int,4),
				new SqlParameter("@Memo", SqlDbType.NVarChar,200),
				new SqlParameter("@AttachmentId_Contract", SqlDbType.VarChar,50),
				new SqlParameter("@AttachmentId_ControlCard", SqlDbType.VarChar,50),
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
            parameters[0].Value = model.ContractCode;
            parameters[1].Value = model.ProjId;
            parameters[2].Value = model.CompanyId;
            parameters[3].Value = model.ContractDate;
            parameters[4].Value = model.ContractNat;
            parameters[5].Value = model.ProjType;
            parameters[6].Value = model.StartDate;
            parameters[7].Value = model.CompleteDate;
            parameters[8].Value = model.Timescale;
            parameters[9].Value = model.Memo;
            parameters[10].Value = model.AttachmentId_Contract;
            parameters[11].Value = model.AttachmentId_ControlCard;
            parameters[12].Value = model.BillSign;
            parameters[13].Value = model.AuditNextId;
            parameters[14].Value = model.BillState;
            parameters[15].Value = model.FlagDel;
            parameters[16].Value = model.DepId;
            parameters[17].Value = model.OperaId;
            parameters[18].Value = model.OperaName;
            parameters[19].Value = model.OperaTime;
            parameters[20].Value = model.AuditEndTime;
            parameters[21].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from  proj_ContractDetail where ContractId=@ContractId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@ContractId", SqlDbType.Int,4)};
            parameters2[0].Value = model.ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3;
            if (model.proj_ContractDetails != null)
            {
                foreach (SCZM.Model.Proj.proj_ContractDetail models in model.proj_ContractDetails)
                {
                    strSql3 = new StringBuilder();
                    strSql3.Append("insert into proj_ContractDetail(");
                    strSql3.Append("ContractId,Progress,ReceiveDate,ReceiveRate,ReceiveNat,Memo,FlagDel)");
                    strSql3.Append(" values (");
                    strSql3.Append("@ContractId,@Progress,@ReceiveDate,@ReceiveRate,@ReceiveNat,@Memo,@FlagDel)");
                    SqlParameter[] parameters3 = {
						new SqlParameter("@ContractId", SqlDbType.Int,4),
						new SqlParameter("@Progress", SqlDbType.NVarChar,20),
						new SqlParameter("@ReceiveDate", SqlDbType.SmallDateTime),
						new SqlParameter("@ReceiveRate", SqlDbType.Decimal,9),
						new SqlParameter("@ReceiveNat", SqlDbType.Decimal,9),
						new SqlParameter("@Memo", SqlDbType.NVarChar,100),
						new SqlParameter("@FlagDel", SqlDbType.Bit,1)};
                    parameters3[0].Value = model.ID;
                    parameters3[1].Value = models.Progress;
                    parameters3[2].Value = models.ReceiveDate;
                    parameters3[3].Value = models.ReceiveRate;
                    parameters3[4].Value = models.ReceiveNat;
                    parameters3[5].Value = models.Memo;
                    parameters3[6].Value = models.FlagDel;

                    cmd = new CommandInfo(strSql3.ToString(), parameters3);
                    sqllist.Add(cmd);
                }
            }
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体 主表
        /// </summary>
        public SCZM.Model.Proj.proj_Contract GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ContractCode,ProjId,CompanyId,ContractDate,ContractNat,ProjType,StartDate,CompleteDate,Timescale,Memo,AttachmentId_Contract,AttachmentId_ControlCard,BillSign,AuditNextId,BillState,FlagDel,DepId,OperaId,OperaName,OperaTime,AuditEndTime from proj_Contract ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Proj.proj_Contract model = new SCZM.Model.Proj.proj_Contract();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                model = DataRowToModel(ds.Tables[0].Rows[0]);
                #endregion  父表信息end

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体 子方法 从DataRow中
        /// </summary>
        public SCZM.Model.Proj.proj_Contract DataRowToModel(DataRow row)
        {
            SCZM.Model.Proj.proj_Contract model = new SCZM.Model.Proj.proj_Contract();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ContractCode"] != null)
                {
                    model.ContractCode = row["ContractCode"].ToString();
                }
                if (row["ProjId"] != null && row["ProjId"].ToString() != "")
                {
                    model.ProjId = int.Parse(row["ProjId"].ToString());
                }
                if (row["CompanyId"] != null && row["CompanyId"].ToString() != "")
                {
                    model.CompanyId = int.Parse(row["CompanyId"].ToString());
                }
                if (row["ContractDate"] != null && row["ContractDate"].ToString() != "")
                {
                    model.ContractDate = DateTime.Parse(row["ContractDate"].ToString());
                }
                if (row["ContractNat"] != null && row["ContractNat"].ToString() != "")
                {
                    model.ContractNat = decimal.Parse(row["ContractNat"].ToString());
                }
                if (row["ProjType"] != null && row["ProjType"].ToString() != "")
                {
                    model.ProjType = int.Parse(row["ProjType"].ToString());
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["CompleteDate"] != null && row["CompleteDate"].ToString() != "")
                {
                    model.CompleteDate = DateTime.Parse(row["CompleteDate"].ToString());
                }
                if (row["Timescale"] != null && row["Timescale"].ToString() != "")
                {
                    model.Timescale = int.Parse(row["Timescale"].ToString());
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["AttachmentId_Contract"] != null)
                {
                    model.AttachmentId_Contract = row["AttachmentId_Contract"].ToString();
                }
                if (row["AttachmentId_ControlCard"] != null)
                {
                    model.AttachmentId_ControlCard = row["AttachmentId_ControlCard"].ToString();
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
            List<string> sqllist = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update proj_ContractDetail set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ContractId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update proj_Contract set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
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
            strSql.Append("select a.ID,a.ContractCode,a.ProjType,a.ContractDate,a.ContractNat,a.BillState,a.OperaName,a.OperaTime,b.ProjName ");
            strSql.Append("FROM proj_Contract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            //strSql.Append("left join base_Customer c on b.CustId=c.ID ");
            strSql.Append("left join sys_Person d on b.SvcManagerId=d.ID ");
            strSql.Append("left join sys_Person e on b.ProjManagerId=e.ID ");
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
            strSql.Append("select a.ID,a.ContractCode,a.ProjType,a.ContractDate,a.ContractNat,a.BillState,a.OperaName,a.OperaTime,b.ProjName ");
            strSql.Append("FROM proj_Contract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            //strSql.Append("left join base_Customer c on b.CustId=c.ID ");
            strSql.Append("left join sys_Person d on b.SvcManagerId=d.ID ");
            strSql.Append("left join sys_Person e on b.ProjManagerId=e.ID ");
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
            strSql.Append("select a.ID,a.ContractCode,a.ProjId,a.CompanyId,a.ProjType,a.ContractDate,a.ContractNat,a.StartDate,a.CompleteDate,a.Timescale,a.AttachmentId_Contract,a.AttachmentId_ControlCard,a.Memo,a.BillSign,a.BillState,a.OperaName,a.OperaTime,b.ProjName,c.CustName,d.PerName as SvcManagerName,e.PerName as ProjManagerName,f.CompanyName ");
            strSql.Append("FROM proj_Contract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            strSql.Append("left join base_Customer c on b.CustId=c.ID ");
            strSql.Append("left join sys_Person d on b.SvcManagerId=d.ID ");
            strSql.Append("left join sys_Person e on b.ProjManagerId=e.ID ");
            strSql.Append("left join base_Company f on a.CompanyId=f.ID ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            string AttachmentId = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                AttachmentId = (ds.Tables[0].Rows[0]["AttachmentId_Contract"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_Contract"].ToString() + ",") +
                    (ds.Tables[0].Rows[0]["AttachmentId_ControlCard"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["AttachmentId_ControlCard"].ToString() + ",")
                    ;
            }
            DataTable dt = new System.sys_Attachment().GetAttachment(AttachmentId);
            dt.TableName = "attachment";
            ds.Tables.Add(dt.Copy());

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select a.ID,a.ContractId,a.Progress,a.ReceiveDate,a.ReceiveRate*100 as ReceiveRate,a.ReceiveNat,a.Memo ");
            strSql2.Append("FROM proj_ContractDetail a ");
            strSql2.Append("where a.FlagDel=0 and ContractId=@ContractId ");
            SqlParameter[] parameters2 = {
				new SqlParameter("@ContractId", SqlDbType.Int,4)};
            parameters2[0].Value = ID;
            DataTable dt2 = DbHelperSQL.Query(strSql2.ToString(), parameters2).Tables[0];
            dt2.TableName = "ContractDetail";
            ds.Tables.Add(dt2.Copy());
            return ds;
        }
        /// <summary>
        /// 项目合同是否存在
        /// </summary>
        /// <param name="ProjId">项目ID</param>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public bool Exists(int ProjId, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from proj_Contract");
            strSql.Append(" where FlagDel=0 and ProjId=@ProjId and BillState<>2 and ID<>@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjId", SqlDbType.Int),
					new SqlParameter("@ID", SqlDbType.Int)};
            parameters[0].Value = ProjId;
            parameters[1].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
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
            strSql.Append("select top 20 a.ID as ContractId,b.ProjName,c.CustName,a.ContractCode,a.ContractDate,a.ContractNat,f.CompanyName,d.PerName as SvcManagerName,e.PerName as ProjManagerName ");
            strSql.Append("FROM proj_Contract a ");
            strSql.Append("left join proj_Project b on a.ProjId=b.ID ");
            strSql.Append("left join base_Customer c on b.CustId=c.ID ");
            strSql.Append("left join sys_Person d on b.SvcManagerId=d.ID ");
            strSql.Append("left join sys_Person e on b.ProjManagerId=e.ID ");
            strSql.Append("left join base_Company f on a.CompanyId=f.ID ");
            strSql.Append("where a.FlagDel=0 and a.BillState=1 and b.ProjManagerId in(select e.CtrlPerId from v_sys_PersonCtrl e where e.PerId=" + operaId.ToString() + ") ");
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


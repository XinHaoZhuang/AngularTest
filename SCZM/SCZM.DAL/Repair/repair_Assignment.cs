using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用
namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类Repair_Assignment。
    /// </summary>
    public partial class Repair_Assignment
    {
        public Repair_Assignment()
        { }
        #region  基本方法
        /// <summary>
        /// 某列是否存在
        /// </summary>
        public bool Exists(string FieldName, string FieldValue, int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Repair_Assignment");
            strSql.Append(" where FlagDel=0 and  " + FieldName + "=@" + FieldName + " and ID<>@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@" + FieldName, SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = FieldValue;
            parameters[1].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(SCZM.Model.Repair.Repair_Assignment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Repair_Assignment(");
            strSql.Append("IntentionId,AssignmentCode,AssignmentDate,ExpectStartDate,ExpectCompleteDate,MainRepair,AssistRepair,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,WorkContent)");
            strSql.Append(" values (");
            strSql.Append("@IntentionId,@AssignmentCode,@AssignmentDate,@ExpectStartDate,@ExpectCompleteDate,@MainRepair,@AssistRepair,@FlagDel,@OperaDepId,@OperaId,@OperaName,@OperaTime,@WorkContent)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@AssignmentCode", SqlDbType.VarChar,20),
				new SqlParameter("@AssignmentDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectStartDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectCompleteDate", SqlDbType.SmallDateTime),
				new SqlParameter("@MainRepair", SqlDbType.Int,4),
				new SqlParameter("@AssistRepair", SqlDbType.VarChar,20),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@WorkContent",SqlDbType.NVarChar,200),
				new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.AssignmentCode;
            parameters[2].Value = model.AssignmentDate;
            parameters[3].Value = model.ExpectStartDate;
            parameters[4].Value = model.ExpectCompleteDate;
            parameters[5].Value = model.MainRepair;
            parameters[6].Value = model.AssistRepair;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaDepId;
            parameters[9].Value = model.OperaId;
            parameters[10].Value = model.OperaName;
            parameters[11].Value = model.OperaTime;
            parameters[12].Value = model.WorkContent;
            parameters[13].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2;
            if (model.Repair_Assignment_Procedure != null)
            {
                foreach (SCZM.Model.Repair.Repair_Assignment_Procedure models in model.Repair_Assignment_Procedure)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into Repair_Assignment_Procedure(");
                    strSql2.Append("AssignmentId,ProcedureId,FlagDel,WorkContent,Num,AllNat)");
                    strSql2.Append(" values (");
                    strSql2.Append("@AssignmentId,@ProcedureId,@FlagDel,@WorkContent,@Num,@AllNat)");
                    SqlParameter[] parameters2 = {
						new SqlParameter("@AssignmentId", SqlDbType.Int,4),
						new SqlParameter("@ProcedureId", SqlDbType.Int,4),
						new SqlParameter("@FlagDel", SqlDbType.Int,4),
                                                 new SqlParameter("@WorkContent",SqlDbType.NVarChar,300),
                                                 new SqlParameter("@Num",SqlDbType.Decimal,18),
                                                 new SqlParameter("@AllNat",SqlDbType.Decimal,18)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.ProcedureId;
                    parameters2[2].Value = models.FlagDel;
                    parameters2[3].Value = models.WorkContent;
                    parameters2[4].Value = models.Num;
                    parameters2[5].Value = models.AllNat;

                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[13].Value;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Repair.Repair_Assignment model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment set ");
            strSql.Append("IntentionId=@IntentionId,");
            strSql.Append("AssignmentCode=@AssignmentCode,");
            strSql.Append("AssignmentDate=@AssignmentDate,");
            strSql.Append("ExpectStartDate=@ExpectStartDate,");
            strSql.Append("ExpectCompleteDate=@ExpectCompleteDate,");
            strSql.Append("MainRepair=@MainRepair,");
            strSql.Append("AssistRepair=@AssistRepair,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("WorkContent=@WorkContent");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@AssignmentCode", SqlDbType.VarChar,20),
				new SqlParameter("@AssignmentDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectStartDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectCompleteDate", SqlDbType.SmallDateTime),
				new SqlParameter("@MainRepair", SqlDbType.Int,4),
				new SqlParameter("@AssistRepair", SqlDbType.VarChar,20),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@WorkContent",SqlDbType.NVarChar,200),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.AssignmentCode;
            parameters[2].Value = model.AssignmentDate;
            parameters[3].Value = model.ExpectStartDate;
            parameters[4].Value = model.ExpectCompleteDate;
            parameters[5].Value = model.MainRepair;
            parameters[6].Value = model.AssistRepair;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaDepId;
            parameters[9].Value = model.OperaId;
            parameters[10].Value = model.OperaName;
            parameters[11].Value = model.OperaTime;
            parameters[12].Value = model.WorkContent;
            parameters[13].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from  Repair_Assignment_Procedure where AssignmentId=@AssignmentId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@AssignmentId", SqlDbType.Int,4)};
            parameters2[0].Value = model.ID;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3;
            if (model.Repair_Assignment_Procedure != null)
            {
                foreach (SCZM.Model.Repair.Repair_Assignment_Procedure models in model.Repair_Assignment_Procedure)
                {
                    strSql3 = new StringBuilder();
                    strSql3.Append("insert into Repair_Assignment_Procedure(");
                    strSql3.Append("AssignmentId,ProcedureId,FlagDel,WorkContent,Num,AllNat)");
                    strSql3.Append(" values (");
                    strSql3.Append("@AssignmentId,@ProcedureId,@FlagDel,@WorkContent,@Num,@AllNat)");
                    SqlParameter[] parameters3 = {
						new SqlParameter("@AssignmentId", SqlDbType.Int,4),
						new SqlParameter("@ProcedureId", SqlDbType.Int,4),
						new SqlParameter("@FlagDel", SqlDbType.Int,4),
                                                 new SqlParameter("@WorkContent",SqlDbType.NVarChar,300),
                                                 new SqlParameter("@Num",SqlDbType.Decimal,18),
                                                 new SqlParameter("@AllNat",SqlDbType.Decimal,18)};
                    parameters3[0].Value = model.ID;
                    parameters3[1].Value = models.ProcedureId;
                    parameters3[2].Value = models.FlagDel;
                    parameters3[3].Value = models.WorkContent;
                    parameters3[4].Value = models.Num;
                    parameters3[5].Value = models.AllNat;

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
        public SCZM.Model.Repair.Repair_Assignment GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,IntentionId,AssignmentCode,AssignmentDate,ExpectStartDate,ExpectCompleteDate,MainRepair,AssistRepair,ActualCompleteDate,FlagDel,OperaDepId,OperaId,OperaName,OperaTime,WorkContent from Repair_Assignment ");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SCZM.Model.Repair.Repair_Assignment model = new SCZM.Model.Repair.Repair_Assignment();
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
        public SCZM.Model.Repair.Repair_Assignment DataRowToModel(DataRow row)
        {
            SCZM.Model.Repair.Repair_Assignment model = new SCZM.Model.Repair.Repair_Assignment();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["IntentionId"] != null && row["IntentionId"].ToString() != "")
                {
                    model.IntentionId = int.Parse(row["IntentionId"].ToString());
                }
                if (row["AssignmentCode"] != null)
                {
                    model.AssignmentCode = row["AssignmentCode"].ToString();
                }
                if (row["AssignmentDate"] != null && row["AssignmentDate"].ToString() != "")
                {
                    model.AssignmentDate = DateTime.Parse(row["AssignmentDate"].ToString());
                }
                if (row["ExpectStartDate"] != null && row["ExpectStartDate"].ToString() != "")
                {
                    model.ExpectStartDate = DateTime.Parse(row["ExpectStartDate"].ToString());
                }
                if (row["ExpectCompleteDate"] != null && row["ExpectCompleteDate"].ToString() != "")
                {
                    model.ExpectCompleteDate = DateTime.Parse(row["ExpectCompleteDate"].ToString());
                }
                if (row["MainRepair"] != null && row["MainRepair"].ToString() != "")
                {
                    model.MainRepair = int.Parse(row["MainRepair"].ToString());
                }
                if (row["AssistRepair"] != null)
                {
                    model.AssistRepair = row["AssistRepair"].ToString();
                }
                if (row["WorkContent"] != null) {
                    model.WorkContent = row["WorkContent"].ToString();
                }
                if (row["ActualCompleteDate"] != null && row["ActualCompleteDate"].ToString() != "")
                {
                    model.ActualCompleteDate = DateTime.Parse(row["ActualCompleteDate"].ToString());
                }
                if (row["FlagDel"] != null && row["FlagDel"].ToString() != "")
                {
                    model.FlagDel = int.Parse(row["FlagDel"].ToString());
                }
                if (row["OperaDepId"] != null && row["OperaDepId"].ToString() != "")
                {
                    model.OperaDepId = int.Parse(row["OperaDepId"].ToString());
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
            List<string> sqllist = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment_Procedure set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and AssignmentId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment set FlagDel=1 ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }
        /// <summary>
        /// 批量作废数据
        /// </summary>
        public int UndoList(string IDList)
        {
            List<string> sqllist = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment_Procedure set FlagDel=2 ");
            strSql.Append("where FlagDel=0 and AssignmentId in(" + IDList + ")");
            sqllist.Add(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment set FlagDel=2 ");
            strSql.Append("where FlagDel=0 and ID in(" + IDList + ")");
            sqllist.Add(strSql.ToString());
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }
        /// <summary>
        /// 获得数据列表 通过Where条件
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.IntentionId,a.AssignmentCode,a.AssignmentDate,a.ExpectStartDate,a.ExpectCompleteDate,a.MainRepair,f.PerName as MainRepairName,a.AssistRepair,a.ActualCompleteDate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,b.IntentionCode,b.IntentionDate,b.IntentionType,b.CustTypeId,b.CustName,c.MachineModel,b.MachineCode,d.RepairTypeName,e.DepName as BusinessDep,b.LinkPhone,a.WorkContent,(select distinct h.ProcedureName+',' from Repair_Assignment_Procedure g left join base_Procedure h on h.FlagDel=0 and g.ProcedureId=h.ID where g.AssignmentId=a.ID and g.FlagDel=0 for xml path('')) as ProcedureNames ");
            strSql.Append("FROM Repair_Assignment a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join base_RepairType d on b.RepairTypeId=d.ID and d.FlagDel=0 ");
            strSql.Append("left join sys_Department e on b.BusinessDepId=e.ID and e.FlagDel=0 ");
            strSql.Append("left join sys_Person f on a.MainRepair=f.ID ");
            strSql.Append("where a.FlagDel=0");
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
            strSql.Append("select a.ID,a.IntentionId,a.AssignmentCode,b.CustName,b.MachineCode,c.MachineModel,b.IntentionCode,b.IntentionDate,b.RepairTypeId,f.RepairTypeName,b.LinkPhone,b.MachineStatus,b.CustOpinion,d.DepName as BusinessDep,b.BusinessPerName,a.AssignmentDate,a.ExpectStartDate,a.ExpectCompleteDate,a.MainRepair,g.PerName as MainRepairName,a.AssistRepair,a.ActualCompleteDate,a.OperaDepId,a.OperaId,a.OperaName,a.OperaTime,e.ID as SchemeId,e.SchemeCode,a.WorkContent ");
            strSql.Append("FROM Repair_Assignment a ");
            strSql.Append("left join Repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join sys_Department d on b.BusinessDepId=d.ID and d.FlagDel=0 ");
            strSql.Append("left join Repair_Scheme e on a.IntentionId=e.IntentionId and e.FlagDel=0 ");
            strSql.Append("left join base_RepairType f on b.RepairTypeId=f.ID and f.FlagDel=0 ");
            strSql.Append("left join sys_Person g on a.MainRepair=g.ID ");
            strSql.Append("where a.FlagDel=0 and a.ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select a.ID,a.AssignmentId,a.ProcedureId,b.ProcedureName,a.WorkContent,a.Num,c.NumType,c.MachineLevelNat,a.AllNat,d.ScheduleType,a.AuditState ");
            strSql2.Append("FROM Repair_Assignment_Procedure a ");
            strSql2.Append("left join base_Procedure b on a.ProcedureId=b.ID ");
            strSql2.Append("left join (select *,case (select c.MachineLevel from Repair_Assignment a left join repair_Intention b on a.IntentionId=b.ID left join base_MachineModel c on b.MachineModelId=c.ID where a.ID=@ID) when 1 then MachineLevel10 when 2 then MachineLevel20 when 3 then MachineLevel30 when 4 then MachineLevel40 when 5 then MachineLevel50 else 0 end as MachineLevelNat from base_ProcedureMachineNat where FlagDel=0 ) c on a.ProcedureId=c.ProcedureId ");
            strSql2.Append("left join repair_Schedule d on d.AssignmentProcedureId=a.ID and d.FlagDel=0 and d.AssignmentId=a.AssignmentId and d.FlagNew=0 ");
            strSql2.Append("where a.FlagDel=0 and a.AssignmentId=@AssignmentId ");
            SqlParameter[] parameters2 = {
                new SqlParameter("@ID",SqlDbType.Int,4),
				new SqlParameter("@AssignmentId", SqlDbType.Int,4)};
            parameters2[0].Value = ID;
            parameters2[1].Value = ID;
            DataTable dt2 = DbHelperSQL.Query(strSql2.ToString(), parameters2).Tables[0];
            dt2.TableName = "Assignment_Process";
            ds.Tables.Add(dt2.Copy());

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select a.ID,a.AssignmentCode ");
            strSql3.Append("FROM Repair_Assignment a ");
            strSql3.Append("where a.FlagDel=0 and IntentionId=@IntentionId order by a.AssignmentDate,a.OperaTime asc");
            SqlParameter[] parameters3 = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4)};
            parameters3[0].Value = int.Parse(ds.Tables[0].Rows[0]["IntentionId"].ToString());
            DataTable dt3 = DbHelperSQL.Query(strSql3.ToString(), parameters3).Tables[0];
            dt3.TableName = "Assignment_ActualList";
            ds.Tables.Add(dt3.Copy());



            return ds;
        }
        /// <summary>
        /// 获取最大ID+1
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "repair_Assignment");
        }
        /// <summary>
        /// 派工单号下拉框
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 20 a.ID as AssignmentId,a.AssignmentCode,b.IntentionCode,b.CustName,b.MachineModelId,c.MachineModel,b.MachineCode,d.PerName ");
            strSql.Append("from repair_Assignment a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join sys_Person d on a.MainRepair=d.ID ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetProcessComboList(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.AssignmentId,a.ProcedureId,b.ProcedureName,a.Num,a.AllNat ");
            strSql.Append("from repair_Assignment_Procedure a ");
            strSql.Append("left join base_Procedure b on a.ProcedureId=b.ID and b.FlagDel=0 ");
            //strSql.Append("left join base_ProcedureClass c on b.ClassId=c.ID and c.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere != "") {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.ID asc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetScheduleList(string strWhere,int PerId,bool IsAdmin) {
            StringBuilder strSql = new StringBuilder();
            if (IsAdmin)
            {
                strSql.Append("select a.AssignmentId,a.ProcedureId,g.ProcedureName,b.AssignmentCode,c.IntentionCode,c.CustName,c.CustTypeId,c.MachineCode,d.MachineModel,e.PerName as MainRepairName,c.LinkPhone,isNull(f.ScheduleType,0) as ScheduleType ,f.ScheduleDate,f.OperaName,f.OperaTime,a.ID,b.OperaTime as AssignmentTime,c.Linkman,a.WorkContent,b.ExpectStartDate,b.ExpectCompleteDate,d.MachineLevel,case d.MachineLevel when 1 then h.MachineLevel10 when 2 then h.MachineLevel20 when 3 then h.MachineLevel30 when 4 then h.MachineLevel40 when 5 then h.MachineLevel50 end as TimeStandard,case when f.ID is not null then dbo.timeSpan(a.ID) else null end as Second_Repair,f.ID as ScheduleId ");
                strSql.Append("from Repair_Assignment_Procedure a ");
                strSql.Append("left join Repair_Assignment b on a.AssignmentId=b.ID ");
                strSql.Append("left join repair_Intention c on b.IntentionId=c.ID and c.FlagDel=0 ");
                strSql.Append("left join base_MachineModel d on c.MachineModelId=d.ID and d.FlagDel=0 ");
                strSql.Append("left join sys_Person e on b.MainRepair=e.ID ");
                strSql.Append("left join repair_Schedule f on a.ID=f.AssignmentProcedureId and f.FlagDel=0 and f.FlagNew=0 ");
                strSql.Append("left join base_Procedure g on a.ProcedureId=g.ID and g.FlagDel=0 ");
                strSql.Append("left join base_RepairTimeStandard h on a.ProcedureId=h.ProcedureId ");
                strSql.Append("where a.FlagDel=0 and b.FlagDel=0 ");
                if (strWhere != "")
                {
                    strSql.Append(strWhere);
                }
                strSql.Append(" order by b.OperaTime desc,ScheduleType desc");
            }
            else
            {
                strSql.Append("select distinct a.AssignmentId,a.ProcedureId,g.ProcedureName,b.AssignmentCode,c.IntentionCode,c.CustName,c.CustTypeId,c.MachineCode,d.MachineModel,e.PerName as MainRepairName,c.LinkPhone,isNull(f.ScheduleType,0) as ScheduleType ,f.ScheduleDate,f.OperaName,f.OperaTime,a.ID,b.OperaTime as AssignmentTime,c.Linkman,a.WorkContent,b.ExpectStartDate,b.ExpectCompleteDate,d.MachineLevel,case d.MachineLevel when 1 then h.MachineLevel10 when 2 then h.MachineLevel20 when 3 then h.MachineLevel30 when 4 then h.MachineLevel40 when 5 then h.MachineLevel50 end as TimeStandard,case when f.ID is not null then dbo.timeSpan(a.ID) else null end as Second_Repair,f.ID as ScheduleId ");
                strSql.Append("from Repair_Assignment_Procedure a ");
                strSql.Append("left join Repair_Assignment b on a.AssignmentId=b.ID ");
                strSql.Append("left join repair_Intention c on b.IntentionId=c.ID and c.FlagDel=0 ");
                strSql.Append("left join base_MachineModel d on c.MachineModelId=d.ID and d.FlagDel=0 ");
                strSql.Append("left join sys_Person e on b.MainRepair=e.ID ");
                strSql.Append("left join repair_Schedule f on a.ID=f.AssignmentProcedureId and f.FlagDel=0 and f.FlagNew=0 ");
                strSql.Append("left join base_Procedure g on a.ProcedureId=g.ID and g.FlagDel=0 ");
                strSql.Append("left join (select ctrlperid from  v_sys_PersonCtrl where PerId=" + PerId + ") i on ','+cast(b.MainRepair as varchar(20))+','+b.AssistRepair+',' like '%,'+CAST(i.ctrlperid as varchar(20))+',%' ");
                strSql.Append("left join base_RepairTimeStandard h on a.ProcedureId=h.ProcedureId ");
                strSql.Append("where a.FlagDel=0 and i.CtrlPerId is not null and b.FlagDel=0 ");
                if (strWhere != "")
                {
                    strSql.Append(strWhere);
                }
                strSql.Append(" order by b.OperaTime desc,ScheduleType desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetScheduleList_search(string strWhere, int PerId, bool IsAdmin)
        {
            StringBuilder strSql = new StringBuilder();
            if (IsAdmin)
            {
                strSql.Append("select a.AssignmentId,a.ProcedureId,g.ProcedureName,b.AssignmentCode,c.IntentionCode,c.CustName,c.CustTypeId,c.MachineCode,d.MachineModel,e.PerName as MainRepairName,c.LinkPhone,isNull(f.ScheduleType,0) as ScheduleType ,f.ScheduleDate,f.OperaName,f.OperaTime,a.ID,b.OperaTime as AssignmentTime,c.Linkman,a.WorkContent,b.ExpectStartDate,b.ExpectCompleteDate,j.minScheduleDate,case when ISNULL(f.ScheduleType,0)=3 then k.maxScheduleDate else null end as maxScheduleDate,e.DepId ");
                strSql.Append("from Repair_Assignment_Procedure a ");
                strSql.Append("left join Repair_Assignment b on a.AssignmentId=b.ID ");
                strSql.Append("left join repair_Intention c on b.IntentionId=c.ID and c.FlagDel=0 ");
                strSql.Append("left join base_MachineModel d on c.MachineModelId=d.ID and d.FlagDel=0 ");
                strSql.Append("left join sys_Person e on b.MainRepair=e.ID ");
                strSql.Append("left join repair_Schedule f on a.ID=f.AssignmentProcedureId and f.FlagDel=0 and f.FlagNew=0 ");
                strSql.Append("left join base_Procedure g on a.ProcedureId=g.ID and g.FlagDel=0 ");
                strSql.Append("left join (select AssignmentId,ProcedureId,MIN(ScheduleDate) as minScheduleDate from repair_Schedule where FlagDel=0 group by AssignmentId,ProcedureId) j on j.AssignmentId=a.AssignmentId and j.ProcedureId=a.ProcedureId ");
                strSql.Append("left join (select AssignmentId,ProcedureId,max(ScheduleDate) as maxScheduleDate from repair_Schedule where FlagDel=0 group by AssignmentId,ProcedureId) k on k.AssignmentId=a.AssignmentId and k.ProcedureId=a.ProcedureId ");
                strSql.Append("left join sys_Department l on l.FlagDel=0 and e.DepId=l.ID ");
                strSql.Append("where a.FlagDel=0 ");
                if (strWhere != "")
                {
                    strSql.Append(strWhere);
                }
                strSql.Append(" order by c.MachineCode,b.OperaTime desc");
            }
            else
            {
                strSql.Append("select distinct a.AssignmentId,a.ProcedureId,g.ProcedureName,b.AssignmentCode,c.IntentionCode,c.CustName,c.CustTypeId,c.MachineCode,d.MachineModel,e.PerName as MainRepairName,c.LinkPhone,isNull(f.ScheduleType,0) as ScheduleType ,f.ScheduleDate,f.OperaName,f.OperaTime,a.ID,b.OperaTime as AssignmentTime,c.Linkman,a.WorkContent,b.ExpectStartDate,b.ExpectCompleteDate,j.minScheduleDate,case when ISNULL(f.ScheduleType,0)=3 then k.maxScheduleDate else null end as maxScheduleDate,e.DepId ");
                strSql.Append("from Repair_Assignment_Procedure a ");
                strSql.Append("left join Repair_Assignment b on a.AssignmentId=b.ID ");
                strSql.Append("left join repair_Intention c on b.IntentionId=c.ID and c.FlagDel=0 ");
                strSql.Append("left join base_MachineModel d on c.MachineModelId=d.ID and d.FlagDel=0 ");
                strSql.Append("left join sys_Person e on b.MainRepair=e.ID ");
                strSql.Append("left join repair_Schedule f on a.ID=f.AssignmentProcedureId and f.FlagDel=0 and f.FlagNew=0 ");
                strSql.Append("left join base_Procedure g on a.ProcedureId=g.ID and g.FlagDel=0 ");
                strSql.Append("left join (select ctrlperid from  v_sys_PersonCtrl where PerId=" + PerId + ") i on ','+cast(c.OperaId as varchar(20))+',' like '%,'+CAST(i.ctrlperid as varchar(20))+',%' ");
                strSql.Append("left join (select AssignmentId,ProcedureId,MIN(ScheduleDate) as minScheduleDate from repair_Schedule where FlagDel=0 group by AssignmentId,ProcedureId) j on j.AssignmentId=a.AssignmentId and j.ProcedureId=a.ProcedureId ");
                strSql.Append("left join (select AssignmentId,ProcedureId,max(ScheduleDate) as maxScheduleDate from repair_Schedule where FlagDel=0 group by AssignmentId,ProcedureId) k on k.AssignmentId=a.AssignmentId and k.ProcedureId=a.ProcedureId ");
                strSql.Append("left join sys_Department l on l.FlagDel=0 and e.DepId=l.ID ");
                strSql.Append("where a.FlagDel=0 and i.CtrlPerId is not null  ");
                if (strWhere != "")
                {
                    strSql.Append(strWhere);
                }
                strSql.Append(" order by c.MachineCode,b.OperaTime desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetAssignmentList(string strWhere, int PerId, bool IsAdmin,string strWhere_all)
        {
            StringBuilder strSql = new StringBuilder();
            if (IsAdmin)
            {
                strSql.Append("select * from (");
                strSql.Append("select a.OperaTime,a.IntentionCode,a.Linkman,b.MachineModel,a.MachineCode,a.CustName,case when (select COUNT(1) from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 where d.IntentionId=a.ID)=(select COUNT(1) from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.IntentionId=a.ID and f.ScheduleType=3) then 1 when (select COUNT(1) from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.IntentionId=a.ID and f.ScheduleType>0)=0 then 0  else 2 end as AssignmentType,(select PerName+',' from (select distinct b.PerName from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID where aa.FlagDel=0 and aa.IntentionId=a.ID ) a for xml path('')) as MainRepairs,(select cast(ID as varchar)+',' from (select distinct c.ID from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID left join sys_Department c on  b.DepId=c.ID and c.FlagDel=0 where aa.FlagDel=0 and aa.IntentionId=a.ID) a for xml path('')) as MainRepairsDep ");
                strSql.Append("from repair_Intention a ");
                strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID and b.FlagDel=0 ");
                strSql.Append("left join (select COUNT(1) as AssignmentProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 where d.FlagDel=0 group by d.IntentionId) f on f.IntentionId=a.ID ");
                strSql.Append("left join (select COUNT(1) as CompleteProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where f.ScheduleType=3 and d.FlagDel=0 group by d.IntentionId) g on g.IntentionId=a.ID ");
                strSql.Append("left join  (select COUNT(1) as RepairingProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.FlagDel=0 and f.ScheduleType>0 group by d.IntentionId)h on h.IntentionId=a.ID ");
                strSql.Append("where (select COUNT(1) from Repair_Assignment c where a.ID=c.IntentionId and c.FlagDel=0)>0 and a.FlagDel=0 ");
                if (strWhere != "")
                {
                    strSql.Append(strWhere);
                }
                strSql.Append(") a where 1=1");
                if (strWhere_all != "") {
                    strSql.Append(strWhere_all);
                }
                strSql.Append(" order by a.OperaTime desc");
            }
            else
            {
                strSql.Append("select * from (");
                strSql.Append("select distinct b.IntentionCode,b.Linkman,c.MachineModel,b.MachineCode,b.CustName,b.OperaTime,f.AssignmentProcedureCount,g.CompleteProcedureCount,h.RepairingProcedureCount,case when AssignmentProcedureCount=ISNULL(CompleteProcedureCount,0) then 1 when ISNULL(RepairingProcedureCount,0)>0 then 2 else 0 end as AssignmentType,(select PerName+',' from (select distinct b.PerName from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID where aa.FlagDel=0 and aa.IntentionId=a.IntentionId ) a for xml path('')) as MainRepairs,(select cast(ID as varchar)+',' from (select distinct c.ID from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID left join sys_Department c on c.FlagDel=0 and c.ID=b.DepId where aa.FlagDel=0 and aa.IntentionId=a.IntentionId) a for xml path('')) as MainRepairsDep ");
                strSql.Append("from repair_Assignment a  ");
                strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
                strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
                strSql.Append("left join (select COUNT(1) as AssignmentProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 where d.FlagDel=0 group by d.IntentionId) f on f.IntentionId=b.ID ");
                strSql.Append("left join (select COUNT(1) as CompleteProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where f.ScheduleType=3 and d.FlagDel=0 group by d.IntentionId) g on g.IntentionId=b.ID ");
                strSql.Append("left join  (select COUNT(1) as RepairingProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.FlagDel=0 and f.ScheduleType>0 group by d.IntentionId)h on h.IntentionId=b.ID ");
                strSql.Append("left join (select ctrlperid from  v_sys_PersonCtrl where PerId=" + PerId + ") d on ','+cast(a.MainRepair as varchar(20))+','+a.AssistRepair+',' like '%,'+CAST(d.ctrlperid as varchar(20))+',%' ");
                strSql.Append("where a.FlagDel=0 and d.CtrlPerId is not null");
                if (strWhere != "")
                {
                    strSql.Append(strWhere);
                }
                strSql.Append(") a where 1=1");
                if (strWhere_all != "")
                {
                    strSql.Append(strWhere_all);
                }
                strSql.Append(" order by a.OperaTime desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetAssignmentList_search(string strWhere, int PerId, bool IsAdmin,string strWhere_all)
        {
            StringBuilder strSql = new StringBuilder();
            if (IsAdmin)
            {
                strSql.Append("select a.IntentionCode,a.Linkman,a.MachineModel,a.MachineCode,a.CustName,a.AssignmentType,LEFT(a.MainRepairs,len(a.MainRepairs)-1) as MainRepairs,case a.AssignmentType when 1 then b.startDate when 2 then c.startDate else null end as StartDate,case a.AssignmentType when 1 then b.completeDate else null end as CompleteDate,left(a.MainRepairsDep,len(a.MainRepairsDep)-1) as MainRepairsDep ");
                strSql.Append("from(");
                strSql.Append("select a.OperaTime,a.ID,a.IntentionCode,a.Linkman,b.MachineModel,a.MachineCode,a.CustName,case when (select COUNT(1) from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 where d.IntentionId=a.ID)=(select COUNT(1) from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.IntentionId=a.ID and f.ScheduleType=3) then 1 when (select COUNT(1) from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.IntentionId=a.ID and f.ScheduleType>0)=0 then 0  else 2 end as AssignmentType,(select PerName+',' from (select distinct b.PerName from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID where aa.FlagDel=0 and aa.IntentionId=a.ID) a for xml path('')) as MainRepairs,(select cast(ID as varchar)+',' from (select distinct c.ID from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID left join sys_Department c on  b.DepId=c.ID and c.FlagDel=0 where aa.FlagDel=0 and aa.IntentionId=a.ID) a for xml path('')) as MainRepairsDep ");
                strSql.Append("from repair_Intention a ");
                strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID and b.FlagDel=0 ");
                strSql.Append("left join (select COUNT(1) as AssignmentProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 where d.FlagDel=0 group by d.IntentionId) f on f.IntentionId=a.ID ");
                strSql.Append("left join (select COUNT(1) as CompleteProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where f.ScheduleType=3 and d.FlagDel=0 group by d.IntentionId) g on g.IntentionId=a.ID ");
                strSql.Append("left join  (select COUNT(1) as RepairingProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.FlagDel=0 and f.ScheduleType>0 group by d.IntentionId)h on h.IntentionId=a.ID ");
                strSql.Append("where (select COUNT(1) from Repair_Assignment c where a.ID=c.IntentionId and c.FlagDel=0)>0 and a.FlagDel=0 ");
                if (strWhere != "")
                {
                    strSql.Append(strWhere);
                }
                strSql.Append(" )as a ");
                strSql.Append("left join (select min(ScheduleDate) as startDate,max(ScheduleDate) as completeDate,b.IntentionId from repair_Schedule a ");
                strSql.Append("left join Repair_Assignment b on a.AssignmentId=b.ID and b.FlagDel=0 ");
                strSql.Append("where a.FlagDel=0 and b.ID is not null group by b.IntentionId) as b on a.ID=b.IntentionId and a.AssignmentType=1 ");
                strSql.Append("left join (select min(ScheduleDate) as startDate,b.IntentionId from repair_Schedule a left join Repair_Assignment b on a.AssignmentId=b.ID and b.FlagDel=0 ");
                strSql.Append("where a.FlagDel=0 and b.ID is not null group by b.IntentionId) as c on a.ID=c.IntentionId and a.AssignmentType=2 ");
                if (strWhere_all != "") {
                    strSql.Append(strWhere_all);
                }
                strSql.Append(" order by a.OperaTime desc");
            }
            else
            {
                strSql.Append("select a.IntentionCode,a.Linkman,a.MachineModel,a.MachineCode,a.CustName,a.AssignmentType,case a.AssignmentType when 1 then b.startDate when 2 then c.startDate else null end as StartDate,case a.AssignmentType when 1 then b.completeDate else null end as CompleteDate,left(a.MainRepairs,len(a.MainRepairs)-1) as MainRepairs,left(a.MainRepairsDep,len(a.MainRepairsDep)-1) as MainRepairsDep ");
                strSql.Append("from(");
                strSql.Append("select distinct b.ID,b.IntentionCode,b.Linkman,c.MachineModel,b.MachineCode,b.CustName,b.OperaTime,f.AssignmentProcedureCount,g.CompleteProcedureCount,h.RepairingProcedureCount,case when AssignmentProcedureCount=ISNULL(CompleteProcedureCount,0) then 1 when ISNULL(RepairingProcedureCount,0)>0 then 2 else 0 end as AssignmentType,(select PerName+',' from (select distinct b.PerName from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID where aa.FlagDel=0 and aa.IntentionId=a.IntentionId) a for xml path('')) as MainRepairs,(select cast(ID as varchar)+',' from (select distinct c.ID from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID left join sys_Department c on c.FlagDel=0 and c.ID=b.DepId where aa.FlagDel=0 and aa.IntentionId=a.IntentionId) a for xml path('')) as MainRepairsDep ");
                strSql.Append("from repair_Assignment a  ");
                strSql.Append("left join repair_Intention b on a.IntentionId=b.ID and b.FlagDel=0 ");
                strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
                strSql.Append("left join (select COUNT(1) as AssignmentProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 where d.FlagDel=0 group by d.IntentionId) f on f.IntentionId=b.ID ");
                strSql.Append("left join (select COUNT(1) as CompleteProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where f.ScheduleType=3 and d.FlagDel=0 group by d.IntentionId) g on g.IntentionId=b.ID ");
                strSql.Append("left join  (select COUNT(1) as RepairingProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.FlagDel=0 and f.ScheduleType>0 group by d.IntentionId)h on h.IntentionId=b.ID ");
                strSql.Append("left join (select ctrlperid from  v_sys_PersonCtrl where PerId=" + PerId + ") d on ','+cast(b.OperaId as varchar(20))+',' like '%,'+CAST(d.ctrlperid as varchar(20))+',%' ");
                strSql.Append("where a.FlagDel=0 and d.CtrlPerId is not null");
                if (strWhere != "")
                {
                    strSql.Append(strWhere);
                }
                strSql.Append(" )as a ");
                strSql.Append("left join (select min(ScheduleDate) as startDate,max(ScheduleDate) as completeDate,b.IntentionId from repair_Schedule a ");
                strSql.Append("left join Repair_Assignment b on a.AssignmentId=b.ID and b.FlagDel=0 ");
                strSql.Append("where a.FlagDel=0 and b.ID is not null group by b.IntentionId) as b on a.ID=b.IntentionId and a.AssignmentType=1 ");
                strSql.Append("left join (select min(ScheduleDate) as startDate,b.IntentionId from repair_Schedule a left join Repair_Assignment b on a.AssignmentId=b.ID and b.FlagDel=0 ");
                strSql.Append("where a.FlagDel=0 and b.ID is not null group by b.IntentionId) as c on a.ID=c.IntentionId and a.AssignmentType=2 ");
                if (strWhere_all != "")
                {
                    strSql.Append(strWhere_all);
                }
                strSql.Append(" order by a.OperaTime desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(SCZM.Model.Repair.Repair_Assignment model, string procedureList_prev)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment set ");
            strSql.Append("IntentionId=@IntentionId,");
            strSql.Append("AssignmentCode=@AssignmentCode,");
            strSql.Append("AssignmentDate=@AssignmentDate,");
            strSql.Append("ExpectStartDate=@ExpectStartDate,");
            strSql.Append("ExpectCompleteDate=@ExpectCompleteDate,");
            strSql.Append("MainRepair=@MainRepair,");
            strSql.Append("AssistRepair=@AssistRepair,");
            strSql.Append("FlagDel=@FlagDel,");
            strSql.Append("OperaDepId=@OperaDepId,");
            strSql.Append("OperaId=@OperaId,");
            strSql.Append("OperaName=@OperaName,");
            strSql.Append("OperaTime=@OperaTime,");
            strSql.Append("WorkContent=@WorkContent");
            strSql.Append(" where FlagDel=0 and ID=@ID ");
            SqlParameter[] parameters = {
				new SqlParameter("@IntentionId", SqlDbType.Int,4),
				new SqlParameter("@AssignmentCode", SqlDbType.VarChar,20),
				new SqlParameter("@AssignmentDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectStartDate", SqlDbType.SmallDateTime),
				new SqlParameter("@ExpectCompleteDate", SqlDbType.SmallDateTime),
				new SqlParameter("@MainRepair", SqlDbType.Int,4),
				new SqlParameter("@AssistRepair", SqlDbType.VarChar,20),
				new SqlParameter("@FlagDel", SqlDbType.Int,4),
				new SqlParameter("@OperaDepId", SqlDbType.Int,4),
				new SqlParameter("@OperaId", SqlDbType.Int,4),
				new SqlParameter("@OperaName", SqlDbType.NVarChar,10),
				new SqlParameter("@OperaTime", SqlDbType.DateTime),
                new SqlParameter("@WorkContent",SqlDbType.NVarChar,200),
				new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.IntentionId;
            parameters[1].Value = model.AssignmentCode;
            parameters[2].Value = model.AssignmentDate;
            parameters[3].Value = model.ExpectStartDate;
            parameters[4].Value = model.ExpectCompleteDate;
            parameters[5].Value = model.MainRepair;
            parameters[6].Value = model.AssistRepair;
            parameters[7].Value = model.FlagDel;
            parameters[8].Value = model.OperaDepId;
            parameters[9].Value = model.OperaId;
            parameters[10].Value = model.OperaName;
            parameters[11].Value = model.OperaTime;
            parameters[12].Value = model.WorkContent;
            parameters[13].Value = model.ID;

            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            
            List<string> procedureList = new List<string>(procedureList_prev.Split(','));
            StringBuilder strSql3;
            if (model.Repair_Assignment_Procedure != null)
            {
                foreach (SCZM.Model.Repair.Repair_Assignment_Procedure models in model.Repair_Assignment_Procedure)
                {
                    if (procedureList.Contains(models.ProcedureId.ToString()))
                    {
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update Repair_Assignment_Procedure set WorkContent=@WorkContent,Num=@Num,AllNat=@AllNat where AssignmentId=@AssignmentId and ProcedureId=@ProcedureId and FlagDel=0 ");
                        SqlParameter[] parameters2 = {
                                new SqlParameter("@WorkContent",SqlDbType.NVarChar,300),
                                new SqlParameter("@Num",SqlDbType.Decimal,18),
                                new SqlParameter("@AllNat",SqlDbType.Decimal,18),
                                new SqlParameter("@AssignmentId", SqlDbType.Int,4),
                                new SqlParameter("@ProcedureId",SqlDbType.Int,4)};
                        parameters2[0].Value = models.WorkContent;
                        parameters2[1].Value = models.Num;
                        parameters2[2].Value = models.AllNat;
                        parameters2[3].Value = model.ID;
                        parameters2[4].Value = models.ProcedureId;
                        cmd = new CommandInfo(strSql2.ToString(), parameters2);
                        sqllist.Add(cmd);
                        procedureList.Remove(models.ProcedureId.ToString());
                    }
                    else
                    {
                        strSql3 = new StringBuilder();
                        strSql3.Append("insert into Repair_Assignment_Procedure(");
                        strSql3.Append("AssignmentId,ProcedureId,FlagDel,WorkContent,Num,AllNat)");
                        strSql3.Append(" values (");
                        strSql3.Append("@AssignmentId,@ProcedureId,@FlagDel,@WorkContent,@Num,@AllNat)");
                        SqlParameter[] parameters3 = {
						new SqlParameter("@AssignmentId", SqlDbType.Int,4),
						new SqlParameter("@ProcedureId", SqlDbType.Int,4),
						new SqlParameter("@FlagDel", SqlDbType.Int,4),
                        new SqlParameter("@WorkContent",SqlDbType.NVarChar,300),
                        new SqlParameter("@Num",SqlDbType.Decimal,18),
                        new SqlParameter("@AllNat",SqlDbType.Decimal,18)};
                        parameters3[0].Value = model.ID;
                        parameters3[1].Value = models.ProcedureId;
                        parameters3[2].Value = models.FlagDel;
                        parameters3[3].Value = models.WorkContent;
                        parameters3[4].Value = models.Num;
                        parameters3[5].Value = models.AllNat;
                        cmd = new CommandInfo(strSql3.ToString(), parameters3);
                    }
                    sqllist.Add(cmd);
                }
            }
            if (procedureList.Count > 0) {
                foreach (string item in procedureList)
                {
                    StringBuilder strSql4 = new StringBuilder();
                    strSql4.Append("update Repair_Assignment_Procedure set FlagDel=1 where AssignmentId=@AssignmentId and ProcedureId="+item);
                    SqlParameter[] parameters4 = {
                        new SqlParameter("@AssignmentId", SqlDbType.Int,4)};
                    parameters4[0].Value = model.ID;
                    cmd = new CommandInfo(strSql4.ToString(), parameters4);
                    sqllist.Add(cmd);

                    StringBuilder strSql5 = new StringBuilder();
                    strSql5.Append("update repair_Schedule set FlagDel=1 where AssignmentId=@AssignmentId and ProcedureId=" + item);
                    SqlParameter[] parameters5 = {
                        new SqlParameter("@AssignmentId", SqlDbType.Int,4)};
                    parameters5[0].Value = model.ID;
                    cmd = new CommandInfo(strSql5.ToString(), parameters5);
                    sqllist.Add(cmd);

                }
                
            }

            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rows;
        }
        public DataSet GetAssignmentProcedureList(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID,a.AssignmentId,a.ProcedureId,a.Num,a.AllNat from Repair_Assignment_Procedure a ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere != "") {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public int UpdateRepairSecond(int AssignmentProcedureId, int RepairSecond_All, int RepairSecond_Repair, int RepairSecond_Pause) {
            string strSql = "update Repair_Assignment_Procedure set RepairSecond_All=" + RepairSecond_All + ",RepairSecond_Repair=" + RepairSecond_Repair + ",RepairSecond_Pause=" + RepairSecond_Pause + " where FlagDel=0 and ID=" + AssignmentProcedureId;
            return DbHelperSQL.ExecuteSql(strSql);
        }
        public int CancelRepairSecond(int AssignmentProcedureId) {
            string strSql = "update Repair_Assignment_Procedure set RepairSecond_All=null,RepairSecond_Repair=null,RepairSecond_Pause=null where FlagDel=0 and ID=" + AssignmentProcedureId;
            return DbHelperSQL.ExecuteSql(strSql);
        }
        #endregion  扩展方法
    }
}


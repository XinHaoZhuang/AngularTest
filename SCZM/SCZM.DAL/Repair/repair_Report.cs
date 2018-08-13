using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.DBUtility;//请先添加引用

namespace SCZM.DAL.Repair
{
    /// <summary>
    /// 数据访问类repair_Report。
    /// </summary>
    public partial class repair_Report
    {
        public repair_Report() { }
        #region 维修台帐
        public DataSet GetRepairTZList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select a.ID as IntentionId ,a.IntentionType,a.CustTypeId, a.IntentionCode,a.CustName,a.MachineModelId,g.MachineModel,a.MachineCode,a.SMR,a.LinkPhone,a.BusinessDepId,h.DepName,a.FlagResult,a.RepairAdress,a.AttachmentId_Agreement,");
            strSql.Append("b.ID as AssignmentId,b.AssignmentCode,b.MainRepair,i.PerName as MainRepairName,b.AssistRepair,(select PerName+',' from sys_Person where CHARINDEX(','+LTRIM(ID)+',',','+b.AssistRepair+',')>0 for xml path('')) as AssistRepairNames,c.RepairContent,");
            strSql.Append("c.ID as SchemeId,c.SchemeCode,c.FlagELE,c.FlagENG,c.FlagENGKC,c.FlagMCV,c.FlagPPM,c.FlagPPMKC,c.FlagRM,c.FlagSM,c.FlagUM,c.FlagVM,c.FlagVR,c.PartFee,c.TimeFee,");
            strSql.Append("a.RepairTypeId,j.RepairTypeName,a.RepairAdress,d.ID as SettlementId,d.SettlementCode,d.SettlementTypeId,l.SettlementName,d.SettlementFee_rebate,d.OperaDepId,a.ActualEnterDate,b.AssignmentDate,a.ExpectLeaveDate,d.OperaTime,a.ActualLeaveDate,e.ID as ReceiveFeeId,e.PayType,k.PayTypeName,e.ReceiveFee,");
            strSql.Append("f.ID as ContractId,f.ContractCode,f.ContractDate,f.WarrantyPeriod,f.WarrantyContent ");
            strSql.Append("From repair_Intention a ");
            strSql.Append("left join Repair_Assignment b on b.FlagDel=0 and b.IntentionId=a.ID ");
            strSql.Append("left join repair_Scheme c on c.FlagDel=0 and c.IntentionId=a.ID ");
            strSql.Append("left join repair_SettlementList d on d.FlagDel=0 and d.IntentionId=a.ID ");
            strSql.Append("left join repair_ReceiveFee e on e.FlagDel=0 and e.IntentionId=a.ID ");
            strSql.Append("left join repair_Contract f on f.FlagDel=0 and f.IntentionId=a.ID ");
            strSql.Append("left join base_MachineModel g on g.FlagDel=0 and g.ID=a.MachineModelId ");
            strSql.Append("left join sys_Department h on h.FlagDel=0 and h.ID=a.BusinessDepId ");
            strSql.Append("left join sys_Person i on i.ID=b.MainRepair ");
            strSql.Append("left join base_RepairType j on j.FlagDel=0 and j.ID=a.RepairTypeId ");
            strSql.Append("left join base_PayType k on k.FlagDel=0 and k.ID=e.PayType ");
            strSql.Append("left join base_Settlement l on l.FlagDel=0 and l.ID=d.SettlementTypeId ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.IntentionDate desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
        #region 维修进度
        public DataSet GetRepairProgressList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select a.ID as IntentionId,a.IntentionCode,a.CustName,a.MachineModelId,g.MachineModel,a.MachineCode,a.IntentionDate,b.ID as AssignmentId,b.AssignmentCode ");
            strSql.Append(",c.ProcedureId,d.ProcedureName,f.ScheduleType,f.ScheduleDate ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join Repair_Assignment b on b.FlagDel=0 and b.IntentionId=a.ID left join Repair_Assignment_Procedure c on c.AssignmentId=b.ID and c.FlagDel=0 left join base_Procedure d on d.FlagDel=0 and c.ProcedureId=d.ID ");
            strSql.Append("left join repair_Schedule f on f.FlagDel=0 and f.AssignmentId=c.AssignmentId and f.AssignmentProcedureId=c.ID ");
            strSql.Append("left join base_MachineModel g on g.FlagDel=0 and g.ID=a.MachineModelId ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.ID,b.ID,d.ProcedureName,f.ScheduleDate asc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
        #region 维修项目统计
        public DataSet GetItemStatistics(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CAST(a.AssignmentDate as DATE) as AssignmentDate,c.ProcedureName,d.CustName,d.MachineModelId,e.MachineModel,d.MachineCode,d.RepairTypeId,f.RepairTypeName from Repair_Assignment a ");
            strSql.Append("left join Repair_Assignment_Procedure b on a.ID=b.AssignmentId and b.FlagDel=0 ");
            strSql.Append("left join base_Procedure c on b.ProcedureId=c.ID ");
            strSql.Append("left join repair_Intention d on a.IntentionId=d.ID ");
            strSql.Append("left join base_MachineModel e on d.MachineModelId=e.ID ");
            strSql.Append("left join base_RepairType f on d.RepairTypeId=f.ID ");
            strSql.Append("where a.FlagDel=0 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.AssignmentDate desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
        #region 维修零件统计
        public DataSet GetPartStatistics(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.CustName,b.MachineModelId,c.MachineModel,b.MachineCode,b.RepairTypeId,d.RepairTypeName from repair_Scheme a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID  ");
            strSql.Append("left join base_RepairType d on b.RepairTypeId=d.ID ");
            strSql.Append("where a.SchemeDate is not null and a.FlagDel=0 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.SchemeDate desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
        public DataSet GetRepairerRewardDetail(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as IntentionId,a.IntentionCode,a.CustName,b.ID as MachineModelId,b.MachineModel,a.MachineCode,a.IntentionDate,a.RepairContent,c.ID as AssignmentId,c.AssignmentCode,d.ID as AssignmentProcedureId,e.ID as ProcedureId,f.ID as ProcedureMachineNatId,b.MachineLevel,d.ProcedureId,e.ProcedureName,d.Num,case MachineLevel when 1 then f.MachineLevel10 when 2 then f.MachineLevel20 when 3 then f.MachineLevel30 when 4 then f.MachineLevel40 when 5 then f.MachineLevel50 end as AllNat");
            strSql.Append(",c.MainRepair,c.AssistRepair,g.PerName,h.Repairs,i.PersonsAssess,j.Assess,k.ScheduleDate,d.AuditState,d.AllNat_Audit,d.AllNat as AllNat_Assignment ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join base_MachineModel b on b.FlagDel=0 and a.MachineModelId=b.ID ");
            strSql.Append("left join Repair_Assignment c on c.FlagDel=0 and c.IntentionId=a.ID ");
            strSql.Append("left join Repair_Assignment_Procedure d on d.FlagDel=0 and d.AssignmentId=c.ID and d.ID in(select distinct AssignmentProcedureId from repair_Schedule where FlagDel=0 and ScheduleType=3) ");
            strSql.Append("left join base_Procedure e on e.FlagDel=0 and d.ProcedureId=e.ID ");
            strSql.Append("left join base_ProcedureMachineNat f on f.FlagDel=0 and e.ID=f.ProcedureId ");
            strSql.Append("left join sys_Person g on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(g.ID as varchar)+',%' ");
            strSql.Append("left join (select ','+CAST(MainRepair as varchar)+','+AssistRepair+',' as Repairs,ID from Repair_Assignment where FlagDel=0) h on h.ID=c.ID ");
            
            //strSql.Append("left join (select a.ID as AssignmentId,SUM(c.Assess) as PersonsAssess from Repair_Assignment a left join sys_Person b on ','+CAST(a.MainRepair as varchar)+','+a.AssistRepair+',' like '%,'+CAST(b.ID as varchar)+',%' left join (select ID,PersonId,CreateDate,Assess from base_PersonAssess where FlagDel=0 and CreateDate in(select MAX(e.CreateDate) from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId)) c on c.PersonId=b.ID where a.FlagDel=0 group by a.ID) i on i.AssignmentId=c.ID ");
            
            //strSql.Append("left join (select ID,PersonId,CreateDate,Assess from base_PersonAssess where FlagDel=0 and CreateDate in(select MAX(e.CreateDate) from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId)) j on g.ID=j.PersonId ");
            strSql.Append("left join(select a.ID as AssignmentId,d.ID as AssignmentProcedureId,SUM(c.Assess) as PersonsAssess from Repair_Assignment a left join Repair_Assignment_Procedure d on d.AssignmentId=a.ID and d.FlagDel=0 left join sys_Person b on b.FlagDel=0 and ','+CAST(a.MainRepair as varchar)+','+a.AssistRepair+',' like '%,'+CAST(b.ID as varchar)+',%' left join (select a.ID,a.PersonId,a.CreateDate,a.Assess,b.AssignmentProcedureId from base_PersonAssess a left join (select MAX(e.CreateDate) as CreateDate,e.PersonId,a.AssignmentProcedureId from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on d.FlagDel=0 and ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId,a.AssignmentProcedureId) b on b.PersonId=a.PersonId and b.CreateDate=a.CreateDate where a.FlagDel=0 and b.AssignmentProcedureId is not null ) c on c.PersonId=b.ID and c.AssignmentProcedureId=d.ID where a.FlagDel=0 group by a.ID,d.ID ) i on i.AssignmentId=c.ID and i.AssignmentProcedureId=d.ID ");
            strSql.Append("left join (select a.ID,a.PersonId,a.CreateDate,a.Assess,b.AssignmentProcedureId from base_PersonAssess a left join (select MAX(e.CreateDate) as CreateDate,e.PersonId,a.AssignmentProcedureId from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on d.FlagDel=0 and ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId,a.AssignmentProcedureId) b on b.PersonId=a.PersonId and b.CreateDate=a.CreateDate where a.FlagDel=0 and b.AssignmentProcedureId is not null ) j on g.ID=j.PersonId and j.AssignmentProcedureId=d.ID ");
            strSql.Append("left join (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) k on k.AssignmentProcedureId=d.ID ");
            strSql.Append("where a.FlagDel=0 and d.ID is not null ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetRepairerReward(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.PerId,a.PerName,sum(case when a.AllNat is null then 0 else case when PersonsAssess is null then 0 else case when a.AuditState=1 then a.AllNat_Audit*a.Assess/a.PersonsAssess else 0 end end end) as Nat,COUNT(1) as num,COUNT(case AllNat_Audit when 1 then 1 else null end) as UnAudit,COUNT(case when AllNat_Audit is null then 1 else null end) as History,a.DepName from ( ");
            strSql.Append("select a.ID as IntentionId,a.IntentionCode,a.CustName,b.ID as MachineModelId,b.MachineModel,a.MachineCode,a.IntentionDate,c.ID as AssignmentId,c.AssignmentCode,d.ID as AssignmentProcedureId,e.ID as ProcedureId,f.ID as ProcedureMachineNatId,b.MachineLevel,e.ProcedureName,d.Num,case MachineLevel when 1 then f.MachineLevel10 when 2 then f.MachineLevel20 when 3 then f.MachineLevel30 when 4 then f.MachineLevel40 when 5 then f.MachineLevel50 end as AllNat");
            strSql.Append(",c.MainRepair,c.AssistRepair,g.ID as PerId,g.PerName,h.Repairs,i.PersonsAssess,j.Assess,k.ScheduleDate,d.AuditState,d.AllNat_Audit,d.AllNat as AllNat_Assignment,l.DepName ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join base_MachineModel b on b.FlagDel=0 and a.MachineModelId=b.ID ");
            strSql.Append("left join Repair_Assignment c on c.FlagDel=0 and c.IntentionId=a.ID ");
            strSql.Append("left join Repair_Assignment_Procedure d on d.FlagDel=0 and d.AssignmentId=c.ID and d.ID in(select distinct AssignmentProcedureId from repair_Schedule where FlagDel=0 and ScheduleType=3) ");
            strSql.Append("left join base_Procedure e on e.FlagDel=0 and d.ProcedureId=e.ID ");
            strSql.Append("left join base_ProcedureMachineNat f on f.FlagDel=0 and e.ID=f.ProcedureId ");
            strSql.Append("left join sys_Person g on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(g.ID as varchar)+',%' ");
            strSql.Append("left join (select ','+CAST(MainRepair as varchar)+','+AssistRepair+',' as Repairs,ID from Repair_Assignment where FlagDel=0) h on h.ID=c.ID ");
            //strSql.Append("left join (select a.ID as AssignmentId,SUM(c.Assess) as PersonsAssess from Repair_Assignment a left join sys_Person b on ','+CAST(a.MainRepair as varchar)+','+a.AssistRepair+',' like '%,'+CAST(b.ID as varchar)+',%' left join (select ID,PersonId,CreateDate,Assess from base_PersonAssess where FlagDel=0 and CreateDate in(select MAX(e.CreateDate) from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId)) c on c.PersonId=b.ID where a.FlagDel=0 group by a.ID) i on i.AssignmentId=c.ID ");
            //strSql.Append("left join (select ID,PersonId,CreateDate,Assess from base_PersonAssess where FlagDel=0 and CreateDate in(select MAX(e.CreateDate) from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId)) j on g.ID=j.PersonId ");
            strSql.Append("left join(select a.ID as AssignmentId,d.ID as AssignmentProcedureId,SUM(c.Assess) as PersonsAssess from Repair_Assignment a left join Repair_Assignment_Procedure d on d.AssignmentId=a.ID and d.FlagDel=0 left join sys_Person b on b.FlagDel=0 and ','+CAST(a.MainRepair as varchar)+','+a.AssistRepair+',' like '%,'+CAST(b.ID as varchar)+',%' left join (select a.ID,a.PersonId,a.CreateDate,a.Assess,b.AssignmentProcedureId from base_PersonAssess a left join (select MAX(e.CreateDate) as CreateDate,e.PersonId,a.AssignmentProcedureId from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on d.FlagDel=0 and ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId,a.AssignmentProcedureId) b on b.PersonId=a.PersonId and b.CreateDate=a.CreateDate where a.FlagDel=0 and b.AssignmentProcedureId is not null ) c on c.PersonId=b.ID and c.AssignmentProcedureId=d.ID where a.FlagDel=0 group by a.ID,d.ID ) i on i.AssignmentId=c.ID and i.AssignmentProcedureId=d.ID ");
            strSql.Append("left join (select a.ID,a.PersonId,a.CreateDate,a.Assess,b.AssignmentProcedureId from base_PersonAssess a left join (select MAX(e.CreateDate) as CreateDate,e.PersonId,a.AssignmentProcedureId from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on d.FlagDel=0 and ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId,a.AssignmentProcedureId) b on b.PersonId=a.PersonId and b.CreateDate=a.CreateDate where a.FlagDel=0 and b.AssignmentProcedureId is not null ) j on g.ID=j.PersonId and j.AssignmentProcedureId=d.ID ");
            strSql.Append("left join (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) k on k.AssignmentProcedureId=d.ID ");
            strSql.Append("left join sys_Department l on g.DepId=l.ID and l.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and d.ID is not null ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" )a  ");
            strSql.Append(" group by a.PerId,a.PerName,a.DepName ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetRepairerReward_Audit(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as IntentionId,a.IntentionCode,a.CustName,b.ID as MachineModelId,b.MachineModel,a.MachineCode,a.IntentionDate,c.ID as AssignmentId,c.AssignmentCode,d.ID as AssignmentProcedureId,e.ID as ProcedureId,f.ID as ProcedureMachineNatId,b.MachineLevel,d.ProcedureId,e.ProcedureName,d.Num,case MachineLevel when 1 then f.MachineLevel10 when 2 then f.MachineLevel20 when 3 then f.MachineLevel30 when 4 then f.MachineLevel40 when 5 then f.MachineLevel50 end as AllNat");
            strSql.Append(",c.MainRepair,c.AssistRepair,k.ScheduleDate,d.AuditorId,d.AuditDate,d.AuditState,d.AllNat_Audit,d.AllNat as AllNat_Assignment,l.PerName as AuditorName,a.RepairContent ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join base_MachineModel b on b.FlagDel=0 and a.MachineModelId=b.ID ");
            strSql.Append("left join Repair_Assignment c on c.FlagDel=0 and c.IntentionId=a.ID ");
            strSql.Append("left join Repair_Assignment_Procedure d on d.FlagDel=0 and d.AssignmentId=c.ID and d.ID in(select distinct AssignmentProcedureId from repair_Schedule where FlagDel=0 and ScheduleType=3) ");
            strSql.Append("left join base_Procedure e on e.FlagDel=0 and d.ProcedureId=e.ID ");
            strSql.Append("left join base_ProcedureMachineNat f on f.FlagDel=0 and e.ID=f.ProcedureId ");
            //strSql.Append("left join sys_Person g on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(g.ID as varchar)+',%' ");
            //strSql.Append("left join (select ','+CAST(MainRepair as varchar)+','+AssistRepair+',' as Repairs,ID from Repair_Assignment where FlagDel=0) h on h.ID=c.ID ");
            //strSql.Append("left join (select a.ID as AssignmentId,SUM(c.Assess) as PersonsAssess from Repair_Assignment a left join sys_Person b on ','+CAST(a.MainRepair as varchar)+','+a.AssistRepair+',' like '%,'+CAST(b.ID as varchar)+',%' left join (select ID,PersonId,CreateDate,Assess from base_PersonAssess where FlagDel=0 and CreateDate in(select MAX(e.CreateDate) from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId)) c on c.PersonId=b.ID where a.FlagDel=0 group by a.ID) i on i.AssignmentId=c.ID ");
            //strSql.Append("left join (select ID,PersonId,CreateDate,Assess from base_PersonAssess where FlagDel=0 and CreateDate in(select MAX(e.CreateDate) from (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) a left join Repair_Assignment_Procedure b on a.AssignmentProcedureId=b.ID and b.FlagDel=0 left join Repair_Assignment c on c.FlagDel=0 and c.ID=b.AssignmentId left join sys_Person d on ','+CAST(c.MainRepair as varchar)+','+c.AssistRepair+',' like '%,'+CAST(d.ID as varchar)+',%' left join base_PersonAssess e on e.FlagDel=0 and e.PersonId=d.ID where a.ScheduleDate>=e.CreateDate group by e.PersonId)) j on g.ID=j.PersonId ");
            strSql.Append("left join (select AssignmentProcedureId,MAX(ScheduleDate) as ScheduleDate from repair_Schedule where FlagDel=0 and ScheduleType=3 group by AssignmentProcedureId ) k on k.AssignmentProcedureId=d.ID ");
            strSql.Append("left join sys_Person l on d.AuditorId=l.ID ");
            strSql.Append("where a.FlagDel=0 and d.ID is not null ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.OperaTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetRepairerSaturation(string strWhere_Procedure, string strWhere_Repairer, int RepairDepId,string lastDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as PerId,a.PerName,a.DepId,a.DepName,a.SupList as DepSupList,b.RepairProcedureNum,b.RepairProcedureNum_NotStart,b.RepairProcedureNum_Start,b.RepairProcedureNum_Pause,b.RepairProcedureNum_Complete,(b.RepairProcedureNum_Start+b.RepairProcedureNum_Pause)as RepairProcedureNum_Uncomplete ");
            strSql.Append("from (select a.ID,a.PerName,b.ID as DepId,b.DepName,b.SupList from sys_Person a left join sys_Department b on b.FlagDel=0 and b.ID=a.DepId where a.FlagDel=0 and b.ID is not null and (b.ID=" + RepairDepId + " or ','+b.SupList like '%," + RepairDepId + ",%'))a ");
            strSql.Append("left join ( select b.RepairerId,b.PerName,COUNT(1) as RepairProcedureNum,COUNT(case when b.ScheduleType=1 then 1 else null end) as RepairProcedureNum_Start,COUNT(case when b.ScheduleType=2 then 2 else null end) as RepairProcedureNum_Pause,COUNT(case when b.ScheduleType=3 then 3 else null end) as RepairProcedureNum_Complete,COUNT(case when b.ScheduleType is null then 1 else null end) as RepairProcedureNum_NotStart from(select a.ID as IntentionId,a.IntentionCode,a.CustName,a.MachineModelId,b.MachineModel,a.MachineCode,c.ID as AssignmentId,c.AssignmentCode,c.AssignmentDate,c.MainRepair,c.AssistRepair,d.ID as AssignmentProcedureId,e.ID as ProcedureId,e.ProcedureName,f.ID as RepairerId,f.PerName,g.ID as ScheduleId_New,g.ScheduleDate,g.ScheduleType ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join Repair_Assignment c on c.FlagDel=0 and c.IntentionId=a.ID ");
            strSql.Append("left join Repair_Assignment_Procedure d on d.FlagDel=0 and d.AssignmentId=c.ID ");
            strSql.Append("left join base_Procedure e on e.FlagDel=0 and d.ProcedureId=e.ID ");
            strSql.Append("left join sys_Person f on ','+RTRIM(c.MainRepair)+','+c.AssistRepair+',' like '%,'+RTRIM(f.ID)+',%' ");
            strSql.Append("left join (select * from repair_Schedule where ID in(select MAX(ID) from repair_Schedule where FlagDel=0 and ScheduleDate<"+lastDay+" group by AssignmentProcedureId )) g on g.FlagDel=0 and g.AssignmentProcedureId=d.ID ");
            strSql.Append("where a.FlagDel=0 and c.ID is not null ");
            //and (c.AssignmentDate>='"+year+"-01-01 00:00:00' or c.AssignmentDate<'"+year+"-01-01 00:00:00' and (g.ScheduleType !=3 or g.ID is null))
            if (strWhere_Procedure != "")
            {
                strSql.Append(strWhere_Procedure);
            }
            strSql.Append(") b group by b.RepairerId,b.PerName )b on a.ID=b.RepairerId ");
            strSql.Append("where 1=1 ");
            if (strWhere_Repairer != "")
            {
                strSql.Append(strWhere_Repairer);
            }
            strSql.Append("order by a.DepId,a.ID ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetRepairerSaturationDetail(string strWhere_Procedure,string lastDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as IntentionId,a.IntentionCode,a.CustName,a.MachineModelId,b.MachineModel,a.MachineCode,a.IntentionDate,c.ID as AssignmentId,c.AssignmentCode,c.AssignmentDate,c.MainRepair,c.AssistRepair,d.ID as AssignmentProcedureId,e.ID as ProcedureId,e.ProcedureName,f.ID as RepairerId,f.PerName,g.ID as ScheduleId_New,g.ScheduleDate,g.ScheduleType ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID and b.FlagDel=0 ");
            strSql.Append("left join Repair_Assignment c on c.FlagDel=0 and c.IntentionId=a.ID ");
            strSql.Append("left join Repair_Assignment_Procedure d on d.FlagDel=0 and d.AssignmentId=c.ID ");
            strSql.Append("left join base_Procedure e on e.FlagDel=0 and d.ProcedureId=e.ID ");
            strSql.Append("left join sys_Person f on ','+RTRIM(c.MainRepair)+','+c.AssistRepair+',' like '%,'+RTRIM(f.ID)+',%' ");
            strSql.Append("left join sys_Department h on h.FlagDel=0 and f.DepId=h.ID ");
            strSql.Append("left join (select * from repair_Schedule where ID in(select MAX(ID) from repair_Schedule where FlagDel=0 and ScheduleDate<"+lastDay+" group by AssignmentProcedureId )) g on g.FlagDel=0 and g.AssignmentProcedureId=d.ID ");
            strSql.Append("where a.FlagDel=0 and c.ID is not null ");
            //and (c.AssignmentDate>='"+year+"-01-01 00:00:00' or c.AssignmentDate<'"+year+"-01-01 00:00:00' and (g.ScheduleType !=3 or g.ID is null))
            if (strWhere_Procedure != "")
            {
                strSql.Append(strWhere_Procedure);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetIntentionNum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(1) as IntentionNum,COUNT(b.IntentionId) as IntentionNum_Assignment from repair_Intention a ");
            strSql.Append("left join (select distinct IntentionId from Repair_Assignment where FlagDel=0) b on b.IntentionId=a.ID ");
            strSql.Append("where a.FlagDel=0 and a.FlagResult=1 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetRepairProgress_Intention(string strWhere, int perId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.ID as IntentionId,a.IntentionCode,a.IntentionDate,a.IntentionType,a.RepairTypeId,a.RepairAdress,a.Linkman,a.MachineModel,a.MachineCode,a.CustTypeId,a.CustName,a.AssignmentType,case a.AssignmentType when 1 then b.startDate when 2 then c.startDate else null end as StartDate, ");
            strSql.Append("case a.AssignmentType when 1 then b.completeDate else null end as CompleteDate, ");
            strSql.Append("left(a.MainRepairs,len(a.MainRepairs)-1) as MainRepairs,left(a.MainRepairsDep,len(a.MainRepairsDep)-1) as MainRepairsDep,a.ServerManagerName,a.ActualEnterDate,a.ActualLeaveDate,a.ExpectLeaveDate,a.SchemeId,a.SchemeDate,a.SchemeFee,a.SchemeDate_predict,a.RepairContent,case a.ProceduresName when '' then '工序异常' else left(a.ProceduresName,len(a.ProceduresName)-1) end as ProceduresName,a.SettlementDate,a.SettlementFee_rebate,a.ReceiveFee_All,a.ReceiveDate_New,a.pull,a.repair,a.push ");
            strSql.Append("from(select distinct b.ID,b.IntentionCode,b.IntentionType,b.RepairTypeId,b.RepairAdress,b.IntentionDate,b.Linkman,c.MachineModel,b.MachineCode,b.CustTypeId,b.CustName,b.OperaTime,b.ActualEnterDate,b.ActualLeaveDate,b.ExpectLeaveDate,b.RepairContent,j.ID as SchemeId,j.SchemeCode,j.SchemeDate,j.SchemeFee,j.SchemeDate_predict, ");
            strSql.Append("f.AssignmentProcedureCount,g.CompleteProcedureCount,h.RepairingProcedureCount, ");
            strSql.Append("case when AssignmentProcedureCount=ISNULL(CompleteProcedureCount,0) then 1 when ISNULL(RepairingProcedureCount,0)>0 then 2 else 0 end as AssignmentType, ");
            strSql.Append("(select PerName+',' from (select distinct b.PerName from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID where aa.FlagDel=0 and aa.IntentionId=a.IntentionId) a for xml path('')) ");
            strSql.Append("as MainRepairs, ");
            strSql.Append("(select cast(ID as varchar)+',' from (select distinct c.ID from Repair_Assignment aa left join sys_Person b on aa.MainRepair=b.ID ");
            strSql.Append("left join sys_Department c on c.FlagDel=0 and c.ID=b.DepId where aa.FlagDel=0 and aa.IntentionId=a.IntentionId) a for xml path('')) as MainRepairsDep,i.PerName as ServerManagerName,k.ProceduresName,l.SettlementDate,l.SettlementFee_rebate,m.ReceiveDate_New,m.ReceiveFee_All,n.pull,n.repair,n.push ");
            strSql.Append("from repair_Intention b ");
            strSql.Append("left join repair_Assignment a on a.IntentionId=b.ID and a.FlagDel=0 ");
            strSql.Append("left join base_MachineModel c on b.MachineModelId=c.ID and c.FlagDel=0 ");
            strSql.Append("left join (select COUNT(1) as AssignmentProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 ");
            strSql.Append("where d.FlagDel=0 group by d.IntentionId) f on f.IntentionId=b.ID left join (select COUNT(1) as CompleteProcedureCount,d.IntentionId from Repair_Assignment d ");
            strSql.Append("left join Repair_Assignment_Procedure e on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where f.ScheduleType=3 ");
            strSql.Append("and d.FlagDel=0 group by d.IntentionId) g on g.IntentionId=b.ID left join  (select COUNT(1) as RepairingProcedureCount,d.IntentionId from Repair_Assignment d left join Repair_Assignment_Procedure e ");
            strSql.Append("on e.AssignmentId=d.ID and e.FlagDel=0 left join repair_Schedule f on f.AssignmentProcedureId=e.ID and f.FlagDel=0 and f.FlagNew=0 where d.FlagDel=0 and f.ScheduleType>0 group by d.IntentionId) h ");
            strSql.Append("on h.IntentionId=b.ID ");
            strSql.Append("left join (select ctrlperid from  v_sys_PersonCtrl where PerId=" + perId + ") d on ','+cast(b.OperaId as varchar(20))+',' like '%,'+CAST(d.ctrlperid as varchar(20))+',%' ");
            strSql.Append("left join sys_Person i on b.BusinessPerName=i.ID ");
            strSql.Append("left join repair_Scheme j on j.FlagDel=0 and b.ID=j.IntentionId ");
            strSql.Append("left join (select a.ID as IntentionId,( ");
            strSql.Append("select d.ProcedureName+',' from Repair_Assignment b ");
            strSql.Append("left join Repair_Assignment_Procedure c on c.FlagDel=0 and c.AssignmentId=b.ID ");
            strSql.Append("left join base_Procedure d on c.ProcedureId=d.ID ");
            strSql.Append("where b.FlagDel=0 and b.IntentionId=a.ID for xml path('') ");
            strSql.Append(") as ProceduresName from repair_Intention a ");
            strSql.Append("where a.FlagDel=0) k on k.IntentionId=b.ID ");
            strSql.Append("left join repair_SettlementList l on l.IntentionId=b.ID ");
            strSql.Append("left join (select IntentionId,MAX(ReceiveDate) as ReceiveDate_New,SUM(ReceiveFee) as ReceiveFee_All from repair_ReceiveFee where FlagDel=0 group by IntentionId) m on m.IntentionId=b.ID ");
            strSql.Append("left join (select a.IntentionId,case when a.flagPull=0 then 1 when pull=1 then 1 else 0 end as pull,case when a.flagPush=0 then 1 when push=1 then 1 else 0 end as push,case when a.flagRepair=0 then 1 when repair=1 then 1 else 0 end as repair from ( ");
            strSql.Append("select a.IntentionId,count(case when a.RepairProcedure_Group=1 then 1 else null end) as flagPull,count(case when a.RepairProcedure_Group=2 then 1 else null end) as flagPush,count(case when a.RepairProcedure_Group=3 then 1 else null end) as flagRepair,COUNT(case when a.RepairProcedure_Group=1 and a.FlagUnComplete_Group=0 then 1 else null end) as pull,COUNT(case when a.RepairProcedure_Group=2 and a.FlagUnComplete_Group=0 then 1 else null end) as push,COUNT(case when a.RepairProcedure_Group=3 and a.FlagUnComplete_Group=0 then 1 else null end) as repair from ( ");
            strSql.Append("select a.IntentionId,a.RepairProcedure_Group,COUNT(1) as GroupNum,COUNT(case when a.ScheduleType !=3 or a.ScheduleType is null  then 1 else null end) as FlagUnComplete_Group from ( ");
            strSql.Append("select a.ID as IntentionId,b.ID as AssignmentId,c.ID as AssignmentProcedureId,c.ProcedureId,d.ProcedureName,d.SupId,d.SupList,e.ScheduleType,case when (d.SupList like '0,1,2,7,%' or d.ID=7) then 1 when (d.SupList like '0,1,2,68,%' or d.ID=68) then 2 else case when d.ID is null then 0 else 3 end end as RepairProcedure_Group  from repair_Intention a ");
            strSql.Append("left join Repair_Assignment b on b.FlagDel=0 and b.IntentionId=a.ID ");
            strSql.Append("left join Repair_Assignment_Procedure c on c.AssignmentId=b.ID and c.FlagDel=0 left join base_Procedure d on c.ProcedureId=d.ID left join (select * from repair_Schedule where FlagDel=0 and FlagNew=0) e on e.AssignmentProcedureId=c.ID ");
            strSql.Append("where a.FlagDel=0 and a.FlagResult=1) a group by a.IntentionId,a.RepairProcedure_Group) a group by a.IntentionId) a) n on b.ID=n.IntentionId ");
            strSql.Append("where b.FlagDel=0 and d.CtrlPerId is not null ");
            strSql.Append(")as a ");
            strSql.Append("left join (select min(ScheduleDate) as startDate,max(ScheduleDate) as completeDate,b.IntentionId from repair_Schedule a ");
            strSql.Append("left join Repair_Assignment b on a.AssignmentId=b.ID and b.FlagDel=0 where a.FlagDel=0 and b.ID is not null group by b.IntentionId) ");
            strSql.Append("as b on a.ID=b.IntentionId and a.AssignmentType=1 ");
            strSql.Append("left join (select min(ScheduleDate) as startDate,b.IntentionId from repair_Schedule a left join Repair_Assignment b on a.AssignmentId=b.ID and b.FlagDel=0 ");
            strSql.Append("where a.FlagDel=0 and b.ID is not null group by b.IntentionId) ");
            strSql.Append("as c on a.ID=c.IntentionId and a.AssignmentType=2 ");
            strSql.Append("where 1=1 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append("order by a.OperaTime desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetRepairIntentionStatistics(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.RepairTypeId,b.RepairTypeName, ");
            strSql.Append("COUNT(1) as IntentionNum, ");
            //strSql.Append("COUNT(case when a.CustTypeId=1 and a.RepairAdress='现场' then 1 else null end) as IntentionNum_OutCust_Locale, ");
            //strSql.Append("COUNT(case when a.CustTypeId=1 and a.RepairAdress='车间' then 1 else null end) as IntentionNum_OutCust_Shop, ");
            //strSql.Append("COUNT(case when a.CustTypeId=1 and a.RepairAdress='车间' and a.ActualLeaveDate is not null then 1 else null end) as IntentionNum_OutCust_Shop_Leave, ");
            //strSql.Append("COUNT(case when a.CustTypeId=2 then 1 else null end) as IntentionNum_InDep ");
            strSql.Append("COUNT(case when a.ActualLeaveDate is not null then 1 else null end) as IntentionNum_Leave ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join base_RepairType b on a.RepairTypeId=b.ID ");
            strSql.Append("left join base_MachineModel c on a.MachineModelId=c.ID ");
            strSql.Append("where a.FlagDel=0 and a.FlagResult=1 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append("group by a.RepairTypeId,b.RepairTypeName ");
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            dt.TableName = "RepairTypeStatistics";
            StringBuilder strSql_All = new StringBuilder();
            strSql_All.Append("select COUNT(1) as IntentionNum, ");
            //strSql_All.Append("COUNT(case when a.FlagResult=1 then 1 else null end) as IntentionNum_Agree, ");
            //strSql_All.Append("COUNT(case when a.FlagResult=0 then 1 else null end) as IntentionNum_Uncertain, ");
            //strSql_All.Append("COUNT(case when a.FlagResult=2 then 1 else null end) as IntentionNum_NotAgree, ");
            //strSql_All.Append("COUNT(case when a.CustTypeId=1 then 1 else null end) as IntentionNum_OutCust, ");
            //strSql_All.Append("COUNT(case when a.CustTypeId=1 and a.FlagResult=1 then 1 else null end) as IntentionNum_OutCust_Agree, ");
            //strSql_All.Append("COUNT(case when a.CustTypeId=1 and a.FlagResult=1 and a.RepairAdress='现场' then 1 else null end) as IntentionNum_OutCust_Agree_Locale, ");
            //strSql_All.Append("COUNT(case when a.CustTypeId=1 and a.FlagResult=1 and a.RepairAdress='车间' then 1 else null end) as IntentionNum_Shop, ");
            //strSql_All.Append("COUNT(case when a.CustTypeId=1 and a.FlagResult=1 and a.ActualLeaveDate is not null then 1 else null end) as IntentionNum_OutCust_Leave, ");
            //strSql_All.Append("COUNT(case when a.CustTypeId=2 then 1 else null end) as IntentionNum_InDep, ");
            //strSql_All.Append("COUNT(case when a.CustTypeId=2 and a.FlagResult=1 then 1 else null end) as IntentionNum_InDep_Agree, ");
            //strSql_All.Append("count(case when a.FlagResult=1 and a.RepairMode=1 then 1 else null end) as IntentionNum_Whole, ");
            //strSql_All.Append("count(case when a.FlagResult=1 and a.FlagLocale=1 and a.RepairMode=1 then 1 else null end) as IntentionNum_Whole_Locale, ");
            //strSql_All.Append("count(case when a.FlagResult=1 and a.RepairMode=1 and a.ActualLeaveDate is not null then 1 else null end) as IntentionNum_Whole_Leave, ");
            //strSql_All.Append("count(case when a.FlagResult=1 and a.FlagENG=1 then 1 else null end) as IntentionNum_ENG, ");
            //strSql_All.Append("count(case when a.FlagResult=1 and a.FlagENG=1 and a.FlagLocale=1 then 1 else null end) as IntentionNum_ENG_Locale, ");
            strSql_All.Append("count(case when a.FlagResult=1 and a.FlagENG=1 and a.ActualLeaveDate is not null then 1 else null end) as IntentionNum_ENG_Leave, ");
            strSql_All.Append("count(case when a.FlagResult=1 and a.FlagENG=1 and a.ActualLeaveDate is not null and a.FlagLocale=1 then 1 else null end) as IntentionNum_ENG_Leave_Locale, ");
            //strSql_All.Append("count(case when a.FlagResult=1 and a.FlagPPM=1 then 1 else null end) as IntentionNum_PPM, ");
            //strSql_All.Append("count(case when a.FlagResult=1 and a.FlagPPM=1 and a.FlagLocale=1 then 1 else null end) as IntentionNum_PPM_Locale, ");
            strSql_All.Append("count(case when a.FlagResult=1 and a.FlagPPM=1 and a.ActualLeaveDate is not null then 1 else null end) as IntentionNum_PPM_Leave, ");
            strSql_All.Append("count(case when a.FlagResult=1 and a.FlagPPM=1 and a.ActualLeaveDate is not null and a.FlagLocale=1 then 1 else null end) as IntentionNum_PPM_Leave_Locale, ");
            strSql_All.Append("count(case when a.FlagResult=1 and a.ActualEnterDate is not null and a.RepairMode=1 then 1 else null end) as IntentionNum_Whole_Enter, ");
            strSql_All.Append("count(case when a.FlagResult=1 and a.ActualEnterDate is not null and a.RepairMode=1 and a.FlagLocale=1 then 1 else null end) as IntentionNum_Whole_Enter_Locale ");
            strSql_All.Append("from repair_Intention a ");
            strSql_All.Append("left join base_MachineModel c on a.MachineModelId=c.ID ");
            strSql_All.Append("where a.FlagDel=0 ");
            if (strWhere != "")
            {
                strSql_All.Append(strWhere);
            }
            DataTable dt_All = DbHelperSQL.Query(strSql_All.ToString()).Tables[0];
            dt_All.TableName = "IntentionStatistics";
            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            ds.Tables.Add(dt_All.Copy());
            return ds;
        }
        public int SaveRepairReward_Audit(Model.Repair.Repair_Assignment_Procedure model) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment_Procedure set AuditorId=@AuditorId,AuditDate=@AuditDate,AuditState=@AuditState,AllNat_Audit=@AllNat_Audit,AuditOpinion=@AuditOpinion where ID=@ID and FlagDel=0 ");
            SqlParameter[] sqlparameters = { 
                new SqlParameter("@AuditorId",SqlDbType.Int,4),
                new SqlParameter("@AuditDate",SqlDbType.DateTime),
                new SqlParameter("@AuditState",SqlDbType.Int,4),
                new SqlParameter("@AllNat_Audit",SqlDbType.Decimal,18),
                new SqlParameter("@AuditOpinion",SqlDbType.NVarChar,200),
                new SqlParameter("@ID",SqlDbType.Int,4)
                                           };
            sqlparameters[0].Value = model.AuditorId;
            sqlparameters[1].Value = model.AuditDate;
            sqlparameters[2].Value = model.AuditState;
            sqlparameters[3].Value = model.AllNat_Audit;
            sqlparameters[4].Value = model.AuditOpinion;
            sqlparameters[5].Value = model.ID;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), sqlparameters);
        }
        public int SaveRepairReward_CancelAudit(string IDStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment_Procedure set AuditorId=null,AuditDate=null,AuditState=null,AllNat_Audit=null,AuditOpinion=null where ID in ("+IDStr+") and FlagDel=0 ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        public int SaveRepairReward_MultipleAudit(string IDStr, Model.Repair.Repair_Assignment_Procedure model) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Repair_Assignment_Procedure set AuditorId=@AuditorId,AuditDate=@AuditDate,AuditState=@AuditState,AuditOpinion=@AuditOpinion,AllNat_Audit=@AllNat_Audit where ID in (" + IDStr + ") and FlagDel=0 ");
            SqlParameter[] sqlparameters ={
                new SqlParameter("AuditorId",SqlDbType.Int,4),
                new SqlParameter("AuditDate",SqlDbType.DateTime),
                new SqlParameter("AuditState",SqlDbType.Int,4),
                new SqlParameter("AuditOpinion",SqlDbType.NVarChar,200),
                new SqlParameter("AllNat_Audit",SqlDbType.Decimal,18)
                                         };
            sqlparameters[0].Value = model.AuditorId;
            sqlparameters[1].Value = model.AuditDate;
            sqlparameters[2].Value = model.AuditState;
            sqlparameters[3].Value = model.AuditOpinion;
            sqlparameters[4].Value = model.AllNat_Audit;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), sqlparameters);
        }
        public DataSet GetWeeks(int year,string FirstDay,string FirstDayOfNextMonth) {
            string strSql = "select case when DATEPART(WEEKDAY,'" + FirstDay + "')=1 or DATEPART(MONTH,'" + FirstDay + "')=1 then DATEDIFF(WEEK,'" + FirstDay + "',DATEADD(DAYOFYEAR,DATEPART(DAYOFYEAR,'" + FirstDayOfNextMonth + "')-2,'" + year + "-01-01'))+1 else DATEDIFF(WEEK,'" + FirstDay + "',DATEADD(DAYOFYEAR,DATEPART(DAYOFYEAR,'" + FirstDayOfNextMonth + "')-2,'" + year + "-01-01')) end AS Weeks,case when DATEPART(WEEKDAY,'" + FirstDay + "')=1 or DATEPART(MONTH,'" + FirstDay + "')=1 then DATEPART(WEEK,'" + FirstDay + "') else DATEPART(WEEK,'" + FirstDay + "')+1 end as FirstWeekOfYear,dbo.Week2Day(" + year + ",case when DATEPART(WEEKDAY,'" + FirstDay + "')=1 or DATEPART(MONTH,'" + FirstDay + "')=1 then DATEPART(WEEK,'" + FirstDay + "') else DATEPART(WEEK,'" + FirstDay + "')+1 end) as FirstWeekDate,CONVERT(Date,DATEADD(DAYOFYEAR,DATEPART(DAYOFYEAR,'" + FirstDayOfNextMonth + "')-2,'" + year + "-01-01')) as MonthLastDay";
           return DbHelperSQL.Query(strSql);
        }
        public DataSet GetRepairSchemeFee(string strWhere,string strWhere_Receive) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.TimeFee,a.PartFee,a.IntentionId,b.IntentionCode,b.CustName,b.MachineModelId,f.MachineModel,b.MachineCode,b.RepairTypeId,d.RepairTypeName,b.IntentionDate,b.BusinessPerName,e.PerName as ServerManagerName,c.ReceiveNum,b.ActualLeaveDate,a.SchemeDate from repair_Scheme a ");
            strSql.Append("left join repair_Intention b on a.IntentionId=b.ID ");
            strSql.Append("left join (select IntentionId,sum(ReceiveFee) as ReceiveNum from repair_ReceiveFee where FlagDel=0 ");
            if (strWhere_Receive != "") {
                strSql.Append(strWhere_Receive);
            }
            strSql.Append(" group by IntentionId) c on a.IntentionId=c.IntentionId ");
            strSql.Append("left join base_RepairType d on b.RepairTypeId=d.ID ");
            strSql.Append("left join sys_Person e on b.BusinessPerName=e.ID ");
            strSql.Append("left join base_MachineModel f on b.MachineModelId=f.ID ");
            strSql.Append("where a.FlagDel=0 and a.SchemeDate is not null  ");
            if (strWhere != "") {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.SchemeDate desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetRepairEfficiency_Procedure(string strWhere,string strOrder) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.AssignmentId,a.ProcedureId,b.ProcedureName,a.Num,a.AllNat_Audit,CAST(AllNat_Audit/Num as float) as AllNat_avg,a.RepairSecond_All,a.RepairSecond_Repair,a.RepairSecond_Pause,cast(cast((case when a.Num=1 then a.RepairSecond_All else a.RepairSecond_All/a.Num end) as decimal(18,2)) as float) as RepairSecond_All_avg,cast(cast((case when a.Num=1 then a.RepairSecond_Repair else a.RepairSecond_Repair/a.Num end) as decimal(18,2)) as float) as RepairSecond_Repair_avg,cast(cast((case when a.Num=1 then a.RepairSecond_Pause else a.RepairSecond_Pause/a.Num end) as decimal(18,2)) as float) as RepairSecond_Pause_avg,c.MainRepair,c.AssistRepair,d.MachineModelId,e.MachineModel,e.MachineLevel,(select PerName+',' from sys_Person where CHARINDEX(','+LTRIM(ID)+',',','+LTRIM(c.MainRepair)+','+c.AssistRepair+',')>0 for xml path('')) as RepairerName,f.ScheduleDate,d.MachineCode,d.CustName,d.IntentionDate,dbo.timeStand(a.ID) as timeStandard ");
            strSql.Append("from Repair_Assignment_Procedure a ");
            strSql.Append("left join base_Procedure b on a.ProcedureId=b.ID ");
            strSql.Append("left join Repair_Assignment c on a.AssignmentId=c.ID ");
            strSql.Append("left join repair_Intention d on c.IntentionId=d.ID ");
            strSql.Append("left join base_MachineModel e on d.MachineModelId=e.ID ");
            strSql.Append("left join repair_Schedule f on a.ID=f.AssignmentProcedureId and f.FlagDel=0 and f.FlagNew=0 and f.ScheduleType=3 ");
            strSql.Append("where a.FlagDel=0 and a.RepairSecond_All is not null ");
            if (strWhere != "") {
                strSql.Append(strWhere);
            }
            if (strOrder != "") {
                strSql.Append(strOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetRepairEfficiency_Person(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.ID,b.PerName,a.* from ( ");
            strSql.Append("select a.AssignmentId,a.ProcedureId,b.ProcedureName,a.Num,a.AllNat_Audit,CAST(AllNat_Audit/Num as float) as AllNat_avg,a.RepairSecond_All,a.RepairSecond_Repair,a.RepairSecond_Pause,cast(cast((case when a.Num=1 then a.RepairSecond_All else a.RepairSecond_All/a.Num end) as decimal(18,2)) as float) as RepairSecond_All_avg,cast(cast((case when a.Num=1 then a.RepairSecond_Repair else a.RepairSecond_Repair/a.Num end) as decimal(18,2)) as float) as RepairSecond_Repair_avg,cast(cast((case when a.Num=1 then a.RepairSecond_Pause else a.RepairSecond_Pause/a.Num end) as decimal(18,2)) as float) as RepairSecond_Pause_avg,c.MainRepair,c.AssistRepair,d.MachineModelId,e.MachineModel,e.MachineLevel,(select PerName+',' from sys_Person where CHARINDEX(','+LTRIM(ID)+',',','+LTRIM(c.MainRepair)+','+c.AssistRepair+',')>0 for xml path('')) as RepairerName,(select ltrim(ID)+',' from sys_Person where CHARINDEX(','+LTRIM(ID)+',',','+LTRIM(c.MainRepair)+','+c.AssistRepair+',')>0 for xml path('')) as RepairerId,f.ScheduleDate,d.MachineCode,d.CustName,d.IntentionDate,dbo.timeStand(a.ID) as timeStandard ");
            strSql.Append("from Repair_Assignment_Procedure a ");
            strSql.Append("left join base_Procedure b on a.ProcedureId=b.ID ");
            strSql.Append("left join Repair_Assignment c on a.AssignmentId=c.ID ");
            strSql.Append("left join repair_Intention d on c.IntentionId=d.ID ");
            strSql.Append("left join base_MachineModel e on d.MachineModelId=e.ID ");
            strSql.Append("left join repair_Schedule f on a.ID=f.AssignmentProcedureId and f.FlagDel=0 and f.FlagNew=0 and f.ScheduleType=3 ");
            strSql.Append("where a.FlagDel=0 and a.RepairSecond_All is not null ");
            strSql.Append(")a left join sys_Person b on CHARINDEX(','+ltrim(b.ID)+',',','+a.RepairerId)>0 where 1=1 ");
            if (strWhere != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by b.ID asc,a.ProcedureName asc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetRepairTimeStatistics(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.IntentionCode,a.CustName,CAST(a.IntentionDate as DATE) as IntentionDate,a.MachineModelId,b.MachineModel,a.MachineCode,a.RepairTypeId,a.RepairContent,c.RepairTypeName,DATEDIFF(DAY,f.ExpectStartDate,f.ExpectCompleteDate)+1 as ExpectDay,DATEDIFF(SECOND,f.RepairStartDate,f.RepairCompleteDate) as RepairSecond,f.RepairStartDate,f.RepairCompleteDate ");
            strSql.Append("from repair_Intention a ");
            strSql.Append("left join base_MachineModel b on a.MachineModelId=b.ID ");
            strSql.Append("left join base_RepairType c on a.RepairTypeId=c.ID ");
            strSql.Append("left join (select a.IntentionId,MIN(a.ExpectStartDate) as ExpectStartDate,MAX(a.ExpectCompleteDate) as ExpectCompleteDate,MIN(case c.ScheduleType when 1 then c.ScheduleDate else null end) as RepairStartDate,MAX(case c.ScheduleType when 3 then c.ScheduleDate else null end) as RepairCompleteDate from Repair_Assignment a ");
            strSql.Append("left join Repair_Assignment_Procedure b on b.FlagDel=0 and b.AssignmentId=a.ID ");
            strSql.Append("left join repair_Schedule c on c.FlagDel=0 and c.AssignmentProcedureId=b.ID ");
            strSql.Append("where a.FlagDel=0 group by a.IntentionId) f on f.IntentionId=a.ID ");
            strSql.Append("where a.FlagDel=0 and a.FlagResult=1 ");
            if (strWhere != "") {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by a.IntentionDate desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

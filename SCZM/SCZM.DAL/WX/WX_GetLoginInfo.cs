using SCZM.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCZM.DAL.WX
{
    public class WX_GetLoginInfo
    {
        public DataSet GetLoginInfo(string WeiXinAccount)
        {
            string strSql0 = "select ID,PerName,DepId,Account,Salt,IsAdmin,PostId,RoleId from sys_Person where FlagDel=0 and WXNo='" + WeiXinAccount+"'";
            return DbHelperSQL.Query(strSql0);
        }
        public DataSet getWxAccount_Id(string userIdList)
        {
            string strSql0 = "select WXNo from sys_Person where FlagDel=0 and ID in (" + userIdList + ")";
            return DbHelperSQL.Query(strSql0);
        }
        public DataSet getWxAccount_Dep(string userDepIdList)
        {
            string strSql0 = "select WXNo from sys_Person where FlagDel=0 and DepId in (" + userDepIdList + ")";
            return DbHelperSQL.Query(strSql0);
        }
        public DataSet getWxAccount_Procedure(int AssignmentProcedureId)
        {
            string strSql0 = "select a.ID,a.IntentionCode,a.CustName,b.MainRepair,b.AssistRepair,c.ID,c.PerName,c.WXNo from repair_Intention a "
            + "left join Repair_Assignment b on b.FlagDel=0 and b.IntentionId=a.ID "
            + "left join sys_Person c on c.FlagDel=0 and ','+RTRIM(b.MainRepair)+','+b.AssistRepair+',' like '%,'+RTRIM(c.ID)+',%' "
            + "where a.FlagDel=0 and a.ID in(select a.ID from repair_Intention a left join Repair_Assignment b on b.FlagDel=0 and b.IntentionId=a.ID left join Repair_Assignment_Procedure c on c.AssignmentId=b.ID and c.FlagDel=0 and c.ID=" + AssignmentProcedureId + ")";
            return DbHelperSQL.Query(strSql0);
        }
        public DataSet getWxAccount_Procedure_onlyGroup(int AssignmentProcedureId) {
            string strSql0 = "select a.ID,a.IntentionCode,a.CustName,b.MainRepair,b.AssistRepair,c.ID,c.PerName,c.WXNo from repair_Intention a "
            + "left join Repair_Assignment b on b.FlagDel=0 and b.IntentionId=a.ID "
            + "left join sys_Person c on c.FlagDel=0 and ','+RTRIM(b.MainRepair)+','+b.AssistRepair+',' like '%,'+RTRIM(c.ID)+',%' "
            + "where a.FlagDel=0 and b.ID in(select AssignmentId from Repair_Assignment_Procedure where FlagDel=0 and ID="+AssignmentProcedureId+")";
            return DbHelperSQL.Query(strSql0);
        }
        public DataSet getTimePart(string type) {
            if (type == "w") {
                string strSql = "select DATEADD(day,-1,DATEADD(WEEK,DATEPART(WEEK,GETDATE())-1,cast(DATEPART(YEAR,GETDATE()) as varchar)+'-01-01')) as s,DATEADD(day,5,DATEADD(WEEK,DATEPART(WEEK,GETDATE())-1,cast(DATEPART(YEAR,GETDATE()) as varchar)+'-01-01')) as e";
                return DbHelperSQL.Query(strSql);
            }
            else if (type == "m") {
                string strSql = "select DATEADD(MONTH,DATEPART(MONTH,GETDATE())-1,cast(DATEPART(YEAR,GETDATE()) as varchar)+'-01-01') as s,DATEADD(day,-1,DATEADD(MONTH,DATEPART(MONTH,GETDATE()),cast(DATEPART(YEAR,GETDATE()) as varchar)+'-01-01')) as e";
                return DbHelperSQL.Query(strSql);
            }
            else {
                return null;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using SCZM.Common;
using SCZM.WebUI;

namespace SCZM.Web.Ashx.Repair
{
    /// <summary>
    /// repair_Report 的摘要说明

    /// </summary>
    public class repair_Report : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string btn = RequestHelper.GetString("Btn");
            string error = BaseWeb.CheckUserBtnPower();
            if (error != "")
            {
                context.Response.Write(error);
                return;
            }
            //取得处事类型
            string action = RequestHelper.GetQueryString("action");
            switch (action)
            {
                case "GetRepairTZList"://获取维修台帐
                    GetRepairTZList(context, btn);
                    break;
                case "GetRepairProgressList"://获取维修进度
                    GetRepairProgressList(context, btn);
                    break;
                case "GetItemStatistics"://维修项目统计
                    GetItemStatistics(context, btn);
                    break;
                case "GetPartStatistics"://维修项目统计
                    GetPartStatistics(context, btn);
                    break;
                case "GetRepairerRewardDetail"://维修工序奖励明细
                    GetRepairerRewardDetail(context, btn);
                    break;
                case "GetRepairerReward_Audit"://维修工序奖励明细
                    GetRepairerReward_Audit(context, btn);
                    break;
                case "GetRepairerReward"://维修工序奖励
                    GetRepairerReward(context, btn);
                    break;
                case "GetRepairerSaturation"://维修人员饱和度
                    GetRepairerSaturation(context, btn);
                    break;
                case "GetRepairerSaturationDetail"://维修人员饱和度
                    GetRepairerSaturationDetail(context, btn);
                    break;
                case "GetRepairProgress_Intention"://基于机型的维修进度表
                    GetRepairProgress_Intention(context, btn);
                    break;
                case "GetRepairIntentionStatistics":
                    GetRepairIntentionStatistics(context, btn);
                    break;
                case "SaveRepairReward_Audit":
                    SaveRepairReward_Audit(context, btn);
                    break;
                case "SaveRepairReward_CancelAudit":
                    SaveRepairReward_CancelAudit(context, btn);
                    break;
                case "SaveRepairReward_multipleAudit":
                    SaveRepairReward_multipleAudit(context, btn);
                    break;
                case "GetWeeks":
                    GetWeeks(context, btn);
                    break;
                case "GetRepairSchemeFee":
                    GetRepairSchemeFee(context, btn);
                    break;
                case "GetRepairEfficiency":
                    GetRepairEfficiency(context, btn);
                    break;
                case "GetRepairTimeStatistics":
                    GetRepairTimeStatistics(context, btn);
                    break;
                case "GetRepairEfficiency_Person":
                    GetRepairEfficiency_Person(context, btn);
                    break;
            }
        }
        #region 获取维修台帐列表==============================;
        private void GetRepairTZList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string IntentionDate_Start = RequestHelper.GetString("IntentionDate_Start").Trim();
                string IntentionDate_End = RequestHelper.GetString("IntentionDate_End").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();
                string FlagEnter = RequestHelper.GetString("FlagEnter").Trim();
                string FlagAssignment = RequestHelper.GetString("FlagAssignment").Trim();
                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                if (LoginUserModel.IsAdmin == false)
                {
                    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                }
                //-------------------------------------------------------------------------

                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                if (IntentionType != "")
                {
                    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType, 0));
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and g.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //报修日期
                if (IntentionDate_Start != "")
                {
                    strWhere.Append(" and a.IntentionDate>= cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime)");
                }
                if (IntentionDate_End != "")
                {
                    strWhere.Append(" and a.IntentionDate<=cast('" + Utils.StrToDateTime(IntentionDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                //业务部门
                if (BusinessDepId != "")
                {
                    strWhere.Append(" and a.BusinessDepId=" + Utils.StrToInt(BusinessDepId, 0));
                }
                if (FlagEnter != "")
                {
                    if (FlagEnter == "0")
                    {
                        strWhere.Append(" and a.ActualEnterDate is null ");
                    }
                    else if (FlagEnter == "1")
                    {
                        strWhere.Append(" and a.ActualEnterDate is not null ");
                    }
                }
                if (FlagAssignment != "")
                {
                    if (FlagAssignment == "0")
                    {
                        strWhere.Append(" and b.ID is null ");
                    }
                    else if (FlagAssignment == "1")
                    {
                        strWhere.Append(" and b.ID is not null ");
                    }
                }
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairTZList(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        #region 获取维修进度列表==============================;
        private void GetRepairProgressList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string AssignmentCode = RequestHelper.GetString("AssignmentCode").Trim();
                //string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string AssignmentDate_Start = RequestHelper.GetString("AssignmentDate_Start").Trim();
                string AssignmentDate_End = RequestHelper.GetString("AssignmentDate_End").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                //string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();

                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                if (LoginUserModel.IsAdmin == false)
                {
                    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                }
                //-------------------------------------------------------------------------

                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                //if (IntentionType != "")
                //{
                //    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType, 0));
                //}
                //派工单号
                if (AssignmentCode != "" && Utils.IsSafeSqlString(AssignmentCode))
                {
                    strWhere.Append(" and b.AssignmentCode like '%" + Utils.Filter(AssignmentCode) + "%'");
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and g.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //派工日期
                if (AssignmentDate_Start != "")
                {
                    strWhere.Append(" and b.AssignmentDate>= cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime)");
                }
                if (AssignmentDate_End != "")
                {
                    strWhere.Append(" and b.AssignmentDate<=cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }

                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairProgressList(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        #region 获取维修项目列表==============================;
        private void GetItemStatistics(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {

                string ProcedureId = RequestHelper.GetString("RepairProcedureId").Trim();
                string DateType = RequestHelper.GetString("DateType").Trim();


                StringBuilder strWhere = new StringBuilder();
                if (ProcedureId != "") {
                    strWhere.Append(" and b.ProcedureId="+Utils.StrToInt(ProcedureId,0)+" ");
                }
                if (DateType == "0")
                {
                    string AssignmentDate_Start = RequestHelper.GetString("AssignmentDate_Start").Trim();
                    string AssignmentDate_End = RequestHelper.GetString("AssignmentDate_End").Trim();
                    if (AssignmentDate_Start != "")
                    {
                        if (AssignmentDate_End != "")
                        {
                            strWhere.Append(" and a.AssignmentDate between cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime) ");

                        }
                        else
                        {
                            strWhere.Append(" and a.AssignmentDate >= cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime) ");
                            
                        }
                    }
                    else
                    {
                        if (AssignmentDate_End != "")
                        {
                            strWhere.Append(" and a.AssignmentDate <= cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime) ");
                           
                        }
                    }
                }
                else if (DateType == "3")
                {
                    string AssignmentDate_Day = RequestHelper.GetString("AssignmentDate_Day").Trim();
                    if (AssignmentDate_Day != "")
                    {
                        strWhere.Append(" and a.AssignmentDate between cast('" + Utils.StrToDateTime(AssignmentDate_Day).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(AssignmentDate_Day + " 23:59:59").ToString() + "' as datetime) ");
                    }
                  
                }
                else if (DateType == "1")
                {
                    string AssignmentDate_Month = RequestHelper.GetString("AssignmentDate_Month").Trim();
                    if (AssignmentDate_Month != "")
                    {
                        string[] monthArray = AssignmentDate_Month.Split('-');
                        int year = int.Parse(monthArray[0]);
                        int month = int.Parse(monthArray[1]);
                        strWhere.Append(" and datepart(YY,a.AssignmentDate)=" + year + " and datepart(MM,a.AssignmentDate)=" + month + " ");
                      
                    }
                }
                else if (DateType == "2")
                {
                    string AssignmentDate_Week = RequestHelper.GetString("AssignmentDate_Week").Trim();
                    string AssignmentDate_Month = RequestHelper.GetString("AssignmentDate_Month").Trim();
                    string[] monthArray = AssignmentDate_Month.Split('-');
                    int year = int.Parse(monthArray[0]);
                    int month = int.Parse(monthArray[1]);
                    int week = int.Parse(AssignmentDate_Week);
                    strWhere.Append(" and datepart(YY,a.AssignmentDate)=" + year + " and datepart(WW,a.AssignmentDate)=" + week + " ");
                    
                }

                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetItemStatistics(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        #region 获取维修部件列表==============================;
        private void GetPartStatistics(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {

                string RepairPart = RequestHelper.GetString("RepairPartId").Trim();
                string DateType = RequestHelper.GetString("DateType").Trim();


                StringBuilder strWhere = new StringBuilder();
                if (RepairPart != "")
                {
                    strWhere.Append(" and a."+RepairPart+"=1 ");
                }
                if (DateType == "0")
                {
                    string SchemeDate_Start = RequestHelper.GetString("SchemeDate_Start").Trim();
                    string SchemeDate_End = RequestHelper.GetString("SchemeDate_End").Trim();
                    if (SchemeDate_Start != "")
                    {
                        if (SchemeDate_End != "")
                        {
                            strWhere.Append(" and a.SchemeDate between cast('" + Utils.StrToDateTime(SchemeDate_Start).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(SchemeDate_End + " 23:59:59").ToString() + "' as datetime) ");

                        }
                        else
                        {
                            strWhere.Append(" and a.SchemeDate >= cast('" + Utils.StrToDateTime(SchemeDate_Start).ToString() + "' as datetime) ");

                        }
                    }
                    else
                    {
                        if (SchemeDate_End != "")
                        {
                            strWhere.Append(" and a.SchemeDate <= cast('" + Utils.StrToDateTime(SchemeDate_End + " 23:59:59").ToString() + "' as datetime) ");

                        }
                    }
                }
                else if (DateType == "3")
                {
                    string SchemeDate_Day = RequestHelper.GetString("SchemeDate_Day").Trim();
                    if (SchemeDate_Day != "")
                    {
                        strWhere.Append(" and a.SchemeDate between cast('" + Utils.StrToDateTime(SchemeDate_Day).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(SchemeDate_Day + " 23:59:59").ToString() + "' as datetime) ");
                    }

                }
                else if (DateType == "1")
                {
                    string SchemeDate_Month = RequestHelper.GetString("SchemeDate_Month").Trim();
                    if (SchemeDate_Month != "")
                    {
                        string[] monthArray = SchemeDate_Month.Split('-');
                        int year = int.Parse(monthArray[0]);
                        int month = int.Parse(monthArray[1]);
                        strWhere.Append(" and datepart(YY,a.SchemeDate)=" + year + " and datepart(MM,a.SchemeDate)=" + month + " ");

                    }
                }
                else if (DateType == "2")
                {
                    string SchemeDate_Week = RequestHelper.GetString("SchemeDate_Week").Trim();
                    string SchemeDate_Month = RequestHelper.GetString("SchemeDate_Month").Trim();
                    string[] monthArray = SchemeDate_Month.Split('-');
                    int year = int.Parse(monthArray[0]);
                    int month = int.Parse(monthArray[1]);
                    int week = int.Parse(SchemeDate_Week);
                    strWhere.Append(" and datepart(YY,a.SchemeDate)=" + year + " and datepart(WW,a.SchemeDate)=" + week + " ");

                }
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetPartStatistics(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        #region 获取维修工序奖励==============================;
        private void GetRepairerReward(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string RepairDate_Start = RequestHelper.GetString("RepairDate_Start").Trim();
                string RepairDate_End = RequestHelper.GetString("RepairDate_End").Trim();
                string RepairerId = RequestHelper.GetString("RepairerId").Trim();
                string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();

                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------


                //维修日期
                if (RepairDate_Start != "")
                {
                    strWhere.Append(" and k.ScheduleDate>= cast('" + Utils.StrToDateTime(RepairDate_Start).ToString() + "' as datetime)");
                }
                if (RepairDate_End != "")
                {
                    strWhere.Append(" and k.ScheduleDate<=cast('" + Utils.StrToDateTime(RepairDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //人员ID
                if (RepairerId != "")
                {
                    strWhere.Append(" and g.ID in (" + RepairerId + ") ");
                }
                //部门ID
                if (BusinessDepId != "")
                {
                    strWhere.Append(" and (','+l.SupList like '%," + Utils.StrToInt(BusinessDepId, -1) + ",%' or l.ID=" + Utils.StrToInt(BusinessDepId, -1) + ") ");
                }
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairerReward(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        #region 获取维修工序奖励明细==============================;
        private void GetRepairerRewardDetail(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string AssignmentCode = RequestHelper.GetString("AssignmentCode").Trim();
                //string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string AssignmentDate_Start = RequestHelper.GetString("AssignmentDate_Start").Trim();
                string AssignmentDate_End = RequestHelper.GetString("AssignmentDate_End").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                //string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();
                //--------------------------------------------------------------------
                string RepairDate_Start = RequestHelper.GetString("RepairDate_Start").Trim();
                string RepairDate_End = RequestHelper.GetString("RepairDate_End").Trim();
                string RepairerId = RequestHelper.GetString("RepairerId").Trim();
                string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();
                string PerId = RequestHelper.GetString("PerId").Trim();
                //--------------------------------------------------------------------

                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                if (LoginUserModel.IsAdmin == false)
                {
                    strWhere.Append(" and g.ID in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                }
                //-------------------------------------------------------------------------

                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                //if (IntentionType != "")
                //{
                //    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType, 0));
                //}
                //派工单号
                if (AssignmentCode != "" && Utils.IsSafeSqlString(AssignmentCode))
                {
                    strWhere.Append(" and c.AssignmentCode like '%" + Utils.Filter(AssignmentCode) + "%'");
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and b.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //派工日期
                if (AssignmentDate_Start != "")
                {
                    strWhere.Append(" and c.AssignmentDate>= cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime)");
                }
                if (AssignmentDate_End != "")
                {
                    strWhere.Append(" and c.AssignmentDate<=cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                //-----------------------------------------------------------------------------------
                //维修日期
                if (RepairDate_Start != "")
                {
                    strWhere.Append(" and k.ScheduleDate>= cast('" + Utils.StrToDateTime(RepairDate_Start).ToString() + "' as datetime)");
                }
                if (RepairDate_End != "")
                {
                    strWhere.Append(" and k.ScheduleDate<=cast('" + Utils.StrToDateTime(RepairDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //人员IDList
                if (RepairerId != "")
                {
                    strWhere.Append(" and g.ID in (" + RepairerId + ") ");
                }
                //部门ID
                if (BusinessDepId != "")
                {
                    strWhere.Append(" and (','+l.SupList like '%," + Utils.StrToInt(BusinessDepId, -1) + ",%' or l.ID=" + Utils.StrToInt(BusinessDepId, -1) + ") ");
                }
                //人员ID
                if (PerId != "")
                {
                    strWhere.Append(" and g.ID=" + Utils.StrToInt(PerId, -1) + " ");
                }
                //-----------------------------------------------------------------------------------
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairerRewardDetail(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        private void GetRepairerReward_Audit(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string AssignmentCode = RequestHelper.GetString("AssignmentCode").Trim();
                //string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string AssignmentDate_Start = RequestHelper.GetString("AssignmentDate_Start").Trim();
                string AssignmentDate_End = RequestHelper.GetString("AssignmentDate_End").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                //string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();
                //--------------------------------------------------------------------
                string RepairDate_Start = RequestHelper.GetString("RepairDate_Start").Trim();
                string RepairDate_End = RequestHelper.GetString("RepairDate_End").Trim();
                //--------------------------------------------------------------------
                string AuditState = RequestHelper.GetString("AuditState").Trim();

                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                if (LoginUserModel.IsAdmin == false)
                {
                    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                }
                //-------------------------------------------------------------------------

                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                //if (IntentionType != "")
                //{
                //    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType, 0));
                //}
                //派工单号
                if (AssignmentCode != "" && Utils.IsSafeSqlString(AssignmentCode))
                {
                    strWhere.Append(" and c.AssignmentCode like '%" + Utils.Filter(AssignmentCode) + "%'");
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and b.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //派工日期
                if (AssignmentDate_Start != "")
                {
                    strWhere.Append(" and c.AssignmentDate>= cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime)");
                }
                if (AssignmentDate_End != "")
                {
                    strWhere.Append(" and c.AssignmentDate<=cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                //-----------------------------------------------------------------------------------
                //维修日期
                if (RepairDate_Start != "")
                {
                    strWhere.Append(" and k.ScheduleDate>= cast('" + Utils.StrToDateTime(RepairDate_Start).ToString() + "' as datetime)");
                }
                if (RepairDate_End != "")
                {
                    strWhere.Append(" and k.ScheduleDate<=cast('" + Utils.StrToDateTime(RepairDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //-----------------------------------------------------------------------------------
                if (AuditState != "" && AuditState != "-1")
                {
                    if (AuditState == "0")
                    {
                        strWhere.Append(" and (d.AuditState=0 or d.AuditState is null) ");
                    }
                    else
                    {
                        strWhere.Append(" and d.AuditState=" + Utils.StrToInt(AuditState, -1) + " ");
                    }
                }
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairerReward_Audit(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #region 获取维修人员饱和度==============================;
        private void GetRepairerSaturation(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {

                string RepairerId = RequestHelper.GetString("RepairerId").Trim();
                string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();
                string RepairDepId = RequestHelper.GetString("RepairDepId").Trim();
                string OfficeDepId = RequestHelper.GetString("OfficeDepId").Trim();
                string DateType = RequestHelper.GetString("DateType").Trim();

                StringBuilder strWhere_Procedure = new StringBuilder();
                StringBuilder strWhere_Repairer = new StringBuilder();

                //--------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------

                //and (c.AssignmentDate>='2018-01-01 00:00:00' or c.AssignmentDate<'2018-01-01 00:00:00' and (g.ScheduleType !=3 or g.ID is null))
                //统计日期
                string lastDay = "'"+DateTime.Now.ToString()+"'";
                if (DateType == "0")
                {
                    string AssignmentDate_Start = RequestHelper.GetString("AssignmentDate_Start").Trim();
                    string AssignmentDate_End = RequestHelper.GetString("AssignmentDate_End").Trim();
                    if (AssignmentDate_Start != "")
                    {
                        if (AssignmentDate_End != "")
                        {
                            strWhere_Procedure.Append(" and (c.AssignmentDate between cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime) or c.AssignmentDate < cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime) and (g.ScheduleType !=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>='"+Utils.StrToDateTime(AssignmentDate_Start).ToString()+"')) ");
                            lastDay = "'"+AssignmentDate_End + " 23:59:59'";
                        }
                        else
                        {
                            strWhere_Procedure.Append(" and (c.AssignmentDate >= cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime)  or c.AssignmentDate < cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime) and (g.ScheduleType !=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>='" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "')) ");
                        }
                    }
                    else
                    {
                        if (AssignmentDate_End != "")
                        {
                            strWhere_Procedure.Append(" and c.AssignmentDate <= cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime) ");
                            lastDay = "'" + AssignmentDate_End + " 23:59:59'";
                        }
                    }
                }
                else if (DateType == "3")
                {
                    string AssignmentDate_Day = RequestHelper.GetString("AssignmentDate_Day").Trim();
                    if (AssignmentDate_Day != "")
                    {
                        strWhere_Procedure.Append(" and (c.AssignmentDate between cast('" + Utils.StrToDateTime(AssignmentDate_Day).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(AssignmentDate_Day + " 23:59:59").ToString() + "' as datetime) or c.AssignmentDate < cast('" + Utils.StrToDateTime(AssignmentDate_Day).ToString() + "' as datetime) and (g.ScheduleType !=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>='" + Utils.StrToDateTime(AssignmentDate_Day).ToString() + "')) ");
                        lastDay = "'" + AssignmentDate_Day + " 23:59:59'";
                    }
                }
                else if (DateType == "1") {
                    string AssignmentDate_Month = RequestHelper.GetString("AssignmentDate_Month").Trim();
                    if (AssignmentDate_Month != "") {
                        string[] monthArray=AssignmentDate_Month.Split('-');
                        int year = int.Parse(monthArray[0]);
                        int month = int.Parse(monthArray[1]);
                        strWhere_Procedure.Append(" and (datepart(YY,c.AssignmentDate)=" + year + " and datepart(MM,c.AssignmentDate)=" + month + " or c.AssignmentDate < cast('" + Utils.StrToDateTime(AssignmentDate_Month + "-01").ToString() + "' as datetime) and (g.ScheduleType !=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>='" + Utils.StrToDateTime(AssignmentDate_Month+"-01").ToString() + "')) ");
                        
                        string firstDatOfNextMonth = month == 12 ? year + 1 + "-01-01" : year + "-" + (month + 1 < 10 ? "0" + (month + 1).ToString() : (month + 1).ToString()) + "-01";
                        lastDay = "dateadd(second,-1,'"+firstDatOfNextMonth+"')";
                    }
                }
                else if (DateType == "2") {
                    string AssignmentDate_Week = RequestHelper.GetString("AssignmentDate_Week").Trim();
                    string AssignmentDate_Month = RequestHelper.GetString("AssignmentDate_Month").Trim();
                    string[] monthArray = AssignmentDate_Month.Split('-');
                    int year = int.Parse(monthArray[0]);
                    int month = int.Parse(monthArray[1]);
                    int week=int.Parse(AssignmentDate_Week);
                    strWhere_Procedure.Append(" and (datepart(YY,c.AssignmentDate)=" + year + " and datepart(WW,c.AssignmentDate)=" + week + " or c.AssignmentDate<DATEADD(DAY,1-DATEPART(weekday,convert(varchar(4)," + year + ")+'-1-1'),DATEADD(week," + week + "-1,convert(varchar(4)," + year + ")+'-1-1')) and (g.ScheduleType!=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>=DATEADD(DAY,1-DATEPART(weekday,convert(varchar(4)," + year + ")+'-1-1'),DATEADD(week," + week + "-1,convert(varchar(4)," + year + ")+'-1-1')))) ");
                    lastDay = "DATEADD(second,-1,DATEADD(day,8-DATEPART(weekday,DATEADD(DAY,1-DATEPART(weekday,convert(varchar(4)," + year + ")+'-1-1'),DATEADD(week," + week + "-1,convert(varchar(4)," + year + ")+'-1-1'))),DATEADD(DAY,1-DATEPART(weekday,convert(varchar(4)," + year + ")+'-1-1'),DATEADD(week," + week + "-1,convert(varchar(4)," + year + ")+'-1-1'))))";
                }
                //人员ID
                if (RepairerId != "")
                {
                    strWhere_Repairer.Append(" and a.ID in (" + RepairerId + ") ");
                }
                //部门ID
                if (BusinessDepId != "")
                {
                    strWhere_Repairer.Append(" and (','+a.SupList like '%," + Utils.StrToInt(BusinessDepId, -1) + ",%' or a.DepId=" + Utils.StrToInt(BusinessDepId, -1) + ") ");
                }
                //办公室
                if (OfficeDepId != "")
                {
                    strWhere_Repairer.Append(" and a.DepId !=" + Utils.StrToInt(OfficeDepId, 28));
                }
                else
                {
                    strWhere_Repairer.Append(" and a.DepId !=28");
                }

                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairerSaturation(strWhere_Procedure.ToString(), strWhere_Repairer.ToString(), Utils.StrToInt(RepairDepId, 24),lastDay).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                DataTable dt_IntentionNum = bll.GetIntentionNum("").Tables[0];
                string intentionNum = Utils.ToJson(dt_IntentionNum);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + ",\"IntentionNum\":" + intentionNum + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        #region 获取维修人员饱和度明细==============================;
        private void GetRepairerSaturationDetail(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string AssignmentCode = RequestHelper.GetString("AssignmentCode").Trim();
                //string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();

                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                string RepairerId = RequestHelper.GetString("RepairerId").Trim();
                string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();
                string PerId = RequestHelper.GetString("PerId").Trim();
                string ScheduleType = RequestHelper.GetString("ScheduleType").Trim();
                string DateType = RequestHelper.GetString("DateType").Trim();
                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------
                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //派工单号
                if (AssignmentCode != "" && Utils.IsSafeSqlString(AssignmentCode))
                {
                    strWhere.Append(" and c.AssignmentCode like '%" + Utils.Filter(AssignmentCode) + "%'");
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and b.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                ////派工日期
                //if (AssignmentDate_Start != "")
                //{
                //    strWhere.Append(" and c.AssignmentDate>= cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime)");
                //}
                //if (AssignmentDate_End != "")
                //{
                //    strWhere.Append(" and c.AssignmentDate<=cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime)");
                //}
                //统计日期
                string lastDay = "'"+DateTime.Now.ToString()+"'";
                if (DateType == "0")
                {
                    string AssignmentDate_Start = RequestHelper.GetString("AssignmentDate_Start").Trim();
                    string AssignmentDate_End = RequestHelper.GetString("AssignmentDate_End").Trim();
                    if (AssignmentDate_Start != "")
                    {
                        if (AssignmentDate_End != "")
                        {
                            strWhere.Append(" and (c.AssignmentDate between cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime) or c.AssignmentDate < cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime) and (g.ScheduleType !=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>='" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "'))");
                            lastDay = "'" + AssignmentDate_End + " 23:59:59'";
                        }
                        else
                        {
                            strWhere.Append(" and (c.AssignmentDate >= cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime)  or c.AssignmentDate < cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime) and (g.ScheduleType !=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>='" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "'))");
                        }
                    }
                    else
                    {
                        if (AssignmentDate_End != "")
                        {
                            strWhere.Append(" and c.AssignmentDate <= cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime)");
                            lastDay = "'" + AssignmentDate_End + " 23:59:59'";
                        }
                    }
                }
                else if (DateType == "3")
                {
                    string AssignmentDate_Day = RequestHelper.GetString("AssignmentDate_Day").Trim();
                    if (AssignmentDate_Day != "")
                    {
                        strWhere.Append(" and (c.AssignmentDate between cast('" + Utils.StrToDateTime(AssignmentDate_Day).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(AssignmentDate_Day + " 23:59:59").ToString() + "' as datetime) or c.AssignmentDate < cast('" + Utils.StrToDateTime(AssignmentDate_Day).ToString() + "' as datetime) and (g.ScheduleType !=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>='" + Utils.StrToDateTime(AssignmentDate_Day).ToString() + "'))");
                        lastDay = "'" + AssignmentDate_Day + " 23:59:59'";
                    }
                }
                else if (DateType == "1")
                {
                    string AssignmentDate_Month = RequestHelper.GetString("AssignmentDate_Month").Trim();
                    if (AssignmentDate_Month != "")
                    {
                        string[] monthArray = AssignmentDate_Month.Split('-');
                        int year = int.Parse(monthArray[0]);
                        int month = int.Parse(monthArray[1]);
                        strWhere.Append(" and (datepart(YY,c.AssignmentDate)=" + year + " and datepart(MM,c.AssignmentDate)=" + month + " or c.AssignmentDate < cast('" + Utils.StrToDateTime(AssignmentDate_Month + "-01").ToString() + "' as datetime) and (g.ScheduleType !=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>='" + Utils.StrToDateTime(AssignmentDate_Month+"-01").ToString() + "'))");

                        string firstDatOfNextMonth = month == 12 ? year + 1 + "-01-01" : year + "-" + (month + 1 < 10 ? "0" + (month + 1).ToString() : (month + 1).ToString()) + "-01";
                        lastDay = "dateadd(second,-1,'" + firstDatOfNextMonth + "')";
                    }
                }
                else if (DateType == "2")
                {
                    string AssignmentDate_Week = RequestHelper.GetString("AssignmentDate_Week").Trim();
                    string AssignmentDate_Month = RequestHelper.GetString("AssignmentDate_Month").Trim();
                    string[] monthArray = AssignmentDate_Month.Split('-');
                    int year = int.Parse(monthArray[0]);
                    int month = int.Parse(monthArray[1]);
                    int week = int.Parse(AssignmentDate_Week);
                    strWhere.Append(" and (datepart(YY,c.AssignmentDate)=" + year + " and datepart(WW,c.AssignmentDate)=" + week + " or c.AssignmentDate<DATEADD(DAY,1-DATEPART(weekday,convert(varchar(4)," + year + ")+'-1-1'),DATEADD(week," + week + "-1,convert(varchar(4)," + year + ")+'-1-1')) and (g.ScheduleType!=3 or g.ID is null or g.ScheduleType=3 and g.ScheduleDate>=DATEADD(DAY,1-DATEPART(weekday,convert(varchar(4)," + year + ")+'-1-1'),DATEADD(week," + week + "-1,convert(varchar(4)," + year + ")+'-1-1')))) ");
                    lastDay = "DATEADD(second,-1,DATEADD(day,8-DATEPART(weekday,DATEADD(DAY,1-DATEPART(weekday,convert(varchar(4)," + year + ")+'-1-1'),DATEADD(week," + week + "-1,convert(varchar(4)," + year + ")+'-1-1'))),DATEADD(DAY,1-DATEPART(weekday,convert(varchar(4)," + year + ")+'-1-1'),DATEADD(week," + week + "-1,convert(varchar(4)," + year + ")+'-1-1'))))";
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                //人员ID
                if (RepairerId != "")
                {
                    strWhere.Append(" and f.ID in (" + RepairerId + ") ");
                }
                //部门ID
                if (BusinessDepId != "")
                {
                    strWhere.Append(" and (','+h.SupList like '%," + Utils.StrToInt(BusinessDepId, -1) + ",%' or f.DepId=" + Utils.StrToInt(BusinessDepId, -1) + ") ");
                }
                if (PerId != "")
                {
                    strWhere.Append(" and f.ID=" + Utils.StrToInt(PerId, -1));
                }
                if (ScheduleType != "-1")
                {
                    if (ScheduleType == "0")
                    {
                        strWhere.Append(" and g.ScheduleType is null ");
                    }
                    else if (ScheduleType == "1")
                    {
                        strWhere.Append(" and g.ScheduleType=1 ");
                    }
                    else if (ScheduleType == "2")
                    {
                        strWhere.Append(" and g.ScheduleType=2 ");
                    }
                    else if (ScheduleType == "3")
                    {
                        strWhere.Append(" and g.ScheduleType=3 ");
                    }
                }
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairerSaturationDetail(strWhere.ToString(),lastDay).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        #region 获取维修进度表==============================;
        private void GetRepairProgress_Intention(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                string FlagEnter = RequestHelper.GetString("FlagEnter").Trim();
                string FlagAssignment = RequestHelper.GetString("FlagAssignment").Trim();
                string IntentionDate_Start = RequestHelper.GetString("IntentionDate_Start").Trim();
                string IntentionDate_End = RequestHelper.GetString("IntentionDate_End").Trim();
                string RepairAdress = RequestHelper.GetString("RepairAdress").Trim();
                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------

                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                if (IntentionType != "")
                {
                    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType, 0));
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and a.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId in(" + RepairTypeId + ") ");
                }
                if (FlagEnter != "")
                {
                    if (FlagEnter == "0")
                    {
                        strWhere.Append(" and a.ActualEnterDate is null ");
                    }
                    else if (FlagEnter == "1")
                    {
                        strWhere.Append(" and a.ActualEnterDate is not null ");
                    }
                }
                if (FlagAssignment != "")
                {
                    if (FlagAssignment == "0")
                    {
                        strWhere.Append(" and a.MainRepairs is null ");
                    }
                    else if (FlagAssignment == "1")
                    {
                        strWhere.Append(" and a.MainRepairs is not null ");
                    }
                }
                //报修日期
                if (IntentionDate_Start != "")
                {
                    strWhere.Append(" and a.IntentionDate>= cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime)");
                }
                if (IntentionDate_End != "")
                {
                    strWhere.Append(" and a.IntentionDate<=cast('" + Utils.StrToDateTime(IntentionDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                if (RepairAdress != "")
                {
                    strWhere.Append(" and a.RepairAdress='" + RepairAdress + "' ");
                }
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairProgress_Intention(strWhere.ToString(), LoginUserModel.ID).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 获取维修意向统计==============================;
        private void GetRepairIntentionStatistics(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                string FlagEnter = RequestHelper.GetString("FlagEnter").Trim();
                string FlagAssignment = RequestHelper.GetString("FlagAssignment").Trim();
                string IntentionDate_Start = RequestHelper.GetString("IntentionDate_Start").Trim();
                string IntentionDate_End = RequestHelper.GetString("IntentionDate_End").Trim();
                string RepairAdress = RequestHelper.GetString("RepairAdress").Trim();
                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------

                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                if (IntentionType != "")
                {
                    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType, 0));
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and c.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId in(" + RepairTypeId + ") ");
                }
                if (FlagEnter != "")
                {
                    if (FlagEnter == "0")
                    {
                        strWhere.Append(" and a.ActualEnterDate is null ");
                    }
                    else if (FlagEnter == "1")
                    {
                        strWhere.Append(" and a.ActualEnterDate is not null ");
                    }
                }
                //if (FlagAssignment != "")
                //{
                //    if (FlagAssignment == "0")
                //    {
                //        strWhere.Append(" and a.MainRepairs is null ");
                //    }
                //    else if (FlagAssignment == "1")
                //    {
                //        strWhere.Append(" and a.MainRepairs is not null ");
                //    }
                //}
                //报修日期
                if (IntentionDate_Start != "")
                {
                    strWhere.Append(" and a.IntentionDate>= cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime)");
                }
                if (IntentionDate_End != "")
                {
                    strWhere.Append(" and a.IntentionDate<=cast('" + Utils.StrToDateTime(IntentionDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                if (RepairAdress != "")
                {
                    strWhere.Append(" and a.RepairAdress='" + RepairAdress + "' ");
                }
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataSet ds = bll.GetRepairIntentionStatistics(strWhere.ToString());
                DataTable dt = ds.Tables["RepairTypeStatistics"];
                string rowsStr = Utils.ToJson(dt);
                DataTable dt2 = ds.Tables["IntentionStatistics"];
                string rowsStr2 = Utils.ToJson(dt2);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"RepairTypeStatistics\":" + rowsStr + ",\"IntentionStatistics\":" + rowsStr2 + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        private void SaveRepairReward_Audit(HttpContext context, string btn)
        {
            if (btn != "btnAudit")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string AuditDate = RequestHelper.GetString("AuditDate");
                string AuditState = RequestHelper.GetString("AuditState");
                string AllNat_Audit = RequestHelper.GetString("AllNat_Audit");
                string AuditOpinion = RequestHelper.GetString("AuditOpinion");
                string ID = RequestHelper.GetString("AssignmentProcedureIdStr");
                Model.System.sys_LoginUser loginUser = BaseWeb.GetLoginInfo();

                Model.Repair.Repair_Assignment_Procedure model = new Model.Repair.Repair_Assignment_Procedure();
                model.AuditorId = loginUser.ID;
                model.AuditDate = Utils.StrToDateTime(AuditDate, DateTime.Now);
                model.AuditState = Utils.StrToInt(AuditState, 1);
                model.AuditOpinion = AuditOpinion;
                model.ID = Utils.StrToInt(ID, 0);
                if (AllNat_Audit != "" && AuditState == "1")
                {
                    model.AllNat_Audit = Utils.StrToDecimal(AllNat_Audit, 0);
                }
                BLL.Repair.repair_Report bll = new BLL.Repair.repair_Report();
                int num = bll.SaveRepairReward_Audit(model);
                if (num > 0)
                {
                    context.Response.Write("{\"status\":\"1\",\"msg\":\"审核成功\"}");
                    BaseWeb.AddOpera(loginUser, int.Parse(RequestHelper.GetString("MenuId")), Enums.ActionEnum.Audit.ToString(), "审核工序奖励" + model.ID);
                }
                else
                {
                    context.Response.Write("{\"status\":\"0.2\",\"msg\":\"审核失败\"}");
                }
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        private void SaveRepairReward_CancelAudit(HttpContext context, string btn)
        {
            if (btn != "btnCancelAudit")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IDStr = RequestHelper.GetString("IDStr");
                Model.System.sys_LoginUser loginUser = BaseWeb.GetLoginInfo();

                Model.Repair.Repair_Assignment_Procedure model = new Model.Repair.Repair_Assignment_Procedure();

                BLL.Repair.repair_Report bll = new BLL.Repair.repair_Report();
                int num = bll.SaveRepairReward_CancelAudit(IDStr);
                if (num > 0)
                {
                    context.Response.Write("{\"status\":\"1\",\"msg\":\"消审成功\"}");
                    BaseWeb.AddOpera(loginUser, int.Parse(RequestHelper.GetString("MenuId")), Enums.ActionEnum.Audit.ToString(), "消审工序奖励" + IDStr);
                }
                else
                {
                    context.Response.Write("{\"status\":\"0.2\",\"msg\":\"消审失败\"}");
                }
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        private void SaveRepairReward_multipleAudit(HttpContext context, string btn)
        {
            if (btn != "btnAudit_All")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string AssignmentProcedureIdStr = RequestHelper.GetString("AssignmentProcedureIdStr");
                string AuditDate = RequestHelper.GetString("AuditDate");
                string AuditState = RequestHelper.GetString("AuditState");
                string AuditOpinion = RequestHelper.GetString("AuditOpinion");
                BLL.Repair.repair_Report bll = new BLL.Repair.repair_Report();
                BLL.Repair.Repair_Assignment bll_Assignment = new BLL.Repair.Repair_Assignment();
                StringBuilder strWhere = new StringBuilder();
                if (AssignmentProcedureIdStr != "")
                {
                    strWhere.Append(" and a.ID in (" + AssignmentProcedureIdStr + ") ");
                }
                bll_Assignment.GetAssignmentProcedureList(strWhere.ToString());
                Model.Repair.Repair_Assignment_Procedure model = new Model.Repair.Repair_Assignment_Procedure();
                Model.System.sys_LoginUser loginUser = BaseWeb.GetLoginInfo();
                string status = "0";
                if (AuditState == "2")
                {
                    model.AuditDate = Utils.StrToDateTime(AuditDate, DateTime.Now);
                    model.AuditOpinion = AuditOpinion;
                    model.AuditorId = loginUser.ID;
                    model.AuditState = 2;
                    int num = bll.SaveRepairReward_MultipleAudit(AssignmentProcedureIdStr, model);
                    if (num > 0)
                    {
                        context.Response.Write("{\"status\":\"1\",\"msg\":\"批量审核成功\"}");
                        status = "1";
                    }
                }
                else if (AuditState == "1")
                {
                    string[] IdArray = AssignmentProcedureIdStr.Split(',');
                    DataTable dt = bll_Assignment.GetAssignmentProcedureList(" and a.ID in(" + AssignmentProcedureIdStr + ") ").Tables[0];
                    if (IdArray.Length > 0)
                    {
                        for (int i = 0, len = IdArray.Length; i < len; i++)
                        {
                            DataRow[] dr = dt.Select("ID=" + IdArray[i]);
                            if (dr.Length == 1)
                            {
                                model = new Model.Repair.Repair_Assignment_Procedure();
                                if (dr[0]["AllNat"].ToString() != "")
                                {
                                    model.AllNat_Audit = Utils.StrToDecimal(dr[0]["AllNat"].ToString(), 0);
                                }
                                else
                                {
                                    DataTable dt_Detail = bll.GetRepairerRewardDetail(" and d.ID=" + IdArray[i] + " ").Tables[0];
                                    if (dt_Detail.Rows[0]["AllNat"].ToString() != "")
                                    {
                                        model.AllNat_Audit = Utils.StrToDecimal(dt_Detail.Rows[0]["AllNat"].ToString(), 0) * Utils.StrToDecimal(dr[0]["Num"].ToString(), 0);
                                    }
                                }
                                model.AuditDate = Utils.StrToDateTime(AuditDate, DateTime.Now);
                                model.AuditOpinion = AuditOpinion;
                                model.AuditState = 1;
                                model.AuditorId = loginUser.ID;
                                model.ID = Utils.StrToInt(IdArray[i], 0);
                                bll.SaveRepairReward_Audit(model);
                            }

                        }
                        context.Response.Write("{\"status\":\"1\",\"msg\":\"批量审核成功\"}");
                        status = "1";
                    }
                }
                if (status == "1")
                {
                    BaseWeb.AddOpera(loginUser, int.Parse(RequestHelper.GetString("MenuId")), Enums.ActionEnum.Audit.ToString(), "批量审核" + AssignmentProcedureIdStr);
                }
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        public void GetWeeks(HttpContext context, string btn) {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string yearStr = RequestHelper.GetString("year");
            string monthStr = RequestHelper.GetString("month");
            int year = Utils.StrToInt(yearStr, 2018);
            int month = Utils.StrToInt(monthStr, 1);
            string firstDay = year + "-" +(month<10?"0"+month.ToString():month.ToString())+ "-01";
            string firstDatOfNextMonth=month==12?year+1+"-01-01":year+"-"+(month+1<10?"0"+(month+1).ToString():(month+1).ToString())+ "-01";
            BLL.Repair.repair_Report bll = new BLL.Repair.repair_Report();
            DataSet ds=bll.GetWeeks(year, firstDay, firstDatOfNextMonth);
            if (ds != null && ds.Tables.Count > 0) {
                DataTable dt = ds.Tables[0];
                string rowStr = Utils.ToJson(dt);
                context.Response.Write("{\"status\":\"1\",\"info\":"+rowStr+"}");
            }
        }
        public void GetRepairSchemeFee(HttpContext context, string btn) {
            if (btn != "show") {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                string DateType = RequestHelper.GetString("DateType").Trim();
                string ServerManagerName = RequestHelper.GetString("ServerManagerName").Trim();
                StringBuilder strWhere = new StringBuilder();
                StringBuilder strWhere_Receive = new StringBuilder();
               
                //-------------------------------------------------------------------------

                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and b.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                
                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and b.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and f.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and b.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and b.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                if (ServerManagerName != "") {
                    strWhere.Append(" and e.PerName like '%" + ServerManagerName + "%' ");
                }
                //-----------------------------------------------------------------------------------
                if (DateType == "0")
                {
                    string SchemeDate_Start = RequestHelper.GetString("SchemeDate_Start").Trim();
                    string SchemeDate_End = RequestHelper.GetString("SchemeDate_End").Trim();
                    if (SchemeDate_Start != "")
                    {
                        if (SchemeDate_End != "")
                        {
                            strWhere.Append(" and a.SchemeDate between cast('" + Utils.StrToDateTime(SchemeDate_Start).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(SchemeDate_End + " 23:59:59").ToString() + "' as datetime) ");
                            
                        }
                        else
                        {
                            strWhere.Append(" and a.SchemeDate >= cast('" + Utils.StrToDateTime(SchemeDate_Start).ToString() + "' as datetime) ");
                            strWhere_Receive.Append(" and ReceiveDate >= cast('" + Utils.StrToDateTime(SchemeDate_Start).ToString() + "' as datetime) ");
                        }
                    }
                    else
                    {
                        if (SchemeDate_End != "")
                        {
                            strWhere.Append(" and a.SchemeDate <= cast('" + Utils.StrToDateTime(SchemeDate_End + " 23:59:59").ToString() + "' as datetime) ");
                            strWhere_Receive.Append(" and ReceiveDate <= cast('" + Utils.StrToDateTime(SchemeDate_End + " 23:59:59").ToString() + "' as datetime) ");
                        }
                    }
                }
                else if (DateType == "3")
                {
                    string SchemeDate_Day = RequestHelper.GetString("SchemeDate_Day").Trim();
                    if (SchemeDate_Day != "")
                    {
                        strWhere.Append(" and a.SchemeDate between cast('" + Utils.StrToDateTime(SchemeDate_Day).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(SchemeDate_Day + " 23:59:59").ToString() + "' as datetime) ");
                        strWhere_Receive.Append(" and ReceiveDate < cast('" + Utils.StrToDateTime(SchemeDate_Day + " 23:59:59").ToString() + "' as datetime) ");
                    }
                }
                else if (DateType == "1")
                {
                    string SchemeDate_Month = RequestHelper.GetString("SchemeDate_Month").Trim();
                    if (SchemeDate_Month != "")
                    {
                        string[] monthArray = SchemeDate_Month.Split('-');
                        int year = int.Parse(monthArray[0]);
                        int month = int.Parse(monthArray[1]);
                        strWhere.Append(" and datepart(YY,a.SchemeDate)=" + year + " and datepart(MM,a.SchemeDate)=" + month + " ");
                        strWhere_Receive.Append(" and (datepart(YY,ReceiveDate)=" + year + " and datepart(MM,ReceiveDate)<=" + month + " or datepart(YY,ReceiveDate)<" + year + " ) ");
                    }
                }
                else if (DateType == "2")
                {
                    string SchemeDate_Week = RequestHelper.GetString("SchemeDate_Week").Trim();
                    string SchemeDate_Month = RequestHelper.GetString("SchemeDate_Month").Trim();
                    string[] monthArray = SchemeDate_Month.Split('-');
                    int year = int.Parse(monthArray[0]);
                    int month = int.Parse(monthArray[1]);
                    int week = int.Parse(SchemeDate_Week);
                    strWhere.Append(" and datepart(YY,a.SchemeDate)=" + year + " and datepart(WW,a.SchemeDate)=" + week + " ");
                    strWhere_Receive.Append(" and (datepart(YY,ReceiveDate)=" + year + " and datepart(WW,ReceiveDate)<=" + week + " or datepart(YY,ReceiveDate)<" + year + " )");
                }
                //-----------------------------------------------------------------------------------
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairSchemeFee(strWhere.ToString(),strWhere_Receive.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        public void GetRepairEfficiency(HttpContext context, string btn) {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.Repair.repair_Report bll = new BLL.Repair.repair_Report();
                StringBuilder strWhere = new StringBuilder();
                StringBuilder strOrder = new StringBuilder();
                string ProcedureId = RequestHelper.GetString("RepairProcedureId");
                string MachineLevel = RequestHelper.GetString("MachineLevel");
                if (ProcedureId != "") {
                    strWhere.Append(" and a.ProcedureId=" + Utils.StrToInt(ProcedureId, 0) + " ");
                }
                if (MachineLevel != "") {
                    strWhere.Append(" and e.MachineLevel=" + Utils.StrToInt(MachineLevel, 0) + " ");
                }
                strOrder.Append(" order by RepairSecond_Repair_avg asc ");
                DataTable dt=bll.GetRepairEfficiency_Procedure(strWhere.ToString(),strOrder.ToString()).Tables[0];
                string rowStr = Utils.ToJson(dt);
                context.Response.Write("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowStr + "}");
            }
            catch (Exception e)
            {

                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + e.Message + "\"}");
            }
        }
        public void GetRepairEfficiency_Person(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.Repair.repair_Report bll = new BLL.Repair.repair_Report();
                StringBuilder strWhere = new StringBuilder();
                string RepairerId = RequestHelper.GetString("RepairerId");
                string ProcedureText = RequestHelper.GetString("RepairProcedureText");
                string MachineModel = RequestHelper.GetString("MachineModel");
                string MachineCode = RequestHelper.GetString("MachineCode");
                string IntentionDate_Start = RequestHelper.GetString("IntentionDate_Start");
                string IntentionDate_End = RequestHelper.GetString("IntentionDate_End");
                string FlagStandard = RequestHelper.GetString("FlagStandard");
                if (ProcedureText != "")
                {
                    strWhere.Append(" and a.ProcedureName like '%" + ProcedureText + "%' ");
                }
                if (MachineModel != "")
                {
                    strWhere.Append(" and a.MachineModel like '%" + MachineModel + "%' ");
                }
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + MachineCode + "%' ");
                }
                if (RepairerId != "")
                {
                    strWhere.Append(" and b.ID in ("+RepairerId+") ");
                }
                if (IntentionDate_Start != "")
                {
                    if (IntentionDate_End != "")
                    {
                        strWhere.Append(" and a.IntentionDate between cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(IntentionDate_End + " 23:59:59").ToString() + "' as datetime) ");

                    }
                    else
                    {
                        strWhere.Append(" and a.IntentionDate >= cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime) ");
                    }
                }
                else
                {
                    if (IntentionDate_End != "")
                    {
                        strWhere.Append(" and a.IntentionDate <= cast('" + Utils.StrToDateTime(IntentionDate_End + " 23:59:59").ToString() + "' as datetime) ");
                    }
                }
                if (FlagStandard != "") {
                    if (FlagStandard == "4") {
                        strWhere.Append(" and a.timeStandard is null ");
                    }
                    else if (FlagStandard == "3") {
                        strWhere.Append(" and a.RepairSecond_Repair<0 and a.timeStandard is not null ");
                    }
                    else if (FlagStandard == "2") {
                        strWhere.Append(" and a.RepairSecond_Repair>a.timeStandard*3600 ");
                    }
                    else if (FlagStandard == "1") {
                        strWhere.Append(" and a.RepairSecond_Repair<=a.timeStandard*3600 and a.RepairSecond_Repair>=0 ");
                    }
                }
                DataTable dt = bll.GetRepairEfficiency_Person(strWhere.ToString()).Tables[0];
                string rowStr = Utils.ToJson(dt);
                context.Response.Write("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowStr + "}");
            }
            catch (Exception e)
            {

                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + e.Message + "\"}");
            }
        }
        public void GetRepairTimeStatistics(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                string DateType = RequestHelper.GetString("DateType").Trim();
                StringBuilder strWhere = new StringBuilder();

                //-------------------------------------------------------------------------

                //维修意向号

                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }

                //客户名

                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and b.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }

                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                //-----------------------------------------------------------------------------------
                if (DateType == "0")
                {
                    string IntentionDate_Start = RequestHelper.GetString("IntentionDate_Start").Trim();
                    string IntentionDate_End = RequestHelper.GetString("IntentionDate_End").Trim();
                    if (IntentionDate_Start != "")
                    {
                        if (IntentionDate_End != "")
                        {
                            strWhere.Append(" and a.IntentionDate between cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(IntentionDate_End + " 23:59:59").ToString() + "' as datetime) ");

                        }
                        else
                        {
                            strWhere.Append(" and a.IntentionDate >= cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime) ");
                        }
                    }
                    else
                    {
                        if (IntentionDate_End != "")
                        {
                            strWhere.Append(" and a.IntentionDate <= cast('" + Utils.StrToDateTime(IntentionDate_End + " 23:59:59").ToString() + "' as datetime) ");
                        }
                    }
                }
                else if (DateType == "3")
                {
                    string IntentionDate_Day = RequestHelper.GetString("IntentionDate_Day").Trim();
                    if (IntentionDate_Day != "")
                    {
                        strWhere.Append(" and a.IntentionDate between cast('" + Utils.StrToDateTime(IntentionDate_Day).ToString() + "' as datetime) and cast('" + Utils.StrToDateTime(IntentionDate_Day + " 23:59:59").ToString() + "' as datetime) ");
                    }
                }
                else if (DateType == "1")
                {
                    string IntentionDate_Month = RequestHelper.GetString("IntentionDate_Month").Trim();
                    if (IntentionDate_Month != "")
                    {
                        string[] monthArray = IntentionDate_Month.Split('-');
                        int year = int.Parse(monthArray[0]);
                        int month = int.Parse(monthArray[1]);
                        strWhere.Append(" and datepart(YY,a.IntentionDate)=" + year + " and datepart(MM,a.IntentionDate)=" + month + " ");
                    }
                }
                else if (DateType == "2")
                {
                    string IntentionDate_Week = RequestHelper.GetString("IntentionDate_Week").Trim();
                    string IntentionDate_Month = RequestHelper.GetString("IntentionDate_Month").Trim();
                    string[] monthArray = IntentionDate_Month.Split('-');
                    int year = int.Parse(monthArray[0]);
                    int month = int.Parse(monthArray[1]);
                    int week = int.Parse(IntentionDate_Week);
                    strWhere.Append(" and datepart(YY,a.IntentionDate)=" + year + " and datepart(WW,a.IntentionDate)=" + week + " ");
                }
                //-----------------------------------------------------------------------------------
                SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
                DataTable dt = bll.GetRepairTimeStatistics(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
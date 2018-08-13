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
using System.Configuration;
namespace SCZM.Web.Ashx.Repair
{
    /// <summary>
    /// Repair_Assignment 的摘要说明
    /// <summary>
    public class repair_Assignment : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string btn = RequestHelper.GetQueryString("Btn");
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
                case "GetList": //获取维修派工列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取维修派工明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存维修派工信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除维修派工信息
                    DelData(context, btn);
                    break;
                case "GetScheduleList":
                    GetScheduleList(context, btn);
                    break;
                case "UndoData":
                    UndoData(context, btn);
                    break;
            }
        }
        #region 获取维修派工列表==============================;
        private void GetList(HttpContext context, string btn)
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
                    strWhere.Append(" and b.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                //if (IntentionType != "")
                //{
                //    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType, 0));
                //}
                //派工单号
                if (AssignmentCode != "" && Utils.IsSafeSqlString(AssignmentCode))
                {
                    strWhere.Append(" and a.AssignmentCode like '%" + Utils.Filter(AssignmentCode) + "%'");
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and b.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名
                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and b.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and c.MachineModel like '%" + Utils.Filter(MachineModel)+"%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and b.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //派工日期
                if (AssignmentDate_Start != "")
                {
                    strWhere.Append(" and a.AssignmentDate>= cast('" + Utils.StrToDateTime(AssignmentDate_Start).ToString() + "' as datetime)");
                }
                if (AssignmentDate_End != "")
                {
                    strWhere.Append(" and a.AssignmentDate<=cast('" + Utils.StrToDateTime(AssignmentDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and b.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                
                SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
                DataTable dt = bll.GetList(strWhere.ToString()).Tables[0];
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

        #region 获取维修派工明细==============================;
        private void GetDetail(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int ID = RequestHelper.GetInt("ID", 0);

                SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
                DataSet ds = bll.GetDetail(ID);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);
                DataTable Assignment_ProcessDT = ds.Tables["Assignment_Process"];
                string Assignment_ProcessStr = Utils.ToJson(Assignment_ProcessDT);
                DataTable Assignment_ActualListDT = ds.Tables["Assignment_ActualList"];
                string Assignment_ActualList = Utils.ToJson(Assignment_ActualListDT);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr);
                jsonStr.Append(",\"Assignment_ProcessInfo\":" + Assignment_ProcessStr);
                jsonStr.Append(",\"Assignment_ActualList\":" + Assignment_ActualList);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 保存维修派工信息==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            string IntentionId = RequestHelper.GetString("IntentionId");
            string AssignmentCode = RequestHelper.GetString("AssignmentCode");
            string AssignmentDate = RequestHelper.GetString("AssignmentDate");
            string ExpectStartDate = RequestHelper.GetString("ExpectStartDate");
            string ExpectCompleteDate = RequestHelper.GetString("ExpectCompleteDate");
            string MainRepair = RequestHelper.GetString("MainRepair").Trim();
            string AssistRepair = RequestHelper.GetString("AssistRepair").Trim();
            string detailStr = RequestHelper.GetString("detailStr");
            string procedureList_prev = RequestHelper.GetString("procedureList_prev");
            string WorkContent = RequestHelper.GetString("WorkContent").Trim();

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.Repair_Assignment model = new SCZM.Model.Repair.Repair_Assignment();
            SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
            model.ID = Utils.StrToInt(ID, 0);
            model.IntentionId = Utils.StrToInt(IntentionId, 0);
            if (AssignmentCode == "") {
                AssignmentCode = "PG" + DateTime.Now.ToString("yyyyMMdd") + bll.GetMaxId();
            }
            model.AssignmentCode = AssignmentCode;
            if (AssignmentDate != "")
            {
                model.AssignmentDate = Utils.StrToDateTime(AssignmentDate);
            }
            if (ExpectStartDate != "")
            {
                model.ExpectStartDate = Utils.StrToDateTime(ExpectStartDate);
            }
            if (ExpectCompleteDate != "")
            {
                model.ExpectCompleteDate = Utils.StrToDateTime(ExpectCompleteDate);
            }
            model.MainRepair = Utils.StrToInt(MainRepair, 0);
            model.AssistRepair = AssistRepair;
            model.WorkContent = WorkContent;
            List<Model.Repair.Repair_Assignment_Procedure> modelList = new List<Model.Repair.Repair_Assignment_Procedure>();
            if (detailStr != "") {
                string[] detailArray = detailStr.Split('≮');
                for (int i = 0; i < detailArray.Length; i++) {
                    string[] detailMXArray = detailArray[i].Split('⊥');
                    Model.Repair.Repair_Assignment_Procedure model_Procedure = new Model.Repair.Repair_Assignment_Procedure();
                    model_Procedure.ProcedureId = Utils.StrToInt(detailMXArray[0], 0);
                    model_Procedure.Num = Utils.StrToDecimal(detailMXArray[1], 0);
                    model_Procedure.WorkContent = Utils.Filter(detailMXArray[2]);
                    model_Procedure.AllNat = Utils.StrToDecimal(detailMXArray[3],0);
                    modelList.Add(model_Procedure);
                }
            }
            model.Repair_Assignment_Procedure = modelList;
            model.OperaDepId = loginUserModel.DepId;
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;

            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (ID == "")
                {
                    model.ID = bll.Add(model, out operaMessage);
                    if (model.ID > 0)
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Add.ToString();
                        operaMemo = "新增维修派工：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model,procedureList_prev, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改维修派工：" + model.ID;
                    }
                }
                if (status == "1")
                {
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                if (status == "1") {
                    string userIdList = "";
                    if (MainRepair != "")
                    {
                        userIdList += MainRepair;
                        if (model.AssistRepair != "")
                        {
                            userIdList += "," + AssistRepair;
                        }
                    }
                    if (userIdList != "") {
                        try
                        {
                            BLL.WX.WX_GetLoginInfo bll_Wx = new BLL.WX.WX_GetLoginInfo();
                            DataSet wx_AccountDs = bll_Wx.getWxAccount_Id(userIdList);
                            if (wx_AccountDs != null && wx_AccountDs.Tables[0].Rows.Count > 0)
                            {
                                string touser = "", touser_ds = "";
                                for (int i = 0; i < wx_AccountDs.Tables[0].Rows.Count; i++)
                                {
                                    touser_ds = wx_AccountDs.Tables[0].Rows[i][0].ToString();
                                    if (touser_ds != "")
                                    {
                                        touser += touser_ds + "|";
                                    }
                                }
                                if (touser.Length > 0)
                                {
                                    touser = touser.Remove(touser.Length - 1);
                                    DataRow dr = bll.GetDetail(model.ID).Tables[0].Rows[0];
                                    //string message = "有新的派工，请及时查看";context.Request.Url.Authority + "/SCZM/Pages/WeiXin/WX_Schedule_List_procedure.html?AssignmentId="+model.ID
                                    WX.WX_GetLoginInfo.GetMessageCard(touser, "", "有新的派工，请及时查看。", " 派工单：" + dr["AssignmentCode"] + "\n 客户：" + dr["CustName"] + "\n 机型：" + dr["MachineModel"] + "\n 机号：" + dr["MachineCode"] + "\n 维修担当：" + dr["MainRepairName"] + "\n 计划维修日期：\n " + DateTime.Parse(dr["ExpectStartDate"].ToString()).ToLongDateString() + "-" + DateTime.Parse(dr["ExpectCompleteDate"].ToString()).ToLongDateString(), "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + new BLL.System.sys_Config().loadConfig().WxCorpid + "&redirect_uri=" + context.Request.Url.Authority + ConfigurationManager.AppSettings["appName"] + "Pages/WeiXin/WX_Schedule_List_new.html&response_type=code&scope=snsapi_userinfo&agentid=" + WX.WX_GetLoginInfo.GetAgentid("Schedule") + "#wechat_redirect", "查看", "Schedule");
                                    //WX.WX_GetLoginInfo.GetMessage(touser,"", message);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            operaMessage+="微信推送失败（"+e.Message+"），请及时联系系统管理员";
                        }
                    }
                }
                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion

        #region 删除维修派工信息==============================;
        private void DelData(HttpContext context, string btn)
        {
            if (btn != "btnDel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string IDStr = RequestHelper.GetString("IDStr");
            if (IDStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要删除的记录！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.DeleteList(PageValidate.SafeLongFilter(IDStr, 0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除维修派工：" + IDStr;
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        #region 获取反馈进度列表==============================;
        private void GetScheduleList(HttpContext context, string btn)
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
                //string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();

                StringBuilder strWhere = new StringBuilder();

                //维修意向号
                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and c.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
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
                    strWhere.Append(" and c.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名
                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and c.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and d.MachineModel like '%" + Utils.Filter(MachineModel)+"%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and c.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and c.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                //维修进度
                string ScheduleType = RequestHelper.GetString("ScheduleType");
                if(ScheduleType!=""){
                if (ScheduleType=="0,1,2") {
                    strWhere.Append(" and (ScheduleType in ("+ScheduleType+") or ScheduleType is null ) ");
                }
                else if (ScheduleType == "5") {
                    strWhere.Append(" and ScheduleType in (1,2) ");
                }
                else if (ScheduleType == "0")
                {
                    strWhere.Append(" and (ScheduleType=0 or ScheduleType is null) ");
                }
                else if (ScheduleType != "4")
                {
                    strWhere.Append(" and ScheduleType=" + ScheduleType);
                }
                }
                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and b.MainRepair in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------

                SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
                DataTable dt = bll.GetScheduleList(strWhere.ToString(),LoginUserModel.ID,LoginUserModel.IsAdmin).Tables[0];
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
        private void UndoData(HttpContext context, string btn)
        {
            if (btn != "btnUndo")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string IDStr = RequestHelper.GetString("IDStr");
            if (IDStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要作废的记录！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.UndoList(PageValidate.SafeLongFilter(IDStr, 0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "作废维修派工：" + IDStr;
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

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
using SCZM.Web.Ashx.WX;
namespace SCZM.Web.Ashx.Repair
{
    /// <summary>
    /// repair_Schedule 的摘要说明
    /// <summary>
    public class repair_Schedule : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取进度反馈列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取进度反馈明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存进度反馈信息
                    SaveData_All(context, btn);
                    break;
                case "DelData": //删除进度反馈信息
                    DelData(context, btn);
                    break;
                case "GetList_HaveAttachment"://包含附件的反馈列表
                    GetList_HaveAttachment(context, btn);
                    break;
                case "DeleteLastSchedule":
                    DeleteLastSchedule(context, btn);
                    break;
            }
        }
        #region 获取进度反馈列表==============================;
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                StringBuilder strWhere = new StringBuilder();
                string AssignmentProcedureId = RequestHelper.GetString("AssignmentProcedureId");
                if (AssignmentProcedureId != "")
                {
                    strWhere.Append(" and a.AssignmentProcedureId=" + Utils.StrToInt(AssignmentProcedureId, 0));
                }

                //------------------------------------
                SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
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

        #region 获取进度反馈明细==============================;
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

                SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
                DataSet ds = bll.GetDetail(ID);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);
                DataTable AttachmentDT = ds.Tables["attachment"];
                string attachmentStr = Utils.ToJson(AttachmentDT);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr);
                jsonStr.Append(",\"attachmentInfo\":" + attachmentStr);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 保存进度反馈信息==============================;
        private void SaveData_All(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ScheduleStr = RequestHelper.GetString("ScheduleStr");
            string ScheduleType = RequestHelper.GetString("ScheduleType");
            string ScheduleDate = RequestHelper.GetString("ScheduleDate");
            string AttachmentList_Schedule = RequestHelper.GetString("AttachmentId_Schedule");
            string Memo = RequestHelper.GetString("Memo").Trim();
            string ScheduleId = RequestHelper.GetString("ScheduleId");
            string PauseReason = RequestHelper.GetString("PauseReason");
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.repair_Schedule model = new SCZM.Model.Repair.repair_Schedule();
            string[] modelList = ScheduleStr.Split(',');
            for (int i = 0; i < modelList.Length; i++)
            {
                model = new Model.Repair.repair_Schedule();
                string[] modelPart = modelList[i].Split('⊥');
                model.AssignmentId = Utils.StrToInt(modelPart[0].ToString(), 0);
                model.AssignmentProcedureId = Utils.StrToInt(modelPart[1].ToString(), 0);
                model.ProcedureId = Utils.StrToInt(modelPart[2].ToString(), 0);

                model.ScheduleType = Utils.StrToInt(ScheduleType, 0);
                if (Memo != "" && Utils.IsSafeSqlString(Memo))
                {
                    model.Memo = Utils.Filter(Memo);
                }
                if (ScheduleDate != "")
                {
                    model.ScheduleDate = Utils.StrToDateTime(ScheduleDate);
                }
                if (AttachmentList_Schedule != "")
                {
                    model.AttachmentList_Schedule = AttachmentList_Schedule;
                }
                model.OperaDepId = loginUserModel.DepId;
                model.OperaId = loginUserModel.ID;
                model.OperaName = loginUserModel.PerName;
                model.OperaTime = DateTime.Now;
                model.ID = Utils.StrToInt(ScheduleId, 0);
                model.PauseReason = Utils.StrToInt(PauseReason, -1);
                SaveData(model, context);
            }
        }
        private void SaveData(Model.Repair.repair_Schedule model, HttpContext context)
        {
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (model.ID==0)
                {
                model.ID = bll.Add(model, out operaMessage);
                if (model.ID > 0)
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Add.ToString();
                    operaMemo = "新增进度反馈：" + model.ID;
                }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改进度反馈：" + model.ID;
                    }
                }
                if (status == "1")
                {
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                //if (status == "1") {
                //    BLL.WX.WX_GetLoginInfo bll_Wx = new BLL.WX.WX_GetLoginInfo();
                //    bll_Wx.getWxAccount_Procedure
                //}
                if (status == "1" && model.PauseReason == 5)
                {
                    //-----------------------------------------------------------
                    string PartDepId = "23";
                    BLL.WX.WX_GetLoginInfo bll_Wx = new BLL.WX.WX_GetLoginInfo();
                    DataSet wx_AccountDs = bll_Wx.getWxAccount_Dep(PartDepId);
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

                            SCZM.BLL.Repair.Repair_Assignment bll_Assignment = new BLL.Repair.Repair_Assignment();
                            DataTable dt_CustMessage = bll_Assignment.GetComboList(" and a.ID=" + model.AssignmentId + " ").Tables[0];
                            SCZM.BLL.Base.base_Procedure bll_Procedure = new BLL.Base.base_Procedure();
                            DataTable dt_Procedure = bll_Procedure.GetComboList(" and a.ID=" + model.ProcedureId + " ").Tables[0];
                            WX_GetLoginInfo.GetMessage(touser, "", "客户：" + dt_CustMessage.Rows[0]["CustName"] + "\n机型：" + dt_CustMessage.Rows[0]["MachineModel"] + " \n机号：" + dt_CustMessage.Rows[0]["MachineCode"] + "\n工序：" + dt_Procedure.Rows[0]["ProcedureName"] + "\n维修担当：" + dt_CustMessage.Rows[0]["PerName"] + "\n因等件原因：" + model.Memo + "\n暂停维修,请及时沟通。", "Schedule");
                        }
                    }
                    //-----------------------------------------------------------
                  
                }
                else if (status == "1" && model.ScheduleType == 3)
                {
                    BLL.Repair.Repair_Assignment bll_Assignment = new BLL.Repair.Repair_Assignment();
                    //model.AssignmentId; 派工ID 
                    //model.AssignmentProcedureId 派工工序ID
                    //---------------------2018/6/26---取消审核（系统内默认审核同意，需手动审核时删掉此段+Wx_GetLoginInfo.ashx也有同样的设置，需同步修改）-----------------------------
                    Model.Repair.Repair_Assignment_Procedure model_AssignmentProcedure = null;
                    BLL.Repair.repair_Report bll_Report = new BLL.Repair.repair_Report();
                    DataTable dt = bll_Assignment.GetAssignmentProcedureList(" and a.ID="+model.AssignmentProcedureId+" ").Tables[0];

                        
                            DataRow[] dr = dt.Select("ID=" + model.AssignmentProcedureId);
                            if (dr.Length == 1)
                            {
                                model_AssignmentProcedure = new Model.Repair.Repair_Assignment_Procedure();
                                if (dr[0]["AllNat"].ToString() != "")
                                {
                                    model_AssignmentProcedure.AllNat_Audit = Utils.StrToDecimal(dr[0]["AllNat"].ToString(), 0);
                                }
                                else
                                {
                                    DataTable dt_Detail = bll_Report.GetRepairerRewardDetail(" and d.ID=" + model.AssignmentProcedureId + " ").Tables[0];
                                    if (dt_Detail.Rows[0]["AllNat"].ToString() != "")
                                    {
                                        model_AssignmentProcedure.AllNat_Audit = Utils.StrToDecimal(dt_Detail.Rows[0]["AllNat"].ToString(), 0) * Utils.StrToDecimal(dr[0]["Num"].ToString(), 0);
                                    }
                                }
                                model_AssignmentProcedure.AuditDate = DateTime.Now;
                                model_AssignmentProcedure.AuditOpinion = "";
                                model_AssignmentProcedure.AuditState = 1;
                                model_AssignmentProcedure.AuditorId = loginUserModel.ID;
                                model_AssignmentProcedure.ID = model.AssignmentProcedureId;
                                bll_Report.SaveRepairReward_Audit(model_AssignmentProcedure);
                            }
                        
                    //----------------------------zhx----------------上箭头-------------------------------------------------------------------

                    BLL.WX.WX_GetLoginInfo bll_Wx = new BLL.WX.WX_GetLoginInfo();
                    DataSet wx_AccountDs = bll_Wx.getWxAccount_Procedure(model.AssignmentProcedureId);
                    if (wx_AccountDs != null && wx_AccountDs.Tables[0].Rows.Count > 0)
                    {
                        string touser = "", touser_ds = "";
                        for (int i = 0; i < wx_AccountDs.Tables[0].Rows.Count; i++)
                        {
                            touser_ds = wx_AccountDs.Tables[0].Rows[i]["WXNo"].ToString();
                            if (touser_ds != "")
                            {
                                touser += touser_ds + "|";
                            }
                        }
                        if (touser.Length > 0)
                        {
                            touser = touser.Remove(touser.Length - 1);

                            //SCZM.BLL.Repair.Repair_Assignment bll_Assignment = new BLL.Repair.Repair_Assignment();
                            DataTable dt_CustMessage = bll_Assignment.GetComboList(" and a.ID=" + model.AssignmentId + " ").Tables[0];
                            SCZM.BLL.Base.base_Procedure bll_Procedure = new BLL.Base.base_Procedure();
                            DataTable dt_Procedure = bll_Procedure.GetComboList(" and a.ID=" + model.ProcedureId + " ").Tables[0];
                            WX_GetLoginInfo.GetMessage(touser, "", "客户：" + dt_CustMessage.Rows[0]["CustName"] + "\n机型：" + dt_CustMessage.Rows[0]["MachineModel"] + " \n机号：" + dt_CustMessage.Rows[0]["MachineCode"] + "\n维修担当：" + dt_CustMessage.Rows[0]["PerName"] + "\n工序：" + dt_Procedure.Rows[0]["ProcedureName"] + "\n维修已完成。" + model.Memo + "\n", "Schedule");
                        }
                    }
                    //---------------------------------------------
                    
                }
                else if (status == "1" && (model.ScheduleType == 1||model.ScheduleType == 2))
                {

                    BLL.Repair.Repair_Assignment bll_Assignment = new BLL.Repair.Repair_Assignment();
                    BLL.WX.WX_GetLoginInfo bll_Wx = new BLL.WX.WX_GetLoginInfo();
                    DataSet wx_AccountDs = bll_Wx.getWxAccount_Procedure_onlyGroup(model.AssignmentProcedureId);
                    if (wx_AccountDs != null && wx_AccountDs.Tables[0].Rows.Count > 0)
                    {
                        string touser = "", touser_ds = "";
                        for (int i = 0; i < wx_AccountDs.Tables[0].Rows.Count; i++)
                        {
                            touser_ds = wx_AccountDs.Tables[0].Rows[i]["WXNo"].ToString();
                            if (touser_ds != "")
                            {
                                touser += touser_ds + "|";
                            }
                        }
                        if (touser.Length > 0)
                        {
                            touser = touser.Remove(touser.Length - 1);
                            
                            //SCZM.BLL.Repair.Repair_Assignment bll_Assignment = new BLL.Repair.Repair_Assignment();
                            DataTable dt_CustMessage = bll_Assignment.GetComboList(" and a.ID=" + model.AssignmentId + " ").Tables[0];
                            SCZM.BLL.Base.base_Procedure bll_Procedure = new BLL.Base.base_Procedure();
                            DataTable dt_Procedure = bll_Procedure.GetComboList(" and a.ID=" + model.ProcedureId + " ").Tables[0];
                            WX_GetLoginInfo.GetMessage(touser, "", "客户：" + dt_CustMessage.Rows[0]["CustName"] + "\n机型：" + dt_CustMessage.Rows[0]["MachineModel"] + " \n机号：" + dt_CustMessage.Rows[0]["MachineCode"] + "\n维修担当：" + dt_CustMessage.Rows[0]["PerName"] + "\n工序：" + dt_Procedure.Rows[0]["ProcedureName"] + "\n维修"+(model.ScheduleType==1?"已开始":"已暂停")+"。" + model.Memo + "\n", "Schedule");
                        }
                    }
                }
                if (status == "1") {
                    if (bll.GetScheduleType(model.AssignmentProcedureId) == 3)
                    {
                        BLL.WX.WX_GetLoginInfo bll_Wx = new BLL.WX.WX_GetLoginInfo();
                        BLL.Repair.Repair_Assignment bll_Assignment = new BLL.Repair.Repair_Assignment();
                        DataTable dt_repairSecond = bll.GetRepairSceond(model.AssignmentProcedureId).Tables[0];
                        DataSet wx_AccountDs_onlyGroup = bll_Wx.getWxAccount_Procedure_onlyGroup(model.AssignmentProcedureId);
                        if (wx_AccountDs_onlyGroup != null && wx_AccountDs_onlyGroup.Tables[0].Rows.Count > 0)
                        {
                            string touser = "", touser_ds = "";
                            for (int i = 0; i < wx_AccountDs_onlyGroup.Tables[0].Rows.Count; i++)
                            {
                                touser_ds = wx_AccountDs_onlyGroup.Tables[0].Rows[i]["WXNo"].ToString();
                                if (touser_ds != "")
                                {
                                    touser += touser_ds + "|";
                                }
                            }
                            if (touser.Length > 0)
                            {
                                touser = touser.Remove(touser.Length - 1);
                                int AllSecond = Convert.IsDBNull(dt_repairSecond.Rows[0]["AllSecond"]) == true ? 0 : Convert.ToInt32(dt_repairSecond.Rows[0]["AllSecond"]);
                                int RepairSecond = Convert.IsDBNull(dt_repairSecond.Rows[0]["RepairSecond"]) == true ? 0 : Convert.ToInt32(dt_repairSecond.Rows[0]["RepairSecond"]);
                                int PauseSecond = Convert.IsDBNull(dt_repairSecond.Rows[0]["PauseSecond"]) == true ? 0 : Convert.ToInt32(dt_repairSecond.Rows[0]["PauseSecond"]);
                                WX_GetLoginInfo.GetMessage(touser, "", "本次派工工序总用时：" + WX_GetLoginInfo.GetDateTime(0, 0, 0, AllSecond) + "\n维修用时：" + WX_GetLoginInfo.GetDateTime(0, 0, 0, RepairSecond) + "\n暂停用时：" + WX_GetLoginInfo.GetDateTime(0, 0, 0, PauseSecond) + "", "Schedule");
                                bll_Assignment.UpdateRepairSecond(model.AssignmentProcedureId, AllSecond, RepairSecond, PauseSecond);
                            }
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

        #region 删除进度反馈信息==============================;
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
            SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
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
                    operaMemo = "删除进度反馈：" + IDStr;
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
        #region 包含附件的反馈列表
        private void GetList_HaveAttachment(HttpContext context, string btn) {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                StringBuilder strWhere = new StringBuilder();
                string AssignmentProcedureId = RequestHelper.GetString("AssignmentProcedureId");
                if (AssignmentProcedureId != "")
                {
                    strWhere.Append(" and a.AssignmentProcedureId=" + Utils.StrToInt(AssignmentProcedureId, 0));
                }
                //------------------------------------
                SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
                DataTable dt = bll.GetList_HaveAttachment(strWhere.ToString()).Tables[0];
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
        #region 删除最后信息==============================;
        private void DeleteLastSchedule(HttpContext context, string btn)
        {
            if (btn != "btnDel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            if (ID == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要删除的记录！\"}");
                return;
            }
            string prevId = RequestHelper.GetString("prevId");
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.DeleteLastSchedule(Utils.StrToInt(ID,0),Utils.StrToInt(prevId,0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除进度反馈：" + ID;
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

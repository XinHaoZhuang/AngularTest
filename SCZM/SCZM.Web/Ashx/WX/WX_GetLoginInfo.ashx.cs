using Newtonsoft.Json.Linq;
using SCZM.Common;
using SCZM.WebUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace SCZM.Web.Ashx.WX
{
    /// <summary>
    /// GetLoginInfo 的摘要说明
    /// </summary>
    public class WX_GetLoginInfo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (RequestHelper.GetString("action").Trim() != "")
            {
                switch (RequestHelper.GetString("action").Trim())
                {
                    case "GetIntentionList"://维修意向列表
                        GetIntentionList(context);
                        break;
                    //case "GetLoginInfo":
                    //    GetLoginInfo(context);
                    //    break;
                    case "GetIntentionDetail"://维修意向详情
                        GetIntentionDetail(context);
                        break;
                    case "GetScheduleList"://进度反馈列表
                        GetScheduleList(context);
                        break;
                    case "GetScheduleList_search"://进度反馈列表
                        GetScheduleList_search(context);
                        break;
                    case "GetScheduleDetail"://进度反馈详情
                        GetScheduleDetail(context);
                        break;
                    case "GetUserId"://微信账号
                        GetUserId(context);
                        break;
                    case "GetMachineModelCombo_WX"://机型
                        GetMachineModelCombo_WX(context);
                        break;
                    case "GetRepairTypeCombo_WX"://维修类型
                        GetRepairTypeCombo_WX(context);
                        break;
                    case "SaveIntention"://保存维修意向
                        SaveIntention(context);
                        break;
                    case "SaveSchedule"://保存进度反馈
                        SaveSchedule(context);
                        break;
                    case "UploadFiles"://上传文件
                        UploadFiles(context);
                        break;
                    case "DelFile"://删除文件
                        DelFile(context);
                        break;
                    case "GetFiles"://获取文件
                        GetFiles(context);
                        break;
                    case "DelFiles"://批量删除文件
                        DelFiles(context);
                        break;
                    case "GetRepairPackage_WX"://维修套包
                        GetRepairPackage_WX(context);
                        break;
                    case "DelIntention_WX"://删除维修意向
                        DelIntention_WX(context);
                        break;
                    case "GetSignature"://签名
                        GetSignature(context);
                        break;
                    case "getTemporaryMaterial"://临时素材
                        getTemporaryMaterial(context);
                        break;
                    case "getTemporaryMaterial_change"://临时素材
                        getTemporaryMaterial_change(context);
                        break;
                    case "DelLastSchedule":
                        DelLastSchedule(context);
                        break;
                    case "GetAssignmentList":
                        GetAssignmentList(context);
                        break;
                    case "GetAssignmentList_search":
                        GetAssignmentList_search(context);
                        break;
                    case "GetMessage":
                        GetMessage(context);
                        break;
                    case "GetRepairReward":
                        GetRepairReward(context);
                        break;
                    case "GetRepairReward_Manager":
                        GetRepairReward_Manager(context);
                        break;
                    case "GetRepairReward_RepairerDetail":
                        GetRepairReward_RepairerDetail(context);
                        break;
                    case "getMenu":
                        getMenu(context);
                        break;
                }
            }
            else
            {
                context.Response.Write("{\"state\":0,\"msg\":\"登陆失败\"}");
            }
        }
        private void GetAssignmentList_search(HttpContext context)
        {
            try
            {
                string MachineModelId = RequestHelper.GetString("MachineModelId").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string AssignmentType = RequestHelper.GetString("AssignmentType").Trim();
                string group = RequestHelper.GetString("group").Trim();
                StringBuilder strWhere = new StringBuilder();
                StringBuilder strWhere_all = new StringBuilder();
                //机型
                if (MachineModelId != "")
                {
                    strWhere.Append(" and MachineModelId=" + Utils.StrToInt(MachineModelId, 0));
                }
                //机号
                if (MachineCode != "" && Utils.IsSafeSqlString(MachineCode))
                {
                    strWhere.Append(" and MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //----------------------------------------------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                // if (LoginUserModel.IsAdmin == false) {
                //    strWhere.Append(" and b.MainRepair =" + LoginUserModel.ID);
                //}
                //----------------------------------------------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and b.MainRepair in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //----------------------------------------------------------------------------------------------------
                if (AssignmentType != "" && AssignmentType != "4")
                {
                    strWhere_all.Append(" where a.AssignmentType=" + AssignmentType);
                }
                //--------------------------------
                if (group != "")
                {
                    if (strWhere_all.Length > 0)
                    {
                        strWhere_all.Append(" and ','+MainRepairsDep like '%," + group + ",%' ");
                    }
                    else
                    {
                        strWhere_all.Append(" where ','+MainRepairsDep like '%," + group + ",%' ");
                    }
                }
                //--------------------------------
                SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
                DataTable dt = bll.GetAssignmentList_search(strWhere.ToString(), LoginUserModel.ID, LoginUserModel.IsAdmin, strWhere_all.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"state\":\"1\",\"msg\":\"数据获取成功！\",\"ScheduleList\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        private void GetAssignmentList(HttpContext context)
        {
            try
            {
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModelId = RequestHelper.GetString("MachineModelId").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string AssignmentType = RequestHelper.GetString("AssignmentType").Trim();
                string MainRepairName = RequestHelper.GetString("MainRepairName").Trim();
                string group = RequestHelper.GetString("group").Trim();
                StringBuilder strWhere = new StringBuilder();
                StringBuilder strWhere_all = new StringBuilder();
                //机型
                if (MachineModelId != "")
                {
                    strWhere.Append(" and MachineModelId=" + Utils.StrToInt(MachineModelId, 0));
                }
                //机号
                if (MachineCode != "" && Utils.IsSafeSqlString(MachineCode))
                {
                    strWhere.Append(" and MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                if (CustName != "") {
                    strWhere.Append(" and CustName like '%" + CustName + "%' ");
                }
                //----------------------------------------------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                // if (LoginUserModel.IsAdmin == false) {
                //    strWhere.Append(" and b.MainRepair =" + LoginUserModel.ID);
                //}
                //----------------------------------------------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and b.MainRepair in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //----------------------------------------------------------------------------------------------------
                if (AssignmentType != "" && AssignmentType != "4")
                {
                    strWhere_all.Append(" and a.AssignmentType=" + AssignmentType + " ");
                }
                //--------------------------------
                if (MainRepairName != "")
                {
                    strWhere_all.Append(" and a.MainRepairs like '%" + MainRepairName + "%' ");
                }
                //----------------------------------
                if (group != "")
                {
                    strWhere_all.Append(" and a.AssignmentProcedureCount-a.RepairingProcedureCount>0 and ','+MainRepairsDep like '%," + group + ",%' ");
                }
                //------------------------------------
                SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
                DataTable dt = bll.GetAssignmentList(strWhere.ToString(), LoginUserModel.ID, LoginUserModel.IsAdmin, strWhere_all.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"state\":\"1\",\"msg\":\"数据获取成功！\",\"ScheduleList\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        private void DelLastSchedule(HttpContext context)
        {
            string currentId = RequestHelper.GetString("currentId");
            if (currentId == "")
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"请选择需要删除的记录！\"}");
                return;
            }
            string previousId = RequestHelper.GetString("previousId");

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.DeleteLastSchedule(Utils.StrToInt(currentId, 0), Utils.StrToInt(previousId, 0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除进度反馈(微信端)：" + currentId;
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, -1, operaAction, operaMemo);
                }
                context.Response.Write("{\"state\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        private void DelIntention_WX(HttpContext context)
        {
            string IntentionId = RequestHelper.GetString("IntentionId");
            if (IntentionId == "")
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"请选择需要删除的记录！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.DeleteList(PageValidate.SafeLongFilter(IntentionId, 0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除维修意向(微信端)：" + IntentionId;
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, -1, operaAction, operaMemo);
                }
                context.Response.Write("{\"state\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        private void GetRepairPackage_WX(HttpContext context)
        {
            string emptyStr = "[{\"PackageId\":\"0\",\"MachineModelId\":\"0\",\"MachineModel\":\"无\",\"PackageName\":\"无\"}]";

            try
            {
                string strWhere = "";
                string MachineModelId = RequestHelper.GetString("MachineModelId").Trim();
                if (MachineModelId != "")
                {
                    strWhere = " and a.MachineModelId=" + Utils.StrToInt(MachineModelId, 0);
                }
                BLL.Base.base_RepairPackage bll = new BLL.Base.base_RepairPackage();
                DataTable dt = bll.GetComboList(strWhere).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        private void DelFiles(HttpContext context)
        {
            try
            {
                string DelIdList = RequestHelper.GetString("DelIdList");

                string message = "";
                BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();
                bll.DeleteList(Utils.Filter(DelIdList), out message);
                context.Response.Write("{\"state\":1,\"msg\":\"删除成功\"}");
            }
            catch
            {

                context.Response.Write("{\"state\":0,\"msg\":\"删除失败\"}");
            }

        }
        private void GetFiles(HttpContext context)
        {
            try
            {
                string IdList = RequestHelper.GetString("IdList");
                BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();
                string strWhere = " ID in(" + Utils.Filter(IdList) + ") ";
                DataTable dt = bll.GetList(strWhere).Tables[0];
                string Files = Utils.ToJson(dt);
                context.Response.Write("{\"state\":1,\"msg\":\"获取成功\",\"Files\":" + Files + "}");
            }
            catch
            {
                context.Response.Write("{\"state\":0,\"msg\":\"附件获取失败\"}");

            }
        }
        private void DelFile(HttpContext context)
        {
            try
            {
                string DelId = RequestHelper.GetString("DelId");
                int id = Utils.StrToInt(DelId, 0);
                string message = "";
                BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();
                bll.Delete(id, out message);
                context.Response.Write("{\"state\":1,\"msg\":\"删除成功\"}");
            }
            catch
            {

                context.Response.Write("{\"state\":0,\"msg\":\"删除失败\"}");
            }

        }
        private void UploadFiles(HttpContext context)
        {
            HttpFileCollection files = context.Request.Files;

            string IdList = "";
            if (files.Count > 0)
            {
                try
                {
                    string message = "";
                    Random rd = new Random();
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];

                        string imgname = DateTime.Now.ToString("yyyy-MM-ddhhmmss") + rd.Next(0, 100) + file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                        string tryimgPath = ConfigurationManager.AppSettings["appName"].ToString() + "UpLoad/WX/";

                        if (!Directory.Exists(context.Server.MapPath(tryimgPath)))
                        {
                            Directory.CreateDirectory(context.Server.MapPath(tryimgPath));
                        }
                        string imgPath = tryimgPath + imgname;
                        string absolutePath = context.Server.MapPath(imgPath);
                        file.SaveAs(absolutePath);
                        BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();
                        Model.System.sys_Attachment model = new Model.System.sys_Attachment();
                        Model.System.sys_LoginUser LoginUser = BaseWeb.GetLoginInfo();
                        model.FileName = imgname;
                        model.Source = 1;
                        model.FilePath = imgPath;
                        model.FileUse = "微信端";
                        model.UseId = LoginUser.ID;
                        model.OperaName = LoginUser.PerName;
                        model.OperaTime = DateTime.Now;
                        int AttachmentId = bll.Add(model, out message);
                        IdList += AttachmentId + ",";
                    }
                    IdList = IdList.Substring(0, IdList.Length - 1);
                    context.Response.Write("{\"state\":1,\"msg\":\"上传成功\",\"AttachmentIdList\":\"" + IdList + "\"}");
                }
                catch
                {
                    context.Response.Write("{\"state\":0,\"msg\":\"上传失败\"}");
                }

            }

        }
        private void UploadFile(HttpContext context, string path, string filename)
        {
            try
            {
                string message = "";
                int IdList = 0;
                BLL.System.sys_Attachment bll = new BLL.System.sys_Attachment();
                Model.System.sys_Attachment model = new Model.System.sys_Attachment();
                Model.System.sys_LoginUser LoginUser = BaseWeb.GetLoginInfo();
                model.FileName = filename;
                model.Source = 1;
                model.FilePath = path;
                model.FileUse = "微信端";
                model.UseId = LoginUser.ID;
                model.OperaName = LoginUser.PerName;
                model.OperaTime = DateTime.Now;
                int AttachmentId = bll.Add(model, out message);
                IdList = AttachmentId;
                context.Response.Write("{\"state\":1,\"msg\":\"上传成功\",\"AttachmentId\":\"" + IdList + "\",\"AttachmentPath\":\"" + path + "\"}");
            }
            catch
            {
                context.Response.Write("{\"state\":0,\"msg\":\"上传失败\"}");
            }
        }
        private void GetIntentionList(HttpContext context)
        {
            try
            {
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModelId = RequestHelper.GetString("MachineModelId").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string SearchType = RequestHelper.GetString("SearchType");
                string startNum = RequestHelper.GetString("startNum");
                string pageSize = RequestHelper.GetString("pageSize");
                BLL.Repair.repair_Intention bll = new BLL.Repair.repair_Intention();
                StringBuilder strWhere = new StringBuilder();
                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                if (LoginUserModel.IsAdmin == false)
                {
                    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                }
                //-------------------------------------------------------------------------
                //客户名
                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModelId != "")
                {
                    strWhere.Append(" and a.MachineModelId=" + Utils.StrToInt(MachineModelId, 0));
                }
                //机号
                if (MachineCode != "" && Utils.IsSafeSqlString(MachineCode))
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                switch (SearchType)
                {
                    case "0":
                        strWhere.Append(" and (a.CustTypeId=1 and a.RepairAdress!='现场') and a.ActualEnterDate is null ");
                        break;
                    case "1":
                        strWhere.Append(" and (a.CustTypeId=1 and a.RepairAdress!='现场') and a.ActualEnterDate is not null ");
                        break;
                    case "2":
                        strWhere.Append(" and a.CustTypeId=2 ");
                        break;
                    case "3":
                        strWhere.Append(" and a.CustTypeId=1 ");
                        break;
                    default:
                        break;
                }
                int StartNum = Utils.StrToInt(startNum, 0);
                int PageSize = Utils.StrToInt(pageSize, 10);
                DataSet ds = bll.GetList_remote(strWhere.ToString(),StartNum,PageSize);
                DataTable dt = ds.Tables[0];
                string IntentionListJson = Utils.ToJson(dt);
                context.Response.Write("{\"state\":1,\"msg\":\"维修意向获取成功\",\"IntentionList\":" + IntentionListJson + "}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"state\":0,\"msg\":\"登陆失败\"}");
            }
        }

        private void GetScheduleList(HttpContext context)
        {
            //if (btn != "show")
            //{
            //    context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
            //    return;
            //}
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string AssignmentCode = RequestHelper.GetString("AssignmentCode").Trim();
                //string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModelId = RequestHelper.GetString("MachineModelId").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                //string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();

                StringBuilder strWhere = new StringBuilder();

                //维修意向号
                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and c.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }

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
                if (MachineModelId != "")
                {
                    strWhere.Append(" and c.MachineModelId=" + Utils.StrToInt(MachineModelId, 0));
                }
                //机号
                if (MachineCode != "" && Utils.IsSafeSqlString(MachineCode))
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
                if (ScheduleType != "")
                {
                    if (ScheduleType == "0,1,2")
                    {
                        strWhere.Append(" and (ScheduleType in (" + ScheduleType + ") or ScheduleType is null ) ");
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
                //----------------------------------------------------------------------------------------------------
                string AssignmentId = RequestHelper.GetString("AssignmentId");
                if (AssignmentId != "")
                {
                    GetUserId(context);
                    strWhere.Append(" and b.ID=" + Utils.StrToInt(AssignmentId, 0) + " ");
                }
                //----------------------------------------------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                // if (LoginUserModel.IsAdmin == false) {
                //    strWhere.Append(" and b.MainRepair =" + LoginUserModel.ID);
                //}
                //----------------------------------------------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and b.MainRepair in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //----------------------------------------------------------------------------------------------------
                SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
                DataTable dt = bll.GetScheduleList(strWhere.ToString(), LoginUserModel.ID, LoginUserModel.IsAdmin).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"state\":\"1\",\"msg\":\"数据获取成功！\",\"ScheduleList\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        private void GetScheduleList_search(HttpContext context)
        {
            //if (btn != "show")
            //{
            //    context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
            //    return;
            //}
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string AssignmentCode = RequestHelper.GetString("AssignmentCode").Trim();
                //string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModelId = RequestHelper.GetString("MachineModelId").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                //string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();
                string group = RequestHelper.GetString("group").Trim();
                StringBuilder strWhere = new StringBuilder();

                //维修意向号
                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and c.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }

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
                if (MachineModelId != "")
                {
                    strWhere.Append(" and c.MachineModelId=" + Utils.StrToInt(MachineModelId, 0));
                }
                //机号
                if (MachineCode != "" && Utils.IsSafeSqlString(MachineCode))
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
                if (ScheduleType != "")
                {
                    if (ScheduleType == "0,1,2")
                    {
                        strWhere.Append(" and (ScheduleType in (" + ScheduleType + ") or ScheduleType is null ) ");
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
                //-------------------
                if (group != "")
                {
                    strWhere.Append(" and e.DepId=" + group + " ");
                }
                //----------------------------------------------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                // if (LoginUserModel.IsAdmin == false) {
                //    strWhere.Append(" and b.MainRepair =" + LoginUserModel.ID);
                //}
                //----------------------------------------------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and b.MainRepair in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //----------------------------------------------------------------------------------------------------
                SCZM.BLL.Repair.Repair_Assignment bll = new SCZM.BLL.Repair.Repair_Assignment();
                DataTable dt = bll.GetScheduleList_search(strWhere.ToString(), LoginUserModel.ID, LoginUserModel.IsAdmin).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"state\":\"1\",\"msg\":\"数据获取成功！\",\"ScheduleList\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }

        private void GetLoginInfo(HttpContext context, string WXAccount)
        {
            string WeiXinAccount = WXAccount;
            try
            {
                if (WeiXinAccount != "")
                {
                    //---------------------------------------
                    //WeiXinAccount = "YaoMaoBin";
                    //--------------------------------------
                    BLL.WX.WX_GetLoginInfo bll = new BLL.WX.WX_GetLoginInfo();
                    DataTable dt = bll.GetLoginInfo(WeiXinAccount).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Model.System.sys_LoginUser model = new Model.System.sys_LoginUser();
                        model.Salt = dt.Rows[0]["Salt"].ToString();
                        model.LoginTime = DateTime.Now;
                        model.PerName = dt.Rows[0]["PerName"].ToString();
                        model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                        model.IsAdmin = bool.Parse(dt.Rows[0]["IsAdmin"].ToString());
                        model.Account = dt.Rows[0]["Account"].ToString();
                        model.DepId = int.Parse(dt.Rows[0]["DepId"].ToString());
                        if (dt.Rows[0]["PostId"] != null && dt.Rows[0]["PostId"].ToString() != "")
                        {
                            model.PostId = int.Parse(dt.Rows[0]["PostId"].ToString());
                        }
                        context.Session[Keys.SESSION_LoginUser] = model;
                        context.Session.Timeout = 45;
                        string operaAction = Enums.ActionEnum.Login.ToString();
                        string operaMemo = "微信用户登录";
                        BaseWeb.AddOpera(model, 0, operaAction, operaMemo);

                        Utils.WriteCookie("SCZMLoginSalt", model.Salt);
                        Utils.WriteCookie("SCZMAccount", "微信用户", 43200);
                        Utils.WriteCookie("SCZMUserName", model.PerName);
                        Utils.WriteCookie("SCZMUserId", model.ID.ToString());
                        Utils.WriteCookie("SCZMDepId", model.DepId.ToString());
                        string jsonData = Utils.ToJson(dt);
                        context.Response.Write("{\"state\":1,\"msg\":\"成功登陆\", \"LoginUserData\":" + jsonData + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"state\":0,\"msg\":\"登陆失败\"}");
                    }
                }
                else
                {
                    context.Response.Write("{\"state\":0,\"msg\":\"登陆失败\"}");
                }
            }
            catch
            {

                context.Response.Write("{\"state\":0,\"msg\":\"登陆失败\"}");
            }
        }
        private void GetIntentionDetail(HttpContext context)
        {
            string IntentionId = RequestHelper.GetString("IntentionId");
            try
            {
                if (IntentionId != "")
                {
                    BLL.Repair.repair_Intention bll = new BLL.Repair.repair_Intention();
                    //DataTable dt = bll.GetDetail(Utils.StrToInt(IntentionId, 0)).Tables[0];
                    DataSet ds = bll.GetDetail(Utils.StrToInt(IntentionId, 0));
                    DataTable dt = ds.Tables[0];
                    string IntentionDetail = Utils.ToJson(dt);
                    dt = ds.Tables["attachment"];
                    string attachmentInfo = Utils.ToJson(dt);
                    dt = ds.Tables["Intention_Package"];
                    string Intention_PackageInfo = Utils.ToJson(dt);
                    context.Response.Write("{\"state\":1,\"msg\":\"成功获取维修意向\", \"IntentionDetail\":" + IntentionDetail + ",\"attachmentInfo\":" + attachmentInfo + ",\"Intention_PackageInfo\":" + Intention_PackageInfo + "}");
                }
                else
                {
                    context.Response.Write("{\"state\":0,\"msg\":\"获取失败\"}");
                }
            }
            catch
            {
                context.Response.Write("{\"state\":0,\"msg\":\"获取失败\"}");
            }
        }
        private void GetScheduleDetail(HttpContext context)
        {
            //if (btn != "show")
            //{
            //    context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
            //    return;
            //}
            try
            {
                StringBuilder strWhere = new StringBuilder();
                string AssignmentProcedureId = RequestHelper.GetString("AssignmentProcedureId");
                if (AssignmentProcedureId != "")
                {
                    strWhere.Append(" and a.AssignmentProcedureId=" + Utils.StrToInt(AssignmentProcedureId, 0));
                }
                string ScheduleId = RequestHelper.GetString("ScheduleId");
                if (ScheduleId != "")
                {
                    strWhere.Append(" and a.ID=" + Utils.StrToInt(ScheduleId, 0));
                }
                SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
                DataTable dt = bll.GetList(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"state\":\"1\",\"msg\":\"数据获取成功！\",\"ScheduleDetail\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        private void GetUserId(HttpContext context)
        {
            //---------------------------------------------
            if (BaseWeb.GetLoginInfo() != null && context.Request.Cookies["SCZMUserId"].Value != "")
            {
                if (BaseWeb.GetLoginInfo().ID.ToString() == context.Request.Cookies["SCZMUserId"].Value)
                {
                    context.Response.Write("{\"state\":1,\"msg\":\"成功登陆(非首次)\"}");
                    return;
                }
            }
            //---------------------------------------------
            Model.System.sys_Config model = new BLL.System.sys_Config().loadConfig();
            string corpid = model.WxCorpid;
            string corpsecret = "";
            string name = RequestHelper.GetString("name");
            switch (name)
            {
                case "Intention":
                    corpsecret = model.WxIntentionCorpSecret;
                    break;
                case "Schedule":
                    corpsecret = model.WxScheduleCorpSecret;
                    break;
                case "scheduleSearch":
                    corpsecret = model.WxscheduleSearchCorpSecret;
                    break;
                case "scheduleReward":
                    corpsecret = model.WxscheduleRewardCorpSecret;
                    break;
                default:
                    break;
            }
            //----
            //corpsecret = model.WxScheduleCorpSecret;
            //------
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//TLS12安全层传输协议
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpid + "&corpsecret=" + corpsecret);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string access_tokenData = reader.ReadToEnd();
                    JObject jObj = JObject.Parse(access_tokenData);
                    string access_token = jObj["access_token"].ToString();
                    if (access_token != "")
                    {
                        string code = RequestHelper.GetString("code");
                        req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=" + access_token + "&code=" + code);
                        req.Method = "GET";
                        using (WebResponse wr2 = req.GetResponse())
                        {
                            using (StreamReader reader2 = new StreamReader(wr2.GetResponseStream()))
                            {
                                string UserIdData = reader2.ReadToEnd();
                                JObject jObj2 = JObject.Parse(UserIdData);
                                string UserId = jObj2["UserId"].ToString();
                                GetLoginInfo(context, UserId);
                            }
                        }
                    }
                }
            }
        }
        #region 机型下拉框(微信)


        private void GetMachineModelCombo_WX(HttpContext context)
        {
            string emptyStr = "[{\"MachineModelId\":\"0\",\"MachineModelName\":\"无\"}]";
            try
            {
                BLL.Base.base_MachineModel bll = new BLL.Base.base_MachineModel();
                DataTable dt = bll.GetComboList("").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 维修类型下拉框(微信)


        private void GetRepairTypeCombo_WX(HttpContext context)
        {
            string emptyStr = "[{\"RepairTypeId\":\"0\",\"RepairTypeName\":\"无\"}]";
            //if (btn != "show")
            //{
            //    context.Response.Write(emptyStr);
            //    return;
            //}
            try
            {
                BLL.Base.base_RepairType bll = new BLL.Base.base_RepairType();
                DataTable dt = bll.GetComboList("").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 保存维修意向信息==============================;
        private void SaveIntention(HttpContext context)
        {
            //if (btn != "btnSave")
            //{
            //    context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
            //    return;
            //}

            string ID = RequestHelper.GetString("ID");
            //-------------------------客户信息-----------------------------------------------------
            string IntentionCode = RequestHelper.GetString("IntentionCode");
            string IntentionDate = RequestHelper.GetString("IntentionDate");
            string IntentionType = RequestHelper.GetString("IntentionType");
            string IntentionCode_Last = RequestHelper.GetString("IntentionCode_Last");
            string BusinessDepId = RequestHelper.GetString("BusinessDepId");
            string BusinessPerName = RequestHelper.GetString("BusinessPerName");
            string CustTypeId = RequestHelper.GetString("CustTypeId");
            string CustName = RequestHelper.GetString("CustName");
            string MachineModelId = RequestHelper.GetString("MachineModelId");
            string MachineCode = RequestHelper.GetString("MachineCode");
            string EngineModel = RequestHelper.GetString("EngineModel");
            string EngineCode = RequestHelper.GetString("EngineCode");
            string SMR = RequestHelper.GetString("SMR");
            string FlagFXGCH = RequestHelper.GetString("FlagFXGCH");
            string Linkman = RequestHelper.GetString("Linkman");
            string LinkPhone = RequestHelper.GetString("LinkPhone");
            string MachineAdress = RequestHelper.GetString("MachineAdress");
            string Machine = RequestHelper.GetString("Machine");
            string MachineStatus = RequestHelper.GetString("MachineStatus");
            //----------------------------------初步方案------------------------------------------------------
            string FlagResult = RequestHelper.GetString("FlagResult");
            //-----------------------------------------------------------------------------
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.repair_Intention model = new SCZM.Model.Repair.repair_Intention();
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
            model.ID = Utils.StrToInt(ID, 0);
            //--------------------------客户信息------------------------
            if (IntentionCode == "")
            {
                IntentionCode = "BX" + DateTime.Now.ToString("yyyyMMdd") + bll.GetMaxId();
            }
            model.IntentionCode = IntentionCode;
            if (IntentionDate != "")
            {
                model.IntentionDate = Utils.StrToDateTime(IntentionDate);
            }

            model.IntentionType = Utils.StrToInt(IntentionType, 0);
            model.IntentionCode_Last = IntentionCode_Last;
            //model.BusinessDepId = Utils.StrToInt(BusinessDepId, 0);
            model.BusinessDepId = loginUserModel.DepId;
            //model.BusinessPerName = BusinessPerName;
            model.BusinessPerName = loginUserModel.ID.ToString();
            model.CustTypeId = Utils.StrToInt(CustTypeId, 0);
            model.CustName = CustName;
            model.MachineModelId = Utils.StrToInt(MachineModelId, 0);
            model.MachineCode = MachineCode;
            model.EngineModel = EngineModel;
            model.EngineCode = EngineCode;
            model.SMR = Utils.StrToInt(SMR, 0);
            model.FlagFXGCH = Utils.StrToInt(FlagFXGCH, 0);
            model.Linkman = Linkman;
            model.LinkPhone = LinkPhone;
            model.MachineAdress = MachineAdress;
            model.Machine = Machine;
            model.MachineStatus = MachineStatus;
            //------------------------------------------------------------
            model.FlagResult = Utils.StrToInt(FlagResult, 0);
            if (model.FlagResult != 0)
            {
                //----------------客户意见：1,2------------------------------------------------
                string CustOpinion = RequestHelper.GetString("CustOpinion");
                model.CustOpinion = CustOpinion;
                if (model.FlagResult == 1)
                {
                    //-----------------------1--------------------------------------
                    string RepairMode = RequestHelper.GetString("RepairMode");
                    string FlagLocale = RequestHelper.GetString("FlagLocale");
                    string RepairTypeId = RequestHelper.GetString("RepairTypeId");
                    string RepairContent = RequestHelper.GetString("RepairContent");
                    string FlagENGKC = RequestHelper.GetString("FlagENGKC");
                    string FlagPPMKC = RequestHelper.GetString("FlagPPMKC");
                    string FlagENG = RequestHelper.GetString("FlagENG");
                    string FlagPPM = RequestHelper.GetString("FlagPPM");
                    string FlagMCV = RequestHelper.GetString("FlagMCV");
                    string FlagELE = RequestHelper.GetString("FlagELE");
                    string FlagVM = RequestHelper.GetString("FlagVM");
                    string FlagRM = RequestHelper.GetString("FlagRM");
                    string FlagSM = RequestHelper.GetString("FlagSM");
                    string FlagUM = RequestHelper.GetString("FlagUM");
                    string FlagVR = RequestHelper.GetString("FlagVR");
                    string RepairAdress = RequestHelper.GetString("RepairAdress");
                    string ExpectEnterDate = RequestHelper.GetString("ExpectEnterDate");
                    string ExpectLeaveDate = RequestHelper.GetString("ExpectLeaveDate");
                    string ExpectTimeFee = RequestHelper.GetString("ExpectTimeFee");
                    string ExpectPartFee = RequestHelper.GetString("ExpectPartFee");
                    string ExpectFee = RequestHelper.GetString("ExpectFee");
                    string detailStr = RequestHelper.GetString("detailStr");
                    //----------------------1-----------------------------------
                    model.RepairMode = Utils.StrToInt(RepairMode, 1);
                    model.FlagLocale = Utils.StrToInt(FlagLocale, 0);
                    model.RepairTypeId = Utils.StrToInt(RepairTypeId, 0);
                    model.RepairContent = RepairContent;
                    model.FlagENGKC = Utils.StrToInt(FlagENGKC, 0);
                    model.FlagPPMKC = Utils.StrToInt(FlagPPMKC, 0);
                    model.FlagENG = Utils.StrToInt(FlagENG, 0);
                    model.FlagPPM = Utils.StrToInt(FlagPPM, 0);
                    model.FlagMCV = Utils.StrToInt(FlagMCV, 0);
                    model.FlagELE = Utils.StrToInt(FlagELE, 0);
                    model.FlagVM = Utils.StrToInt(FlagVM, 0);
                    model.FlagRM = Utils.StrToInt(FlagRM, 0);
                    model.FlagSM = Utils.StrToInt(FlagSM, 0);
                    model.FlagUM = Utils.StrToInt(FlagUM, 0);
                    model.FlagVR = Utils.StrToInt(FlagVR, 0);
                    model.RepairAdress = RepairAdress;
                    if (ExpectEnterDate != "")
                    {
                        model.ExpectEnterDate = Utils.StrToDateTime(ExpectEnterDate);
                    }
                    if (ExpectLeaveDate != "")
                    {
                        model.ExpectLeaveDate = Utils.StrToDateTime(ExpectLeaveDate);
                    }
                    model.ExpectTimeFee = Utils.StrToDecimal(ExpectTimeFee, 0);
                    model.ExpectPartFee = Utils.StrToDecimal(ExpectPartFee, 0);
                    model.ExpectFee = Utils.StrToDecimal(ExpectFee, 0);
                    //--------------------------------维修方案-子表-------------------
                    List<Model.Repair.repair_Intention_Package> modelsList = new List<Model.Repair.repair_Intention_Package>();
                    Model.Repair.repair_Intention_Package models = new Model.Repair.repair_Intention_Package();
                    if (detailStr != "")
                    {
                        string[] detailArray = detailStr.Split('≮');

                        for (int i = 0; i < detailArray.Length; i++)
                        {
                            string[] detailMXArray = detailArray[i].Split('⊥');
                            models = new Model.Repair.repair_Intention_Package();
                            models.PackageId = Utils.StrToInt(detailMXArray[0], 0);
                            models.PackageNum = Utils.StrToInt(detailMXArray[1], 0);
                            modelsList.Add(models);
                        }
                    }
                    model.repair_Intention_Packages = modelsList;
                    //-------------------维修协议---------------------------------------

                    //string FlagAgreement = RequestHelper.GetString("FlagAgreement");
                    //model.FlagAgreement = Utils.StrToInt(FlagAgreement, 0);
                    //if (model.FlagAgreement == 1)
                    //{
                    //    //------------------------1----------------------------------------------------
                    //    string AgreementDate = RequestHelper.GetString("AgreementDate");
                    //    string AttachmentId_Agreement = RequestHelper.GetString("AttachmentId_Agreement");
                    //    //------------------------end--------------------------------------------------
                    //    if (AgreementDate != "")
                    //    {
                    //        model.AgreementDate = Utils.StrToDateTime(AgreementDate);
                    //    }
                    //    model.AttachmentId_Agreement = AttachmentId_Agreement;
                    //}
                }
            }
            //------------------10、客户谈判-----------------------------------------
            model.RepairState = 10;
            //现场维修跳过登记入厂 内部客户跳过
            if (model.RepairAdress == "现场" || model.CustTypeId == 2)
            {
                model.RepairState = 20;
            }
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
                        operaMemo = "新增维修意向：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改维修意向：" + model.ID;
                    }
                }
                if (status == "1")
                {
                    //写入操作日志 微信统一 menuid为-1
                    BaseWeb.AddOpera(loginUserModel, -1, operaAction, operaMemo);
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
        private void SaveSchedule(HttpContext context)
        {

            string ScheduleStr = RequestHelper.GetString("ScheduleStr");
            string ScheduleType = RequestHelper.GetString("ScheduleType");
            string ScheduleDate = RequestHelper.GetString("ScheduleDate");
            string AttachmentList_Schedule = RequestHelper.GetString("AttachmentList_Schedule");
            string Memo = RequestHelper.GetString("Memo").Trim();
            string PauseReason = "-1";
            if (ScheduleType == "2")
            {
                PauseReason = RequestHelper.GetString("PauseReason");
            }
            int ScheduleId = Utils.StrToInt(RequestHelper.GetString("ScheduleId"), -1);

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
                model.PauseReason = Utils.StrToInt(PauseReason, -1);
                model.ID = ScheduleId;
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
                SaveData(model, context);
            }
        }
        private void SaveData(Model.Repair.repair_Schedule model, HttpContext context)
        {
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Schedule bll = new SCZM.BLL.Repair.repair_Schedule();
            string operaMessage = "";
            string state = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (model.ID == -1)
                {
                    model.ID = bll.Add(model, out operaMessage);
                    if (model.ID > 0)
                    {
                        state = "1";
                        operaAction = Enums.ActionEnum.Add.ToString();
                        operaMemo = "新增进度反馈：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        state = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改进度反馈：" + model.ID;
                    }
                }
                if (state == "1")
                {
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, -1, operaAction, operaMemo);
                }
                if (state == "1" && model.PauseReason == 5)
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
                else if (state == "1" && model.ScheduleType == 3)
                {
                    SCZM.BLL.Repair.Repair_Assignment bll_Assignment = new BLL.Repair.Repair_Assignment();
                    //model.AssignmentId; 派工ID 
                    //model.AssignmentProcedureId 派工工序ID
                    //---------------------2018/6/26---取消审核（系统内默认审核同意，需手动审核时删掉此段+repair_Schedule.ashx也有同样的设置，需同步修改）-----------------------------
                    Model.Repair.Repair_Assignment_Procedure model_AssignmentProcedure = null;
                    BLL.Repair.repair_Report bll_Report = new BLL.Repair.repair_Report();
                    DataTable dt = bll_Assignment.GetAssignmentProcedureList(" and a.ID=" + model.AssignmentProcedureId + " ").Tables[0];


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


                            DataTable dt_CustMessage = bll_Assignment.GetComboList(" and a.ID=" + model.AssignmentId + " ").Tables[0];
                            SCZM.BLL.Base.base_Procedure bll_Procedure = new BLL.Base.base_Procedure();
                            DataTable dt_Procedure = bll_Procedure.GetComboList(" and a.ID=" + model.ProcedureId + " ").Tables[0];
                            WX_GetLoginInfo.GetMessage(touser, "", "客户：" + dt_CustMessage.Rows[0]["CustName"] + "\n机型：" + dt_CustMessage.Rows[0]["MachineModel"] + " \n机号：" + dt_CustMessage.Rows[0]["MachineCode"] + "\n维修担当：" + dt_CustMessage.Rows[0]["PerName"] + "\n工序：" + dt_Procedure.Rows[0]["ProcedureName"] + "\n维修已完成。" + model.Memo + "\n", "Schedule");
                        }
                    }
                    //------------------------------------------------------------------------------------
                }
                else if (state == "1" && (model.ScheduleType == 1 || model.ScheduleType == 2))
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
                            WX_GetLoginInfo.GetMessage(touser, "", "客户：" + dt_CustMessage.Rows[0]["CustName"] + "\n机型：" + dt_CustMessage.Rows[0]["MachineModel"] + " \n机号：" + dt_CustMessage.Rows[0]["MachineCode"] + "\n维修担当：" + dt_CustMessage.Rows[0]["PerName"] + "\n工序：" + dt_Procedure.Rows[0]["ProcedureName"] + "\n维修" + (model.ScheduleType == 1 ? "已开始" : "已暂停") + "。" + model.Memo + "\n", "Schedule");
                        }
                    }
                }
                if (state == "1")
                {
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
                context.Response.Write("{\"state\":\"" + state + "\",\"msg\":\"" + operaMessage + "\"}"); 
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"state\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }

        public static string sha1(string content)
        {
            return sha1(content, Encoding.UTF8);
        }
        public static string sha1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
        private void GetSignature(HttpContext context)
        {
            //---------------------------------------------
            //---------------------------------------------
            Model.System.sys_Config model = new BLL.System.sys_Config().loadConfig();
            string corpid = model.WxCorpid;
            string corpsecret = "";
            corpsecret = model.WxIntentionCorpSecret;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpid + "&corpsecret=" + corpsecret);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string access_tokenData = reader.ReadToEnd();
                    JObject jObj = JObject.Parse(access_tokenData);
                    string access_token = jObj["access_token"].ToString();
                    if (access_token != "")
                    {
                        req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token=" + access_token);
                        req.Method = "GET";
                        using (WebResponse wr2 = req.GetResponse())
                        {
                            using (StreamReader read2 = new StreamReader(wr2.GetResponseStream()))
                            {
                                string ticketData = read2.ReadToEnd();
                                JObject jObj2 = JObject.Parse(ticketData);
                                string ticket = jObj2["ticket"].ToString();
                                //------------------------------------------
                                string noncestr = "zhuanghaoxin";
                                long timestamp = GetTimeStamp();
                                //string url = "http://1e9a741164.imwork.net/SCZM/Pages/WeiXin/WX_SDK.html";
                                string url = RequestHelper.GetString("url");
                                string string1 = "jsapi_ticket=" + ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
                                string signature = sha1(string1);
                                JObject jObj3 = new JObject();
                                jObj3.Add("noncestr", noncestr);
                                jObj3.Add("timestamp", timestamp);
                                jObj3.Add("signature", signature);
                                jObj3.Add("corpid", corpid);
                                String jsonStr = jObj3.ToString();
                                context.Response.Write("{\"state\":1,\"msg\":\"ok\",\"configData\":" + jsonStr + "}");
                                //------------------------------------------
                            }
                        }

                    }
                }
            }
        }
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        private void getTemporaryMaterial(HttpContext context)
        {
            Model.System.sys_Config model = new BLL.System.sys_Config().loadConfig();
            string corpid = model.WxCorpid;
            string corpsecret = "";
            corpsecret = model.WxIntentionCorpSecret;
            string serverId = RequestHelper.GetString("serverId");
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpid + "&corpsecret=" + corpsecret);
            req.Method = "get";
            string path = "";
            string fileName = "";
            using (WebResponse wr = req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string access_tokenData = reader.ReadToEnd();
                    JObject jObj = JObject.Parse(access_tokenData);
                    string access_token = jObj["access_token"].ToString();
                    //reader.Close();
                    //wr.Close();
                    if (access_token != "" && serverId != "")
                    {
                        req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=" + access_token + "&media_id=" + serverId);

                        try
                        {
                            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                            {

                                if (response.StatusCode == HttpStatusCode.OK)
                                {

                                    fileName = response.Headers["Content-disposition"];
                                    fileName = fileName.Substring(22, fileName.Length - 23);
                                    string tryimgPath = ConfigurationManager.AppSettings["appName"].ToString() + "UpLoad/WX/";
                                    if (!Directory.Exists(context.Server.MapPath(tryimgPath)))
                                    {
                                        Directory.CreateDirectory(context.Server.MapPath(tryimgPath));
                                    }

                                    path = tryimgPath + fileName;
                                    Stream responseStream = response.GetResponseStream();
                                    BinaryReader br = new BinaryReader(responseStream);

                                    //------------------------------------------------------
                                    FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(path), FileMode.Create, FileAccess.Write);
                                    const int buffsize = 1024;
                                    byte[] bytes = new byte[buffsize];
                                    int totalread = 0;
                                    int numread = buffsize;
                                    while (numread != 0)
                                    {
                                        numread = br.Read(bytes, 0, buffsize);
                                        totalread += numread;

                                        fs.Write(bytes, 0, numread);
                                    }
                                    br.Close();
                                    fs.Close();

                                    //response.Close();
                                    string isThumbnail = RequestHelper.GetString("isThumbnail").Trim();
                                    if (isThumbnail == "1")
                                    {
                                        string tryimgPath_Thumbnail = ConfigurationManager.AppSettings["appName"].ToString() + "UpLoad/WX_Thumbnail/";
                                        if (!Directory.Exists(context.Server.MapPath(tryimgPath_Thumbnail)))
                                        {
                                            Directory.CreateDirectory(context.Server.MapPath(tryimgPath_Thumbnail));
                                        }
                                        string path_Thumbnail = tryimgPath_Thumbnail + fileName;
                                        Thumbnail.MakeThumbnailImage(HttpContext.Current.Server.MapPath(path), HttpContext.Current.Server.MapPath(path_Thumbnail), 100, 100, "Cut");
                                    }
                                    UploadFile(context, path, fileName);
                                }
                                else
                                {
                                    //response.Close();
                                    path = "";
                                    context.Response.Write("{\"state\":0,\"msg\":\"上传失败\"}");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            path = "";
                            context.Response.Write("{\"state\":0,\"msg\":\"上传失败\"}");
                        }
                    }
                }
            }

            //context.Response.Write("{\"state\":0,\"msg\":\"上传失败\"}");
            //context.Response.Write("{\"path\":\"" + path + "\"}");
        }
        private void getTemporaryMaterial_change(HttpContext context)
        {
            string path = "";
            string fileName = "";
            try
            {
                string[] base64Data = context.Request.Form[context.Request.QueryString["name"]].Split(new char[3] { ':', ';', ',' });
                fileName = context.Request.QueryString["name"] + "." + base64Data[1].Split('/')[1];
                string tryimgPath = ConfigurationManager.AppSettings["appName"].ToString() + "UpLoad/WX/";

                if (!Directory.Exists(context.Server.MapPath(tryimgPath)))
                {
                    Directory.CreateDirectory(context.Server.MapPath(tryimgPath));
                }
                path = tryimgPath + fileName;
                byte[] bt = Convert.FromBase64String(base64Data[3]);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(path), bt);
                //MemoryStream stream = new MemoryStream(bt);
                //Bitmap bitmap = new Bitmap(stream);
                //bitmap.Save(HttpContext.Current.Server.MapPath(path));
                UploadFile(context, path, fileName);
            }
            catch (Exception)
            {
                throw;
            }


        }
        //message
        private void GetMessage(HttpContext context)
        {
            Model.System.sys_Config model = new BLL.System.sys_Config().loadConfig();
            string corpid = model.WxCorpid;
            string corpsecret = "";
            corpsecret = model.WxScheduleCorpSecret;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpid + "&corpsecret=" + corpsecret);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string access_tokenData = reader.ReadToEnd();
                    JObject jObj = JObject.Parse(access_tokenData);
                    string access_token = jObj["access_token"].ToString();
                    if (access_token != "")
                    {
                        req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + access_token);
                        req.Method = "POST";
                        req.ContentType = "application/json";
                        string message = "你的快递已到，请携带工卡前往邮件中心领取。\n出发前可查看<a href='http://work.weixin.qq.com'>邮件中心视频实况</a>，聪明避开排队。";
                        int agentid = 1000004;
                        string touser = "@all";
                        string strContent = "{\"touser\":\"" + touser + "\",\"msgtype\" : \"text\",\"agentid\" :" + agentid + ",\"text\":{\"content\" : \"" + message + "\"},\"safe\":0}";
                        using (StreamWriter dataStream = new StreamWriter(req.GetRequestStream()))
                        {
                            dataStream.Write(strContent);
                            //dataStream.Close();
                        }

                        using (WebResponse wr2 = req.GetResponse())
                        {
                            using (StreamReader read2 = new StreamReader(wr2.GetResponseStream()))
                            {
                                string ticketData = read2.ReadToEnd();
                                JObject jObj2 = JObject.Parse(ticketData);
                                string errmsg = jObj2["errmsg"].ToString();
                                //------------------------------------------
                                //string noncestr = "zhuanghaoxin";
                                //long timestamp = GetTimeStamp();
                                ////string url = "http://1e9a741164.imwork.net/SCZM/Pages/WeiXin/WX_SDK.html";
                                //string url = RequestHelper.GetString("url");
                                //string string1 = "jsapi_ticket=" + ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
                                //string signature = sha1(string1);
                                //JObject jObj3 = new JObject();
                                //jObj3.Add("noncestr", noncestr);
                                //jObj3.Add("timestamp", timestamp);
                                //jObj3.Add("signature", signature);
                                //jObj3.Add("corpid", corpid);
                                //String jsonStr = jObj3.ToString();
                                //context.Response.Write("{\"state\":1,\"msg\":\"ok\",\"configData\":" + jsonStr + "}");
                                //------------------------------------------
                            }
                        }

                    }
                }
            }
        }
        public static int GetAgentid(string ProjectName)
        {
            Model.System.sys_Config model = new BLL.System.sys_Config().loadConfig();
            string corpid = model.WxCorpid;
            string corpsecret = "";
            switch (ProjectName)
            {
                case "Intention":
                    corpsecret = model.WxIntentionCorpSecret;
                    break;
                case "Schedule":
                    corpsecret = model.WxScheduleCorpSecret;
                    break;
                case "scheduleSearch":
                    corpsecret = model.WxscheduleSearchCorpSecret;
                    break;
                default:
                    break;
            }
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpid + "&corpsecret=" + corpsecret);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                {
                    string access_tokenData = reader.ReadToEnd();
                    JObject jObj = JObject.Parse(access_tokenData);
                    string access_token = jObj["access_token"].ToString();
                    if (access_token != "")
                    {
                        req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token=" + access_token);
                        req.Method = "GET";
                        using (WebResponse wr3 = req.GetResponse())
                        {
                            using (StreamReader read3 = new StreamReader(wr3.GetResponseStream()))
                            {
                                string agentListData = read3.ReadToEnd();
                                JObject jObj3 = JObject.Parse(agentListData);
                                string agentidStr = jObj3["agentlist"][0]["agentid"].ToString();
                                if (agentidStr != "")
                                {
                                    int agentid = int.Parse(agentidStr);
                                    return agentid;
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }
        public static void GetMessage(string touser, string toparty, string message, string name)
        {
            Model.System.sys_Config model = new BLL.System.sys_Config().loadConfig();
            string corpid = model.WxCorpid;
            string corpsecret = "";
            switch (name)
            {
                case "Intention":
                    corpsecret = model.WxIntentionCorpSecret;
                    break;
                case "Schedule":
                    corpsecret = model.WxScheduleCorpSecret;
                    break;
                case "scheduleSearch":
                    corpsecret = model.WxscheduleSearchCorpSecret;
                    break;
                default:
                    break;
            }
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpid + "&corpsecret=" + corpsecret);
                req.Method = "GET";
                using (WebResponse wr = req.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                    {
                        string access_tokenData = reader.ReadToEnd();
                        JObject jObj = JObject.Parse(access_tokenData);
                        string access_token = jObj["access_token"].ToString();

                        if (access_token != "")
                        {
                            req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token=" + access_token);
                            req.Method = "GET";
                            using (WebResponse wr3 = req.GetResponse())
                            {
                                using (StreamReader read3 = new StreamReader(wr3.GetResponseStream()))
                                {
                                    string agentListData = read3.ReadToEnd();
                                    JObject jObj3 = JObject.Parse(agentListData);
                                    string agentidStr = jObj3["agentlist"][0]["agentid"].ToString();
                                    //read3.Close();
                                    //wr3.Close();
                                    if (agentidStr != "")
                                    {
                                        int agentid = int.Parse(agentidStr);
                                        //--------------------------------------------------------------------
                                        req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + access_token);
                                        req.Method = "POST";
                                        req.ContentType = "application/json";

                                        //string message = "你的快递已到，请携带工卡前往邮件中心领取。\n出发前可查看<a href='http://work.weixin.qq.com'>邮件中心视频实况</a>，聪明避开排队。";
                                        //int agentid = 1000004;
                                        //string touser = "@all";
                                        string strContent = "{\"touser\":\"" + touser + "\",\"toparty\":\"" + toparty + "\",\"msgtype\" : \"text\",\"agentid\" :" + agentid + ",\"text\":{\"content\" : \"" + message + "\"},\"safe\":0}";
                                        using (StreamWriter dataStream = new StreamWriter(req.GetRequestStream()))
                                        {
                                            dataStream.Write(strContent);
                                            //dataStream.Close();
                                        }

                                        using (WebResponse wr2 = req.GetResponse())
                                        {
                                            using (StreamReader read2 = new StreamReader(wr2.GetResponseStream()))
                                            {
                                                string ticketData = read2.ReadToEnd();
                                                JObject jObj2 = JObject.Parse(ticketData);
                                                string errmsg = jObj2["errmsg"].ToString();
                                                //read2.Close();
                                            }
                                            //wr2.Close();
                                        }
                                    }
                                }
                            }



                        }
                        //reader.Close();
                    }
                    //wr.Close();
                }
            }
            catch (Exception e)
            {

            }
        }
        public static void GetMessageCard(string touser, string toparty, string title, string description, string url, string btntxt, string name)
        {
            Model.System.sys_Config model = new BLL.System.sys_Config().loadConfig();
            string corpid = model.WxCorpid;
            string corpsecret = "";
            switch (name)
            {
                case "Intention":
                    corpsecret = model.WxIntentionCorpSecret;
                    break;
                case "Schedule":
                    corpsecret = model.WxScheduleCorpSecret;
                    break;
                case "scheduleSearch":
                    corpsecret = model.WxscheduleSearchCorpSecret;
                    break;
                default:
                    break;
            }
            //corpsecret = model.WxScheduleCorpSecret;
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpid + "&corpsecret=" + corpsecret);
                req.Method = "GET";
                using (WebResponse wr = req.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                    {
                        string access_tokenData = reader.ReadToEnd();
                        JObject jObj = JObject.Parse(access_tokenData);
                        string access_token = jObj["access_token"].ToString();
                        if (access_token != "")
                        {
                            req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token=" + access_token);
                            req.Method = "GET";
                            using (WebResponse wr3 = req.GetResponse())
                            {
                                using (StreamReader read3 = new StreamReader(wr3.GetResponseStream()))
                                {
                                    string agentListData = read3.ReadToEnd();
                                    JObject jObj3 = JObject.Parse(agentListData);
                                    string agentidStr = jObj3["agentlist"][0]["agentid"].ToString();
                                    //read3.Close();
                                    //wr3.Close();
                                    if (agentidStr != "")
                                    {
                                        int agentid = int.Parse(agentidStr);

                                        req = (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + access_token);
                                        req.Method = "POST";
                                        req.ContentType = "application/json";
                                        //string message = "你的快递已到，请携带工卡前往邮件中心领取。\n出发前可查看<a href='http://work.weixin.qq.com'>邮件中心视频实况</a>，聪明避开排队。";<div class=\"gray\">"+DateTime.Now.ToLongDateString()+"</div><div class=\"normal\">"+description+"</div>
                                        //int agentid = 1000004;
                                        //string touser = "@all";
                                        string strContent = "{\"touser\":\"" + touser + "\",\"toparty\":\"" + toparty + "\",\"msgtype\" : \"textcard\",\"agentid\" :" + agentid + ",\"textcard\":{\"title\" : \"" + title + "\",\"description\":\"<div class='gray'>" + DateTime.Now.ToLongDateString() + "</div><div class='normal'>" + description + "</div>\",\"url\":\"" + url + "\",\"btntxt\":\"" + btntxt + "\"}}";
                                        using (StreamWriter dataStream = new StreamWriter(req.GetRequestStream()))
                                        {
                                            dataStream.Write(strContent);
                                            //dataStream.Close();
                                        }

                                        using (WebResponse wr2 = req.GetResponse())
                                        {
                                            using (StreamReader read2 = new StreamReader(wr2.GetResponseStream()))
                                            {
                                                string ticketData = read2.ReadToEnd();
                                                JObject jObj2 = JObject.Parse(ticketData);
                                                string errmsg = jObj2["errmsg"].ToString();
                                                //read2.Close();
                                            }
                                            //wr2.Close();
                                        }
                                    }
                                }
                            }
                            //reader.Close();
                        }
                        //wr.Close();
                    }
                }
            }
            catch (Exception e)
            {
             
            }
        }
        private void GetRepairReward(HttpContext context) {
            string assignmentProcedureId = context.Request.Form["assignmentProcedureId"];
            string person = context.Request.Form["person"];
            string ScheduleDate_Start = context.Request.Form["ScheduleDate_Start"];
            string ScheduleDate_End = context.Request.Form["ScheduleDate_End"];
            string type = context.Request.Form["type"];
            BLL.Repair.repair_Report bll_Report = new BLL.Repair.repair_Report();
            BLL.WX.WX_GetLoginInfo bll = new BLL.WX.WX_GetLoginInfo();
            Model.System.sys_LoginUser loginUser = BaseWeb.GetLoginInfo();
            //string strWhere = " and g.ID in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + loginUser.ID + ")";
            StringBuilder strWhere = new StringBuilder();
            string timepart = "[]";
            if (person == "manager")
            {
                if (assignmentProcedureId != "")
                {
                    strWhere.Append(" and d.ID=" + Utils.StrToInt(assignmentProcedureId, 0) + " ");
                }
            }
            else if (person == "repairer")
            {
                strWhere.Append(" and g.ID =" + loginUser.ID + " ");
                
                if (type != "")
                {
                    DataSet ds = bll.getTimePart(type);
                    if (ds != null)
                    {
                        timepart = Utils.ToJson(ds.Tables[0]);
                    }
                    if (type == "w") {
                        strWhere.Append(" and datepart(week,k.ScheduleDate)=datepart(week,getdate()) ");
                    }
                    else if (type == "m")
                    {
                        strWhere.Append(" and datepart(month,k.ScheduleDate)=datepart(month,getdate()) ");
                    }
                    else {
                        strWhere.Append(" and 1=2 ");
                    }
                }
                else {
                    ScheduleDate_Start = ScheduleDate_Start.Trim();
                    ScheduleDate_End = ScheduleDate_End.Trim();
                    if (ScheduleDate_Start != "" && ScheduleDate_Start != null)
                    {
                        string[] DStart = ScheduleDate_Start.Split('-');
                        strWhere.Append(" and (datepart(year,k.ScheduleDate)=" + DStart[0] + " and datepart(month,k.ScheduleDate)>=" + DStart[1] + " or datepart(year,k.ScheduleDate)>" + DStart[0] + ") ");
                    }
                    if (ScheduleDate_End != "" && ScheduleDate_End != null)
                    {
                        string[] DEnd = ScheduleDate_End.Split('-');
                        strWhere.Append(" and (datepart(year,k.ScheduleDate)=" + DEnd[0] + " and datepart(month,k.ScheduleDate)<=" + DEnd[1] + " or datepart(year,k.ScheduleDate)<" + DEnd[0] + ") ");
                    }
                }
            }
            else {
                strWhere.Append(" and 1=2 ");
            }
            DataTable dt=bll_Report.GetRepairerRewardDetail(strWhere.ToString()).Tables[0];
            string rowStr = Utils.ToJson(dt);
            context.Response.Write("{\"status\":\"1\",\"msg\":\"数据获取成功!\",\"info\":" + rowStr + ",\"timepart\":"+timepart+"}");
        }
        private void GetRepairReward_Manager(HttpContext context)
        {
            string ScheduleDate_Start = context.Request.Form["ScheduleDate_Start"];
            string ScheduleDate_End = context.Request.Form["ScheduleDate_End"];
            string type = context.Request.Form["type"];
            string time = context.Request.Form["time"];
            BLL.Repair.repair_Report bll_Report = new BLL.Repair.repair_Report();
            Model.System.sys_LoginUser loginUser = BaseWeb.GetLoginInfo();
            BLL.WX.WX_GetLoginInfo bll = new BLL.WX.WX_GetLoginInfo();
            //string strWhere = " and g.ID in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + loginUser.ID + ")";
            //string strWhere = " and g.ID =" + loginUser.ID + " ";
            StringBuilder strWhere = new StringBuilder();
            string timepart = "[]";
            if (type == "procedure")
            {
                if (ScheduleDate_Start != "" && ScheduleDate_Start != null)
                {
                    strWhere.Append(" and k.ScheduleDate>'" + Utils.StrToDateTime(ScheduleDate_Start) + "' ");
                }
                if (ScheduleDate_End != "" && ScheduleDate_End != null)
                {
                    strWhere.Append(" and k.ScheduleDate<'" + Utils.StrToDateTime(ScheduleDate_End + " 23:59:59") + "' ");
                }
                DataTable dt = bll_Report.GetRepairerReward_Audit(strWhere.ToString()).Tables[0];
                string rowStr = Utils.ToJson(dt);
                context.Response.Write("{\"status\":\"1\",\"msg\":\"数据获取成功!\",\"info\":" + rowStr + "}");
            }
            else if (type == "person") {
                if (time != "")
                {
                    DataSet ds = bll.getTimePart(time);
                    if (ds != null)
                    {
                        timepart = Utils.ToJson(ds.Tables[0]);
                    }
                    if (time == "w")
                    {
                        strWhere.Append(" and datepart(week,k.ScheduleDate)=datepart(week,getdate()) ");
                    }
                    else if (time == "m")
                    {
                        strWhere.Append(" and datepart(month,k.ScheduleDate)=datepart(month,getdate()) ");
                    }
                    else
                    {
                        strWhere.Append(" and 1=2 ");
                    }
                }
                else
                {
                    //if (ScheduleDate_Start != "" && ScheduleDate_Start != null)
                    //{
                    //    strWhere.Append(" and k.ScheduleDate>'" + Utils.StrToDateTime(ScheduleDate_Start) + "' ");
                    //}
                    //if (ScheduleDate_End != "" && ScheduleDate_End != null)
                    //{
                    //    strWhere.Append(" and k.ScheduleDate<'" + Utils.StrToDateTime(ScheduleDate_End + " 23:59:59") + "' ");
                    //}
                    ScheduleDate_Start = ScheduleDate_Start.Trim();
                    ScheduleDate_End = ScheduleDate_End.Trim();
                    if (ScheduleDate_Start != "" && ScheduleDate_Start != null)
                    {
                        string[] DStart = ScheduleDate_Start.Split('-');
                        strWhere.Append(" and (datepart(year,k.ScheduleDate)=" + DStart[0] + " and datepart(month,k.ScheduleDate)>=" + DStart[1] + " or datepart(year,k.ScheduleDate)>" + DStart[0] + ") ");
                    }
                    if (ScheduleDate_End != "" && ScheduleDate_End != null)
                    {
                        string[] DEnd = ScheduleDate_End.Split('-');
                        strWhere.Append(" and (datepart(year,k.ScheduleDate)=" + DEnd[0] + " and datepart(month,k.ScheduleDate)<=" + DEnd[1] + " or datepart(year,k.ScheduleDate)<" + DEnd[0] + ") ");
                    }
                }
                DataTable dt=bll_Report.GetRepairerReward(strWhere.ToString()).Tables[0];
                string rowStr = Utils.ToJson(dt);
                context.Response.Write("{\"status\":\"1\",\"msg\":\"数据获取成功!\",\"info\":" + rowStr + ",\"timepart\":" + timepart + "}");
            }
        }
        private void GetRepairReward_RepairerDetail(HttpContext context) {
            string PerId = context.Request.Form["PerId"];
            string ScheduleDate_Start = context.Request.Form["ScheduleDate_Start"];
            string ScheduleDate_End = context.Request.Form["ScheduleDate_End"];
            StringBuilder strWhere = new StringBuilder();
            //人员ID
            if (PerId != "")
            {
                strWhere.Append(" and g.ID=" + Utils.StrToInt(PerId, -1) + " ");
            }
            else {
                strWhere.Append(" and 1=2 ");
            }
            if (ScheduleDate_Start != "" && ScheduleDate_Start != null)
            {
                strWhere.Append(" and k.ScheduleDate>'" + Utils.StrToDateTime(ScheduleDate_Start) + "' ");
            }
            if (ScheduleDate_End != "" && ScheduleDate_End != null)
            {
                strWhere.Append(" and k.ScheduleDate<'" + Utils.StrToDateTime(ScheduleDate_End + " 23:59:59") + "' ");
            }
            //-----------------------------------------------------------------------------------
            SCZM.BLL.Repair.repair_Report bll = new SCZM.BLL.Repair.repair_Report();
            DataTable dt = bll.GetRepairerRewardDetail(strWhere.ToString()).Tables[0];
            string rowsStr = Utils.ToJson(dt);
            StringBuilder jsonStr = new StringBuilder();
            jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
            context.Response.Write(jsonStr);
        }
        public static string GetDateTime(int days,int hours,int minutes,int seconds) {
            TimeSpan ts = new TimeSpan(days, hours, minutes, seconds);
            if (ts.Days != 0) {
                return ts.Days + "天 " + ts.Hours + "时 " + ts.Minutes + "分 " + ts.Seconds + "秒 ";
            }
            else if (ts.Hours != 0)
            {
                return ts.Hours + "时 " + ts.Minutes + "分 " + ts.Seconds + "秒 ";
            }
            else if(ts.Minutes!=0){
                return ts.Minutes + "分 " + ts.Seconds + "秒 ";
            }
            else if (ts.Seconds != 0)
            {
                return ts.Seconds + "秒 ";
            }
            else {
                return "0秒";
            }
        }
        public void getMenu(HttpContext context) {
            BLL.WX.WX_GetLoginInfo bll = new BLL.WX.WX_GetLoginInfo();
            DataSet ds=bll.getMenu();
            if (ds != null && ds.Tables.Count > 0)
            {
                string jsonData = Utils.ToJson(ds.Tables[0]);
                context.Response.Write(jsonData);
            }
            else {
                context.Response.Write("[]");
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

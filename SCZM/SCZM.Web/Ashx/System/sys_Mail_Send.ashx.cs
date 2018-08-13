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

namespace SCZM.Web.Ashx.System
{
    /// <summary>
    /// sys_Mail_Send 的摘要说明


    /// </summary>
    public class sys_Mail_Send : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取发件箱邮件列表


                    GetList(context, btn);
                    break;
                case "GetReceiveList": //获取收件箱邮件列表


                    GetReceiveList(context, btn);
                    break;
               
                case "GetData": //获取邮件信息
                    GetData(context, btn);
                    break;
                case "SaveData": //保存邮件信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除邮件信息
                    DelData(context, btn);
                    break;
                case "DelReceiveData"://删除收件箱邮件列表
                    DelReceiveData(context, btn);
                    break;
            }
        }
        #region 获取发件箱邮件列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.System.sys_Mail_Send bll = new BLL.System.sys_Mail_Send();
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                string strWhere = " and a.OperaId=" + loginUserModel.ID.ToString();

                DataTable dt = bll.GetList(strWhere).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append("{\"total\":" + dt.Rows.Count + ",\"rows\":");
                jsonStr.Append(rowsStr);
                jsonStr.Append("}}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 获取收件箱邮件列表==============================
        private void GetReceiveList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.System.sys_Mail_Send bll = new BLL.System.sys_Mail_Send();
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                string strWhere = " and a.PerId=" + loginUserModel.ID.ToString();

                DataTable dt = bll.GetReceiveList(strWhere).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append("{\"total\":" + dt.Rows.Count + ",\"rows\":");
                jsonStr.Append(rowsStr);
                jsonStr.Append("}}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 获取邮件信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int mailId = RequestHelper.GetInt("id", 0);
                int receiveId = RequestHelper.GetInt("receiveId", 0);
                BLL.System.sys_Mail_Send bll = new BLL.System.sys_Mail_Send();


                DataTable dt = bll.GetList(mailId).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                //如果收件箱ID大于0，表示来源于收件箱页面，则更新收件箱已读标志
                if (receiveId >0)
                {
                    bll.UpdateFlagRead(receiveId.ToString(), 1);
                }

                string rowsStr = Utils.ToJson(dt);

                string attachStr = "";
                string attachId = Utils.ObjectToStr(dt.Rows[0]["Attachment"]);
                if (attachId != "" && attachId != "0")
                {
                    DataTable attachDT = new BLL.System.sys_Attachment().GetList("ID in(" + attachId + ")").Tables[0];
                    attachStr = Utils.ToJson(attachDT);
                }

                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append(rowsStr);
                if (attachStr == "")
                {
                    jsonStr.Append(",\"attachinfo\":\"\"");
                }
                else
                {
                    jsonStr.Append(",\"attachinfo\":" + attachStr);
                }
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 保存邮件信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave" && btn != "btnSaveSend")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string submitFlag = RequestHelper.GetString("submitFlag");
            string id = RequestHelper.GetString("id");
            string MailName = RequestHelper.GetString("MailName");
            string ReceivePerId = RequestHelper.GetString("ReceivePerId");
            string ReceivePerName = RequestHelper.GetString("ReceivePerName");
            string content = RequestHelper.GetString("content");
            string fileId = RequestHelper.GetString("fileId");
            string attachId = RequestHelper.GetString("attachId");

            if (MailName == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"邮件名称不能为空！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            BLL.System.sys_Mail_Send bll = new BLL.System.sys_Mail_Send();
            Model.System.sys_Mail_Send model = new Model.System.sys_Mail_Send();
            model.ID = Utils.StrToInt(id, 0);
            model.MailName = MailName;
            model.ReceivePerId = ReceivePerId;
            model.ReceivePerName = ReceivePerName;
            model.MailContent = content;
            model.Attachment = PageValidate.SafeLongFilter(attachId, 0);
            model.BillState = Utils.StrToInt(submitFlag, 0);
            model.FlagDel = false;
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;

            List<Model.System.sys_Mail_Send_ReceivePerson> modelsList = new List<Model.System.sys_Mail_Send_ReceivePerson>();
            Model.System.sys_Mail_Send_ReceivePerson models = new Model.System.sys_Mail_Send_ReceivePerson();
            string[] depArray = ReceivePerId.Split(',');

            foreach (string perId in depArray)
            {
                models = new Model.System.sys_Mail_Send_ReceivePerson();
                models.ReceivePerId = int.Parse(perId);
                models.FlagDel = false;
                modelsList.Add(models);
            }
            model.sys_Mail_Send_ReceivePersons = modelsList;
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {

                if (id == "")
                {
                    model.ID = bll.Add(model, PageValidate.SafeLongFilter(fileId, 0), out operaMessage);
                    if (model.ID > 0)
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Add.ToString();
                        operaMemo = "新增邮件：" + model.MailName + "(" + model.ID + ")";
                        //写入操作日志
                        BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                    }
                }
                else
                {

                    if (bll.Update(model, PageValidate.SafeLongFilter(fileId, 0), out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改邮件：" + model.MailName + "(" + model.ID + ")";
                        //写入操作日志
                        BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
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
        #region 删除发件箱邮件信息==============================
        private void DelData(HttpContext context, string btn)
        {
            if (btn != "btnDel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string idStr = RequestHelper.GetString("idStr");
            string nameStr = RequestHelper.GetString("nameStr");
            if (idStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要删除的记录！\"}");
                return;
            }
            BLL.System.sys_Mail_Send bll = new BLL.System.sys_Mail_Send();
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {

                if (bll.DeleteList(PageValidate.SafeLongFilter(idStr, 0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除邮件：(" + idStr + ")";
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
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

        #region 删除收件箱邮件信息==============================
        private void DelReceiveData(HttpContext context, string btn)
        {
            if (btn != "btnDel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string idStr = RequestHelper.GetString("idStr");
            string nameStr = RequestHelper.GetString("nameStr");
            if (idStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要删除的记录！\"}");
                return;
            }
            BLL.System.sys_Mail_Send bll = new BLL.System.sys_Mail_Send();
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {

                if (bll.DeleteReceiveList(PageValidate.SafeLongFilter(idStr, 0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除邮件：(" + idStr + ")";
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
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
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
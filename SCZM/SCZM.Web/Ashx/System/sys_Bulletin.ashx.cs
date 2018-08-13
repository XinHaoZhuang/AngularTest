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
    /// sys_Bulletin 的摘要说明


    /// </summary>
    public class sys_Bulletin : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取公告列表
                    GetList(context, btn);
                    break;
                case "GetListByPower": //获取公告列表 根据个人权限
                    GetListByPower(context, btn);
                    break;
                case "GetData": //获取公告信息
                    GetData(context, btn);
                    break;
                case "SaveData": //保存公告信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除公告信息
                    DelData(context, btn);
                    break;
                case "Submit": //发布公告信息
                    Submit(context, btn);
                    break;
                case "SetTop": //置顶公告信息
                    SetTop(context, btn);
                    break;
            }
        }
        #region 获取公告列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.sys_Bulletin bll = new BLL.sys_Bulletin();

                string strWhere = "";

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
        #region 获取公告列表 根据个人权限==============================
        private void GetListByPower(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.sys_Bulletin bll = new BLL.sys_Bulletin();
                //int recordNum = RequestHelper.GetInt("recordNum",0);


                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

                string strWhere = "";

                DataTable dt = bll.GetListByPower(strWhere, 0,loginUserModel.DepId).Tables[0];
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
        #region 获取公告信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int bulletinId = RequestHelper.GetInt("id", 0);
                BLL.sys_Bulletin bll = new BLL.sys_Bulletin();


                DataTable dt = bll.GetList(bulletinId).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);

                string attachStr = "";
                string attachId =Utils.ObjectToStr(dt.Rows[0]["Attachment"]);
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
        #region 保存公告信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave" && btn != "btnSaveAndSubmit")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string submitFlag = RequestHelper.GetString("submitFlag");
            string id = RequestHelper.GetString("id");
            string bulletinName = RequestHelper.GetString("bulletinName");
            string receiveDepId = RequestHelper.GetString("receiveDepId");
            string receiveDepName = RequestHelper.GetString("receiveDepName");
            string flagTop = RequestHelper.GetString("flagTop");
            string content = RequestHelper.GetString("content");
            string fileId = RequestHelper.GetString("fileId");
            string attachId = RequestHelper.GetString("attachId");

            if (bulletinName == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"公告名称不能为空！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            BLL.sys_Bulletin bll = new BLL.sys_Bulletin();
            Model.System.sys_Bulletin model = new Model.System.sys_Bulletin();
            model.ID = Utils.StrToInt(id, 0);
            model.BulletinName = bulletinName;
            model.ReceiveDepId = receiveDepId;
            model.ReceiveDepName = receiveDepName;
            model.BulletinContent = content;
            model.Attachment = PageValidate.SafeLongFilter(attachId, 0);
            model.FlagTop = Utils.StrToInt(flagTop, 0);
            model.BillState = Utils.StrToInt(submitFlag, 0);
            model.FlagDel = false;
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;

            List<Model.System.sys_BulletinReceiveDep> modelsList = new List<Model.System.sys_BulletinReceiveDep>();
            Model.System.sys_BulletinReceiveDep models = new Model.System.sys_BulletinReceiveDep();
            string[] depArray = receiveDepId.Split(',');

            foreach (string depId in depArray)
            {
                models = new Model.System.sys_BulletinReceiveDep();
                models.ReceiveDepId = int.Parse(depId);
                models.FlagDel = false;
                modelsList.Add(models);
            }
            model.sys_BulletinReceiveDeps = modelsList;
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
                        operaMemo = "新增公告：" + model.BulletinName + "(" + model.ID + ")";
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
                        operaMemo = "修改公告：" + model.BulletinName + "(" + model.ID + ")";
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
        #region 删除公告信息==============================
        private void DelData(HttpContext context, string btn)
        {
            if (btn != "btnDel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string idStr = RequestHelper.GetString("idStr");
            //string nameStr = RequestHelper.GetString("nameStr");
            if (idStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要删除的记录！\"}");
                return;
            }
            BLL.sys_Bulletin bll = new BLL.sys_Bulletin();
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
                    operaMemo = "删除公告：(" + idStr + ")";
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
        #region 发布公告信息==============================
        private void Submit(HttpContext context, string btn)
        {
            if (btn != "btnSubmit" && btn != "btnCancelSubmit")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string idStr = RequestHelper.GetString("idStr");
            int submitFlag = RequestHelper.GetInt("submitFlag",0);
            if (idStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要操作的记录！\"}");
                return;
            }
            BLL.sys_Bulletin bll = new BLL.sys_Bulletin();
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {

                if (bll.Submit(PageValidate.SafeLongFilter(idStr, 0),submitFlag,loginUserModel.ID,loginUserModel.PerName, out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = (submitFlag == 0 ? "取消" : "") + "发布公告：(" + idStr + ")";
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
        #region 置顶公告信息==============================
        private void SetTop(HttpContext context, string btn)
        {
            if (btn != "btnSetTop" && btn != "btnCancelSetTop")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string idStr = RequestHelper.GetString("idStr");
            int flagTop = RequestHelper.GetInt("flagTop", 0);
            if (idStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要操作的记录！\"}");
                return;
            }
            BLL.sys_Bulletin bll = new BLL.sys_Bulletin();
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {

                if (bll.SetTop(PageValidate.SafeLongFilter(idStr, 0), flagTop, loginUserModel.ID, loginUserModel.PerName, out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = (flagTop == 0 ? "取消" : "") + "置顶公告：(" + idStr + ")";
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
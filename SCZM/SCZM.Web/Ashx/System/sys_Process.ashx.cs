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
    /// sys_Process 的摘要说明


    /// </summary>
    public class sys_Process : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取审批流列
                    GetList(context, btn);
                    break;
                case "GetData": //获取审批流信息

                    GetData(context, btn);
                    break;
                case "SaveData": //保存审批流信息

                    SaveData(context, btn);
                    break;
                case "DelData": //删除审批流信息

                    DelData(context, btn);
                    break;
            }
        }
        #region 获取审批流列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {

                
                BLL.System.sys_Process bll = new BLL.System.sys_Process();

                
                DataTable dt = bll.GetList("").Tables[0];
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
        #region 获取审批流信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int ProcessId = RequestHelper.GetInt("id", 0);
                BLL.System.sys_Process bll = new BLL.System.sys_Process();

                DataTable dt = bll.GetList(ProcessId).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);

                DataTable detailDT = bll.GetDetailList(ProcessId).Tables[0];
                string detailInfo = Utils.ToJson(detailDT);

                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + ",\"detailInfo\":" + detailInfo + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 保存审批流信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string id = RequestHelper.GetString("id");
            string processName = RequestHelper.GetString("processName");
            string flagUse = RequestHelper.GetString("flagUse");
            string memo = RequestHelper.GetString("memo");
            string post = RequestHelper.GetString("post");

            if (processName == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"审批流名称不能为空！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

            BLL.System.sys_Process bll = new BLL.System.sys_Process();
            Model.System.sys_Process model = new Model.System.sys_Process();

            model.ID = Utils.StrToInt(id, 0);
            model.ProcessName = processName;
            model.FlagUse = Utils.StrToBool(flagUse, true);
            model.Memo = memo;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;


            List<Model.System.sys_ProcessDetail> detailModelList = new List<Model.System.sys_ProcessDetail>();
            Model.System.sys_ProcessDetail detailModel = new Model.System.sys_ProcessDetail();
            string[] postArray = post.Split(',');
            int postId = 0;
            int i = 0;
            foreach (string s in postArray)
            {
                postId = Utils.StrToInt(s, 0);
                if (postId != 0)
                {
                    i++;
                    detailModel = new Model.System.sys_ProcessDetail();
                    detailModel.OrderId = i;
                    detailModel.PostId = postId;
                    detailModelList.Add(detailModel);
                }
            }
            model.sys_ProcessDetails = detailModelList;

            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (id == "")
                {
                    model.ID = bll.Add(model, out operaMessage);
                    if (model.ID > 0)
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Add.ToString();
                        operaMemo = "新增审批流：" + model.ProcessName + "(" + model.ID + ")";
                        //写入操作日志
                        BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改审批流：" + model.ProcessName + "(" + model.ID + ")";
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
        #region 删除审批流信息==============================
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
            BLL.System.sys_Process bll = new BLL.System.sys_Process();

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
                    operaMemo = "删除审批流：" + nameStr + "(" + idStr + ")";
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
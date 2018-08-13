using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    /// sys_Process_BillSet 的摘要说明


    /// </summary>
    public class sys_Process_BillSet : IHttpHandler, IReadOnlySessionState
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

                case "GetList": //获取模块配置列表
                    GetList(context, btn);
                    break;
                case "GetData": //获取模块配置信息
                    GetData(context, btn);
                    break;
                case "SaveData": //保存模块配置信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除模块配置信息
                    DelData(context, btn);
                    break;
                case "GetBillProcess"://获取单据的审批流信息
                    GetBillProcess(context, btn);
                    break;
            }
        }
        #region 获取模块配置列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.System.sys_Process_BillSet bll = new BLL.System.sys_Process_BillSet();
                DataTable dt = bll.GetMxList("").Tables[0];
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append("{\"total\":" + dt.Rows.Count + ",\"rows\":[");
                if (dt.Rows.Count > 0)
                {
                    string FlagHistory = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Memo"].ToString() != "")
                        {
                            if (bool.Parse(dt.Rows[i]["FlagHistory"].ToString()))
                            {
                                FlagHistory = "是";
                            }
                            else
                            {
                                FlagHistory = "否";
                            }
                        }
                        jsonStr.Append("{\"ID\":" + dt.Rows[i]["ID"].ToString() + ",\"BillName\":\"" + dt.Rows[i]["BillName"].ToString() + "\",\"ProcessName\":\"" + dt.Rows[i]["ProcessName"].ToString() + "\",\"Memo\":\"" + dt.Rows[i]["Memo"].ToString() + "\",\"SortId\":\"" + dt.Rows[i]["SortId"].ToString() + "\",\"FlagHistory\":\"" + FlagHistory + "\",\"OperaName\":\"" + dt.Rows[i]["OperaName"].ToString() + "\",\"OperaTime\":\"" + dt.Rows[i]["OperaTime"].ToString() + "\"");
                        if (dt.Rows[i]["SupId"].ToString() != "0")
                        {
                            jsonStr.Append(",\"_parentId\":" + dt.Rows[i]["SupId"].ToString());
                        }
                        jsonStr.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            jsonStr.Append(",");
                        }
                    }
                }
                jsonStr.Append("]}}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 获取模块配置信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int billId = RequestHelper.GetInt("id", 0);
                BLL.System.sys_Process_BillSet bll = new BLL.System.sys_Process_BillSet();
                DataTable dt = bll.GetList(billId).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);

                DataTable pageDT = bll.GetList(billId).Tables[0];


                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append(rowsStr);
                jsonStr.Append(",\"pageInfo\":{\"total\":" + pageDT.Rows.Count + ",\"rows\":" + Utils.ToJson(pageDT) + "}");
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
        #region 保存模块配置信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string id = RequestHelper.GetString("id");
            string supId = RequestHelper.GetString("supId");
            string billName = RequestHelper.GetString("billName");
            string billSign = RequestHelper.GetString("billSign");
            string tableName = RequestHelper.GetString("tableName");
            string sortId = RequestHelper.GetString("sortId"); 
            string flagHistory = RequestHelper.GetString("flagHistory");
            string processId = RequestHelper.GetString("processId");

            if (supId == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"上级模块编码不能为空！\"}");
                return;
            }
            if (billName == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"模块名称不能为空！\"}");
                return;
            }
            if (billSign != "" && Utils.StrToInt(processId,0)==0)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"必须选择审批流！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

            BLL.System.sys_Process_BillSet bll = new BLL.System.sys_Process_BillSet();
            Model.System.sys_Process_BillSet model = new Model.System.sys_Process_BillSet();
            model.ID = Utils.StrToInt(id, 0);
            model.BillName = billName;
            model.BillSign = billSign;
            model.TableName = tableName;
            model.SortId = int.Parse(sortId);
            model.SupId = int.Parse(supId);
            model.ProcessId = Utils.StrToInt(processId,0);
            model.FlagHistory = Utils.StrToBool(flagHistory, false);

            DataTable dt = bll.GetList(int.Parse(supId)).Tables[0];
            
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;

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
                        operaMemo = "新增模块配置：" + model.BillName + "(" + model.ID + ")";
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
                        operaMemo = "修改模块配置：" + model.BillName + "(" + model.ID + ")";
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
        #region 删除模块配置信息==============================
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
            BLL.System.sys_Process_BillSet bll = new BLL.System.sys_Process_BillSet();
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
                    operaMemo = "删除模块配置：" + nameStr + "(" + idStr + ")";
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 获取单据的审批流信息==============================
        private void GetBillProcess(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string billSign = RequestHelper.GetString("billSign");
                int billId = RequestHelper.GetInt("billId", 0);
                if (billSign == "")
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，单据识别号不能为空！\"}");
                    return;
                }
                if (billId == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，单据ID不能为空！\"}");
                    return;
                }
                BLL.System.sys_Process_Exec bll = new BLL.System.sys_Process_Exec();

                //string billStateMemo = bll.GetBillState(billSign, billId);

                DataTable dt = bll.GetBillProcess(billSign, billId).Tables[0];
                //if (dt.Rows.Count == 0)
                //{
                //    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                //    return;
                //}
                string rowsStr = Utils.ToJson(dt);

                DataTable historyDT = bll.GetBillProcessHistory(billSign,billId).Tables[0];
                string historyStr = Utils.ToJson(historyDT);

                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append(rowsStr);
                jsonStr.Append(",\"historyInfo\":");
                jsonStr.Append(historyStr);
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
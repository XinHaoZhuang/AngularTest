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
namespace SCZM.Web.Ashx.Base
{
    /// <summary>
    /// base_PersonAssess 的摘要说明
    /// <summary>
    public class base_PersonAssess : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取人员考核系数列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取人员考核系数明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存人员考核系数信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除人员考核系数信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取人员考核系数列表==============================;
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
                string SupId = RequestHelper.GetString("SupId").Trim();
                if (SupId != "")
                {
                    strWhere.Append(" and a.DepId=" + SupId + " ");
                }
                else
                {
                    context.Response.Write("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":[]}");
                    return;
                }
                SCZM.BLL.Base.base_PersonAssess bll = new SCZM.BLL.Base.base_PersonAssess();
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

        #region 获取人员考核系数明细==============================;
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

                SCZM.BLL.Base.base_PersonAssess bll = new SCZM.BLL.Base.base_PersonAssess();
                DataSet ds = bll.GetDetail(ID);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                DataSet ds_history = bll.GetHistory(int.Parse(dt.Rows[0]["ID"].ToString()), int.Parse(dt.Rows[0]["PersonId"].ToString()));
                DataTable dt_history = ds_history.Tables[0];
                string rowsStr = Utils.ToJson(dt);
                string rowsStr_history = Utils.ToJson(dt_history);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + ",\"history\":" + rowsStr_history);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 保存人员考核系数信息==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            string PersonId = RequestHelper.GetString("PersonId");
            string Assess = RequestHelper.GetString("Assess");
            string CreateDate = RequestHelper.GetString("CreateDate");

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Base.base_PersonAssess model = new SCZM.Model.Base.base_PersonAssess();
            SCZM.BLL.Base.base_PersonAssess bll = new SCZM.BLL.Base.base_PersonAssess();
            model.ID = Utils.StrToInt(ID, 0);
            model.PersonId = Utils.StrToInt(PersonId, 0);
            model.Assess = Utils.StrToDecimal(Assess, 0);
            if (CreateDate != "")
            {
                model.CreateDate = Utils.StrToDateTime(CreateDate);
            }
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;

            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                //if (ID == "")
                //{
                //    model.ID = bll.Add(model, out operaMessage);
                //    if (model.ID > 0)
                //    {
                //        status = "1";
                //        operaAction = Enums.ActionEnum.Add.ToString();
                //        operaMemo = "新增人员考核系数：" + model.ID;
                //        }
                //    }
                //    else
                //    {
                //    if (bll.Update(model, out operaMessage))
                //    {
                //        status = "1";
                //        operaAction = Enums.ActionEnum.Edit.ToString();
                //        operaMemo = "修改人员考核系数：" + model.ID;
                //    }
                //}
                model.ID = bll.Add(model, out operaMessage);
                if (model.ID > 0)
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Add.ToString();
                    operaMemo = "新增人员考核系数：" + model.ID;
                }
                if (status == "1")
                {
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

        #region 删除人员考核系数信息==============================;
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
            SCZM.BLL.Base.base_PersonAssess bll = new SCZM.BLL.Base.base_PersonAssess();
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
                    operaMemo = "删除人员考核系数：" + IDStr;
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

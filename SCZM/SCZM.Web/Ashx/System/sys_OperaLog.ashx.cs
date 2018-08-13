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
    /// sys_OperaLog 的摘要说明


    /// </summary>
    public class sys_OperaLog : IHttpHandler, IReadOnlySessionState
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

                case "GetList": //获取操作日志列表
                    GetList(context, btn);
                    break;
                
                case "DelData": //删除操作日志信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取操作日志列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string perName = RequestHelper.GetString("perName");
            string perAccount = RequestHelper.GetString("perAccount");
            string menuId = RequestHelper.GetString("menu");
            string operaType = RequestHelper.GetString("operaType");
            string memo = RequestHelper.GetString("memo");
            string beginDate = RequestHelper.GetString("beginDate");
            string endDate = RequestHelper.GetString("endDate");

            StringBuilder strWhere =new StringBuilder();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            SqlParameter tempParameter = new SqlParameter();

            if (perName != "")
            {
                strWhere.Append("a.PerName like '%' + @perName + '%' and ");
                tempParameter = new SqlParameter("@perName", SqlDbType.NVarChar);
                tempParameter.Value = perName;
                parameterList.Add(tempParameter);
            }
            if (perAccount != "")
            {
                strWhere.Append("a.PerAccount like '%' + @PerAccount + '%' and ");
                tempParameter = new SqlParameter("@PerAccount", SqlDbType.NVarChar);
                tempParameter.Value = perAccount;
                parameterList.Add(tempParameter);
            }
            if (menuId != "")
            {
                strWhere.Append("a.MenuId = @MenuId and ");
                tempParameter = new SqlParameter("@MenuId", SqlDbType.Int);
                tempParameter.Value = Utils.StrToInt(menuId, 0);
                parameterList.Add(tempParameter);
            }
            if (operaType != "")
            {
                strWhere.Append("a.OperaType = @OperaType and ");
                tempParameter = new SqlParameter("@OperaType", SqlDbType.VarChar);
                tempParameter.Value = operaType;
                parameterList.Add(tempParameter);
            }
            if (memo != "")
            {
                strWhere.Append("a.Memo like '%' + @Memo + '%' and ");
                tempParameter = new SqlParameter("@Memo", SqlDbType.NVarChar);
                tempParameter.Value = memo;
                parameterList.Add(tempParameter);
            }
            if (beginDate != "")
            {
                strWhere.Append("a.OperaTime >= @BeginOperTime and ");
                tempParameter = new SqlParameter("@BeginOperTime", SqlDbType.DateTime);
                tempParameter.Value = DateTime.Parse(beginDate);
                parameterList.Add(tempParameter);
            }
            if (endDate != "")
            {
                strWhere.Append("a.OperaTime <= @EndOperTime and ");
                tempParameter = new SqlParameter("@EndOperTime", SqlDbType.DateTime);
                tempParameter.Value = DateTime.Parse(endDate + " 23:59:59");
                parameterList.Add(tempParameter);
            }

            BLL.System.sys_OperaLog bll = new BLL.System.sys_OperaLog();
            try
            {
                DataTable dt = bll.GetList_Menu(Utils.DelLastChar(strWhere.ToString(), " and "), parameterList).Tables[0];
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

        #region 删除操作日志信息==============================
        private void DelData(HttpContext context, string btn)
        {
            if (btn != "btnDel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }

            string perName = RequestHelper.GetString("perName");
            string perAccount = RequestHelper.GetString("perAccount");
            string menuId = RequestHelper.GetString("menu");
            string operaType = RequestHelper.GetString("operaType");
            string memo = RequestHelper.GetString("memo");
            string beginDate = RequestHelper.GetString("beginDate");
            string endDate = RequestHelper.GetString("endDate");

            StringBuilder strWhere =new StringBuilder();
            StringBuilder strWhere1 = new StringBuilder();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            SqlParameter tempParameter = new SqlParameter();

            if (perName != "")
            {
                strWhere1.Append(" and PerName like '%" + perName + "%'");
                strWhere.Append("PerName like '%' + @perName + '%' and ");
                tempParameter = new SqlParameter("@perName", SqlDbType.NVarChar);
                tempParameter.Value = perName;
                parameterList.Add(tempParameter);
            }
            if (perAccount != "")
            {
                strWhere1.Append(" and PerAccount like '%" + perAccount + "%'");
                strWhere.Append("PerAccount like '%' + @PerAccount + '%' and ");
                tempParameter = new SqlParameter("@PerAccount", SqlDbType.NVarChar);
                tempParameter.Value = perAccount;
                parameterList.Add(tempParameter);
            }
            if (menuId != "")
            {
                strWhere1.Append(" and MenuId =" + menuId + "");
                strWhere.Append("MenuId = @MenuId and ");
                tempParameter = new SqlParameter("@MenuId", SqlDbType.Int);
                tempParameter.Value = Utils.StrToInt(menuId, 0);
                parameterList.Add(tempParameter);
            }
            if (operaType != "")
            {
                strWhere1.Append(" and OperaType = '" + operaType + "'");
                strWhere.Append("OperaType = @OperaType and ");
                tempParameter = new SqlParameter("@OperaType", SqlDbType.VarChar);
                tempParameter.Value = operaType;
                parameterList.Add(tempParameter);
            }
            if (memo != "")
            {
                strWhere1.Append(" and memo like '%" + memo + "%'");
                strWhere.Append("Memo like '%' + @Memo + '%' and ");
                tempParameter = new SqlParameter("@Memo", SqlDbType.NVarChar);
                tempParameter.Value = memo;
                parameterList.Add(tempParameter);
            }
            if (beginDate != "")
            {
                strWhere1.Append(" and OperaTime >= '" + beginDate + "'");
                strWhere.Append("OperTime >= @BeginOperTime and ");
                tempParameter = new SqlParameter("@BeginOperTime", SqlDbType.DateTime);
                tempParameter.Value = DateTime.Parse(beginDate);
                parameterList.Add(tempParameter);
            }
            if (endDate != "")
            {
                strWhere1.Append(" and OperTime <= '" + endDate + " 23:59:59'");
                strWhere.Append("OperaTime <= @EndOperTime and ");
                tempParameter = new SqlParameter("@EndOperTime", SqlDbType.DateTime);
                tempParameter.Value = DateTime.Parse(endDate + " 23:59:59");
                parameterList.Add(tempParameter);
            }

            BLL.System.sys_OperaLog bll = new BLL.System.sys_OperaLog();

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                
                if (bll.DeleteListByFilter(Utils.DelLastChar(strWhere.ToString(), " and "), parameterList, out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除操作日志：" + Utils.Filter(strWhere1.ToString());
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
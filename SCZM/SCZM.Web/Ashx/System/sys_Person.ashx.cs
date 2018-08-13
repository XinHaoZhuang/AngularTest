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
    /// sys_Person 的摘要说明


    /// </summary>
    public class sys_Person : IHttpHandler, IReadOnlySessionState
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

                case "GetList": //获取人员列表
                    GetList(context, btn);
                    break;
                case "GetData": //获取人员信息
                    GetData(context, btn);
                    break;
                case "SaveData": //保存人员信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除人员信息
                    DelData(context, btn);
                    break;
                case "GetPwd": //获取人员密码
                    GetPwd(context, btn);
                    break;
                case "InitPwd": //初始化人员密码


                    InitPwd(context, btn);
                    break;
            }
        }
        #region 获取人员列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int depId = RequestHelper.GetInt("depId", 1);
                string filterFlag = RequestHelper.GetString("filterFlag");
                StringBuilder strWhere =new StringBuilder();
                List<SqlParameter> parameterList = new List<SqlParameter>();
                SqlParameter tempParameter = new SqlParameter();

                if (filterFlag == "")
                {
                    strWhere.Append("a.DepId=@DepId and ");
                    tempParameter = new SqlParameter("@DepId", SqlDbType.Int, 4);
                    tempParameter.Value = depId;
                    parameterList.Add(tempParameter);
                }
                else
                {
                    string perName = RequestHelper.GetString("perName");
                    string account = RequestHelper.GetString("account");
                    string postId = RequestHelper.GetString("postId");
                    string roleId = RequestHelper.GetString("roleId");
                    if (perName != "")
                    {
                        strWhere.Append("a.PerName like '%'+ @PerName +'%' and ");
                        tempParameter = new SqlParameter("@PerName", SqlDbType.NVarChar);
                        tempParameter.Value = perName;
                        parameterList.Add(tempParameter);
                    }
                    if (account != "")
                    {
                        strWhere.Append("a.Account like '%'+  @Account +'%' and ");
                        tempParameter = new SqlParameter("@Account", SqlDbType.NVarChar);
                        tempParameter.Value = account;
                        parameterList.Add(tempParameter);
                    }
                    if (postId != "")
                    {
                        strWhere.Append("a.PostId =@PostId and ");
                        tempParameter = new SqlParameter("@PostId", SqlDbType.Int);
                        tempParameter.Value = Utils.StrToInt(postId, 0).ToString();
                        parameterList.Add(tempParameter);
                    }
                    if (roleId != "")
                    {
                        strWhere.Append("','+a.RoleId+',' like '%,'+  @RoleId +',%' and ");
                        tempParameter = new SqlParameter("@RoleId", SqlDbType.VarChar);
                        tempParameter.Value = roleId;
                        parameterList.Add(tempParameter);
                    }
                }

                BLL.System.sys_Person bll = new BLL.System.sys_Person();
                DataTable dt = bll.GetList(Utils.DelLastChar(strWhere.ToString(), " and "), parameterList).Tables[0];
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
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 获取人员信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int perId = RequestHelper.GetInt("id", 0);
                BLL.System.sys_Person bll = new BLL.System.sys_Person();
                DataTable dt = bll.GetList(perId).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"获取数据成功！\",\"info\":");
                jsonStr.Append(rowsStr);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 保存人员信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string id = RequestHelper.GetString("id");
            string depId = RequestHelper.GetString("depId");
            string postId = RequestHelper.GetString("postId");
            string perName = RequestHelper.GetString("perName");
            string account = RequestHelper.GetString("account");
            string perTel = RequestHelper.GetString("perTel");
            string perEmail = RequestHelper.GetString("perEmail");
            string ddNo = RequestHelper.GetString("ddNo");
            string wxNo = RequestHelper.GetString("wxNo");
            string roleId = RequestHelper.GetString("roleId");
            string roleName = RequestHelper.GetString("roleName");
            string ctrlPersonType = RequestHelper.GetString("ctrlPersonType");
            string ctrlDepId = RequestHelper.GetString("ctrlDepId");
            string ctrlPerId = RequestHelper.GetString("ctrlPerId");
            if (depId == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"部门不能为空！\"}");
                return;
            }
            if (perName == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"姓名不能为空！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            BLL.System.sys_Person bll = new BLL.System.sys_Person();
            Model.System.sys_Person model = new Model.System.sys_Person();
            model.ID = Utils.StrToInt(id, 0);
            model.DepId = Utils.StrToInt(depId,0);
            model.PostId = Utils.StrToInt(postId, 0);
            model.PerName = perName;
            model.Account = account;
            model.PerTel = perTel;
            model.PerEmail = perEmail;
            model.DDNo = ddNo;
            model.WXNo = wxNo;
            model.RoleId = roleId;
            model.RoleName = roleName;
            model.CtrlPersonType = Utils.StrToInt(ctrlPersonType,1);
            model.CtrlDepId = Utils.StrToInt(ctrlDepId, 0);
            model.CtrlPerId = ctrlPerId;

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
                        operaMemo = "新增人员：" + model.PerName + "(" + model.ID + ")";
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
                        operaMemo = "修改人员：" + model.PerName + "(" + model.ID + ")";
                        //写入操作日志
                        BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                    }
                    
                }
                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 删除人员信息==============================
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
            BLL.System.sys_Person bll = new BLL.System.sys_Person();
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
                    operaMemo = "删除人员：" + nameStr + "(" + idStr + ")";
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }

        }
        #endregion
        #region 获取人员密码==============================
        private void GetPwd(HttpContext context, string btn)
        {
            if (btn != "btnGetPwd")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int perId = RequestHelper.GetInt("id", 0);
                BLL.System.sys_Person bll = new BLL.System.sys_Person();
                string pwd = bll.GetPwd(perId);
                
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"获取数据成功！\",\"info\":");
                jsonStr.Append(pwd);
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
        #region 初始化人员密码==============================
        private void InitPwd(HttpContext context, string btn)
        {
            if (btn != "btnInitPwd")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int perId = RequestHelper.GetInt("id", 0);
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

                string operaAction = "";
                string operaMemo = "";

                BLL.System.sys_Person bll = new BLL.System.sys_Person();
                string pwd = bll.InitPwd(perId);
                if (pwd == "")
                {
                    context.Response.Write("{\"status\":\"1\",\"msg\":\"初始化密码失败！\"}");
                    return;
                }
                else
                {
                    operaAction = Enums.ActionEnum.Edit.ToString();
                    operaMemo = "初始化密码：(" + perId + ")";
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                context.Response.Write("{\"status\":\"1\",\"msg\":\"初始化成功，密码为" + pwd + "！\"}");
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
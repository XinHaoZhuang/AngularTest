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
    /// sys_Role 的摘要说明


    /// </summary>
    public class sys_Role : IHttpHandler, IReadOnlySessionState
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

                case "GetList": //获取角色列表
                    GetList(context, btn);
                    break;
                case "GetData": //获取角色信息
                    GetData(context, btn);
                    break;
                case "SaveData": //保存角色信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除角色信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取角色列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                
                BLL.System.sys_Role bll = new BLL.System.sys_Role();
                DataTable dt = bll.GetListAll("").Tables[0];
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
        #region 获取角色信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int roleId = RequestHelper.GetInt("id", 0);
                string roleStr = "";
                BLL.System.sys_Role bll = new BLL.System.sys_Role();
                if (roleId != 0)
                {
                    DataTable dt = bll.GetList(roleId).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                        return;
                    }
                    roleStr = Utils.ToJson(dt);
                }
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                StringBuilder strWhere = new StringBuilder();
                if (!loginUserModel.IsAdmin)
                {
                    strWhere.Append(" and MenuType<>'system'");
                }
                BLL.System.sys_Menu menuBll = new BLL.System.sys_Menu();
                DataTable menuDT = menuBll.GetList(strWhere.ToString()).Tables[0];
                DataTable powerDT = bll.GetRolePowerAllList(roleId).Tables[0];
                string menuStr = DtToRolePowerJson(menuDT, powerDT, 0);
                menuStr = "[" + menuStr.Substring(1) + "]";
                StringBuilder jsonStr = new StringBuilder();
                if (roleId != 0)
                {
                    jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"roleInfo\":" + roleStr + ",\"menuInfo\":" + menuStr + "}");
                }
                else
                {
                    jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"roleInfo\":\"\",\"menuInfo\":" + menuStr + "}");
                }
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 保存角色信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string roleId = RequestHelper.GetString("id");
            string roleName = RequestHelper.GetString("roleName");
            string memo = RequestHelper.GetString("memo");
            string powerStr = RequestHelper.GetString("powerStr");

            if (roleId != "" && Utils.StrToInt(roleId, 0) == 0)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"非法传入页面！\"}");
                return;
            }
            if (roleName == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"角色名称不能为空！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

            Model.System.sys_Role model = new Model.System.sys_Role();
            model.ID =Utils.StrToInt(roleId,0);
            model.RoleName = roleName;
            model.Memo = memo;

            List<Model.System.sys_RolePower> powerList = new List<Model.System.sys_RolePower>();
            string[] powerArray = powerStr.Split(',');
            Model.System.sys_RolePower rolePowerModel = new Model.System.sys_RolePower();
            foreach (string powerId in powerArray)
            {
                rolePowerModel = new Model.System.sys_RolePower();
                rolePowerModel.PowerId = Utils.StrToInt(powerId, 0);
                powerList.Add(rolePowerModel);
            }
            model.sys_RolePowers = powerList;
            model.FlagDel = false;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;
            
            BLL.System.sys_Role bll = new BLL.System.sys_Role();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (model.ID == 0)
                {
                    model.ID = bll.Add(model, out operaMessage);
                    if (model.ID > 0)
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Add.ToString();
                        operaMemo = "新增角色：" + model.RoleName + "(" + model.ID + ")";
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
                        operaMemo = "修改角色：" + model.RoleName + "(" + model.ID + ")";
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
        #region 删除角色信息==============================
        private void DelData(HttpContext context, string btn)
        {
            if (btn != "btnDel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string idStr =PageValidate.SafeLongFilter(RequestHelper.GetString("idStr"),0);
            string nameStr = RequestHelper.GetString("nameStr");
            if (idStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要删除的记录！\"}");
                return;
            }
            
            BLL.System.sys_Role bll = new BLL.System.sys_Role();
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.DeleteList(idStr,out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除角色：" + nameStr + "(" + idStr + ")";
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

        #region 生成角色的菜单权限JSON
        /// <summary>
        /// 生成角色的菜单权限JSON
        /// </summary>
        /// <param name="menuDT"></param>
        /// <param name="powerDT"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        private string DtToRolePowerJson(DataTable menuDT, DataTable powerDT, int nodeId)
        {
            StringBuilder menuJsonStr =new StringBuilder();
            //int rowNum = 0;

            DataRow[] drs = menuDT.Select("SupId=" + nodeId + "","SortId asc");
            DataRow[] powerDRs = null;
            int i=0;
            if (drs.Length != 0)
            {
                foreach (DataRow dr in drs)
                {
                    //第一行以“{”开始，其他行以“,{”开始


                    menuJsonStr.Append(",{");
                    menuJsonStr.Append("\"MenuID\":" + dr["ID"].ToString() + ",\"MenuName\":\"" + dr["MenuName"].ToString() + "\",\"LevelId\":" + dr["LevelId"].ToString() + "");
                    //获取菜单的权限


                    powerDRs = null;
                    powerDRs = powerDT.Select("MenuId=" + dr["ID"].ToString(), "PowerId asc");

                    if (powerDRs.Length != 0)
                    {

                        menuJsonStr.Append(",\"FlagPower\":\"1\",\"Power\":[");
                        for (i = 0; i < powerDRs.Length; i++)
                        {
                            menuJsonStr.Append("{\"PowerId\":\"" + powerDRs[i]["PowerId"].ToString() + "\",\"PowerName\":\"" + powerDRs[i]["PowerName"].ToString() + "\",\"FlagSet\":\"" + powerDRs[i]["FlagSet"].ToString() + "\"}");
                            if (i < powerDRs.Length - 1)
                            {
                                menuJsonStr.Append(",");
                            }
                        }
                        menuJsonStr.Append("]");
                    }
                    else
                    {
                        menuJsonStr.Append(",\"FlagPower\":\"0\"");
                    }
                    //判断是否有下级，有则递归调用
                    if (menuDT.Select("SupId=" + dr["ID"].ToString() + "").Length != 0)
                    {
                        menuJsonStr.Append(",\"FlagLast\":\"0\"}");

                        menuJsonStr.Append(DtToRolePowerJson(menuDT, powerDT, Utils.ObjToInt(dr["ID"],0)));
                    }
                    else
                    {
                        menuJsonStr.Append(",\"FlagLast\":\"1\"}");
                    }
                }
            }

            return menuJsonStr.ToString();
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
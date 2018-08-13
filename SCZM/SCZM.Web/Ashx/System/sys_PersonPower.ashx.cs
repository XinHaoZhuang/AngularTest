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
    /// sys_PersonPower 的摘要说明


    /// </summary>
    public class sys_PersonPower : IHttpHandler, IReadOnlySessionState
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

                case "GetList": //获取人员权限列表
                    GetList(context, btn);
                    break;
                case "GetData": //获取人员权限信息
                    GetData(context, btn);
                    break;
                case "SaveData": //保存人员权限信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除人员权限信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取人员权限列表==============================
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
                BLL.System.sys_PersonPower bll = new BLL.System.sys_PersonPower();
                DataTable dt = bll.GetPerSettingList("a.DepId=" + depId.ToString()).Tables[0];
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
        #region 获取人员权限信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            int perId = RequestHelper.GetInt("id", 0);
            if (perId == 0)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，人员ID不能为空！\"}");
                return;
            }
            try
            {
                BLL.System.sys_PersonPower bll = new BLL.System.sys_PersonPower();

                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                StringBuilder strWhere = new StringBuilder();
                if (!loginUserModel.IsAdmin)
                {
                    strWhere.Append(" and MenuType<>'system'");
                }
                BLL.System.sys_Menu menuBll = new BLL.System.sys_Menu();
                DataTable menuDT = menuBll.GetList(strWhere.ToString()).Tables[0];
                DataTable powerDT = bll.GetPerPowerAllList(perId).Tables[0];
                string menuStr = DtToRolePowerJson(menuDT, powerDT, 0);
                menuStr = "[" + menuStr.Substring(1) + "]";
                StringBuilder jsonStr = new StringBuilder();

                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"menuInfo\":" + menuStr + "}");

                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 保存人员权限信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            int perId = RequestHelper.GetInt("id",0);
            string perName = RequestHelper.GetString("perName");
            string powerStr = RequestHelper.GetString("powerStr");
            if (perId == 0)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，人员ID不能为空！\"}");
                return;
            }
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                BLL.System.sys_PersonPower bll = new BLL.System.sys_PersonPower();

                DateTime operaTime = DateTime.Now;
                List<Model.System.sys_PersonPower> powerList = new List<Model.System.sys_PersonPower>();
                string[] powerArray = powerStr.Split(',');
                Model.System.sys_PersonPower perPowerModel = new Model.System.sys_PersonPower();
                foreach (string powerId in powerArray)
                {
                    perPowerModel = new Model.System.sys_PersonPower();
                    perPowerModel.PerId = perId;
                    perPowerModel.PowerId = Utils.StrToInt(powerId, 0);
                    perPowerModel.FlagDel = false;
                    perPowerModel.OperaName = loginUserModel.PerName;
                    perPowerModel.OperaTime = operaTime;
                    powerList.Add(perPowerModel);
                }
                if (bll.Update(powerList, out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Edit.ToString();
                    operaMemo = "设置人员权限：" + perName + "(" + perId.ToString() + ")";
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
        #region 删除人员权限信息==============================
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
            BLL.System.sys_PersonPower bll = new BLL.System.sys_PersonPower();
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
                    operaMemo = "删除人员权限：" + nameStr + "(" + idStr + ")";
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

        #region 生成人员权限的菜单权限JSON
        /// <summary>
        /// 生成人员权限的菜单权限JSON
        /// </summary>
        /// <param name="menuDT"></param>
        /// <param name="powerDT"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        private string DtToRolePowerJson(DataTable menuDT, DataTable powerDT, int nodeId)
        {
            StringBuilder menuJsonStr =new StringBuilder();
            //int rowNum = 0;

            DataRow[] drs = menuDT.Select("SupId=" + nodeId + "", "SortId asc");
            DataRow[] powerDRs = null;
            int i = 0;
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
                            menuJsonStr.Append("{\"PowerId\":\"" + powerDRs[i]["PowerId"].ToString() + "\",\"PowerName\":\"" + powerDRs[i]["PowerName"].ToString() + "\",\"FlagSet\":\"" + powerDRs[i]["FlagSet"].ToString() + "\",\"FlagRole\":\"" + powerDRs[i]["FlagRole"].ToString() + "\"}");
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

                        menuJsonStr.Append(DtToRolePowerJson(menuDT, powerDT, Utils.ObjToInt(dr["ID"], 0)));
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
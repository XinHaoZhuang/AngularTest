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
    /// sys_Menu 的摘要说明


    /// </summary>
    public class sys_Menu : IHttpHandler, IReadOnlySessionState
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

                case "GetList": //获取菜单列表
                    GetList(context, btn);
                    break;
                case "GetData": //获取菜单信息
                    GetData(context, btn);
                    break;
                case "SaveData": //保存菜单信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除菜单信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取菜单列表==============================
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                BLL.System.sys_Menu bll = new BLL.System.sys_Menu();
                DataTable dt = bll.GetList("").Tables[0];
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append("{\"total\":" + dt.Rows.Count + ",\"rows\":[");
                string flagDel = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["FlagDel"].ToString() == "0")
                        {
                            flagDel = "否";
                        }
                        else
                        {
                            flagDel = "是";
                        }
                        jsonStr.Append("{\"ID\":" + dt.Rows[i]["ID"].ToString() + ",\"MenuName\":\"" + dt.Rows[i]["MenuName"].ToString() + "\",\"LinkUrl\":\"" + dt.Rows[i]["LinkUrl"].ToString() + "\",\"LevelId\":\"" + dt.Rows[i]["LevelId"].ToString() + "\",\"SortId\":\"" + dt.Rows[i]["SortId"].ToString() + "\",\"PowerList\":\"" + dt.Rows[i]["PowerList"].ToString() + "\",\"FlagDel\":\"" + flagDel + "\"");
                        
                        if (dt.Rows[i]["SupId"].ToString() != "0")
                        {
                            jsonStr.Append(",\"_parentId\":" + dt.Rows[i]["SupId"].ToString());
                        }
                        if (dt.Select("SupId=" + dt.Rows[i]["ID"].ToString()).Length > 0)
                        {
                            jsonStr.Append(",\"state\":\"closed\"");
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
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 获取菜单信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int menuId = RequestHelper.GetInt("id", 1);
                BLL.System.sys_Menu bll = new BLL.System.sys_Menu();
                DataTable dt = bll.GetList(menuId).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);

                DataTable pageDT = bll.GetPageList(menuId).Tables[0];
                

                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":");
                jsonStr.Append(rowsStr);
                jsonStr.Append(",\"pageInfo\":{\"total\":" + pageDT.Rows.Count + ",\"rows\":" + Utils.ToJson(pageDT) + "}");
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
        #region 保存菜单信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string id = RequestHelper.GetString("id");
            string supId = RequestHelper.GetString("supId");
            string menuName = RequestHelper.GetString("menuName");
            string linkUrl = RequestHelper.GetString("linkUrl");
            string sortId = RequestHelper.GetString("sortId");
            string flagDel = RequestHelper.GetString("flagDel");
            string powerList = RequestHelper.GetString("powerList");
            string pageStr1 = RequestHelper.GetString("pageStr1");
            string pageStr2 = RequestHelper.GetString("pageStr2");
            string pageStr3 = RequestHelper.GetString("pageStr3");
            string pageStr4 = RequestHelper.GetString("pageStr4");
            string pageStr5 = RequestHelper.GetString("pageStr5");
            string pageStr6 = RequestHelper.GetString("pageStr6");
            string pageStr7 = RequestHelper.GetString("pageStr7");
            string pageStr8 = RequestHelper.GetString("pageStr8");
            if (supId == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"上级菜单编码不能为空！\"}");
                return;
            }
            if (menuName == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"菜单名称不能为空！\"}");
                return;
            }
            if (flagDel == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"是否隐藏不能为空！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

            BLL.System.sys_Menu bll = new BLL.System.sys_Menu();
            Model.System.sys_Menu model = new Model.System.sys_Menu();
            model.ID = Utils.StrToInt(id,0);
            model.MenuName = menuName;
            model.LinkUrl = linkUrl;
            model.SortId = int.Parse(sortId);
            model.SupId = int.Parse(supId);
            model.PowerList = powerList;
            model.FlagDel = Utils.StrToInt(flagDel, 0);
            DataTable dt = bll.GetList(int.Parse(supId)).Tables[0];
            if (dt.Rows.Count > 0)
            {
                model.SupList = dt.Rows[0]["SupList"].ToString() + supId + ",";
                model.LevelId = Utils.ObjToInt(dt.Rows[0]["levelId"], 1) + 1;
            }
            else
            {
                model.SupList = supId + ",";
                model.LevelId = 1;
            }
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
                    model.ID = bll.Add(model, pageStr1, pageStr2, pageStr3, pageStr4, pageStr5, pageStr6, pageStr7, pageStr8, out operaMessage);

                    if (model.ID > 0)
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Add.ToString();
                        operaMemo = "新增菜单：" + model.MenuName + "(" + model.ID + ")";
                        //写入操作日志
                        BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                    }
                }
                else
                {
                    if (bll.Update(model, pageStr1, pageStr2, pageStr3, pageStr4, pageStr5, pageStr6, pageStr7, pageStr8, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改菜单：" + model.MenuName + "(" + model.ID + ")";
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
        #region 删除菜单信息==============================
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
            BLL.System.sys_Menu bll = new BLL.System.sys_Menu();
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
                    operaMemo = "删除菜单：" + nameStr + "(" + idStr + ")";
                    //写入操作日志
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
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
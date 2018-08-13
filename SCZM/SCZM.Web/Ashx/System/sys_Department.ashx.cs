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
    /// sys_Department 的摘要说明


    /// </summary>
    public class sys_Department: IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取部门列表
                    GetList(context,btn);
                    break;
                case "GetData": //获取部门信息
                    GetData(context,btn);
                    break;
                case "SaveData": //保存部门信息
                    SaveData(context,btn);
                    break;
                case "DelData": //删除部门信息
                    DelData(context,btn);
                    break;
                case "GetShopInfo"://获取专卖店信息


                    GetShopInfo(context, btn);
                    break;
            }
        }
        #region 获取部门列表==============================
        private void GetList(HttpContext context,string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                
                int supId = RequestHelper.GetInt("supId", 1);
                string filterFlag = RequestHelper.GetString("filterFlag");

                BLL.System.sys_Department bll = new BLL.System.sys_Department();

                StringBuilder strWhere =new StringBuilder();
                List<SqlParameter> parameterList = new List<SqlParameter>();
                SqlParameter tempParameter = new SqlParameter();

                if (filterFlag == "")
                {
                    strWhere.Append(" and SupId=@SupId");
                    tempParameter = new SqlParameter("@SupId", SqlDbType.Int, 4);
                    tempParameter.Value = supId;
                    parameterList.Add(tempParameter);
                }
                else
                {
                    var depName = RequestHelper.GetString("depName");
                    var QDCode = RequestHelper.GetString("QDCode");
                    if (depName != "")
                    {
                        strWhere.Append(" and a.DepName like '%'+ @DepName +'%'");
                        tempParameter = new SqlParameter("@DepName", SqlDbType.NVarChar);
                        tempParameter.Value = depName;
                        parameterList.Add(tempParameter);
                    }
                    if (QDCode != "")
                    {
                        strWhere.Append(" and a.QDCode like '%'+ @QDCode +'%'");
                        tempParameter = new SqlParameter("@QDCode", SqlDbType.NVarChar);
                        tempParameter.Value = QDCode;
                        parameterList.Add(tempParameter);
                    }
                }

                DataTable dt = bll.GetList(strWhere.ToString(), parameterList).Tables[0];
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
        #region 获取部门信息==============================
        private void GetData(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int depId = RequestHelper.GetInt("id", 0);
                BLL.System.sys_Department bll = new BLL.System.sys_Department();

                DataTable dt = bll.GetList(depId).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);

                //DataTable ctrlDepDT = bll.GetCtrlDepList(depId).Tables[0];
                //string ctrlDepInfo = Utils.ToJson(ctrlDepDT);

                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion
        #region 保存部门信息==============================
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string id = RequestHelper.GetString("id");
            string supId = RequestHelper.GetString("supId");
            string depName = RequestHelper.GetString("depName");
            string depType = RequestHelper.GetString("depType");
            string QDCode = RequestHelper.GetString("QDCode");
            string depTel = RequestHelper.GetString("depTel");
            string sortId = RequestHelper.GetString("sortId");
            string flagUse = RequestHelper.GetString("flagUse");
            //string ctrlDep = RequestHelper.GetString("ctrlDep");
            if (supId == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"上级部门编码不能为空！\"}");
                return;
            }
            if (depName == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"部门名称不能为空！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();

            BLL.System.sys_Department bll = new BLL.System.sys_Department();
            Model.System.sys_Department model = new Model.System.sys_Department();

            model.ID = Utils.StrToInt(id, 0);
            model.DepName = depName;
            model.DepTel = depTel;
            model.DepTypeId = Utils.StrToInt(depType, 1);
            model.QDCode = QDCode;
            model.SortId = Utils.StrToInt(sortId,1);
            model.SupId = Utils.StrToInt(supId,1);
            model.FlagUse = Utils.StrToBool(flagUse, true);
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;
            
            DataTable dt = bll.GetList(int.Parse(supId)).Tables[0];
            model.SupList = dt.Rows[0]["SupList"].ToString() + model.SupId + ",";
            model.LevelId = Utils.ObjToInt(dt.Rows[0]["levelId"], 1) + 1;

            //List<Model.System.sys_DepartmentCtrl> ctrlModelList=new List<Model.System.sys_DepartmentCtrl> ();
            //Model.System.sys_DepartmentCtrl ctrlModel = new Model.System.sys_DepartmentCtrl();
            //string[] ctrlDepIdArray = ctrlDep.Split(',');

            //string[] ctrlDepIdArrayNew= ctrlDepIdArray.Intersect(ctrlDepIdArray).ToArray();

            //int ctrlDepId = 0;
            //foreach (string s in ctrlDepIdArrayNew)
            //{
            //    ctrlDepId = Utils.StrToInt(s, 0);
            //    if (ctrlDepId != 0)
            //    {
                    
            //        ctrlModel = new Model.System.sys_DepartmentCtrl();
            //        ctrlModel.CtrlDepId = ctrlDepId;
            //        ctrlModelList.Add(ctrlModel);
            //    }
            //}
            //model.sys_DepartmentCtrls = ctrlModelList;

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
                        operaMemo = "新增部门：" + model.DepName + "(" + model.ID + ")";
                        //写入操作日志
                        BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo); 
                    }     
                }
                else
                {
                    if (bll.Update(model,out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改部门：" + model.DepName + "(" + model.ID + ")";
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
        #region 删除部门信息==============================
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
            BLL.System.sys_Department bll = new BLL.System.sys_Department();

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
                    operaMemo = "删除部门：" + nameStr + "(" + idStr + ")";
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
        #region 获取专卖店信息==============================
        private void GetShopInfo(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int depId = RequestHelper.GetInt("id", 0);
                BLL.System.sys_Department bll = new BLL.System.sys_Department();

                DataTable dt = bll.GetShopInfo(depId).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);

                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
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
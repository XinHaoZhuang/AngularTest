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
namespace SCZM.Web.Ashx.Repair
{
    /// <summary>
    /// repair_ReceiveFee 的摘要说明
    /// <summary>
    public class repair_ReceiveFee : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取收款确认列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取收款确认明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存收款确认信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除收款确认信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取收款确认列表==============================;
        private void GetList(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string PayType = RequestHelper.GetString("PayType").Trim();
                string ReceiveDate_Start = RequestHelper.GetString("ReceiveDate_Start").Trim();
                string ReceiveDate_End = RequestHelper.GetString("ReceiveDate_End").Trim();

                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                if (LoginUserModel.IsAdmin == false)
                {
                    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                }
                //-------------------------------------------------------------------------

                //维修意向号
                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and b.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }

                
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and b.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名
                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and b.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and c.MachineModel LIKE '%" + Utils.Filter(MachineModel)+"%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and b.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //付款方式
                if (PayType != "") {
                    strWhere.Append(" and a.PayType=" + Utils.StrToInt(PayType, 0));
                }
                //收款时间
                if (ReceiveDate_Start != "") {
                    strWhere.Append(" and a.ReceiveDate>=cast('" + Utils.StrToDateTime(ReceiveDate_Start).ToString()+"' as datetime)");
                }
                if (ReceiveDate_End != "") {
                    strWhere.Append(" and a.ReceiveDate<=cast('" + Utils.StrToDateTime(ReceiveDate_End+" 23:59:59").ToString() + "' as datetime) ");
                }
                SCZM.BLL.Repair.repair_ReceiveFee bll = new SCZM.BLL.Repair.repair_ReceiveFee();
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

        #region 获取收款确认明细==============================;
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

                SCZM.BLL.Repair.repair_ReceiveFee bll = new SCZM.BLL.Repair.repair_ReceiveFee();
                DataSet ds = bll.GetDetail(ID);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 保存收款确认信息==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            string IntentionId = RequestHelper.GetString("IntentionId");
            string PayType = RequestHelper.GetString("PayType");
            string ReceiveFee = RequestHelper.GetString("ReceiveFee");
            string ReceiveDate = RequestHelper.GetString("ReceiveDate");
          

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.repair_ReceiveFee model = new SCZM.Model.Repair.repair_ReceiveFee();
            SCZM.BLL.Repair.repair_ReceiveFee bll = new SCZM.BLL.Repair.repair_ReceiveFee();
            model.ID = Utils.StrToInt(ID, 0);
            model.IntentionId = Utils.StrToInt(IntentionId, 0);
            model.PayType = Utils.StrToInt(PayType, 0);
            model.ReceiveFee = Utils.StrToDecimal(ReceiveFee, 0);
            if (ReceiveDate != "")
            {
                model.ReceiveDate = Utils.StrToDateTime(ReceiveDate);
            }
            model.OperaDepId = loginUserModel.DepId;
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;

            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (ID == "")
                {
                    model.ID = bll.Add(model, out operaMessage);
                    if (model.ID > 0)
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Add.ToString();
                        operaMemo = "新增收款确认：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改收款确认：" + model.ID;
                    }
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

        #region 删除收款确认信息==============================;
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
            SCZM.BLL.Repair.repair_ReceiveFee bll = new SCZM.BLL.Repair.repair_ReceiveFee();
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
                    operaMemo = "删除收款确认：" + IDStr;
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

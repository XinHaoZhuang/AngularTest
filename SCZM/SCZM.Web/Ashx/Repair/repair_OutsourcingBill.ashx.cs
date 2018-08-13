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
    /// repair_OutsourcingBill 的摘要说明
    /// <summary>
    public class repair_OutsourcingBill : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取外加工台帐列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取外加工台帐明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存外加工台帐信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除外加工台帐信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取外加工台帐列表==============================;
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
                string IntentionCode = RequestHelper.GetString("IntentionCode").Trim();
                string FeeItemId = RequestHelper.GetString("FeeItemId").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string ReimbursementDate_Start = RequestHelper.GetString("ReimbursementDate_Start").Trim();
                string ReimbursementDate_End = RequestHelper.GetString("ReimbursementDate_End").Trim();
                string XsSp = RequestHelper.GetString("XsSp").Trim();
                string OperaTime_Start = RequestHelper.GetString("OperaTime_Start").Trim();
                string OperaTime_End = RequestHelper.GetString("OperaTime_End").Trim();

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
                //保修类型
                if (FeeItemId != "")
                {
                    strWhere.Append(" and b.IntentionType=" + Utils.StrToInt(FeeItemId, 0));
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
                    strWhere.Append(" and c.MachineModel like '%" + Utils.Filter(MachineModel) + "%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and b.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //报修日期
                if (ReimbursementDate_Start != "")
                {
                    strWhere.Append(" and a.ReimbursementDate>= cast('" + Utils.StrToDateTime(ReimbursementDate_Start).ToString() + "' as datetime)");
                }
                if (ReimbursementDate_End != "")
                {
                    strWhere.Append(" and a.ReimbursementDate<=cast('" + Utils.StrToDateTime(ReimbursementDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                //维修类型
                if (XsSp != "")
                {
                    strWhere.Append(" and a.XsSp=" + Utils.StrToInt(XsSp, 0));
                }
                //录入日期
                if (OperaTime_Start != "")
                {
                    strWhere.Append(" and a.OperaTime>= cast('" + Utils.StrToDateTime(OperaTime_Start).ToString() + "' as datetime)");
                }
                if (OperaTime_End != "")
                {
                    strWhere.Append(" and a.OperaTime<=cast('" + Utils.StrToDateTime(OperaTime_End + " 23:59:59").ToString() + "' as datetime)");
                }
                SCZM.BLL.Repair.repair_OutsourcingBill bll = new SCZM.BLL.Repair.repair_OutsourcingBill();
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

        #region 获取外加工台帐明细==============================;
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

                SCZM.BLL.Repair.repair_OutsourcingBill bll = new SCZM.BLL.Repair.repair_OutsourcingBill();
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

        #region 保存外加工台帐信息==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            string IntentionId = RequestHelper.GetString("IntentionId");
            string HappendDate = RequestHelper.GetString("HappendDate");
            string Address = RequestHelper.GetString("Address");
            string FeeItemId = RequestHelper.GetString("FeeItemId");
            string Plant = RequestHelper.GetString("Plant");
            string PlantContent = RequestHelper.GetString("PlantContent");
            string PayFee = RequestHelper.GetString("PayFee");
            string SystemFee = RequestHelper.GetString("SystemFee");
            string XsSp = RequestHelper.GetString("XsSp");
            string FlagRepair = RequestHelper.GetString("FlagRepair");
            string FlagZB = RequestHelper.GetString("FlagZB");
            string FlagOther = RequestHelper.GetString("FlagOther");
            string ReimbursementDate = RequestHelper.GetString("ReimbursementDate");
            string Memo = RequestHelper.GetString("Memo");
            string OperaDepId = RequestHelper.GetString("OperaDepId");

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.repair_OutsourcingBill model = new SCZM.Model.Repair.repair_OutsourcingBill();
            SCZM.BLL.Repair.repair_OutsourcingBill bll = new SCZM.BLL.Repair.repair_OutsourcingBill();
            model.ID = Utils.StrToInt(ID, 0);
            model.IntentionId = Utils.StrToInt(IntentionId, 0);
            if (HappendDate != "")
            {
                model.HappendDate = Utils.StrToDateTime(HappendDate);
            }
            model.Address = Address;
            model.FeeItemId = Utils.StrToInt(FeeItemId, 0);
            model.Plant = Plant;
            model.PlantContent = PlantContent;
            model.PayFee = Utils.StrToDecimal(PayFee, 0);
            model.SystemFee = Utils.StrToDecimal(SystemFee, 0);
            model.XsSp = Utils.StrToInt(XsSp, 0);
            model.FlagRepair = Utils.StrToInt(FlagRepair, 0);
            model.FlagZB = Utils.StrToInt(FlagZB, 0);
            model.FlagOther = Utils.StrToInt(FlagOther, 0);
            if (ReimbursementDate != "")
            {
                model.ReimbursementDate = Utils.StrToDateTime(ReimbursementDate);
            }
            model.Memo = Memo;
            model.OperaDepId = Utils.StrToInt(OperaDepId, 0);
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
                        operaMemo = "新增外加工台帐：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改外加工台帐：" + model.ID;
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

        #region 删除外加工台帐信息==============================;
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
            SCZM.BLL.Repair.repair_OutsourcingBill bll = new SCZM.BLL.Repair.repair_OutsourcingBill();
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
                    operaMemo = "删除外加工台帐：" + IDStr;
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

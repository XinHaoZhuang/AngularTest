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
    /// repair_SettlementList 的摘要说明
    /// <summary>
    public class repair_SettlementList : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取费用结算单列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取费用结算单明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存费用结算单信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除费用结算单信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取费用结算单列表==============================;
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
                string SettlementCode = RequestHelper.GetString("SettlementCode").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string FlagSX = RequestHelper.GetString("FlagSX").Trim();
                string SettlementTypeId = RequestHelper.GetString("SettlementTypeId").Trim();

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
               
                //结算单号
                if (SettlementCode != "" && Utils.IsSafeSqlString(SettlementCode))
                {
                    strWhere.Append(" and a.SettlementCode like '%" + Utils.Filter(SettlementCode) + "%'");
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
                    strWhere.Append(" and c.MachineModel like '%" + Utils.Filter(MachineModel)+"%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and b.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //赊销
                if (FlagSX != "")
                {
                    strWhere.Append(" and a.FlagSX=" + Utils.StrToInt(FlagSX,0));
                }
               
                //结算方式
                if (SettlementTypeId != "")
                {
                    strWhere.Append(" and a.SettlementTypeId=" + Utils.StrToInt(SettlementTypeId, 0));
                }


                SCZM.BLL.Repair.repair_SettlementList bll = new SCZM.BLL.Repair.repair_SettlementList();
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

        #region 获取费用结算单明细==============================;
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

                SCZM.BLL.Repair.repair_SettlementList bll = new SCZM.BLL.Repair.repair_SettlementList();
                DataSet ds = bll.GetDetail(ID);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                DataTable dt_attachment = ds.Tables["attachment"];
                string rowsStr = Utils.ToJson(dt);
                string attachment = Utils.ToJson(dt_attachment);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + ",\"AttachmentId_SettlementInfo\":" + attachment + "");
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 保存费用结算单信息==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            SCZM.BLL.Repair.repair_SettlementList bll = new SCZM.BLL.Repair.repair_SettlementList();
            string ID = RequestHelper.GetString("ID");
            string SettlementCode = RequestHelper.GetString("SettlementCode");
            if (SettlementCode == "") {
                SettlementCode = "JS" + DateTime.Now.ToString("yyyyMMdd") + bll.GetMaxId();
            }
            string IntentionId = RequestHelper.GetString("IntentionId");
            string SettlementTypeId = RequestHelper.GetString("SettlementTypeId");
            string SettlementFee = RequestHelper.GetString("SettlementFee");
            string FlagSX = RequestHelper.GetString("FlagSX");
            string SettlementFee_rebate = RequestHelper.GetString("SettlementFee_rebate");
            //string SettlementDate = RequestHelper.GetString("SettlementDate");
            string Memo = RequestHelper.GetString("Memo");
            string AttachmentId_Settlement = RequestHelper.GetString("AttachmentId_Settlement");
            string TimeFee = RequestHelper.GetString("TimeFee");
            string PartFee = RequestHelper.GetString("PartFee");


            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.repair_SettlementList model = new SCZM.Model.Repair.repair_SettlementList();
            
            model.ID = Utils.StrToInt(ID, 0);
            model.SettlementCode = SettlementCode;
            model.IntentionId = Utils.StrToInt(IntentionId, 0);
            model.SettlementTypeId = Utils.StrToInt(SettlementTypeId, 0);
            model.SettlementFee = Utils.StrToDecimal(SettlementFee, 0);
            model.FlagSX = Utils.StrToInt(FlagSX, 0);
            model.SettlementFee_rebate = Utils.StrToDecimal(SettlementFee_rebate, 0);
            //if (SettlementDate != "")
            //{
            //    model.SettlementDate = Utils.StrToDateTime(SettlementDate);
            //}
            model.SettlementDate = DateTime.Now;
            model.Memo = Memo;
            model.AttachmentId_Settlement = AttachmentId_Settlement;
            model.OperaDepId = loginUserModel.DepId;
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;
            model.TimeFee = Utils.StrToDecimal(TimeFee, 0);
            model.PartFee = Utils.StrToDecimal(PartFee, 0);

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
                        operaMemo = "新增费用结算单：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改费用结算单：" + model.ID;
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

        #region 删除费用结算单信息==============================;
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
            string IntentionIdList = RequestHelper.GetString("IntentionIdList");
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_SettlementList bll = new SCZM.BLL.Repair.repair_SettlementList();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.DeleteList(PageValidate.SafeLongFilter(IDStr, 0),PageValidate.SafeLongFilter(IntentionIdList,0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除费用结算单：" + IDStr;
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

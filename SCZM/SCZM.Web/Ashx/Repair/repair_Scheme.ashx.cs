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
    /// repair_Scheme 的摘要说明
    /// <summary>
    public class repair_Scheme : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取维修方案列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取维修方案明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存维修方案信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除维修方案信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取维修方案列表==============================;
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
                string IntentionType = RequestHelper.GetString("IntentionType").Trim();
                string CustTypeId = RequestHelper.GetString("CustTypeId").Trim();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                //string IntentionDate_Start = RequestHelper.GetString("IntentionDate_Start").Trim();
                //string IntentionDate_End = RequestHelper.GetString("IntentionDate_End").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                //string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();

                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------
                strWhere.Append(" and a.FlagResult=1 ");
                //维修意向号
                if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                if (IntentionType != "")
                {
                    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType, 0));
                }
                //客户类型
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                //客户名
                if (CustName != "" && Utils.IsSafeSqlString(CustName))
                {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "")
                {
                    strWhere.Append(" and d.MachineModel like '%" + Utils.Filter(MachineModel)+"%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //报修日期
                //if (IntentionDate_Start != "")
                //{
                //    strWhere.Append(" and a.IntentionDate>= cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime)");
                //}
                //if (IntentionDate_End != "")
                //{
                //    strWhere.Append(" and a.IntentionDate<=cast('" + Utils.StrToDateTime(IntentionDate_End + " 23:59:59").ToString() + "' as datetime)");
                //}
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                //业务部门
                //if (BusinessDepId != "")
                //{
                //    strWhere.Append(" and a.BusinessDepId=" + Utils.StrToInt(BusinessDepId, 0));
                //}
                string FlagScheme = RequestHelper.GetString("FlagScheme");
                if (FlagScheme == "2") {
                    strWhere.Append(" and b.ID is null");
                }
                else if (FlagScheme == "1") {
                    strWhere.Append(" and b.ID is not null");
                }
                SCZM.BLL.Repair.repair_Scheme bll = new SCZM.BLL.Repair.repair_Scheme();
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

        #region 获取维修方案明细==============================;
        private void GetDetail(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                int SchemeId = RequestHelper.GetInt("SchemeId", 0);

                SCZM.BLL.Repair.repair_Scheme bll = new SCZM.BLL.Repair.repair_Scheme();
                DataSet ds = bll.GetDetail(SchemeId);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);
                DataTable AttachmentId_Scheme_predictDT = ds.Tables["AttachmentId_Scheme_predict"];
                string AttachmentId_Scheme_predictStr = Utils.ToJson(AttachmentId_Scheme_predictDT);
                DataTable AttachmentId_SchemeDT = ds.Tables["AttachmentId_Scheme"];
                string AttachmentId_SchemeStr = Utils.ToJson(AttachmentId_SchemeDT);
                DataTable AttachmentId_AgreementDT = ds.Tables["AttachmentId_Agreement"];
                string AttachmentId_AgreementStr = Utils.ToJson(AttachmentId_AgreementDT);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr);
                jsonStr.Append(",\"AttachmentId_Scheme_predictInfo\":" + AttachmentId_Scheme_predictStr);
                jsonStr.Append(",\"AttachmentId_Scheme\":" + AttachmentId_SchemeStr);
                jsonStr.Append(",\"AttachmentId_Agreement\":" + AttachmentId_AgreementStr);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 保存维修方案信息==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            string IntentionId = RequestHelper.GetString("IntentionId");
            string SchemeType = RequestHelper.GetString("SchemeType");
            string SchemeCode = RequestHelper.GetString("SchemeCode");
            string RepairContent = RequestHelper.GetString("RepairContent");
            string FlagENGKC = RequestHelper.GetString("FlagENGKC");
            string FlagPPMKC = RequestHelper.GetString("FlagPPMKC");
            string FlagENG = RequestHelper.GetString("FlagENG");
            string FlagPPM = RequestHelper.GetString("FlagPPM");
            string FlagMCV = RequestHelper.GetString("FlagMCV");
            string FlagELE = RequestHelper.GetString("FlagELE");
            string FlagVM = RequestHelper.GetString("FlagVM");
            string FlagRM = RequestHelper.GetString("FlagRM");
            string FlagSM = RequestHelper.GetString("FlagSM");
            string FlagUM = RequestHelper.GetString("FlagUM");
            string FlagVR = RequestHelper.GetString("FlagVR");
            string FlagSP = RequestHelper.GetString("FlagSP");
            string FlagOther = RequestHelper.GetString("FlagOther");
            string SchemeDate = RequestHelper.GetString("SchemeDate");
            string PromiseLeaveDate = RequestHelper.GetString("PromiseLeaveDate");
            string TimeFee = RequestHelper.GetString("TimeFee");
            string PartFee = RequestHelper.GetString("PartFee");
            string SchemeFee = RequestHelper.GetString("SchemeFee");
            string OperaDepId = RequestHelper.GetString("OperaDepId");

            string AttachmentId_Scheme = RequestHelper.GetString("AttachmentId_Scheme").Trim();
            
            string SchemeDate_predict = RequestHelper.GetString("SchemeDate_predict").Trim();
            string SchemeFee_predict = RequestHelper.GetString("SchemeFee_predict");
            string AttachmentId_Scheme_predict = RequestHelper.GetString("AttachmentId_Scheme_predict");
            string AttachmentId_Agreement = RequestHelper.GetString("AttachmentId_Agreement");
            string Memo=RequestHelper.GetString("Memo").Trim();

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.repair_Scheme model = new SCZM.Model.Repair.repair_Scheme();
            SCZM.BLL.Repair.repair_Scheme bll = new SCZM.BLL.Repair.repair_Scheme();
            model.ID = Utils.StrToInt(ID, 0);
            model.IntentionId = Utils.StrToInt(IntentionId, 0);
            if (SchemeCode == "")
            {
                SchemeCode = "FA" + DateTime.Now.ToString("yyyyMMdd") + bll.GetMaxId();
            }
            model.SchemeCode = SchemeCode;
            model.RepairContent = RepairContent;
            model.FlagENGKC = Utils.StrToInt(FlagENGKC, 0);
            model.FlagPPMKC = Utils.StrToInt(FlagPPMKC, 0);
            model.FlagENG = Utils.StrToInt(FlagENG, 0);
            model.FlagPPM = Utils.StrToInt(FlagPPM, 0);
            model.FlagMCV = Utils.StrToInt(FlagMCV, 0);
            model.FlagELE = Utils.StrToInt(FlagELE, 0);
            model.FlagVM = Utils.StrToInt(FlagVM, 0);
            model.FlagRM = Utils.StrToInt(FlagRM, 0);
            model.FlagSM = Utils.StrToInt(FlagSM, 0);
            model.FlagUM = Utils.StrToInt(FlagUM, 0);
            model.FlagVR = Utils.StrToInt(FlagVR, 0);
            model.FlagSP = Utils.StrToInt(FlagSP, 0);
            model.FlagOther = Utils.StrToInt(FlagOther, 0);
            if (SchemeDate != "")
            {
                model.SchemeDate = Utils.StrToDateTime(SchemeDate);
            }
            //else {
            //    model.SchemeDate = DateTime.Now;
            //}
            if (PromiseLeaveDate != "")
            {
                model.PromiseLeaveDate = Utils.StrToDateTime(PromiseLeaveDate);
            }
            if (SchemeDate_predict != "") {
                model.SchemeDate_predict = Utils.StrToDateTime(SchemeDate_predict);
            }
            model.AttachmentId_Scheme_predict = AttachmentId_Scheme_predict;
            model.AttachmentId_Scheme = AttachmentId_Scheme;
            model.AttachmentId_Agreement = AttachmentId_Agreement;
            model.TimeFee = Utils.StrToDecimal(TimeFee, 0);
            model.PartFee = Utils.StrToDecimal(PartFee, 0);
            model.SchemeFee = Utils.StrToDecimal(SchemeFee, 0);
            model.SchemeFee_predict = Utils.StrToDecimal(SchemeFee_predict, 0);
            model.FlagDel = 0;
            model.OperaDepId = Utils.StrToInt(OperaDepId, 0);
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;
            model.Memo = Memo;

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
                        operaMemo = "新增维修方案：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改维修方案：" + model.ID;
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

        #region 删除维修方案信息==============================;
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
            string IntentionIdStr = RequestHelper.GetString("IntentionIdStr");
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Scheme bll = new SCZM.BLL.Repair.repair_Scheme();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.DeleteList(PageValidate.SafeLongFilter(IDStr, 0), IntentionIdStr, out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "删除维修方案：" + IDStr;
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

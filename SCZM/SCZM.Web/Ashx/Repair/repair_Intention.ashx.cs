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
    /// repair_Intention 的摘要说明
    /// <summary>
    public class repair_Intention : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取维修意向列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取维修意向明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存维修意向信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除维修意向信息
                    DelData(context, btn);
                    break;
                case "GetList_Enter"://获取入场登记列表
                    GetList_Enter(context, btn);
                    break;
                case "SaveActualEnterDate":
                    SaveActualEnterDate(context, btn);
                    break;
                case "CancelActualEnterDate":
                    CancelActualEnterDate(context, btn);
                    break;
                case "GetList_Agreement"://获取协议
                    GetList_Agreement(context, btn);
                    break;
                case "SaveAgreement":
                    SaveAgreement(context, btn);
                    break;
                case "GetList_Leave"://获取出厂登记列表
                    GetList_Leave(context, btn);
                    break;
                case "SaveActualLeaveDate":
                    SaveActualLeaveDate(context, btn);
                    break;
                case "CancelActualLeaveDate":
                    CancelActualLeaveDate(context, btn);
                    break;
            }
        }
        #region 获取维修意向列表==============================;
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
                string IntentionDate_Start = RequestHelper.GetString("IntentionDate_Start").Trim();
                string IntentionDate_End = RequestHelper.GetString("IntentionDate_End").Trim();
                string RepairTypeId = RequestHelper.GetString("RepairTypeId").Trim();
                string BusinessDepId = RequestHelper.GetString("BusinessDepId").Trim();

                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                if (LoginUserModel.IsAdmin == false)
                {
                    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                }
                //-------------------------------------------------------------------------

                //维修意向号
                if (IntentionCode != ""&&Utils.IsSafeSqlString(IntentionCode)) {
                    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                }
                //保修类型
                if (IntentionType != "") {
                    strWhere.Append(" and a.IntentionType=" + Utils.StrToInt(IntentionType,0));
                }
                //客户类型
                if (CustTypeId != "") {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId,0));
                }
                //客户名
                if (CustName != ""&&Utils.IsSafeSqlString(CustName)) {
                    strWhere.Append(" and a.CustName like '%" + Utils.Filter(CustName) + "%'");
                }
                //机型
                if (MachineModel != "") {
                    strWhere.Append(" and b.MachineModel like '%" + Utils.Filter(MachineModel)+"%' ");
                }
                //机号
                if (MachineCode != "") {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //报修日期
                if (IntentionDate_Start != "") {
                    strWhere.Append(" and a.IntentionDate>= cast('" + Utils.StrToDateTime(IntentionDate_Start).ToString() + "' as datetime)");
                }
                if (IntentionDate_End != "") { 
                    strWhere.Append(" and a.IntentionDate<=cast('"+Utils.StrToDateTime(IntentionDate_End+" 23:59:59").ToString()+"' as datetime)");
                }
                //维修类型
                if (RepairTypeId != "") {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                //业务部门
                if (BusinessDepId != "") {
                    strWhere.Append(" and a.BusinessDepId=" + Utils.StrToInt(BusinessDepId,0));
                }
                SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
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

        #region 获取维修意向明细==============================;
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

                SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
                DataSet ds = bll.GetDetail(ID);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);
                DataTable AttachmentDT = ds.Tables["attachment"];
                string attachmentStr = Utils.ToJson(AttachmentDT);
                DataTable Intention_PackageDT = ds.Tables["Intention_Package"];
                string Intention_PackageStr = Utils.ToJson(Intention_PackageDT);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr);
                jsonStr.Append(",\"attachmentInfo\":" + attachmentStr);
                jsonStr.Append(",\"Intention_PackageInfo\":" + Intention_PackageStr);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 保存维修意向信息==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }

            string ID = RequestHelper.GetString("ID");
            //-------------------------客户信息-----------------------------------------------------
            string IntentionCode = RequestHelper.GetString("IntentionCode");
            string IntentionDate = RequestHelper.GetString("IntentionDate");
            string IntentionType = RequestHelper.GetString("IntentionType");
            string IntentionCode_Last = RequestHelper.GetString("IntentionCode_Last");
            string BusinessDepId = RequestHelper.GetString("BusinessDepId");
            string BusinessPerName = RequestHelper.GetString("BusinessPerName");
            string CustTypeId = RequestHelper.GetString("CustTypeId");
            string CustName = RequestHelper.GetString("CustName");
            string MachineModelId = RequestHelper.GetString("MachineModelId");
            string MachineCode = RequestHelper.GetString("MachineCode");
            string SMR = RequestHelper.GetString("SMR");
            string Linkman = RequestHelper.GetString("Linkman");
            string LinkPhone = RequestHelper.GetString("LinkPhone");
            string MachineAdress = RequestHelper.GetString("MachineAdress");
            string Machine = RequestHelper.GetString("Machine");
            string MachineStatus = RequestHelper.GetString("MachineStatus");
            //----------------------------------初步方案------------------------------------------------------
            string FlagResult = RequestHelper.GetString("FlagResult");
            
           
            //string RepairState = RequestHelper.GetString("RepairState");
            //string OperaDepId = RequestHelper.GetString("OperaDepId");
            //string ActualEnterDate = RequestHelper.GetString("ActualEnterDate");
            //string OperaName_Enter = RequestHelper.GetString("OperaName_Enter");
            //string OperaTime_Enter = RequestHelper.GetString("OperaTime_Enter");
            //string ActualLeaveDate = RequestHelper.GetString("ActualLeaveDate");
            //string OperaName_Leave = RequestHelper.GetString("OperaName_Leave");
            //string OperaTime_Leave = RequestHelper.GetString("OperaTime_Leave");
            //-----------------------------------------------------------------------------
            
            //-----------------------------------------------------------------------------
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.repair_Intention model = new SCZM.Model.Repair.repair_Intention();
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
            model.ID = Utils.StrToInt(ID, 0);
            //--------------------------客户信息------------------------
            if (IntentionCode == "") {
                IntentionCode = "BX" + DateTime.Now.ToString("yyyyMMdd") + bll.GetMaxId();
            }
            model.IntentionCode = IntentionCode;
            if (IntentionDate != "")
            {
                model.IntentionDate = Utils.StrToDateTime(IntentionDate);
            }
            model.IntentionType = Utils.StrToInt(IntentionType, 0);
            model.IntentionCode_Last = IntentionCode_Last;
            model.BusinessDepId = Utils.StrToInt(BusinessDepId, 0);
            model.BusinessPerName = BusinessPerName;
            model.CustTypeId = Utils.StrToInt(CustTypeId, 0);
            model.CustName = CustName;
            model.MachineModelId = Utils.StrToInt(MachineModelId, 0);
            model.MachineCode = MachineCode;
            model.SMR = Utils.StrToInt(SMR, 0);

            model.Linkman = Linkman;
            model.LinkPhone = LinkPhone;
            model.MachineAdress = MachineAdress;
            model.Machine = Machine;
            model.MachineStatus = MachineStatus;
            //------------------------------------------------------------
            model.FlagResult = Utils.StrToInt(FlagResult, 0);
            if (model.FlagResult != 0)
            {
                //----------------客户意见：1,2------------------------------------------------
                string CustOpinion = RequestHelper.GetString("CustOpinion");
                model.CustOpinion = CustOpinion;
                if (model.FlagResult ==1)
                {
                    //-----------------------1--------------------------------------
                    string RepairMode = RequestHelper.GetString("RepairMode");
                    string FlagLocale = RequestHelper.GetString("FlagLocale");
                    string RepairTypeId = RequestHelper.GetString("RepairTypeId");
                    string RepairContent = RequestHelper.GetString("RepairContent");

                    string EngineModel = RequestHelper.GetString("EngineModel");
                    string EngineCode = RequestHelper.GetString("EngineCode");
                    string FlagFXGCH = RequestHelper.GetString("FlagFXGCH");

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
                    string RepairAdress = RequestHelper.GetString("RepairAdress");
                    string ExpectEnterDate = RequestHelper.GetString("ExpectEnterDate");
                    string ExpectLeaveDate = RequestHelper.GetString("ExpectLeaveDate");
                    string ExpectTimeFee = RequestHelper.GetString("ExpectTimeFee");
                    string ExpectPartFee = RequestHelper.GetString("ExpectPartFee");
                    string ExpectFee = RequestHelper.GetString("ExpectFee");
                    string detailStr = RequestHelper.GetString("detailStr");
                    //----------------------1-----------------------------------
                    model.RepairMode = Utils.StrToInt(RepairMode, 1);
                    model.FlagLocale = Utils.StrToInt(FlagLocale, 0);
                    model.RepairTypeId = Utils.StrToInt(RepairTypeId, 0);
                    model.RepairContent = RepairContent;

                    model.EngineModel = EngineModel;
                    model.EngineCode = EngineCode;
                    model.FlagFXGCH = Utils.StrToInt(FlagFXGCH, 0);

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
                    model.RepairAdress = RepairAdress;
                    if (ExpectEnterDate != "")
                    {
                        model.ExpectEnterDate = Utils.StrToDateTime(ExpectEnterDate);
                    }
                    if (ExpectLeaveDate != "")
                    {
                        model.ExpectLeaveDate = Utils.StrToDateTime(ExpectLeaveDate);
                    }
                    model.ExpectTimeFee = Utils.StrToDecimal(ExpectTimeFee, 0);
                    model.ExpectPartFee = Utils.StrToDecimal(ExpectPartFee, 0);
                    model.ExpectFee = Utils.StrToDecimal(ExpectFee, 0);
                    //--------------------------------维修方案-子表-------------------
                    List<Model.Repair.repair_Intention_Package> modelsList = new List<Model.Repair.repair_Intention_Package>();
                    Model.Repair.repair_Intention_Package models = new Model.Repair.repair_Intention_Package();
                    if (detailStr != "")
                    {
                        string[] detailArray = detailStr.Split('≮');

                        for (int i = 0; i < detailArray.Length; i++)
                        {
                            string[] detailMXArray = detailArray[i].Split('⊥');
                            models = new Model.Repair.repair_Intention_Package();
                            models.PackageId= Utils.StrToInt(detailMXArray[0],0);
                            models.PackageNum = Utils.StrToInt(detailMXArray[1], 0);
                            modelsList.Add(models);
                        }
                    }
                    model.repair_Intention_Packages = modelsList;
                    //-------------------维修协议---------------------------------------
                    
                    string FlagAgreement = RequestHelper.GetString("FlagAgreement");
                    model.FlagAgreement = Utils.StrToInt(FlagAgreement, 0);
                    if (model.FlagAgreement == 1)
                    {
                        //------------------------1----------------------------------------------------
                        string AgreementDate = RequestHelper.GetString("AgreementDate");
                        string AttachmentId_Agreement = RequestHelper.GetString("AttachmentId_Agreement");
                        //------------------------end--------------------------------------------------
                        if (AgreementDate != "")
                        {
                            model.AgreementDate = Utils.StrToDateTime(AgreementDate);
                        }
                        model.AttachmentId_Agreement = AttachmentId_Agreement;
                    }
                }
            }
            //------------------10、客户谈判-----------------------------------------
            model.RepairState = 10;
            //现场维修跳过登记入厂 内部客户跳过
            if (model.RepairAdress == "现场"||model.CustTypeId==2) {
                model.RepairState = 20;
            }
            model.OperaDepId = loginUserModel.DepId;
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;
            //--------------------出入厂信息-------------------------------------
            //if (ActualEnterDate != "")
            //{
            //    model.ActualEnterDate = Utils.StrToDateTime(ActualEnterDate);
            //}
            //model.OperaName_Enter = OperaName_Enter;
            //if (OperaTime_Enter != "")
            //{
            //    model.OperaTime_Enter = Utils.StrToDateTime(OperaTime_Enter);
            //}
            //if (ActualLeaveDate != "")
            //{
            //    model.ActualLeaveDate = Utils.StrToDateTime(ActualLeaveDate);
            //}
            //model.OperaName_Leave = OperaName_Leave;
            //if (OperaTime_Leave != "")
            //{
            //    model.OperaTime_Leave = Utils.StrToDateTime(OperaTime_Leave);
            //}

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
                        operaMemo = "新增维修意向：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改维修意向：" + model.ID;
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

        #region 删除维修意向信息==============================;
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
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
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
                    operaMemo = "删除维修意向：" + IDStr;
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
        #region 获取入厂登记列表==============================;
        private void GetList_Enter(HttpContext context, string btn)
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
                string ActualEnterDate_Start = RequestHelper.GetString("ActualEnterDate_Start").Trim();
                string ActualEnterDate_End = RequestHelper.GetString("ActualEnterDate_End").Trim();
                string FlagRecord = RequestHelper.GetString("FlagRecord");
                StringBuilder strWhere = new StringBuilder();
                //strWhere.Append(" and a.FlagResult=1 and (a.RepairState=10 or (a.RepairState=20 and a.CustTypeId=1 and a.RepairAdress!='现场'))");

                //--------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------

                strWhere.Append(" and a.FlagResult=1 and (a.CustTypeId=1 and a.RepairAdress!='现场') ");
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
                    strWhere.Append(" and b.MachineModel like '%" + Utils.Filter(MachineModel)+"%' ");
                }
                //机号
                if (MachineCode != "" )
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                ////报修日期
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
                ////业务部门
                //if (BusinessDepId != "")
                //{
                //    strWhere.Append(" and a.BusinessDepId=" + Utils.StrToInt(BusinessDepId, 0));
                //}
                if (FlagRecord != "" && FlagRecord != "0")
                {
                    if (FlagRecord == "1")
                    {
                        strWhere.Append(" and a.ActualEnterDate is not null ");
                    }
                    else if (FlagRecord == "2")
                    {
                        strWhere.Append(" and a.ActualEnterDate is null ");
                    }
                }
                if (FlagRecord == "0" || FlagRecord == "1")
                {
                    //入厂日期
                    if (ActualEnterDate_Start != "")
                    {
                        strWhere.Append(" and a.ActualEnterDate>= cast('" + Utils.StrToDateTime(ActualEnterDate_Start).ToString() + "' as datetime)");
                    }
                    if (ActualEnterDate_End != "")
                    {
                        strWhere.Append(" and a.ActualEnterDate<=cast('" + Utils.StrToDateTime(ActualEnterDate_End + " 23:59:59").ToString() + "' as datetime)");
                    }
                }

                
                SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
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
        #region 登记入厂信息
        private void SaveActualEnterDate(HttpContext context, string btn)
        {
            if (btn != "btnEnter")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string IDStr = RequestHelper.GetString("IDStr");
            
            if (IDStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要入厂登记的记录！\"}");
                return;
            }
            string actualEnterDate = RequestHelper.GetString("ActualEnterDate").Trim();
            DateTime ActualEnterDate=new DateTime();
            if (actualEnterDate == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择入厂登记的时间！\"}");
                return;
            }
            else {
                ActualEnterDate = Utils.StrToDateTime(actualEnterDate);
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.SaveActualEnterDate(PageValidate.SafeLongFilter(IDStr, 0),ActualEnterDate,loginUserModel.PerName,out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "入场登记：" + IDStr;
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
        #region 取消登记入厂信息
        private void CancelActualEnterDate(HttpContext context, string btn)
        {
            if (btn != "btnCancelEnter")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string IDStr = RequestHelper.GetString("IDStr");
            if (IDStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要入厂登记的记录！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.CancelActualEnterDate(PageValidate.SafeLongFilter(IDStr, 0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "取消入场登记：" + IDStr;
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
        #region 获取维修协议列表==============================;
        private void GetList_Agreement(HttpContext context, string btn)
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
                string FlagAgreement = RequestHelper.GetString("FlagAgreement").Trim();
                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------

                //strWhere.Append(" and a.FlagResult=1 and (a.RepairState=10 or (a.RepairState=20 and a.RepairAdress!='现场'))");
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
                    strWhere.Append(" and b.MachineModel like '%" + Utils.Filter(MachineModel)+"%' ");
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
                //维修协议 0未上传 1已上传
                if (FlagAgreement != "") {
                    if (FlagAgreement == "2") {
                        strWhere.Append(" and a.AttachmentId_Agreement is null ");
                    }
                    else if (FlagAgreement == "1") {
                        strWhere.Append(" and a.AttachmentId_Agreement is not null ");
                    }
                }
                SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
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
        #region 上传维修协议信息
        private void SaveAgreement(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            if (ID == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要上传协议的记录！\"}");
                return;
            }
            string AttachmentId_Agreement = RequestHelper.GetString("AttachmentId_Agreement").Trim();
            if (AttachmentId_Agreement == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请上传协议！\"}");
                return;
            }
            string AgreementDate = RequestHelper.GetString("AgreementDate").Trim();
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.SaveAgreement(Utils.StrToInt(ID,0),Utils.Filter(AttachmentId_Agreement),Utils.StrToDateTime(AgreementDate,new DateTime()), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "上传协议：" + ID;
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
        #region 获取出厂登记列表==============================;
        private void GetList_Leave(HttpContext context, string btn)
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
                string ActualLeaveDate_Start = RequestHelper.GetString("ActualLeaveDate_Start").Trim();
                string ActualLeaveDate_End = RequestHelper.GetString("ActualLeaveDate_End").Trim();
                StringBuilder strWhere = new StringBuilder();

                //--------------------------------------------------------------
                //Model.System.sys_LoginUser LoginUserModel = BaseWeb.GetLoginInfo();
                //if (LoginUserModel.IsAdmin == false)
                //{
                //    strWhere.Append(" and a.OperaId in (select CtrlPerId from v_sys_PersonCtrl where PerId=" + LoginUserModel.ID + ") ");
                //}
                //-------------------------------------------------------------------------

                //strWhere.Append(" and a.FlagResult=1 and (a.RepairState=10 or (a.RepairState=20 and a.CustTypeId=1 and a.RepairAdress!='现场'))");
                strWhere.Append(" and a.FlagResult=1 and (a.CustTypeId=1 and a.RepairAdress!='现场')");
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
                    strWhere.Append(" and b.MachineModel like '%" + Utils.StrToInt(MachineModel, 0)+"%' ");
                }
                //机号
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + Utils.Filter(MachineCode) + "%'");
                }
                //维修类型
                if (RepairTypeId != "")
                {
                    strWhere.Append(" and a.RepairTypeId=" + Utils.StrToInt(RepairTypeId, 0));
                }
                ////业务部门
                //if (BusinessDepId != "")
                //{
                //    strWhere.Append(" and a.BusinessDepId=" + Utils.StrToInt(BusinessDepId, 0));
                //}
                //出厂日期
                if (ActualLeaveDate_Start != "")
                {
                    strWhere.Append(" and a.ActualLeaveDate>= cast('" + Utils.StrToDateTime(ActualLeaveDate_Start).ToString() + "' as datetime)");
                }
                if (ActualLeaveDate_End != "")
                {
                    strWhere.Append(" and a.ActualLeaveDate<=cast('" + Utils.StrToDateTime(ActualLeaveDate_End + " 23:59:59").ToString() + "' as datetime)");
                }
                SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
                DataTable dt = bll.GetList_Leave(strWhere.ToString()).Tables[0];
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
        #region 登记出厂信息
        private void SaveActualLeaveDate(HttpContext context, string btn)
        {
            if (btn != "btnLeave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string IDStr = RequestHelper.GetString("IDStr");
            if (IDStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要出厂登记的记录！\"}");
                return;
            }
            string actualLeaveDate = RequestHelper.GetString("ActualLeaveDate").Trim();
            DateTime ActualLeaveDate = new DateTime();
            if (actualLeaveDate == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择出厂登记的时间！\"}");
                return;
            }
            else
            {
                ActualLeaveDate = Utils.StrToDateTime(actualLeaveDate);
            }
            int LeaveTypeId =Utils.StrToInt(RequestHelper.GetString("LeaveTypeId").Trim(),0);
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.SaveActualLeaveDate(PageValidate.SafeLongFilter(IDStr, 0), ActualLeaveDate,LeaveTypeId, loginUserModel.PerName, out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "入场登记：" + IDStr;
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
        #region 取消登记出厂信息
        private void CancelActualLeaveDate(HttpContext context, string btn)
        {
            if (btn != "btnCancelLeave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string IDStr = RequestHelper.GetString("IDStr");
            if (IDStr == "")
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"请选择需要出厂登记的记录！\"}");
                return;
            }
            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Repair.repair_Intention bll = new SCZM.BLL.Repair.repair_Intention();
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (bll.CancelActualLeaveDate(PageValidate.SafeLongFilter(IDStr, 0), out operaMessage))
                {
                    status = "1";
                    operaAction = Enums.ActionEnum.Delete.ToString();
                    operaMemo = "取消入场登记：" + IDStr;
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

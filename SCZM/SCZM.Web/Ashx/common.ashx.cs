using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using SCZM.Common;
//using SCZM.BLL;
using SCZM.WebUI;

namespace SCZM.Web.Ashx
{
    /// <summary>
    /// common 的摘要说明



    /// </summary>
    public class common : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string btn = RequestHelper.GetQueryString("Btn");
            string error = BaseWeb.CheckUserBtnPower();
            if (error != "")
            {
                context.Response.Write("[]");
                return;
            }
            //取得处事类型
            string action = RequestHelper.GetQueryString("action");

            switch (action)
            {
                case "GetDepTree":
                    GetDepTree(context, btn);//部门树

                    break;
                case "GePerTree":
                    GePerTree(context, btn);//人员树

                    break;
                case "GetRoleCombobox":
                    GetRoleCombobox(context, btn);//角色下拉框

                    break;
                case "GetPostCombobox":
                    GetPostCombobox(context, btn);//岗位下拉框

                    break;
                case "GetDepTypeCombobox":
                    GetDepTypeCombobox(context, btn);//部门类别下拉框

                    break;
                case "GetMenuTree":
                    GetMenuTree(context, btn);//菜单树 有根节点
                    break;
                case "GetMenuTreeNoRoot":
                    GetMenuTreeNoRoot(context, btn);//菜单树无根节点

                    break;
                case "GetBillTree":
                    GetBillTree(context, btn);//审批模块树 有根节点
                    break;
                case "GetProcGrid":
                    GetProcGrid(context, btn);//审批ComboGrid
                    break;
                case "GetPersonCombo":
                    GetPersonCombo(context, btn);//人员下拉框

                    break;
                case "GetProcessClassCombo"://工序大类
                    GetProcessClassCombo(context, btn);
                    break;
                case "GetMachineModelCombo"://机型
                    GetMachineModelCombo(context, btn);
                    break;
                case "GetRepairTypeCombo"://维修类型
                    GetRepairTypeCombo(context, btn);
                    break;
                case "GetRepairPackageCombo"://维修套包
                    GetRepairPackageCombo(context, btn);
                    break;
                case "GetRepairIntentionCombo"://维修意向号下拉框
                    GetRepairIntentionCombo(context, btn);
                    break;
                case "GetRepairItemCombo"://维修项目
                    GetRepairItemCombo(context, btn);
                    break;
                case "GetRepairProcessCombo":
                    GetRepairProcessCombo(context, btn);
                    break;
                case "GetRepairAssignmentByIntention":
                    GetRepairAssignmentByIntention(context, btn);
                    break;
                case "GetRepairAssignmentCombo"://维修意向号下拉框
                    GetRepairAssignmentCombo(context, btn);
                    break;
                case "GetRepairProcessByAssignment"://维修工序--通过维修意向号

                    GetRepairProcessByAssignment(context, btn);
                    break;
                case "GetFeeItemCombo":
                    GetFeeItemCombo(context, btn);
                    break;
                case "GetSettlementTypeCombo":
                    GetSettlementTypeCombo(context, btn);
                    break;
                case "GetPayTypeCombo":
                    GetPayTypeCombo(context, btn);
                    break;
                case "GetIntentionCode_LastCombo":
                    GetIntentionCode_LastCombo(context, btn);
                    break;
                case "GetCustomerTypeCombo":
                    GetCustomerTypeCombo(context, btn);
                    break;
                case "GetAccessoriesCombo":
                    GetAccessoriesCombo(context, btn);
                    break;
                case "GetProcedureComboTree":
                    GetProcedureComboTree(context, btn);
                    break;
                case "GetProcedureComboTree_Base":
                    GetProcedureComboTree_Base(context, btn);
                    break;
                case "GetDepTree_Repair":
                    GetDepTree_Repair(context, btn);
                    break;
                case "GetCustomCombogrid":
                    GetCustomCombogrid(context, btn);
                    break;
            }
        }
        #region 部门树



        private void GetDepTree(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("[]");
                return;
            }
            try
            {
                int depId = RequestHelper.GetInt("id", 1);
                string treeJsonStr = "[]";
                BLL.System.sys_Department bll = new BLL.System.sys_Department();
                DataTable dt = bll.GetListByCache_sys_Department().Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    treeJsonStr = DtToTreeJson(dt, "ID", "DepName", "SupId", "SortId", "0", "0");
                }
                context.Response.Write(treeJsonStr);
            }
            catch
            {
                context.Response.Write("[]");
            }
        }
        #endregion
        #region 人员树



        private void GePerTree(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("[]");
                return;
            }
            try
            {
                string treeJsonStr = "[]";
                BLL.System.sys_Person bll = new BLL.System.sys_Person();
                DataTable dt = bll.GetDepPersonList().Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    treeJsonStr = DtToTreeJson(dt, "ID", "Name", "SupId", "SortId", "B0", "B0");
                }
                context.Response.Write(treeJsonStr);
            }
            catch
            {
                context.Response.Write("[]");
            }
        }
        #endregion
        #region 角色下拉框


        private void GetRoleCombobox(HttpContext context, string btn)
        {
            string emptyStr = "[{\"ID\":\"0\",\"RoleName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.System.sys_Role bll = new BLL.System.sys_Role();
                DataTable dt = bll.GetAllList().Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }

            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 岗位下拉框



        private void GetPostCombobox(HttpContext context, string btn)
        {
            string emptyStr = "[{\"ID\":\"0\",\"PostName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.System.sys_Post bll = new BLL.System.sys_Post();
                DataTable dt = bll.GetAllList().Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 部门类别下拉框



        private void GetDepTypeCombobox(HttpContext context, string btn)
        {
            string emptyStr = "[{\"ID\":\"0\",\"DepTypeName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.System.sys_Department bll = new BLL.System.sys_Department();
                DataTable dt = bll.GetDepTypeList().Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 菜单树 有根节点
        private void GetMenuTree(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("[]");
                return;
            }
            try
            {
                string treeJsonStr = "[{\"id\":0,\"text\":\"根菜单\",\"children\":";
                BLL.System.sys_Menu bll = new BLL.System.sys_Menu();
                DataTable dt = bll.GetList("").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    treeJsonStr += DtToTreeJson(dt, "ID", "MenuName", "SupId", "SortId", "0", "0");
                }
                else
                {
                    treeJsonStr += "\"\"";
                }
                treeJsonStr += "}]";
                context.Response.Write(treeJsonStr);

            }
            catch
            {
                context.Response.Write("[]");
            }
        }
        #endregion
        #region 菜单树无根节点



        private void GetMenuTreeNoRoot(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("");
                return;
            }
            try
            {

                string treeJsonStr = "{}";
                BLL.System.sys_Menu bll = new BLL.System.sys_Menu();
                DataTable dt = bll.GetList("").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    treeJsonStr = DtToTreeJson(dt, "ID", "MenuName", "SupId", "SortId", "0", "0");
                }
                context.Response.Write(treeJsonStr);
            }
            catch
            {
                context.Response.Write("{}");
            }
        }
        #endregion
        #region 审批模块树 有根节点
        private void GetBillTree(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{}");
                return;
            }
            try
            {
                string treeJsonStr = "[{\"id\":0,\"text\":\"根模块\",\"children\":";
                BLL.System.sys_Process_BillSet bll = new BLL.System.sys_Process_BillSet();
                DataTable dt = bll.GetList("").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    treeJsonStr += DtToTreeJson(dt, "ID", "BillName", "SupId", "SortId", "0", "0");
                }
                else
                {
                    treeJsonStr += "\"\"";
                }
                treeJsonStr += "}]";
                context.Response.Write(treeJsonStr);

            }
            catch
            {
                context.Response.Write("{}");
            }
        }
        #endregion
        #region 审批流ComboGrid==============================
        private void GetProcGrid(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{}");
                return;
            }
            try
            {


                BLL.System.sys_Process bll = new BLL.System.sys_Process();


                DataTable dt = bll.GetUseList("").Tables[0];
                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"total\":" + dt.Rows.Count + ",\"rows\":");
                jsonStr.Append(rowsStr);
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
        #region 人员下拉框

        private void GetPersonCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"PerId\":\"0\",\"PerName\":\"无\",\"DepName\":\"无\",\"DepId\":\"0\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {

                BLL.System.sys_Person bll = new BLL.System.sys_Person();
                string DepId = RequestHelper.GetString("DepId");
                string PostId = RequestHelper.GetString("PostId");
                string FlagCtrl = RequestHelper.GetString("FlagCtrl");
                string PerName = RequestHelper.GetString("q").Trim();
                StringBuilder strWhere = new StringBuilder();
                if (DepId != "")
                {
                    strWhere.Append(" and a.DepId in(select ID from sys_Department where ','+SupList like '%," + DepId + ",%' or ID=" + DepId + ")");
                }
                if (PostId != "")
                {
                    strWhere.Append(" and a.PostId in(" + PostId + ")");
                }
                if (FlagCtrl == "1")
                {
                    Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
                    strWhere.Append(" and a.ID in(select CtrlPerId from v_sys_PersonCtrl where PerId=" + loginUserModel.ID.ToString() + ")");
                }
                if (PerName != "")
                {
                    strWhere.Append(" and a.PerName like '%" + PerName + "%' ");
                }
                DataTable dt = bll.GetComboList(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                context.Response.Write(rowsStr);
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 根据DT生成tree json
        /// <summary>
        /// 根据DT生成tree json，默认第一级展开，其他级关闭
        /// </summary>
        /// <param name="dt">数据集 非空</param>
        /// <param name="fieldId">字段名-id</param>
        /// <param name="fieldText">字段名-txt</param>
        /// <param name="fieldSupId">字段名-上级id</param>
        /// <param name="fieldSupId">字段名-排序字段</param>
        /// <param name="nodeId">本次需要显示下级的id</param>
        /// <param name="nodeBeginId">开始显示的id</param>
        /// <returns></returns>
        private string DtToTreeJson(DataTable treeDT, string fieldId, string fieldText, string fieldSupId, string fieldSort, string nodeId, string nodeBeginId)
        {
            StringBuilder treeJsonStr = new StringBuilder();
            string stateOpen = "closed";//打开标记
            int rowNum = 0;
            if (nodeId == nodeBeginId)
            {
                stateOpen = "open";
            }

            DataRow[] drs = treeDT.Select(fieldSupId + "='" + nodeId + "'", fieldSort + " asc");
            if (drs.Length != 0)
            {
                treeJsonStr.Append("[");
                foreach (DataRow dr in drs)
                {
                    //第一行以“{”开始，其他行以“,{”开始



                    if (rowNum == 0)
                    {
                        treeJsonStr.Append("{");
                    }
                    else
                    {
                        treeJsonStr.Append(",{");
                    }
                    //判断是否有下级，有则递归调用
                    if (treeDT.Select(fieldSupId + "='" + dr[fieldId].ToString() + "'").Length != 0)
                    {
                        treeJsonStr.Append("\"id\":\"" + dr[fieldId].ToString() + "\",\"text\":\"" + dr[fieldText] + "\",\"state\":\"" + stateOpen + "\",\"children\":");
                        treeJsonStr.Append(DtToTreeJson(treeDT, fieldId, fieldText, fieldSupId, fieldSort, Utils.ObjectToStr(dr[fieldId]), nodeBeginId));
                    }
                    else
                    {
                        treeJsonStr.Append("\"id\":\"" + dr[fieldId].ToString() + "\",\"text\":\"" + dr[fieldText] + "\"");
                    }
                    treeJsonStr.Append("}");
                    rowNum += 1;
                }
                treeJsonStr.Append("]");
            }

            return treeJsonStr.ToString();
        }
        private string DtToTreeJson(DataTable treeDT, string fieldId, string fieldText, string fieldSupId, string fieldSort, string nodeId, string nodeBeginId, string[] extendField)
        {
            StringBuilder treeJsonStr = new StringBuilder();
            string stateOpen = "closed";//打开标记
            int rowNum = 0;
            if (nodeId == nodeBeginId)
            {
                stateOpen = "open";
            }

            DataRow[] drs = treeDT.Select(fieldSupId + "='" + nodeId + "'", fieldSort + " asc");
            if (drs.Length != 0)
            {
                treeJsonStr.Append("[");
                foreach (DataRow dr in drs)
                {
                    //第一行以“{”开始，其他行以“,{”开始



                    if (rowNum == 0)
                    {
                        treeJsonStr.Append("{");
                    }
                    else
                    {
                        treeJsonStr.Append(",{");
                    }
                    if (extendField.Length > 0)
                    {
                        treeJsonStr.Append("\"attributes\":{");
                        foreach (var field in extendField)
                        {
                            treeJsonStr.Append("\"" + field + "\":\"" + dr[field].ToString() + "\",");
                        }
                        treeJsonStr.Remove(treeJsonStr.Length - 1, 1);
                        treeJsonStr.Append("},");
                    }
                    //判断是否有下级，有则递归调用
                    if (treeDT.Select(fieldSupId + "='" + dr[fieldId].ToString() + "'").Length != 0)
                    {
                        treeJsonStr.Append("\"id\":\"" + dr[fieldId].ToString() + "\",\"text\":\"" + dr[fieldText] + "\",\"state\":\"" + stateOpen + "\",\"children\":");
                        treeJsonStr.Append(DtToTreeJson(treeDT, fieldId, fieldText, fieldSupId, fieldSort, Utils.ObjectToStr(dr[fieldId]), nodeBeginId, extendField));
                    }
                    else
                    {
                        treeJsonStr.Append("\"id\":\"" + dr[fieldId].ToString() + "\",\"text\":\"" + dr[fieldText] + "\"");
                    }
                    treeJsonStr.Append("}");
                    rowNum += 1;
                }
                treeJsonStr.Append("]");
            }

            return treeJsonStr.ToString();
        }
        #endregion
        #region 工序大类下拉框


        private void GetProcessClassCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"ProcessClassId\":\"0\",\"ProcessClassName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Base.base_ProcessClass bll = new BLL.Base.base_ProcessClass();
                DataTable dt = bll.GetComboList("").Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 机型下拉框


        private void GetMachineModelCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"MachineModelId\":\"0\",\"MachineModelName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Base.base_MachineModel bll = new BLL.Base.base_MachineModel();
                StringBuilder strWhere = new StringBuilder();
                string MachineModelName = RequestHelper.GetString("q").Trim();
                if (MachineModelName != "")
                {
                    strWhere.Append(" and a.MachineModel like '%" + MachineModelName + "%' ");
                }
                DataTable dt = bll.GetComboList(strWhere.ToString()).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 维修类型下拉框


        private void GetRepairTypeCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"RepairTypeId\":\"0\",\"RepairTypeName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Base.base_RepairType bll = new BLL.Base.base_RepairType();
                DataTable dt = bll.GetComboList("").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 维修套包下拉框



        private void GetRepairPackageCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"PackageId\":\"0\",\"MachineModelId\":\"0\",\"MachineModel\":\"无\",\"PackageName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                string strWhere = "";
                string MachineModelId = RequestHelper.GetString("MachineModelId").Trim();
                if (MachineModelId != "")
                {
                    strWhere = " and a.MachineModelId=" + Utils.StrToInt(MachineModelId, 0);
                }
                BLL.Base.base_RepairPackage bll = new BLL.Base.base_RepairPackage();
                DataTable dt = bll.GetComboList(strWhere).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 维修意向号下拉框

        private void GetRepairIntentionCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"IntentionId\":\"0\",\"IntentionCode\":\"无\",\"CustName\":\"无\",\"MachineModel\":\"无\",\"MachineCode\":\"无\",\"MachineStatus\":\"无\",\"CustOpinion\":\"无\",\"RepairContent\":\"无\",\"FlagENGKC\":\"无\",\"FlagPPMKC\":\"无\",\"FlagENG\":\"无\",\"FlagPPM\":\"无\",\"FlagMVC\":\"无\",\"FlagELE\":\"无\",\"FlagVM\":\"无\",\"FlagRM\":\"无\",\"FlagSM\":\"无\",\"FlagUM\":\"无\",\"FlagVR\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                //string IntentionCode = RequestHelper.GetString("q");
                string filter = RequestHelper.GetString("q");
                string RepairState = RequestHelper.GetString("RepairState");
                string FlagContract = RequestHelper.GetString("FlagContract");
                string FlagSettlementList = RequestHelper.GetString("FlagSettlementList");
                string FlagResult = RequestHelper.GetString("FlagResult");
                StringBuilder strWhere = new StringBuilder();
                //if (IntentionCode != "" && Utils.IsSafeSqlString(IntentionCode))
                //{
                //    strWhere.Append(" and a.IntentionCode like '%" + Utils.Filter(IntentionCode) + "%'");
                //}
                if (filter != "" && Utils.IsSafeSqlString(filter))
                {
                    strWhere.Append(" and (a.CustName like '%" + Utils.Filter(filter) + "%' or a.MachineCode like '%" + Utils.Filter(filter) + "%' or b.MachineModel like '%" + Utils.Filter(filter) + "%') ");
                }
                if (RepairState != "")
                {
                    strWhere.Append(" and a.RepairState in (" + Utils.Filter(RepairState) + ") ");
                }
                if (FlagContract == "1")
                {
                    strWhere.Append(" and not exists(select ID from repair_Contract aa where aa.FlagDel=0 and a.ID=aa.IntentionId)");
                }
                if (FlagSettlementList == "1")
                {
                    strWhere.Append(" and not exists(select ID from repair_SettlementList bb where bb.FlagDel=0 and a.ID=bb.IntentionId)");
                }
                if (FlagResult != "")
                {
                    strWhere.Append(" and a.FlagResult=" + Utils.StrToInt(FlagResult, 1));
                }
                BLL.Repair.repair_Intention bll = new BLL.Repair.repair_Intention();
                DataTable dt = bll.GetComboList(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                context.Response.Write(rowsStr);
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 维修项目下拉框



        private void GetRepairItemCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"ItemId\":\"0\",\"ItemName\":\"无\",\"ItemNat\":\"0\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                string ItemName = RequestHelper.GetString("q").Trim();
                if (ItemName != "")
                {
                    strSql.Append(" and a.ItemName like '%" + ItemName + "%'");
                }
                BLL.Base.base_RepairItem bll = new BLL.Base.base_RepairItem();
                DataTable dt = bll.GetComboList(strSql.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 工序下拉框


        private void GetRepairProcessCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"ProcessId\":\"0\",\"ProcessName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                StringBuilder strWhere = new StringBuilder();
                BLL.Base.base_Process bll = new BLL.Base.base_Process();
                string ProcessName = RequestHelper.GetString("q");
                if (ProcessName != "")
                {
                    strWhere.Append(" and b.ProcessClassName+'——'+a.ProcessName like '%" + ProcessName + "%' ");
                }
                string AssignmentIdList = RequestHelper.GetString("AssignmentIdList");
                if (AssignmentIdList != "")
                {
                    string AssignmentId = RequestHelper.GetString("AssignmentId");
                    List<string> list = new List<string>();
                    list = AssignmentIdList.Split(',').ToList();
                    if (AssignmentId != "")
                    {
                        list.Remove(AssignmentId);
                    }
                    string list2 = String.Join(",", list);
                    if (list2 != "")
                    {
                        StringBuilder strWhere2 = new StringBuilder();
                        strWhere2.Append(" and a.AssignmentId in (" + list2 + ") ");
                        BLL.Repair.Repair_Assignment bll2 = new BLL.Repair.Repair_Assignment();
                        DataTable dt2 = bll2.GetProcessComboList(strWhere2.ToString()).Tables[0];
                        string ProcessIdList = "";
                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                ProcessIdList += dt2.Rows[i]["ProcessId"].ToString() + ",";
                            }
                            ProcessIdList = ProcessIdList.Remove(ProcessIdList.Length - 1);
                            if (ProcessIdList != "")
                            {
                                strWhere.Append(" and a.ID not in (" + ProcessIdList + ") ");
                            }
                        }

                    }
                }

                DataTable dt = bll.GetComboList(strWhere.ToString()).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 维修派工--通过维修意向id
        public void GetRepairAssignmentByIntention(HttpContext context, string btn)
        {
            string emptyStr = "[{\"AssigmentId\":\"0\",\"AssignmentCode\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                string IntentionId = RequestHelper.GetString("IntentionId");
                BLL.Repair.Repair_Assignment bll = new BLL.Repair.Repair_Assignment();
                string strWhere = " and a.IntentionId=" + Utils.StrToInt(IntentionId, 0);
                DataTable dt = bll.GetList(strWhere).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                context.Response.Write(rowsStr);
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 维修派工下拉框


        private void GetRepairAssignmentCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"AssignmentId\":\"0\",\"AssignmentCode\":\"无\",\"IntentionCode\":\"无\",\"IntentionCode\":\"无\",\"CustName\":\"无\",\"MachineModel\":\"无\",\"MachineCode\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                string AssignmentCode = RequestHelper.GetString("q");

                StringBuilder strWhere = new StringBuilder();
                if (AssignmentCode != "" && Utils.IsSafeSqlString(AssignmentCode))
                {
                    strWhere.Append(" and a.AssignmentCode like '%" + Utils.Filter(AssignmentCode) + "%'");
                }

                BLL.Repair.Repair_Assignment bll = new BLL.Repair.Repair_Assignment();
                DataTable dt = bll.GetComboList(strWhere.ToString()).Tables[0];
                string rowsStr = Utils.ToJson(dt);
                context.Response.Write(rowsStr);
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        #region 维修工序--通过派工单号
        public void GetRepairProcessByAssignment(HttpContext context, string btn)
        {
            string emptyStr = "[{\"ProcessId\":\"0\",\"ProcessName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Repair.Repair_Assignment bll = new BLL.Repair.Repair_Assignment();
                string AssignmentId = RequestHelper.GetString("AssignmentId");
                StringBuilder strWhere = new StringBuilder();
                if (AssignmentId != "")
                {
                    strWhere.Append(" and a.AssignmentId=" + Utils.StrToInt(AssignmentId, 0));
                }
                DataTable dt = bll.GetProcessComboList(strWhere.ToString()).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        public void GetFeeItemCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"FeeItemId\":\"0\",\"FeeItemName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Base.base_FeeItem bll = new BLL.Base.base_FeeItem();
                StringBuilder strWhere = new StringBuilder();
                DataTable dt = bll.GetFeeItemCombo(strWhere.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        public void GetSettlementTypeCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"SettlementTypeId\":\"0\",\"SettlementName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Base.base_Settlement bll = new BLL.Base.base_Settlement();
                StringBuilder strWhere = new StringBuilder();
                string CustTypeId = RequestHelper.GetString("CustTypeId");
                if (CustTypeId != "")
                {
                    strWhere.Append(" and a.CustTypeId=" + Utils.StrToInt(CustTypeId, 0));
                }
                DataTable dt = bll.GetSettlementTypeCombo(strWhere.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        #endregion
        public void GetPayTypeCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"PayTypeId\":\"0\",\"PayTypeName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Base.base_PayType bll = new BLL.Base.base_PayType();
                StringBuilder strWhere = new StringBuilder();
                DataTable dt = bll.GetPayTypeCombo(strWhere.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        //GetIntentionCode_LastCombo
        public void GetIntentionCode_LastCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"ContractCode\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Repair.repair_Contract bll = new BLL.Repair.repair_Contract();
                StringBuilder strWhere = new StringBuilder();
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModelId = RequestHelper.GetString("MachineModelId");
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                if (CustName != "")
                {
                    strWhere.Append(" and b.CustName like '%" + Utils.Filter(CustName) + "%' ");
                }
                if (MachineModelId != "")
                {
                    strWhere.Append(" and b.MachineModelId=" + Utils.StrToInt(MachineModelId, 0));
                }
                if (MachineCode != "")
                {
                    strWhere.Append(" and b.MachineCode like '%" + MachineCode + "%' ");
                }
                DataTable dt = bll.GetIntentionCode_LastCombo(strWhere.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        public void GetCustomerTypeCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"PayTypeId\":\"0\",\"PayTypeName\":\"无\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Base.base_CustomerType bll = new BLL.Base.base_CustomerType();
                StringBuilder strWhere = new StringBuilder();
                DataTable dt = bll.GetCustomerTypeCombo(strWhere.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        public void GetAccessoriesCombo(HttpContext context, string btn)
        {
            string emptyStr = "[{\"AccessoriesId\":\"0\",\"AccessoriesName\":\"无\",\"AccessoriesUnit\":\"\",\"AccessoriesName--Unit\":\"\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyStr);
                return;
            }
            try
            {
                BLL.Base.base_Accessories bll = new BLL.Base.base_Accessories();

                StringBuilder strWhere = new StringBuilder();
                string AccessoriesName = RequestHelper.GetString("q");
                if (AccessoriesName != "")
                {
                    strWhere.Append(" and a.AccessoriesName like '%" + AccessoriesName + "%' ");
                }
                DataTable dt = bll.GetAccessoriesCombo(strWhere.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rowsStr = Utils.ToJson(dt);
                    context.Response.Write(rowsStr);
                }
                else
                {
                    context.Response.Write(emptyStr);
                }
            }
            catch
            {
                context.Response.Write(emptyStr);
            }
        }
        public void GetProcedureComboTree(HttpContext context, string btn)
        {
            string emptyComboTree = "[{\"id\":0,\"text\":\"\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyComboTree);
                return;
            }
            try
            {
                string MachineModel = RequestHelper.GetString("MachineModel");
                string NoHistory = RequestHelper.GetString("NoHistory").Trim();
                string AssignmentIdList = RequestHelper.GetString("AssignmentIdList").Trim();
                string IsCommon = RequestHelper.GetString("IsCommon").Trim();

                if (MachineModel == "")
                {
                    context.Response.Write(emptyComboTree);
                    return;
                }
                if (IsCommon == "1")
                {
                    AssignmentIdList = "";
                }
                BLL.Base.base_Procedure bll = new BLL.Base.base_Procedure();
                StringBuilder strWhere = new StringBuilder();
                string fields = "NumType,MachineLevelNat";
                string[] extendField = fields.Split(',');
                if (NoHistory != "")
                {
                    strWhere.Append(" and a.ID<>" + Utils.StrToInt(NoHistory, 0) + " and ','+a.SupList not like '%," + Utils.StrToInt(NoHistory, 0) + ",%' ");
                }
                if (AssignmentIdList != "")
                {
                    strWhere.Append(" and a.ID not in(select ProcedureId from Repair_Assignment_Procedure where FlagDel=0 and AssignmentId in(" + AssignmentIdList + "))");
                }
                //--------------
                strWhere.Append(" or a.ID in(179,176,177,178,165) ");
                //--------------
                DataSet ds = bll.GetList(strWhere.ToString(), MachineModel);
                DataTable dt = ds.Tables[0];
                string ProcedureComboTreeData = DtToTreeJson(dt, "ID", "ProcedureName", "SupId", "SupList", "0", "0", extendField);
                context.Response.Write(ProcedureComboTreeData);
            }
            catch
            {
                context.Response.Write(emptyComboTree);
            }
        }
        public void GetProcedureComboTree_Base(HttpContext context, string btn)
        {
            string emptyComboTree = "[{\"id\":0,\"text\":\"\"}]";
            if (btn != "show")
            {
                context.Response.Write(emptyComboTree);
                return;
            }
            try
            {
                BLL.Base.base_Procedure bll = new BLL.Base.base_Procedure();
                StringBuilder strWhere = new StringBuilder();
                string NoHistory = RequestHelper.GetString("NoHistory").Trim();
                if (NoHistory != "")
                {
                    strWhere.Append(" and a.ID<>" + Utils.StrToInt(NoHistory, 0) + " and ','+a.SupList not like '%," + Utils.StrToInt(NoHistory, 0) + ",%' ");
                }
                string fields = "NumType";
                string[] extendField = fields.Split(',');
                DataSet ds = bll.GetList(strWhere.ToString());
                DataTable dt = ds.Tables[0];
                string ProcedureComboTreeData = DtToTreeJson(dt, "ID", "ProcedureName", "SupId", "SupList", "0", "0", extendField);
                context.Response.Write(ProcedureComboTreeData);
            }
            catch
            {
                context.Response.Write(emptyComboTree);
            }
        }
        private void GetDepTree_Repair(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("[]");
                return;
            }
            try
            {
                string SupId = RequestHelper.GetString("SupId").Trim();
                string DepId = RequestHelper.GetString("DepId").Trim();
                StringBuilder strWhere = new StringBuilder();
                if (DepId != "")
                {
                    strWhere.Append(" (','+SupList like '%," + DepId + ",%' or ID=" + DepId + ") ");
                }
                string treeJsonStr = "[]";
                BLL.System.sys_Department bll = new BLL.System.sys_Department();
                DataTable dt = bll.GetList(strWhere.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    treeJsonStr = DtToTreeJson(dt, "ID", "DepName", "SupId", "SortId", SupId, SupId);
                }
                context.Response.Write(treeJsonStr);
            }
            catch
            {
                context.Response.Write("[]");
            }
        }
        private void GetCustomCombogrid(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("[]");
                return;
            }
            try
            {
                StringBuilder strWhere = new StringBuilder();
                string q = RequestHelper.GetString("q").Trim();
                if(q!=""){
                    strWhere.Append(" and (a.CustName like '%" + q + "%' or a.MachineModel like '%"+q+"%' or a.MachineCode like '%"+q+"%') ");
                }
                BLL.Base.base_CustomerInformation bll = new BLL.Base.base_CustomerInformation();
                DataSet ds = bll.GetComboList(strWhere.ToString());
                string rowStr = Utils.ToJson(ds.Tables[0]);
                context.Response.Write(rowStr);
            }
            catch
            {
                context.Response.Write("[]");
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
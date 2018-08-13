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
    /// repair_AccessoriesBill 的摘要说明
    /// <summary>
    public class repair_AccessoriesBill : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //获取辅料明细列表
                    GetList(context, btn);
                    break;
                case "GetDetail": //获取辅料明细明细
                    GetDetail(context, btn);
                    break;
                case "SaveData": //保存辅料明细信息
                    SaveData(context, btn);
                    break;
                case "DelData": //删除辅料明细信息
                    DelData(context, btn);
                    break;
            }
        }
        #region 获取辅料明细列表==============================;
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
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();
                string GetAccessoriesDate_Start = RequestHelper.GetString("GetAccessoriesDate_Start").Trim();
                string GetAccessoriesDate_End = RequestHelper.GetString("GetAccessoriesDate_End").Trim();
                string UserName = RequestHelper.GetString("UserName").Trim();
                string OperaTime = RequestHelper.GetString("OperaTime").Trim();
                string BillType = RequestHelper.GetString("BillType").Trim();
                
                if (IntentionCode != "") {
                    strWhere.Append(" and b.IntentionCode like '%"+IntentionCode+"%' ");
                }
                if (CustName != "") {
                    strWhere.Append(" and b.CustName like '%" + CustName + "%' ");
                }
                if (MachineModel != "")
                {
                    strWhere.Append(" and c.MachineModel like '%" + MachineModel + "%' ");
                }
                if (MachineCode != "")
                {
                    strWhere.Append(" and b.MachineCode like '%" + MachineCode + "%' ");
                }
                if (UserName != "")
                {
                    strWhere.Append(" and a.UserName like '%" + UserName + "%' ");
                }
                if (GetAccessoriesDate_Start != "") {
                    strWhere.Append(" and a.GetAccessoriesDate > '" + GetAccessoriesDate_Start+" 00:00:00' ");
                }
                if (GetAccessoriesDate_End != "")
                {
                    strWhere.Append(" and a.GetAccessoriesDate < '" + GetAccessoriesDate_End + " 23:59:59' ");
                }
                if (OperaTime != "") {
                    strWhere.Append(" and a.OperaTime between '" + OperaTime + " 00:00:00' and '"+OperaTime+" 23:59:59' ");
                }
                if (BillType != "" && BillType != "-1") {
                    strWhere.Append(" and a.BillType=" + Utils.StrToInt(BillType, -1) + " ");
                }
                SCZM.BLL.Repair.repair_AccessoriesBill bll = new SCZM.BLL.Repair.repair_AccessoriesBill();
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

        #region 获取辅料明细明细==============================;
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

                SCZM.BLL.Repair.repair_AccessoriesBill bll = new SCZM.BLL.Repair.repair_AccessoriesBill();
                DataSet ds = bll.GetDetail(ID);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，该条数据已被其他人删除！\"}");
                    return;
                }
                string rowsStr = Utils.ToJson(dt);
                DataTable AccessoriesBill_AccessoriesDT = ds.Tables["AccessoriesBill_Accessories"];
                string AccessoriesBill_AccessoriesStr = Utils.ToJson(AccessoriesBill_AccessoriesDT);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr);
                jsonStr.Append(",\"AccessoriesBill_AccessoriesInfo\":" + AccessoriesBill_AccessoriesStr);
                jsonStr.Append("}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region 保存辅料明细信息==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            string IntentionId = RequestHelper.GetString("IntentionId");
            string GetAccessoriesDate = RequestHelper.GetString("GetAccessoriesDate");
            string UserName = RequestHelper.GetString("UserName");
            string AllFee = RequestHelper.GetString("AllFee");
            string AllFee_actual = RequestHelper.GetString("AllFee_actual");
            string Memo = RequestHelper.GetString("Memo");
            string detailStr_Accessories = RequestHelper.GetString("detailStr_Accessories");
            string BillType = RequestHelper.GetString("BillType");

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Repair.repair_AccessoriesBill model = new SCZM.Model.Repair.repair_AccessoriesBill();
            SCZM.BLL.Repair.repair_AccessoriesBill bll = new SCZM.BLL.Repair.repair_AccessoriesBill();
            model.ID = Utils.StrToInt(ID, 0);
            model.IntentionId = Utils.StrToInt(IntentionId, 0);
            if (GetAccessoriesDate != "")
            {
                model.GetAccessoriesDate = Utils.StrToDateTime(GetAccessoriesDate);
            }
            model.UserName = UserName;
            model.AllFee = Utils.StrToDecimal(AllFee, 0);
            model.AllFee_actual = Utils.StrToDecimal(AllFee_actual, 0);
            model.BillType = Utils.StrToInt(BillType, 0);
            model.Memo = Memo;
            model.OperaDepId =loginUserModel.DepId;
            model.OperaId = loginUserModel.ID;
            model.OperaName = loginUserModel.PerName;
            model.OperaTime = DateTime.Now;

            List<Model.Repair.repair_AccessoriesBill_Accessories> model_detail = new List<Model.Repair.repair_AccessoriesBill_Accessories>();
            Model.Repair.repair_AccessoriesBill_Accessories models = new Model.Repair.repair_AccessoriesBill_Accessories();
            if (detailStr_Accessories != "") {
                string[] detailRow = detailStr_Accessories.Split('≮');
                for (int i = 0; i < detailRow.Length; i++)
                {
                    string[] detailCell = detailRow[i].Split('⊥');
                    models = new Model.Repair.repair_AccessoriesBill_Accessories();
                    models.AccessoriesId = Utils.StrToInt(detailCell[0], 0);
                    models.AccessoriesNat = Utils.StrToInt(detailCell[1], 0);
                    models.AccessoriesNum = Utils.StrToInt(detailCell[2], 0);
                    models.AccessoriesFee = Utils.StrToInt(detailCell[3], 0);
                    models.AccessoriesMemo = detailCell[4];
                    models.AccessoriesDate = Utils.StrToDateTime(detailCell[5], DateTime.Now);
                    model_detail.Add(models);
                }
                model.repair_AccessoriesBill_Accessoriess = model_detail;
            }

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
                        operaMemo = "新增辅料明细：" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "修改辅料明细：" + model.ID;
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

        #region 删除辅料明细信息==============================;
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
            SCZM.BLL.Repair.repair_AccessoriesBill bll = new SCZM.BLL.Repair.repair_AccessoriesBill();
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
                    operaMemo = "删除辅料明细：" + IDStr;
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

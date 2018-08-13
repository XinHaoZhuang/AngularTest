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
namespace SCZM.Web.Ashx.Base
{
	/// <summary>
	/// base_CustomerInformation 的摘要说明
	/// <summary>
	public class base_CustomerInformation : IHttpHandler, IReadOnlySessionState
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
				case "GetList": //获取客户档案列表
					GetList(context, btn);
					break;
				case "GetDetail": //获取客户档案明细
					GetDetail(context, btn);
					break;
				case "SaveData": //保存客户档案信息
					SaveData(context, btn);
					break;
				case "DelData": //删除客户档案信息
					DelData(context, btn);
					break;
                case "ImportData":
                    ImportData(context, btn);
                    break;
                case "truncateTable":
                    truncateTable(context, btn);
                    break;
                case "GetList_Import":
                    GetList_Import(context, btn);
                    break;
                case "checkData":
                    checkData(context, btn);
                    break;
                case "DelList_Import":
                    DelList_Import(context, btn);
                    break;
                case "InsertInformation":
                    InsertInformation(context, btn);
                    break;
                case "GetMachineLevel_Undo":
                    GetMachineLevel_Undo(context, btn);
                    break;
                case "setLevel":
                    setLevel(context, btn);
                    break;
                case "setMachineId":
                    setMachineId(context, btn);
                    break;
			}
		}
		#region 获取客户档案列表==============================;
		private void GetList(HttpContext context, string btn)
		{
			if (btn != "show")
			{
				context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
				return;
			}
			try
			{

                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();

				StringBuilder strWhere = new StringBuilder();

                if (CustName != "") {
                    strWhere.Append(" and a.CustName like '%"+CustName+"%' ");
                }
                if (MachineModel != "")
                {
                    strWhere.Append(" and a.MachineModel like '%" + MachineModel + "%' ");
                }
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + MachineCode + "%' ");
                }
				SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
				DataTable dt = bll.GetList(strWhere.ToString()).Tables[0];
				string rowsStr = Utils.ToJson(dt);
				StringBuilder jsonStr = new StringBuilder();
				jsonStr.Append("{\"status\":\"1\",\"msg\":\"数据获取成功！\",\"info\":" + rowsStr + "}");
				context.Response.Write(jsonStr);
			}
			catch (Exception e)
			{
				context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
			}
		}
        private void GetList_Import(HttpContext context, string btn)
        {
            if (btn != "show")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string CustName = RequestHelper.GetString("CustName").Trim();
                string MachineModel = RequestHelper.GetString("MachineModel").Trim();
                string MachineCode = RequestHelper.GetString("MachineCode").Trim();

                StringBuilder strWhere = new StringBuilder();

                if (CustName != "")
                {
                    strWhere.Append(" and a.CustName like '%" + CustName + "%' ");
                }
                if (MachineModel != "")
                {
                    strWhere.Append(" and a.MachineModel like '%" + MachineModel + "%' ");
                }
                if (MachineCode != "")
                {
                    strWhere.Append(" and a.MachineCode like '%" + MachineCode + "%' ");
                }

                SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
                DataTable dt = bll.GetList_Import(strWhere.ToString()).Tables[0];
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

		#region 获取客户档案明细==============================;
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

				SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
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
				context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
			}
		}
		#endregion

		#region 保存客户档案信息==============================;
		private void SaveData(HttpContext context, string btn)
		{
			if (btn != "btnSave")
			{
				context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
				return;
			}
			string ID = RequestHelper.GetString("ID");
			string CustCode = RequestHelper.GetString("CustCode");
			string CustName = RequestHelper.GetString("CustName");
			string CustType = RequestHelper.GetString("CustType");
			string MachineModelId = RequestHelper.GetString("MachineModelId");
			string MachineModel = RequestHelper.GetString("MachineModel");
			string MachineCode = RequestHelper.GetString("MachineCode");
			string MachineState = RequestHelper.GetString("MachineState");
			string Part = RequestHelper.GetString("Part");
			string SMR = RequestHelper.GetString("SMR");
			string Linkman = RequestHelper.GetString("Linkman");
			string LinkPhone = RequestHelper.GetString("LinkPhone");

			Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
			SCZM.Model.Base.base_CustomerInformation model = new SCZM.Model.Base.base_CustomerInformation();
			SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
			model.ID = Utils.StrToInt(ID, 0);
			model.CustCode = CustCode;
			model.CustName = CustName;
			model.CustType = CustType;
			model.MachineModelId = Utils.StrToInt(MachineModelId, 0);
			model.MachineModel = MachineModel;
			model.MachineCode = MachineCode;
			model.MachineState = MachineState;
			model.Part = Part;
			model.SMR = Utils.StrToDecimal(SMR, 0);
			model.Linkman = Linkman;
			model.LinkPhone = LinkPhone;
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
						operaMemo = "新增客户档案：" + model.ID;
						}
					}
					else
					{
					if (bll.Update(model, out operaMessage))
					{
						status = "1";
						operaAction = Enums.ActionEnum.Edit.ToString();
						operaMemo = "修改客户档案：" + model.ID;
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

		#region 删除客户档案信息==============================;
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
			SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
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
					operaMemo = "删除客户档案：" + IDStr ;
					//写入操作日志
					BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
				}
				context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
				return;
			}
			catch (Exception e)
			{
				context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" +Utils.HtmlEncode(e.Message) + "\"}");
			}
		}
        private void DelList_Import(HttpContext context, string btn)
        {
            if (btn != "delImport")
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
            SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
            try
            {
                if (bll.DelList_Import(IDStr) > 0)
                {
                    context.Response.Write("{\"status\":\"1\",\"msg\":\"操作成功\"}");
                }
                else {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"操作失败\"}");
                }
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
		#endregion
        public void ImportData(HttpContext context, string btn) {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            string rows = RequestHelper.GetString("rows");
            

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
            
            string operaMessage = "";
            string status = "0";
            string operaAction = "";
            string operaMemo = "";
            try
            {
                if (rows != "")
                {
                     if (bll.ImportData(rows, out operaMessage) > 0)
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Add.ToString();
                        operaMemo = "导入客户档案";
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
        public void truncateTable(HttpContext context,string btn) {
            if (btn != "btnSave") {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
                bll.truncateTable();
                context.Response.Write("{\"status\":\"1\",\"msg\":\"操作成功\"}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        public void checkData(HttpContext context, string btn) {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
                DataSet ds = bll.chooseMachineCode();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string rowStr = Utils.ToJson(ds.Tables[0]);
                        context.Response.Write("{\"status\":\"1\",\"msg\":\"操作成功\",\"info\":" + rowStr + "}");
                    }
                    else {
                        context.Response.Write("{\"status\":\"2\",\"msg\":\"操作成功\"}");
                    }
                }
                else
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"操作失败\"}");
                }
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        public void InsertInformation(HttpContext context, string btn)
        {
            if (btn != "btnInsert")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
                int num=bll.InsertInformation();
                context.Response.Write("{\"status\":\"1\",\"msg\":\"操作成功\",\"num\":\""+num+"\"}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        public void GetMachineLevel_Undo(HttpContext context, string btn)
        {
            if (btn != "btnMachineLevel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
                DataSet ds = bll.GetMachineLevel_Undo();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string rowStr = Utils.ToJson(ds.Tables[0]);
                        context.Response.Write("{\"status\":\"1\",\"msg\":\"操作成功\",\"info\":" + rowStr + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"status\":\"2\",\"msg\":\"操作成功\"}");
                    }
                }
                else
                {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"操作失败\"}");
                }
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        //setLevel
        public void setLevel(HttpContext context, string btn)
        {
            if (btn != "btnLevel")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                string rows = RequestHelper.GetString("rows").Trim();
                SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
                if (rows != "")
                {
                    int num = bll.setLevel(rows);
                    context.Response.Write("{\"status\":\"1\",\"msg\":\"操作成功\",\"num\":\"" + num + "\"}");
                }
                else {
                    context.Response.Write("{\"status\":\"0\",\"msg\":\"未选中数据\"}");
                }
                
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        //setMachineId
        public void setMachineId(HttpContext context, string btn)
        {
            if (btn != "btnMachineId")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
                return;
            }
            try
            {
                
                SCZM.BLL.Base.base_CustomerInformation bll = new SCZM.BLL.Base.base_CustomerInformation();
                int num = bll.setMachineId();
                context.Response.Write("{\"status\":\"1\",\"msg\":\"操作成功\",\"num\":\"" + num + "\"}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"对不起，系统出错：" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
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

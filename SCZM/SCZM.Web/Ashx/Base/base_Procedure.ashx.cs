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
	/// base_Procedure 的摘要说明
	/// <summary>
	public class base_Procedure : IHttpHandler, IReadOnlySessionState
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
				case "GetList": //获取工序列表
					GetList(context, btn);
					break;
				case "GetDetail": //获取工序明细
					GetDetail(context, btn);
					break;
				case "SaveData": //保存工序信息
					SaveData(context, btn);
					break;
				case "DelData": //删除工序信息
					DelData(context, btn);
					break;
			}
		}
		#region 获取工序列表==============================;
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
                string supId = RequestHelper.GetString("supId").Trim();
                string ProcedureName = RequestHelper.GetString("ProcedureName").Trim();
                if (supId != "") {
                    strWhere.Append(" and a.SupId=" + supId + " ");
                }
                if (ProcedureName != "") {
                    strWhere.Append(" and a.ProcedureName like '%"+ProcedureName+"%' ");
                }
                SCZM.BLL.Base.base_Procedure bll = new SCZM.BLL.Base.base_Procedure();
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
		#endregion

		#region 获取工序明细==============================;
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

				SCZM.BLL.Base.base_Procedure bll = new SCZM.BLL.Base.base_Procedure();
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

		#region 保存工序信息==============================;
		private void SaveData(HttpContext context, string btn)
		{
			if (btn != "btnSave")
			{
				context.Response.Write("{\"status\":\"0.2\",\"msg\":\"对不起，您没有操作权限！\"}");
				return;
			}
			string ID = RequestHelper.GetString("ID");
			string ProcedureName = RequestHelper.GetString("ProcedureName");
			string SupId = RequestHelper.GetString("SupId");
            //string SupList = RequestHelper.GetString("SupList");
			string SortId = RequestHelper.GetString("SortId");
			string Memo = RequestHelper.GetString("Memo");

			Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
			SCZM.Model.Base.base_Procedure model = new SCZM.Model.Base.base_Procedure();
			SCZM.BLL.Base.base_Procedure bll = new SCZM.BLL.Base.base_Procedure();
            
			model.ID = Utils.StrToInt(ID, 0);
			model.ProcedureName = ProcedureName;
			model.SupId = Utils.StrToInt(SupId, 0);
            model.SupList = bll.GetSupList(model.SupId).Trim() + SupId + ",";
			model.SortId = Utils.StrToInt(SortId, 0);
			model.Memo = Memo;
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
						operaMemo = "新增工序：" + model.ID;
						}
					}
					else
					{
					if (bll.Update(model, out operaMessage))
					{
						status = "1";
						operaAction = Enums.ActionEnum.Edit.ToString();
						operaMemo = "修改工序：" + model.ID;
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

		#region 删除工序信息==============================;
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
			SCZM.BLL.Base.base_Procedure bll = new SCZM.BLL.Base.base_Procedure();
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
					operaMemo = "删除工序：" + IDStr ;
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

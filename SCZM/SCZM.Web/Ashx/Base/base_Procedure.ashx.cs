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
	/// base_Procedure ��ժҪ˵��
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
			//ȡ�ô�������
			string action = RequestHelper.GetQueryString("action");
			switch (action)
			{
				case "GetList": //��ȡ�����б�
					GetList(context, btn);
					break;
				case "GetDetail": //��ȡ������ϸ
					GetDetail(context, btn);
					break;
				case "SaveData": //���湤����Ϣ
					SaveData(context, btn);
					break;
				case "DelData": //ɾ��������Ϣ
					DelData(context, btn);
					break;
			}
		}
		#region ��ȡ�����б�==============================;
		private void GetList(HttpContext context, string btn)
		{
			if (btn != "show")
			{
				context.Response.Write("{\"status\":\"0.2\",\"msg\":\"�Բ�����û�в���Ȩ�ޣ�\"}");
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
				jsonStr.Append("{\"status\":\"1\",\"msg\":\"���ݻ�ȡ�ɹ���\",\"info\":" + rowsStr + "}");
				context.Response.Write(jsonStr);
			}
			catch (Exception e)
			{
				context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ���ϵͳ����" +Utils.HtmlEncode(e.Message) + "\"}");
			}
		}
		#endregion

		#region ��ȡ������ϸ==============================;
		private void GetDetail(HttpContext context, string btn)
		{
			if (btn != "show")
			{
				context.Response.Write("{\"status\":\"0.2\",\"msg\":\"�Բ�����û�в���Ȩ�ޣ�\"}");
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
					context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ��𣬸��������ѱ�������ɾ����\"}");
					return;
				}
				string rowsStr = Utils.ToJson(dt);
				StringBuilder jsonStr = new StringBuilder();
				jsonStr.Append("{\"status\":\"1\",\"msg\":\"���ݻ�ȡ�ɹ���\",\"info\":" + rowsStr);
				jsonStr.Append("}");
				context.Response.Write(jsonStr);
			}
			catch (Exception e)
			{
				context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ���ϵͳ����" +Utils.HtmlEncode(e.Message) + "\"}");
			}
		}
		#endregion

		#region ���湤����Ϣ==============================;
		private void SaveData(HttpContext context, string btn)
		{
			if (btn != "btnSave")
			{
				context.Response.Write("{\"status\":\"0.2\",\"msg\":\"�Բ�����û�в���Ȩ�ޣ�\"}");
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
						operaMemo = "��������" + model.ID;
						}
					}
					else
					{
					if (bll.Update(model, out operaMessage))
					{
						status = "1";
						operaAction = Enums.ActionEnum.Edit.ToString();
						operaMemo = "�޸Ĺ���" + model.ID;
					}
				}
				if (status == "1")
				{
					//д�������־
						BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
				}
				context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
				return;
			}
			catch (Exception e)
			{
				context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ���ϵͳ����" + Utils.HtmlEncode(e.Message) + "\"}");
				return;
			}
		}
		#endregion

		#region ɾ��������Ϣ==============================;
		private void DelData(HttpContext context, string btn)
		{
			if (btn != "btnDel")
			{
				context.Response.Write("{\"status\":\"0.2\",\"msg\":\"�Բ�����û�в���Ȩ�ޣ�\"}");
				return;
			}
			string IDStr = RequestHelper.GetString("IDStr");
			if (IDStr == "")
			{
				context.Response.Write("{\"status\":\"0\",\"msg\":\"��ѡ����Ҫɾ���ļ�¼��\"}");
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
					operaMemo = "ɾ������" + IDStr ;
					//д�������־
					BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
				}
				context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
				return;
			}
			catch (Exception e)
			{
				context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ���ϵͳ����" +Utils.HtmlEncode(e.Message) + "\"}");
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

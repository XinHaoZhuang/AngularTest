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
	/// base_CustomerType ��ժҪ˵��
	/// <summary>
	public class base_CustomerType : IHttpHandler, IReadOnlySessionState
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
				case "GetList": //��ȡ�ͻ�����б�
					GetList(context, btn);
					break;
				case "GetDetail": //��ȡ�ͻ������ϸ
					GetDetail(context, btn);
					break;
				case "SaveData": //����ͻ������Ϣ
					SaveData(context, btn);
					break;
				case "DelData": //ɾ���ͻ������Ϣ
					DelData(context, btn);
					break;
			}
		}
		#region ��ȡ�ͻ�����б�==============================;
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

				SCZM.BLL.Base.base_CustomerType bll = new SCZM.BLL.Base.base_CustomerType();
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

		#region ��ȡ�ͻ������ϸ==============================;
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

				SCZM.BLL.Base.base_CustomerType bll = new SCZM.BLL.Base.base_CustomerType();
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

		#region ����ͻ������Ϣ==============================;
		private void SaveData(HttpContext context, string btn)
		{
			if (btn != "btnSave")
			{
				context.Response.Write("{\"status\":\"0.2\",\"msg\":\"�Բ�����û�в���Ȩ�ޣ�\"}");
				return;
			}
			string ID = RequestHelper.GetString("ID");
			string CustTypeName = RequestHelper.GetString("CustTypeName");
			string FlagRegister = RequestHelper.GetString("FlagRegister");

			Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
			SCZM.Model.Base.base_CustomerType model = new SCZM.Model.Base.base_CustomerType();
			SCZM.BLL.Base.base_CustomerType bll = new SCZM.BLL.Base.base_CustomerType();
			model.ID = Utils.StrToInt(ID, 0);
			model.CustTypeName = CustTypeName;
			model.FlagRegister = Utils.StrToInt(FlagRegister, 0);
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
						operaMemo = "�����ͻ����" + model.ID;
						}
					}
					else
					{
					if (bll.Update(model, out operaMessage))
					{
						status = "1";
						operaAction = Enums.ActionEnum.Edit.ToString();
						operaMemo = "�޸Ŀͻ����" + model.ID;
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

		#region ɾ���ͻ������Ϣ==============================;
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
			SCZM.BLL.Base.base_CustomerType bll = new SCZM.BLL.Base.base_CustomerType();
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
					operaMemo = "ɾ���ͻ����" + IDStr ;
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

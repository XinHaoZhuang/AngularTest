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
    /// base_RepairTimeStandard ��ժҪ˵��
    /// <summary>
    public class base_RepairTimeStandard : IHttpHandler, IReadOnlySessionState
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
                case "GetList": //��ȡ��ʱ��׼�б�
                    GetList(context, btn);
                    break;
                case "GetDetail": //��ȡ��ʱ��׼��ϸ
                    GetDetail(context, btn);
                    break;
                case "SaveData": //���湤ʱ��׼��Ϣ
                    SaveData(context, btn);
                    break;
                case "DelData": //ɾ����ʱ��׼��Ϣ
                    DelData(context, btn);
                    break;
            }
        }
        #region ��ȡ��ʱ��׼�б�==============================;
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

                SCZM.BLL.Base.base_RepairTimeStandard bll = new SCZM.BLL.Base.base_RepairTimeStandard();
                DataTable dt = bll.GetList(strWhere.ToString()).Tables[0];
                StringBuilder jsonData = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        jsonData.Append("{\"ProcedureId\":" + dt.Rows[i]["ProcedureId"].ToString() + ",\"ProcedureName\":\"" + dt.Rows[i]["ProcedureName"].ToString() + "\",\"SupId\":" + dt.Rows[i]["SupId"].ToString() + ",\"ID\":\"" + dt.Rows[i]["ID"].ToString() + "\",\"MachineLevel10\":\"" + dt.Rows[i]["MachineLevel10"].ToString() + "\",\"MachineLevel10\":\"" + dt.Rows[i]["MachineLevel10"].ToString() + "\",\"MachineLevel20\":\"" + dt.Rows[i]["MachineLevel20"].ToString() + "\",\"MachineLevel30\":\"" + dt.Rows[i]["MachineLevel30"].ToString() + "\",\"MachineLevel40\":\"" + dt.Rows[i]["MachineLevel40"].ToString() + "\",\"MachineLevel50\":\"" + dt.Rows[i]["MachineLevel50"].ToString() + "\",\"NumType\":\""+dt.Rows[i]["NumType"].ToString()+"\"");
                        if (dt.Rows[i]["SupId"].ToString() != "0") {
                            jsonData.Append(",\"_parentId\":" + dt.Rows[i]["SupId"].ToString() + "");
                        }
                        if (dt.Select("SupId=" + dt.Rows[i]["ProcedureId"].ToString()).Length > 0 && dt.Rows[i]["SupId"].ToString() != "0")
                        {
                            jsonData.Append(",\"state\":\"closed\"");
                        }
                        jsonData.Append("},");
                    }
                    jsonData.Remove(jsonData.Length - 1, 1);
                }

                string rowsStr = Utils.ToJson(dt);
                StringBuilder jsonStr = new StringBuilder();
                jsonStr.Append("{\"status\":\"1\",\"msg\":\"���ݻ�ȡ�ɹ���\",\"info\":{\"total\":" + dt.Rows.Count + ",\"rows\":[" + jsonData.ToString() + "]}}");
                context.Response.Write(jsonStr);
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ���ϵͳ������" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region ��ȡ��ʱ��׼��ϸ==============================;
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

                SCZM.BLL.Base.base_RepairTimeStandard bll = new SCZM.BLL.Base.base_RepairTimeStandard();
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
                context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ���ϵͳ������" + Utils.HtmlEncode(e.Message) + "\"}");
            }
        }
        #endregion

        #region ���湤ʱ��׼��Ϣ==============================;
        private void SaveData(HttpContext context, string btn)
        {
            if (btn != "btnSave")
            {
                context.Response.Write("{\"status\":\"0.2\",\"msg\":\"�Բ�����û�в���Ȩ�ޣ�\"}");
                return;
            }
            string ID = RequestHelper.GetString("ID");
            string ProcedureId = RequestHelper.GetString("ProcedureId");
            string MachineLevel10 = RequestHelper.GetString("MachineLevel10");
            string MachineLevel20 = RequestHelper.GetString("MachineLevel20");
            string MachineLevel30 = RequestHelper.GetString("MachineLevel30");
            string MachineLevel40 = RequestHelper.GetString("MachineLevel40");
            string MachineLevel50 = RequestHelper.GetString("MachineLevel50");
            string NumType = RequestHelper.GetString("NumType");

            Model.System.sys_LoginUser loginUserModel = BaseWeb.GetLoginInfo();
            SCZM.Model.Base.base_RepairTimeStandard model = new SCZM.Model.Base.base_RepairTimeStandard();
            SCZM.BLL.Base.base_RepairTimeStandard bll = new SCZM.BLL.Base.base_RepairTimeStandard();
            model.ID = Utils.StrToInt(ID, 0);
            model.ProcedureId = Utils.StrToInt(ProcedureId, 0);
            model.MachineLevel10 = Utils.StrToDecimal(MachineLevel10, 0);
            model.MachineLevel20 = Utils.StrToDecimal(MachineLevel20, 0);
            model.MachineLevel30 = Utils.StrToDecimal(MachineLevel30, 0);
            model.MachineLevel40 = Utils.StrToDecimal(MachineLevel40, 0);
            model.MachineLevel50 = Utils.StrToDecimal(MachineLevel50, 0);
            model.NumType = Utils.StrToInt(NumType, 0);
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
                        operaMemo = "������ʱ��׼��" + model.ID;
                    }
                }
                else
                {
                    if (bll.Update(model, out operaMessage))
                    {
                        status = "1";
                        operaAction = Enums.ActionEnum.Edit.ToString();
                        operaMemo = "�޸Ĺ�ʱ��׼��" + model.ID;
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
                context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ���ϵͳ������" + Utils.HtmlEncode(e.Message) + "\"}");
                return;
            }
        }
        #endregion

        #region ɾ����ʱ��׼��Ϣ==============================;
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
            SCZM.BLL.Base.base_RepairTimeStandard bll = new SCZM.BLL.Base.base_RepairTimeStandard();
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
                    operaMemo = "ɾ����ʱ��׼��" + IDStr;
                    //д�������־
                    BaseWeb.AddOpera(loginUserModel, int.Parse(RequestHelper.GetQueryString("MenuId")), operaAction, operaMemo);
                }
                context.Response.Write("{\"status\":\"" + status + "\",\"msg\":\"" + operaMessage + "\"}");
                return;
            }
            catch (Exception e)
            {
                context.Response.Write("{\"status\":\"0\",\"msg\":\"�Բ���ϵͳ������" + Utils.HtmlEncode(e.Message) + "\"}");
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
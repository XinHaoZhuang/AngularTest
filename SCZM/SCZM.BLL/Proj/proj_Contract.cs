using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Proj;
namespace SCZM.BLL.Proj
{
    /// <summary>
    /// ҵ���߼���proj_Contract ��ժҪ˵����
    /// </summary>
    public partial class proj_Contract
    {
        private readonly SCZM.DAL.Proj.proj_Contract dal = new SCZM.DAL.Proj.proj_Contract();
        public proj_Contract()
        { }
        #region  ��������
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(SCZM.Model.Proj.proj_Contract model, int submitFlag, out string message)
        {
            message = "����ɹ���";
            if(dal.Exists(model.ProjId,0))
            {
                message = "�Բ��𣬸���Ŀ��ͬ�Ѵ��ڣ�";
                return 0;
            }
            string errMessage = "";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "proj_Contract";
            if (submitFlag == 1)
            {
                message = "���沢�ύ�ɹ���";
                errMessage = procBLL.CheckSubmit(tableName, model.BillSign, model.DepId, "", model.OperaId);
                if (errMessage != "")
                {
                    message = errMessage;
                    return 0;
                }
            }
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "����ʧ�ܣ�";
            }
            else
            {
                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (model.AttachmentId_Contract == "" || model.AttachmentId_Contract == null ? "" : model.AttachmentId_Contract + ",") +
                    (model.AttachmentId_ControlCard == "" || model.AttachmentId_ControlCard == null ? "" : model.AttachmentId_ControlCard + ",")
                    ;
                string FileUse = "��Ŀ��ͬ";
                if (IDList != "")
                {
                    attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, rowId);
                }
                if (submitFlag == 1)
                {
                    errMessage = procBLL.SetBillProcess_Submit(tableName, rowId.ToString(), 1, model.OperaId);
                    if (errMessage != "")
                    {
                        message = "����ɹ�,���ύʧ�ܣ�����ϵϵͳ����Ա��";
                    }
                }
            }
            return rowId;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(SCZM.Model.Proj.proj_Contract model, int submitFlag, out string message)
        {
            message = "����ɹ���";
            if (dal.Exists(model.ProjId, model.ID))
            {
                message = "�Բ��𣬸���Ŀ��ͬ�Ѵ��ڣ�";
                return false;
            }

            string errMessage = "";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "proj_Contract";
            Model.Proj.proj_Contract model_old = dal.GetModel(model.ID);
            if (model_old.BillState == 2)
            {
                int Num = procBLL.DelBillProcess(model.BillSign, model.ID, tableName, model.OperaId, model.OperaName);
                //if (Num == -1) {
                //    message = "�Բ���ϵͳ��������ϵϵͳ����Ա��";
                //    return false;
                //}
            }
            if (submitFlag == 1)
            {
                message = "���沢�ύ�ɹ���";
                errMessage = procBLL.CheckSubmit(tableName, model.BillSign, model.DepId, "", model.OperaId);
                if (errMessage != "")
                {
                    message = errMessage;
                    return false;
                }
            }
            int rows = dal.Update(model);
            if (rows == 0)
            {
                message = "�Բ��𣬸��������ѱ�������ɾ����";
                return false;
            }
            else
            {
                BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
                string IDList = (model.AttachmentId_Contract == "" || model.AttachmentId_Contract == null ? "" : model.AttachmentId_Contract + ",") +
                    (model.AttachmentId_ControlCard == "" || model.AttachmentId_ControlCard == null ? "" : model.AttachmentId_ControlCard + ",")
                    ;
                string FileUse = "��Ŀ��ͬ";
                if (IDList != "")
                {
                    attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, model.ID);
                }
                
                //
                if (submitFlag == 1)
                {
                    errMessage = procBLL.SetBillProcess_Submit(tableName, model.ID.ToString(), 1, model.OperaId);
                    if (errMessage != "")
                    {
                        message = "����ɹ�,���ύʧ�ܣ�����ϵϵͳ����Ա��";
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public SCZM.Model.Proj.proj_Contract GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// ��ȡ����ҳ��������б� ͨ���洢����
        /// </summary>
        /// <param name="strWhere">where����</param>
        /// <param name="perId">��ѯ��ID</param>
        /// <param name="billState">����״̬0ȫ�� 1������� 2 ����δ��� 3������ 4������ͬ�� 5δ�ύ 6���ύ</param>
        /// <returns></returns>
        public DataSet GetApplyList(string strWhere, int perId, int billState)
        {
            return dal.GetApplyList(strWhere, perId, billState);
        }

        /// <summary>
        /// ��ȡ���ҳ��������б� ͨ���洢����
        /// </summary>
        /// <param name="strWhere">where����</param>
        /// <param name="perId">��ѯ��ID</param>
        /// <param name="billState">����״̬0ȫ�� 1������� 2 ����δ��� 3������ 4������ͬ�� 5δ�ύ 6���ύ</param>
        /// <param name="auditState">���״̬ 0ȫ�� 1����� 2 δ���	3 ����ͬ��	4 ������ͬ��</param>
        /// <returns></returns>
        public DataSet GetAuditList(string strWhere, int perId, int billState, int auditState)
        {
            return dal.GetAuditList(strWhere, perId, billState, auditState);
        }

        /// <summary>
        /// ���������ϸ ����ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            return dal.GetDetail(ID);
        }

        /// <summary>
        /// �����ύ����
        /// </summary>
        /// <param name="IDList">ID�ַ���</param>
        /// <param name="operaId">������ID</param>
        /// <param name="message">������Ϣ</param>
        /// <returns></returns>
        public bool Submit(string IDList, int operaId, out string message)
        {
            message = "�ύ�ɹ���";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "proj_Contract";
            string errMessage = procBLL.CheckSubmit(tableName, "", 0, IDList, operaId);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            errMessage = procBLL.SetBillProcess_Submit(tableName, IDList, 1, operaId);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����ȡ���ύ����
        /// </summary>
        /// <param name="IDList">ID�ַ���</param>
        /// <param name="operaId">������ID</param>
        /// <param name="message">������Ϣ</param>
        /// <returns></returns>
        public bool CancelSubmit(string IDList, int operaId, out string message)
        {
            message = "ȡ���ύ�ɹ���";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "proj_Contract";
            string errMessage = procBLL.CheckCancelSubmit(tableName, IDList, operaId);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            errMessage = procBLL.SetBillProcess_Submit(tableName, IDList, 2, operaId);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            return true;
        }

        #endregion  ��������
        #region  ��չ����
        /// <summary>
        /// ����ɾ������
        /// </summary>
        public bool DeleteList(string IDList, out string message)
        {
            message = "ɾ���ɹ���";
            string[] idArr = IDList.Split(',');
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "proj_Contract";
            string errMessage = procBLL.ExistSubmit(tableName, IDList);
            if (errMessage != "")
            {
                message = errMessage;
                return false;
            }
            int rows = dal.DeleteList(IDList);
            if (rows == 0)
            {
                message = "�Բ�����ѡ�����ѱ�������ɾ����";
                return false;
            }
            else
            {
                errMessage = procBLL.SetBillProcess_Del(tableName, IDList);
                return true;
            }
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="IDList">ID�ַ���</param>
        /// <param name="operaId">������ID</param>
        /// <param name="flagAudit">���״̬</param>
        /// <param name="auditorOpinion">������</param>
        /// <param name="message">������Ϣ</param>
        /// <returns></returns>
        public bool Audit(string IDList, int operaId, int flagAudit, string auditorOpinion, out string message)
        {
            message = "�����ɹ���";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "proj_Contract";
            string resultMessage = "";
            bool result = procBLL.SetBillProcess_Audit_Batch(tableName, IDList, 1, operaId, flagAudit, auditorOpinion, out resultMessage);
            if (result == false)
            {
                message = resultMessage;
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����ȡ���������
        /// </summary>
        /// <param name="IDList">ID�ַ���</param>
        /// <param name="operaId">������ID</param>
        /// <param name="message">������Ϣ</param>
        /// <returns></returns>
        public bool CanclAudit(string IDList, int operaId, out string message)
        {
            message = "ȡ�������ɹ���";
            BLL.System.sys_Process_Exec procBLL = new BLL.System.sys_Process_Exec();
            string tableName = "proj_Contract";
            string resultMessage = "";
            bool result = procBLL.SetBillProcess_Audit_Batch(tableName, IDList, 2, operaId, 0, "", out resultMessage);
            if (result == false)
            {
                message = resultMessage;
                return false;
            }
            return true;
        }
        /// <summary>
        /// ��ò��������б�
        /// </summary>
        /// <param name="strWhere">where����</param>
        /// <param name="operaId">������</param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere, int operaId)
        {
            return dal.GetComboList(strWhere, operaId);
        }
        #endregion  ��չ����
    }
}


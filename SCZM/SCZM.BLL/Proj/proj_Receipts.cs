using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SCZM.Common;
using SCZM.Model.Proj;
namespace SCZM.BLL.Proj
{
    /// <summary>
    /// ҵ���߼���proj_Receipts ��ժҪ˵����
    /// </summary>
    public partial class proj_Receipts
    {
        private readonly SCZM.DAL.Proj.proj_Receipts dal = new SCZM.DAL.Proj.proj_Receipts();
        public proj_Receipts()
        { }
        #region  ��������
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(SCZM.Model.Proj.proj_Receipts model, out string message)
        {
            message = "����ɹ���";
            int rowId = dal.Add(model);
            if (rowId < 1)
            {
                message = "����ʧ�ܣ�";
            }
            return rowId;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(SCZM.Model.Proj.proj_Receipts model, out string message)
        {
            message = "����ɹ���";
            int rows = dal.Update(model);
            if (rows == 0)
            {
                message = "�Բ��𣬸��������ѱ�������ɾ����";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public SCZM.Model.Proj.proj_Receipts GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// ��������б� ͨ��Where����
        /// </summary>
        public DataSet GetList(string strWhere, int operaId)
        {
            return dal.GetList(strWhere, operaId);
        }

        /// <summary>
        /// ���������ϸ ����ID
        /// </summary>
        public DataSet GetDetail(int ID)
        {
            return dal.GetDetail(ID);
        }

        #endregion  ��������
        #region  ��չ����
        /// <summary>
        /// ����ɾ������
        /// </summary>
        public bool DeleteList(string IDList, out string message)
        {
            message = "ɾ���ɹ���";
            int rows = dal.DeleteList(IDList);
            if (rows == 0)
            {
                message = "�Բ�����ѡ�����ѱ�������ɾ����";
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion  ��չ����
    }
}


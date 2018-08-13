using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Base;
namespace SCZM.BLL.Base
{
	/// <summary>
	/// ҵ���߼���base_CustomerInformation ��ժҪ˵����
	/// </summary>
	public partial class base_CustomerInformation
	{
		private readonly SCZM.DAL.Base.base_CustomerInformation dal=new SCZM.DAL.Base.base_CustomerInformation();
		public base_CustomerInformation()
		{}
		#region  ��������
		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(SCZM.Model.Base.base_CustomerInformation model, out string message)
		{
			message = "����ɹ���";
			int rowId= dal.Add(model);
			if (rowId <1)
			{
				message = "����ʧ�ܣ�";
			}
			return rowId;
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(SCZM.Model.Base.base_CustomerInformation model,out string message)
		{
			message = "����ɹ���";
			int rows= dal.Update(model);
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
		public SCZM.Model.Base.base_CustomerInformation GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// ��������б� ͨ��Where����
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        public DataSet GetComboList(string strWhere)
		{
            return dal.GetComboList(strWhere);
		}
        public DataSet GetList_Import(string strWhere)
        {
            return dal.GetList_Import(strWhere);
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
        public int ImportData(string rows,out string message) {
            message = "����ɹ�";
            int num=dal.ImportData(rows);
            if (num == 0) {
                message = "����ʧ��";
            }
            return num;
        }
        public void truncateTable() {
            dal.truncateTable();
        }
        public DataSet chooseMachineCode() {
            if (dal.DelMachineCode_Null() >= 0)
            {
                return dal.chooseMachineCode();
            }
            else {
                return null;
            }
        }
        public int DelList_Import(string IdStr) {
            return dal.DelList_Import(IdStr);
        }
        public int InsertInformation()
        {
            return dal.InsertInformation();
        }
        public DataSet GetMachineLevel_Undo() {
            return dal.GetMachineLevel_Undo();
        }
        public int setLevel(string rows) {
            return dal.setLevel(rows);
        }
        public int setMachineId()
        {
            return dal.setMachineId();
        }
		#endregion  ��չ����
	}
}


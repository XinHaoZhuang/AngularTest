using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Base;
namespace SCZM.BLL.Base
{
	/// <summary>
	/// ҵ���߼���base_Customer ��ժҪ˵����
	/// </summary>
	public partial class base_Customer
	{
		private readonly SCZM.DAL.Base.base_Customer dal=new SCZM.DAL.Base.base_Customer();
		public base_Customer()
		{}
		#region  ��������
		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(SCZM.Model.Base.base_Customer model, out string message)
		{
			message = "����ɹ���";
            if (dal.Exists("CustName", model.CustName, 0))
            {
                message = "�ͻ������Ѵ��ڣ����ܱ��棡";
                return 0;
            }
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
		public bool Update(SCZM.Model.Base.base_Customer model,out string message)
		{
			message = "����ɹ���";
            if (dal.Exists("CustName", model.CustName, model.ID))
            {
                message = "�ͻ������Ѵ��ڣ����ܱ��棡";
                return false;
            }
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
		public SCZM.Model.Base.base_Customer GetModel(int ID)
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
        /// <summary>
        /// ��������б� ͨ��Where����
        /// </summary>
        public DataSet GetComboList(string strWhere)
        {
            return dal.GetComboList(strWhere);
        }
		#endregion  ��չ����
	}
}


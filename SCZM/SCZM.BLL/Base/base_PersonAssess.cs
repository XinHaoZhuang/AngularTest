using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Base;
namespace SCZM.BLL.Base
{
	/// <summary>
	/// ҵ���߼���base_PersonAssess ��ժҪ˵����
	/// </summary>
	public partial class base_PersonAssess
	{
		private readonly SCZM.DAL.Base.base_PersonAssess dal=new SCZM.DAL.Base.base_PersonAssess();
		public base_PersonAssess()
		{}
		#region  ��������
		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(SCZM.Model.Base.base_PersonAssess model, out string message)
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
		public bool Update(SCZM.Model.Base.base_PersonAssess model,out string message)
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
		public SCZM.Model.Base.base_PersonAssess GetModel(int ID)
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
        public DataSet GetHistory(int ID, int PersonId) {
            return dal.GetHistory(ID, PersonId);
        }
		#endregion  ��չ����
	}
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Proj;
namespace SCZM.BLL.Proj
{
	/// <summary>
	/// ҵ���߼���proj_Project ��ժҪ˵����
	/// </summary>
	public partial class proj_Project
	{
		private readonly SCZM.DAL.Proj.proj_Project dal=new SCZM.DAL.Proj.proj_Project();
		public proj_Project()
		{}
		#region  ��������
		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(SCZM.Model.Proj.proj_Project model, out string message)
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
		public bool Update(SCZM.Model.Proj.proj_Project model,out string message)
		{
			message = "����ɹ���";
			int rows= dal.Update(model);
			if (rows == 0)
			{
				message = "�Բ�����û�п��Ƹ�����¼��Ȩ�ޣ�";
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
		public SCZM.Model.Proj.proj_Project GetModel(int ID)
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
		public bool DeleteList(string IDList, int operaId, out string message)
		{
			message = "ɾ���ɹ���";
            int rows = dal.DeleteList(IDList, operaId);
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
        /// ��ò��������б�
        /// </summary>
        /// <param name="strWhere">where����</param>
        /// <param name="operaId">������</param>
        /// <param name="flagContract">�Ƿ������ͬ 0δ���� 1������������ͨ��</param>
        /// <returns></returns>
        public DataSet GetComboList(string strWhere, int operaId, int flagContract)
        {
            return dal.GetComboList(strWhere, operaId, flagContract);
        }
		#endregion  ��չ����
	}
}


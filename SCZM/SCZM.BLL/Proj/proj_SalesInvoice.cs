using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCZM.Common;
using SCZM.Model.Proj;
namespace SCZM.BLL.Proj
{
	/// <summary>
	/// ҵ���߼���proj_SalesInvoice ��ժҪ˵����
	/// </summary>
	public partial class proj_SalesInvoice
	{
		private readonly SCZM.DAL.Proj.proj_SalesInvoice dal=new SCZM.DAL.Proj.proj_SalesInvoice();
		public proj_SalesInvoice()
		{}
		#region  ��������
		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(SCZM.Model.Proj.proj_SalesInvoice model, out string message)
		{
			message = "����ɹ���";
			int rowId= dal.Add(model);
			if (rowId <1)
			{
				message = "����ʧ�ܣ�";
			}
			else
			{
				BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
				string IDList = (model.AttachmentId_Invoice == "" || model.AttachmentId_Invoice == null ? "" : model.AttachmentId_Invoice + ",") 
					;
				string FileUse = "��Ŀ�ؿƱ";
				if (IDList != "")
				{
					attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, rowId);
				}
			}
			return rowId;
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(SCZM.Model.Proj.proj_SalesInvoice model,out string message)
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
				BLL.System.sys_Attachment attachmenBLL = new BLL.System.sys_Attachment();
				string IDList = (model.AttachmentId_Invoice == "" || model.AttachmentId_Invoice == null ? "" : model.AttachmentId_Invoice + ",") 
					;
				string FileUse = "��Ŀ�ؿƱ";
				if (IDList != "")
				{
					attachmenBLL.UpdateUseList(Utils.DelLastComma(IDList), FileUse, model.ID);
				}
				return true;
			}
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public SCZM.Model.Proj.proj_SalesInvoice GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// ��������б� ͨ��Where����
		/// </summary>
		public DataSet GetList(string strWhere,int operaId)
		{
			return dal.GetList(strWhere,operaId);
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


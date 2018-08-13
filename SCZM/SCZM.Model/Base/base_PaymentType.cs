using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// ʵ����base_PaymentType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class base_PaymentType
	{
		public base_PaymentType()
		{}
		#region Model
		private int _id;
		private string _paymenttypename;
		private string _memo;
		private int _sortid;
		private bool _flagdel= false;
		private string _operaname;
		private DateTime _operatime= DateTime.Now;
		/// <summary>
		/// ��ˮ��
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// ��������
		/// </summary>
		public string PaymentTypeName
		{
			set{ _paymenttypename=value;}
			get{return _paymenttypename;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// �����
		/// </summary>
		public int SortId
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// ɾ�����
		/// </summary>
		public bool FlagDel
		{
			set{ _flagdel=value;}
			get{return _flagdel;}
		}
		/// <summary>
		/// ���ղ�����
		/// </summary>
		public string OperaName
		{
			set{ _operaname=value;}
			get{return _operaname;}
		}
		/// <summary>
		/// ���ղ���ʱ��
		/// </summary>
		public DateTime OperaTime
		{
			set{ _operatime=value;}
			get{return _operatime;}
		}
		#endregion Model

	}
}


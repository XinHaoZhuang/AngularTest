using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// ʵ����base_Partner ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class base_Partner
	{
		public base_Partner()
		{}
		#region Model
		private int _id;
		private int _partnertypeid;
		private string _partnername;
		private string _mdmcode;
		private string _manager;
		private string _mobilephone;
		private string _email;
		private string _memo;
		private bool _flagdel= false;
		private int _operaid=0;
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
		/// ������λ����ID
		/// </summary>
		public int PartnerTypeId
		{
			set{ _partnertypeid=value;}
			get{return _partnertypeid;}
		}
		/// <summary>
		/// ������λ����
		/// </summary>
		public string PartnerName
		{
			set{ _partnername=value;}
			get{return _partnername;}
		}
		/// <summary>
		/// MDM����
		/// </summary>
		public string MDMCode
		{
			set{ _mdmcode=value;}
			get{return _mdmcode;}
		}
		/// <summary>
		/// ��ϵ��
		/// </summary>
		public string Manager
		{
			set{ _manager=value;}
			get{return _manager;}
		}
		/// <summary>
		/// �ֻ�
		/// </summary>
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
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
		/// ɾ�����
		/// </summary>
		public bool FlagDel
		{
			set{ _flagdel=value;}
			get{return _flagdel;}
		}
		/// <summary>
		/// ������ID
		/// </summary>
		public int OperaId
		{
			set{ _operaid=value;}
			get{return _operaid;}
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


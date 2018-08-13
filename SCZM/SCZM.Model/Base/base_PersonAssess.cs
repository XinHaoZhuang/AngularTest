using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// ʵ����base_PersonAssess ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class base_PersonAssess
	{
		public base_PersonAssess()
		{}
		#region Model
		private int _id;
		private int _personid;
		private decimal _assess;
		private DateTime _createdate= DateTime.Now;
		private int _flagdel=0;
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
		/// ��ԱID
		/// </summary>
		public int PersonId
		{
			set{ _personid=value;}
			get{return _personid;}
		}
		/// <summary>
		/// ����ϵ��
		/// </summary>
		public decimal Assess
		{
			set{ _assess=value;}
			get{return _assess;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// ɾ�����
		/// </summary>
		public int FlagDel
		{
			set{ _flagdel=value;}
			get{return _flagdel;}
		}
		/// <summary>
		/// ������ԱID
		/// </summary>
		public int OperaId
		{
			set{ _operaid=value;}
			get{return _operaid;}
		}
		/// <summary>
		/// ������Ա����
		/// </summary>
		public string OperaName
		{
			set{ _operaname=value;}
			get{return _operaname;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperaTime
		{
			set{ _operatime=value;}
			get{return _operatime;}
		}
		#endregion Model

	}
}


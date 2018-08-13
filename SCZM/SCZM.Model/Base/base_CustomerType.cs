using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// ʵ����base_CustomerType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class base_CustomerType
	{
		public base_CustomerType()
		{}
		#region Model
		private int _id;
		private string _custtypename=("");
		private int _flagregister=0;
		private int _flagdel=0;
		private int _operaid=0;
		private string _operaname=("");
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
		/// �ͻ����
		/// </summary>
		public string CustTypeName
		{
			set{ _custtypename=value;}
			get{return _custtypename;}
		}
		/// <summary>
		/// �Ƿ���Ҫ���볡�Ǽ� 0����Ҫ 1��Ҫ
		/// </summary>
		public int FlagRegister
		{
			set{ _flagregister=value;}
			get{return _flagregister;}
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
		/// ��ԱID
		/// </summary>
		public int OperaId
		{
			set{ _operaid=value;}
			get{return _operaid;}
		}
		/// <summary>
		/// ��Ա����
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


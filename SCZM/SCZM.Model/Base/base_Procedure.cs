using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// ʵ����base_Procedure ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class base_Procedure
	{
		public base_Procedure()
		{}
		#region Model
		private int _id;
		private string _ProcedureName=("");
		private int _supid=0;
		private string _suplist= "0";
		private int _sortid=0;
		private string _memo=("");
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
		/// ����
		/// </summary>
		public string ProcedureName
		{
			set{ _ProcedureName=value;}
			get{return _ProcedureName;}
		}
		/// <summary>
		/// �ϼ�ID
		/// </summary>
		public int SupId
		{
			set{ _supid=value;}
			get{return _supid;}
		}
		/// <summary>
		/// �ϼ�ID�б�
		/// </summary>
		public string SupList
		{
			set{ _suplist=value;}
			get{return _suplist;}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int SortId
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
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


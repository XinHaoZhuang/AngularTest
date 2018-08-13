using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// ʵ����base_CustomerInformation ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class base_CustomerInformation
	{
		public base_CustomerInformation()
		{}
		#region Model
		private int _id;
		private string _custcode;
		private string _custname;
		private string _custtype;
		private int? _machinemodelid;
		private string _machinemodel;
		private string _machinecode;
		private string _machinestate;
		private string _part;
		private decimal? _smr;
		private string _linkman;
		private string _linkphone;
		private int _flagdel=0;
		private string _operaname;
		private DateTime _operatime;
		/// <summary>
		/// ��ˮ��
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustCode
		{
			set{ _custcode=value;}
			get{return _custcode;}
		}
		/// <summary>
		/// �ͻ�
		/// </summary>
		public string CustName
		{
			set{ _custname=value;}
			get{return _custname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustType
		{
			set{ _custtype=value;}
			get{return _custtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MachineModelId
		{
			set{ _machinemodelid=value;}
			get{return _machinemodelid;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string MachineModel
		{
			set{ _machinemodel=value;}
			get{return _machinemodel;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string MachineCode
		{
			set{ _machinecode=value;}
			get{return _machinecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MachineState
		{
			set{ _machinestate=value;}
			get{return _machinestate;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Part
		{
			set{ _part=value;}
			get{return _part;}
		}
		/// <summary>
		/// ����Сʱ��
		/// </summary>
		public decimal? SMR
		{
			set{ _smr=value;}
			get{return _smr;}
		}
		/// <summary>
		/// ��ϵ��
		/// </summary>
		public string Linkman
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		public string LinkPhone
		{
			set{ _linkphone=value;}
			get{return _linkphone;}
		}
		/// <summary>
		/// ɾ��
		/// </summary>
		public int FlagDel
		{
			set{ _flagdel=value;}
			get{return _flagdel;}
		}
		/// <summary>
		/// ������
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


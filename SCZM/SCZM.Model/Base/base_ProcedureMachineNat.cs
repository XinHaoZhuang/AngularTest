using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// ʵ����base_ProcedureMachineNat ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class base_ProcedureMachineNat
	{
		public base_ProcedureMachineNat()
		{}
		#region Model
		private int _id;
		private int _procedureid=0;
		private decimal _machinelevel10=0M;
		private decimal _machinelevel20=0M;
		private decimal _machinelevel30=0M;
		private decimal _machinelevel40=0M;
		private decimal _machinelevel50=0M;
		private int _numtype;
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
		/// ����ID
		/// </summary>
		public int ProcedureId
		{
			set{ _procedureid=value;}
			get{return _procedureid;}
		}
		/// <summary>
		/// mini-160
		/// </summary>
		public decimal MachineLevel10
		{
			set{ _machinelevel10=value;}
			get{return _machinelevel10;}
		}
		/// <summary>
		/// 200-270
		/// </summary>
		public decimal MachineLevel20
		{
			set{ _machinelevel20=value;}
			get{return _machinelevel20;}
		}
		/// <summary>
		/// 300-360
		/// </summary>
		public decimal MachineLevel30
		{
			set{ _machinelevel30=value;}
			get{return _machinelevel30;}
		}
		/// <summary>
		/// 400-460
		/// </summary>
		public decimal MachineLevel40
		{
			set{ _machinelevel40=value;}
			get{return _machinelevel40;}
		}
		/// <summary>
		/// 650����
		/// </summary>
		public decimal MachineLevel50
		{
			set{ _machinelevel50=value;}
			get{return _machinelevel50;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public int NumType
		{
			set{ _numtype=value;}
			get{return _numtype;}
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
		/// ������ID
		/// </summary>
		public int OperaId
		{
			set{ _operaid=value;}
			get{return _operaid;}
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


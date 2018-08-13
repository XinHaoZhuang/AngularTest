using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// 实体类base_CustomerType 。(属性说明自动提取数据库字段的描述信息)
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
		/// 流水号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 客户类别
		/// </summary>
		public string CustTypeName
		{
			set{ _custtypename=value;}
			get{return _custtypename;}
		}
		/// <summary>
		/// 是否需要出入场登记 0不需要 1需要
		/// </summary>
		public int FlagRegister
		{
			set{ _flagregister=value;}
			get{return _flagregister;}
		}
		/// <summary>
		/// 删除标记
		/// </summary>
		public int FlagDel
		{
			set{ _flagdel=value;}
			get{return _flagdel;}
		}
		/// <summary>
		/// 人员ID
		/// </summary>
		public int OperaId
		{
			set{ _operaid=value;}
			get{return _operaid;}
		}
		/// <summary>
		/// 人员姓名
		/// </summary>
		public string OperaName
		{
			set{ _operaname=value;}
			get{return _operaname;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime OperaTime
		{
			set{ _operatime=value;}
			get{return _operatime;}
		}
		#endregion Model

	}
}


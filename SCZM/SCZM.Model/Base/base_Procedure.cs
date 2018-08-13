using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// 实体类base_Procedure 。(属性说明自动提取数据库字段的描述信息)
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
		/// 流水号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 工序
		/// </summary>
		public string ProcedureName
		{
			set{ _ProcedureName=value;}
			get{return _ProcedureName;}
		}
		/// <summary>
		/// 上级ID
		/// </summary>
		public int SupId
		{
			set{ _supid=value;}
			get{return _supid;}
		}
		/// <summary>
		/// 上级ID列表
		/// </summary>
		public string SupList
		{
			set{ _suplist=value;}
			get{return _suplist;}
		}
		/// <summary>
		/// 序号
		/// </summary>
		public int SortId
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
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
		/// 操作人员ID
		/// </summary>
		public int OperaId
		{
			set{ _operaid=value;}
			get{return _operaid;}
		}
		/// <summary>
		/// 操作人员名称
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


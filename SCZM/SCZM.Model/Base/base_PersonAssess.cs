using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// 实体类base_PersonAssess 。(属性说明自动提取数据库字段的描述信息)
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
		/// 流水号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 人员ID
		/// </summary>
		public int PersonId
		{
			set{ _personid=value;}
			get{return _personid;}
		}
		/// <summary>
		/// 考核系数
		/// </summary>
		public decimal Assess
		{
			set{ _assess=value;}
			get{return _assess;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
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


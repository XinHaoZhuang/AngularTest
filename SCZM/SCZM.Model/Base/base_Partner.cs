using System;
namespace SCZM.Model.Base
{
	/// <summary>
	/// 实体类base_Partner 。(属性说明自动提取数据库字段的描述信息)
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
		/// 流水号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 合作单位类型ID
		/// </summary>
		public int PartnerTypeId
		{
			set{ _partnertypeid=value;}
			get{return _partnertypeid;}
		}
		/// <summary>
		/// 合作单位名称
		/// </summary>
		public string PartnerName
		{
			set{ _partnername=value;}
			get{return _partnername;}
		}
		/// <summary>
		/// MDM代码
		/// </summary>
		public string MDMCode
		{
			set{ _mdmcode=value;}
			get{return _mdmcode;}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string Manager
		{
			set{ _manager=value;}
			get{return _manager;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 删除标记
		/// </summary>
		public bool FlagDel
		{
			set{ _flagdel=value;}
			get{return _flagdel;}
		}
		/// <summary>
		/// 操作人ID
		/// </summary>
		public int OperaId
		{
			set{ _operaid=value;}
			get{return _operaid;}
		}
		/// <summary>
		/// 最终操作人
		/// </summary>
		public string OperaName
		{
			set{ _operaname=value;}
			get{return _operaname;}
		}
		/// <summary>
		/// 最终操作时间
		/// </summary>
		public DateTime OperaTime
		{
			set{ _operatime=value;}
			get{return _operatime;}
		}
		#endregion Model

	}
}


using System;
namespace UHPROJ.Model.Base
{
    /// <summary>
    /// 实体类shop_NewBuildApply 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class shop_NewBuildApply
    {
        public shop_NewBuildApply()
        { }
        #region Model
        private int _id;
        private string _applyno;
        private int _cityareaid = 0;
        private string _cityname;
        private string _countyname;
        private int _marketlevel;
        private string _shopname;
        private string _shopaddress;
        private int _shoptype;
        private int _shopclass;
        private decimal _displayarea = 0M;
        private decimal _displayarea_chg = 0M;
        private decimal _displayarea_yg = 0M;
        private DateTime? _builddate;
        private string _distributorname;
        private string _distributortelephone;
        private string _distributormobile;
        private string _distributoremail;
        private string _distributorcontext;
        private int _category = 1;
        private decimal _franchisefee = 0M;
        private decimal _guaranteemoney = 0M;
        private decimal _firstpayment = 0M;
        private string _attachmentid_zsrw;
        private string _attachmentid_gh;
        private string _billsign = ("");
        private int _auditnextid = 0;
        private int _billstate = 0;
        private bool _flagdel = false;
        private int _depid;
        private int _operaid = 0;
        private string _operaname = ("");
        private DateTime _operatime = DateTime.Now;
        private DateTime? _auditendtime;
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 申请单号
        /// </summary>
        public string ApplyNo
        {
            set { _applyno = value; }
            get { return _applyno; }
        }
        /// <summary>
        /// 城市区域ID
        /// </summary>
        public int CityAreaId
        {
            set { _cityareaid = value; }
            get { return _cityareaid; }
        }
        /// <summary>
        /// 城市名
        /// </summary>
        public string CityName
        {
            set { _cityname = value; }
            get { return _cityname; }
        }
        /// <summary>
        /// 区县
        /// </summary>
        public string CountyName
        {
            set { _countyname = value; }
            get { return _countyname; }
        }
        /// <summary>
        /// 申请市场级别
        /// </summary>
        public int MarketLevel
        {
            set { _marketlevel = value; }
            get { return _marketlevel; }
        }
        /// <summary>
        /// 网店名称
        /// </summary>
        public string ShopName
        {
            set { _shopname = value; }
            get { return _shopname; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string ShopAddress
        {
            set { _shopaddress = value; }
            get { return _shopaddress; }
        }
        /// <summary>
        /// 加盟店类型 1衣柜 2橱柜 3综合店
        /// </summary>
        public int ShopType
        {
            set { _shoptype = value; }
            get { return _shoptype; }
        }
        /// <summary>
        /// 网店类别   1旗舰店    2形象店   3样板店    4体验店   5普通店
        /// </summary>
        public int ShopClass
        {
            set { _shopclass = value; }
            get { return _shopclass; }
        }
        /// <summary>
        /// 展示面积
        /// </summary>
        public decimal DisplayArea
        {
            set { _displayarea = value; }
            get { return _displayarea; }
        }
        /// <summary>
        /// 橱柜展示面积
        /// </summary>
        public decimal DisplayArea_CHG
        {
            set { _displayarea_chg = value; }
            get { return _displayarea_chg; }
        }
        /// <summary>
        /// 衣柜展示面积
        /// </summary>
        public decimal DisplayArea_YG
        {
            set { _displayarea_yg = value; }
            get { return _displayarea_yg; }
        }
        /// <summary>
        /// 建店日期
        /// </summary>
        public DateTime? BuildDate
        {
            set { _builddate = value; }
            get { return _builddate; }
        }
        /// <summary>
        /// 经销商姓名
        /// </summary>
        public string DistributorName
        {
            set { _distributorname = value; }
            get { return _distributorname; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string DistributorTelephone
        {
            set { _distributortelephone = value; }
            get { return _distributortelephone; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string DistributorMobile
        {
            set { _distributormobile = value; }
            get { return _distributormobile; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string DistributorEmail
        {
            set { _distributoremail = value; }
            get { return _distributoremail; }
        }
        /// <summary>
        /// 经销商背景
        /// </summary>
        public string DistributorContext
        {
            set { _distributorcontext = value; }
            get { return _distributorcontext; }
        }
        /// <summary>
        /// 类别（不用） 1新建店 2分店 
        /// </summary>
        public int Category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// 加盟费
        /// </summary>
        public decimal FranchiseFee
        {
            set { _franchisefee = value; }
            get { return _franchisefee; }
        }
        /// <summary>
        /// 质保金
        /// </summary>
        public decimal GuaranteeMoney
        {
            set { _guaranteemoney = value; }
            get { return _guaranteemoney; }
        }
        /// <summary>
        /// 首批货款
        /// </summary>
        public decimal FirstPayment
        {
            set { _firstpayment = value; }
            get { return _firstpayment; }
        }
        /// <summary>
        /// 附件ID-招商入围评分表
        /// </summary>
        public string AttachmentId_ZSRW
        {
            set { _attachmentid_zsrw = value; }
            get { return _attachmentid_zsrw; }
        }
        /// <summary>
        /// 附件ID-新建店规划书
        /// </summary>
        public string AttachmentId_GH
        {
            set { _attachmentid_gh = value; }
            get { return _attachmentid_gh; }
        }
        /// <summary>
        /// 单据标识
        /// </summary>
        public string BillSign
        {
            set { _billsign = value; }
            get { return _billsign; }
        }
        /// <summary>
        /// 下一步审核sys_Process_Exec ID 审核如果同意，则变为下一步
        /// </summary>
        public int AuditNextId
        {
            set { _auditnextid = value; }
            get { return _auditnextid; }
        }
        /// <summary>
        /// 单据状态 0 审批中 1 审批完成 2审批不同意
        /// </summary>
        public int BillState
        {
            set { _billstate = value; }
            get { return _billstate; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepId
        {
            set { _depid = value; }
            get { return _depid; }
        }
        /// <summary>
        /// 最终操作人ID
        /// </summary>
        public int OperaId
        {
            set { _operaid = value; }
            get { return _operaid; }
        }
        /// <summary>
        /// 最终操作人
        /// </summary>
        public string OperaName
        {
            set { _operaname = value; }
            get { return _operaname; }
        }
        /// <summary>
        /// 最终操作时间
        /// </summary>
        public DateTime OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        /// <summary>
        /// 审批结束时间
        /// </summary>
        public DateTime? AuditEndTime
        {
            set { _auditendtime = value; }
            get { return _auditendtime; }
        }
        #endregion Model

    }
}


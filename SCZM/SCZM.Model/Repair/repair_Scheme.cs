using System;
using System.Collections.Generic;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类repair_Scheme 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_Scheme
    {
        public repair_Scheme()
        { }
        #region Model
        private int _id;
        private int _intentionid;
        private string _schemecode;
        private string _repaircontent;
        private int? _flagengkc;
        private int? _flagppmkc;
        private int? _flageng;
        private int? _flagppm;
        private int? _flagmcv;
        private int? _flagele;
        private int? _flagvm;
        private int? _flagrm;
        private int? _flagsm;
        private int? _flagum;
        private int? _flagvr;
        private int? _flagsp;
        private int? _flagother;
        private DateTime? _schemedate;
        private DateTime? _promiseleavedate;
        private decimal? _timefee;
        private decimal? _partfee;
        private decimal _schemefee;
        private int _flagdel = 0;
        private int _operadepid;
        private int _operaid;
        private string _operaname;
        private DateTime _operatime = DateTime.Now;
        private string attachmentid_scheme;
        private DateTime? _schemedate_predict;
        private decimal? _schemefee_predict;
        private string attachmentid_scheme_predict="";
        private string _memo;
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        /// <summary>
        /// 报价单
        /// </summary>
        public string AttachmentId_Scheme_predict
        {
            get { return attachmentid_scheme_predict; }
            set { attachmentid_scheme_predict = value; }
        }
        private string attachmentid_agreement;
        /// <summary>
        /// 协议
        /// </summary>
        public string AttachmentId_Agreement
        {
            get { return attachmentid_agreement; }
            set { attachmentid_agreement = value; }
        }
        /// <summary>
        /// 报价金额
        /// </summary>
        public decimal? SchemeFee_predict
        {
            get { return _schemefee_predict; }
            set { _schemefee_predict = value; }
        }
       
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 维修意愿ID
        /// </summary>
        public int IntentionId
        {
            set { _intentionid = value; }
            get { return _intentionid; }
        }
        /// <summary>
        /// 方案号
        /// </summary>
        public string SchemeCode
        {
            set { _schemecode = value; }
            get { return _schemecode; }
        }
        /// <summary>
        /// 维修内容
        /// </summary>
        public string RepairContent
        {
            set { _repaircontent = value; }
            get { return _repaircontent; }
        }
        /// <summary>
        /// ENG是否按KC标准大修
        /// </summary>
        public int? FlagENGKC
        {
            set { _flagengkc = value; }
            get { return _flagengkc; }
        }
        /// <summary>
        /// PPM是否按KC标准大修
        /// </summary>
        public int? FlagPPMKC
        {
            set { _flagppmkc = value; }
            get { return _flagppmkc; }
        }
        /// <summary>
        /// 发动机
        /// </summary>
        public int? FlagENG
        {
            set { _flageng = value; }
            get { return _flageng; }
        }
        /// <summary>
        /// 主泵
        /// </summary>
        public int? FlagPPM
        {
            set { _flagppm = value; }
            get { return _flagppm; }
        }
        /// <summary>
        /// 主控阀
        /// </summary>
        public int? FlagMCV
        {
            set { _flagmcv = value; }
            get { return _flagmcv; }
        }
        /// <summary>
        /// 电器
        /// </summary>
        public int? FlagELE
        {
            set { _flagele = value; }
            get { return _flagele; }
        }
        /// <summary>
        /// 行走马达
        /// </summary>
        public int? FlagVM
        {
            set { _flagvm = value; }
            get { return _flagvm; }
        }
        /// <summary>
        /// 回转马达
        /// </summary>
        public int? FlagRM
        {
            set { _flagrm = value; }
            get { return _flagrm; }
        }
        /// <summary>
        /// 钣金
        /// </summary>
        public int? FlagSM
        {
            set { _flagsm = value; }
            get { return _flagsm; }
        }
        /// <summary>
        /// 底盘
        /// </summary>
        public int? FlagUM
        {
            set { _flagum = value; }
            get { return _flagum; }
        }
        /// <summary>
        /// 焊修
        /// </summary>
        public int? FlagVR
        {
            set { _flagvr = value; }
            get { return _flagvr; }
        }
        /// <summary>
        /// 喷漆
        /// </summary>
        public int? FlagSP
        {
            get { return _flagsp; }
            set { _flagsp = value; }
        }
        /// <summary>
        /// 其它
        /// </summary>
        public int? FlagOther
        {
            get { return _flagother; }
            set { _flagother = value; }
        }
        /// <summary>
        /// 方案确认日期
        /// </summary>
        public DateTime? SchemeDate
        {
            set { _schemedate = value; }
            get { return _schemedate; }
        }
        /// <summary>
        /// 客户确认订件时间
        /// </summary>
        public DateTime? SchemeDate_predict
        {
            get { return _schemedate_predict; }
            set { _schemedate_predict = value; }
        }
        /// <summary>
        /// 承诺出厂时间
        /// </summary>
        public DateTime? PromiseLeaveDate
        {
            set { _promiseleavedate = value; }
            get { return _promiseleavedate; }
        }
        /// <summary>
        /// 工时费
        /// </summary>
        public decimal? TimeFee
        {
            set { _timefee = value; }
            get { return _timefee; }
        }
        /// <summary>
        /// 零件费
        /// </summary>
        public decimal? PartFee
        {
            set { _partfee = value; }
            get { return _partfee; }
        }
        /// <summary>
        /// 订价
        /// </summary>
        public decimal SchemeFee
        {
            set { _schemefee = value; }
            get { return _schemefee; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        /// <summary>
        /// 操作部门ID
        /// </summary>
        public int OperaDepId
        {
            set { _operadepid = value; }
            get { return _operadepid; }
        }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperaId
        {
            set { _operaid = value; }
            get { return _operaid; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperaName
        {
            set { _operaname = value; }
            get { return _operaname; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        /// <summary>
        /// 订价单
        /// </summary>
        public string AttachmentId_Scheme
        {
            get { return attachmentid_scheme; }
            set { attachmentid_scheme = value; }
        }
        #endregion Model

        private List<repair_Scheme_Part> _repair_scheme_parts;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<repair_Scheme_Part> repair_Scheme_Parts
        {
            set { _repair_scheme_parts = value; }
            get { return _repair_scheme_parts; }
        }
        private List<repair_Scheme_Item> _repair_scheme_items;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<repair_Scheme_Item> repair_Scheme_Items
        {
            set { _repair_scheme_items = value; }
            get { return _repair_scheme_items; }
        }

    }
    /// <summary>
    /// 实体类repair_Scheme_Part 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_Scheme_Part
    {
        public repair_Scheme_Part()
        { }
        #region Model
        private int _id;
        private int _schemeid;
        private string _partcode;
        private string _partname;
        private decimal _partnat;
        private int _partnum;
        private decimal _partfee;
        private decimal _partfee_rebate;


        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 维修方案id
        /// </summary>
        public int SchemeId
        {
            set { _schemeid = value; }
            get { return _schemeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PartCode
        {
            set { _partcode = value; }
            get { return _partcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PartName
        {
            set { _partname = value; }
            get { return _partname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PartNat
        {
            set { _partnat = value; }
            get { return _partnat; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int PartNum
        {
            set { _partnum = value; }
            get { return _partnum; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal PartFee
        {
            set { _partfee = value; }
            get { return _partfee; }
        }
        /// <summary>
        /// 折后价
        /// </summary>
        public decimal PartFee_rebate
        {
            get { return _partfee_rebate; }
            set { _partfee_rebate = value; }
        }
        #endregion Model

    }
    /// <summary>
    /// 实体类repair_Scheme_Item 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_Scheme_Item
    {
        public repair_Scheme_Item()
        { }
        #region Model
        private int _id;
        private int _schemeid;
        private int _itemid;
        private int _itemnum;
        private decimal _itemfee;
        private decimal _itemfee_rebate;
        private decimal _itemnat;
        /// <summary>
        /// 单价
        /// </summary>
        public decimal ItemNat
        {
            get { return _itemnat; }
            set { _itemnat = value; }
        }


        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 维修方案id
        /// </summary>
        public int SchemeId
        {
            set { _schemeid = value; }
            get { return _schemeid; }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ItemId
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int ItemNum
        {
            set { _itemnum = value; }
            get { return _itemnum; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal ItemFee
        {
            set { _itemfee = value; }
            get { return _itemfee; }
        }
        /// <summary>
        /// 折后价
        /// </summary>
        public decimal ItemFee_rebate
        {
            get { return _itemfee_rebate; }
            set { _itemfee_rebate = value; }
        }
        #endregion Model
    }
}

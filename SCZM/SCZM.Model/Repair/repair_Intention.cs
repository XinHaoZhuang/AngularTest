using System;
using System.Collections.Generic;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类repair_Intention 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_Intention
    {
        public repair_Intention()
        { }
        #region Model
        private int _id;
        private string _intentioncode;
        private DateTime _intentiondate=DateTime.Now;
        private int _intentiontype;
        private string _intentioncode_last;
        private int? _businessdepid;
        private string _businesspername;
        private int? _custtypeid;
        private string _custname;
        private int? _machinemodelid;
        private string _machinecode;
        private string _enginemodel;
        private string _enginecode;
        private int? _smr;
        private int? _flagfxgch;
        private string _linkman;
        private string _linkphone;
        private string _machineadress;
        private string _machine;
        private string _machinestatus;
        private int? _flagresult = 1;
        private int? _repairtypeid;
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
        private string _repairadress;
        private DateTime? _expectenterdate;
        private DateTime? _expectleavedate;
        private decimal? _expecttimefee = 0M;
        private decimal? _expectpartfee = 0M;
        private decimal? _expectfee = 0M;
        private string _custopinion;
        private int? _flagagreement = 0;
        private DateTime? _agreementdate;
        private string _attachmentid_agreement;
        private int? _repairstate = 1;
        private int _flagdel = 0;
        private int _operadepid;
        private int _operaid;
        private string _operaname;
        private DateTime _operatime = DateTime.Now;
        private DateTime? _actualenterdate;
        private string _operaname_enter;
        private DateTime? _operatime_enter;
        private DateTime? _actualleavedate;
        private string _operaname_leave;
        private DateTime? _operatime_leave;
        private int _repairmode = 1;
        private int _flaglocale = 0;
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 意向号
        /// </summary>
        public string IntentionCode
        {
            set { _intentioncode = value; }
            get { return _intentioncode; }
        }
        /// <summary>
        /// 意向日期
        /// </summary>
        public DateTime IntentionDate
        {
            set { _intentiondate = value; }
            get { return _intentiondate; }
        }
        /// <summary>
        /// 报修类型 1新报修 2返修
        /// </summary>
        public int IntentionType
        {
            set { _intentiontype = value; }
            get { return _intentiontype; }
        }
        /// <summary>
        /// 返修意向号
        /// </summary>
        public string IntentionCode_Last
        {
            set { _intentioncode_last = value; }
            get { return _intentioncode_last; }
        }
        /// <summary>
        /// 业务部门ID
        /// </summary>
        public int? BusinessDepId
        {
            set { _businessdepid = value; }
            get { return _businessdepid; }
        }
        /// <summary>
        /// 业务人员ID
        /// </summary>
        public string BusinessPerName
        {
            set { _businesspername = value; }
            get { return _businesspername; }
        }
        /// <summary>
        /// 客户类型ID
        /// </summary>
        public int? CustTypeId
        {
            set { _custtypeid = value; }
            get { return _custtypeid; }
        }
        /// <summary>
        /// 客户名
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }
        /// <summary>
        /// 机型ID
        /// </summary>
        public int? MachineModelId
        {
            set { _machinemodelid = value; }
            get { return _machinemodelid; }
        }
        /// <summary>
        /// 机号
        /// </summary>
        public string MachineCode
        {
            set { _machinecode = value; }
            get { return _machinecode; }
        }
        /// <summary>
        /// 发动机型号
        /// </summary>
        public string EngineModel
        {
            set { _enginemodel = value; }
            get { return _enginemodel; }
        }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineCode
        {
            set { _enginecode = value; }
            get { return _enginecode; }
        }
        /// <summary>
        /// 工作小时数
        /// </summary>
        public int? SMR
        {
            set { _smr = value; }
            get { return _smr; }
        }
        /// <summary>
        /// 是否签订放心工程
        /// </summary>
        public int? FlagFXGCH
        {
            set { _flagfxgch = value; }
            get { return _flagfxgch; }
        }
        /// <summary>
        /// 送修人
        /// </summary>
        public string Linkman
        {
            set { _linkman = value; }
            get { return _linkman; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone
        {
            set { _linkphone = value; }
            get { return _linkphone; }
        }
        /// <summary>
        /// 客户机地址
        /// </summary>
        public string MachineAdress
        {
            set { _machineadress = value; }
            get { return _machineadress; }
        }
        /// <summary>
        /// 机器状况
        /// </summary>
        public string Machine
        {
            set { _machine = value; }
            get { return _machine; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MachineStatus
        {
            set { _machinestatus = value; }
            get { return _machinestatus; }
        }
        /// <summary>
        /// 沟通结果 0尚未有结论 1同意维修 2不同意维修
        /// </summary>
        public int? FlagResult
        {
            set { _flagresult = value; }
            get { return _flagresult; }
        }
        /// <summary>
        /// 维修类型ID
        /// </summary>
        public int? RepairTypeId
        {
            set { _repairtypeid = value; }
            get { return _repairtypeid; }
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
        /// 维修地点
        /// </summary>
        public string RepairAdress
        {
            set { _repairadress = value; }
            get { return _repairadress; }
        }
        /// <summary>
        /// 预计进厂日期
        /// </summary>
        public DateTime? ExpectEnterDate
        {
            set { _expectenterdate = value; }
            get { return _expectenterdate; }
        }
        /// <summary>
        /// 预计出厂日期
        /// </summary>
        public DateTime? ExpectLeaveDate
        {
            set { _expectleavedate = value; }
            get { return _expectleavedate; }
        }
        /// <summary>
        /// 预估工时费
        /// </summary>
        public decimal? ExpectTimeFee
        {
            set { _expecttimefee = value; }
            get { return _expecttimefee; }
        }
        /// <summary>
        /// 预估零件费
        /// </summary>
        public decimal? ExpectPartFee
        {
            set { _expectpartfee = value; }
            get { return _expectpartfee; }
        }
        /// <summary>
        /// 预估合计费用
        /// </summary>
        public decimal? ExpectFee
        {
            set { _expectfee = value; }
            get { return _expectfee; }
        }
        /// <summary>
        /// 客户意见
        /// </summary>
        public string CustOpinion
        {
            set { _custopinion = value; }
            get { return _custopinion; }
        }
        /// <summary>
        /// 是否签订维修协议 0未签订 1已签订
        /// </summary>
        public int? FlagAgreement
        {
            set { _flagagreement = value; }
            get { return _flagagreement; }
        }
        /// <summary>
        /// 协议签订日期
        /// </summary>
        public DateTime? AgreementDate
        {
            set { _agreementdate = value; }
            get { return _agreementdate; }
        }
        /// <summary>
        /// 协议附件ID
        /// </summary>
        public string AttachmentId_Agreement
        {
            set { _attachmentid_agreement = value; }
            get { return _attachmentid_agreement; }
        }
        /// <summary>
        /// 维修状态 10客户谈判 20入厂 30已确认维修方案 40维修中 50维修完成  60出厂 
        /// </summary>
        public int? RepairState
        {
            set { _repairstate = value; }
            get { return _repairstate; }
        }
        /// <summary>
        /// 维修方式 1整机 2部件    
        /// </summary>
        public int RepairMode
        {
            get { return _repairmode; }
            set { _repairmode = value; }
        }
        /// <summary>
        /// 是否现场承揽 0否 1是
        /// </summary>
        public int FlagLocale
        {
            get { return _flaglocale; }
            set { _flaglocale = value; }
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
        /// 进厂日期
        /// </summary>
        public DateTime? ActualEnterDate
        {
            set { _actualenterdate = value; }
            get { return _actualenterdate; }
        }
        /// <summary>
        /// 进厂操作人
        /// </summary>
        public string OperaName_Enter
        {
            set { _operaname_enter = value; }
            get { return _operaname_enter; }
        }
        /// <summary>
        /// 进厂操作时间
        /// </summary>
        public DateTime? OperaTime_Enter
        {
            set { _operatime_enter = value; }
            get { return _operatime_enter; }
        }
        /// <summary>
        /// 出厂日期
        /// </summary>
        public DateTime? ActualLeaveDate
        {
            set { _actualleavedate = value; }
            get { return _actualleavedate; }
        }
        /// <summary>
        /// 出厂操作人
        /// </summary>
        public string OperaName_Leave
        {
            set { _operaname_leave = value; }
            get { return _operaname_leave; }
        }
        /// <summary>
        /// 出厂操作时间
        /// </summary>
        public DateTime? OperaTime_Leave
        {
            set { _operatime_leave = value; }
            get { return _operatime_leave; }
        }
        #endregion Model

        private List<repair_Intention_Package> _repair_intention_packages;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<repair_Intention_Package> repair_Intention_Packages
        {
            set { _repair_intention_packages = value; }
            get { return _repair_intention_packages; }
        }

    }
    /// <summary>
    /// 实体类repair_Intention_Package 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_Intention_Package
    {
        public repair_Intention_Package()
        { }
        #region Model
        private int _id;
        private int _intentionid;
        private int _packageid;
        private int _packagenum;
        private int _flagdel = 0;
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IntentionId
        {
            set { _intentionid = value; }
            get { return _intentionid; }
        }
        /// <summary>
        /// 套包名称
        /// </summary>
        public int PackageId
        {
            set { _packageid = value; }
            get { return _packageid; }
        }
        /// <summary>
        /// 套包数量
        /// </summary>
        public int PackageNum
        {
            get { return _packagenum; }
            set { _packagenum = value; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        #endregion Model

    }
}


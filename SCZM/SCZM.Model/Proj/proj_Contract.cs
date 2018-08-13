using System;
using System.Collections.Generic;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// 实体类proj_Contract 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class proj_Contract
    {
        public proj_Contract()
        { }
        #region Model
        private int _id;
        private string _contractcode;
        private int _projid;
        private int _companyid;
        private DateTime? _contractdate;
        private decimal _contractnat;
        private int _projtype;
        private DateTime? _startdate;
        private DateTime? _completedate;
        private int _timescale;
        private string _memo;
        private string _attachmentid_contract;
        private string _attachmentid_controlcard;
        private string _billsign = ("");
        private int _auditnextid = 0;
        private int _billstate = 0;
        private bool _flagdel = false;
        private int _depid;
        private int _operaid = 0;
        private string _operaname;
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
        /// 合同号
        /// </summary>
        public string ContractCode
        {
            set { _contractcode = value; }
            get { return _contractcode; }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjId
        {
            set { _projid = value; }
            get { return _projid; }
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 签约日期
        /// </summary>
        public DateTime? ContractDate
        {
            set { _contractdate = value; }
            get { return _contractdate; }
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal ContractNat
        {
            set { _contractnat = value; }
            get { return _contractnat; }
        }
        /// <summary>
        /// 项目类型ID 1挂靠 2合营 3自营
        /// </summary>
        public int ProjType
        {
            set { _projtype = value; }
            get { return _projtype; }
        }
        /// <summary>
        /// 计划开工日期
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 计划竣工日期
        /// </summary>
        public DateTime? CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
        }
        /// <summary>
        /// 工期
        /// </summary>
        public int Timescale
        {
            set { _timescale = value; }
            get { return _timescale; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 附件ID-合同
        /// </summary>
        public string AttachmentId_Contract
        {
            set { _attachmentid_contract = value; }
            get { return _attachmentid_contract; }
        }
        /// <summary>
        /// 附件ID-业务控制卡
        /// </summary>
        public string AttachmentId_ControlCard
        {
            set { _attachmentid_controlcard = value; }
            get { return _attachmentid_controlcard; }
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
        /// 单据状态 0 审批中 1 审批完成 2审批不同意 3提交
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
        /// 操作部门ID
        /// </summary>
        public int DepId
        {
            set { _depid = value; }
            get { return _depid; }
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

        private List<proj_ContractDetail> _proj_contractdetails;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<proj_ContractDetail> proj_ContractDetails
        {
            set { _proj_contractdetails = value; }
            get { return _proj_contractdetails; }
        }

    }
    /// <summary>
    /// 实体类proj_ContractDetail 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class proj_ContractDetail
    {
        public proj_ContractDetail()
        { }
        #region Model
        private int _id;
        private int _contractid;
        private string _progress = ("");
        private DateTime? _receivedate;
        private decimal _receiverate;
        private decimal _receivenat;
        private string _memo;
        private bool _flagdel = false;
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContractId
        {
            set { _contractid = value; }
            get { return _contractid; }
        }
        /// <summary>
        /// 收款进度
        /// </summary>
        public string Progress
        {
            set { _progress = value; }
            get { return _progress; }
        }
        /// <summary>
        /// 收款时间
        /// </summary>
        public DateTime? ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 收款比例
        /// </summary>
        public decimal ReceiveRate
        {
            set { _receiverate = value; }
            get { return _receiverate; }
        }
        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal ReceiveNat
        {
            set { _receivenat = value; }
            get { return _receivenat; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        #endregion Model

    }
}


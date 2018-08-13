using System;
using System.Collections.Generic;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// 实体类proj_PartnerContract 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class proj_PartnerContract
    {
        public proj_PartnerContract()
        { }
        #region Model
        private int _id;
        private int _projid;
        private int _partnertypeid;
        private int _partnerid;
        private int _companyid;
        private string _contractcode;
        private DateTime? _contractdate;
        private decimal _contractnat;
        private DateTime? _startdate;
        private DateTime? _completedate;
        private int _timescale = 0;
        private string _memo;
        private string _attachmentid_contract;
        private string _billsign;
        private int _auditnextid = 0;
        private int _billstate = 0;
        private bool _flagdel = false;
        private int _depid = 0;
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
        /// 项目ID
        /// </summary>
        public int ProjId
        {
            set { _projid = value; }
            get { return _projid; }
        }
        /// <summary>
        /// 合作类别ID
        /// </summary>
        public int PartnerTypeId
        {
            set { _partnertypeid = value; }
            get { return _partnertypeid; }
        }
        /// <summary>
        /// 合作单位ID
        /// </summary>
        public int PartnerId
        {
            set { _partnerid = value; }
            get { return _partnerid; }
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
        /// 合同号
        /// </summary>
        public string ContractCode
        {
            set { _contractcode = value; }
            get { return _contractcode; }
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
        /// 开工日期
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 竣工日期
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
        /// 部门ID
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

        private List<proj_PartnerContractDetail> _proj_partnercontractdetails;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<proj_PartnerContractDetail> proj_PartnerContractDetails
        {
            set { _proj_partnercontractdetails = value; }
            get { return _proj_partnercontractdetails; }
        }

    }
    /// <summary>
    /// 实体类proj_PartnerContractDetail 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class proj_PartnerContractDetail
    {
        public proj_PartnerContractDetail()
        { }
        #region Model
        private int _id;
        private int _contractid;
        private string _progress = ("");
        private DateTime? _paydate;
        private decimal _payrate;
        private decimal _paynat;
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
        /// 付款进度
        /// </summary>
        public string Progress
        {
            set { _progress = value; }
            get { return _progress; }
        }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
        }
        /// <summary>
        /// 付款比例
        /// </summary>
        public decimal PayRate
        {
            set { _payrate = value; }
            get { return _payrate; }
        }
        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PayNat
        {
            set { _paynat = value; }
            get { return _paynat; }
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


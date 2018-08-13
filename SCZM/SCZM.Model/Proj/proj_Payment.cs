using System;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// 实体类proj_Payment 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class proj_Payment
    {
        public proj_Payment()
        { }
        #region Model
        private int _id;
        private int _contractid;
        private DateTime? _paydate;
        private decimal _paynat;
        private int _flaginvoice = 0;
        private string _invoiceid;
        private string _invoicecode;
        private string _memo;
        private string _billsign;
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
        /// 合同ID
        /// </summary>
        public int ContractId
        {
            set { _contractid = value; }
            get { return _contractid; }
        }
        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime? PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
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
        /// 开票标记 0未开票 1已开票
        /// </summary>
        public int FlagInvoice
        {
            set { _flaginvoice = value; }
            get { return _flaginvoice; }
        }
        /// <summary>
        /// 发票号ID字符串
        /// </summary>
        public string InvoiceId
        {
            set { _invoiceid = value; }
            get { return _invoiceid; }
        }
        /// <summary>
        /// 发票号(金额)字符串
        /// </summary>
        public string InvoiceCode
        {
            set { _invoicecode = value; }
            get { return _invoicecode; }
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

    }
}


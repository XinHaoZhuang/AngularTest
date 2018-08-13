using System;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// 实体类proj_PurchaseInvoice 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class proj_PurchaseInvoice
    {
        public proj_PurchaseInvoice()
        { }
        #region Model
        private int _id;
        private int _contractid;
        private string _invoicecode;
        private DateTime _invoicedate;
        private decimal? _invoicenat;
        private decimal? _taxrate;
        private string _memo;
        private string _attachmentid_invoice;
        private bool _flagdel = false;
        private int _operaid = 0;
        private string _operaname;
        private DateTime _operatime = DateTime.Now;
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
        /// 发票号

        /// </summary>
        public string InvoiceCode
        {
            set { _invoicecode = value; }
            get { return _invoicecode; }
        }
        /// <summary>
        /// 开票时间

        /// </summary>
        public DateTime InvoiceDate
        {
            set { _invoicedate = value; }
            get { return _invoicedate; }
        }
        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal? InvoiceNat
        {
            set { _invoicenat = value; }
            get { return _invoicenat; }
        }
        /// <summary>
        /// 税率
        /// </summary>
        public decimal? TaxRate
        {
            set { _taxrate = value; }
            get { return _taxrate; }
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
        /// 附件ID-发票
        /// </summary>
        public string AttachmentId_Invoice
        {
            set { _attachmentid_invoice = value; }
            get { return _attachmentid_invoice; }
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
        #endregion Model

    }
}


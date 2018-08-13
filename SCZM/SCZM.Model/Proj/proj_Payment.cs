using System;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// ʵ����proj_Payment ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ��ˮ��
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ��ͬID
        /// </summary>
        public int ContractId
        {
            set { _contractid = value; }
            get { return _contractid; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime? PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public decimal PayNat
        {
            set { _paynat = value; }
            get { return _paynat; }
        }
        /// <summary>
        /// ��Ʊ��� 0δ��Ʊ 1�ѿ�Ʊ
        /// </summary>
        public int FlagInvoice
        {
            set { _flaginvoice = value; }
            get { return _flaginvoice; }
        }
        /// <summary>
        /// ��Ʊ��ID�ַ���
        /// </summary>
        public string InvoiceId
        {
            set { _invoiceid = value; }
            get { return _invoiceid; }
        }
        /// <summary>
        /// ��Ʊ��(���)�ַ���
        /// </summary>
        public string InvoiceCode
        {
            set { _invoicecode = value; }
            get { return _invoicecode; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// ���ݱ�ʶ
        /// </summary>
        public string BillSign
        {
            set { _billsign = value; }
            get { return _billsign; }
        }
        /// <summary>
        /// ��һ�����sys_Process_Exec ID ������ͬ�⣬���Ϊ��һ��
        /// </summary>
        public int AuditNextId
        {
            set { _auditnextid = value; }
            get { return _auditnextid; }
        }
        /// <summary>
        /// ����״̬ 0 ������ 1 ������� 2������ͬ�� 3�ύ
        /// </summary>
        public int BillState
        {
            set { _billstate = value; }
            get { return _billstate; }
        }
        /// <summary>
        /// ɾ�����
        /// </summary>
        public bool FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        /// <summary>
        /// ��������ID
        /// </summary>
        public int DepId
        {
            set { _depid = value; }
            get { return _depid; }
        }
        /// <summary>
        /// ������ID
        /// </summary>
        public int OperaId
        {
            set { _operaid = value; }
            get { return _operaid; }
        }
        /// <summary>
        /// ���ղ�����
        /// </summary>
        public string OperaName
        {
            set { _operaname = value; }
            get { return _operaname; }
        }
        /// <summary>
        /// ���ղ���ʱ��
        /// </summary>
        public DateTime OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public DateTime? AuditEndTime
        {
            set { _auditendtime = value; }
            get { return _auditendtime; }
        }
        #endregion Model

    }
}


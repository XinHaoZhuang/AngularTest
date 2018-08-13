using System;
using System.Collections.Generic;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// ʵ����proj_Contract ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ��ˮ��
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ��ͬ��
        /// </summary>
        public string ContractCode
        {
            set { _contractcode = value; }
            get { return _contractcode; }
        }
        /// <summary>
        /// ��ĿID
        /// </summary>
        public int ProjId
        {
            set { _projid = value; }
            get { return _projid; }
        }
        /// <summary>
        /// ��˾ID
        /// </summary>
        public int CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// ǩԼ����
        /// </summary>
        public DateTime? ContractDate
        {
            set { _contractdate = value; }
            get { return _contractdate; }
        }
        /// <summary>
        /// ��ͬ���
        /// </summary>
        public decimal ContractNat
        {
            set { _contractnat = value; }
            get { return _contractnat; }
        }
        /// <summary>
        /// ��Ŀ����ID 1�ҿ� 2��Ӫ 3��Ӫ
        /// </summary>
        public int ProjType
        {
            set { _projtype = value; }
            get { return _projtype; }
        }
        /// <summary>
        /// �ƻ���������
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// �ƻ���������
        /// </summary>
        public DateTime? CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int Timescale
        {
            set { _timescale = value; }
            get { return _timescale; }
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
        /// ����ID-��ͬ
        /// </summary>
        public string AttachmentId_Contract
        {
            set { _attachmentid_contract = value; }
            get { return _attachmentid_contract; }
        }
        /// <summary>
        /// ����ID-ҵ����ƿ�
        /// </summary>
        public string AttachmentId_ControlCard
        {
            set { _attachmentid_controlcard = value; }
            get { return _attachmentid_controlcard; }
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

        private List<proj_ContractDetail> _proj_contractdetails;
        /// <summary>
        /// ���� 
        /// </summary>
        public List<proj_ContractDetail> proj_ContractDetails
        {
            set { _proj_contractdetails = value; }
            get { return _proj_contractdetails; }
        }

    }
    /// <summary>
    /// ʵ����proj_ContractDetail ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// �տ����
        /// </summary>
        public string Progress
        {
            set { _progress = value; }
            get { return _progress; }
        }
        /// <summary>
        /// �տ�ʱ��
        /// </summary>
        public DateTime? ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// �տ����
        /// </summary>
        public decimal ReceiveRate
        {
            set { _receiverate = value; }
            get { return _receiverate; }
        }
        /// <summary>
        /// �տ���
        /// </summary>
        public decimal ReceiveNat
        {
            set { _receivenat = value; }
            get { return _receivenat; }
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
        /// ɾ�����
        /// </summary>
        public bool FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        #endregion Model

    }
}


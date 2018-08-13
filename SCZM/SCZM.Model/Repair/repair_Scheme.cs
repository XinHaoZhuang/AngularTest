using System;
using System.Collections.Generic;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// ʵ����repair_Scheme ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ��ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        /// <summary>
        /// ���۵�
        /// </summary>
        public string AttachmentId_Scheme_predict
        {
            get { return attachmentid_scheme_predict; }
            set { attachmentid_scheme_predict = value; }
        }
        private string attachmentid_agreement;
        /// <summary>
        /// Э��
        /// </summary>
        public string AttachmentId_Agreement
        {
            get { return attachmentid_agreement; }
            set { attachmentid_agreement = value; }
        }
        /// <summary>
        /// ���۽��
        /// </summary>
        public decimal? SchemeFee_predict
        {
            get { return _schemefee_predict; }
            set { _schemefee_predict = value; }
        }
       
        /// <summary>
        /// ��ˮ��
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ά����ԸID
        /// </summary>
        public int IntentionId
        {
            set { _intentionid = value; }
            get { return _intentionid; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string SchemeCode
        {
            set { _schemecode = value; }
            get { return _schemecode; }
        }
        /// <summary>
        /// ά������
        /// </summary>
        public string RepairContent
        {
            set { _repaircontent = value; }
            get { return _repaircontent; }
        }
        /// <summary>
        /// ENG�Ƿ�KC��׼����
        /// </summary>
        public int? FlagENGKC
        {
            set { _flagengkc = value; }
            get { return _flagengkc; }
        }
        /// <summary>
        /// PPM�Ƿ�KC��׼����
        /// </summary>
        public int? FlagPPMKC
        {
            set { _flagppmkc = value; }
            get { return _flagppmkc; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public int? FlagENG
        {
            set { _flageng = value; }
            get { return _flageng; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int? FlagPPM
        {
            set { _flagppm = value; }
            get { return _flagppm; }
        }
        /// <summary>
        /// ���ط�
        /// </summary>
        public int? FlagMCV
        {
            set { _flagmcv = value; }
            get { return _flagmcv; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int? FlagELE
        {
            set { _flagele = value; }
            get { return _flagele; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public int? FlagVM
        {
            set { _flagvm = value; }
            get { return _flagvm; }
        }
        /// <summary>
        /// ��ת���
        /// </summary>
        public int? FlagRM
        {
            set { _flagrm = value; }
            get { return _flagrm; }
        }
        /// <summary>
        /// �ӽ�
        /// </summary>
        public int? FlagSM
        {
            set { _flagsm = value; }
            get { return _flagsm; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int? FlagUM
        {
            set { _flagum = value; }
            get { return _flagum; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int? FlagVR
        {
            set { _flagvr = value; }
            get { return _flagvr; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int? FlagSP
        {
            get { return _flagsp; }
            set { _flagsp = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int? FlagOther
        {
            get { return _flagother; }
            set { _flagother = value; }
        }
        /// <summary>
        /// ����ȷ������
        /// </summary>
        public DateTime? SchemeDate
        {
            set { _schemedate = value; }
            get { return _schemedate; }
        }
        /// <summary>
        /// �ͻ�ȷ�϶���ʱ��
        /// </summary>
        public DateTime? SchemeDate_predict
        {
            get { return _schemedate_predict; }
            set { _schemedate_predict = value; }
        }
        /// <summary>
        /// ��ŵ����ʱ��
        /// </summary>
        public DateTime? PromiseLeaveDate
        {
            set { _promiseleavedate = value; }
            get { return _promiseleavedate; }
        }
        /// <summary>
        /// ��ʱ��
        /// </summary>
        public decimal? TimeFee
        {
            set { _timefee = value; }
            get { return _timefee; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public decimal? PartFee
        {
            set { _partfee = value; }
            get { return _partfee; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal SchemeFee
        {
            set { _schemefee = value; }
            get { return _schemefee; }
        }
        /// <summary>
        /// ɾ�����
        /// </summary>
        public int FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        /// <summary>
        /// ��������ID
        /// </summary>
        public int OperaDepId
        {
            set { _operadepid = value; }
            get { return _operadepid; }
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
        /// ������
        /// </summary>
        public string OperaName
        {
            set { _operaname = value; }
            get { return _operaname; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        /// <summary>
        /// ���۵�
        /// </summary>
        public string AttachmentId_Scheme
        {
            get { return attachmentid_scheme; }
            set { attachmentid_scheme = value; }
        }
        #endregion Model

        private List<repair_Scheme_Part> _repair_scheme_parts;
        /// <summary>
        /// ���� 
        /// </summary>
        public List<repair_Scheme_Part> repair_Scheme_Parts
        {
            set { _repair_scheme_parts = value; }
            get { return _repair_scheme_parts; }
        }
        private List<repair_Scheme_Item> _repair_scheme_items;
        /// <summary>
        /// ���� 
        /// </summary>
        public List<repair_Scheme_Item> repair_Scheme_Items
        {
            set { _repair_scheme_items = value; }
            get { return _repair_scheme_items; }
        }

    }
    /// <summary>
    /// ʵ����repair_Scheme_Part ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ��ˮ��
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ά�޷���id
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
        /// ����
        /// </summary>
        public int PartNum
        {
            set { _partnum = value; }
            get { return _partnum; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public decimal PartFee
        {
            set { _partfee = value; }
            get { return _partfee; }
        }
        /// <summary>
        /// �ۺ��
        /// </summary>
        public decimal PartFee_rebate
        {
            get { return _partfee_rebate; }
            set { _partfee_rebate = value; }
        }
        #endregion Model

    }
    /// <summary>
    /// ʵ����repair_Scheme_Item ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ����
        /// </summary>
        public decimal ItemNat
        {
            get { return _itemnat; }
            set { _itemnat = value; }
        }


        /// <summary>
        /// ��ˮ��
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ά�޷���id
        /// </summary>
        public int SchemeId
        {
            set { _schemeid = value; }
            get { return _schemeid; }
        }
        /// <summary>
        /// ��ĿID
        /// </summary>
        public int ItemId
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int ItemNum
        {
            set { _itemnum = value; }
            get { return _itemnum; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public decimal ItemFee
        {
            set { _itemfee = value; }
            get { return _itemfee; }
        }
        /// <summary>
        /// �ۺ��
        /// </summary>
        public decimal ItemFee_rebate
        {
            get { return _itemfee_rebate; }
            set { _itemfee_rebate = value; }
        }
        #endregion Model
    }
}

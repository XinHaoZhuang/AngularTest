using System;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// ʵ����proj_Receipts ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class proj_Receipts
    {
        public proj_Receipts()
        { }
        #region Model
        private int _id;
        private int _contractid;
        private DateTime _receivedate = DateTime.Now;
        private int _settlementtypeid = 0;
        private decimal _receivenat = 0M;
        private string _memo;
        private bool _flagdel = false;
        private int _operaid = 0;
        private string _operaname;
        private DateTime _operatime = DateTime.Now;
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
        /// ʵ�ʻؿ�����
        /// </summary>
        public DateTime ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// �ؿ�����
        /// </summary>
        public int SettlementTypeId {
            set { _settlementtypeid = value; }
            get { return _settlementtypeid; }
        }
        /// <summary>
        /// ʵ�ʻؿ���
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
        #endregion Model

    }
}


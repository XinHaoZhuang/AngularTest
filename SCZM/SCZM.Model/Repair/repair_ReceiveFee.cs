using System;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类repair_ReceiveFee 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_ReceiveFee
    {
        public repair_ReceiveFee()
        { }
        #region Model
        private int _id;
        private int _intentionid = 0;
        private int _paytype = 0;
        private decimal _receivefee = 0M;
        private DateTime _receivedate = DateTime.Now;
        private int _flagdel = 0;
        private int _operadepid;
        private int _operaid;
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
        /// 维修意向id
        /// </summary>
        public int IntentionId
        {
            set { _intentionid = value; }
            get { return _intentionid; }
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        public int PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal ReceiveFee
        {
            set { _receivefee = value; }
            get { return _receivefee; }
        }
        /// <summary>
        /// 收款日期
        /// </summary>
        public DateTime ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
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
        #endregion Model

    }
}


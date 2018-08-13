using System;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类repair_SettlementList 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_SettlementList
    {
        public repair_SettlementList()
        { }
        #region Model
        private int _id;
        private string _settlementcode;
        private int _intentionid = 0;
        private int _settlementtypeid = 0;
        private decimal _settlementfee = 0M;
        private int _flagsx = 0;
        private decimal _settlementfee_rebate = 0M;
        private DateTime _settlementdate = DateTime.Now;
        private string _memo;
        private int _flagdel = 0;
        private int _operadepid;
        private int _operaid;
        private string _operaname;
        private DateTime _operatime = DateTime.Now;
        private string _attachmentid_settlement;
        private decimal _timefee = 0M;
        private decimal _partfee = 0M;
        //工时费   
        public decimal TimeFee
        {
            get { return _timefee; }
            set { _timefee = value; }
        }
        //零件费
        public decimal PartFee
        {
            get { return _partfee; }
            set { _partfee = value; }
        }
        //附件
        public string AttachmentId_Settlement
        {
            get { return _attachmentid_settlement; }
            set { _attachmentid_settlement = value; }
        }
        /// <summary>
        /// 流水号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 结算单号
        /// </summary>
        public string SettlementCode
        {
            set { _settlementcode = value; }
            get { return _settlementcode; }
        }
        /// <summary>
        /// 维修意向号
        /// </summary>
        public int IntentionId
        {
            set { _intentionid = value; }
            get { return _intentionid; }
        }
        /// <summary>
        /// 结算方式
        /// </summary>
        public int SettlementTypeId
        {
            set { _settlementtypeid = value; }
            get { return _settlementtypeid; }
        }
        /// <summary>
        /// 结算金额（总计）
        /// </summary>
        public decimal SettlementFee
        {
            set { _settlementfee = value; }
            get { return _settlementfee; }
        }
        /// <summary>
        /// 是否赊销
        /// </summary>
        public int FlagSX
        {
            set { _flagsx = value; }
            get { return _flagsx; }
        }
        /// <summary>
        /// 本次付款金额
        /// </summary>
        public decimal SettlementFee_rebate
        {
            set { _settlementfee_rebate = value; }
            get { return _settlementfee_rebate; }
        }
        /// <summary>
        /// 本次付款日期
        /// </summary>
        public DateTime SettlementDate
        {
            set { _settlementdate = value; }
            get { return _settlementdate; }
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
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


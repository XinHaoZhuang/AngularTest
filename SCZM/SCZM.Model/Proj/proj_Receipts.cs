using System;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// 实体类proj_Receipts 。(属性说明自动提取数据库字段的描述信息)
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
        /// 实际回款日期
        /// </summary>
        public DateTime ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 回款类型
        /// </summary>
        public int SettlementTypeId {
            set { _settlementtypeid = value; }
            get { return _settlementtypeid; }
        }
        /// <summary>
        /// 实际回款金额
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


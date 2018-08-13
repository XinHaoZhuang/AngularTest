using System;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// 实体类proj_Receipts 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class proj_FinalAccounts
    {
        public proj_FinalAccounts()
        { }
        #region Model
        private int _id;
        private int _contractid;
        private DateTime _finalaccountsdate = DateTime.Now;
        private decimal _finalaccountsnat = 0M;
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
        /// 项目合同ID
        /// </summary>
        public int ContractId
        {
            set { _contractid = value; }
            get { return _contractid; }
        }
        /// <summary>
        /// 决算日期
        /// </summary>
        public DateTime FinalAccountsDate
        {
            set { _finalaccountsdate = value; }
            get { return _finalaccountsdate; }
        }
        /// <summary>
        /// 决算金额
        /// </summary>
        public decimal FinalAccountsNat
        {
            set { _finalaccountsnat = value; }
            get { return _finalaccountsnat; }
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


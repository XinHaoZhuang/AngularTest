using System;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类repair_OutsourcingBill 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_OutsourcingBill
    {
        public repair_OutsourcingBill()
        { }
        #region Model
        private int _id;
        private int _intentionid = 0;
        private DateTime _happenddate = DateTime.Now;
        private string _address = ("");
        private int _feeitemid = 0;
        private string _plant = ("");
        private string _plantcontent;
        private decimal _payfee = 0M;
        private decimal _systemfee = 0M;
        private int _xssp = 0;
        private int _flagrepair = 0;

        
        private int _flagzb = 0;
        
        private int _flagother = 0;

        private DateTime _reimbursementdate = DateTime.Now;
        private string _memo;
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
        /// 维修意向号
        /// </summary>
        public int IntentionId
        {
            set { _intentionid = value; }
            get { return _intentionid; }
        }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappendDate
        {
            set { _happenddate = value; }
            get { return _happenddate; }
        }
        /// <summary>
        /// 地点 车间 现场
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 费用项目ID
        /// </summary>
        public int FeeItemId
        {
            set { _feeitemid = value; }
            get { return _feeitemid; }
        }
        /// <summary>
        /// 返修
        /// </summary>
        public int FlagRepair
        {
            get { return _flagrepair; }
            set { _flagrepair = value; }
        }
        /// <summary>
        /// 二手机整备
        /// </summary>
        public int FlagZB
        {
            get { return _flagzb; }
            set { _flagzb = value; }
        }
        /// <summary>
        /// 其他
        /// </summary>
        public int FlagOther
        {
            get { return _flagother; }
            set { _flagother = value; }
        }
        /// <summary>
        /// 加工厂
        /// </summary>
        public string Plant
        {
            set { _plant = value; }
            get { return _plant; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string PlantContent
        {
            set { _plantcontent = value; }
            get { return _plantcontent; }
        }
        /// <summary>
        /// 支付
        /// </summary>
        public decimal PayFee
        {
            set { _payfee = value; }
            get { return _payfee; }
        }
        /// <summary>
        /// 系统收取
        /// </summary>
        public decimal SystemFee
        {
            set { _systemfee = value; }
            get { return _systemfee; }
        }
        /// <summary>
        /// 是否小松索赔 0否 1是
        /// </summary>
        public int XsSp
        {
            set { _xssp = value; }
            get { return _xssp; }
        }
        /// <summary>
        /// 报销时间
        /// </summary>
        public DateTime ReimbursementDate
        {
            set { _reimbursementdate = value; }
            get { return _reimbursementdate; }
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


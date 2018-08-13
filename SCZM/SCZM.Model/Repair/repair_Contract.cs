using System;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类repair_Contract 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_Contract
    {
        public repair_Contract()
        { }
        #region Model
        private int _id;
        private int _intentionid = 0;
        private int _warrantyperiod = 0;
        private string _warrantycontent;
        private string _attachmentid_contract;
        private DateTime _contractdate = DateTime.Now;
        private string _contractcode;

        
       
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
        /// 保修期
        /// </summary>
        public int WarrantyPeriod
        {
            set { _warrantyperiod = value; }
            get { return _warrantyperiod; }
        }
        /// <summary>
        /// 保修内容
        /// </summary>
        public string WarrantyContent
        {
            set { _warrantycontent = value; }
            get { return _warrantycontent; }
        }
        /// <summary>
        /// 维修合同
        /// </summary>
        public string AttachmentId_Contract
        {
            set { _attachmentid_contract = value; }
            get { return _attachmentid_contract; }
        }
        /// <summary>
        /// 签约日期
        /// </summary>
        public DateTime ContractDate
        {
            get { return _contractdate; }
            set { _contractdate = value; }
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
        /// <summary>
        /// 维修合同号
        /// </summary>
        public string ContractCode
        {
            get { return _contractcode; }
            set { _contractcode = value; }
        }

        #endregion Model

    }
}


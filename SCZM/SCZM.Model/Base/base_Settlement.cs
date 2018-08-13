using System;
namespace SCZM.Model.Base
{
    /// <summary>
    /// 实体类base_Settlement 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class base_Settlement
    {
        public base_Settlement()
        { }
        #region Model
        private int _id;
        private int? _custtypeid;
        private string _settlementname;
        private int _flagdel = 0;
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
        /// 客户类型ID
        /// </summary>
        public int? CustTypeId
        {
            set { _custtypeid = value; }
            get { return _custtypeid; }
        }
        /// <summary>
        /// 结算方式名称
        /// </summary>
        public string SettlementName
        {
            set { _settlementname = value; }
            get { return _settlementname; }
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


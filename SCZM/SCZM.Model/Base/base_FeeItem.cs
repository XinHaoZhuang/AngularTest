using System;
namespace SCZM.Model.Base
{
    /// <summary>
    /// 实体类base_FeeItem 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class base_FeeItem
    {
        public base_FeeItem()
        { }
        #region Model
        private int _id;
        private string _feeitemname;
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
        /// 费用项目名称
        /// </summary>
        public string FeeItemName
        {
            set { _feeitemname = value; }
            get { return _feeitemname; }
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


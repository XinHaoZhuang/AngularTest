using System;
namespace SCZM.Model.System
{
    /// <summary>
    /// 实体类sys_Process_BillSet 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Process_BillSet
    {
        public sys_Process_BillSet()
        { }
        #region Model
        private int _id;
        private string _billsign;
        private string _tablename;
        private string _billname;
        private int _processid;
        private int? _supid;
        private int? _sortid;
        private bool _flaghistory = false;
        private bool _flagdel = false;
        private string _operaname;
        private DateTime? _operatime = DateTime.Now;
        /// <summary>
        /// 流水号

        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 单据标识
        /// </summary>
        public string BillSign
        {
            set { _billsign = value; }
            get { return _billsign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// 流水号

        /// </summary>
        public string BillName
        {
            set { _billname = value; }
            get { return _billname; }
        }
        /// <summary>
        /// 流程ID
        /// </summary>
        public int ProcessId
        {
            set { _processid = value; }
            get { return _processid; }
        }
        /// <summary>
        /// 上级ID
        /// </summary>
        public int? SupId
        {
            set { _supid = value; }
            get { return _supid; }
        }
        /// <summary>
        /// 排序号

        /// </summary>
        public int? SortId
        {
            set { _sortid = value; }
            get { return _sortid; }
        }
        /// <summary>
        /// 是否保存历史记录
        /// </summary>
        public bool FlagHistory
        {
            set { _flaghistory = value; }
            get { return _flaghistory; }
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
        public DateTime? OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        #endregion Model

    }
}


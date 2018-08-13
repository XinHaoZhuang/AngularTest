using System;
namespace SCZM.Model.System
{
    /// <summary>
    /// 实体类sys_Process_ExecHistory 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Process_ExecHistory
    {
        public sys_Process_ExecHistory()
        { }
        #region Model
        private int _id;
        private int _processid;
        private string _billsign;
        private int? _billid;
        private int? _orderid;
        private int? _postid;
        private string _postname;
        private string _auditorid;
        private string _auditorname;
        private DateTime? _auditortime = DateTime.Now;
        private int? _flagaudit;
        private string _auditoropinion;
        private bool _flagdel = false;
        /// <summary>
        /// 流水号

        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 单据标识
        /// </summary>
        public string BillSign
        {
            set { _billsign = value; }
            get { return _billsign; }
        }
        /// <summary>
        /// 单据ID
        /// </summary>
        public int? BillId
        {
            set { _billid = value; }
            get { return _billid; }
        }
        /// <summary>
        /// 审批次序
        /// </summary>
        public int? OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public int? PostId
        {
            set { _postid = value; }
            get { return _postid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostName
        {
            set { _postname = value; }
            get { return _postname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditorId
        {
            set { _auditorid = value; }
            get { return _auditorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditorName
        {
            set { _auditorname = value; }
            get { return _auditorname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AuditorTime
        {
            set { _auditortime = value; }
            get { return _auditortime; }
        }
        /// <summary>
        /// 审批状态 0取消审核1同意2不同意

        /// </summary>
        public int? FlagAudit
        {
            set { _flagaudit = value; }
            get { return _flagaudit; }
        }
        /// <summary>
        /// 意见
        /// </summary>
        public string AuditorOpinion
        {
            set { _auditoropinion = value; }
            get { return _auditoropinion; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        #endregion Model

    }
}


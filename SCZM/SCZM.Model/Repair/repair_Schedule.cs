using System;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类repair_Schedule 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_Schedule
    {
        public repair_Schedule()
        { }
        #region Model
        private int _id;
        private int _assignmentid;
        private int _assignmentprocedureid;
        private int _processid;
        private int _scheduletype = 0;
        private DateTime _scheduledate;
        private string _memo;
        private string _attachmentlist_schedule;
        private int _pausereason = -1;

  


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
        /// 派工id
        /// </summary>
        public int AssignmentId
        {
            set { _assignmentid = value; }
            get { return _assignmentid; }
        }
        /// <summary>
        /// 派工工序id
        /// </summary>
        public int AssignmentProcedureId
        {
            set { _assignmentprocedureid = value; }
            get { return _assignmentprocedureid; }
        }
        /// <summary>
        /// 工序id
        /// </summary>
        public int ProcedureId
        {
            set { _processid = value; }
            get { return _processid; }
        }
        /// <summary>
        /// 0未开始 1开始 2暂停 3完成
        /// </summary>
        public int ScheduleType
        {
            set { _scheduletype = value; }
            get { return _scheduletype; }
        }
        /// <summary>
        /// 反馈时间节点
        /// </summary>
        public DateTime ScheduleDate
        {
            set { _scheduledate = value; }
            get { return _scheduledate; }
        }
        /// <summary>
        /// 暂停原因 非暂停默认-1
        /// </summary>
        public int PauseReason
        {
            get { return _pausereason; }
            set { _pausereason = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachmentList_Schedule
        {
            get { return _attachmentlist_schedule; }
            set { _attachmentlist_schedule = value; }
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


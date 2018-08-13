using System;
using System.Collections.Generic;
namespace SCZM.Model.System
{
    /// <summary>
    /// 实体类sys_Process 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_Process
    {
        public sys_Process()
        { }
        #region Model
        private int _id;
        private string _processname;
        private string _memo;
        private bool _flaguse = true;
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
        /// 审批流名称

        /// </summary>
        public string ProcessName
        {
            set { _processname = value; }
            get { return _processname; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 使用标记
        /// </summary>
        public bool FlagUse
        {
            set { _flaguse = value; }
            get { return _flaguse; }
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

        private List<sys_ProcessDetail> _sys_processdetails;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<sys_ProcessDetail> sys_ProcessDetails
        {
            set { _sys_processdetails = value; }
            get { return _sys_processdetails; }
        }

    }
    /// <summary>
    /// 实体类sys_ProcessDetail 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class sys_ProcessDetail
    {
        public sys_ProcessDetail()
        { }
        #region Model
        private int _id;
        private int? _processid;
        private int? _orderid;
        private int? _postid;
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
        /// 审批流ID
        /// </summary>
        public int? ProcessId
        {
            set { _processid = value; }
            get { return _processid; }
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


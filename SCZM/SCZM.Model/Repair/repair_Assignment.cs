using System;
using System.Collections.Generic;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类Repair_Assignment 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Repair_Assignment
    {
        public Repair_Assignment()
        { }
        #region Model
        private int _id;
        private int _intentionid;
        private string _assignmentcode;
        private DateTime _assignmentdate;
        private DateTime _expectstartdate;
        private DateTime _expectcompletedate;
        private int _mainrepair;
        private string _assistrepair;
        private string _workcontent;
        private DateTime? _actualcompletedate;
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
        /// 意向id
        /// </summary>
        public int IntentionId
        {
            set { _intentionid = value; }
            get { return _intentionid; }
        }
        /// <summary>
        /// 派工单号
        /// </summary>
        public string AssignmentCode
        {
            set { _assignmentcode = value; }
            get { return _assignmentcode; }
        }
        /// <summary>
        /// 派工日期
        /// </summary>
        public DateTime AssignmentDate
        {
            set { _assignmentdate = value; }
            get { return _assignmentdate; }
        }
        /// <summary>
        /// 计划开始日期
        /// </summary>
        public DateTime ExpectStartDate
        {
            set { _expectstartdate = value; }
            get { return _expectstartdate; }
        }
        /// <summary>
        /// 计划完成日期
        /// </summary>
        public DateTime ExpectCompleteDate
        {
            set { _expectcompletedate = value; }
            get { return _expectcompletedate; }
        }
        /// <summary>
        /// 主修担当
        /// </summary>
        public int MainRepair
        {
            set { _mainrepair = value; }
            get { return _mainrepair; }
        }
        /// <summary>
        /// 辅修担当
        /// </summary>
        public string AssistRepair
        {
            set { _assistrepair = value; }
            get { return _assistrepair; }
        }
        /// <summary>
        /// 工作内容
        /// </summary>
        public string WorkContent
        {
            get { return _workcontent; }
            set { _workcontent = value; }
        }
        /// <summary>
        /// 实际完成日期
        /// </summary>
        public DateTime? ActualCompleteDate
        {
            set { _actualcompletedate = value; }
            get { return _actualcompletedate; }
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

        private List<Repair_Assignment_Procedure> _repair_assignment_procedure;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<Repair_Assignment_Procedure> Repair_Assignment_Procedure
        {
            set { _repair_assignment_procedure = value; }
            get { return _repair_assignment_procedure; }
        }

    }
    /// <summary>
    /// 实体类Repair_Assignment_Procedure 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Repair_Assignment_Procedure
    {
        public Repair_Assignment_Procedure()
        { }
        #region Model
        private int _id;
        private int _assignmentid;
        private int _procedureid;
        private decimal _num;
        private int _flagdel = 0;
        private string _workcontent = "";
        private int? _auditorid;
        private DateTime? _auditdate;
        private int? _auditstate;
        private decimal? _allnat;
        private decimal? _allnat_audit;
        private string _auditopinion;
        private int? _repairsecond_all;
        private int? _repairsecond_repair;
        private int? _repairsecond_pause;

        public int? RepairSecond_Pause
        {
            get { return _repairsecond_pause; }
            set { _repairsecond_pause = value; }
        }

        public int? RepairSecond_Repair
        {
            get { return _repairsecond_repair; }
            set { _repairsecond_repair = value; }
        }

        public int? RepairSecond_All
        {
            get { return _repairsecond_all; }
            set { _repairsecond_all = value; }
        }
        public decimal? AllNat
        {
            get { return _allnat; }
            set { _allnat = value; }
        }
        public string AuditOpinion
        {
            get { return _auditopinion; }
            set { _auditopinion = value; }
        }
        public int? AuditorId
        {
            get { return _auditorid; }
            set { _auditorid = value; }
        }
        public DateTime? AuditDate
        {
            get { return _auditdate; }
            set { _auditdate = value; }
        }
        public int? AuditState
        {
            get { return _auditstate; }
            set { _auditstate = value; }
        }
        public decimal? AllNat_Audit
        {
            get { return _allnat_audit; }
            set { _allnat_audit = value; }
        }
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
        /// 工序id
        /// </summary>
        public int ProcedureId
        {
            set { _procedureid = value; }
            get { return _procedureid; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Num
        {
            get { return _num; }
            set { _num = value; }
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
        /// 工作内容
        /// </summary>
        public string WorkContent
        {
            get { return _workcontent; }
            set { _workcontent = value; }
        }
        #endregion Model

    }
}


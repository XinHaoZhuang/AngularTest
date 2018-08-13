using System;
namespace SCZM.Model.Base
{
    /// <summary>
    /// 实体类base_RepairPackage 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class base_RepairPackage
    {
        public base_RepairPackage()
        { }
        #region Model
        private int _id;
        private int _machinemodelid;
        private string _packagename;
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
        /// 机型ID
        /// </summary>
        public int MachineModelId
        {
            set { _machinemodelid = value; }
            get { return _machinemodelid; }
        }
        /// <summary>
        /// 套包名称
        /// </summary>
        public string PackageName
        {
            set { _packagename = value; }
            get { return _packagename; }
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


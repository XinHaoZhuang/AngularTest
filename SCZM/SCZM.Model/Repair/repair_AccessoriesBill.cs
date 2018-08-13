using System;
using System.Collections.Generic;
namespace SCZM.Model.Repair
{
    /// <summary>
    /// 实体类repair_AccessoriesBill 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_AccessoriesBill
    {
        public repair_AccessoriesBill()
        { }
        #region Model
        private int _id;
        private int _intentionid;
        private DateTime _getaccessoriesdate;
        private string _username;
        private decimal _allfee;
        private decimal _allfee_actual;
        private int _billtype = 0;
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
        /// 领用时间
        /// </summary>
        public DateTime GetAccessoriesDate
        {
            set { _getaccessoriesdate = value; }
            get { return _getaccessoriesdate; }
        }
        /// <summary>
        /// 领用人
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal AllFee
        {
            set { _allfee = value; }
            get { return _allfee; }
        }
        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal AllFee_actual
        {
            set { _allfee_actual = value; }
            get { return _allfee_actual; }
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 明细类型
        /// </summary>
        public int BillType
        {
            get { return _billtype; }
            set { _billtype = value; }
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

        private List<repair_AccessoriesBill_Accessories> _repair_accessoriesbill_accessoriess;
        /// <summary>
        /// 子类 
        /// </summary>
        public List<repair_AccessoriesBill_Accessories> repair_AccessoriesBill_Accessoriess
        {
            set { _repair_accessoriesbill_accessoriess = value; }
            get { return _repair_accessoriesbill_accessoriess; }
        }

    }
    /// <summary>
    /// 实体类repair_AccessoriesBill_Accessories 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class repair_AccessoriesBill_Accessories
    {
        public repair_AccessoriesBill_Accessories()
        { }
        #region Model
        private int _id;
        private int _accessoriesbillid;
        private DateTime _accessoriesdate;
        private int _accessoriesid;
        private decimal _accessoriesnum;
        private decimal _accessoriesnat;
        private decimal _accessoriesfee;
        private string _accessoriesmemo;
        private int _flagdel;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 辅料台帐id
        /// </summary>
        public int AccessoriesBillId
        {
            set { _accessoriesbillid = value; }
            get { return _accessoriesbillid; }
        }
        /// <summary>
        /// 领取辅料日期
        /// </summary>
        public DateTime AccessoriesDate
        {
            set { _accessoriesdate = value; }
            get { return _accessoriesdate; }
        }
        /// <summary>
        /// 辅料ID
        /// </summary>
        public int AccessoriesId
        {
            set { _accessoriesid = value; }
            get { return _accessoriesid; }
        }
        /// <summary>
        /// 辅料数量
        /// </summary>
        public decimal AccessoriesNum
        {
            set { _accessoriesnum = value; }
            get { return _accessoriesnum; }
        }
        /// <summary>
        /// 辅料单价
        /// </summary>
        public decimal AccessoriesNat
        {
            set { _accessoriesnat = value; }
            get { return _accessoriesnat; }
        }
        /// <summary>
        /// 辅料金额
        /// </summary>
        public decimal AccessoriesFee
        {
            set { _accessoriesfee = value; }
            get { return _accessoriesfee; }
        }
        /// <summary>
        /// 备注/用途
        /// </summary>
        public string AccessoriesMemo
        {
            get { return _accessoriesmemo; }
            set { _accessoriesmemo = value; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        #endregion Model

    }
}


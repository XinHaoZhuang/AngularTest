using System;
namespace SCZM.Model.Proj
{
    /// <summary>
    /// ʵ����proj_Project ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class proj_Project
    {
        public proj_Project()
        { }
        #region Model
        private int _id;
        private string _projname;
        private int _custid;
        private int? _svcmanagerid;
        private int? _projmanagerid;
        private string _projmanagerhistory;
        private string _memo;
        private bool _flagdel = false;
        private int _operaid = 0;
        private string _operaname;
        private DateTime _operatime = DateTime.Now;
        /// <summary>
        /// ��ˮ��
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ProjName
        {
            set { _projname = value; }
            get { return _projname; }
        }
        /// <summary>
        /// �ͻ�ID
        /// </summary>
        public int CustId
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// ҵ����
        /// </summary>
        public int? SvcManagerId
        {
            set { _svcmanagerid = value; }
            get { return _svcmanagerid; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public int? ProjManagerId
        {
            set { _projmanagerid = value; }
            get { return _projmanagerid; }
        }
        /// <summary>
        /// ��Ŀ������ʷ
        /// </summary>
        public string ProjManagerHistory
        {
            set { _projmanagerhistory = value; }
            get { return _projmanagerhistory; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// ɾ�����
        /// </summary>
        public bool FlagDel
        {
            set { _flagdel = value; }
            get { return _flagdel; }
        }
        /// <summary>
        /// ������ID
        /// </summary>
        public int OperaId
        {
            set { _operaid = value; }
            get { return _operaid; }
        }
        /// <summary>
        /// ���ղ�����
        /// </summary>
        public string OperaName
        {
            set { _operaname = value; }
            get { return _operaname; }
        }
        /// <summary>
        /// ���ղ���ʱ��
        /// </summary>
        public DateTime OperaTime
        {
            set { _operatime = value; }
            get { return _operatime; }
        }
        #endregion Model

    }
}


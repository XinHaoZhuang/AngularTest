using System;
namespace SCZM.Model.Base
{
    /// <summary>
    /// ʵ����base_Company ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class base_Company
    {
        public base_Company()
        { }
        #region Model
        private int _id;
        private string _companyname;
        private int _sortid;
        private bool _flagdel = false;
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
        /// ��˾����
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public int SortId
        {
            set { _sortid = value; }
            get { return _sortid; }
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


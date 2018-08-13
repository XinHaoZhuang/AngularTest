using System;
namespace SCZM.Model.Proj
{
	/// <summary>
	/// ʵ����proj_SalesInvoice ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class proj_SalesInvoice
	{
		public proj_SalesInvoice()
		{}
		#region Model
		private int _id;
		private int _contractid;
		private string _invoicecode;
		private DateTime _invoicedate;
		private decimal? _invoicenat;
        private decimal? _taxrate;
		private string _memo;
		private string _attachmentid_invoice;
		private bool _flagdel= false;
		private int _operaid=0;
		private string _operaname;
		private DateTime _operatime= DateTime.Now;
		/// <summary>
		/// ��ˮ��
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ��ͬID
		/// </summary>
		public int ContractId
		{
			set{ _contractid=value;}
			get{return _contractid;}
		}
		/// <summary>
		/// ��Ʊ��
		/// </summary>
		public string InvoiceCode
		{
			set{ _invoicecode=value;}
			get{return _invoicecode;}
		}
		/// <summary>
		/// ��Ʊʱ��
		/// </summary>
		public DateTime InvoiceDate
		{
			set{ _invoicedate=value;}
			get{return _invoicedate;}
		}
		/// <summary>
		/// ��Ʊ���
		/// </summary>
		public decimal? InvoiceNat
		{
			set{ _invoicenat=value;}
			get{return _invoicenat;}
		}
        /// <summary>
        /// ˰��
        /// </summary>
        public decimal? TaxRate
        {
            set { _taxrate = value; }
            get { return _taxrate; }
        }
		/// <summary>
		/// ��ע
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// ����ID-��Ʊ
		/// </summary>
		public string AttachmentId_Invoice
		{
			set{ _attachmentid_invoice=value;}
			get{return _attachmentid_invoice;}
		}
		/// <summary>
		/// ɾ�����
		/// </summary>
		public bool FlagDel
		{
			set{ _flagdel=value;}
			get{return _flagdel;}
		}
		/// <summary>
		/// ������ID
		/// </summary>
		public int OperaId
		{
			set{ _operaid=value;}
			get{return _operaid;}
		}
		/// <summary>
		/// ���ղ�����
		/// </summary>
		public string OperaName
		{
			set{ _operaname=value;}
			get{return _operaname;}
		}
		/// <summary>
		/// ���ղ���ʱ��
		/// </summary>
		public DateTime OperaTime
		{
			set{ _operatime=value;}
			get{return _operatime;}
		}
		#endregion Model

	}
}


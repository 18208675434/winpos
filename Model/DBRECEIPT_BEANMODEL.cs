using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBRECEIPT_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBRECEIPT_BEANMODEL
	{
		public DBRECEIPT_BEANMODEL()
		{}
		#region Model
		private int __id;
		private string _cashier;
		private decimal _cashtotalamt;
		private int _endtime;
		private decimal _netsaleamt;
		private string _operatetimestr;
		private string _receiptdetail;
		private int _starttime;
		private decimal _totalpayment;
		private int _syn_time;
		private string _offline_receipt_id;
		private int _create_time;
		private string _create_sn;
		private string _create_url_ip;
		private int _version_code;
		private string _backups_sn;
		private string _backups_sn_time;
		/// <summary>
		/// 
		/// </summary>
		public int _id
		{
			set{ __id=value;}
			get{return __id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CASHIER
		{
			set{ _cashier=value;}
			get{return _cashier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal CASHTOTALAMT
		{
			set{ _cashtotalamt=value;}
			get{return _cashtotalamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ENDTIME
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal NETSALEAMT
		{
			set{ _netsaleamt=value;}
			get{return _netsaleamt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OPERATETIMESTR
		{
			set{ _operatetimestr=value;}
			get{return _operatetimestr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RECEIPTDETAIL
		{
			set{ _receiptdetail=value;}
			get{return _receiptdetail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int STARTTIME
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal TOTALPAYMENT
		{
			set{ _totalpayment=value;}
			get{return _totalpayment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SYN_TIME
		{
			set{ _syn_time=value;}
			get{return _syn_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OFFLINE_RECEIPT_ID
		{
			set{ _offline_receipt_id=value;}
			get{return _offline_receipt_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CREATE_TIME
		{
			set{ _create_time=value;}
			get{return _create_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATE_SN
		{
			set{ _create_sn=value;}
			get{return _create_sn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATE_URL_IP
		{
			set{ _create_url_ip=value;}
			get{return _create_url_ip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int VERSION_CODE
		{
			set{ _version_code=value;}
			get{return _version_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BACKUPS_SN
		{
			set{ _backups_sn=value;}
			get{return _backups_sn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BACKUPS_SN_TIME
		{
			set{ _backups_sn_time=value;}
			get{return _backups_sn_time;}
		}
		#endregion Model

	}
}


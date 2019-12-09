using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBORDER_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBORDER_BEANMODEL
	{
		public DBORDER_BEANMODEL()
		{}
		#region Model
		private int __id;
		private string _offlineorderid;
		private string _order_json;
		private int _orderat;
		private string _customerphone;
		private string _title;
		private decimal _pricetotal;
		private string _orderstatus;
		private string _synstatus;
		private int _syn_time;
		private int _orderstatusvalue;
		private int _refund_time;
		private int _create_time;
		private string _create_sn;
		private string _backups_sns;
		private string _backups_time;
		private string _backups_refund_sn;
		private string _backups_refund_time;
		private string _create_url_ip;
		private int _version_code;
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
		public string OFFLINEORDERID
		{
			set{ _offlineorderid=value;}
			get{return _offlineorderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ORDER_JSON
		{
			set{ _order_json=value;}
			get{return _order_json;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ORDERAT
		{
			set{ _orderat=value;}
			get{return _orderat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CUSTOMERPHONE
		{
			set{ _customerphone=value;}
			get{return _customerphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TITLE
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal PRICETOTAL
		{
			set{ _pricetotal=value;}
			get{return _pricetotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ORDERSTATUS
		{
			set{ _orderstatus=value;}
			get{return _orderstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SYNSTATUS
		{
			set{ _synstatus=value;}
			get{return _synstatus;}
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
		public int ORDERSTATUSVALUE
		{
			set{ _orderstatusvalue=value;}
			get{return _orderstatusvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int REFUND_TIME
		{
			set{ _refund_time=value;}
			get{return _refund_time;}
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
		public string BACKUPS_SNS
		{
			set{ _backups_sns=value;}
			get{return _backups_sns;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BACKUPS_TIME
		{
			set{ _backups_time=value;}
			get{return _backups_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BACKUPS_REFUND_SN
		{
			set{ _backups_refund_sn=value;}
			get{return _backups_refund_sn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BACKUPS_REFUND_TIME
		{
			set{ _backups_refund_time=value;}
			get{return _backups_refund_time;}
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
		#endregion Model

	}
}


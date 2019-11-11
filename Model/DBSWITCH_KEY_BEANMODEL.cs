using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBSWITCH_KEY_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBSWITCH_KEY_BEANMODEL
	{
		public DBSWITCH_KEY_BEANMODEL()
		{}
		#region Model
        private Int64 __id;
		private string _shopid;
		private string _skucode;
		private string _skuname;
		private string _scaleip;
		private string _scaletype;
		private string _scaletypename;
		private string _keyplanname;
		private string _keyno;
		private string _pno;
		private string _yno;
		private string _xno;
		private int _syn_time;
		private int _error_time;
		private int _status;
		private string _create_url_ip;
		private int _create_time;
		/// <summary>
		/// 
		/// </summary>
		public Int64 _id
		{
			set{ __id=value;}
			get{return __id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SHOPID
		{
			set{ _shopid=value;}
			get{return _shopid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SKUCODE
		{
			set{ _skucode=value;}
			get{return _skucode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SKUNAME
		{
			set{ _skuname=value;}
			get{return _skuname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SCALEIP
		{
			set{ _scaleip=value;}
			get{return _scaleip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SCALETYPE
		{
			set{ _scaletype=value;}
			get{return _scaletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SCALETYPENAME
		{
			set{ _scaletypename=value;}
			get{return _scaletypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KEYPLANNAME
		{
			set{ _keyplanname=value;}
			get{return _keyplanname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KEYNO
		{
			set{ _keyno=value;}
			get{return _keyno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PNO
		{
			set{ _pno=value;}
			get{return _pno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string YNO
		{
			set{ _yno=value;}
			get{return _yno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XNO
		{
			set{ _xno=value;}
			get{return _xno;}
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
		public int ERROR_TIME
		{
			set{ _error_time=value;}
			get{return _error_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int STATUS
		{
			set{ _status=value;}
			get{return _status;}
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
		public int CREATE_TIME
		{
			set{ _create_time=value;}
			get{return _create_time;}
		}
		#endregion Model

	}
}


using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBEXPENSE_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBEXPENSE_BEANMODEL
	{
		public DBEXPENSE_BEANMODEL()
		{}
		#region Model
		private int __id;
		private int _createat;
		private string _createurlip;
		private string _devicecode;
		private string _devicesn;
		private decimal _expensefee;
		private string _expenseid;
		private string _offlinerecordid;
		private string _saleclerkphone;
		private string _createby;
		private string _shopid;
		private string _expensename;
		private string _type;
		private int _versioncode;
		private int _syntime;
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
		public int CREATEAT
		{
			set{ _createat=value;}
			get{return _createat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATEURLIP
		{
			set{ _createurlip=value;}
			get{return _createurlip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DEVICECODE
		{
			set{ _devicecode=value;}
			get{return _devicecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DEVICESN
		{
			set{ _devicesn=value;}
			get{return _devicesn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal EXPENSEFEE
		{
			set{ _expensefee=value;}
			get{return _expensefee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EXPENSEID
		{
			set{ _expenseid=value;}
			get{return _expenseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OFFLINERECORDID
		{
			set{ _offlinerecordid=value;}
			get{return _offlinerecordid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SALECLERKPHONE
		{
			set{ _saleclerkphone=value;}
			get{return _saleclerkphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATEBY
		{
			set{ _createby=value;}
			get{return _createby;}
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
		public string EXPENSENAME
		{
			set{ _expensename=value;}
			get{return _expensename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TYPE
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int VERSIONCODE
		{
			set{ _versioncode=value;}
			get{return _versioncode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SYNTIME
		{
			set{ _syntime=value;}
			get{return _syntime;}
		}
		#endregion Model

	}
}


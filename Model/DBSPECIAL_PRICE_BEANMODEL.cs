using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBSPECIAL_PRICE_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBSPECIAL_PRICE_BEANMODEL
	{
		public DBSPECIAL_PRICE_BEANMODEL()
		{}
		#region Model
		private int __id;
		private string _shopid;
		private string _skucode;
		private decimal _price;
		private int _status;
		private string _date;
		private string _create_url_ip;
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
		public decimal PRICE
		{
			set{ _price=value;}
			get{return _price;}
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
		public string DATE
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATE_URL_IP
		{
			set{ _create_url_ip=value;}
			get{return _create_url_ip;}
		}
		#endregion Model

	}
}


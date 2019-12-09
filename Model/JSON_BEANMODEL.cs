using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// JSON_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class JSON_BEANMODEL
	{
		public JSON_BEANMODEL()
		{}
		#region Model
		private int __id;
		private string _json;
		private string _devicesn;
		private string _user_id;
		private string _condition;
		private string _create_time;
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
		public string JSON
		{
			set{ _json=value;}
			get{return _json;}
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
		public string USER_ID
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CONDITION
		{
			set{ _condition=value;}
			get{return _condition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATE_TIME
		{
			set{ _create_time=value;}
			get{return _create_time;}
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


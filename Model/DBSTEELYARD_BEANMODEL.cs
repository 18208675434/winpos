using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBSTEELYARD_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBSTEELYARD_BEANMODEL
	{
		public DBSTEELYARD_BEANMODEL()
		{}
		#region Model
		private int __id;
		private string _no;
		private string _name;
		private string _ip;
		private string _type;
		private int _syn_time;
		private string _user_name;
		private string _password;
		private int _create_time;
		private string _create_sn;
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
		public string NO
		{
			set{ _no=value;}
			get{return _no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NAME
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IP
		{
			set{ _ip=value;}
			get{return _ip;}
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
		public int SYN_TIME
		{
			set{ _syn_time=value;}
			get{return _syn_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string USER_NAME
		{
			set{ _user_name=value;}
			get{return _user_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PASSWORD
		{
			set{ _password=value;}
			get{return _password;}
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
		#endregion Model

	}
}


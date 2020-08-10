using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// SYSTEM_USER_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SYSTEM_USER_BEANMODEL
	{
		public SYSTEM_USER_BEANMODEL()
		{}
		#region Model
		private int __id;
		private string _loginaccount;
		private string _nickname;
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
		public string LOGINACCOUNT
		{
			set{ _loginaccount=value;}
			get{return _loginaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NICKNAME
		{
			set{ _nickname=value;}
			get{return _nickname;}
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


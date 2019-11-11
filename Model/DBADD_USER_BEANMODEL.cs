using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBADD_USER_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBADD_USER_BEANMODEL
	{
		public DBADD_USER_BEANMODEL()
		{}
		#region Model
		private int __id;
		private string _loginaccount;
		private string _nickname;
		private int _create_time;
		private string _create_sn;
		private string _create_url_ip;
		private int _version_code;
		private string _backups_sn;
		private string _backups_time;
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
		public string BACKUPS_TIME
		{
			set{ _backups_time=value;}
			get{return _backups_time;}
		}
		#endregion Model

	}
}


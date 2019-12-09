using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBTRANSFER_SCALE_STATUS_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBTRANSFER_SCALE_STATUS_BEANMODEL
	{
		public DBTRANSFER_SCALE_STATUS_BEANMODEL()
		{}
		#region Model
		private  Int64 __id;
		private Int64 _sys_success_time;
		private Int64 _sys_error_time;
		private Int64 _status;
		private string _scaleip;
		private string _create_url_ip;
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
		public Int64 SYS_SUCCESS_TIME
		{
			set{ _sys_success_time=value;}
			get{return _sys_success_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Int64 SYS_ERROR_TIME
		{
			set{ _sys_error_time=value;}
			get{return _sys_error_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Int64 STATUS
		{
			set{ _status=value;}
			get{return _status;}
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
		public string CREATE_URL_IP
		{
			set{ _create_url_ip=value;}
			get{return _create_url_ip;}
		}
		#endregion Model

	}
}


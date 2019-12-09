using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBFEE_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBFEE_BEANMODEL
	{
		public DBFEE_BEANMODEL()
		{}
		#region Model
		private int __id;
		private string _fee_id;
		private string _discription;
		private string _inputflag;
		private string _visibleflag;
		private string _type;
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
		public string FEE_ID
		{
			set{ _fee_id=value;}
			get{return _fee_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DISCRIPTION
		{
			set{ _discription=value;}
			get{return _discription;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string INPUTFLAG
		{
			set{ _inputflag=value;}
			get{return _inputflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VISIBLEFLAG
		{
			set{ _visibleflag=value;}
			get{return _visibleflag;}
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
		public string CREATE_URL_IP
		{
			set{ _create_url_ip=value;}
			get{return _create_url_ip;}
		}
		#endregion Model

	}
}


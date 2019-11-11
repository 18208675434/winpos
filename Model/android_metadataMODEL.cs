using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// android_metadata:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class android_metadataMODEL
	{
		public android_metadataMODEL()
		{}
		#region Model
		private string _locale;
		/// <summary>
		/// 
		/// </summary>
		public string locale
		{
			set{ _locale=value;}
			get{return _locale;}
		}
		#endregion Model

	}
}


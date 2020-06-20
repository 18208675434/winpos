using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model
{	/// <summary>
    /// DBSCALE_KEY_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class DBSCALE_KEY_BEANMODEL
    {
        public DBSCALE_KEY_BEANMODEL()
        { }
        #region Model
        private int __id;
        private string _tempid;
        private string _tempname;
        private string _scalesid;
        private string _ip;
        private string _port;
        private string _shopid;
        private int _settingpage;
        private string _scalestype;
        private int _xpos;
        private int _ypos;
        private int _nopos;
        private string _skucode;
        private string _create_url_ip;
        private long _syn_time;
        private long _error_time;
        private int _status;
        private int _scalesrow;
        /// <summary>
        /// 
        /// </summary>
        public int _id
        {
            set { __id = value; }
            get { return __id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TEMPID
        {
            set { _tempid = value; }
            get { return _tempid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TEMPNAME
        {
            set { _tempname = value; }
            get { return _tempname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SCALESID
        {
            set { _scalesid = value; }
            get { return _scalesid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PORT
        {
            set { _port = value; }
            get { return _port; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SHOPID
        {
            set { _shopid = value; }
            get { return _shopid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SETTINGPAGE
        {
            set { _settingpage = value; }
            get { return _settingpage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SCALESTYPE
        {
            set { _scalestype = value; }
            get { return _scalestype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int XPOS
        {
            set { _xpos = value; }
            get { return _xpos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int YPOS
        {
            set { _ypos = value; }
            get { return _ypos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NOPOS
        {
            set { _nopos = value; }
            get { return _nopos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SKUCODE
        {
            set { _skucode = value; }
            get { return _skucode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATE_URL_IP
        {
            set { _create_url_ip = value; }
            get { return _create_url_ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long SYN_TIME
        {
            set { _syn_time = value; }
            get { return _syn_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ERROR_TIME
        {
            set { _error_time = value; }
            get { return _error_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int STATUS
        {
            set { _status = value; }
            get { return _status; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int SCALESROW
        {
            set { _scalesrow = value; }
            get { return _scalesrow; }
        }
        #endregion Model

    }
}

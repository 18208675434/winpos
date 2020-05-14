using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model
{/// <summary>
    /// DBPROMOTION_CACHE_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class DBPROMOTION_CACHE_BEANMODEL
    {
        public DBPROMOTION_CACHE_BEANMODEL()
        { }
        #region Model
        private Int64 __id;
        private string _code;
        private Int64 _rank;
        private string _tenantscope;
        private string _districtscope;
        private string _shopscope;
        private string _promotype;
        private string _promosubtype;
        private string _promoaction;
        private string _eligibilitycondition;
        private string _promoconditiontype;
        private string _promoconditioncontext;
        private string _promoactioncontext;
        private string _name;
        private string _description;
        private string _tag;
        private Int64 _canbecombined;
        private Int64 _enabled;
        private Int64 _salechannel;
        private Int64 _enabledfrom;
        private Int64 _enabledto;
        private string _enabledtimeinfo;
        private Int64 _createdat;
        private Int64 _updatedat;
        private string _createdby;
        private string _updatedby;
        private Int64 _fromouter;
        private string _outercode;
        private string _costcenterinfo;
        private string _costrulecontext;
        private Int64 _canmixcoupon;
        private Int64 _ordersubtype;
        private string _availablecategory;
        private string _tenantid;
        private string _shopid;
        private string _create_url_ip;
        private Int64 _onlyuseinoriginal;





        
        /// <summary>
        /// 
        /// </summary>
        public Int64 _id
        {
            set { __id = value; }
            get { return __id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CODE
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 RANK
        {
            set { _rank = value; }
            get { return _rank; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TENANTSCOPE
        {
            set { _tenantscope = value; }
            get { return _tenantscope; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DISTRICTSCOPE
        {
            set { _districtscope = value; }
            get { return _districtscope; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SHOPSCOPE
        {
            set { _shopscope = value; }
            get { return _shopscope; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROMOTYPE
        {
            set { _promotype = value; }
            get { return _promotype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROMOSUBTYPE
        {
            set { _promosubtype = value; }
            get { return _promosubtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROMOACTION
        {
            set { _promoaction = value; }
            get { return _promoaction; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ELIGIBILITYCONDITION
        {
            set { _eligibilitycondition = value; }
            get { return _eligibilitycondition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROMOCONDITIONTYPE
        {
            set { _promoconditiontype = value; }
            get { return _promoconditiontype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROMOCONDITIONCONTEXT
        {
            set { _promoconditioncontext = value; }
            get { return _promoconditioncontext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROMOACTIONCONTEXT
        {
            set { _promoactioncontext = value; }
            get { return _promoactioncontext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NAME
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DESCRIPTION
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TAG
        {
            set { _tag = value; }
            get { return _tag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 CANBECOMBINED
        {
            set { _canbecombined = value; }
            get { return _canbecombined; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 ENABLED
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 SALECHANNEL
        {
            set { _salechannel = value; }
            get { return _salechannel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 ENABLEDFROM
        {
            set { _enabledfrom = value; }
            get { return _enabledfrom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 ENABLEDTO
        {
            set { _enabledto = value; }
            get { return _enabledto; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ENABLEDTIMEINFO
        {
            set { _enabledtimeinfo = value; }
            get { return _enabledtimeinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 CREATEDAT
        {
            set { _createdat = value; }
            get { return _createdat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 UPDATEDAT
        {
            set { _updatedat = value; }
            get { return _updatedat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATEDBY
        {
            set { _createdby = value; }
            get { return _createdby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UPDATEDBY
        {
            set { _updatedby = value; }
            get { return _updatedby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 FROMOUTER
        {
            set { _fromouter = value; }
            get { return _fromouter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OUTERCODE
        {
            set { _outercode = value; }
            get { return _outercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COSTCENTERINFO
        {
            set { _costcenterinfo = value; }
            get { return _costcenterinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COSTRULECONTEXT
        {
            set { _costrulecontext = value; }
            get { return _costrulecontext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 CANMIXCOUPON
        {
            set { _canmixcoupon = value; }
            get { return _canmixcoupon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 ORDERSUBTYPE
        {
            set { _ordersubtype = value; }
            get { return _ordersubtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AVAILABLECATEGORY
        {
            set { _availablecategory = value; }
            get { return _availablecategory; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TENANTID
        {
            set { _tenantid = value; }
            get { return _tenantid; }
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
        public string CREATE_URL_IP
        {
            set { _create_url_ip = value; }
            get { return _create_url_ip; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Int64 ONLYUSEINORIGINAL
        {
            set { _onlyuseinoriginal = value; }
            get { return _onlyuseinoriginal; }
        }
        #endregion Model



        //ADD 0511
        private Int64 _onlymember;
        private string _membertags;
        private Int64 _purchaselimit;
        public Int64 ONLYMEMBER
        {
            set { _onlymember = value; }
            get { return _onlymember; }
        }

        public Int64 PURCHASELIMIT
        {
            set { _purchaselimit = value; }
            get { return _purchaselimit; }
        }

        public string MEMBERTAGS
        {
            set { _membertags = value; }
            get { return _membertags; }
        }

    }
}

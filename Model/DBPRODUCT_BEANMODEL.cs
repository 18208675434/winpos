using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DBPRODUCT_BEAN:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DBPRODUCT_BEANMODEL
	{
		public DBPRODUCT_BEANMODEL()
		{}
        #region Model
        private Int64 __id;
        private string _skucode;
        private string _goods_id;
        private string _categoryid;
        private string _categoryname;
        private decimal _originprice;
        private decimal _saleprice;
        private decimal _special_price;
        private decimal _totalstockqty;
        private string _salesunit;
        private string _shopid;
        private string _tenantid;
        private string _title;
        private string _skuname;
        private string _barcode;
        private string _specdesc;
        private Int64 _goodstagid;
        private Int64 _weightflag;
        private decimal _num;
        private decimal _specnum;
        private Int64 _spectype;
        private string _pricetag;
        private Int64 _pricetagid;
        private string _create_url_ip;
        private string _tagformat;
        private string _barcodeformat;
        private Int64 _bestdays;
        private string _spinfo;
        private string _qrcodecontent;
        private string _ingredient;
        private string _location;
        private string _spec;
        private string _store_type;
        private string _company;
        private string _remark;
        private string _specialmessage;
        private string _firstcategoryid;
        private string _firstcategoryname;
        private string _secondcategoryid;
        private string _secondcategoryname;
        private string _panelflag;
        private Int64 _panelshowflag;
        private string _mainimg;
        private Int64 _status;
        private Int64 _special_status;
        private Int64 _is_query_barcode;
        private Int64 _createdat;
        private Int64 _salecount;
        private string _innerbarcode;
        private string _first_letter;
        private string _all_first_letter;
        private Int64 _shelflife;
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
        public string SKUCODE
        {
            set { _skucode = value; }
            get { return _skucode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GOODS_ID
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CATEGORYID
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CATEGORYNAME
        {
            set { _categoryname = value; }
            get { return _categoryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ORIGINPRICE
        {
            set { _originprice = value; }
            get { return _originprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SALEPRICE
        {
            set { _saleprice = value; }
            get { return _saleprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SPECIAL_PRICE
        {
            set { _special_price = value; }
            get { return _special_price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TOTALSTOCKQTY
        {
            set { _totalstockqty = value; }
            get { return _totalstockqty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SALESUNIT
        {
            set { _salesunit = value; }
            get { return _salesunit; }
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
        public string TENANTID
        {
            set { _tenantid = value; }
            get { return _tenantid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TITLE
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SKUNAME
        {
            set { _skuname = value; }
            get { return _skuname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BARCODE
        {
            set { _barcode = value; }
            get { return _barcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPECDESC
        {
            set { _specdesc = value; }
            get { return _specdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 GOODSTAGID
        {
            set { _goodstagid = value; }
            get { return _goodstagid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 WEIGHTFLAG
        {
            set { _weightflag = value; }
            get { return _weightflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal NUM
        {
            set { _num = value; }
            get { return _num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SPECNUM
        {
            set { _specnum = value; }
            get { return _specnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 SPECTYPE
        {
            set { _spectype = value; }
            get { return _spectype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRICETAG
        {
            set { _pricetag = value; }
            get { return _pricetag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 PRICETAGID
        {
            set { _pricetagid = value; }
            get { return _pricetagid; }
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
        public string TAGFORMAT
        {
            set { _tagformat = value; }
            get { return _tagformat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BARCODEFORMAT
        {
            set { _barcodeformat = value; }
            get { return _barcodeformat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 BESTDAYS
        {
            set { _bestdays = value; }
            get { return _bestdays; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPINFO
        {
            set { _spinfo = value; }
            get { return _spinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QRCODECONTENT
        {
            set { _qrcodecontent = value; }
            get { return _qrcodecontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string INGREDIENT
        {
            set { _ingredient = value; }
            get { return _ingredient; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LOCATION
        {
            set { _location = value; }
            get { return _location; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPEC
        {
            set { _spec = value; }
            get { return _spec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STORE_TYPE
        {
            set { _store_type = value; }
            get { return _store_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COMPANY
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARK
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPECIALMESSAGE
        {
            set { _specialmessage = value; }
            get { return _specialmessage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FIRSTCATEGORYID
        {
            set { _firstcategoryid = value; }
            get { return _firstcategoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FIRSTCATEGORYNAME
        {
            set { _firstcategoryname = value; }
            get { return _firstcategoryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SECONDCATEGORYID
        {
            set { _secondcategoryid = value; }
            get { return _secondcategoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SECONDCATEGORYNAME
        {
            set { _secondcategoryname = value; }
            get { return _secondcategoryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PANELFLAG
        {
            set { _panelflag = value; }
            get { return _panelflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 PANELSHOWFLAG
        {
            set { _panelshowflag = value; }
            get { return _panelshowflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MAINIMG
        {
            set { _mainimg = value; }
            get { return _mainimg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 SPECIAL_STATUS
        {
            set { _special_status = value; }
            get { return _special_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 IS_QUERY_BARCODE
        {
            set { _is_query_barcode = value; }
            get { return _is_query_barcode; }
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
        public Int64 SALECOUNT
        {
            set { _salecount = value; }
            get { return _salecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string INNERBARCODE
        {
            set { _innerbarcode = value; }
            get { return _innerbarcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FIRST_LETTER
        {
            set { _first_letter = value; }
            get { return _first_letter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ALL_FIRST_LETTER
        {
            set { _all_first_letter = value; }
            get { return _all_first_letter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int64 SHELFLIFE
        {
            set { _shelflife = value; }
            get { return _shelflife; }
        }
        #endregion Model

	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model
{
    public class UpdateSqlLite
    {

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

      
    }

    public class ColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DateType { get; set; }
    }
}

using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class AllProduct
{
    public List<DBPRODUCT_BEANMODEL> rows { get; set; }
public string total { get; set; }
public int page { get; set; }
public int size { get; set; }
public long timestamp { get; set; }
}

}

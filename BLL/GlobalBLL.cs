using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.BLL
{

  

    public partial class GlobalBLL
    {
        private readonly Maticsoft.DAL.GlobalDAL dal = new DAL.GlobalDAL();

        public bool UpdateDbProduct()
        {
            return dal.UpdateDbProduct();
        } 
    }
}

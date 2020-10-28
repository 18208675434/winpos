using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DAL
{
    public class GlobalDAL
    {


        public bool UpdateSqlLite(List<UpdateSqlLite> lstdata){
            try
            {
                //string isexitsproduct = "select sql from sqlite_master where type = 'table' and name = 'DBPRODUCT_BEAN'";

                //object objproduct = Maticsoft.DBUtility.DbHelperSQLite.GetSingle(isexitsproduct);

                //if (!objproduct.ToString().Contains("SCALEFLAG"))
                //{
                //    lststring.Add("ALTER TABLE DBPRODUCT_BEAN ADD COLUMN 'SCALEFLAG' INTEGER");
                //    needadd = true;
                //}
                //if (needadd)
                //{
                //    Maticsoft.DBUtility.DbHelperSQLite.ExecuteSqlTran(lststring);

                //}

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        } 

        public bool UpdateDbProduct(){

            try{  
                
               System.Collections.ArrayList LstSql = new System.Collections.ArrayList();
                bool needUpdate =false;

                string isexitsproduct = "select sql from sqlite_master where type = 'table' and name = 'DBPRODUCT_BEAN'";

                object objproduct = Maticsoft.DBUtility.DbHelperSQLite.GetSingle(isexitsproduct);

                if (!objproduct.ToString().Contains("MEMBERPRICE"))
                {
                    LstSql.Add("delete from DBPRODUCT_BEAN");
                    LstSql.Add("ALTER TABLE DBPRODUCT_BEAN ADD COLUMN 'MEMBERPRICE' REAL");
                    needUpdate = true;
                }

                if (!objproduct.ToString().Contains("LOCALSTATUS"))
                {
                    LstSql.Add("ALTER TABLE DBPRODUCT_BEAN ADD COLUMN 'LOCALSTATUS' INTEGER");
                    needUpdate = true;
                }

                if (needUpdate)
                {
                    Maticsoft.DBUtility.DbHelperSQLite.ExecuteSqlTran(LstSql);

                }

                return true;

            }catch{
                return false;
            }

        }
      


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MDL;
using System.Data.SqlClient;

namespace DAL
{
    public  class ProductsDAL
    {

        /// <summary>
        /// 根据条件查询查询商品
        /// </summary>
        /// <param name="PTID"></param>
        /// <param name="JP"></param>
        /// <returns></returns>
        public static List<ProductsMDL> selectProducts(int  PTID,string JP)
        {
            string sql = "select * from dbo.Products p join dbo.ProductType t on p.PTID=t.PTID where 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (PTID!=0)
            {
                sql += " and p.PTID=@PTID ";
                sp.Add(new SqlParameter("@PTID", PTID));
                
            }
            if (JP!="")
            {
                sql += " and ProductJP like @ProductJP";
                sp.Add(new SqlParameter("@ProductJP", "%"+JP+"%"));
            }
          
          
                                
                               
                               
           SqlDataReader dr= DBhelper.MySqlDataReader(sql,sp.ToArray());
           List<ProductsMDL> list = new List<ProductsMDL>();
           while (dr.Read()) {
               ProductsMDL pd = new ProductsMDL();
               pd.ProductID = Convert.ToInt32(dr[0]);
               pd.ProductName = dr[1].ToString();
               pd.PTID = Convert.ToInt32(dr[2]);
               pd.ProductJP = dr[3].ToString();
               pd.ProductPrice = Convert.ToDouble(dr[4]);
               pd.ProductIntroduction = dr[5].ToString();
               pd.ProductPic = dr[6].ToString();
               pd.PT = dr[8].ToString();
               list.Add(pd);
           
           }
           dr.Close();
           return list;
        }
        /// <summary>
        /// 查询所有商品
        /// </summary>
        /// <param name="PTID"></param>
        /// <param name="JP"></param>
        /// <returns></returns>
        public static List<ProductsMDL> selectProduct(string ProductJP)
        {
            string sql = "select * from dbo.Products where 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (ProductJP!="")
            {
                sql += " and ProductJP like @ProductJP";
                sp.Add(new SqlParameter("@ProductJP", "%"+ProductJP+"%"));
            }

            SqlDataReader dr = DBhelper.MySqlDataReader(sql, sp.ToArray());
            List<ProductsMDL> list = new List<ProductsMDL>();
            while (dr.Read())
            {
                ProductsMDL pd = new ProductsMDL();
                pd.ProductID = Convert.ToInt32(dr[0]);
                pd.ProductName = dr[1].ToString();
                pd.PTID = Convert.ToInt32(dr[2]);
                pd.ProductJP = dr[3].ToString();
                pd.ProductPrice = Convert.ToDouble(dr[4]);
                pd.ProductPic = dr[6].ToString();
                pd.ProductIntroduction = dr[5].ToString();
                list.Add(pd);

            }
            dr.Close();
            return list;
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="p"></param>
        /// <returns>几行受影响</returns>
        public static int AddProducts( ProductsMDL p) {
            string sql = "insert into Products values(@ProductName,@PTID,@ProductJP,@ProductPrice,@ProductIntroduction,@ProductPic)";
            SqlParameter[] sp ={
                               new SqlParameter("@ProductName",p.ProductName),
                               new SqlParameter("@PTID",p.PTID),
                               new SqlParameter("@ProductJP",p.ProductJP),
                               new SqlParameter("@ProductPrice",p.ProductPrice),
                               new SqlParameter("@ProductIntroduction",p.ProductIntroduction),
                               new SqlParameter("@ProductPic",p.ProductPic)


                               
                               };
         return   DBhelper.MyExecuteNonQuery(sql,sp);
        
        
        }
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int UpdateProducts(ProductsMDL p)
        {
            string sql = @" update Products 
 set ProductName=@ProductName,PTID=@PTID,ProductPrice=@ProductPrice,ProductIntroduction=@ProductIntroduction,ProductPic=@ProductPic
 where ProductID=@ProductID";
            SqlParameter[] sp = { 
                                new SqlParameter("@ProductName",p.ProductName),
                                new SqlParameter("@PTID",p.PTID),
                                new SqlParameter("@ProductPrice",p.ProductPrice),
                                new SqlParameter("@ProductID",p.ProductID),
                                 new SqlParameter("@ProductIntroduction",p.ProductIntroduction),
                                  new SqlParameter("@ProductPic",p.ProductPic)

                                };
            return DBhelper.MyExecuteNonQuery(sql,sp);



        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static int DeleteProducts(int ProductID) {
            string sql = "delete from Products where ProductID=@ProductID";
            SqlParameter [] sp = { 
                              new SqlParameter("@ProductID",ProductID)
                              
                              };
            return DBhelper.MyExecuteNonQuery(sql,sp);
            
        
        
        
        }
        public static DataTable GetPT()
        {
            string sql = "select ProductName as '商品名字',SUM(CDAmount) '销量' from ConsumerDetail c join Products p on c.ProdcutID=p.ProductID group by ProductName";
            return DBhelper.dd(sql, null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public  class ProductTypeDAL
    {
        /// <summary>
        /// 查询商品类型
        /// </summary>
        /// <returns></returns>
        public static List<ProductTypeMDL> selectProductType() {
            string sql = "select * from dbo.ProductType";
            SqlDataReader dr = DBhelper.MySqlDataReader(sql, null);
            List<ProductTypeMDL> list = new List<ProductTypeMDL>();

            while(dr.Read()){
                ProductTypeMDL pt = new ProductTypeMDL();
                pt.PTID = Convert.ToInt32(dr[0]);
                pt.PTName = dr[1].ToString();
                list.Add(pt);
              
            }
            
            dr.Close();
            return list;
        
        }
        /// <summary>
        /// combox高级绑定
        /// </summary>
        /// <returns>datatable</returns>
        public static DataTable selectcombox() {
            string sql = "select * from dbo.ProductType";
            DataTable dt= DBhelper.dd(sql, null);
            return dt;

            
        }
        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="p"></param>
        /// <returns>0,1是否执行成功</returns>
        public static int AddProductType(ProductTypeMDL p){
            string sql = "insert into dbo.ProductType values(@PTName)";
            SqlParameter [] sp={
                               new SqlParameter("@PTName",p.PTName)
                               };
      return DBhelper.MyExecuteNonQuery(sql,sp);
        
        }
        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int AlterProductType(ProductTypeMDL p)
        {
            string sql = "update  dbo.ProductType set PTName=@PTName where PTID=@PTID";
            SqlParameter[] sp ={
                               new SqlParameter("@PTName",p.PTName),
                                 new SqlParameter("@PTID",p.PTID)
                               };
            return DBhelper.MyExecuteNonQuery(sql, sp);
        
        
        }
        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="p">房间类型对象</param>
        /// <returns>受影响的行数</returns>
        public static int DeleteProductType(ProductTypeMDL p)
        {
            string sql = "delete from dbo.ProductType where PTID=@PTID";
            SqlParameter[] sp = { 
                                new SqlParameter("@PTID",p.PTID)
                                };
           return DBhelper.MyExecuteNonQuery(sql,sp);
        
        }
    }
}

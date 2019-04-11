using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDL;
using DAL;
using System.Data;

namespace BLL
{
    public class ProductsBLL
    {
        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="PTID"></param>
        /// <param name="JP"></param>
        /// <returns></returns>
        public static List<ProductsMDL> selectProducts(int PTID,string JP)
        {
            return ProductsDAL.selectProducts(PTID,JP);
        }
        /// <summary>
        /// 查询所有商品
        /// </summary>
        /// <param name="PTID"></param>
        /// <param name="JP"></param>
        /// <returns></returns>
        public static List<ProductsMDL> selectProduct(string ProductJP)
        {
            return ProductsDAL.selectProduct(ProductJP);
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int AddProducts(ProductsMDL p) {

            return ProductsDAL.AddProducts(p);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int UpdateProducts(ProductsMDL p) {
            return ProductsDAL.UpdateProducts(p);
        
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static int DeleteProducts(int ProductID) {

            return ProductsDAL.DeleteProducts(ProductID);
        }
        public static DataTable GetPT()
        {
            return ProductsDAL.GetPT();
        }
    }
}

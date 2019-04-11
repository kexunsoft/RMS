using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MDL;
using DAL;

namespace BLL
{
   
    public class ProductTypeBLL
    {
        /// <summary>
        /// 查询商品类型
        /// </summary>
        public static List<ProductTypeMDL> selectProductType() {
           return ProductTypeDAL.selectProductType();
           
        
        }
        /// <summary>
        /// combox高级绑定
        /// </summary>
        /// <returns></returns>
        public static DataTable selectcombox() {
            return ProductTypeDAL.selectcombox();
        }
        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int AddProductType(ProductTypeMDL p) {
           return ProductTypeDAL.AddProductType(p);
        
        
        }
        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int AlterProductType(ProductTypeMDL p)
        {
           return ProductTypeDAL.AlterProductType(p);
        
        }
          /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="p">房间类型对象</param>
        /// <returns>受影响的行数</returns>
        public static int DeleteProductType(ProductTypeMDL p)
        {
            return ProductTypeDAL.DeleteProductType(p);
        
        }
    }
}

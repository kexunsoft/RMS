
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MDL;
using System.Data.SqlClient;

namespace DAL
{
    public class ConsumerDetailsDAL
    {
        //获取菜单
        public static List<ConsumerDetails> GetCD(int TableID) {
            string sql = @"proc_GetCB";
            SqlParameter[] sp = { 
                                
                                new SqlParameter("@TableID",TableID)
                                };
            SqlDataReader dr=DBhelper.MySqlDataReaderProc(sql,sp);
            List<ConsumerDetails> list=new List<ConsumerDetails>();
            while(dr.Read()){
                ConsumerDetails cb = new ConsumerDetails();
                cb.TableID = Convert.ToInt32(dr[0]);
                cb.ProductName = dr[1].ToString();
                cb.CDAmount = Convert.ToInt32(dr[2]);
                cb.ProductPrice = Convert.ToDouble(dr[3]);
                cb.CBID = dr[4].ToString();
                cb.CBDate = dr[6].ToString();
                cb.CBJE = Convert.ToInt32(dr[2]) * Convert.ToDouble(dr[3]);
                cb.ProductID = dr[7].ToString();
                list.Add(cb);
            
            }
            dr.Close();
            return list;
        
        }
        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <returns></returns>
        public static int AddConsumerDetails(ConsumerDetails c) {
            string sql = "insert into dbo.ConsumerDetail values(@CBID,@ProdcutID,@CDPrice,@CDAmount,@CDSale,@CDMoney,@CDType,GETDATE())";
            SqlParameter[] sp = { 
                                new SqlParameter("@CBID",c.CBID),
                                new SqlParameter("@ProdcutID",c.ProductID),
                                new SqlParameter("@CDPrice",c.CDPrice),
                                new SqlParameter("@CDAmount",c.CDAmount),
                                new SqlParameter("@CDSale",c.CDSale),
                                new SqlParameter("@CDMoney",c.CDMoney),
                                new SqlParameter("@CDType",c.CDType),

                                
                                };
            return DBhelper.MyExecuteNonQuery(sql,sp);
        }
        public static int RemoveConsumerDetails(int PTID) {
            string sql = @"delete from ConsumerDetail 
        where ProdcutID=@ProdcutID 
        and CDID=(select MAX(CDID) 
        from dbo.ConsumerDetail 
        where ProdcutID=@ProdcutID)";
            SqlParameter[] sp = { 
                                new SqlParameter("@ProdcutID",PTID)
                                };
            return DBhelper.MyExecuteNonQuery(sql, sp);
        }

    }
}

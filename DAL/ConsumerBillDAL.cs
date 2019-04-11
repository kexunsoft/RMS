using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MDL;
using System.Data.SqlClient;

namespace DAL
{
    public class ConsumerBillDAL
    {
        /// <summary>
        /// 获取最大单号
        /// </summary>
        /// <returns>最大单号</returns>
        public static string GetMaxDT() {
            string sql = " select MAX(CBID) from dbo.ConsumerBill where CBID like '%'+CONVERT(varchar,GETDATE(),112)+'%' ";
           object d= DBhelper.MyExecuteScalar(sql,null);
           return d.ToString();
        
        
        }
        /// <summary>
        /// 增加消费
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int AddCB(ConsumerBills c) {
            string sql = @"insert into ConsumerBill(CBID,TableID,CBAmount,CBStartDate,CBClose) 
                    values(@CBID,@TableID,@CBAmount,@CBStartDate,@CBClose)";
            SqlParameter[] sp = { 
                                new  SqlParameter("@CBID",c.CBID),
                                new  SqlParameter("@TableID",c.TableID),
                                new  SqlParameter("@CBAmount",c.CBAmount),
                                new  SqlParameter("@CBStartDate",c.CBStartDate),
                                new  SqlParameter("@CBClose",c.CBClose)

                                };
           return DBhelper.MyExecuteNonQuery(sql,sp);
            
        
        }
        public static int UPCB(ConsumerBills c) {
            string sql = "update dbo.ConsumerBill set CBClose=1,VipID=@VipID,CBDiscount=@CBDiscount,CBEndDate=GETDATE(),AdminID=@AdminID,CBSale=@CBSale where CBID=@CBID";
            SqlParameter[] sp = { 
                                new SqlParameter("@CBID",c.CBID),
                                new SqlParameter("@VipID",c.VipID),
                                new SqlParameter("@CBDiscount",c.CBDiscount),
                                new SqlParameter("@AdminID",c.AdminID),
                                new SqlParameter("@CBSale",c.CBSale),

                                };
            return DBhelper.MyExecuteNonQuery(sql,sp);
        
        
        }
        public static DataTable GetBill() {
            string sql = "select sum(CDPrice) CDPrice,CDDate from  dbo.ConsumerDetail group by CDDate";
            return DBhelper.dd(sql,null);
        }
        public static string billYY(string Date){
            string sql = "select sum(CBSale) from dbo.ConsumerBill where CBID like @Date";
            SqlParameter [] sp={
                               new SqlParameter("@Date","%"+Date+"%")
                               };

            object o = DBhelper.MyExecuteScalar(sql, sp);
            if (o.ToString()=="")
            {
                 return "0";
            }
            return o.ToString();
        }
        public static int UpTable(string CBID,int TBID) {
            string sql = "update ConsumerBill set TableID=@TableID where CBID=@CBID";
            SqlParameter[] sp = { 
                                new SqlParameter("@TableID",TBID),
                                new SqlParameter("@CBID",CBID),
                                };
            return DBhelper.MyExecuteNonQuery(sql,sp);
        }


    }
}

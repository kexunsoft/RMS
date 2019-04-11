using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using MDL;
using System.Data;


namespace BLL
{
    public class ConsumerBillBLL
    {
        public static string GetMaxDT() {
           return ConsumerBillDAL.GetMaxDT();
        
        }
        public static int AddCB(ConsumerBills c) {

            return ConsumerBillDAL.AddCB(c);
        }
        public static int UPCB(ConsumerBills c) {
          return  ConsumerBillDAL.UPCB(c);
        }
        public static DataTable GetBill() {
            return ConsumerBillDAL.GetBill();
        }
        public static string billYY(string Date) {
            if (ConsumerBillDAL.billYY(Date) == "")
            {
                return "0";
            }
            else {
                return ConsumerBillDAL.billYY(Date);
            }
           
        }
        public static int UpTable(string CBID, int TBID) {
            return ConsumerBillDAL.UpTable(CBID,TBID);
        }
    }
}

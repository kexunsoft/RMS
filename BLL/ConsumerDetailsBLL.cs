using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using MDL;

namespace BLL
{
    public class ConsumerDetailsBLL
    {
        public static List<ConsumerDetails> GetCD(int TableID) {
            return ConsumerDetailsDAL.GetCD(TableID);
        
        }
        public static int AddConsumerDetails(ConsumerDetails c) {
            return ConsumerDetailsDAL.AddConsumerDetails(c);
        }
        //移除商品(退单)
        public static int RemoveConsumerDetails(int PTID) {

            return ConsumerDetailsDAL.RemoveConsumerDetails(PTID);
        }
    }
}

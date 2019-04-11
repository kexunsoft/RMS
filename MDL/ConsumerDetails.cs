using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL
{
    public class ConsumerDetails
    {
        /// <summary>
        /// 餐桌编号
        /// </summary>
        public int TableID{get; set;}
        /// <summary>
        /// 菜名
        /// </summary>
        public string ProductName{get; set;}
        /// <summary>
        /// 数量
        /// </summary>
        public int CDAmount{get; set;}
        /// <summary>
        /// 单价
        /// </summary>
        public double ProductPrice { get; set; }
        /// <summary>
        /// 账单编号
        /// </summary>
        public string CBID { get; set; }
        /// <summary>
        /// 开单时间
        /// </summary>
        public string CBDate { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double CBJE{ get; set; }

        public string CDID{ get; set; }
        
        public string ProductID{ get; set; }
        public string CDPrice{ get; set; }
        public string CDNum{ get; set; }
        public string CDSale{ get; set; }
        public string CDMoney{ get; set; }
        public string CDType{ get; set; }


    }
}

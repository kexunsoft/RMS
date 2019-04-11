using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL
{
    public class RoomTypeMDL
    {                                                       
       public int  RTID;
       public string  RTName ;
       public double  RTConsume ;
       public int  RTIsDisCount ;
       public int  RTMount ;

        //下面是梁承煜的
       public static int tid;
       public static int ui;
       public static string tname;
       public static string tmount;
       public int rtid { get; set; }
       public string rtname { get; set; }
       public int rtconsumer { get; set; }
       public bool rtisdiscount { get; set; }
       public int rtmount { get; set; }
    }
}

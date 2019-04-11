using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL
{
    public class TablesMDL
    {
        public int TableID { get; set; }
        public string TableName { get; set; }
        public int RTID { get; set; }
        public string TableArea { get; set; }
        public int TableState { get; set; }

        //梁承煜
        public static int tid;
        public static string tbname;
        public static int ui;
        public int tableid { get; set; }
        public string tablename { get; set; }
        public string rtid { get; set; }
        public string tablearea { get; set; }
        public string tablestate { get; set; }
    }
}

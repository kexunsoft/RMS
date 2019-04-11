using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using MDL;

namespace UI
{
    public partial class UpTable : Form
    {
        public UpTable()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string TBID;
        public string TBName;

        private void UpTable_Load(object sender, EventArgs e)
        {
            List<TablesMDL> list = TablesBLL.selectTable(0);
            
            comboBox1.DataSource = list;
            comboBox1.ValueMember = "TableID";
            comboBox1.DisplayMember = "TableName";
           

            label3.Text = TBName;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取订单编号
            string CBID = TablesBLL.GetCBID(Convert.ToInt32(TBID));
            //根据订单编号修改桌子
            int c = ConsumerBillBLL.UpTable(CBID, Convert.ToInt32(comboBox1.SelectedValue));
            if (c > 0)
            {
                UptableHelp.isTrue = true;
                //修改目标桌子状态
                TablesBLL.UpdateTables(Convert.ToInt32(comboBox1.SelectedValue), 1);
                //修改当前餐桌状态
                TablesBLL.UpdateTables(Convert.ToInt32(TBID), 0);
                new Warning("换桌成功", 图标.Yes).Show();
                this.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class FrmSelectBill : Form
    {
        public FrmSelectBill()
        {
            InitializeComponent();
        }

        private void FrmSelectBill_Load(object sender, EventArgs e)
        {
           
            DataTable dt = ConsumerBillBLL.GetBill();
            chart1.Series[0].YValueMembers = "CDprice";
            chart1.Series[0].XValueMember = "CDDate";
            chart1.DataSource = dt;
            DataTable dt_p = ProductsBLL.GetPT();
            chart2.Series[0].YValueMembers = "销量";
            chart2.Series[0].XValueMember = "商品名字";
            chart2.DataSource = dt_p;
            DateTime dtt = dateTimePicker1.Value;

            //日
            label10.Text = ConsumerBillBLL.billYY(dtt.ToString("yyyyMMdd")) + "元";
            //月
            label11.Text = ConsumerBillBLL.billYY(dtt.ToString("yyyyMM")) + "元";
            //年
            label12.Text = ConsumerBillBLL.billYY(dtt.ToString("yyyy")) + "元";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;

            //日
            label10.Text = ConsumerBillBLL.billYY(dt.ToString("yyyyMMdd")) + "元";
            //月
            label11.Text = ConsumerBillBLL.billYY(dt.ToString("yyyyMM")) + "元";
            //年
            label12.Text = ConsumerBillBLL.billYY(dt.ToString("yyyy")) + "元";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

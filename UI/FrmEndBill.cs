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
using System.IO;

namespace UI
{
    public partial class FrmEndBill : Form
    {
        public FrmEndBill()
        {
            InitializeComponent();
        }
        public string TBID;
        public string TBName;
        double price = 0.00;
        private void FrmEndBill_Load(object sender, EventArgs e)
        {
         
           label2.Text= TablesBLL.GetCBID(Convert.ToInt32(TBID));
           label4.Text = TBName;
           List<ConsumerDetails> list = ConsumerDetailsBLL.GetCD(int.Parse(TBID));
           dataGridView1.AutoGenerateColumns = false;
           dataGridView1.DataSource = list;
            //价格
        
           foreach ( ConsumerDetails item in list)
           {
               price += item.CBJE;
           }
           label10.Text = price + "";
        }
        double VGDiscount = 1;
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            string vipid = textBox1.Text;
            if (vipid.Length==0)
            {
                label15.Text = "";
                label16.Text = "";
                VGDiscount = 1;
                label17.Text = "";
                label10.Text = price + "";
                label22.Text = 0 + "";
                return;
            }
           List<VipsMDL>list = VipsBLL.GetVip(int.Parse(textBox1.Text));
           if (list.Count<=0)
           {
                label15.Text = "会员不存在/已过期";
                label16.Text = "";
                VGDiscount = 1;
                 label17.Text ="";
                 label10.Text = price + "";
                 
               return;
           }
           label15.Text = list[0].VipName;
           label16.Text = list[0].VGName;
           VGDiscount = list[0].VGDiscount;
            label17.Text = VGDiscount.ToString();
            label10.Text = price*VGDiscount + "";
            label22.Text = price - (price * VGDiscount) + "";
          
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length<=0)
            {
                new Warning("结账失败,请输入金额", 图标.Erro).Show();
                return;
            }

            try
            {
                if ((double.Parse(textBox2.Text) - (price * VGDiscount)) >= 0)
                {
                    //获取数据
                    ConsumerBills c = new ConsumerBills();
                    c.CBID = label2.Text;
                    c.VipID = textBox1.Text;
                    c.CBDiscount = label17.Text;
                    c.AdminID = admins.UserId;
                    c.CBSale = double.Parse(label10.Text);
                    //修改支付状态
                    if (ConsumerBillBLL.UPCB(c) > 0)
                    {
                        ZD.cg = 1;
                        if (checkBox1.Checked)
                        {
                            FileStream fs = new FileStream(label2.Text+".txt", FileMode.Create);
                            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                            string tt = "餐饮系统标题";
                            #region 获取发票抬头
                            FileStream fsr = new FileStream("配置文件.ini", FileMode.Open);
                            StreamReader rd = new StreamReader(fsr, Encoding.Default);
                            while (!rd.EndOfStream)
                            {
                                string r = rd.ReadLine();
                                string r1 = r.Split(':')[0];
                                switch (r1)
                                {
                                  
                                    case "发票打印抬头":
                                        tt= r.Split(':')[1];
                                        break;
                                    default:
                                        break;
                                }
                            }
                            rd.Close();
                            fsr.Close();
                            #endregion
                            sw.WriteLine("===="+tt+"====");
                            sw.WriteLine("订单号:" + label2.Text);
                            sw.WriteLine("菜名\t数量\t价格");
                            sw.WriteLine("===\t===\t===");
                            if (dataGridView1.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow item in dataGridView1.Rows)
                                {
                                    sw.WriteLine(item.Cells[0].Value + "\t" + item.Cells[1].Value + "\t" + item.Cells[2].Value);
                                }
                            }
                            else {
                                sw.WriteLine("====无=====");
                            
                            }
                            
                            sw.WriteLine("================");
                            sw.WriteLine("实收金额:" + label10.Text);
                            sw.WriteLine("会员优惠:" + label22.Text);
                            sw.Close();
                            fs.Close();
                            System.Diagnostics.Process.Start(label2.Text + ".txt");
                        }
                        this.Close();
                    }
                }
                else
                {
                    new Warning("支付余额不足", 图标.Erro).Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("发生未只的异常,请联系开发者", "未知异常", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
           
           
          
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar))&&e.KeyChar!=(char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length<=0)
            {
                return;
            }
            label14.Text = (double.Parse(textBox2.Text) - (price * VGDiscount)).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

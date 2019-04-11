using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using MDL;
using BLL;

namespace UI
{
    public partial class FrmAddConsumer : Form
    {
        public FrmAddConsumer()
        {
            InitializeComponent();
        }
        public string CBID;
        public string TableName;
        public int TableID;
        double price = 0;
        private void FrmAddConsumer_Load(object sender, EventArgs e)
        {
            
            lbl_CBID.Text = TablesBLL.GetCBID(TableID);
            lbl_TBID.Text = TableName;
            //查询所有商品
            List<ProductsMDL> list_cm = ProductsBLL.selectProduct("");
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = list_cm;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[3].Visible = false; 
            dataGridView2.Columns[4].Visible = false;
            //查询菜单
            List<ConsumerDetails> list = ConsumerDetailsBLL.GetCD(TableID);
            foreach ( ConsumerDetails item in list)
            {
                ListViewItem lvi = new ListViewItem(item.ProductName);
                lvi.SubItems.Add(item.ProductPrice.ToString());
                lvi.SubItems.Add(item.CDAmount.ToString());
                lvi.SubItems.Add(item.CBJE.ToString());
                lvi.SubItems.Add(item.CBDate);
                lvi.Tag = item.ProductID;
                listView1.Items.Add(lvi);

            }

            foreach (ConsumerDetails item in list)
            {
                price += item.CBJE;
            }
            label10.Text = price.ToString();
            //加载treeview数据
            List<ProductTypeMDL> list_PR = ProductTypeBLL.selectProductType();
          

            foreach (var item in list_PR)
            {
                TreeNode tr = new TreeNode();
                tr.Text = item.PTName;
                tr.Tag = item.PTID;
                List<ProductsMDL> list_P = ProductsBLL.selectProducts(item.PTID, "");
                foreach (ProductsMDL item_P in list_P)
                {

                    TreeNode tr_P = new TreeNode();
                    tr_P.Text = item_P.ProductName;
                    tr_P.Tag = item_P.ProductID;
                    tr_P.Name = item_P.ProductPrice.ToString();
                    tr.Nodes.Add(tr_P);
                }

                treeView1.Nodes.Add(tr);

            }


            label9.Text = listView1.Items.Count.ToString();
            label11.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<ProductsMDL> list_cm = ProductsBLL.selectProduct(textBox1.Text);

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = list_cm;
            dataGridView2.Columns[2].Visible = false;
        }
        string ProductName;
        string ProductPrice;
        string ProductJE;
        string ProductID;
       
        
        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(tabControl1.SelectedIndex.ToString());
            //验证内容
            if (textBox2.Text.Trim().Length <= 0)
            {
                new Warning("数量不能为空", 图标.Erro).Show();
                return;
            }
            if (tabControl1.SelectedIndex == 0)
            {
                if (dataGridView2.SelectedRows.Count <= 0)
                {
                    new Warning("请选择需要添加的商品", 图标.Erro).Show();
                    return;
                }
           
                //获取数据保存到集合
                ProductName = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                ProductPrice = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                ProductJE = (double.Parse(ProductPrice) * double.Parse(textBox2.Text)).ToString();
                ProductID = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            }
            else {

                if (treeView1.SelectedNode.Level == 1)
                {
                    ProductName = treeView1.SelectedNode.Text;
                    ProductPrice = treeView1.SelectedNode.Name;
                    ProductJE = (double.Parse(ProductPrice) * double.Parse(textBox2.Text)).ToString();
                    ProductID = treeView1.SelectedNode.Tag.ToString();

                }
                else if (treeView1.SelectedNode.Level==0)
                {

                    return;
                }
                   
                 
              
            
            
            }
         







            //添加到数据库
            ConsumerDetails c = new ConsumerDetails();
            c.CBID = lbl_CBID.Text;
            c.ProductName = ProductName;
            c.ProductPrice = Convert.ToDouble(ProductPrice);
            c.CDAmount = Convert.ToInt32(textBox2.Text);
            c.CDPrice = ProductPrice.ToString();
            c.CDSale = "0";
            c.CDType = "0";
            c.ProductID = ProductID;
            c.CDMoney = ProductJE.ToString();
            int count = ConsumerDetailsBLL.AddConsumerDetails(c);
          
            //添加到白道
                ListViewItem lvi = new ListViewItem(ProductName);
                lvi.SubItems.Add(ProductPrice.ToString());
                lvi.SubItems.Add(textBox2.Text);
                lvi.SubItems.Add(ProductJE);
                lvi.SubItems.Add(DateTime.Now.ToString());
                lvi.Tag =ProductID;
                listView1.Items.Add(lvi);

         
            price += double.Parse(ProductJE);
            label9.Text = listView1.Items.Count.ToString();
            label10.Text = price.ToString();
        }
        //点单关闭按钮
        private void button3_Click(object sender, EventArgs e)
        {
            
          
            this.Close();

        }

        //退菜按钮
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count<=0)
            {
                new Warning("请选择要退的商品",图标.Erro).Show();
                
                return;
            }
            int PTID = Convert.ToInt32(listView1.SelectedItems[0].Tag);
            int count = ConsumerDetailsBLL.RemoveConsumerDetails(PTID);
            if (count > 0)
            {

                
                new Warning("退菜成功", 图标.Yes).Show();
                price -= Convert.ToDouble(listView1.SelectedItems[0].SubItems[3].Text);
                label10.Text = price.ToString();
                listView1.SelectedItems[0].Remove();
            }
            label9.Text = listView1.Items.Count.ToString();
           
           
            //退菜

        

        }
        //点击显示菜名
        private void dataGridView2_Click(object sender, EventArgs e)
        {
            label11.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
           // string productID = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
          //  图片3介绍4
            //MessageBox.Show(dataGridView2.SelectedRows[0].Cells[4].Value.ToString());
            if (dataGridView2.SelectedRows.Count>0)
            {

                if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString() == "")
                {
                    label12.Text = "商品介绍:暂无";

                }
                else {
                    label12.Text = "商品介绍:" + dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                }
               
                if (dataGridView2.SelectedRows[0].Cells[3].Value.ToString()=="")
                {
                    pictureBox1.Image = Image.FromFile(@"麻辣牛肉.jpg");
                    return;
                }
                try
                {
                    pictureBox1.Image = Image.FromFile(dataGridView2.SelectedRows[0].Cells[3].Value.ToString());
                }
                catch (Exception )
                {
                    //MessageBox.Show(a.ToString());
                    pictureBox1.Image = Image.FromFile(@"麻辣牛肉.jpg");
                }
              
               
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            结账状态.state = true;
            this.Close();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }








    }
}

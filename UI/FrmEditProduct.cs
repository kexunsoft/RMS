using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDL;
using BLL;

namespace UI
{
    public partial class FrmEditProduct : Form
    {
        public FrmEditProduct()
        {
            InitializeComponent();
        }
        //打开类型
        int OpenType;
        private void FrmEditProduct_Load(object sender, EventArgs e)
        {
            //高级绑定
            List<ProductTypeMDL> list1 = ProductTypeBLL.selectProductType();
            comboBox1.DisplayMember = "PTName";
            comboBox1.ValueMember = "PTID";
            comboBox1.DataSource = list1;

            OpenType=EditProduct.OpenType;
            if (OpenType == 1)
            {

                this.Text = "增加商品";
                label7.Text = "增加商品";
            }
            else {

                this.Text = "修改商品信息";
                label7.Text = "修改商品信息";

                comboBox1.SelectedValue = EditProduct.ShopType;
                textBox1.Text = EditProduct.ShopName;
                textBox2.Text = EditProduct.ShopJP;
                textBox3.Text = EditProduct.ShopPrice.ToString();
                textBox4.Text = EditProduct.ProductJs;
                if (EditProduct.ShopPIC=="")
                {
                    pictureBox1.Image = Image.FromFile(path);
                    return;
                }
                path = EditProduct.ShopPIC;
                try
                {
                        pictureBox1.Image = Image.FromFile(path);
                }
                catch (Exception)
                {

                    pictureBox1.Image = Image.FromFile("add.jpg");

                }
            

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""||textBox2.Text==""||textBox3.Text=="")
            {
                  new Warning("信息填写不完整", 图标.Erro).Show();
                  return;
            }

            if (OpenType == 1)
            {
                //增加
                ProductsMDL pr = new ProductsMDL();
                pr.ProductName = textBox1.Text;
                pr.ProductPrice = Convert.ToInt32(textBox3.Text);
                pr.ProductJP = textBox2.Text;
                pr.PTID = Convert.ToInt32(comboBox1.SelectedValue);
                pr.ProductPic = path;
                pr.ProductIntroduction = textBox4.Text;
               int count= ProductsBLL.AddProducts(pr);
               if (count > 0)
               {
                   new Warning("添加成功", 图标.Yes).Show();
                   this.Close();
               }
               else {

                   new Warning("添加失败", 图标.Erro).Show();
               
               }

            }
            else
            {
                //修改
                this.Text = "修改商品信息";
                ProductsMDL p = new ProductsMDL();
                p.ProductName = textBox1.Text;
                p.ProductJP = textBox2.Text;
                p.PTID = Convert.ToInt32(comboBox1.SelectedValue);
                p.ProductPrice = Convert.ToDouble(textBox3.Text);
                p.ProductID = EditProduct.ShopID;
                p.ProductIntroduction = textBox4.Text;
                p.ProductPic = path;
                
                int count =ProductsBLL.UpdateProducts(p);
                if (count > 0)
                {
                    new Warning("修改成功", 图标.Yes).Show();
                    this.Close();
                }
                else {
                    new Warning("修改成功", 图标.Erro).Show();
                }
            }
        }
        string path = @"add.jpg";
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "图片|*.jpg";
          DialogResult dr=  op.ShowDialog();
          if (dr== System.Windows.Forms.DialogResult.OK)
          {
              path = op.FileName;
              pictureBox1.Image = Image.FromFile(path);
          }

        

          
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

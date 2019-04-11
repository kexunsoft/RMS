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
    public partial class FrmEditPT : Form
    {
        public FrmEditPT()
        {
            InitializeComponent();
        }
        int type;
        private void FrmEditPT_Load(object sender, EventArgs e)
        {
            type=Product.type;
            if (type == 1)
            {
                this.Text = "添加商品类型";
            }
            else {
                this.textBox1.Text = Product.name;
                this.Text = "修改商品类型";
            }
                
          

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length<=0)
            {
                new Warning("请输入类型名称",图标.Erro).Show();
                return;
            }
            try
            {
                //执行添加
                if (type == 1)
                {
                    this.Text = "添加商品类型";
                    ProductTypeMDL p = new ProductTypeMDL();
                    p.PTName = textBox1.Text;
                    int count = ProductTypeBLL.AddProductType(p);
                    if (count > 0)
                    {
                        this.Close();
                    }
                }
                //执行修改
                else
                {
                    this.Text = "修改商品类型";
                    ProductTypeMDL p = new ProductTypeMDL();
                    p.PTName = textBox1.Text;
                    p.PTID = Convert.ToInt32(Product.ID);
                    int count = ProductTypeBLL.AlterProductType(p);
                    if (count > 0)
                    {
                        this.Close();
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("发生未只的异常,请联系开发者", "未知异常", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

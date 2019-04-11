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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""||textBox2.Text=="")
            {
                new Warning("信息填写不完整",图标.Yes).Show();
                return;
            }

            try
            {
                if (VipGradeMDL.upin == 1)
                {
                    VipGradeMDL vg = new VipGradeMDL();
                    vg.vgname = textBox1.Text;
                    vg.vgdiscount = double.Parse(textBox2.Text);
                    int i = VipGradeBLL.insertvg(vg);
                    if (i > 0)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("增加失败");
                    }
                }
                else
                {
                    VipGradeMDL vg = new VipGradeMDL();
                    vg.vgname = textBox1.Text;
                    vg.vgdiscount = double.Parse(textBox2.Text);
                    vg.vgid = VipGradeMDL.id;
                    int i = VipGradeBLL.updatevg(vg);
                    if (i > 0)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("修改失败");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("发生未只的异常,请联系开发者","未知异常",MessageBoxButtons.OK,MessageBoxIcon.Error); 
               
            }
          
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            if (VipGradeMDL.upin == 1)
            {
                this.Text = "增加会员等级";
                label3.Text = "增加会员等级";
            }
            else {
                this.textBox1.Text = VipGradeMDL.name;
                this.textBox2.Text = VipGradeMDL.discount;
                this.Text = "修改会员等级";
                label3.Text = "修改会员等级";

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }
    }
}

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
    public partial class vipAdd : Form
    {
        public vipAdd()
        {
            InitializeComponent();
        }
        public int showState = 1;//1,增加,2修改
        public int id;
        private void vipAdd_Load(object sender, EventArgs e)
        {
           
            List<VipGradeMDL> list = VipGradeBLL.getlist(null);
            list.Insert(1, new VipGradeMDL() { vgname = "其他会员", vgid = 0 });
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "vgname";
            comboBox1.ValueMember = "vgid";
            if (showState == 1)
            {
                label7.Text = (VipsBLL.GetMaxID() + 1).ToString();
                label8.Text = "添加会员";

            }
            else {
                label8.Text = "修改会员";
                label7.Text = id.ToString();
                List<VipsMDL> list_v = VipsBLL.GetVips(id);
                if (list_v.Count <= 0)
                {
                    return;
                }
                textBox1.Text = list_v[0].VipName;
                textBox2.Text = list_v[0].VipTel;
                comboBox1.SelectedValue =list_v[0].GradeID;
                if (list_v[0].VipSex == "男")
                {
                    radioButton1.Checked = true;
                }
                else {
                    radioButton2.Checked = true;
                }
                dateTimePicker1.Value = Convert.ToDateTime(list_v[0].VipEndDate);
               

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //验证内容
            if (textBox1.Text.Trim().Length<=0||textBox2.Text.Trim().Length<=0)
            {
                new Warning("信息填写不完整",图标.Erro).Show();
                return;
            }
            //添加
            if (showState == 1)
            {


                try
                {
                    VipsMDL v = new VipsMDL();
                    v.VipName = textBox1.Text;
                    v.VipTel = textBox2.Text;
                    v.GradeID = Convert.ToInt32(comboBox1.SelectedValue);
                    v.VipSex = radioButton1.Checked ? "男" : "女";
                    v.VipEndDate = dateTimePicker1.Value.ToString();
                    if (VipsBLL.AddVip(v) > 0)
                    {
                        this.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("发生未只的异常,请联系开发者", "未知异常", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else { 
            //修改会员
                VipsMDL v = new VipsMDL();
                v.VipName = textBox1.Text;
                v.VipTel = textBox2.Text;
                v.GradeID = Convert.ToInt32(comboBox1.SelectedValue);
                v.VipSex = radioButton1.Checked ? "男" : "女";
                v.VipEndDate = dateTimePicker1.Value.ToString();
                v.VipID = Convert.ToInt32(label7.Text);
                if (VipsBLL.UpdateVip(v)>0)
                {
                    new Warning("修改成功",图标.Yes).Show();
                    this.Close();
                } 
            //执行修改
            
            
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

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
    public partial class FrmEditRoom : Form
    {
        public FrmEditRoom()
        {
            InitializeComponent();
        }

        private void FrmEditRoom_Load(object sender, EventArgs e)
        {
            if (RoomTypeMDL.ui == 1)
            {
                this.Text = "添加房间类型";
                label3.Text = "添加房间类型";
            }
            else
            {
                this.Text = "修改房间类型";
                label3.Text = "修改房间类型";
                textBox1.Text = RoomTypeMDL.tname;
                textBox2.Text = RoomTypeMDL.tmount;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "房间名称不允许为空");
                return;
            }
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "容纳人数不允许为空");
                return;
            }
            try
            {
                if (RoomTypeMDL.ui == 1)
                {
                    RoomTypeMDL rt = new RoomTypeMDL();
                    rt.rtname = textBox1.Text;
                    rt.rtmount = int.Parse(textBox2.Text);
                    RoomTypeBLL.insertroom(rt);
                    this.Close();
                }
                else
                {
                    List<RoomTypeMDL> list = new List<RoomTypeMDL>();
                    RoomTypeMDL rt = new RoomTypeMDL();
                    rt.rtname = textBox1.Text;
                    rt.rtmount = int.Parse(textBox2.Text);
                    rt.rtid = RoomTypeMDL.tid;
                    RoomTypeBLL.updateroom(rt);
                    this.Close();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

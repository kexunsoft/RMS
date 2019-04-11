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
    public partial class FrmEditAdmin : Form
    {
        public FrmEditAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "") {
                new Warning("信息填写不完整", 图标.Erro).Show();
                return;
            }
            try
            {
                AdminsMDL ad = new AdminsMDL();
                ad.userid = AdminsMDL.USERID;
                ad.UserName = textBox1.Text;
                ad.UserCompellation = textBox2.Text;
                int i = AdminsBLL.updateadmin(ad);
                if (i > 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("发生未只的异常,请联系开发者", "未知异常", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            
        }

        private void FrmEditAdmin_Load(object sender, EventArgs e)
        {

            textBox1.Text = AdminsMDL.username;
            textBox2.Text = AdminsMDL.reallyname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

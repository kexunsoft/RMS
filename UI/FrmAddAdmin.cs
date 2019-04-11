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
    public partial class FrmAddAdmin : Form
    {
        public FrmAddAdmin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string UserName = textBox1.Text;
            string UserPWD = textBox2.Text;
            string userpwd = textBox3.Text;
            string UserCompellation = textBox4.Text;
            if (UserName == "" || UserPWD == "" || userpwd == "" || UserCompellation == "") {
                new Warning("信息填写不完整", 图标.Erro).Show();
                return;
            }
            if (UserPWD != userpwd) {
                
                new Warning("两次密码填写不一致，请重新填写", 图标.Erro).Show();
                return;
            }
            try
            {
                AdminsMDL ad = new AdminsMDL();
                ad.UserName = textBox1.Text;
                ad.UserPWD = textBox2.Text;
                ad.UserCompellation = textBox4.Text;
                int i = AdminsBLL.insertadmin(ad);
                if (i > 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("发生未只的异常,请联系开发者", "未知异常", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

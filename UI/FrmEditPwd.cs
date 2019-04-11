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

namespace UI
{
    public partial class FrmEditPwd : Form
    {
        public FrmEditPwd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = textBox1.Text;
            string a1 = textBox2.Text;
            string a2 = textBox3.Text;
            string a3 = textBox4.Text;
            if (a == "" || a1 == "" || a2 == "" || a3 == "")
            {
                new Warning("信息填写不完整", 图标.Erro).Show();
                return;
            }
            if (a2 != a3)
            {
                new Warning("两次输入密码不一致", 图标.Erro).Show();
                return;
            }
            try
            {
                AdminsMDL ad = new AdminsMDL();
                ad.UserName = a;
                ad.UserPWD = a1;
                string aa = AdminsBLL.selectpwd(ad);
                if (aa == "")
                {
                    new Warning("用户名不存在", 图标.Erro).Show();
                    return;
                }
                AdminsMDL am = new AdminsMDL();
                am.UserPWD = a2;
                int b = AdminsBLL.updatepwd(ad, am);
                if (b > 0)
                {
                    new Warning("密码修改成功", 图标.Yes).Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("修改失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

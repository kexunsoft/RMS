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
using System.IO;


namespace UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string userpwd = txtPwd.Text;
            
            if (username == "请输入用户名")
            {
                new Warning("请输入用户名", 图标.Erro).Show();

               
                //MessageBox.Show("请输入账号!");
                return;
            }
            if (userpwd == "")
            {
                new Warning("请输入密码", 图标.Erro).Show();
                return;
            }
            try
            {
                List<AdminsMDL> list = AdminsBLL.GetLogin(username);
                if (list.Count > 0)
                {
                    if (list[0].LoginType == 1)
                    {
                        //登录成功

                        admins.UserCompellation = list[0].UserCompellation;
                        admins.name = list[0].UserName;
                        admins.id = list[0].headID;
                        admins.UserId = list[0].userid;
                        AdminsMDL a = new AdminsMDL();
                        a.LoginType = 2;
                        a.UserName = username;
                        AdminsBLL.logintype(a);
                        FrmMain f = new FrmMain();
                        f.Show();
                        this.Hide();
                    }
                    else if (list[0].LoginType == 2)
                    {
                        new Warning("请勿重复登录", 图标.Erro).Show();
                    }
                }
                else
                {

                    new Warning("密码错误", 图标.Erro).Show();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("配置文件.ini", FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.Default);
            while (!rd.EndOfStream)
            {
                string r = rd.ReadLine();
                string r1 = r.Split(':')[0];
                switch (r1)
                {
                    case "系统名称":
                        label2.Text = r.Split(':')[1];
                        break;
                    
                    default:
                        break;
                }
            }
            rd.Close();
            fs.Close();
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "请输入用户名")
            {
                txtUser.Text = "";
            }
            
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text=="")
            {
                txtUser.Text = "请输入用户名";
            }
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FrmEditPwd f = new FrmEditPwd();
            f.Show();
        }
    }
}

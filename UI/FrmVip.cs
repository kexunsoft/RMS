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
using System.Runtime.InteropServices;

namespace UI
{
    public partial class FrmVip : Form
    {
        public FrmVip()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private const int WM_SETREDRAW = 0xB;
        private void FrmVip_Load(object sender, EventArgs e)
        {
            List<VipsMDL> list = VipsBLL.GetVip(0);
            list.Insert(0,new VipsMDL() { VipName = "添加" ,VipID=0});
            foreach (VipsMDL item in list)
            {
                  ListViewItem lvi = new ListViewItem();
                  if (item.VipID == 0)
                  {
                      lvi.ImageIndex = 0;
                  }
                  else {
                      lvi.ImageIndex = 1;
                  }
           
            lvi.Tag = item.VipID;
            lvi.Text = item.VipName;
            
            listView1.Items.Add(lvi);
            }


         
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count>0)
            {
                if (Convert.ToInt32(listView1.SelectedItems[0].Tag) == 0)
                {
                    vipAdd v = new vipAdd();
                    v.showState = 1;
                    v.ShowDialog();
                    listView1.Items.Clear();
                    FrmVip_Load(null,null);
                }
                else {

                    vipAdd v = new vipAdd();
                    v.id = Convert.ToInt32(listView1.SelectedItems[0].Tag); 
                    v.showState = 2;
                    v.ShowDialog();
                    listView1.Items.Clear();
                    FrmVip_Load(null, null);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count>0)
            {
                label3.Text = listView1.SelectedItems[0].Tag.ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            vipAdd v = new vipAdd();
           
            v.showState = 1;
            v.ShowDialog();
            listView1.Items.Clear();
            FrmVip_Load(null, null);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (Convert.ToInt32(listView1.SelectedItems[0].Tag) != 0)
                {
                    vipAdd v = new vipAdd();
                    v.id = Convert.ToInt32(listView1.SelectedItems[0].Tag);
                    v.showState = 2;
                    v.ShowDialog();
                    listView1.Items.Clear();
                    FrmVip_Load(null, null);
                }

            }
            else {
                new Warning("未选择",图标.Erro).Show();
            }
           
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVip_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                //执行删除
                if (VipsBLL.DeleteVip(Convert.ToInt32(listView1.SelectedItems[0].Tag)) > 0)
                {
                    new Warning("删除成功", 图标.Yes).Show();
                    listView1.Items.Clear();
                    FrmVip_Load(null, null);
                }
            }
            else {
                new Warning("未选择",图标.Erro).Show();
            }
        }

        private void dataGridView1_VisibleChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = VipsBLL.GetJL();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (!dataGridView1.Visible)
            {
                dataGridView1.Visible = true;
                listView1.Visible = false;
            }
            else {
                dataGridView1.Visible = false;
                listView1.Visible = true;

            }
            
        }
    }
}

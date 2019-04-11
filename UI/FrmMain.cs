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
using System.Speech.Synthesis;
using System.IO;


namespace UI
{
    enum 餐台状态 { 
             可用,占用,预订,停用
    }
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        //圆角图片
        public Bitmap WaySTwo(int ID)
        {
            using (Image i = imageList2.Images[ID])
            {
                Bitmap b = new Bitmap(imageList2.ImageSize.Width, imageList2.ImageSize.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.DrawImage(i, 0, 0, b.Width, b.Height);
                    int r = Math.Min(b.Width, b.Height) / 2;
                    PointF c = new PointF(b.Width / 2.0F, b.Height / 2.0F);
                    for (int h = 0; h < b.Height; h++)
                        for (int w = 0; w < b.Width; w++)
                            if ((int)Math.Pow(r, 2) < ((int)Math.Pow(w * 1.0 - c.X, 2) + (int)Math.Pow(h * 1.0 - c.Y, 2)))
                            {
                                b.SetPixel(w, h, Color.Transparent);
                            }

                }
                return b;
            }
        }
        int TableCount = 0;
        int nullcount = 0;
        int kaicount = 0;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists("配置文件.ini"))
            {
                MessageBox.Show("文件不存在","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            FileStream fs = new FileStream("配置文件.ini", FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.Default);
            while (!rd.EndOfStream)
            {
                string r = rd.ReadLine();
                string r1 = r.Split(':')[0];
                switch (r1)
                {
                    case "系统名称":
                        label3.Text= r.Split(':')[1];
                        break;
                    
                    default:
                        break;
                }
            }
            rd.Close();
            fs.Close();

            label4.Text = admins.UserCompellation;
            label1.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            label2.Text = DateTime.Now.ToString("hh:mm:ss");
            label14.Text = "日营业额"+ConsumerBillBLL.billYY(DateTime.Now.ToString("yyyyMMdd"))+"元";
            pictureBox8.Image = WaySTwo(admins.id);
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
           
            #region 动态生成
            List<RoomTypeMDL> list = RoomTypeBLL.selectRoom();
            foreach (RoomTypeMDL item in list)
            {
                //动态添加tabpage

                TabPage t = new TabPage();
                t.Text = item.RTName;
                t.Tag = item.RTID;
                

                //准备listview
                ListView lview = new ListView();
                lview.Dock = DockStyle.Fill;
                lview.LargeImageList = imageList1;
                lview.View = View.LargeIcon;
               lview.Font=new Font("微软雅黑",12);
               
                lview.MouseClick += new MouseEventHandler(lview_MouseClick);
                

                t.Controls.Add(lview);
                this.tabControl1.TabPages.Add(t);
                List<TablesMDL> list_t = TablesBLL.selectTable(item.RTID);
                foreach (TablesMDL items in list_t)
                {
                    TableCount += 1;
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = items.TableName;
                    lvi.ImageIndex = items.TableState;
                    if (items.TableState==1)
                    {
                        kaicount += 1;
                    }
                    if (items.TableState == 0)
                    {
                        nullcount += 1;
                    }
                    lvi.Tag = items.TableID;
                    lview.Items.Add(lvi);
                }



            }
           
            #endregion
            label10.Text = "总台:"+TableCount.ToString();
            label11.Text = "空台:" + nullcount.ToString();
            label12.Text = "开台:" + kaicount.ToString();
            double sz = kaicount / (TableCount + 0.0);
            string bf = (sz * 100).ToString();
            string bf1 = "";
            if (bf.Length>4)
            {
              bf1=  bf.Substring(0,4);
            }
            label13.Text ="上桌率:"+ bf1+ "%";
            }
            catch (Exception)
            {
                
                MessageBox.Show("发生未只的异常,请联系开发者", "未知异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }
        }
        //listview绑定单价事件
        void lview_MouseClick(object sender, MouseEventArgs e)
        {

            ListView lview = sender as ListView;
          
            if (lview.SelectedItems.Count <= 0) {
               
                return;
            }
            餐台状态 t = (餐台状态)lview.SelectedItems[0].ImageIndex;
            switch (t)
            {
                    
                case 餐台状态.可用:
                    改为禁用ToolStripMenuItem.Enabled = true;
                    改为可用ToolStripMenuItem.Enabled = false;
                    改为占用ToolStripMenuItem.Enabled = true;
                    改为停用ToolStripMenuItem.Enabled = true;
                    换桌ToolStripMenuItem.Enabled = false;

                    break;
                case 餐台状态.占用:
                    改为禁用ToolStripMenuItem.Enabled = false;
                    改为可用ToolStripMenuItem.Enabled = false;
                    改为占用ToolStripMenuItem.Enabled = false;
                    改为停用ToolStripMenuItem.Enabled = false;
                    换桌ToolStripMenuItem.Enabled = true;
                    break;
                case 餐台状态.预订:
                    改为禁用ToolStripMenuItem.Enabled = false;
                    改为可用ToolStripMenuItem.Enabled = true;
                    改为占用ToolStripMenuItem.Enabled = false;
                    改为停用ToolStripMenuItem.Enabled = false;
                    换桌ToolStripMenuItem.Enabled = true;
                    break;
                case 餐台状态.停用:
                    改为禁用ToolStripMenuItem.Enabled = false;
                    改为可用ToolStripMenuItem.Enabled = true;
                    改为占用ToolStripMenuItem.Enabled = false;
                    改为停用ToolStripMenuItem.Enabled = false;
                    换桌ToolStripMenuItem.Enabled = false;
                    break;
                default:
                    break;  
            }
   


            lview.ContextMenuStrip = contextMenuStrip2;
            int ID = Convert.ToInt32(lview.SelectedItems[0].Tag.ToString());
            label9.Text = ID + "号";
            label6.Text = "开台: " + lview.SelectedItems[0].Text;
            
            List<ConsumerDetails> list = ConsumerDetailsBLL.GetCD(ID);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = list;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            double price = 0;
            foreach (ConsumerDetails item in list)
            {
                price += item.CBJE;
            }
            label8.Text = price + "元";
            label5.Text = "编号:" + TablesBLL.GetCBID(Convert.ToInt32(lview.SelectedItems[0].Tag));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            label2.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //主窗体关闭事件
            AdminsMDL a = new AdminsMDL();
            a.LoginType = 1;
            a.UserName = admins.name;
           
            AdminsBLL.logintype(a);
            Application.Exit();
        }

       

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            //锁定
            AdminsMDL a = new AdminsMDL();
            a.LoginType = 1;
            a.UserName = admins.name;

            AdminsBLL.logintype(a);
            this.Hide();
            new FrmLogin().Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //开单按钮
            ListView lvi=tabControl1.SelectedTab.Controls[0] as ListView;
            if (lvi.SelectedItems.Count <= 0) {
                new Warning("请选择餐桌",图标.Erro).Show();
                return;
            }
              
            餐台状态 c = (餐台状态)lvi.SelectedItems[0].ImageIndex;
            
            switch (c)
            {
                case 餐台状态.可用:
                      FrmStartBill fs = new FrmStartBill();
                fs.TableID = lvi.SelectedItems[0].Tag.ToString();
                fs.TableName = lvi.SelectedItems[0].Text;
                fs.RoomType = tabControl1.SelectedTab.Text;
                fs.FormClosed += new FormClosedEventHandler(fs_FormClosed);
                fs.ShowDialog();
     
                    break;
                case 餐台状态.占用:
                    new Warning("此餐桌已经被占用", 图标.Erro).Show();
                    break;
                case 餐台状态.预订:
                    new Warning("此餐桌已经被预订", 图标.Erro).Show();
                    break;
                case 餐台状态.停用:
                    new Warning("此餐桌已经被停用", 图标.Erro).Show();
                    break;
                default:
                    break;
            }
           
      
            

            
        }

        void fs_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
            if (CBState.isTrue)
            {
                //MessageBox.Show("开单成功");
                lvi.SelectedItems[0].ImageIndex = 1;
               
                if (CBState.isCheknd)
                {
                    CBState.isCheknd = false;
                    FrmAddConsumer af = new FrmAddConsumer();
                    af.TableID = Convert.ToInt32(lvi.SelectedItems[0].Tag);
                    af.TableName = lvi.SelectedItems[0].Text;
                    af.FormClosed += new FormClosedEventHandler(af_FormClosed);
                    af.Show();
                    
                }
            }
        }
      
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //添加消费
            ListView lvi = (tabControl1.SelectedTab.Controls[0] as ListView);
            if ((tabControl1.SelectedTab.Controls[0] as ListView).SelectedItems.Count<=0)
            {
                new Warning("选择一张餐桌进行消费", 图标.Erro).Show();
                return;
            }

            餐台状态 c = (餐台状态)lvi.SelectedItems[0].ImageIndex;
            switch (c)
            {
                case 餐台状态.可用:
                    new Warning("此餐桌还没有开单", 图标.Erro).Show();
                    break;
                case 餐台状态.占用:
                     FrmAddConsumer af = new FrmAddConsumer();
                 af.TableID = Convert.ToInt32(lvi.SelectedItems[0].Tag.ToString());
                 af.TableName = lvi.SelectedItems[0].Text;
                 af.FormClosed += new FormClosedEventHandler(af_FormClosed);
                 af.ShowDialog();
              
                    break;
                case 餐台状态.预订:
                    new Warning("此餐桌已经被预定", 图标.Erro).Show();
                    break;
                case 餐台状态.停用:
                    new Warning("此餐桌已经被停用", 图标.Erro).Show();
                    break;
                default:
                    break;
            }
          
          
        }

        void af_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (结账状态.state)
            {
                ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
                FrmEndBill fb = new FrmEndBill();
                fb.TBID = lvi.SelectedItems[0].Tag.ToString();
                fb.TBName = lvi.SelectedItems[0].Text;
                fb.FormClosing += new FormClosingEventHandler(fb_FormClosing);
                结账状态.state = false;
               
                fb.Show();
              
            }
        }

      

      

       

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //顾客结账
            ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
            if (lvi.SelectedItems.Count<=0)
            {
                new Warning("尚未选中结账餐桌", 图标.Erro).Show();
                return;
            }
            餐台状态 c = (餐台状态)lvi.SelectedItems[0].ImageIndex;
            switch (c)
            {
                case 餐台状态.可用:
                    new Warning("此餐桌还没有开单", 图标.Erro).Show();
                    break;
                case 餐台状态.占用:
                    FrmEndBill fb = new FrmEndBill();
                fb.TBID = lvi.SelectedItems[0].Tag.ToString();
                fb.TBName = lvi.SelectedItems[0].Text;
                
                
                fb.FormClosing += new FormClosingEventHandler(fb_FormClosing);
              
                fb.ShowDialog();
                    break;
                case 餐台状态.预订:
                    new Warning("此餐桌已经被预定", 图标.Erro).Show();
                    break;
                case 餐台状态.停用:
                    new Warning("此餐桌已经被停用", 图标.Erro).Show();
                    break;
                default:
                    break;
            }
         
           
        }

      

        void fb_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭事件
            ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
            if (ZD.cg == 1)
            {

                new Warning("结账成功", 图标.Yes).Show();
                
              
                lvi.SelectedItems[0].ImageIndex = 0;
                TablesBLL.UpdateTables(Convert.ToInt32(lvi.SelectedItems[0].Tag), 0);
                ZD.cg = 0;
            }
        }

        

       

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            设置 s = new 设置();
            s.ShowDialog();
            TableCount = 0;
            nullcount = 0;
            kaicount = 0;
            tabControl1.TabPages.Clear();
            FrmMain_Load(null, null);
          
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(null,null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TableCount = 0;
            nullcount = 0;
            kaicount = 0;
            tabControl1.TabPages.Clear();
            FrmMain_Load(null,null);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmVip fv = new FrmVip();
            fv.ShowDialog();
        }

        private void 改为可用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
            if (lvi.SelectedItems.Count <= 0)
                return;
          
            lvi.SelectedItems[0].ImageIndex = (int)餐台状态.可用;
            TablesBLL.UpdateTables(Convert.ToInt32(lvi.SelectedItems[0].Tag), (int)餐台状态.可用);
        }

        private void 改为禁用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
            if (lvi.SelectedItems.Count <= 0)
                return;
            lvi.SelectedItems[0].ImageIndex = (int)餐台状态.预订;
            TablesBLL.UpdateTables(Convert.ToInt32(lvi.SelectedItems[0].Tag), (int)餐台状态.预订);
        }

        private void 改为停用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
            if (lvi.SelectedItems.Count <= 0)
                return;
            lvi.SelectedItems[0].ImageIndex = (int)餐台状态.停用;
            TablesBLL.UpdateTables(Convert.ToInt32(lvi.SelectedItems[0].Tag), (int)餐台状态.停用);
        }

        private void 改为占用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
            if (lvi.SelectedItems.Count <= 0)
                return;
            lvi.SelectedItems[0].ImageIndex = (int)餐台状态.占用;
            TablesBLL.UpdateTables(Convert.ToInt32(lvi.SelectedItems[0].Tag), (int)餐台状态.占用);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            FrmSelectBill fs = new FrmSelectBill();
            fs.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("真的要退出系统吗?","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (dr== System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
          
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("便签.exe");
        }

        private void 换桌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lvi = tabControl1.SelectedTab.Controls[0] as ListView;
            if (lvi.SelectedItems.Count<=0)
            {
                new Warning("未选择",图标.Erro).Show();
                return;
            }
            UpTable u = new UpTable();
            u.TBID = lvi.SelectedItems[0].Tag.ToString();
            u.TBName = lvi.SelectedItems[0].Text;
            u.ShowDialog();
            TableCount = 0;
            nullcount = 0;
            kaicount = 0;
            tabControl1.TabPages.Clear();
            FrmMain_Load(null, null);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            预定 y = new 预定();
            y.ShowDialog();
        }
        
       
        
    }
}

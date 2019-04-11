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
    public partial class AddYD : Form
    {
        public AddYD()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AddYD_Load(object sender, EventArgs e)
        {
            string MaxCB = ConsumerBillBLL.GetMaxDT();
            if (MaxCB == "")
            {
                MaxCB = "ZD" + DateTime.Now.ToString("yyyyMMdd") + "0000";

            }
            int day = int.Parse(MaxCB.Substring(MaxCB.Length - 4, 4)) + 1;

            string dayCB = "YD" + DateTime.Now.ToString("yyyyMMdd") + day.ToString().PadLeft(4, '0');
            label9.Text = dayCB;


            List<RoomTypeMDL> list_rt = RoomTypeBLL.selectRoom();
            foreach ( RoomTypeMDL item in list_rt)
            {
                TreeNode tr_r = new TreeNode();
                tr_r.Text = item.RTName;
                tr_r.Tag = item.RTID;
                List<TablesMDL> list_tb = TablesBLL.selectTable(item.RTID);
                foreach (TablesMDL item_tb in list_tb)
                {
                    //只添加可用桌子
                    if (item_tb.TableState==0)
                    {
                        TreeNode tr_t = new TreeNode();
                        tr_t.Text = item_tb.TableName;
                        tr_t.Tag = item_tb.TableID;
                        tr_r.Nodes.Add(tr_t);
                    }
                    
                }
                

                treeView1.Nodes.Add(tr_r);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Level == 1)
            {
                //判断白道是已经有这个桌子
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Tag==treeView1.SelectedNode.Tag)
                    {
                        new Warning("此餐桌已经被选择",图标.Erro).Show();
                        return;
                    }
                }

                ListViewItem lvi = new ListViewItem(treeView1.SelectedNode.Parent.Text);
                lvi.SubItems.Add(treeView1.SelectedNode.Text);
                //房间隐藏值
                lvi.Name = treeView1.SelectedNode.Parent.Tag.ToString();
                //餐桌隐藏值
                lvi.Tag = treeView1.SelectedNode.Tag;
                listView1.Items.Add(lvi);
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count>0)
            {
                listView1.SelectedItems[0].Remove();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //验证数据
            if (textBox1.Text==""||textBox2.Text==""||textBox3.Text=="")
            {
                new Warning("信息填写不完整",图标.Erro).Show();
                return;
            }
            //遍历listview1,插入数据
            //判断白道是已经有这个桌子
            int c=0;
            foreach (ListViewItem item in listView1.Items)
            {
                //@YDID,@KName,@KPhone,@PTID,@TBID,@YTime,@YJ,@Price,@bz
                YDMDL y = new YDMDL();
                y.YDID = label9.Text;
                y.KName = textBox1.Text;
                y.KPhone = textBox2.Text;
                y.RTID = item.Name;
                y.TBID = item.Tag.ToString();
                y.YTime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                y.YJ = textBox3.Text;
                y.bz = textBox5.Text;
                y.Price = "0";
                y.daoda = "0";
               c=  YDBLL.AddYd(y);
               TablesBLL.UpdateTables(Convert.ToInt32(item.Tag),(int)餐台状态.预订);
            }

            if (c>0)
            {
                new Warning("预约成功",图标.Yes).Show();
                
                this.Close();
            }
            //MessageBox.Show("房间:"+listView1.SelectedItems[0].Name+"\n\r"+"桌子:"+listView1.SelectedItems[0].Tag);
        }
    }
}

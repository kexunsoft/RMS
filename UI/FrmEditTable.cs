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
    public partial class FrmEditTable : Form
    {
        public FrmEditTable()
        {
            InitializeComponent();
        }

        private void FrmEditTable_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Add("一楼");
            comboBox2.Items.Add("二楼");
            comboBox2.Items.Add("三楼");
            comboBox2.SelectedIndex = 0;
            List<RoomTypeMDL> list = RoomTypeBLL.getlist();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "RTName";
            comboBox1.ValueMember = "RTID";
            comboBox1.SelectedIndex = 0;
            if (TablesMDL.ui == 1)
            {
                this.Text = "餐台添加";
                label4.Text = "餐台添加";
            }
            else
            {
                this.Text = "修改餐台";
                label4.Text = "修改餐台";
                textBox1.Text = TablesMDL.tbname;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length<=0)
            {
                new Warning("名称不能为空",图标.Erro).Show();
                return;
            }
            try
            {
                if (TablesMDL.ui == 1)
                {
                    //查询房间类型下剩余桌子数量
                    if (TablesBLL.GetTableCount(Convert.ToInt32(comboBox1.SelectedValue))<=0)
                    {
                        new Warning("房间以满",图标.Erro).Show();
                        return;
                    }
                    TablesMDL tb = new TablesMDL();
                    tb.rtid = comboBox1.SelectedValue.ToString();
                    tb.tablename = textBox1.Text;
                    tb.tablestate = "0";
                    tb.tablearea = comboBox2.Text;
                    int i = TablesBLL.inserttb(tb);
                    if (i > 0)
                    {
                        new Warning("添加成功", 图标.Yes).Show();
                        this.Close();
                    }
                }
                else
                {
                    TablesMDL tb = new TablesMDL();
                    tb.tableid = TablesMDL.tid;
                    tb.rtid = comboBox1.SelectedValue.ToString();
                    tb.tablename = textBox1.Text;
                    tb.tablearea = comboBox2.Text;
                    int i = TablesBLL.updatetb(tb);
                    if (i > 0)
                    {
                        new Warning("修改成功", 图标.Yes).Show();
                        this.Close();
                    }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

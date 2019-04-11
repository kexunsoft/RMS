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
    public partial class FrmBatchAddTable : Form
    {
        public FrmBatchAddTable()
        {
            InitializeComponent();
        }

        private void FrmBatchAddTable_Load(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.BackColor = Color.White;
                }
            }



            comboBox2.Items.Add("一楼");
            comboBox2.Items.Add("二楼");
            comboBox2.Items.Add("三楼");
            comboBox2.SelectedIndex = 0;
            List<RoomTypeMDL> list = RoomTypeBLL.getlist();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "RTName";
            comboBox1.ValueMember = "RTID";
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox2.Text.Trim().Length > 0 && textBox3.Text.Trim().Length > 0 && textBox3.Text.Trim().Length > 0)
            {
                int count = int.Parse(textBox3.Text) - int.Parse(textBox2.Text);
                if (TablesBLL.GetTableCount(Convert.ToInt32(comboBox1.SelectedValue)) <= count)
                {
                    new Warning("添加失败,房间剩余:" + TablesBLL.GetTableCount(Convert.ToInt32(comboBox1.SelectedValue)), 图标.Erro).Show();
                    return;
                }
            }
            else {
                new Warning("信息填写不完整",图标.Erro).Show();
                return;
            }

            try
            {
                int a1 = Convert.ToInt32(textBox2.Text);
                int b = Convert.ToInt32(textBox3.Text);
                List<TablesMDL> list = new List<TablesMDL>();
                for (int a = Convert.ToInt32(textBox2.Text); a <= b; a++)
                {

                    TablesMDL tb = new TablesMDL();
                    if (a >= 10)
                    {
                        tb.tablename = textBox1.Text + "0" + a;
                    }
                    else if (a < 10)
                    {
                        tb.tablename = textBox1.Text + "00" + a;
                    }
                    else
                    {
                        tb.tablename = textBox1.Text + a;
                    }
                    tb.tablearea = comboBox2.Text;
                    tb.rtid = comboBox1.SelectedValue.ToString();
                    tb.tablestate = "0";
                    int i = TablesBLL.inserttb(tb);
                    if (i>0)
                    {
                        new Warning("添加成功",图标.Yes).Show();
                        this.Close();
                    }
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}

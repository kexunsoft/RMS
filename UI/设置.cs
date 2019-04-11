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

using System.IO;

namespace UI
{
    public partial class 设置 : Form
    {
        public 设置()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label2.Text = "房间与餐台";
            Pl_Room.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;


        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            label2.Text = "商品与商品类型";
            panel3.Visible = true;
            Pl_Room.Visible = false;
            panel4.Visible = false;


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            label2.Text = "系统设置";
            panel3.Visible = false;
            Pl_Room.Visible = false;
            panel4.Visible = true;

        }

        private void 设置_Load(object sender, EventArgs e)
        {

            comboBox1.Items.Clear();
            comboBox1.Items.Add("全部");
            List<RoomTypeMDL> list = RoomTypeBLL.getlist();
            foreach (RoomTypeMDL item in list)
            {
                ListViewItem lv = new ListViewItem(item.rtname.ToString());
                lv.SubItems.Add(item.rtmount.ToString());
                lv.SubItems.Add(item.rtid.ToString());
                listView1.Items.Add(lv);
                comboBox1.Items.Add(item.rtname);
            }
            comboBox1.SelectedIndex = 0;
            List<TablesMDL> list1 = TablesBLL.getlist(null);
            listView2.Items.Clear();
            foreach (TablesMDL item in list1)
            {
                ListViewItem lv = new ListViewItem(item.tablename.ToString());
                lv.SubItems.Add(item.rtid);
                lv.SubItems.Add(item.tablestate.ToString());
                lv.SubItems.Add(item.tablearea.ToString());
                lv.SubItems.Add(item.tableid.ToString());
                listView2.Items.Add(lv);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RoomTypeMDL.ui = 1;
            FrmEditRoom fer = new FrmEditRoom();
            fer.ShowDialog();
            listView1.Items.Clear();
            listView2.Items.Clear();
            设置_Load(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                MessageBox.Show("请选择一条数据");
                return;
            }
            RoomTypeMDL.tid = Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text);
            RoomTypeMDL.tname = Convert.ToString(listView1.SelectedItems[0].SubItems[0].Text);
            RoomTypeMDL.tmount = Convert.ToString(listView1.SelectedItems[0].SubItems[1].Text);
            RoomTypeMDL.ui = 2;
            FrmEditRoom fer = new FrmEditRoom();
            fer.ShowDialog();
            listView1.Items.Clear();
            listView2.Items.Clear();
            设置_Load(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {

                //判断房间类型是否有桌子
                List<TablesMDL> list = TablesBLL.selectTable(Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text));
                //判断
                if (list.Count>0)
                {
                    new Warning("无法删除有餐桌的房间",图标.Erro).Show();
                    return;
                }
                RoomTypeMDL rt1 = new RoomTypeMDL();
                rt1.rtid = Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text);
                RoomTypeBLL.deleteroom(rt1);
                listView1.Items.Clear();
                listView2.Items.Clear();
                设置_Load(null, null);
            }
            else
            {
                MessageBox.Show("请选中一项数据");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string combo = comboBox1.Text;
            if (combo == "全部")
                combo = "";
            RoomTypeMDL rt = new RoomTypeMDL();
            rt.rtname = combo;
            listView2.Items.Clear();
            List<TablesMDL> list1 = TablesBLL.getlist(rt);
            foreach (TablesMDL item in list1)
            {
                ListViewItem lv = new ListViewItem(item.tablename.ToString());
                lv.SubItems.Add(item.rtid);
                lv.SubItems.Add(item.tablestate.ToString());
                lv.SubItems.Add(item.tablearea.ToString());
                lv.SubItems.Add(item.tableid.ToString());
                listView2.Items.Add(lv);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TablesMDL.ui = 1;
            FrmEditTable fet = new FrmEditTable();
            fet.ShowDialog();
            listView1.Items.Clear();
            listView2.Items.Clear();
            设置_Load(null, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBatchAddTable fbat = new FrmBatchAddTable();
            fbat.ShowDialog();
            listView1.Items.Clear();
            listView2.Items.Clear();
            设置_Load(null, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                TablesMDL.ui = 2;
                TablesMDL.tbname = listView2.SelectedItems[0].SubItems[0].Text.ToString();
                TablesMDL.tid = Convert.ToInt32(listView2.SelectedItems[0].SubItems[4].Text);
                FrmEditTable fet = new FrmEditTable();
                fet.ShowDialog();
                listView1.Items.Clear();
                listView2.Items.Clear();
                设置_Load(null, null);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                if (listView2.SelectedItems[0].SubItems[2].Text=="占用")
                {
                    new Warning("选中房间正在使用",图标.Erro).Show();
                    return;
                }
                TablesMDL tb = new TablesMDL();
                tb.tableid = Convert.ToInt32(listView2.SelectedItems[0].SubItems[4].Text);
                int i = TablesBLL.deletetb(tb);
                if (i > 0)
                {
                    MessageBox.Show("删除成功");
                    listView1.Items.Clear();
                    listView2.Items.Clear();
                    设置_Load(null, null);
                }
            }
            else
            {
                MessageBox.Show("请选择一条数据");
            }
        }

        private void panel3_VisibleChanged(object sender, EventArgs e)
        {
            List<ProductTypeMDL> list = ProductTypeBLL.selectProductType();

            dataGridView1.DataSource = list;

            List<ProductsMDL> list_cm = ProductsBLL.selectProducts(0, "");

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = list_cm;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;



           




            List<ProductTypeMDL> list1 = ProductTypeBLL.selectProductType();
            list1.Insert(0, (new ProductTypeMDL { PTID = 0, PTName = "全部" }));
            comboBox2.DisplayMember = "PTName";
            comboBox2.ValueMember = "PTID";

            comboBox2.DataSource = list1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<ProductsMDL> list_cm = ProductsBLL.selectProducts(Convert.ToInt32(comboBox2.SelectedValue.ToString()), "");
            dataGridView2.AutoGenerateColumns = false;

            dataGridView2.DataSource = list_cm;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //1,修改 2,删除
            Product.type = 1;
            new FrmEditPT().ShowDialog();
            List<ProductTypeMDL> list = ProductTypeBLL.selectProductType();
            dataGridView1.DataSource = list;

            List<ProductTypeMDL> list1 = ProductTypeBLL.selectProductType();
            list1.Insert(0, (new ProductTypeMDL { PTID = 0, PTName = "全部" }));
            comboBox2.DisplayMember = "PTName";
            comboBox2.ValueMember = "PTID";

            comboBox2.DataSource = list1;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Product.type = 2;
            Product.ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Product.name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            new FrmEditPT().ShowDialog();
            List<ProductTypeMDL> list = ProductTypeBLL.selectProductType();
            dataGridView1.DataSource = list;

            List<ProductTypeMDL> list1 = ProductTypeBLL.selectProductType();
            list1.Insert(0, (new ProductTypeMDL { PTID = 0, PTName = "全部" }));
            comboBox2.DisplayMember = "PTName";
            comboBox2.ValueMember = "PTID";

            comboBox2.DataSource = list1;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                new Warning("请选择一行数据进行删除",图标.Erro).Show();
                return;
            }
            //删除
            DialogResult d = MessageBox.Show("是否确认删除", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {

                List<ProductsMDL> listP = ProductsBLL.selectProducts(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value),"");
                if (listP.Count>0)
                {
                    new Warning("请先移除此类型下的商品",图标.Erro).Show();
                    return;
                }




                ProductTypeMDL p = new ProductTypeMDL();
                p.PTID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                if (ProductTypeBLL.DeleteProductType(p) > 0)
                {
                    new Warning("删除成功", 图标.Yes).Show();
                    List<ProductTypeMDL> list = ProductTypeBLL.selectProductType();
                    dataGridView1.DataSource = list;

                    List<ProductTypeMDL> list1 = ProductTypeBLL.selectProductType();
                    list1.Insert(0, (new ProductTypeMDL { PTID = 0, PTName = "全部" }));
                    comboBox2.DisplayMember = "PTName";
                    comboBox2.ValueMember = "PTID";

                    comboBox2.DataSource = list1;

                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            List<ProductsMDL> list_cm = ProductsBLL.selectProducts(Convert.ToInt32(comboBox2.SelectedValue.ToString()), textBox1.Text);
            dataGridView2.AutoGenerateColumns = false;

            dataGridView2.DataSource = list_cm;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //1.增加,2,修改
            EditProduct.OpenType = 1;

            new FrmEditProduct().ShowDialog();
            List<ProductsMDL> list_cm = ProductsBLL.selectProducts(Convert.ToInt32(comboBox2.SelectedValue.ToString()), "");
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = list_cm;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            EditProduct.OpenType = 2;
            EditProduct.ShopType = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[3].Value);
            EditProduct.ShopName = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            EditProduct.ShopPrice = Convert.ToDouble(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
            EditProduct.ShopJP = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
            EditProduct.ShopID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[5].Value);
            EditProduct.ShopPIC = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
            EditProduct.ProductJs = dataGridView2.SelectedRows[0].Cells[7].Value.ToString();
            new FrmEditProduct().ShowDialog();
            List<ProductsMDL> list_cm = ProductsBLL.selectProducts(Convert.ToInt32(comboBox2.SelectedValue.ToString()), "");
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = list_cm;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;


        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否确认删除?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {

                int count = ProductsBLL.DeleteProducts(Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[5].Value));
                if (count > 0)
                {
                    new Warning("删除成功", 图标.Yes).Show();
                    List<ProductsMDL> list_cm = ProductsBLL.selectProducts(Convert.ToInt32(comboBox2.SelectedValue.ToString()), "");
                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.DataSource = list_cm;
                    dataGridView2.Columns[3].Visible = false;
                    dataGridView2.Columns[4].Visible = false;
                    dataGridView2.Columns[5].Visible = false;
                    dataGridView2.Columns[6].Visible = false;
                    dataGridView2.Columns[7].Visible = false;

                }
                else
                {
                    new Warning("删除失败", 图标.Erro).Show();
                }
            }
        }

        private void panel4_VisibleChanged(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            listView3.Items.Clear();
            List<AdminsMDL> list = AdminsBLL.getlist(null);
            foreach (AdminsMDL item in list)
            {
                ListViewItem lv = new ListViewItem(item.userid.ToString());
                lv.SubItems.Add(item.UserName);
                lv.SubItems.Add(item.UserCompellation);
                listView4.Items.Add(lv);
            }
            List<VipGradeMDL> list1 = VipGradeBLL.getlist(null);
            foreach (VipGradeMDL item in list1)
            {
                ListViewItem lv = new ListViewItem(item.vgid.ToString());
                lv.SubItems.Add(item.vgname);
                lv.SubItems.Add(item.vgdiscount.ToString());
                listView3.Items.Add(lv);
            }

            FileStream fs = new FileStream("配置文件.ini",FileMode.Open);
            StreamReader rd = new StreamReader(fs,Encoding.Default);
            while(!rd.EndOfStream){
                string  r=rd.ReadLine();
                string r1 = r.Split(':')[0];
                switch (r1)
                {
                    case "系统名称":
                        textBox2.Text =  r.Split(':')[1];
                        break;
                    case "发票打印抬头":
                        textBox3.Text = r.Split(':')[1];
                        break;
                    default:
                        break;
                }
            }
            rd.Close();
            fs.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            FrmAddAdmin faa = new FrmAddAdmin();
            faa.ShowDialog();
            panel4_VisibleChanged(null, null);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                FrmEditAdmin fea = new FrmEditAdmin();
                AdminsMDL.username = listView4.SelectedItems[0].SubItems[1].Text;
                AdminsMDL.reallyname = listView4.SelectedItems[0].SubItems[2].Text;
                AdminsMDL.USERID = Convert.ToInt32(listView4.SelectedItems[0].SubItems[0].Text);
                fea.ShowDialog();
                panel4_VisibleChanged(null, null);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                
                if (listView4.SelectedItems[0].SubItems[0].Text==admins.UserId.ToString())
                {
                    new Warning("不能删除当前用户",图标.Erro).Show();
                    return;
                }
                AdminsMDL ad = new AdminsMDL();
                ad.userid = Convert.ToInt32(listView4.SelectedItems[0].SubItems[0].Text);
                AdminsBLL.deleteadmin(ad);
                panel4_VisibleChanged(null, null);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            VipGradeMDL.upin = 1;
            FrmAdmin fa = new FrmAdmin();
            fa.ShowDialog();
            panel4_VisibleChanged(null, null);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            VipGradeMDL.upin = 2;
            if (listView3.SelectedItems.Count > 0)
            {
                FrmAdmin fa = new FrmAdmin();
                VipGradeMDL.name = listView3.SelectedItems[0].SubItems[1].Text;
                VipGradeMDL.discount = listView3.SelectedItems[0].SubItems[2].Text;
                VipGradeMDL.id = Convert.ToInt32(listView3.SelectedItems[0].SubItems[0].Text);
                fa.ShowDialog();
                panel4_VisibleChanged(null, null);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                VipGradeMDL vg = new VipGradeMDL();
                vg.vgid = int.Parse(listView3.SelectedItems[0].SubItems[0].Text);
                int i = VipGradeBLL.deletevg(vg);
                if (i > 0)
                {
                    listView4.Items.Clear();
                    listView3.Items.Clear();
                    panel4_VisibleChanged(null, null);
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }




        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim().Length<=0||textBox3.Text.Trim().Length<=0)
            {
                new Warning("信息填写不完整",图标.Erro).Show();
                return;
            }
            FileStream fs = new FileStream("配置文件.ini", FileMode.Create);
            StreamWriter er = new StreamWriter(fs, Encoding.Default);
            er.WriteLine("系统名称:"+textBox2.Text);
            er.WriteLine("发票打印抬头:" + textBox3.Text);
            er.Close();
            fs.Close();
            new Warning("保存成功", 图标.Yes).Show();
        }
    }
}

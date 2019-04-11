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
    public partial class 选定套餐 : Form
    {
        public 选定套餐()
        {
            InitializeComponent();
        }

        private void 选定套餐_Load(object sender, EventArgs e)
        {
            //查询所有商品
            List<ProductsMDL> list_cm = ProductsBLL.selectProduct("");
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = list_cm;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            List<ProductTypeMDL> list_PR = ProductTypeBLL.selectProductType();


            foreach (var item in list_PR)
            {
                TreeNode tr = new TreeNode();
                tr.Text = item.PTName;
                tr.Tag = item.PTID;
                List<ProductsMDL> list_P = ProductsBLL.selectProducts(item.PTID, "");
                foreach (ProductsMDL item_P in list_P)
                {

                    TreeNode tr_P = new TreeNode();
                    tr_P.Text = item_P.ProductName;
                    tr_P.Tag = item_P.ProductID;
                    tr_P.Name = item_P.ProductPrice.ToString();
                    tr.Nodes.Add(tr_P);
                }

                treeView1.Nodes.Add(tr);

            }
        }
    }
}

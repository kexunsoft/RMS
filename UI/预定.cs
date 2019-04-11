using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class 预定 : Form
    {
        public 预定()
        {
            InitializeComponent();
        }

        private void 预定_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = YDBLL.GetYD();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddYD ay = new AddYD();
            ay.ShowDialog();
            预定_Load(null,null);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认删除","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== System.Windows.Forms.DialogResult.Yes)
            {
                if (YDBLL.DeleteYD(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()) > 0)
                {
                    if (YDBLL.UPTB(dataGridView2.SelectedRows[0].Cells[0].Value.ToString())>0)
                    {
                        new Warning("删除成功", 图标.Yes).Show();
                        预定_Load(null, null);
                    }
                    
                   
                }  
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

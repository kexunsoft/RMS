using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using redius;
public enum 图标
{
    Yes, Erro
}
namespace UI
{
    public partial class Warning : Form
    {
        public Warning()
        {
            InitializeComponent();
        }
        public Warning(string title,图标 icon)
        {
            InitializeComponent();
            this.title = title;
            this.icon = icon;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Warning_Paint(object sender, PaintEventArgs e)
        {
            RoundFormPainter.Paint(sender, e);
        }
        图标 icon;
        string title;
        private void Warning_Load(object sender, EventArgs e)
        {
            this.Text = "提示";
            label1.Text = title;
            switch (icon)
            {
                case 图标.Yes:
                    pictureBox1.Image = IconList.Images[0];
                    timer1.Start();
                    break;
                case 图标.Erro:
                    pictureBox1.Image = IconList.Images[1];

                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

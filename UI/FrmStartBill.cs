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
    public partial class FrmStartBill : Form
    {
        public string TableName;
        public string RoomType;
        public string TableID;
        public FrmStartBill()
        {
            InitializeComponent();
        }

        private void FrmStartBill_Load(object sender, EventArgs e)
        {
            //获取餐台和房间类型
            lbl_TbName.Text = TableName;
            lbl_RTType.Text = RoomType;
            
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_Count.Text.Trim().Length<=0)
            {
                new Warning("人数不能为空",图标.Erro).Show();
                return;
            }
            try
            {
                #region 获取单号
                string MaxCB = ConsumerBillBLL.GetMaxDT();
                if (MaxCB == "")
                {
                    MaxCB = "ZD" + DateTime.Now.ToString("yyyyMMdd") + "0000";

                }
                int day = int.Parse(MaxCB.Substring(MaxCB.Length - 4, 4)) + 1;

                string dayCB = "ZD" + DateTime.Now.ToString("yyyyMMdd") + day.ToString().PadLeft(4, '0');
                #endregion

                #region 开单消费
                ConsumerBills c = new ConsumerBills();
                c.CBID = dayCB;
                c.TableID = int.Parse(TableID);
                c.CBAmount = txt_Count.Text;
                c.CBStartDate = DateTime.Now.ToString();
                c.CBClose = 0;

                //添加餐桌插入单号
                if (ConsumerBillBLL.AddCB(c) > 0)
                {
                    CBState.isTrue = true;
                    //开单成功修改桌子状态
                    TablesBLL.UpdateTables(Convert.ToInt32(TableID), 1);

                    if (checkBox1.Checked)
                    {
                        CBState.isCheknd = true;
                    }
                    this.Close();
                }
                #endregion
           
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

        private void txt_Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}

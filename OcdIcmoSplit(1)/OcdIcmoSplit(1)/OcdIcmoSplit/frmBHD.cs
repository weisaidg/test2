using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;

namespace OcdIcmoSplit
{
    public partial class frmBHD : Form
    {
        //string WorkShop;
        //DateTime beginDate;
        //DateTime endDate;


        public frmBHD(string myWorkShop, DateTime mybeginDate, DateTime myendDate)
        {
            InitializeComponent();
            this.comboBox1.Text = myWorkShop;
            this.dateTimePicker1.Value = mybeginDate;
            this.dateTimePicker2.Value = myendDate;
        }

        private void frmBHD_Load(object sender, EventArgs e)
        {
            InsertComShopID();
            LoadData();

        }

        private void LoadData()
        {
            System.TimeSpan t3 = this.dateTimePicker2.Value - this.dateTimePicker1.Value;  //两个时间相减 。默认得到的是 两个时间之间的天数   得到：365.00:00:00 

            double getDay = t3.TotalDays;

            if (getDay < 50)
            {

                System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                string strConn = config.AppSettings.Settings["connectionstring"].Value;//this.txtUrl.Text.Trim();
                DataTable dt = SqlHelper.ExecuteDataset(strConn, "pr_xc_StatPC", this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString(), this.comboBox1.Text,this.textBoxPlaner.Text.Trim(),this.textBoxItemNumber.Text.Trim()).Tables[0];

                this.dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show( "为了保证查看统计的速度，统计只能看50天的数据","金蝶提示");
            }
            
        }

        public void InsertComShopID()
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string strConn = config.AppSettings.Settings["connectionstring"].Value;//this.txtUrl.Text.Trim();

            this.comboBox1.Items.Clear();//清空ComBox
            IDataReader dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "pr_xc_getWorkShop");
            while (dr.Read())
            {
                this.comboBox1.Items.Add(dr[0].ToString());//循环读取数据
            }//end block while

            dr.Close();//  关闭数据集
            // DB.GetColse();//关闭数据库连接
        }

        private void buttonSerach_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}

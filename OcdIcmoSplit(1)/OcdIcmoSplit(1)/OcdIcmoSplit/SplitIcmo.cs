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
using System.IO;


namespace OcdIcmoSplit
{
    public partial class SplitIcmo : Form
    {
        public SplitIcmo()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataTableToCsv(DataTable table, string file)
        {

            string title = "";

            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);

            //FileStream fs1 = File.Open(file, FileMode.Open, FileAccess.Read);

            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

            for (int i = 0; i < table.Columns.Count; i++)
            {

                title += table.Columns[i].ColumnName + "\t"; //栏位：自动跳到下一单元格

            }

            title = title.Substring(0, title.Length - 1) + "\n";

            sw.Write(title);

            foreach (DataRow row in table.Rows)
            {

                string line = "";

                for (int i = 0; i < table.Columns.Count; i++)
                {

                    line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格

                }

                line = line.Substring(0, line.Length - 1) + "\n";

                sw.Write(line);

            }

            sw.Close();

            fs.Close();

        }

        /// <summary>
        /// 在连接串中，获取数据库名称
        /// </summary>
        /// <param name="sConnstring"></param>
        /// <returns></returns>
        public string GetDataBaseName(string sConnstring)
        {
            if (sConnstring != null && !string.IsNullOrEmpty(sConnstring))
            {
                string[] arr = sConnstring.Split(';');
                foreach (string str in arr)
                {
                    if (str.ToLower().Contains("initial catalog"))
                    {
                        string[] arr2 = str.Split('=');
                        if (arr2.Length > 1)
                        {
                            return arr2[1];
                        }
                    }
                }
            }

            return "";
        }

        private void SplitIcmo_Load(object sender, EventArgs e)
        {
            //修改标题
            this.Text = string.Format("{0}【{1}】", this.Text, GetDataBaseName(HJ.Data.SQLDBConnect.SQLConnstring));

            InsertComShopID();

            this.dateTimePicker1.Value = System.DateTime.Now.AddDays(-10);
            this.dateTimePicker2.Value = System.DateTime.Now.AddMonths(1);

          this.gridView1.OptionsView.ColumnAutoWidth = false;
 //this.gridView1.ScrollStyle = ScrollStyleFlags.LiveHorzScroll | ScrollStyleFlags.LiveVertScroll;
 //this.gridView1.HorzScrollVisibility = ScrollVisibility.Always;



            //Query();
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

        private void tbRefresh_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void tbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            
            //获取选择的行数
            //获取右键菜单的状态
            int intselect = gridView1.SelectedRowsCount;
            tsmiSplit.Enabled = false;
            tsmiDelete.Enabled = false;
            if (intselect == 1)
            {
                tsmiDelete.Enabled = true;
                tsmiSplit.Enabled = true;
            }
            else if (intselect > 1)
            {
                tsmiSplit.Enabled = false;
                tsmiDelete.Enabled = false;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch ((e.ClickedItem).Name)
            {
                    //拆分
                case "tsmiSplit":
                    int[] iSelects = gridView1.GetSelectedRows();
                    if (iSelects != null && iSelects.Length > 0)
                    {
                        DataRow dr = gridView1.GetDataRow(iSelects[0]);
                        SplitDetail frm = new SplitDetail(dr,this.comboBox1.Text,this.dateTimePicker1.Value,this.dateTimePicker2.Value);

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            //刷新
                            Query();
                        }
                        //frm.Show();
                       
                    }
                    else
                    {
                        MessageBox.Show("金蝶提示", "请选择需要拆分的行");
                    }
                    break;
                    //删除
                case "tsmiDelete":
                    break;
            }
        }

        /// <summary>
        /// 左边的行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void tbQuery_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(this.comboBox1.Text);
            Query();
        }

        /// <summary>
        /// 刷新，查询生产任务单列表
        /// </summary>
        private void Query()
        {
            //string sWhereStr = string.Empty;
            //if (!string.IsNullOrWhiteSpace(tbFBillNo.Text))
            //{
            //    sWhereStr += string.Format(" and FBILLNO like '%{0}%'", tbFBillNo.Text);
            //}

            //string sSql = string.Format(" select * from V_IcmoSplit_LWH where 1=1 {0} ", sWhereStr);

             System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string strConn = config.AppSettings.Settings["connectionstring"].Value;//this.txtUrl.Text.Trim();

             DataTable dt = SqlHelper.ExecuteDataset(strConn,"pr_xc_GetICMO",this.tbFBillNo.Text,this.dateTimePicker1.Value.ToShortDateString(),this.dateTimePicker2.Value.ToShortDateString(),this.comboBox1.Text.Trim(),this.textBoxSEorder.Text.Trim(),this.FPlanner.Text.Trim(),this.textBoxItemFNumber.Text.Trim()).Tables[0];
            // DataTable dt = SqlHelper.ExecuteDataset(strConn, "pr_xc_GetICMO", this.tbFBillNo.Text, this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString(), this.comboBox1.Text.Trim(), this.textBoxSEorder.Text.Trim(), this.FPlanner.Text.Trim() ).Tables[0];

            //DataTable dt = HJ.Data.SQLDBConnect.SQLDBconntion.ExecuteDataSet(sSql).Tables[0];
            gridControl1.DataSource = dt;

           // dataTableToCsv(dt, @"E:\KT\希锐\1.xlsx"); //调用函数
        }

        private void toolStripButtonBHD_Click(object sender, EventArgs e)
        {

            System.TimeSpan t3 = this.dateTimePicker2.Value-this.dateTimePicker1.Value;  //两个时间相减 。默认得到的是 两个时间之间的天数   得到：365.00:00:00 

            double getDay = t3.TotalDays;

            if (getDay < 50)
            {

                frmBHD bhd = new frmBHD(this.comboBox1.Text, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                bhd.ShowDialog();
            }
            else
            {
                MessageBox.Show( "为了保证查看统计的速度，统计只能看50天的数据","金蝶提示");
            }

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.C)
            {
                Clipboard.SetDataObject(gridView1.GetFocusedRowCellValue(gridView1.FocusedColumn));
                e.Handled = true;
            }
        }

        private void tsmiSplit_Click(object sender, EventArgs e)
        {

        }

     

         
    }
}

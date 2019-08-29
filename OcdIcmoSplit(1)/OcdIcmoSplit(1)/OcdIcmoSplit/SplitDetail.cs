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
    public partial class SplitDetail : Form
    {
        /// <summary>
        /// 复制行数
        /// </summary>
        private DataRow[] copyRows = null;
        /// <summary>
        /// 原行数的信息
        /// </summary>
        private DataRow drOrg = null;

        private string WrokShop;
        private DateTime dtBegin;
        private DateTime dtEnd;
        private string IndenNum;
        DataTable dt;

        #region 需要插入SQL的字段，暂时没用
        private string sSqlField = string.Format(@"[FBrNo]
                                                              ,[FInterID]
                                                              ,[FBillNo]
                                                              ,[FTranType]
                                                              ,[FStatus]
                                                              ,[FMRP]
                                                              ,[FType]
                                                              ,[FWorkShop]
                                                              ,[FItemID]
                                                              ,[FQty]
                                                              ,[FCommitQty]
                                                              ,[FPlanCommitDate]
                                                              ,[FPlanFinishDate]
                                                              ,[FConveyerID]
                                                              ,[FCommitDate]
                                                              ,[FCheckerID]
                                                              ,[FCheckDate]
                                                              ,[FRequesterID]
                                                              ,[FBillerID]
                                                              ,[FSourceEntryID]
                                                              ,[FClosed]
                                                              ,[FNote]
                                                              ,[FUnitID]
                                                              ,[FAuxCommitQty]
                                                              ,[FAuxQty]
                                                              ,[FOrderInterID]
                                                              ,[FPPOrderInterID]
                                                              ,[FParentInterID]
                                                              ,[FCancellation]
                                                              ,[FSupplyID]
                                                              ,[FQtyFinish]
                                                              ,[FQtyScrap]
                                                              ,[FQtyForItem]
                                                              ,[FQtyLost]
                                                              ,[FPlanIssueDate]
                                                              ,[FRoutingID]
                                                              ,[FStartDate]
                                                              ,[FFinishDate]
                                                              ,[FAuxQtyFinish]
                                                              ,[FAuxQtyScrap]
                                                              ,[FAuxQtyForItem]
                                                              ,[FAuxQtyLost]
                                                              ,[FMrpClosed]
                                                              ,[FBomInterID]
                                                              ,[FQtyPass]
                                                              ,[FAuxQtyPass]
                                                              ,[FQtyBack]
                                                              ,[FAuxQtyBack]
                                                              ,[FFinishTime]
                                                              ,[FReadyTIme]
                                                              ,[FPowerCutTime]
                                                              ,[FFixTime]
                                                              ,[FWaitItemTime]
                                                              ,[FWaitToolTime]
                                                              ,[FTaskID]
                                                              ,[FWorkTypeID]
                                                              ,[FCostObjID]
                                                              ,[FStockQty]
                                                              ,[FAuxStockQty]
                                                              ,[FSuspend]
                                                              ,[FProjectNO]
                                                              ,[FProductionLineID]
                                                              ,[FReleasedQty]
                                                              ,[FReleasedAuxQty]
                                                              ,[FUnScheduledQty]
                                                              ,[FUnScheduledAuxQty]
                                                              ,[FSubEntryID]
                                                              ,[FScheduleID]
                                                              ,[FPlanOrderInterID]
                                                              ,[FProcessPrice]
                                                              ,[FProcessFee]
                                                              ,[FGMPBatchNo]
                                                              ,[FGMPCollectRate]
                                                              ,[FGMPItemBalance]
                                                              ,[FGMPBulkQty]
                                                              ,[FCustID]
                                                              ,[FMultiCheckLevel1]
                                                              ,[FMultiCheckLevel2]
                                                              ,[FMultiCheckLevel3]
                                                              ,[FMultiCheckLevel4]
                                                              ,[FMultiCheckLevel5]
                                                              ,[FMultiCheckLevel6]
                                                              ,[FMultiCheckDate1]
                                                              ,[FMultiCheckDate2]
                                                              ,[FMultiCheckDate3]
                                                              ,[FMultiCheckDate4]
                                                              ,[FMultiCheckDate5]
                                                              ,[FMultiCheckDate6]
                                                              ,[FCurCheckLevel]
                                                              ,[FMRPLockFlag]
                                                              ,[FHandworkClose]
                                                              ,[FConfirmerID]
                                                              ,[FConfirmDate]
                                                              ,[FInHighLimit]
                                                              ,[FInHighLimitQty]
                                                              ,[FAuxInHighLimitQty]
                                                              ,[FInLowLimit]
                                                              ,[FInLowLimitQty]
                                                              ,[FAuxInLowLimitQty]
                                                              ,[FChangeTimes]
                                                              ,[FCheckCommitQty]
                                                              ,[FAuxCheckCommitQty]
                                                              ,[FCloseDate]
                                                              ,[FPlanConfirmed]
                                                              ,[FPlanMode]
                                                              ,[FMTONo]
                                                              ,[FPrintCount]
                                                              ,[FFinClosed]
                                                              ,[FFinCloseer]
                                                              ,[FFinClosedate]
                                                              ,[FStockFlag]
                                                              ,[FStartFlag]
                                                              ,[FVchBillNo]
                                                              ,[FVchInterID]
                                                              ,[FCardClosed]
                                                              ,[FHRReadyTime]
                                                              ,[FPlanCategory]
                                                              ,[FBomCategory]
                                                              ,[FSourceTranType]
                                                              ,[FSourceInterId]
                                                              ,[FSourceBillNo]
                                                              ,[FReprocessedAuxQty]
                                                              ,[FReprocessedQty]
                                                              ,[FSelDiscardStockInAuxQty]
                                                              ,[FSelDiscardStockInQty]
                                                              ,[FDiscardStockInAuxQty]
                                                              ,[FDiscardStockInQty]
                                                              ,[FSampleBreakAuxQty]
                                                              ,[FSampleBreakQty]
                                                              ,[FResourceID]");
        #endregion

        public SplitDetail(DataRow dr, string myWrokShop, DateTime mydtBegin, DateTime mydtEnd)
        {
            InitializeComponent();
            this.drOrg = dr;

            WrokShop = myWrokShop;
            dtBegin = mydtBegin;
            dtEnd = mydtEnd;

        }

        private void SplitDetail_Load(object sender, EventArgs e)
        {
            //清空表体
            if(drOrg == null)
            {
                gridControl1.DataSource = null;
                return;
            }

            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string strConn = config.AppSettings.Settings["connectionstring"].Value;//this.txtUrl.Text.Trim();
        

            DataTable dtTmp =  SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "pr_xc_SelectDepaertment").Tables[0];
            //dtTmp.Columns.Add("ID");
            //dtTmp.Columns.Add("Name");
            //DataRow drTmp = dtTmp.NewRow();
            //drTmp[0] = 322;
            //drTmp[1] = "制造1课";
            //dtTmp.Rows.Add(drTmp);

            //drTmp = dtTmp.NewRow();
            //drTmp[0] = 323;
            //drTmp[1] = "制造2课";
            //dtTmp.Rows.Add(drTmp);
           

            repositoryItemLookUpEdit1.DataSource = dtTmp;
            repositoryItemLookUpEdit1.DisplayMember = "Name";
            repositoryItemLookUpEdit1.ValueMember = "ID";

            DataTable dtt = dtTmp.Copy();
            this.comboBox1.DataSource = dtt;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "ID";

            this.gridView1.OptionsView.ColumnAutoWidth = false;

            //初始化空列表
            string sSql = string.Format(" select '' FBillNo,322 FWorkShopName,'' FLongNumber, '' FItemName, '' FUnitIDName, 0 FOrgAuxqty, 0 Fauxqty, '2019-01-01' FPlanBeginDate, '2019-01-01' FPlanFinishDate, 0 FInterID, '' fmodel,'' cust, ''  saleBillno, '2019-1-1'  DelivDate   ,0  SaleOrderEntryID,'' FIndetidy");

    

           // DataTable 
            dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sSql).Tables[0];
           //     dt = HJ.Data.SQLDBConnect.SQLDBconntion.ExecuteDataSet(sSql).Tables[0];
            dt.Rows.Clear();

            //根据原生产任务单信息，计划数量/产能=多少行数
            DateTime dtPlanBegin = Convert.ToDateTime(drOrg["FPlanBeginDate"]);
            double dProduceQty = Convert.ToDouble(drOrg["FProduceQty"]);
            this.txtchafen.Text = dProduceQty.ToString();
            double iPlanQty = Convert.ToDouble(drOrg["Fauxqty"]);
            double iRemainQty = 0;
            int iRowCount = 0;
            if (dProduceQty > 0)
            {
                //多少行
                iRowCount = int.Parse(Math.Ceiling(iPlanQty / dProduceQty).ToString());
                //拆分后，剩下余数
                iRemainQty = iPlanQty % dProduceQty;
            }

             Random ro = new Random();
             IndenNum = System.DateTime.Now.ToString() + "," + ro.Next().ToString();


             DataTable drSaleData = SqlHelper.ExecuteDataset(strConn, "pr_xc_GetDetailData", int.Parse(drOrg["FInterID"].ToString())).Tables[0];

             int depaetid = 0;
             IDataReader drd = SqlHelper.ExecuteReader(strConn, "pr_xc_getDepartmentID", drOrg["FWorkShopName"].ToString());

            if( drd.Read())
            {
                depaetid =int.Parse( drd[0].ToString());
            }
            double sunICMOQty = 0;
            //for (int i = 0; i < iRowCount; i++)
            //{
            //    if (iRemainQty > 0 && i == iRowCount - 1)
            //    {
            //        sunICMOQty = iRemainQty;
            //    }
            //    else
            //    {
            //        sunICMOQty = dProduceQty;
            //    }
                DataRow dr = dt.NewRow();
                dr["FBillNo"] = string.Format("{0}_{1}", drOrg["FBillNo"], 0 + 1);
                dr["FWorkShopName"] = depaetid;// 322;// drOrg["FWorkShopName"];
                dr["FLongNumber"] = drOrg["FLongNumber"];
                dr["FItemName"] = drOrg["FItemName"];
                dr["FUnitIDName"] = drOrg["FUnitIDName"];
                //计划产量
                dr["FOrgAuxqty"] = drOrg["Fauxqty"];
                dr["Fauxqty"] = sunICMOQty;
                dr["FPlanBeginDate"] = dtPlanBegin.AddDays(0);
                dr["FPlanFinishDate"] = dtPlanBegin.AddDays(0);

                dr["fmodel"] =  drSaleData.Rows[0]["FModel"];
                dr["cust"] = drSaleData.Rows[0]["FCustName"];
                dr["saleBillno"] = drSaleData.Rows[0]["FSourceBillNo"];
                dr["DelivDate"] = drSaleData.Rows[0]["FDate"];
                //  dr["SaleItem"] = drSaleData.Rows[0]["FNumber"];
                dr["SaleOrderEntryID"] = drSaleData.Rows[0]["fentryid"];


                dr["FIndetidy"] = IndenNum;
                //  dr["FInterID"] = drOrg["FInterID"];

                //  '' fmodel,'' cust, ''  saleBillno, '2019-1-1'  'DelivDate'   ,''  SaleItem,
                dt.Rows.Add(dr);


            //}

            ////余数行
            //if (iRemainQty > 0)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["FBillNo"] = string.Format("{0}_{1}", drOrg["FBillNo"], iRowCount); 
            //    dr["FWorkShopName"] = drOrg["FWorkShopName"];
            //    dr["FLongNumber"] = drOrg["FLongNumber"];
            //    dr["FItemName"] = drOrg["FItemName"];
            //    dr["FUnitIDName"] = drOrg["FUnitIDName"];
            //    dr["FOrgAuxqty"] = drOrg["Fauxqty"];
            //    dr["Fauxqty"] = dProduceQty; //iRemainQty;// dProduceQty;
            //    dr["FPlanBeginDate"] = dtPlanBegin.AddDays(iRowCount); ;
            //    dr["FPlanFinishDate"] = dtPlanBegin.AddDays(iRowCount);
            //    dt.Rows.Add(dr);
            //}

            //设置原行信息
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Rows[0]["FInterID"] = drOrg["FInterID"];
               // dt.Rows[0]["FBillNo"] = drOrg["FBillNo"];
            }

            //赋值数据给表体
            gridControl1.DataSource = dt;

              DevExpress.XtraGrid.Columns.GridColumn col_Profit = gridView1.Columns[9];
            gridView1.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
           // gridView1.Columns[9].SummaryItem.DisplayFormat ="合计：{0：n2}";




        }

        private void DtToDb(string strconnection, string tableName, System.Data.DataTable dt)
        {
            System.Data.SqlClient.SqlBulkCopy sbc = new System.Data.SqlClient.SqlBulkCopy(strconnection, System.Data.SqlClient.SqlBulkCopyOptions.UseInternalTransaction);
            sbc.NotifyAfter = dt.Rows.Count;
            sbc.DestinationTableName = tableName;
            sbc.WriteToServer(dt);
        }

        private void tbConfirm_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string strConn = config.AppSettings.Settings["connectionstring"].Value;//this.txtUrl.Text.Trim();


          


      
            //检查数据
            if (!checkData()) return;

            try
             {

                 Random ro = new Random();
                 string  myIndenNum = System.DateTime.Now.ToString() + "," + ro.Next().ToString();

                int i;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["FIndetidy"] = myIndenNum;
                }
                

                  DtToDb(strConn, "t_xc_ICMO", dt);

                  SqlHelper.ExecuteNonQuery(strConn, "pr_xc_GenICMO", dt.Rows[0]["FInterID"], myIndenNum);

                    MessageBox.Show("操作完成！", "金蝶提示");


                    this.tbConfirm.Enabled = false;
                    this.Close();
              }

            catch (Exception ex)
            {
                this.tbConfirm.Enabled = false;
                MessageBox.Show("生成生产任务单失败：" + ex.Message, "金蝶提示");
            }

//            //转为tabledate
//            DataTable dt_CreateNew = gridControl1.DataSource as DataTable;

//            if (dt_CreateNew != null && dt_CreateNew.Rows.Count > 0)
//            {
//                StringBuilder sSqlBuilder = new StringBuilder();
//                sSqlBuilder.Append(string.Format(@" declare @interID int
//                                                    set @interID=0 "));
//                //生成SQL
//                foreach (DataRow dr in dt_CreateNew.Rows)
//                {
//                    //修改原单
//                    if (!string.IsNullOrWhiteSpace(Convert.ToString(dr["FInterID"])) && Convert.ToInt32(dr["FInterID"]) > 0)
//                    {
//                        sSqlBuilder.Append(string.Format(" update ICMO set FQty = {1},FPlanCommitDate = '{2}',FPlanFinishDate = '{3}',FAuxQty={1},FInHighLimitQty={1},FAuxInHighLimitQty={1},FInLowLimitQty={1},FAuxInLowLimitQty={1} where FInterID = {0}  ", dr["FInterID"], dr["Fauxqty"], dr["FPlanBeginDate"], dr["FPlanFinishDate"]));
//                    }
//                    //新增生产任务单
//                    else
//                    {
//                        //根据原单信息插表，修改对应的信息
//                        sSqlBuilder.Append(string.Format(@" exec GetICMaxNum 'ICMO',@interID output,1  "));
//                        sSqlBuilder.Append(string.Format(@" insert into ICMO ( {0} )
//                                                        SELECT [FBrNo]
//                                                              ,@interID [FInterID]
//                                                              ,'{1}' [FBillNo]
//                                                              ,[FTranType]
//                                                              ,[FStatus]
//                                                              ,[FMRP]
//                                                              ,[FType]
//                                                              ,[FWorkShop]
//                                                              ,[FItemID]
//                                                              ,{2} [FQty]
//                                                              ,[FCommitQty]
//                                                              ,'{3}' [FPlanCommitDate]
//                                                              ,'{4}' [FPlanFinishDate]
//                                                              ,[FConveyerID]
//                                                              ,[FCommitDate]
//                                                              ,[FCheckerID]
//                                                              ,[FCheckDate]
//                                                              ,[FRequesterID]
//                                                              ,[FBillerID]
//                                                              ,[FSourceEntryID]
//                                                              ,[FClosed]
//                                                              ,[FNote]
//                                                              ,[FUnitID]
//                                                              ,[FAuxCommitQty]
//                                                              ,{2} [FAuxQty]
//                                                              ,{5} [FOrderInterID]
//                                                              ,[FPPOrderInterID]
//                                                              ,[FParentInterID]
//                                                              ,[FCancellation]
//                                                              ,[FSupplyID]
//                                                              ,[FQtyFinish]
//                                                              ,[FQtyScrap]
//                                                              ,[FQtyForItem]
//                                                              ,[FQtyLost]
//                                                              ,[FPlanIssueDate]
//                                                              ,[FRoutingID]
//                                                              ,[FStartDate]
//                                                              ,[FFinishDate]
//                                                              ,[FAuxQtyFinish]
//                                                              ,[FAuxQtyScrap]
//                                                              ,[FAuxQtyForItem]
//                                                              ,[FAuxQtyLost]
//                                                              ,[FMrpClosed]
//                                                              ,[FBomInterID]
//                                                              ,[FQtyPass]
//                                                              ,[FAuxQtyPass]
//                                                              ,[FQtyBack]
//                                                              ,[FAuxQtyBack]
//                                                              ,[FFinishTime]
//                                                              ,[FReadyTIme]
//                                                              ,[FPowerCutTime]
//                                                              ,[FFixTime]
//                                                              ,[FWaitItemTime]
//                                                              ,[FWaitToolTime]
//                                                              ,[FTaskID]
//                                                              ,[FWorkTypeID]
//                                                              ,[FCostObjID]
//                                                              ,[FStockQty]
//                                                              ,[FAuxStockQty]
//                                                              ,[FSuspend]
//                                                              ,[FProjectNO]
//                                                              ,[FProductionLineID]
//                                                              ,[FReleasedQty]
//                                                              ,[FReleasedAuxQty]
//                                                              ,[FUnScheduledQty]
//                                                              ,[FUnScheduledAuxQty]
//                                                              ,[FSubEntryID]
//                                                              ,[FScheduleID]
//                                                              ,[FPlanOrderInterID]
//                                                              ,[FProcessPrice]
//                                                              ,[FProcessFee]
//                                                              ,[FGMPBatchNo]
//                                                              ,[FGMPCollectRate]
//                                                              ,[FGMPItemBalance]
//                                                              ,[FGMPBulkQty]
//                                                              ,[FCustID]
//                                                              ,[FMultiCheckLevel1]
//                                                              ,[FMultiCheckLevel2]
//                                                              ,[FMultiCheckLevel3]
//                                                              ,[FMultiCheckLevel4]
//                                                              ,[FMultiCheckLevel5]
//                                                              ,[FMultiCheckLevel6]
//                                                              ,[FMultiCheckDate1]
//                                                              ,[FMultiCheckDate2]
//                                                              ,[FMultiCheckDate3]
//                                                              ,[FMultiCheckDate4]
//                                                              ,[FMultiCheckDate5]
//                                                              ,[FMultiCheckDate6]
//                                                              ,[FCurCheckLevel]
//                                                              ,[FMRPLockFlag]
//                                                              ,[FHandworkClose]
//                                                              ,[FConfirmerID]
//                                                              ,[FConfirmDate]
//                                                              ,[FInHighLimit]
//                                                              ,{2} [FInHighLimitQty]
//                                                              ,{2} [FAuxInHighLimitQty]
//                                                              ,[FInLowLimit]
//                                                              ,{2} [FInLowLimitQty]
//                                                              ,{2} [FAuxInLowLimitQty]
//                                                              ,[FChangeTimes]
//                                                              ,[FCheckCommitQty]
//                                                              ,[FAuxCheckCommitQty]
//                                                              ,[FCloseDate]
//                                                              ,[FPlanConfirmed]
//                                                              ,[FPlanMode]
//                                                              ,[FMTONo]
//                                                              ,[FPrintCount]
//                                                              ,[FFinClosed]
//                                                              ,[FFinCloseer]
//                                                              ,[FFinClosedate]
//                                                              ,[FStockFlag]
//                                                              ,[FStartFlag]
//                                                              ,[FVchBillNo]
//                                                              ,[FVchInterID]
//                                                              ,[FCardClosed]
//                                                              ,[FHRReadyTime]
//                                                              ,[FPlanCategory]
//                                                              ,[FBomCategory]
//                                                              ,[FSourceTranType]
//                                                              ,[FSourceInterId]
//                                                              ,[FSourceBillNo]
//                                                              ,[FReprocessedAuxQty]
//                                                              ,[FReprocessedQty]
//                                                              ,[FSelDiscardStockInAuxQty]
//                                                              ,[FSelDiscardStockInQty]
//                                                              ,[FDiscardStockInAuxQty]
//                                                              ,[FDiscardStockInQty]
//                                                              ,[FSampleBreakAuxQty]
//                                                              ,[FSampleBreakQty]
//                                                              ,[FResourceID]
//                                                          FROM [ICMO] where FInterID = '{5}'  
//                                                             ", sSqlField, dr["FBillNo"], dr["Fauxqty"], dr["FPlanBeginDate"], dr["FPlanFinishDate"], drOrg["FInterID"]));
                    
//                    }
//                }
//                try
//                {
//                    HJ.Data.SQLDBConnect.SQLDBconntion.SQLTransBegin();
//                    HJ.Data.SQLDBConnect.SQLDBconntion.ExecuteNonQuery(sSqlBuilder.ToString());
//                    HJ.Data.SQLDBConnect.SQLDBconntion.SQLTransCommit();
//                    MessageBox.Show( "已经批量生成生产任务单！", "金蝶提示");
//                    this.Close();
//                }
//                catch (Exception ex)
//                {
//                    HJ.Data.SQLDBConnect.SQLDBconntion.SQLRollback();
//                    MessageBox.Show("生成生产任务单失败：" + ex.Message, "金蝶提示");
//                }
//            }
        }

        private void tbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            //设置右键菜单状态
            //获取选择的行数
            int[] iSelects = gridView1.GetSelectedRows();
            tsmiCopyRow.Enabled = false;
            tsmiPasterRow.Enabled = false;
            tsmiDelRow.Enabled = false;
            tsmiInsRow.Enabled = false;

            if (iSelects != null && iSelects[0] == 0)
            {
                tsmiCopyRow.Enabled = true;
                tsmiPasterRow.Enabled = true;
                tsmiDelRow.Enabled = false;
                tsmiInsRow.Enabled = true;
            }
            else if (iSelects != null && iSelects[0] > 0)
            {
                tsmiCopyRow.Enabled = true;
                tsmiPasterRow.Enabled = true;
                tsmiDelRow.Enabled = true;
                tsmiInsRow.Enabled = true;
            }

            if (copyRows == null)
            {
                tsmiPasterRow.Enabled = false;
                tsmiInsRow.Enabled = false;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            int[] iSelects = gridView1.GetSelectedRows();

            switch ((e.ClickedItem).Name)
            {
                case "tsmiCopyRow":
                    this.tbCopyRow_Click(null, null);
                    break;
                case "tsmiPasterRow":
                    this.tbPasterRow_Click(null, null);
                    break;
                case "tsmiDelRow":
                    this.tbDelRow_Click(null, null);
                    break;
                //case "tsmiInsRow":
                //    this.tbInsRow_Click(null, null);
                //    break;
            }
        }


        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //用颜色判断哪条是原单信息，不能被删
            string sFInterID = gridView1.GetRowCellValue(e.RowHandle, "FInterID").ToString();
            if (!string.IsNullOrWhiteSpace(sFInterID) && Convert.ToInt32(sFInterID) > 0)
            {
                e.Appearance.BackColor = Color.Yellow;//改变背景色
                //e.Appearance.ForeColor = Color.Red;//改变字体颜色
            }
        }

        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataRow CopyRow(DataRow dr, DataTable dt)
        {

            DataRow copyRow = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName != "FInterID")
                    copyRow[dc.ColumnName] = dr[dc.ColumnName];
            }

            return copyRow;
        }


        private void tbCopyRow_Click(object sender, EventArgs e)
        {
            
            DataTable dt = gridControl1.DataSource as DataTable;
            int[] iSelects = gridView1.GetSelectedRows();

            //获取对应的行
            if (iSelects != null && iSelects.Length > 0)
            {
                copyRows = new DataRow[iSelects.Length];
                for (int i = 0; i < iSelects.Length; i++)
                {
                    copyRows[i] = this.gridView1.GetDataRow(iSelects[i]);
                }
            }
            else
            {
                copyRows = null;
            }
        }

        /// <summary>
        /// 粘贴行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPasterRow_Click(object sender, EventArgs e)
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            int[] iSelects = gridView1.GetSelectedRows();

            if (dt != null && copyRows != null)
            {
                int rowCount = dt.Rows.Count;
                string EndFbillNo = dt.Rows[rowCount - 1]["FBillNo"].ToString();
               // MessageBox.Show(dt.Rows[rowCount - 1]["FBillNo"].ToString());

                string[] sArray = EndFbillNo.Split('_');
                int begNum =int.Parse( sArray[sArray.Length - 1].ToString());
                string OrgNum = EndFbillNo.Substring(0,EndFbillNo.Length-sArray[sArray.Length - 1].ToString().Length);

               // MessageBox.Show(sArray[sArray.Length-1].ToString());
                //dr["FBillNo"] = string.Format("{0}_{1}", drOrg["FBillNo"], i + 1);
                string OrgBillNo = "";
                foreach (DataRow dr in copyRows)
                {
                    begNum = begNum + 1;

                    OrgBillNo = dr["FBillNo"].ToString();
                    dr["FBillNo"] = string.Format("{0}{1}", OrgNum, begNum);
                    dt.Rows.Add(CopyRow(dr, dt));
                    dr["FBillNo"] = OrgBillNo;

                 
                }
                gridControl1.Refresh();
            }
        }

        //删除行
        private void tbDelRow_Click(object sender, EventArgs e)
        {
            int[] iSelects = gridView1.GetSelectedRows();
            if (iSelects != null && iSelects.Length > 0)
            {
                DataRow dr = this.gridView1.GetDataRow(iSelects[0]);
                if (!string.IsNullOrWhiteSpace(Convert.ToString( dr["FInterID"]) ) && Convert.ToInt32(Convert.ToString( dr["FInterID"] ) ) > 0)
                {
                    return;
                }
            }
            //删除选中行
            gridView1.DeleteSelectedRows();
            gridControl1.Refresh();
        }

        //插入行
        private void tbInsRow_Click(object sender, EventArgs e)
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            int[] iSelects = gridView1.GetSelectedRows();

            if (dt != null && copyRows != null)
            {
                if (iSelects != null && iSelects.Length > 0)
                {
                    int iRow = 0;
                    foreach (DataRow dr in copyRows)
                    {
                        dt.Rows.InsertAt(CopyRow(dr, dt), iSelects[iRow++]);
                    }
                    gridControl1.Refresh();
                }
            }
        }

        /// <summary>
        /// 行数
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


        /// <summary>
        /// 检查数据
        /// </summary>
        /// <returns></returns>
        private bool checkData()
        {
            DataTable dt = gridControl1.DataSource as DataTable;

            #region 检测数量
            double dAuxQtys = 0.0;
            if (dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    dAuxQtys += Convert.ToDouble(dr["Fauxqty"]);
                }
            }

            if (dAuxQtys != Convert.ToDouble(drOrg["Fauxqty"]))
            {
                MessageBox.Show(string.Format("计划数量不等于【{0}】，不能确认", drOrg["Fauxqty"]), "温馨提示");
                return false;
            }

            #endregion


            #region 检测单号
            List<string> lBillNos = new List<string>();

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string sFBillNo = Convert.ToString(dr["FBillNo"]);
                    if (lBillNos.Contains(sFBillNo))
                    {
                        MessageBox.Show(string.Format("单据号【{0}】重复", sFBillNo), "温馨提示");
                        return false;
                    }
                    lBillNos.Add(sFBillNo);
                }
            }
            #endregion


            foreach (DataRow dr in dt.Rows)
            {
                string FWorkShopName = Convert.ToString(dr["FWorkShopName"]);
                if (FWorkShopName.Length<=0)
                {
                    MessageBox.Show("车间必须录入", "温馨提示");
                    return false;
                }
                
            }

            return true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            System.TimeSpan t3 = dtEnd - dtBegin;  //两个时间相减 。默认得到的是 两个时间之间的天数   得到：365.00:00:00 

            double getDay = t3.TotalDays;

            if (getDay < 50)
            {

                frmBHD bhd = new frmBHD(WrokShop, dtBegin, dtEnd);
                bhd.ShowDialog();
            }
            else
            {
                MessageBox.Show( "为了保证查看统计的速度，统计只能看50天的数据","金蝶提示");
            }
            
          
        }

        private void SplitDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void tbBatcheUpdateWS_Click(object sender, EventArgs e)
        {
            getYS();
          

        }

        private void getYS()
        {
            double dAuxQtys = 0.0;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dAuxQtys += Convert.ToDouble(dr["Fauxqty"]);
                }
            }

            this.textBoxYS.Text = (Convert.ToDouble(drOrg["Fauxqty"]) - dAuxQtys).ToString();
        }

        private void buttonCX_Click(object sender, EventArgs e)
        {
            int rowindex = this.gridView1.GetFocusedDataSourceRowIndex();
            int i = 0;
            if (rowindex >= 0)
            {


                i = rowindex;
            }

            int j = 0;
            for (j = i; j < dt.Rows.Count; j++)
            {
                dt.Rows[j]["FWorkShopName"] = this.comboBox1.SelectedValue.ToString();

            }
            dt.AcceptChanges();
            gridControl1.Refresh();
        }
        //根据时间日期批改
        private void buttonRQ_Click(object sender, EventArgs e)
        {
            int rowindex = this.gridView1.GetFocusedDataSourceRowIndex();
            int i = 0;
            if (rowindex >= 0)
            {
                i = rowindex;
            }
            int j = 0;
            DateTime NewDT = this.dateTimePicker1.Value;
            int addDay = 0;
            int addDay1 = 0;
            for (j = i; j < dt.Rows.Count; j++)
            {
                String time1 = NewDT.AddDays(addDay).ToShortDateString();
                //String[] text = time1.Split(new char[] { ' ', '-' });
                //int y = Int32.Parse(text[0].ToString());
                //int m = Int32.Parse(text[1].ToString());
                //int d = Int32.Parse(text[2].ToString());

                DateTime dtn = DateTime.Parse(time1);
                // String[] text = time.Split(new char[] { ' ', '-' });
                //int y = Int32.Parse(text[0].ToString());
                //int m = Int32.Parse(text[1].ToString());
                //int d = Int32.Parse(text[2].ToString());
                int y = dtn.Year;
                int m = dtn.Month;
                int d = dtn.Day;
                if (m == 1 || m == 2)
                {
                    m += 12;
                    y--;
                }
                int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400 + 1) % 7;
                if (week != 0)
                {
                    dt.Rows[j]["FPlanBeginDate"] = NewDT.AddDays(addDay).ToShortDateString();
                }
                else {
                    dt.Rows[j]["FPlanBeginDate"] = NewDT.AddDays(addDay+1).ToShortDateString();
                    addDay = addDay + 2;
                    continue;
                }
                //FPlanBeginDate, '2019-01-01' FPlanFinishDate, '2019-01-01' FPlanFinishDate
              
                addDay = addDay + 1;

            }
            for (j = i; j < dt.Rows.Count; j++)
            {
                String time2 = NewDT.AddDays(addDay1).ToShortDateString();
                //String[] text1 = time2.Split(new char[] { ' ', '-' });
                //int y1 = Int32.Parse(text1[0].ToString());
                //int m1 = Int32.Parse(text1[1].ToString());
                //int d1 = Int32.Parse(text1[2].ToString());

                DateTime dtn = DateTime.Parse(time2);
                // String[] text = time.Split(new char[] { ' ', '-' });
                //int y = Int32.Parse(text[0].ToString());
                //int m = Int32.Parse(text[1].ToString());
                //int d = Int32.Parse(text[2].ToString());
                int y1 = dtn.Year;
                int m1 = dtn.Month;
                int d1 = dtn.Day;
                if (m1 == 1 || m1 == 2)
                {
                    m1 += 12;
                    y1--;
                }
                int week1 = (d1 + 2 * m1 + 3 * (m1 + 1) / 5 + y1 + y1 / 4 - y1 / 100 + y1 / 400 + 1) % 7;
                if (week1 != 0)
                {
                    dt.Rows[j]["FPlanFinishDate"] = NewDT.AddDays(addDay1).ToShortDateString();
                }
                else
                {
                    dt.Rows[j]["FPlanFinishDate"] = NewDT.AddDays(addDay1 + 1).ToShortDateString();
                    addDay1 = addDay1 + 2;
                    continue;
                }
                addDay1 = addDay1 + 1;
            }
                        
            dt.AcceptChanges();
            gridControl1.Refresh();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.C)
            {
                Clipboard.SetDataObject(gridView1.GetFocusedRowCellValue(gridView1.FocusedColumn));
                e.Handled = true;
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }


        //点击拆分按钮，进行拆分
        private void btnchafen_Click(object sender, EventArgs e)
        {
           // DataRow dr = dt.NewRow();
            
            int num = Int32.Parse(this.txtchafen.Text.Trim());

          

            //清空表体
            if (drOrg == null)
            {
                gridControl1.DataSource = null;
                return;
            }

            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string strConn = config.AppSettings.Settings["connectionstring"].Value;//this.txtUrl.Text.Trim();


            DataTable dtTmp = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "pr_xc_SelectDepaertment").Tables[0];
            //dtTmp.Columns.Add("ID");
            //dtTmp.Columns.Add("Name");
            //DataRow drTmp = dtTmp.NewRow();
            //drTmp[0] = 322;
            //drTmp[1] = "制造1课";
            //dtTmp.Rows.Add(drTmp);

            //drTmp = dtTmp.NewRow();
            //drTmp[0] = 323;
            //drTmp[1] = "制造2课";
            //dtTmp.Rows.Add(drTmp);


            repositoryItemLookUpEdit1.DataSource = dtTmp;
            repositoryItemLookUpEdit1.DisplayMember = "Name";
            repositoryItemLookUpEdit1.ValueMember = "ID";

            DataTable dtt = dtTmp.Copy();
            this.comboBox1.DataSource = dtt;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "ID";

            this.gridView1.OptionsView.ColumnAutoWidth = false;

            //初始化空列表
            string sSql = string.Format(" select '' FBillNo,322 FWorkShopName,'' FLongNumber, '' FItemName, '' FUnitIDName, 0 FOrgAuxqty, 0 Fauxqty, '2019-01-01' FPlanBeginDate, '2019-01-01' FPlanFinishDate, 0 FInterID, '' fmodel,'' cust, ''  saleBillno, '2019-1-1'  DelivDate   ,0  SaleOrderEntryID,'' FIndetidy");

            // DataTable 
            dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sSql).Tables[0];
        
            dt.Rows.Clear();
        
            //根据原生产任务单信息，计划数量/产能=多少行数
            DateTime dtPlanBegin = Convert.ToDateTime(drOrg["FPlanBeginDate"]);
            double dProduceQty = num; // Convert.ToDouble(drOrg["FProduceQty"]);
            double iPlanQty = Convert.ToDouble(drOrg["Fauxqty"]);
            double iRemainQty = 0;
            int iRowCount = 0;
            if (dProduceQty > 0)
            {
                //多少行
                iRowCount = int.Parse(Math.Ceiling(iPlanQty / dProduceQty).ToString());
                //拆分后，剩下余数
                iRemainQty = iPlanQty % dProduceQty;
            }

            Random ro = new Random();
            IndenNum = System.DateTime.Now.ToString() + "," + ro.Next().ToString();


            DataTable drSaleData = SqlHelper.ExecuteDataset(strConn, "pr_xc_GetDetailData", int.Parse(drOrg["FInterID"].ToString())).Tables[0];

            int depaetid = 0;
            IDataReader drd = SqlHelper.ExecuteReader(strConn, "pr_xc_getDepartmentID", drOrg["FWorkShopName"].ToString());

            if (drd.Read())
            {
                
                depaetid = int.Parse(drd[0].ToString());
                
            }
            double sunICMOQty = 0;
            int add = 0;
            int RowCount = 0;
       
            for (int i = 0; i < iRowCount; i++)
            {
                if (iRemainQty > 0 && i == iRowCount - 1)
                {
                    sunICMOQty = iRemainQty;
                }
                else
                {
                    sunICMOQty = dProduceQty;
            }
            DataRow dr = dt.NewRow();
            RowCount += 1;
            dr["FBillNo"] = string.Format("{0}_{1}", drOrg["FBillNo"], RowCount);
            dr["FWorkShopName"] = depaetid;// 322;// drOrg["FWorkShopName"];
            dr["FLongNumber"] = drOrg["FLongNumber"];
            dr["FItemName"] = drOrg["FItemName"];
            dr["FUnitIDName"] = drOrg["FUnitIDName"];
            dr["FOrgAuxqty"] = drOrg["Fauxqty"];
            dr["Fauxqty"] = sunICMOQty;
            String time = dtPlanBegin.AddDays(add).ToString();
            DateTime dtn = DateTime.Parse(time);
           // String[] text = time.Split(new char[] { ' ', '-' });
            //int y = Int32.Parse(text[0].ToString());
            //int m = Int32.Parse(text[1].ToString());
            //int d = Int32.Parse(text[2].ToString());
            int y = dtn.Year;
            int m = dtn.Month;
            int d = dtn.Day;

            if (m == 1 || m == 2)
            {
                m += 12;
                y--;
            }
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400 + 1) % 7;
            if (week != 0)
            {
                dr["FPlanBeginDate"] = dtPlanBegin.AddDays(add);
                dr["FPlanFinishDate"] = dtPlanBegin.AddDays(add);

                dr["fmodel"] = drSaleData.Rows[0]["FModel"];
                dr["cust"] = drSaleData.Rows[0]["FCustName"];
                dr["saleBillno"] = drSaleData.Rows[0]["FSourceBillNo"];
                dr["DelivDate"] = drSaleData.Rows[0]["FDate"];

                dr["SaleOrderEntryID"] = drSaleData.Rows[0]["fentryid"];
                dr["FIndetidy"] = IndenNum;
                dt.Rows.Add(dr);
            }
            else
            {
                dr["FPlanBeginDate"] = dtPlanBegin.AddDays(add+1);
                dr["FPlanFinishDate"] = dtPlanBegin.AddDays(add+1);
                add += 2;
                dr["fmodel"] = drSaleData.Rows[0]["FModel"];
                dr["cust"] = drSaleData.Rows[0]["FCustName"];
                dr["saleBillno"] = drSaleData.Rows[0]["FSourceBillNo"];
                dr["DelivDate"] = drSaleData.Rows[0]["FDate"];
                dr["SaleOrderEntryID"] = drSaleData.Rows[0]["fentryid"];


                dr["FIndetidy"] = IndenNum;
                dt.Rows.Add(dr);
                continue;
            }
            add += 1;
           

            }
            //for (int i = 0; i < iRowCount; i++)
            //{
            //    if (iRemainQty > 0 && i == iRowCount - 1)
            //    {
            //        sunICMOQty = iRemainQty;
            //    }
            //    else
            //    {
            //        sunICMOQty = dProduceQty;
            //    }
                
            //    String time = dtPlanBegin.AddDays(add).ToString();
            //    String[] text = time.Split(new char[] { ' ', '-' });
            //    int y = Int32.Parse(text[0].ToString());
            //    int m = Int32.Parse(text[1].ToString());
            //    int d = Int32.Parse(text[2].ToString());
            //    if (m == 1 || m == 2)
            //    {
            //        m += 12;
            //        y--;
            //    }
            //    int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400 + 1) % 7;
            //    if (week != 0)
            //    {

            //        dr["FPlanFinishDate"] = dtPlanBegin.AddDays(add);
            //        dt.Rows.Add(dr);
            //    }
            //    else
            //    {

            //        dr["FPlanFinishDate"] = dtPlanBegin.AddDays(add + 1);
            //        add += 2;

            //        dt.Rows.Add(dr);
            //        continue;
            //    }
            //    add += 1;

            //}
            //设置原行信息
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Rows[0]["FInterID"] = drOrg["FInterID"];
                // dt.Rows[0]["FBillNo"] = drOrg["FBillNo"];
            }

            //赋值数据给表体
            gridControl1.DataSource = dt;
            DevExpress.XtraGrid.Columns.GridColumn col_Profit = gridView1.Columns[9];
            gridView1.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            // gridView1.Columns[9].SummaryItem.DisplayFormat ="合计：{0：n2}";








            
        }


    }
}

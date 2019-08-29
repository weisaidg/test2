using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OcdIcmoSplit
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //如果参数大于0，表明是通过外接打开传来的参数，否则连接App.config文件的测试连接
            if (args.Length > 0)
            {
                try
                {
                    //处理外接连接串
                    //string sBaseInfo = string.Join(" ", args);
                    //string[] arrBaseInfo = sBaseInfo.Split('|');
                    //HJ.Data.SQLDBConnect.DbConnectInit(formatConnection(arrBaseInfo[0]));

                    //HJ.Systems.Setting.UserID = arrBaseInfo[1]; //登录的ID
                    //HJ.Systems.Setting.UserName = arrBaseInfo[2];
            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                bool b = GetDataConn();
                HJ.Systems.Setting.UserName = "Demo";
                HJ.Systems.Setting.UserID = "Demo";
                HJ.Systems.Setting.OperateDate = DateTime.Now;
            }

            //if (GetRegist() != "16E63808-3A1B-4022-8023-1B8CF0A7206B")
            //{
            //    if (HJ.Systems.Setting.OperateDate >= Convert.ToDateTime("2014-05-01"))
            //    {
            //        return;
            //    }
            //}
            HJ.Resources.HJResource.ChanageLauage("zh-HK");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplitIcmo());
        }


        private static string formatConnection(string connstring)
        {
            string strconn = string.Empty;
            foreach (string str in connstring.Split(';'))
            {
                if (!str.ToLower().Contains("provider"))
                {
                    strconn += str + ";";
                }
            }
            return strconn;
        }

        /// <summary>
        /// 获取测试版的数据库连接串
        /// </summary>
        /// <returns></returns>
        private static bool GetDataConn()
        {
            string strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            // MessageBox.Show(strFileName);
            //HJ.Base .Common .ConfigFile.UpdateConfig 
            string conn = "workstation id=" + HJ.Base.Common.ConfigFile.ReadConfig("ServerName", strFileName) + ";data source =" + HJ.Base.Common.ConfigFile.ReadConfig("ServerName", strFileName) + ";packet size=4096;user id=" + HJ.Base.Common.ConfigFile.ReadConfig("Uid", strFileName) + ";password =" + HJ.Base.Common.ConfigFile.ReadConfig("Pwd", strFileName) + ";persist security info = false; initial catalog=" + HJ.Base.Common.ConfigFile.ReadConfig("DataBase", strFileName) + ";Connect Timeout=30";
            return HJ.Data.SQLDBConnect.DbConnectInit(conn);
        }

    }
}

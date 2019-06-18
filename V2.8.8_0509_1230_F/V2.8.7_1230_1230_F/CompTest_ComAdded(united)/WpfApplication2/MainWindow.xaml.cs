using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Forms.ComponentModel;
//下面两个关闭取消用
using System.Runtime.InteropServices;
using System.Windows.Interop;


namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Starting : Window
    {
        public Starting()
        {
            InitializeComponent();
        }

        #region 取消关闭按钮功能20150917
        //取消左上角关闭按钮功能：第一部分20150917
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //取消左上角关闭按钮功能：第二部分20150917
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            #region
            #endregion
        }
        #endregion 取消关闭按钮功能20150917


        //int timertic = 1;
        System.Windows.Threading.DispatcherTimer timer_startsplash;

        //在这个下面加通讯初始化、读取DB、PLC等的初始化：三个功能：20150922
        private void textBlock1_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Threading.DispatcherTimer timer;
            timer_startsplash = new System.Windows.Threading.DispatcherTimer();
            timer_startsplash.Interval = new TimeSpan(0, 0, 5);
            timer_startsplash.Tick += new EventHandler(time_Tick);
            timer_startsplash.Start();

            //闪屏初始化utility变量：20150914:201590922添加通讯初始化：1
            BackPanel.UtilityMod_Header.UtilityIni();

        }

        void time_Tick(object sender, EventArgs e)
        {
            //系统正在自动选择冷却模式，请等待【】秒
            //textBlock1.Text = "系统正在准备中\n请等待...";
            //timertic--;
            //if (timertic == 0)
            //{
            //获取debug目录20150923
            GlobelVar.PathDebug = System.AppDomain.CurrentDomain.BaseDirectory;
            BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel = System.AppDomain.CurrentDomain.BaseDirectory;

            #region 读取数据库，初始化list：20150922
            BackPanel.PLCMod.BuildPLCDIDOListFromDB(BackPanel.PLCMod.PLCDIList, GlobelVar.PathDebug + "CarChiller.mdb", "PLC_DILIST");
            BackPanel.PLCMod.BuildPLCDIDOListFromDB(BackPanel.PLCMod.PLCDOList, GlobelVar.PathDebug + "CarChiller.mdb", "PLC_DOLIST");
            BackPanel.Agilent.BuildAgilentList(BackPanel.Agilent.AgilentList, GlobelVar.PathDebug + "CarChiller.mdb", "Table_Agilent");
            //过滤系数初始化
            GlobleFun.BuildFilterCoe(BackPanel.Agilent.AgilentList, GlobelVar.PathDebug + "CarChiller.mdb", "FilterCoe");


            BackPanel.Control.BuildControlerList(BackPanel.Control.Controllist, GlobelVar.PathDebug + "CarChiller.mdb", "Table_Controller");
            #endregion 读取数据库，初始化list：20150922

            #region PLC,Agilent，Control初始化
            //给PLCDOlist给加上20150922
            for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
            {

                BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
            }

            BackPanel.PLCMod.IsTestCarComp(true);

            //PLC的低压报警位寄存，置为初始值20150930
            BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDIList[5], BackPanel.PLCMod.PLCDIList[5].Initial, true);

            ////给PLCDIlist给加上20150922
            //for (int i = 0; i < BackPanel.PLCMod.PLCDIList.Count; i++)
            //{
            //    //给PLCDIlist给加上20150922
            //    BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDIList[i], BackPanel.PLCMod.PLCDIList[i].Initial, true);
            //}

            //控制器的初始化20150922 //自己只是设定了设定值，其他没有管
            for (int i = 0; i < BackPanel.Control.Controllist.Count; i++)
            {
                //把SV的值设置给控制器20150922
                BackPanel.Control.Set(BackPanel.Control.Controllist[i].StackNum, "SV", BackPanel.Control.Controllist[i].SV, Convert.ToInt32(BackPanel.Control.Controllist[i].SDP));
            }
            #endregion


            #region Agilent拼好字符串20150922
            // <param name="VDChsNumStr">电压通道选定</param>
            // <param name="ADCChsNumStr">电流通道选定</param>
            // <param name="FRTDChsNumStr">四线RTD通道选定</param>
            // <param name="TCChsNumStr">热电偶</param>
            // <param name="TCChsTypeStr">热电偶类型</param>
            //变量声明在后台Backpanel的AgilentHeader里20150922
            BackPanel.Agilent.CombineAgilentChannel();

            #endregion Agilent拼好字符串20150922

            //开始初始界面

            MainWindow newmainwind = new MainWindow();
            newmainwind.Show();
            timer_startsplash.Stop();
            this.Close();


        }









    }
}

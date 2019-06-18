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
using Utility;
using BackPanel;

using System.ComponentModel;

namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        BackgroundWorker _PLCAlert = new BackgroundWorker();

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

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


            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler(MaintTime_Tick);
            timer.Start();

            _PLCAlert.DoWork += new DoWorkEventHandler(_PLCAlert_DoWork);


            if (BackPanel.Strategy.IsTherePLC_Error == true)
            {

                this.ProjectSelect.IsEnabled = false;
            }

        }

        void _PLCAlert_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            BackPanel.PLCMod.PLCAlter(BackPanel.PLCMod.PLCDIList);
            //这个是PLC的控制策略20150922
            BackPanel.Strategy.StrategyPLCAlarm_ForCar(BackPanel.PLCMod.PLCDIList, BackPanel.PLCMod.PLCDOList);
        }
        #endregion 取消关闭按钮功能20150917

        public void MaintTime_Tick(object sender, EventArgs e)
        {
            if (!_PLCAlert.IsBusy)
            {
                _PLCAlert.RunWorkerAsync();
            }

            if (BackPanel.Strategy.IsTherePLC_Error == true)
            {
                ProjectSelect.IsEnabled = false;

                //MessageBoxResult mbr = MessageBox.Show("请确认设备关闭,故障排除后点击确认!", "报警", MessageBoxButton.OK);
                //if (mbr == MessageBoxResult.OK)
                if (true)
                {
                    BackPanel.Strategy.IsTherePLC_Error = false;
                    //PLC低压寄存器复位！强制的20150928改:20150930
                    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[5], false, true);
                    //量热器低液位寄存器复位！
                    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[2], false, true);
                }

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            //20150923:20150925转移到Ending闪屏
            //BackPanel.Strategy.StrategyPLCExit();
            //20150925退出，先进入退出闪屏
            EndingSplash newendingsplash = new EndingSplash();
            newendingsplash.Show();

            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //20151104
            //BackPanel.PLCMod.CommonStartEquip(true);
            //BackPanel.PLCMod.CarStartEquip(true);

            CompressorSelected compressorselected = new CompressorSelected();
            compressorselected.ShowDialog();

            this.Close();
            
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            CarAirConditionInfo carairconditioninfo = new CarAirConditionInfo();
            carairconditioninfo.ShowDialog();

            this.Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //////20151104
            ////BackPanel.PLCMod.CommonStartEquip(true);
            ////BackPanel.PLCMod.ChillerStartEquip(true);

            ChillerInfo chillerinfo = new ChillerInfo();
            chillerinfo.ShowDialog();


            this.Close();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            AirCooledChillerInfo aircooledchillerinfo = new AirCooledChillerInfo();
            aircooledchillerinfo.ShowDialog();

            this.Close();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            EvaporatorInfo evaporatorinfo = new EvaporatorInfo();
            evaporatorinfo.ShowDialog();

            this.Close();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            CondenserInfo condenserinfo = new CondenserInfo();
            condenserinfo.ShowDialog();

            this.Close();
        }


    }
}

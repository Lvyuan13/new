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
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;

using BackPanel;
using System.ComponentModel;

namespace WpfApplication2
{
    /// <summary>
    /// Car.xaml 的交互逻辑
    /// </summary>
    public partial class CarAirCondition : Window
    {
        public CarAirCondition()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 汽车空调压缩机制冷试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void CoolingTrial_Click(object sender, RoutedEventArgs e)
        //{
        //    //CarCooling carcooling = new CarCooling();
        //    //carcooling.textBlock47.Visibility = Visibility.Hidden;
        //    //carcooling.textBox31.Visibility = Visibility.Hidden;
        //    //carcooling.textBlock48.Visibility = Visibility.Hidden;

        //    //场景
        //    //GlobelVar.senario = GlobelVar.Senario.CarCooling;
        //    //string SenairoStr = GlobelVar.senario.ToString();
        //    //替代20150917
        //    BackPanel.InformationGlo.senario = BackPanel.InformationGlo.Senario.CarCooling;
        //    string SenairoStr = BackPanel.InformationGlo.senario.ToString();

        //    //报表专用场景20151220
        //    Report.SenarioForReport.senario_ForReport = Report.SenarioForReport.Senario_ForReport.CarCooling;

        //    //开始闪屏
        //    StartingSplash startingsplash_CoolingTrial = new StartingSplash();
        //    startingsplash_CoolingTrial.Show();

        //    Report.ReportParameterMySelf.RP1CarExpType = "汽车压缩机制冷性能试验";

        //    //carcooling.Show();
        //    this.Close();
        //}

        //private void NoiseTrial_Click(object sender, RoutedEventArgs e)
        //{
        //    //CarCooling carnoise = new CarCooling();
        //    //carnoise.Title = "汽车空调压缩机试验：噪声试验";
        //    //carnoise.textBlock9.Visibility = Visibility.Hidden;
        //    //carnoise.textBox8.Visibility = Visibility.Hidden;
        //    //carnoise.textBox16.Visibility = Visibility.Hidden;
        //    //carnoise.textBlock15.Visibility = Visibility.Hidden;

        //    //场景
        //    //GlobelVar.senario = GlobelVar.Senario.CarNoise;
        //    //string SenairoStr = GlobelVar.senario.ToString();
        //    //场景替代20150917
        //    BackPanel.InformationGlo.senario = BackPanel.InformationGlo.Senario.CarNoise;
        //    string SenairoStr = BackPanel.InformationGlo.senario.ToString();

        //    //报表专用20151220
        //    Report.SenarioForReport.senario_ForReport = Report.SenarioForReport.Senario_ForReport.CarNoise;

        //    //开始闪屏
        //    StartingSplash startingsplash_NoiseTrial = new StartingSplash();
        //    startingsplash_NoiseTrial.Show();

        //    //报表参数20151219
        //    Report.ReportParameterMySelf.RP1CarExpType = "汽车空调压缩机噪声性能试验原始记录表";

        //    //carnoise.Show();
        //    this.Close();
        //}

        private void ReturnMainWindow_Click(object sender, RoutedEventArgs e)
        {
            #region 关闭辅助设备20150923
            BackPanel.Strategy.StrategyPLCAuxiliaryStop();
            #endregion 关闭辅助设备20150923

            MainWindow newmainwindow = new MainWindow();

            newmainwindow.Show();

            //PLCMod.CloseALLEquipment();
            this.Close();

        }
        BackgroundWorker _PLCAlert = new BackgroundWorker();
        public System.Windows.Threading.DispatcherTimer timer;

        #region 取消关闭按钮功能20150917
        //此处为定义
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


            #region 控制策略：如果从主界面返回，则“返回主菜单”隐藏，变为“退出试验”：20150923
            if (BackPanel.Strategy.IsReturnFromMain)
            {
                ReturnMainWindow.Visibility = Visibility.Collapsed;
                ExitExperiment.Visibility = Visibility.Visible;
            }
            else
            {
                ReturnMainWindow.Visibility = Visibility.Visible;
                ExitExperiment.Visibility = Visibility.Collapsed;
            }
            #endregion

            #region 控制策略开启辅助设备：20150923
            BackPanel.Strategy.StrategyPLCAuxiliaryStart();
            #endregion 控制策略开启辅助设备：20150923
            //如报警退出，则只能退出20151125
            //if(BackPanel.InformationGlo.IsAlertingReset)
            //{
            //    this.TestProject.IsEnabled = false;
            //}

            //if(BackPanel.Strategy.IsTherePLC_Error==true)
            //{
            //    this.TestProject.IsEnabled = false;

            //}

            if (BackPanel.InformationGlo.BackFormMainBecauseOfError == true)
            {
                this.TestProject.IsEnabled = false;
                BackPanel.InformationGlo.BackFormMainBecauseOfError = false;
            }
            _PLCAlert.DoWork += new DoWorkEventHandler(_PLCAlert_DoWork);

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler(MaintTime_Tick);
            timer.Start();


        }

        void _PLCAlert_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            BackPanel.PLCMod.PLCAlter(BackPanel.PLCMod.PLCDIList);
            //这个是PLC的控制策略20150922
            BackPanel.Strategy.StrategyPLCAlarm_ForCar(BackPanel.PLCMod.PLCDIList, BackPanel.PLCMod.PLCDOList);
        }
        //主钟函数20151125
        //世上本没有主钟，说的人多了，也就成了主钟  ——鲁迅（定时器而已，没有主钟这个概念）
        public void MaintTime_Tick(object sender, EventArgs e)
        {
            if (!_PLCAlert.IsBusy)
            {
                _PLCAlert.RunWorkerAsync();
            }

            if (BackPanel.Strategy.IsTherePLC_Error == true)
            {
                TestProject.IsEnabled = false;

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


        #endregion 取消关闭按钮功能20150917

        /// <summary>
        /// 从Car中退出实验20150923
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitExperiment_Click(object sender, RoutedEventArgs e)
        {
            #region 关闭辅助设备20150923
            BackPanel.Strategy.StrategyPLCAuxiliaryStop();
            #endregion 关闭辅助设备20150923

            //#region 退出试验的控制逻辑：20150923
            //BackPanel.Strategy.StrategyPLCExit();
            //#endregion 退出试验的控制逻辑：20150923
            //20150925转移到ending闪屏里
            EndingSplash newendsplashing = new EndingSplash();
            newendsplashing.Show();

            //PLCMod.CloseALLEquipment();

            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            timer.Stop();
        }

        //private void CoolingTrialReport_Click(object sender, RoutedEventArgs e)
        //{
        //    //报表专用场景20151220
        //    Report.SenarioForReport.senario_ForReport = Report.SenarioForReport.Senario_ForReport.CarCooling;
        //    Report.Infomation.IsPreview = true;

        //    Report.MainWindow reportmainwindow = new Report.MainWindow();
        //    reportmainwindow.Show();
        //}

        //private void NoiseTrialReport_Click(object sender, RoutedEventArgs e)
        //{
        //    //报表专用场景20151220
        //    Report.SenarioForReport.senario_ForReport = Report.SenarioForReport.Senario_ForReport.CarNoise;
        //    Report.Infomation.IsPreview = true;

        //    Report.MainWindow reportmainwindow = new Report.MainWindow();
        //    reportmainwindow.Show();
        //}

        private void TestProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OperationTrial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MaximumLoadTrial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CondensationTrial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LowTemperatureTrial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OperationTrialReport_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

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

using WpfApplication2;
using System.Windows.Interop;
using System.Runtime.InteropServices;

using BackPanel;

using System.ComponentModel;

using Report;

namespace WpfApplication2
{
    /// <summary>
    /// Chiller.xaml 的交互逻辑
    /// </summary>
    public partial class Evaporator : Window
    {
        public Evaporator()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 水冷压缩机组试验：名义工况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NominalCondition_Click(object sender, RoutedEventArgs e)
        {
            //ChillerTrialcommon ChillerNominalCondition = new ChillerTrialcommon();
            //if (WpfApplication2.GlobelVar.IsControlWaterTemperature == true)
            //{
            //    //场景
            //    //GlobelVar.senario = GlobelVar.Senario.ChillerNormialConditionTemp;
            //    //string SenairoStr = GlobelVar.senario.ToString();
            //    //场景替代20150917
            //    BackPanel.InformationGlo.senario = BackPanel.InformationGlo.Senario.ChillerNormialConditionTemp;
            //    string SenairoStr = BackPanel.InformationGlo.senario.ToString();

            //    //ChillerNominalCondition.textBlock4.Text = "出水温度";
            //    //ChillerNominalCondition.textBlock9.Text = "℃";
            //}
            //else
            //{
            //    //场景
            //    //GlobelVar.senario = GlobelVar.Senario.ChillerNormialConditionWaterFlow;
            //    //string SenairoStr = GlobelVar.senario.ToString();
            //    //场景替代20150917
            //    BackPanel.InformationGlo.senario = BackPanel.InformationGlo.Senario.ChillerNormialConditionWaterFlow;
            //    string SenairoStr = BackPanel.InformationGlo.senario.ToString();

            //    //ChillerNominalCondition.textBlock4.Text = "冷却水流量";
            //    //ChillerNominalCondition.textBlock9.Text = "m3/h";

            //}
            //控制可场景分开，场景是四个；两种控制个4个
            BackPanel.InformationGlo.senario = BackPanel.InformationGlo.Senario.ChillerNormialCondition;
            string SenairoStr = BackPanel.InformationGlo.senario.ToString();

            //BackPanel.Control.ControlInitiate_ForChiller();
            //报表20151220
            SenarioForReport.senario_ForReport = SenarioForReport.Senario_ForReport.ChillerNormialCondition;


            ReportParameterMySelf_ForChiller.RP1ChillerExpType = "冷水机组名义工况性能试验原始记录表";
            
            //ReportParameterMySelf_ForChiller.RP20PartialLoad = "";
            //ReportParameterMySelf_ForChiller.RP21PartialLoadName = "";



            //开始闪屏  20150911
            StartingSplash startingsplash_Chiller = new StartingSplash();
            startingsplash_Chiller.Show();

            //ChillerNominalCondition.Show();
            this.Close();


        }
       /// <summary>
       /// 部分负荷
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void PartialCondition_Click(object sender, RoutedEventArgs e)
        {

            //部分负荷选择窗口
            PartialLoadSelected partloadWindow = new PartialLoadSelected();
            partloadWindow.ShowDialog();

            this.Close();



        }
        /// <summary>
        /// 最大负荷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxCondition_Click(object sender, RoutedEventArgs e)
        {

            //场景替换20150917
            BackPanel.InformationGlo.senario = BackPanel.InformationGlo.Senario.ChillerMaxCondition;
            string SenairoStr = BackPanel.InformationGlo.senario.ToString();

            ////进水温度
            //BackPanel.Control.Set(5, "SV", 33, 2);
            ////Thread.Sleep(sleeptime);

            //报表20151220
            SenarioForReport.senario_ForReport = SenarioForReport.Senario_ForReport.ChillerMaxCondition;

            ReportParameterMySelf_ForChiller.RP1ChillerExpType = "冷水机组最大运行工况性能试验原始记录表";

            //ReportParameterMySelf_ForChiller.RP20PartialLoad = "";
            //ReportParameterMySelf_ForChiller.RP21PartialLoadName = "";

            //开始闪屏
            StartingSplash startingsplash_Chiller = new StartingSplash();
            startingsplash_Chiller.Show();

            this.Close();



        }

        //水冷压缩机组试验：变工况试验
        private void ChangingCondition_Click(object sender, RoutedEventArgs e)
        {
            //场景
            //GlobelVar.senario = GlobelVar.Senario.ChillerChangCondition;
            //string SenairoStr = GlobelVar.senario.ToString();
            //场景替换20150917
            BackPanel.InformationGlo.senario = BackPanel.InformationGlo.Senario.ChillerChangCondition;
            string SenairoStr = BackPanel.InformationGlo.senario.ToString();

            //报表20151220
            SenarioForReport.senario_ForReport = SenarioForReport.Senario_ForReport.ChillerChangCondition;

            ReportParameterMySelf_ForChiller.RP1ChillerExpType = "冷水机组变工况性能试验原始记录表";

            //ReportParameterMySelf_ForChiller.RP20PartialLoad = "";
            //ReportParameterMySelf_ForChiller.RP21PartialLoadName = "";

            //开始闪屏
            StartingSplash startingsplash_Chiller = new StartingSplash();
            startingsplash_Chiller.Show();

            this.Close();

        }


        //返回主界面
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            #region 关闭辅助设备20150923
            BackPanel.Strategy.StrategyPLCAuxiliaryStop();
            #endregion 关闭辅助设备20150923

            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();

            //PLCMod.CloseALLEquipment();

            this.Close();


        }

        BackgroundWorker _PLCAlert = new BackgroundWorker();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();


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

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler(MaintTime_Tick);
            timer.Start();

            _PLCAlert.DoWork += new DoWorkEventHandler(_PLCAlert_DoWork);
            //if (BackPanel.Strategy.IsTherePLC_Error == true)
            //{
            //    this.ProjectSelect.IsEnabled = false;
            //}

            if (BackPanel.InformationGlo.BackFormMainBecauseOfError)
            {
                TestProj.IsEnabled = false;
            }
        }

        public void MaintTime_Tick(object sender, EventArgs e)
        {
            if (!_PLCAlert.IsBusy)
            {
                _PLCAlert.RunWorkerAsync();
            }

            if(BackPanel.Strategy.IsTherePLC_Error)
            {
                //this.CatchRecord.IsEnabled = false;
                //this.button2.IsEnabled = false;
                this.ReportMenu.IsEnabled = false;
                this.TestProj.IsEnabled = false;
                //MessageBox.Show("设备出现问题，请检查无误后退出试验！重新开启！");
                //MessageBoxResult mbr = MessageBox.Show("设备出现问题，请检查无误后退出试验！重新开启！", "报警", MessageBoxButton.OK);
                //if (mbr == MessageBoxResult.OK)
                if (true)
                {
                    BackPanel.Strategy.IsTherePLC_Error = false;
                    //初始化PLC报警
                    BackPanel.Strategy.StrategyPLCReset();
                }
            }
        }



        void _PLCAlert_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            BackPanel.PLCMod.PLCAlter(BackPanel.PLCMod.PLCDIList);
            //这a个?是º?PLC的Ì?控?制?策?略?20150922
            BackPanel.Strategy.StrategyPLCAlarm_ForChiller(BackPanel.PLCMod.PLCDIList, BackPanel.PLCMod.PLCDOList);
        }


        #region 取消关闭按钮功能20150917
        //取消左上角关闭按钮功能：第一部分20150917
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        #endregion 取消关闭按钮功能20150917

        private void ExitExperiment_Click(object sender, RoutedEventArgs e)
        {
            #region 关闭辅助设备20150923
            BackPanel.Strategy.StrategyPLCAuxiliaryStop();
            #endregion 关闭辅助设备20150923



            //#region 退出试验的控制逻辑：20150923
            //BackPanel.Strategy.StrategyPLCExit();
            //#endregion 退出试验的控制逻辑：20150923

            EndingSplash newendingsplash = new EndingSplash();
            newendingsplash.Show();

            //PLCMod.CloseALLEquipment();

            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            timer.Stop();
        }

        private void NominalConditionReport_Click(object sender, RoutedEventArgs e)
        {
            Report.SenarioForReport.senario_ForReport = Report.SenarioForReport.Senario_ForReport.ChillerNormialCondition;
            Report.Infomation.IsPreview = true;

            Report.MainWindow reportmainwindow = new Report.MainWindow();
            reportmainwindow.Show();
        }

        private void PartialConditionReport_Click(object sender, RoutedEventArgs e)
        {
            Report.SenarioForReport.senario_ForReport = Report.SenarioForReport.Senario_ForReport.ChillerPartialCondition;
            Report.Infomation.IsPreview = true;

            Report.MainWindow reportmainwindow = new Report.MainWindow();
            reportmainwindow.Show();
        }

        private void MaxConditionReport_Click(object sender, RoutedEventArgs e)
        {
            Report.SenarioForReport.senario_ForReport = Report.SenarioForReport.Senario_ForReport.ChillerMaxCondition;
            Report.Infomation.IsPreview = true;

            Report.MainWindow reportmainwindow = new Report.MainWindow();
            reportmainwindow.Show();
        }

        private void ChangingConditionReport_Click(object sender, RoutedEventArgs e)
        {
            Report.SenarioForReport.senario_ForReport = Report.SenarioForReport.Senario_ForReport.ChillerChangCondition;
            Report.Infomation.IsPreview = true;

            Report.MainWindow reportmainwindow = new Report.MainWindow();
            reportmainwindow.Show();
        }
    }
}

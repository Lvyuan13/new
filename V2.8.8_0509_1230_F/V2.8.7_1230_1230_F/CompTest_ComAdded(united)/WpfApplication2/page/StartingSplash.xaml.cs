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
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace WpfApplication2
{
    /// <summary>
    /// StartingSplash.xaml 的交互逻辑
    /// </summary>
    public partial class StartingSplash : Window
    {
        public StartingSplash()
        {
            InitializeComponent();
            ////闪屏初始化utility变量：20150914
            //BackPanel.UtilityMod_Header.UtilityIni();
           
        }
        int i = 1;
        System.Windows.Threading.DispatcherTimer timer_startsplash;
        private void textBlock1_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Threading.DispatcherTimer timer;
            timer_startsplash = new System.Windows.Threading.DispatcherTimer();
            timer_startsplash.Interval = new TimeSpan(0, 0, 1);
            timer_startsplash.Tick += new EventHandler(time_Tick);
            timer_startsplash.Start();
        }

        void time_Tick(object sender, EventArgs e)
        {
            //系统正在自动选择冷却模式，请等待【】秒
            textBlock1.Text = "系统正在准备中\n请等待【" + i + "】秒";
            i--;
            if (i == 0)
            {
                //MainWindow mainwindow = new MainWindow();
                //mainwindow.Show();
                switch (BackPanel.InformationGlo.senario)
                {
                    //汽车空调制冷压缩机制冷

                    case BackPanel.InformationGlo.Senario.CarCooling:
                        CarCooling carcooling = new CarCooling();
                        //carcooling.textBlock47.Visibility = Visibility.Hidden;
                        //carcooling.textBox31.Visibility = Visibility.Hidden;
                        //carcooling.textBlock48.Visibility = Visibility.Hidden;
                        carcooling.Show();
                        break;

                    //汽车空调压缩机噪声试验
                    case BackPanel.InformationGlo.Senario.CarNoise:
                        CarCooling carnoise = new CarCooling();
                        carnoise.Title = "汽车空调压缩机试验：噪声试验";
                        carnoise.textBlock9.Visibility = Visibility.Hidden;
                        carnoise.textBox8.Visibility = Visibility.Hidden;
                        carnoise.textBox16.Visibility = Visibility.Hidden;
                        carnoise.textBlock15.Visibility = Visibility.Hidden;
                        carnoise.Show();
                        break;


                    //水冷压缩机组试验：名义工况，出水温度
                    case BackPanel.InformationGlo.Senario.ChillerNormialCondition:
                        ChillerTrialcommon ChillerNominalConditionTemp = new ChillerTrialcommon();
                        switch(BackPanel.InformationGlo.senariocontrol)
                        {
                            case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:
                                ChillerNominalConditionTemp.Title = "水冷压缩冷凝机组试验：名义工况试验(控制出水温度)";
                                break;
                            case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                                ChillerNominalConditionTemp.Title = "水冷压缩冷凝机组试验：名义工况试验(控制水流量)";
                                break;
                        }
                        BackPanel.Control.ControlInitiate_ForChiller();
                        GlobelVar.ChillerInWaterTempSetOnlyStable = 30;
                        ChillerNominalConditionTemp.Show();
                        break;
                   
                    //水冷压缩机组试验：部分负荷试验
                    case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
                        ChillerTrialcommon ChillerPartialCondition = new ChillerTrialcommon();
                        switch (BackPanel.InformationGlo.senariocontrol)
                        {
                            case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:
                                ChillerPartialCondition.Title = "水冷压缩冷凝机组试验：部分负荷试验(控制出水温度)";
                                break;
                            case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                                ChillerPartialCondition.Title = "水冷压缩冷凝机组试验：部分负荷试验(控制水流量)";
                                break;
                        }
                        BackPanel.Control.ControlInitiate_ForChiller();
                        GlobelVar.ChillerInWaterTempSetOnlyStable = 30;
                        ChillerPartialCondition.Show();
                        break;
                    //水冷压缩机组试验：最大负荷试验
                    case BackPanel.InformationGlo.Senario.ChillerMaxCondition:
                        ChillerTrialcommon ChillerMaxCondition = new ChillerTrialcommon();
                        switch (BackPanel.InformationGlo.senariocontrol)
                        {
                            case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:
                                ChillerMaxCondition.Title = "水冷压缩冷凝机组试验：最大负荷试验(控制出水温度)";
                                break;
                            case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                                ChillerMaxCondition.Title = "水冷压缩冷凝机组试验：最大负荷试验(控制水流量)";
                                break;
                        }

                        BackPanel.Control.ControlInitiate_ForChiller();
                        //进水温度
                        BackPanel.Control.Set(5, "SV", 33, 2);
                        //Thread.Sleep(sleeptime);
                        ChillerMaxCondition.textBox4.Text = "33.00";

                        GlobelVar.ChillerInWaterTempSetOnlyStable = 33;
                        //ChillerMaxCondition.Title = "水冷压缩冷凝机组试验：最大负荷试验";
                        ChillerMaxCondition.Show();
                        break;
                    //水冷压缩机组试验：变工况试验
                    case BackPanel.InformationGlo.Senario.ChillerChangCondition:
                        ChillerTrialcommon ChillerChangingCondition = new ChillerTrialcommon();
                        switch (BackPanel.InformationGlo.senariocontrol)
                        {
                            case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:
                                ChillerChangingCondition.Title = "水冷压缩冷凝机组试验：变工况试验(控制出水温度)";
                                break;
                            case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                                ChillerChangingCondition.Title = "水冷压缩冷凝机组试验：变工况试验(控制水流量)";
                                break;
                        }
                        BackPanel.Control.ControlInitiate_ForChiller();
                        GlobelVar.ChillerInWaterTempSetOnlyStable = 30;
                        //ChillerChangingCondition.Title = "水冷压缩冷凝机组试验：变工况试验";
                        ChillerChangingCondition.Show();
                        break;
                }



                timer_startsplash.Stop();
                //this.Close();
                this.Close();


            }

            //初始化闪屏
            GlobelVar.GlobelVarIni();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            //新添加20150923PLC
            BackPanel.Strategy.StrategyPLCBeforeMain();
            //新添加20150923UT35A:改20150929，改成下面的，通过control场景完成
            //BackPanel.Strategy.StrategyUT35ABeforeMain();

            //变成了下面这个20150929
            //BackPanel.Control.BuildControlerList(BackPanel.Control.Controllist, GlobelVar.PathDebug + "CarChiller.mdb", "Table_Controller");

            ////控制器的初始化20150922
            //for (int i = 0; i < BackPanel.Control.Controllist.Count; i++)
            //{
            //    //把SV的值设置给控制器20150922
            //    BackPanel.Control.Set(BackPanel.Control.Controllist[i].StackNum, "SV", BackPanel.Control.Controllist[i].SV, Convert.ToInt32(BackPanel.Control.Controllist[i].SDP));
            //}

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
    }
}

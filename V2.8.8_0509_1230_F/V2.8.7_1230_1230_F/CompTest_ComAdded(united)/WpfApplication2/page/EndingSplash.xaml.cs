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

using BackPanel;

namespace WpfApplication2
{
    /// <summary>
    /// EndingSplash.xaml 的交互逻辑
    /// </summary>
    public partial class EndingSplash : Window
    {
        int i = 5;
        System.Windows.Threading.DispatcherTimer timer_endingsplash;
        public EndingSplash()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer_endingsplash = new System.Windows.Threading.DispatcherTimer();
            timer_endingsplash.Interval = new TimeSpan(0, 0, 1);
            timer_endingsplash.Tick += new EventHandler(time_Tick);
            timer_endingsplash.Start();

            

            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
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
        void time_Tick(object sender, EventArgs e)
        {
            //系统正在自动选择冷却模式，请等待【】秒
            textBlock2.Text =i.ToString();
            i--;
            if (i == 0)
            {
                //MainWindow mainwindow = new MainWindow();
                //mainwindow.Show();
                this.Close();

                timer_endingsplash.Stop();

            }
            ////初始化闪屏
            //GlobelVar.GlobelVarIni();

        }

        private void textBlock1_Loaded(object sender, RoutedEventArgs e)
        {
            //#region 退出试验的控制逻辑：20150923:20150925改动
            BackPanel.Strategy.StrategyPLCExit();
            //#endregion 退出试验的控制逻辑：20150923
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           PLCMod.CloseALLEquipment();
        }
    }
}

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

using Report;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace WpfApplication2
{
    /// <summary>
    /// PartialLoadSelected.xaml 的交互逻辑
    /// </summary>
    public partial class CompressorSelected : Window
    {
        public CompressorSelected()
        {
            InitializeComponent();
        }
 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

        }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void rbOpenConpresser_Checked(object sender, RoutedEventArgs e)
        {
            OpenCarInfo opencarinfo = new OpenCarInfo();
            opencarinfo.ShowDialog();
            this.Close();
        }

        private void rbMotorConpresser_Checked(object sender, RoutedEventArgs e)
        {
            MotorCarInfo motorcarinfo = new MotorCarInfo();
            motorcarinfo.ShowDialog();
            this.Close();
        }  
   
        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.ShowDialog();
            this.Close();
        }

         private void btConfirm_Click(object sender, RoutedEventArgs e)
        {

            BackPanel.InformationGlo.senario = BackPanel.InformationGlo.Senario.ChillerPartialCondition;
            string SenairoStr = BackPanel.InformationGlo.senario.ToString();

            //报表20151220
            SenarioForReport.senario_ForReport = SenarioForReport.Senario_ForReport.ChillerPartialCondition;

            ReportParameterMySelf_ForChiller.RP1ChillerExpType = "冷水机组部分负荷性能试验原始记录表";
            //ChillerTrialcommon ChillerPartialCondition = new ChillerTrialcommon();
            //ChillerPartialCondition.Title = "水冷压缩冷凝机组试验：部分负荷试验";
            //ChillerPartialCondition.Show();
            //开始闪屏
            StartingSplash startingsplash_Chiller = new StartingSplash();
            startingsplash_Chiller.Show();

            //BackPanel.Control.ControlInitiate_ForChiller();

            this.Close();
        }



    }
}

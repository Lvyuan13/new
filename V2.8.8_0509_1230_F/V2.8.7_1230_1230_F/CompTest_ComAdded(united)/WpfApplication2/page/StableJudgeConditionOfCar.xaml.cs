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
    /// StableJudgeConditionOfCar.xaml 的交互逻辑
    /// </summary>
    public partial class StableJudgeConditionOfCar : Window
    {
        public StableJudgeConditionOfCar()
        {
            InitializeComponent();

            //LoadCondition();
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveTable();
                this.Close();
            }
            catch
            {
                MessageBox.Show("请输入正确的数值格式！");
                return;
            }

            
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveTable()
        {
            GlobelVar.CarDischargePressureRequire_Dif = double.Parse(txt_1.Text);

            GlobelVar.CarSuctionPressureRequire_Dif = double.Parse(txt_2.Text);

            GlobelVar.CarSuctionTemperatureRequire_Dif = double.Parse(txt_3.Text);

            GlobelVar.CarRotateRequire_Dif = double.Parse(txt_4.Text);

            GlobelVar.CarAGcoolingCapacity_Dif = double.Parse(txt_5.Text);
            //GlobelVar.CarCoolingWaterRequire_Dif = double.Parse(txt_5.Text);
        }

        private void LoadCondition()
        {
            //GlobelVar.CarDischargeTemperature = double.Parse(txt_1.Text);

            //GlobelVar.CarInputSaturateTemperature = double.Parse(txt_2.Text);

            //GlobelVar.CarInputTemperature = double.Parse(txt_3.Text);

            //GlobelVar.CarCompressorRotate = double.Parse(txt_4.Text);

            //GlobelVar.CarCoolingWater = double.Parse(txt_5.Text);
            txt_1.Text = GlobelVar.CarDischargePressureRequire_Dif.ToString();
            txt_2.Text = GlobelVar.CarSuctionPressureRequire_Dif.ToString();
            txt_3.Text = GlobelVar.CarSuctionTemperatureRequire_Dif.ToString();
            txt_4.Text = GlobelVar.CarRotateRequire_Dif.ToString();

            txt_5.Text = GlobelVar.CarAGcoolingCapacity_Dif.ToString();
            //txt_5.Text = GlobelVar.CarCoolingWaterRequire_Dif.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            LoadCondition();
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

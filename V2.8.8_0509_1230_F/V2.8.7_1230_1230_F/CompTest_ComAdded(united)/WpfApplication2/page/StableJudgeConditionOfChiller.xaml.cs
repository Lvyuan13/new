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
    /// StableJudgeConditionOfChiller.xaml 的交互逻辑
    /// </summary>
    public partial class StableJudgeConditionOfChiller : Window
    {
        public StableJudgeConditionOfChiller()
        {
            InitializeComponent();

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
            this.Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadCondition()
        {
            txt_1.Text = GlobelVar.ChillerEvaperator_PressRequire_Dif.ToString();
            txt_2.Text = GlobelVar.ChillerSuction_TemperatureRequire_Dif.ToString();
            txt_3.Text = GlobelVar.ChillerInputWater_TemperatureRequire_Dif.ToString();

            txt_4waterflow.Text = GlobelVar.ChillerCoolingWater_FlowRateRequire_Percent_Dif.ToString();
            txt_5waterTemperature.Text = GlobelVar.ChillerOutputWater_TemperatureRequire_Dif.ToString();

            if (BackPanel.InformationGlo.senariocontrol == BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp)
            {
                txt_4waterflow.IsEnabled = false;
                txt_5waterTemperature.IsEnabled = true;
            }
            else
            {
                txt_5waterTemperature.IsEnabled = false;
                txt_4waterflow.IsEnabled = true;
            }
        }

        private void SaveTable()
        {
            //GlobelVar.ChillerEvaperator_TemperatureRequire_Dif = double.Parse(txt_1.Text);
            //GlobelVar.ChillerInputWater_TemperatureRequire_Dif = double.Parse(txt_2.Text);
            //GlobelVar.ChillerCoolingWater_FlowRateRequire_Dif = double.Parse(txt_3waterflow.Text);
            //GlobelVar.ChillerOutputWater_TemperatureRequire_Dif = double.Parse(txt_4waterTemperature.Text);

            GlobelVar.ChillerEvaperator_PressRequire_Dif = double.Parse(txt_1.Text);
            GlobelVar.ChillerSuction_TemperatureRequire_Dif = double.Parse(txt_2.Text);
            GlobelVar.ChillerInputWater_TemperatureRequire_Dif = double.Parse(txt_3.Text);

            GlobelVar.ChillerCoolingWater_FlowRateRequire_Percent_Dif = double.Parse(txt_4waterflow.Text);
            GlobelVar.ChillerOutputWater_TemperatureRequire_Dif = double.Parse(txt_5waterTemperature.Text);

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

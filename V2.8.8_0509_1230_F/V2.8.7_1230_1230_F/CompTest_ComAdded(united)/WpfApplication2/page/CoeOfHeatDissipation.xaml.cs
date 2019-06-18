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

namespace WpfApplication2
{
    /// <summary>
    /// CoeOfHeatDissipation.xaml 的交互逻辑
    /// </summary>
    public partial class CoeOfHeatDissipation : Window
    {
        public CoeOfHeatDissipation()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveCoeOfHeatDissipationg();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void SaveCoeOfHeatDissipationg()
        {
            double temp1 = 0, temp2 = 0,temp3=0;

            if (double.TryParse(txt_1.Text, out temp1))
            {
                if (temp1 < 0)
                {
                    txt_1.Focus();
                    //throw new Exception("\"总取样时间\"不能小于“1”分钟！");
                    throw new Exception("漏热系数不能小于0");
                }
            }
            else
            {
                //txt_1.Focus();
                //throw new Exception("\"总取样时间\"输入格式错误，请输入正确的数值格式！");
                txt_1.Focus();
                throw new Exception("\"漏热系数\"输入格式错误，请输入正确的数值格式！");

            }

            if (double.TryParse(txt_2.Text, out temp2))
            {
                if (temp2 < 0)
                {
                    txt_2.Focus();
                    //throw new Exception("\"分段取样时间\"不能小于“1”分钟！");
                    throw new Exception("漏热系数不能小于0");
                }
            }
            else
            {
                //txt_1.Focus();
                //throw new Exception("\"总取样时间\"输入格式错误，请输入正确的数值格式！");
                txt_2.Focus();
                throw new Exception("\"漏热系数\"输入格式错误，请输入正确的数值格式！");

            }

            if (double.TryParse(txt_3.Text, out temp3))
            {
                if (temp3 < 0)
                {
                    txt_3.Focus();
                    //throw new Exception("\"分段取样时间\"不能小于“1”分钟！");
                    throw new Exception("漏热系数不能小于0");
                }
            }
            else
            {
                //txt_1.Focus();
                //throw new Exception("\"总取样时间\"输入格式错误，请输入正确的数值格式！");
                txt_3.Focus();
                throw new Exception("\"漏热系数\"输入格式错误，请输入正确的数值格式！");

            }

            BackPanel.Calculate.CalculateCar.A_HeatDissipCoe = temp1;
            BackPanel.Calculate.CalculateCar.G_HeatDissipCoe = temp2;
            BackPanel.Calculate.CalculateChiller.HeatDissipCoe = temp3;

        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_1.Text= BackPanel.Calculate.CalculateCar.A_HeatDissipCoe.ToString() ;
            txt_2.Text= BackPanel.Calculate.CalculateCar.G_HeatDissipCoe.ToString();
            txt_3.Text= BackPanel.Calculate.CalculateChiller.HeatDissipCoe.ToString();

            //var hwnd = new WindowInteropHelper(this).Handle;
            //SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

        }

    }
}

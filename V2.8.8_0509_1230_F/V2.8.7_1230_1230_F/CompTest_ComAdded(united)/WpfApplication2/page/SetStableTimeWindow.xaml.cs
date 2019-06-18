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
    /// SetStableTimeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SetStableTimeWindow : Window
    {
        public SetStableTimeWindow()
        {
            InitializeComponent();

           

        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveTime();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 加载原始值函数
        /// </summary>
        void LoadTime()
        {

            txt_1.Text = GlobelVar.RecordTotalTime.ToString();
            txt_2.Text = GlobelVar.RecordSpan.ToString();
        }
        /// <summary>
        /// 保存时间
        /// </summary>
        void SaveTime()
        {
            double temp1 = 10, temp2 = 2;

            if (double.TryParse(txt_1.Text, out temp1))
            {
                if (temp1 < 1)
                {
                    txt_1.Focus();
                    throw new Exception("\"总取样时间\"不能小于“1”分钟！");
                }
            }
            else
            {
                //txt_1.Focus();
                //throw new Exception("\"总取样时间\"输入格式错误，请输入正确的数值格式！");
                txt_1.Focus();
                throw new Exception("\"总取样时间\"输入格式错误，请输入正确的数值格式！");

            }

            if (double.TryParse(txt_2.Text, out temp2))
            {
                if (temp2 < 1)
                {
                    txt_2.Focus();
                    throw new Exception("\"分段取样时间\"不能小于“1”分钟！");
                }
            }
            else
            {
                //txt_1.Focus();
                //throw new Exception("\"总取样时间\"输入格式错误，请输入正确的数值格式！");
                txt_2.Focus();
                throw new Exception("\"分段取样时间\"输入格式错误，请输入正确的数值格式！");

            }

            GlobelVar.RecordTotalTime = temp1;
            GlobelVar.RecordSpan = temp2;

            #region 换算成秒：以及得出次数：20150917

            GlobelVar.RecordSpanSec = Convert.ToInt32(temp2*60);
            GlobelVar.RecordNum = Convert.ToInt32(temp1/temp2);

            #endregion 换算成秒：以及得出次数：20150917

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTime();

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
    }
}

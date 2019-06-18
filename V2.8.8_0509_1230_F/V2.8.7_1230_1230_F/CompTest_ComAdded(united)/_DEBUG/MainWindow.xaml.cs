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
using BackPanel;
using Utility;

namespace _DEBUG
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

        private void btM02_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM00_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM01_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM03_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM04_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM05_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM06_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM07_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM10_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM11_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM12_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM13_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM14_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM15_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM16_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM17_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM20_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM21_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM22_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM23_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM24_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM25_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM26_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM27_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM30_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM31_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM32_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM33_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM34_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM35_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM36_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM37_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM40_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM41_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM42_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM43_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM44_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM45_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM46_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM47_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM50_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM51_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM52_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM53_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM54_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM55_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM56_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void btM57_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }


        #region 这段核心应该注意:添加20150914
        /// <summary>
        /// 改变值函数，红关，绿开；
        /// </summary>
        /// <param name="bttemp"></param>
        private void ChangState(Button bttemp)
        {
            SolidColorBrush scb = new SolidColorBrush(Colors.Red);
            //button1.Background.ToString()==scb.ToString()
            if (bttemp.Background.ToString() == scb.ToString())
            {
                //MessageBox.Show("开始");
                //button1.Background.SetValue();
                #region 打开模块20150914

                string btName = bttemp.Name;
                switch (btName)
                {
                    case "btM00":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[0], true, true);
                        break;
                    case "btM01":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[1], true, true);
                        break;
                    case "btM02":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[2], true, true);
                        break;
                    case "btM03":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[3], true, true);
                        break;
                    case "btM04":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[4], true, true);
                        break;
                    case "btM05":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[5], true, true);
                        break;
                    case "btM06":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[6], true, true);
                        break;
                    case "btM07":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[7], true, true);
                        break;
                    case "btM10":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[8], true, true);
                        break;
                    case "btM11":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[9], true, true);
                        break;
                    //20150928新加的
                    case "btM12":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[10], true, true);
                        break;
                    case "btM13":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[11], true, true);
                        break;
                    case "btM14":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[12], true, true);
                        break;
                    case "btM15":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[13], true, true);
                        break;
                    case "btM16":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[14], true, true);
                        break;
                    case "btM17":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[15], true, true);
                        break;



                    case "btM20":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[16], true, true);
                        break;
                    case "btM21":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[17], true, true);
                        break;
                    case "btM22":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[18], true, true);
                        break;
                    case "btM23":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[19], true, true);
                        break;
                    case "btM24":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[20], true, true);
                        break;
                    case "btM25":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[21], true, true);
                        break;
                    case "btM26":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[22], true, true);
                        break;
                    case "btM27":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[23], true, true);
                        break;

                    case "btM30":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[24], true, true);
                        break;
                    case "btM31":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[25], true, true);
                        break;
                    case "btM32":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[26], true, true);
                        break;
                    case "btM33":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[27], true, true);
                        break;
                    case "btM34":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[28], true, true);
                        break;
                    case "btM35":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[29], true, true);
                        break;
                    case "btM36":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[30], true, true);
                        break;
                    case "btM37":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[31], true, true);
                        break;

                    case "btM40":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[32], true, true);
                        break;
                    case "btM41":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[33], true, true);
                        break;
                    case "btM42":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[34], true, true);
                        break;
                    case "btM43":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[35], true, true);
                        break;
                    case "btM44":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[36], true, true);
                        break;
                    case "btM45":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[37], true, true);
                        break;
                    case "btM46":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[38], true, true);
                        break;
                    case "btM47":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[39], true, true);
                        break;

                    case "btM50":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[40], true, true);
                        break;
                    case "btM51":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[41], true, true);
                        break;
                    case "btM52":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[42], true, true);
                        break;
                    case "btM53":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[43], true, true);
                        break;
                    case "btM54":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[44], true, true);
                        break;
                    case "btM55":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[45], true, true);
                        break;
                    case "btM56":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[46], true, true);
                        break;
                    case "btM57":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[47], true, true);
                        break;

                }

                bttemp.Background = new SolidColorBrush(Colors.Green);
                #endregion 打开模块20150914

            }
            else
            {
                #region 关闭模块20150914
                string btName = bttemp.Name;
                switch (btName)
                {
                    case "btM00":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[0], false, true);
                        break;
                    case "btM01":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[1], false, true);
                        break;
                    case "btM02":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[2], false, true);
                        break;
                    case "btM03":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[3], false, true);
                        break;
                    case "btM04":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[4], false, true);
                        break;
                    case "btM05":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[5], false, true);
                        break;
                    case "btM06":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[6], false, true);
                        break;
                    case "btM07":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[7], false, true);
                        break;
                    case "btM10":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[8], false, true);
                        break;
                    case "btM11":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[9], false, true);
                        break;
                    //20150928新加的
                    case "btM12":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[10], false, true);
                        break;
                    case "btM13":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[11], false, true);
                        break;
                    case "btM14":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[12], false, true);
                        break;
                    case "btM15":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[13], false, true);
                        break;
                    case "btM16":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[14], false, true);
                        break;
                    case "btM17":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[15], false, true);
                        break;



                    case "btM20":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[16], false, true);
                        break;
                    case "btM21":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[17], false, true);
                        break;
                    case "btM22":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[18], false, true);
                        break;
                    case "btM23":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[19], false, true);
                        break;
                    case "btM24":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[20], false, true);
                        break;
                    case "btM25":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[21], false, true);
                        break;
                    case "btM26":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[22], false, true);
                        break;
                    case "btM27":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[23], false, true);
                        break;

                    case "btM30":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[24], false, true);
                        break;
                    case "btM31":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[25], false, true);
                        break;
                    case "btM32":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[26], false, true);
                        break;
                    case "btM33":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[27], false, true);
                        break;
                    case "btM34":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[28], false, true);
                        break;
                    case "btM35":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[29], false, true);
                        break;
                    case "btM36":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[30], false, true);
                        break;
                    case "btM37":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[31], false, true);
                        break;

                    case "btM40":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[32], false, true);
                        break;
                    case "btM41":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[33], false, true);
                        break;
                    case "btM42":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[34], false, true);
                        break;
                    case "btM43":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[35], false, true);
                        break;

                    //case "btM20":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[10], false, true);
                    //    break;
                    //case "btM21":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[11], false, true);
                    //    break;
                    //case "btM22":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[12], false, true);
                    //    break;
                    //case "btM23":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[13], false, true);
                    //    break;
                    //case "btM24":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[14], false, true);
                    //    break;
                    //case "btM25":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[15], false, true);
                    //    break;
                    //case "btM26":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[16], false, true);
                    //    break;
                    //case "btM27":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[17], false, true);
                    //    break;
                    //case "btM30":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[18], false, true);
                    //    break;
                    //case "btM31":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[19], false, true);
                    //    break;
                    //case "btM32":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[20], false, true);
                    //    break;
                    //case "btM33":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[21], false, true);
                    //    break;
                    //case "btM34":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[22], false, true);
                    //    break;
                    //case "btM35":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[23], false, true);
                    //    break;
                    //case "btM36":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[24], false, true);
                    //    break;
                    //case "btM37":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[25], false, true);
                    //    break;
                    //case "btM40":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[26], false, true);
                    //    break;
                    //case "btM41":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[27], false, true);
                    //    break;
                    //case "btM42":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[28], false, true);
                    //    break;
                    //case "btM43":
                    //    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[29], false, true);
                    //    break;
                    
                    case "btM44":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[36], false, true);
                        break;
                    case "btM45":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[37], false, true);
                        break;
                    case "btM46":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[38], false, true);
                        break;
                    case "btM47":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[39], false, true);
                        break;

                    case "btM50":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[40], false, true);
                        break;
                    case "btM51":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[41], false, true);
                        break;
                    case "btM52":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[42], false, true);
                        break;
                    case "btM53":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[43], false, true);
                        break;
                    case "btM54":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[44], false, true);
                        break;
                    case "btM55":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[45], false, true);
                        break;
                    case "btM56":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[46], false, true);
                        break;
                    case "btM57":
                        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[47], false, true);
                        break;

                }
                //MessageBox.Show("出现错误！");
                bttemp.Background = new SolidColorBrush(Colors.Red);
                #endregion 关闭模块20150914
            }
        }



        #region 调试界面初始化20150914
        /// <summary>
        /// 进入调试界面初始化各个按钮，依据本身状态：20150914
        /// </summary>
        public void ReadPLCStateAndChangeColor()
        {
            //0
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[0]))
            {
                btM00.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM00.Background = new SolidColorBrush(Colors.Red);
            }
            //1
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[1]))
            {
                btM01.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM01.Background = new SolidColorBrush(Colors.Red);
            }
            //2
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[2]))
            {
                btM02.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM02.Background = new SolidColorBrush(Colors.Red);
            }
            //3
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[3]))
            {
                btM03.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM03.Background = new SolidColorBrush(Colors.Red);
            }
            //4
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[4]))
            {
                btM04.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM04.Background = new SolidColorBrush(Colors.Red);
            }
            //5
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[5]))
            {
                btM05.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM05.Background = new SolidColorBrush(Colors.Red);
            }
            //6
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[6]))
            {
                btM06.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM06.Background = new SolidColorBrush(Colors.Red);
            }
            //7
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[7]))
            {
                btM07.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM07.Background = new SolidColorBrush(Colors.Red);
            }

            //8
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[8]))
            {
                btM10.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM10.Background = new SolidColorBrush(Colors.Red);
            }
            //9
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[9]))
            {
                btM11.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM11.Background = new SolidColorBrush(Colors.Red);
            }
            //20150928新加，重改
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[10]))
            {
                btM12.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM12.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[11]))
            {
                btM13.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM13.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[12]))
            {
                btM14.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM14.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[13]))
            {
                btM15.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM15.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[14]))
            {
                btM16.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM16.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[15]))
            {
                btM17.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM17.Background = new SolidColorBrush(Colors.Red);
            }


            //10
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[16]))
            {
                btM20.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM20.Background = new SolidColorBrush(Colors.Red);
            }

            //11
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[17]))
            {
                btM21.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM21.Background = new SolidColorBrush(Colors.Red);
            }
            //12
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[18]))
            {
                btM22.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM22.Background = new SolidColorBrush(Colors.Red);
            }
            //13
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[19]))
            {
                btM23.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM23.Background = new SolidColorBrush(Colors.Red);
            }
            //14
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[20]))
            {
                btM24.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM24.Background = new SolidColorBrush(Colors.Red);
            }
            //15
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[21]))
            {
                btM25.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM25.Background = new SolidColorBrush(Colors.Red);
            }
            //16
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[22]))
            {
                btM26.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM26.Background = new SolidColorBrush(Colors.Red);
            }
            //17
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[23]))
            {
                btM27.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM27.Background = new SolidColorBrush(Colors.Red);
            }
            //18
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[24]))
            {
                btM30.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM30.Background = new SolidColorBrush(Colors.Red);
            }
            //19
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[25]))
            {
                btM31.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM31.Background = new SolidColorBrush(Colors.Red);
            }
            //20
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[26]))
            {
                btM32.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM32.Background = new SolidColorBrush(Colors.Red);
            }
            //21
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[27]))
            {
                btM33.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM33.Background = new SolidColorBrush(Colors.Red);
            }
            //22
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[28]))
            {
                btM34.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM34.Background = new SolidColorBrush(Colors.Red);
            }
            //23
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[29]))
            {
                btM35.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM35.Background = new SolidColorBrush(Colors.Red);
            }
            //24
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[30]))
            {
                btM36.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM36.Background = new SolidColorBrush(Colors.Red);
            }

            //25
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[31]))
            {
                btM37.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM37.Background = new SolidColorBrush(Colors.Red);
            }

            //26
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[32]))
            {
                btM40.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM40.Background = new SolidColorBrush(Colors.Red);
            }
            //27
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[33]))
            {
                btM41.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM41.Background = new SolidColorBrush(Colors.Red);
            }
            //28
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[34]))
            {
                btM42.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM42.Background = new SolidColorBrush(Colors.Red);
            }
            //29
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[35]))
            {
                btM43.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM43.Background = new SolidColorBrush(Colors.Red);
            }
            //30
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[36]))
            {
                btM44.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM44.Background = new SolidColorBrush(Colors.Red);
            }
            //31
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[37]))
            {
                btM45.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM45.Background = new SolidColorBrush(Colors.Red);
            }
            //32
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[38]))
            {
                btM46.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM46.Background = new SolidColorBrush(Colors.Red);
            }
            //33
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[39]))
            {
                btM47.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM47.Background = new SolidColorBrush(Colors.Red);
            }

            //34
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[40]))
            {
                btM50.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM50.Background = new SolidColorBrush(Colors.Red);
            }
            //35
            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[41]))
            {
                btM51.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM51.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[42]))
            {
                btM52.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM52.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[43]))
            {
                btM53.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM53.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[44]))
            {
                btM54.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM54.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[45]))
            {
                btM55.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM55.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[46]))
            {
                btM56.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM56.Background = new SolidColorBrush(Colors.Red);
            }

            if (PLCMod.ReadPLCBitVar(PLCMod.PLCDOList[47]))
            {
                btM57.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                btM57.Background = new SolidColorBrush(Colors.Red);
            }
        }
        #endregion 调试界面初始化20150914:20150928改

        //是否单独运行20151102
        public bool IsSolo=false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if(IsSolo)
            {
                string PathDebug = System.AppDomain.CurrentDomain.BaseDirectory;
                //从正常界面进来的是不需要这一步的20150929
                BackPanel.PLCMod.BuildPLCDIDOListFromDB(BackPanel.PLCMod.PLCDOList, PathDebug + "CarChiller.mdb", "PLC_DOLIST");

                UtilityMod_Header.IsDemo = false;
                //UtilityMod_Header.UtilityIni();201509151230
                UtilityMod_Header.PLC_COM2 = new S7200("COM2", 19200, System.IO.Ports.Parity.Even, 8, System.IO.Ports.StopBits.One);
            }
            //PLC_COM2 = new S7200("COM1", 9600, System.IO.Ports.Parity.Even, 8, System.IO.Ports.StopBits.One);
            ReadPLCStateAndChangeColor();
        }


        #endregion 这段核心应该注意:添加20150914
    }
}

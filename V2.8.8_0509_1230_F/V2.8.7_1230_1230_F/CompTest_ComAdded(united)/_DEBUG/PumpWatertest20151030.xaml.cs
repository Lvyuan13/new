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

using BackPanel;
using Utility;

namespace _DEBUG
{
    /// <summary>
    /// PumpWatertest20151030.xaml 的交互逻辑
    /// </summary>
    public partial class PumpWatertest20151030 : Window
    {
        public PumpWatertest20151030()
        {
            InitializeComponent();
            UtilityMod_Header.UtilityIni();
        }

        private void btM07_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ChangState(sender as Button);
        }
        private void ChangState(Button bttemp)
        {
            SolidColorBrush scb = new SolidColorBrush(Colors.Red);
            //button1.Background.ToString()==scb.ToString()
            if (bttemp.Background.ToString() == scb.ToString())
            {
                //MessageBox.Show("开始");
                //button1.Background.SetValue();
                #region 打开模块20150914

                string btName = bttemp.Content.ToString();

                if(btName=="循环泵")
                {
                    UtilityMod_Header.PLC_COM2.TFSet("M", "0.7", true);
                }

                if (btName == "压缩机组")
                {
                    UtilityMod_Header.PLC_COM2.TFSet("M", "1.1", true);
                    UtilityMod_Header.PLC_COM2.TFSet("M", "2.0", true);
                    UtilityMod_Header.PLC_COM2.TFSet("M", "2.1", true);
                    UtilityMod_Header.PLC_COM2.TFSet("M", "1.0", true);
                }
                
                

                
                //switch (btName)
                //{
                //    case "btM00":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[0], true, true);
                //        break;
                //    case "btM01":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[1], true, true);
                //        break;
                //    case "btM02":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[2], true, true);
                //        break;
                //    case "btM03":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[3], true, true);
                //        break;
                //    case "btM04":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[4], true, true);
                //        break;
                //    case "btM05":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[5], true, true);
                //        break;
                //    case "btM06":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[6], true, true);
                //        break;
                //    case "btM07":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[7], true, true);
                //        break;
                //    case "btM10":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[8], true, true);
                //        break;
                //    case "btM11":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[9], true, true);
                //        break;
                //    //20150928新加的
                //    case "btM12":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[10], true, true);
                //        break;
                //    case "btM13":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[11], true, true);
                //        break;
                //    case "btM14":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[12], true, true);
                //        break;
                //    case "btM15":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[13], true, true);
                //        break;
                //    case "btM16":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[14], true, true);
                //        break;
                //    case "btM17":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[15], true, true);
                //        break;



                //    case "btM20":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[16], true, true);
                //        break;
                //    case "btM21":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[17], true, true);
                //        break;
                //    case "btM22":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[18], true, true);
                //        break;
                //    case "btM23":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[19], true, true);
                //        break;
                //    case "btM24":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[20], true, true);
                //        break;
                //    case "btM25":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[21], true, true);
                //        break;
                //    case "btM26":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[22], true, true);
                //        break;
                //    case "btM27":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[23], true, true);
                //        break;

                //    case "btM30":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[24], true, true);
                //        break;
                //    case "btM31":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[25], true, true);
                //        break;
                //    case "btM32":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[26], true, true);
                //        break;
                //    case "btM33":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[27], true, true);
                //        break;
                //    case "btM34":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[28], true, true);
                //        break;
                //    case "btM35":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[29], true, true);
                //        break;
                //    case "btM36":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[30], true, true);
                //        break;
                //    case "btM37":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[31], true, true);
                //        break;

                //    case "btM40":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[32], true, true);
                //        break;
                //    case "btM41":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[33], true, true);
                //        break;
                //    case "btM42":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[34], true, true);
                //        break;
                //    case "btM43":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[35], true, true);
                //        break;
                //    case "btM44":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[36], true, true);
                //        break;
                //    case "btM45":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[37], true, true);
                //        break;
                //    case "btM46":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[38], true, true);
                //        break;
                //    case "btM47":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[39], true, true);
                //        break;

                //    case "btM50":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[40], true, true);
                //        break;
                //    case "btM51":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[41], true, true);
                //        break;
                //    case "btM52":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[42], true, true);
                //        break;
                //    case "btM53":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[43], true, true);
                //        break;
                //    case "btM54":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[44], true, true);
                //        break;
                //    case "btM55":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[45], true, true);
                //        break;
                //    case "btM56":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[46], true, true);
                //        break;
                //    case "btM57":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[47], true, true);
                //        break;

                //}

                bttemp.Background = new SolidColorBrush(Colors.Green);
                #endregion 打开模块20150914

            }
            else
            {
                #region 关闭模块20150914
                string btName = bttemp.Content.ToString();

                if (btName == "循环泵")
                {
                    UtilityMod_Header.PLC_COM2.TFSet("M", "0.7", false);
                }

                if (btName == "压缩机组")
                {
                    UtilityMod_Header.PLC_COM2.TFSet("M", "1.0", false);
                    UtilityMod_Header.PLC_COM2.TFSet("M", "1.1", false);
                    UtilityMod_Header.PLC_COM2.TFSet("M", "2.0", false);
                    UtilityMod_Header.PLC_COM2.TFSet("M", "2.1", false);
                    
                }

                //UtilityMod_Header.PLC_COM2.TFSet("M", "0.7", false);
                //switch (btName)
                //{
                //    case "btM00":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[0], false, true);
                //        break;
                //    case "btM01":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[1], false, true);
                //        break;
                //    case "btM02":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[2], false, true);
                //        break;
                //    case "btM03":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[3], false, true);
                //        break;
                //    case "btM04":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[4], false, true);
                //        break;
                //    case "btM05":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[5], false, true);
                //        break;
                //    case "btM06":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[6], false, true);
                //        break;
                //    case "btM07":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[7], false, true);
                //        break;
                //    case "btM10":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[8], false, true);
                //        break;
                //    case "btM11":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[9], false, true);
                //        break;
                //    case "btM20":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[10], false, true);
                //        break;
                //    case "btM21":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[11], false, true);
                //        break;
                //    case "btM22":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[12], false, true);
                //        break;
                //    case "btM23":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[13], false, true);
                //        break;
                //    case "btM24":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[14], false, true);
                //        break;
                //    case "btM25":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[15], false, true);
                //        break;
                //    case "btM26":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[16], false, true);
                //        break;
                //    case "btM27":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[17], false, true);
                //        break;
                //    case "btM30":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[18], false, true);
                //        break;
                //    case "btM31":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[19], false, true);
                //        break;
                //    case "btM32":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[20], false, true);
                //        break;
                //    case "btM33":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[21], false, true);
                //        break;
                //    case "btM34":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[22], false, true);
                //        break;
                //    case "btM35":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[23], false, true);
                //        break;
                //    case "btM36":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[24], false, true);
                //        break;
                //    case "btM37":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[25], false, true);
                //        break;
                //    case "btM40":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[26], false, true);
                //        break;
                //    case "btM41":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[27], false, true);
                //        break;
                //    case "btM42":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[28], false, true);
                //        break;
                //    case "btM43":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[29], false, true);
                //        break;
                //    case "btM44":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[30], false, true);
                //        break;
                //    case "btM45":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[31], false, true);
                //        break;
                //    case "btM46":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[32], false, true);
                //        break;
                //    case "btM47":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[33], false, true);
                //        break;
                //    case "btM50":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[34], false, true);
                //        break;
                //    case "btM51":
                //        BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDOList[35], false, true);
                //        break;

                //}
                //MessageBox.Show("出现错误！");
                bttemp.Background = new SolidColorBrush(Colors.Red);
                #endregion 关闭模块20150914
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UtilityMod_Header.PLC_COM2.TFSet("M", "0.7", false);

            UtilityMod_Header.PLC_COM2.TFSet("M", "1.0", false);

            UtilityMod_Header.PLC_COM2.TFSet("M", "1.1", false);
            UtilityMod_Header.PLC_COM2.TFSet("M", "2.0", false);
            UtilityMod_Header.PLC_COM2.TFSet("M", "2.1", false);
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UtilityMod_Header.PLC_COM2.TFSet("M", "0.7", false);

            UtilityMod_Header.PLC_COM2.TFSet("M", "1.0", false);

            UtilityMod_Header.PLC_COM2.TFSet("M", "1.1", false);
            UtilityMod_Header.PLC_COM2.TFSet("M", "2.0", false);
            UtilityMod_Header.PLC_COM2.TFSet("M", "2.1", false);
        }

        
    }
}

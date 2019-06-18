using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Utility
{
    /// <summary>
    /// 横河控制器UT35A的通讯类
    /// </summary>
    public class UT35A 
    {
        #region Filed
        private object _lock = new object();
        #endregion

        #region 构造方法
        public UT35A()
        {

        }

        public UT35A(SerialPort serialPort)
        {
            this._port = serialPort;
        }

        public UT35A(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            this._port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }
        #endregion

        #region 端口
        private SerialPort _port;
        /// <summary>
        /// 设备所用端口
        /// </summary>
        public SerialPort Port
        {
            get { return _port; }
            set { _port = value; }
        }
        #endregion

        ///<summary>
        /// 设置UT351
        /// </summary>
        /// <param name="ZD">栈号</param>
        /// <param name="CMD">命令字符 SV:设定值 BS:误差值 OUT:输出值 MOD:模式(1、手动)</param>
        /// <param name="ZValue">设置的值</param>
        /// <param name="SDP">小数位数</param>
        private    void Set(int ZD, string CMD, double ZValue, int SDP)
        {
            // SH:最大值 SL:最小值 DR:方向 OUT:输入值 MOD:模式(1、手动)
            lock (_lock)
            {
                #region UT351
                char STX, ETX, CR, LF;
                string ZDD, ZB;

                STX = (char)2;
                ETX = (char)3;
                CR = (char)13;
                LF = (char)10;
                //ESC = (char)27;

                ZDD = string.Format("{0:00}", (ZD % 100));

                int temp = Convert.ToInt32(ZValue * Math.Pow(10, SDP));
                temp = temp < 0 ? temp + 65536 : temp;
                ZB = string.Format("{0:X}", temp);
                ZB = ZB.Length > 4 ? ZB.Substring(ZB.Length - 4, 4) : ZB.PadLeft(4, '0');
                switch (CMD)
                {
                    case "SV":
                        ZB = "WWRD2101,01," + ZB;
                        break;
                    //case "SH":
                    //    ZB = "WWRD5104,01," + ZB;
                    //    break;
                    //case "SL":
                    //    ZB = "WWRD5105,01," + ZB;
                    //    break;
                    case "DR"://方向 0,1
                        ZB = "WWRD0257,01," + ZB;
                        break;
                    case "BS":
                        ZB = "WWRD2901,01," + ZB;
                        break;
                    case "OUT"://1位小数，手动模式下
                        ZB = "WWRD0217,01," + ZB;
                        break;
                    case "MOD"://1手动0自动
                        ZB = "WWRD0201,01," + ZB;
                        break;
                    case "P"://1位小数，和设定值小数位没有关系
                        ZB = "WWRD0306,01," + ZB;
                        break;
                    case "I"://1位小数，和设定值小数位没有关系
                        ZB = "WWRD0307,01," + ZB;
                        break;
                    case "D"://1位小数，和设定值小数位没有关系
                        ZB = "WWRD0308,01," + ZB;
                        break;

                    case "SDP"://仪表显示小数位温度2位，流量1位，设定小数位后再设定最大最小值
                        ZB = "WWRD1206,01," + ZB;
                        break;
                    case "SH"://最大值
                        ZB = "WWRD1207,01," + ZB;
                        break;
                    case "SL"://最小值
                        ZB = "WWRD1208,01," + ZB;
                        break;
                }
                ZB = STX + ZDD + "010" + ZB + ETX + CR + LF;
                if (!_port.IsOpen)
                {
                    _port.Open();
                }
                _port.DiscardInBuffer();
                _port.DiscardOutBuffer();
                _port.Write(ZB);
                int startTick = Environment.TickCount;
                while (_port.BytesToRead < 9 && Environment.TickCount - startTick < 150)
                {
                    System.Threading.Thread.Sleep(1);
                }
                _port.Close();
                #endregion
            }
        }
        /// <summary>
        /// 读取仪表的显示值
        /// </summary>
        /// <param name="ZD">栈号</param>
        /// <param name="CMD">命令字符 PV:当前值 SV:设定值 BS:误差值 OUT:输入值</param>
        /// <param name="SDP">小数位</param>
        private  double Read(int ZD, string CMD, int SDP)
        {
            lock (_lock) // SH:最大值 SL:最小值 OUT:输入值
            {
                #region UT520
                char STX, ETX, CR, LF;
                string ZDD, ZB = "";

                STX = (char)2;
                ETX = (char)3;
                CR = (char)13;
                LF = (char)10;

                ZDD = string.Format("{0:00}", (ZD % 100));
                switch (CMD)
                {
                    case "PV":
                        ZB = "WRDD2003,01";
                        break;
                    case "SV":
                        ZB = "WRDD2101,01";
                        break;
                    //case "SH":
                    //    ZB = "WRDD5104,01";
                    //    break;
                    //case "SL":
                    //    ZB = "WRDD5105,01";
                    //    break;
                    case "BS":
                        ZB = "WRDD2901,01";
                        break;
                    case "OUT":
                        ZB = "WRDD0005,01";
                        break;
                }
                ZB = STX + ZDD + "010" + ZB + ETX + CR + LF;
                if (!_port.IsOpen)
                {
                    _port.Open();
                }
                _port.DiscardInBuffer();
                _port.DiscardOutBuffer();
                _port.Write(ZB);
                int startTick = Environment.TickCount;
                while (_port.BytesToRead < 13 && Environment.TickCount - startTick < 150)
                {
                    System.Threading.Thread.Sleep(1);
                }
                string result; double val;
                try
                {
                     result = _port.ReadExisting().Substring(7, 4);
                }
                catch(ArgumentOutOfRangeException)
                {
                    result = "XXXX";
                }
                _port.Close();
                try
                {
                    val = Convert.ToInt32(result, 16);
                    val = val > 32768 ? val - 65536 : val;
                    return val / Math.Pow(10, SDP);
                }
                catch
                {
                    return 65535;
                }
                #endregion
            }
        }

        /*给外部调用的接口*/
        private bool _IsSimulate = false;
        public  bool IsSimulate
        {
            get { return _IsSimulate; }
            set { _IsSimulate = value; }
        }

        public  void SetControllerDB(int StackNum, string CMD, double Value, int SDP)
        {
            if (!_IsSimulate)
            {
                this.Set(StackNum, CMD, Value, SDP);
            }
            else
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        public  void SetControllerTF(int StackNum, string CMD, bool Value)
        {
            if (!_IsSimulate)
            {
                double DBValue = 0;
                if (Value)
                    DBValue = 1;
                else
                    DBValue = 0;

                this.Set(StackNum, CMD, DBValue, 1);
            }
            else
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        public double ReadControllerDB(int StackNum, string CMD, int SDP)
        {
            double res = 0;
            if(!_IsSimulate)
            {
               res=this.Read(StackNum,CMD,SDP);
            }
            else
            {
                System.Threading.Thread.Sleep(10);
            }
            return  res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Utility
{
    public class ADAMsVia4520
    {
        //通过ADAM4520挂载的ADAM板卡的通讯
        //通讯默认的设定是 9600,N,8,1 //(10bit)
        //目前支持的挂载板卡为
        //ADAM4024设定4-20mADC电流输出或者1-5VDC的模块
        //ADAM4117测量模块，用于读取电压或者热电偶信号
        

        private object _lock = new object();
        private SerialPort _port;

        public ADAMsVia4520()
        { }

        public ADAMsVia4520(SerialPort port)
        {
            this._port = port;
        }

        public ADAMsVia4520(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            this._port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }



        /// <summary>
        /// 4117读取数据
        /// </summary>
        /// <param name="addr">地址</param>
        /// <returns>返回字符串</returns>
        private  string Read4117(string addr)
        {
            lock (_lock)
            {
                string result = "";
                if (!_port.IsOpen)
                {
                    _port.Open();
                }
                _port.DiscardOutBuffer();
                _port.DiscardInBuffer();
                string cmd = "#" + addr.PadLeft(2, '0') + (Char)13;
                _port.Write(cmd);
                System.Threading.Thread.Sleep(100);
                result = _port.ReadExisting();
                _port.Close();
                return result;
            }
                //string str1 = result.Substring(1, 7);
                //string str2 = result.Substring(8, 7);
        }

        /// <summary>
        /// 4024设定通道值的方法
        /// </summary>
        /// <param name="addr">字符串，不要填充0位</param>
        /// <param name="CHNum"></param>
        /// <param name="Value">通道值允许+99.999到-99.999，范围外的值按0处理</param>
        /// <returns></returns>
        public void  Set4024(string addr, int CHNum, double Value)
        {
            string CHNumStr = CHNum.ToString();
            string ValueStr = For4024ValueStr(Value);//值字符串必须是这个格式
            //string result = "";
            //double DBres = 0;

            if (!_IsSimulate)
            {
                lock (_lock)
                {

                    if (!_port.IsOpen)
                    {
                        _port.Open();
                    }
                    _port.DiscardOutBuffer();
                    _port.DiscardInBuffer();
                    string cmd = "#" + addr.PadLeft(2, '0') + "C"+CHNumStr + ValueStr + (Char)13;
                    _port.Write(cmd);
                    System.Threading.Thread.Sleep(100);
                    //result = _port.ReadExisting();
                    _port.Close();
                    //DBres = Convert.ToDouble(result.Substring(6, 7));

                }
            }
      
        }

        public double Read4024(string addr, int CHNum)
        {
            string CHNumStr = CHNum.ToString();
            double DBres = 0 ;
            if (!_IsSimulate)
            {
                lock (_lock)
                {

                    if (!_port.IsOpen)
                    {
                        _port.Open();
                    }
                    _port.DiscardOutBuffer();
                    _port.DiscardInBuffer();
                    string cmd = "$" + addr.PadLeft(2, '0') + "6C" + CHNumStr  +(Char)13;
                    _port.Write(cmd);
                    System.Threading.Thread.Sleep(100);
                    string  StrRes = _port.ReadExisting();
                    _port.Close();
                    DBres = Convert.ToDouble(StrRes.Substring(4, 7));

                }
            }
            return DBres;
        }

        public string For4024ValueStr(double Value)
        {
            string ValueStr = "+00.000";//值字符串必须是这个格式
            string PosNegSym="+";
            double ValueChecked = 0;
            if (Value > 99.999 || Value < -99.999)
            {
                ValueChecked = 0;
            }
            else
            {
                ValueChecked = Math.Abs(Value);
            }

            if (Value < 0)
            {
                PosNegSym = "-";
            }
            else
            {
                PosNegSym = "+";
            }
            ValueStr = PosNegSym+ValueChecked.ToString("00.000");

            return ValueStr;
        }


        /*对外接口*/
        private bool _IsSimulate = false;

        public bool IsSimulate
        {
            get
            {
                return _IsSimulate;
            }
            set
            {
                _IsSimulate = value;
            }
        }

        /// <summary>
        /// 4117读取对外接口
        /// </summary>
        /// <param name="StackNum">4117的栈号</param>
        /// <param name="ChNums">读取该栈号下4117的通道号的数组</param>
        /// <returns></returns>
        public double[] AquisitionData(int StackNum, int[] ChNums)
        {
            //以下为测试代码
            ////注意电流单位是A不是mA;后面量程转换会涉及
            //double[] res = new double[ChNums.Count()];
            //res[0] = 10e-3;
            //return res;
            //以上为测试代码
            double[] RequireDBres = new double[ChNums.Count()];
            if (!_IsSimulate)
            {
                string ADAMString = this.Read4117(StackNum.ToString());
                double[] ADAMAllDBres = CutADAMString(ADAMString, 8, 7);
                
                for (int k = 0; k < ChNums.Count(); k++)
                {
                    RequireDBres[k] = ADAMAllDBres[ChNums[k]];
                }
            }
            return RequireDBres;

        }

        /// <summary>
        /// 给读取配套的字符串切割函数
        /// </summary>
        /// <param name="ADAMString"></param>
        /// <param name="ADAMChNum"></param>
        /// <param name="SingleChlength"></param>
        /// <returns></returns>
        double[] CutADAMString(string ADAMString,int ADAMChNum,int SingleChlength)
        {
            double[] DBresult = new double[ADAMChNum];
            //int StringTotalLenth = ADAMChNum * SingleChlength + 1;
            for (int i = 0; i < ADAMChNum; i++)
            {
                try
                {
                    DBresult[i] =Convert.ToDouble( ADAMString.Substring(i * SingleChlength + 1, SingleChlength));
                }
                catch
                {
                    DBresult[i] = 9999;
                }
            }
            return DBresult; 
        }







    }
}

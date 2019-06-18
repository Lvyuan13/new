using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Agilent34970A_Comm;
using System.Collections;
//using WT3XXDAQ;

namespace Utility
{
    public class Agilent34970A
    {
        private int _COMPortNum;
        /// <summary>
        /// 安捷伦34970A，按照约定的232通讯设定安捷伦
        /// b:9600,Parity:N,DataSize:8,StopBits:1,FlowCtrl:DTR/DSR
        /// 返回的数组顺序对应通道号从小到大，101-120,201-220?,301-320
        /// </summary>
        /// <param name="COMPortNum"></param>
        public Agilent34970A(int COMPortNum)
        {
            _COMPortNum = COMPortNum;
        }


        /// <summary>
        /// 20150913
        /// </summary>
        /// <param name="VDChsNumStr">电压通道选定</param>
        /// <param name="ADCChsNumStr">电流通道选定</param>
        /// <param name="FRTDChsNumStr">四线RTD通道选定</param>
        /// <param name="TCChsNumStr">热电偶</param>
        /// <param name="TCChsTypeStr">热电偶类型</param>
        /// <returns></returns>
        public   double[] GetAgilentData
            (string VDChsNumStr, string ADCChsNumStr, string FRTDChsNumStr, string TCChsNumStr, string TCChsTypeStr)
        {
            ArrayList Reading = new ArrayList();
            ArrayList TimeStamp = new ArrayList();
            ArrayList ChNum = new ArrayList();

            string ChDelay = "0.01";//通道延迟，秒
            //string CurrentDCChNum = ADCChsNumStr;//电流通道选定
            //string VoltageDCChNum = VDChsNumStr;//"101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117";//电压通道选定omniscience
            //string FRTDChNum = FRTDChsNumStr;//四线RTD通道选定
            string FRTDalpha = "85";//四线RTD温度系数代号
            //string TCChNum = TCChsNumStr;//"201,202,203,204";TC thermal couple 热电偶，
            //string TCChsType = TCChsTypeStr;//"T";
            int COMPortNum = _COMPortNum;//COM号

            //config Agilent34970a communication
            Agilent34970A_Comm.SimpleDAQ AgilentDAQ = new Agilent34970A_Comm.SimpleDAQ();
            Agilent34970A_Comm.SimpleDAQ.ChDef mychdef = new Agilent34970A_Comm.SimpleDAQ.ChDef
                (ChDelay, VDChsNumStr, ADCChsNumStr, FRTDChsNumStr, FRTDalpha, TCChsNumStr, TCChsTypeStr, COMPortNum);



            //Get Agilent data
            AgilentDAQ.GetMeasurement(mychdef,
                                      ref Reading,
                                      ref TimeStamp,
                                      ref ChNum
                                      );
            //AgilentIO.AgilentData.Reading = (double[])Reading.ToArray(typeof(double));
            //AgilentIO.AgilentData.ChNum = (short[])ChNum.ToArray(typeof(short));
            //AgilentIO.AgilentData.TimeStamp = (string[])TimeStamp.ToArray(typeof(string));
            return ((double[])Reading.ToArray(typeof(double)));

        }

    }
}

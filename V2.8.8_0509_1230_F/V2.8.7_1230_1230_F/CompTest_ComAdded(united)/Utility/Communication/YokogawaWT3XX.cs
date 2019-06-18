using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Utility
{
    public  class YokogawaWT3XX
    {

        private int _COMPortNum;
        /// <summary>
        /// 横河功率计，按照约定的232通讯设定功率计
        /// hand-2,for-0,b-4800,LF
        /// </summary>
        /// <param name="COMPortNum"></param>
        public YokogawaWT3XX(int COMPortNum)
        {
            _COMPortNum = COMPortNum;
        }

        public   double[] GetWT310Data()
        {
            WT3XXDAQ.SimpleDAQ WT310DAQ =
            new WT3XXDAQ.SimpleDAQ();
            int COMPortNum =_COMPortNum ;
            //如果需要设定倍率或者接线方式请设定，否则给出空字符串""
            WT310DAQ.CT = ""; //"1";//电流系数;
            WT310DAQ.PhaseWire = ""; //"P3W3";//App.Infos.PowerPhaseWire;

            ArrayList WTIO = new ArrayList();
            WTIO.Add("U,1"); WTIO.Add("U,2"); WTIO.Add("U,3");
            WTIO.Add("I,1"); WTIO.Add("I,2"); WTIO.Add("I,3");
            WTIO.Add("P,SIGMA");
            WTIO.Add("LAMBda,SIGMA");
            //WTIO.Add("FI,1");
            WTIO.Add("FU,1");


            WT310DAQ.GetWT3XXValue(COMPortNum, ref WTIO);
            return (double[])WTIO.ToArray(typeof(double));

        }

    }
}

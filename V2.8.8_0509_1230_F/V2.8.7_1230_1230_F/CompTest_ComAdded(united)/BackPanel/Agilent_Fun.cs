using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Collections;

namespace BackPanel
{

    public static partial class Agilent
    {
        #region 之前工作
        ////public Agilent(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        ////{
        ////    this._port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        ////}

        ////public void ReadFormAgilent()
        ////{ }

        ////public void SendDataToDB()
        ////{ }

        ////public void Initiate()
        ////{ }

        ///// <summary>
        ///// 静态方法：安捷伦通讯获取安捷伦测量量
        ///// </summary>
        ///// <param name="TFRun">是否运行安捷伦</param>
        //private static void GetAgilentData(bool TFRun)
        //{
        //    if (TFRun)
        //    {
        //        //config Agilent34970a communication
        //        SimpleDAQ AgilentDAQ = new SimpleDAQ();
        //        SimpleDAQ.ChDef mychdef = new SimpleDAQ.ChDef();
        //        ArrayList Reading = new ArrayList();
        //        ArrayList TimeStamp = new ArrayList();
        //        ArrayList ChNum = new ArrayList();

        //        mychdef.ChDelay = "0.05";//通道延迟，秒
        //        mychdef.CurrentDCChNum = "";//电流通道选定
        //        mychdef.VoltageDCChNum = "101,102,105,106,107,108,109,110,111,112";//电压通道选定omniscience
        //        mychdef.FRTDChNum = "103,104";//四线RTD通道选定
        //        mychdef.FRTDalpha = "85";//四线RTD温度系数代号
        //        mychdef.COMPortNum = 3;//COM号

        //        //Get Agilent data
        //        AgilentDAQ.GetMeasurement(mychdef,
        //                                  ref Reading,
        //                                  ref TimeStamp,
        //                                  ref ChNum
        //                                  );
        //        AgilentIO.AgilentData.Reading = (double[])Reading.ToArray(typeof(double));
        //        AgilentIO.AgilentData.ChNum = (short[])ChNum.ToArray(typeof(short));
        //        AgilentIO.AgilentData.TimeStamp = (string[])TimeStamp.ToArray(typeof(string));
        //    }
        //    else
        //    {
        //        return;
        //    }


        //}

        ///// <summary>
        ///// 安捷伦变量输出调整量程
        ///// </summary>
        ///// <param name="IO"></param>
        //private static void AgilentIO2Output(AgilentIODef IO)
        //{
        //    //!!!!现场要核对通道顺序是否就是数组顺序
        //    //!!!!现场要核对量程
        //    if (IO.AgilentData.Reading.Count() == 12)
        //    {
        //        //2014-07-27变送器输出全部信号为1-5V
        //        AgilentOutput.AirInDBT = AnalogSignalChangeRange(5, 1, 50, 0, IO.AgilentData.Reading[0]);
        //        AgilentOutput.AirInWBT = AnalogSignalChangeRange(5, 1, 50, 0, IO.AgilentData.Reading[1]);
        //        AgilentOutput.AirOutDBT = IO.AgilentData.Reading[2];//直接测的FRTD
        //        AgilentOutput.AirOutWBT = IO.AgilentData.Reading[3];//直接测的FRTD
        //        AgilentOutput.WaterInTemp = AnalogSignalChangeRange(5, 1, 100, 0, IO.AgilentData.Reading[4]);
        //        AgilentOutput.WaterReturnTemp = AnalogSignalChangeRange(5, 1, 100, 0, IO.AgilentData.Reading[5]);
        //        AgilentOutput.WaterTankOutT = AnalogSignalChangeRange(5, 1, 100, 0, IO.AgilentData.Reading[10]);
        //        AgilentOutput.AirOutP = AnalogSignalChangeRange(5, 1, 250, -250, IO.AgilentData.Reading[7]);//pa
        //        AgilentOutput.NozzleDP = AnalogSignalChangeRange(5, 1, 900, 0, IO.AgilentData.Reading[8]);//pa
        //        AgilentOutput.AtmosphereP = AnalogSignalChangeRange(5, 1, 120, 80, IO.AgilentData.Reading[9]);//kpa
        //        AgilentOutput.WaterDP = AnalogSignalChangeRange(5, 1, 75, 0, IO.AgilentData.Reading[6]);//Kpa
        //        AgilentOutput.WaterFlowRate = AnalogSignalChangeRange(5, 1, 3 * 1000 / 60, 0, IO.AgilentData.Reading[11]);//L/min
        //    }
        //}


        ///// <summary>
        ///// 线性调整测量量的量程
        ///// </summary>
        ///// <param name="SignalUp">原始信号最大值</param>
        ///// <param name="SignalDn">原始信号最小值</param>
        ///// <param name="TargetUp">目标信号最大值</param>
        ///// <param name="TargetDn">目标信号最小值</param>
        ///// <param name="SignalVal">当前原始信号值</param>
        ///// <returns>调整量程后的信号值</returns>
        //private static double AnalogSignalChangeRange
        //(double SignalUp, double SignalDn, double TargetUp, double TargetDn, double SignalVal)
        //{
        //    return ((SignalVal - SignalDn) / (SignalUp - SignalDn) * (TargetUp - TargetDn) + TargetDn);
        //}
        #endregion

        /// <summary>
        /// 建立各个Agilent通道，的各个参数，从最开始的数据库中读取
        /// </summary>
        /// <param name="ListAgilient"></param>
        /// <param name="DBPath"></param>
        /// <param name="TableName"></param>
        public static void BuildAgilentList(List<AgilentVar> ListAgilient, string DBPath, string TableName)
        {

            ListAgilient.Clear();
            List<string[]> StringsListFromDB = Utility.MDBComm.ReadAllStringFromTable(DBPath, TableName);
            for (int i = 0; i < StringsListFromDB.Count; i++)
            {
                AgilentVar AgilientVar = new AgilentVar();  //安捷伦数据定义类的声明  注意：要声明在函数里面
                //各个信号的名称和量程、通道号的赋值
                AgilientVar.Name = StringsListFromDB[i][1];
                AgilientVar.Type = StringsListFromDB[i][2];
                AgilientVar.ChannelNum = StringsListFromDB[i][3];
                AgilientVar.SignalDn = Convert.ToDouble(StringsListFromDB[i][4]);
                AgilientVar.SignalUp = Convert.ToDouble(StringsListFromDB[i][5]);
                try
                {
                    AgilientVar.TargetDn = Convert.ToDouble(StringsListFromDB[i][6]);
                    AgilientVar.TargetUp = Convert.ToDouble(StringsListFromDB[i][7]);
                }
                catch
                {
                    AgilientVar.TargetDn = double.NaN;
                    AgilientVar.TargetUp = double.NaN;
                }
                //修正系数的赋值
                for (int j = 0; j < Agilent.CorrXYPointsNum; j = j + 1)
                {
                    try
                    {
                        AgilientVar.CorrX[j] = Convert.ToDouble(StringsListFromDB[i][2 * j + 8]);
                        AgilientVar.CorrY[j] = Convert.ToDouble(StringsListFromDB[i][2 * j + 1 + 8]);
                    }
                    catch
                    {
                        AgilientVar.CorrX[j] = double.NaN;
                        AgilientVar.CorrY[j] = double.NaN;
                    }
                }

                //AgilientVar.CorrX.sor
                ListAgilient.Add(AgilientVar);

            }
            ListAgilient.TrimExcess();

        }


        /// <summary>
        /// 把读取的硬件数据，转存到，初始值SignalValue里面，最终获得转量程值
        /// </summary>
        /// <param name="ListAgilent"></param>
        /// <returns></returns>
        public static double[] ReadAgilentData(List<AgilentVar> ListAgilent)
        {
            #region 从底层通讯获得的数组：到底是demo还是不模拟:20150915

            double[] DataFromCommAgilent = new double[ListAgilent.Count];

            if (UtilityMod_Header.IsDemo)
            {
                Random rdmtemp = new Random();
                for (int i = 0; i < ListAgilent.Count; i++)
                {
                    //DataFromCommAgilent[i] = 2 + rdmtemp.NextDouble();
                    DataFromCommAgilent[i] = 2 + rdmtemp.Next(1, 3);
                }
            }
            else
            {
                DataFromCommAgilent = UtilityMod_Header.Agilent_COM1.GetAgilentData("205,206,207,208,209,210,215,106,201,204,216,220,219,218", "", "101,102,103,104,105,107,108,109,202", "", "");
            }



            //DataFromCommAgilent 数组为从Agilent里面读取的值
            //double[] DataFromCommAgilent = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };  //从安捷伦通讯程序获取程序
            //DataFromCommAgilent 数组为从Agilent里面读取的值20150913
            //double[] DataFromCommAgilentDown101_122 = UtilityMod_Header.Agilent_COM1.GetAgilentData("106,201,205,206,207,208,209,210,215,216,219,220","221","101,102,103,104,105,107,108,109,110,202,203,204", "", "");  //从安捷伦通讯程序获取程序
            #endregion 从底层通讯获得的数组：到底是demo还是不模拟:20150915


            for (int i = 0; i < AgilentList.Count; i++)
            {
                #region 为了保证接错线而做20151008

                switch (i)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
                #endregion 为了保证接错线而做20151008

                ListAgilent[i].SignalValue = DataFromCommAgilent[i];

                bool IfNAN = double.IsNaN(ListAgilent[i].TargetUp) || double.IsNaN(ListAgilent[i].TargetDn);
                //如果超出范围，就等于测量值
                if (IfNAN == true)
                {
                    ListAgilent[i].TargetValue = ListAgilent[i].SignalValue;

                }
                //否则要等于转量程值
                else
                {
                    ListAgilent[i].TargetValue = AgilentChangeRange
                    (
                    ListAgilent[i].SignalUp, ListAgilent[i].SignalDn,
                    ListAgilent[i].TargetUp, ListAgilent[i].TargetDn,
                    ListAgilent[i].SignalValue
                    );

                }


            }


            //扭矩仪转速修正20511106
            ListAgilent[19].TargetValue = ListAgilent[19].TargetValue * 1.1653 - 7.5;

            return DataFromCommAgilent;
        }

        /// <summary>
        /// 转量程函数，获取目标值
        /// </summary>
        public static double AgilentChangeRange(double SignalUp, double SignalDn, double TargetUp, double TargetDn, double SignalValue)
        {
            double temp;
            //错了！！！
            //temp=SignalValue/(SignalUp-SignalDn)*(TargetUp-TargetDn);

            //改正
            temp = (SignalValue - SignalDn) / (SignalUp - SignalDn) * (TargetUp - TargetDn) + TargetDn;
            return temp;
        }
        /// <summary>
        /// Agilent修正函数
        /// </summary>
        /// <param name="ListAgilentVar"></param>
        public static void AgilentVarListCorrect(List<AgilentVar> ListAgilentVar)
        {
            for (int i = 0; i < ListAgilentVar.Count; i++)
            {
                //找到插值区间
                int k = FindCorrectInterval(ListAgilentVar[i]);
                //获得修正值
                AgilentVarCorrect(ListAgilentVar[i], k);
            }
            //20151104压缩机转速小于0，默认为0
            if (ListAgilentVar[19].TargetCorr < 0)
            {
                ListAgilentVar[19].TargetCorr = 0;
            }
            //20151123冷却水出水温度增加0.14度修正
            ListAgilentVar[9].TargetCorr = ListAgilentVar[9].TargetCorr + 0.14;
            
            for (int i = 0; i < ListAgilentVar.Count;i++ )
            {
                if (InformationGlo.FilterNumber == 1)
                {
                    ListAgilentVar[i].FilterMid = ListAgilentVar[i].TargetCorr;
                }
                if (InformationGlo.FilterNumber != 1)
                {
                    ListAgilentVar[i].TargetCorr = ListAgilentVar[i].TargetCorr * ListAgilentVar[i].FilterCoe + ListAgilentVar[i].FilterMid * (1 - ListAgilentVar[i].FilterCoe);

                    ListAgilentVar[i].FilterMid = ListAgilentVar[i].TargetCorr;
                }
                
            }



        }



        /// <summary>
        /// 找需要修正值的插值区间的序号
        /// </summary>
        /// <param name="AgilentVINP"></param>
        /// <returns></returns>
        public static int FindCorrectInterval(AgilentVar AgilentVINP)
        {
            int Index = -1;

            for (int i = 0; i < Agilent.CorrXYPointsNum - 1; i++)
            {
                if (AgilentVINP.TargetValue >= AgilentVINP.CorrX[i] && AgilentVINP.TargetValue < AgilentVINP.CorrX[i + 1])
                {
                    Index = i;
                    break;
                }
                else
                {
                    //说明不在范围内
                    Index = -1;
                }
            }

            return Index;
        }

        /// <summary>
        /// Agilent修正函数
        /// </summary>
        /// <param name="AgilentVarINP"></param>
        /// <param name="CorrectIntervalIndex"></param>
        public static void AgilentVarCorrect(AgilentVar AgilentVarINP, int CorrectIntervalIndex)
        {
            if (CorrectIntervalIndex != -1)
            {
                double y1 = AgilentVarINP.CorrY[CorrectIntervalIndex];
                double y2 = AgilentVarINP.CorrY[CorrectIntervalIndex + 1];

                double x1 = AgilentVarINP.CorrX[CorrectIntervalIndex];
                double x2 = AgilentVarINP.CorrX[CorrectIntervalIndex + 1];

                double a = (y1 - y2) / (x1 - x2);
                double b = y1 - a * x1;

                AgilentVarINP.TargetCorr = LinearCorrect(a, b, AgilentVarINP.TargetValue);
            }
            else
            {
                AgilentVarINP.TargetCorr = AgilentVarINP.TargetValue;
            }

        }

        /// <summary>
        /// 修正函数里剥离的小线性修正函数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="Inp"></param>
        /// <returns></returns>
        public static double LinearCorrect(double a, double b, double Inp)
        {
            return a * Inp + b;

        }



        #region 整合为一个函数：经过验证，直接调用前台：可以完成，从采集值到，最终的修正值，都在一个list里:20150915
        /// <summary>
        /// 整合为一个函数：经过验证，直接调用前台：可以完成，从采集值到，最终的修正值，都在一个list里:20150915
        /// </summary>
        public static List<AgilentVar> AgilentRecTotal(List<AgilentVar> AgilentList)
        {

            //PLCMod.BuildPLCDIDOListFromDB(PLCMod.PLCDOList, "D:dbPractice.mdb", "abc");
            Agilent.ReadAgilentData(AgilentList);

            Agilent.AgilentVarListCorrect(AgilentList);

            return AgilentList;
        }

        #endregion 整合为一个函数：经过验证，直接调用前台：可以完成，从采集值到，最终的修正值，都在一个list里:20150915


        #region Agilent拼通道，字符串的函数20150922
        public static void CombineAgilentChannel()
        {
            for (int i = 0; i < AgilentList.Count; i++)
            {
                //电压通道选定VDChsNumStr:构造出类似“101，102，103”
                if (AgilentList[i].Type == "电压")
                {
                    Agilent.VDChsNumStr += AgilentList[i].ChannelNum + ",";

                }
                //电流通道选定ADCChsNumStr
                if (AgilentList[i].Type == "电流")
                {
                    Agilent.ADCChsNumStr += AgilentList[i].ChannelNum + ",";

                }
                //四线RTD通道选定FRTDChsNumStr
                if (AgilentList[i].Type == "温度")
                {
                    Agilent.FRTDChsNumStr += AgilentList[i].ChannelNum + ",";

                }
            }

            Agilent.VDChsNumStr = Agilent.VDChsNumStr.Remove(Agilent.VDChsNumStr.Length - 1, 1);
            Agilent.ADCChsNumStr = Agilent.ADCChsNumStr.Remove(Agilent.ADCChsNumStr.Length - 1, 1);
            Agilent.FRTDChsNumStr = Agilent.FRTDChsNumStr.Remove(Agilent.FRTDChsNumStr.Length - 1, 1);



        }
        #endregion Agilent拼通道，字符串的函数
    }


}

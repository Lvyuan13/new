using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackPanel
{
    public static partial class Agilent
    {

        #region 原始
        ///// <summary>
        ///// 安捷伦硬件输入输出数据类
        ///// </summary>
        //public class AgilentIODef
        //{

        //    /// <summary>
        //    /// 安捷伦的IO结构变量定义，这个结构变量定义只用在Background内负责获得数组形式的安捷伦数据
        //    /// </summary>
        //    public struct AgilentDataDef
        //    {
        //        public double[] Reading;
        //        public short[] ChNum;
        //        public string[] TimeStamp;
        //    }
        //    public AgilentDataDef AgilentData = new AgilentDataDef();

        //    /// <summary>
        //    /// 安捷伦硬件输入输出数据类实例构造函数，声明结构变量内各个数组的维数
        //    /// </summary>
        //    public AgilentIODef()
        //    {
        //        AgilentData.Reading = new double[12];
        //        AgilentData.ChNum = new short[12];
        //        AgilentData.TimeStamp = new string[12];
        //    }
        //}

        ///// <summary>
        ///// 安捷伦测量变量类
        ///// </summary>
        //public class AgilentOPT
        //{
        //    public double AirInDBT;  //1
        //    public double AirInWBT;  //2
        //    public double AirOutDBT;//3
        //    public double AirOutWBT;//4
        //    public double WaterInTemp;//5
        //    public double WaterReturnTemp;//6
        //    public double WaterTankOutT;//7
        //    public double AirOutP;//8
        //    public double NozzleDP;//9
        //    public double AtmosphereP;//10
        //    public double WaterDP;//11
        //    public double WaterFlowRate;//12
        //    public double Power;//13

        //}

        ////安捷伦相关变量
        //private static AgilentIODef AgilentIO = new AgilentIODef();//硬件通讯变量
        //public static AgilentOPT AgilentOutput = new AgilentOPT();//测量量变量
        //public static AgilentOPT AgilentOutputCorrected = new AgilentOPT();//测量量修正后变量
        #endregion

        public static int CorrXYPointsNum = 10;

        public class AgilentVar
        {
            //从数据库中读取
            /// <summary>
            /// 
            /// </summary>
            public string Name = "";
            //测量类型：电流、电压、RTD
            public string Type = "";

            public string ChannelNum;
            //量程转换作用
            public double SignalUp;
            public double SignalDn;
            public double TargetUp;
            public double TargetDn;

            //修正系数，获得修正值用
            public double[] CorrX=new double[CorrXYPointsNum];
            public double[] CorrY=new double[CorrXYPointsNum];

            /// <summary>
            /// 原始信号
            /// </summary>
            public double SignalValue;
            /// <summary>
            /// 转量程值
            /// </summary>
            public double TargetValue;
            /// <summary>
            /// 修正值，经过XY系数修正
            /// </summary>
            public double TargetCorr;
            /// <summary>
            /// 过滤系数
            /// </summary>
            public double FilterCoe=1;
            /// <summary>
            /// 过滤中间量
            /// </summary>
            public double FilterMid=0;
        }

        public static List<AgilentVar> AgilentList=new List<AgilentVar>();


        #region 通道字符串变量声明20150922
        // <param name="VDChsNumStr">电压通道选定</param>
        // <param name="ADCChsNumStr">电流通道选定</param>
        // <param name="FRTDChsNumStr">四线RTD通道选定</param>
        // <param name="TCChsNumStr">热电偶</param>
        // <param name="TCChsTypeStr">热电偶类型</param>
        /// <summary>
        /// 电压通道选定
        /// </summary>
        public static string VDChsNumStr;
        /// <summary>
        /// 电流通道选定
        /// </summary>
        public static string ADCChsNumStr;
        /// <summary>
        /// 四线RTD通道选定
        /// </summary>
        public static string FRTDChsNumStr;
        /// <summary>
        /// 热电偶
        /// </summary>
        public static string TCChsNumStr;
        /// <summary>
        /// 热电偶类型
        /// </summary>
        public static string TCChsTypeStr;

        #endregion 通道字符串变量声明
        
    }
}

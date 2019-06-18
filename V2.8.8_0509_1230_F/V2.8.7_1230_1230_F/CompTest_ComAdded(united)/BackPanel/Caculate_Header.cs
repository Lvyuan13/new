using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackPanel
{
    public static partial class Calculate
    {
        //double.NaN是非数字的意思20150908
        //添加double.NaN，是为了验证，这个值是否经过计算；经过计算则为数字！20150908
        public class CalCar
        {
            /// <summary>
            /// 漏热系数W/C
            /// </summary>
            public double A_HeatDissipCoe = 20;
            /// <summary>
            /// 制冷剂流量
            /// </summary>
            public double A_RefrigFlowMass = double.NaN;
            /// <summary>
            /// 制冷量
            /// </summary>
            public double A_CoolingCapacity = 0;
            ///// <summary>
            ///// 性能系数
            ///// </summary>
            //public double A_COP = double.NaN;
            /// <summary>
            /// 量热器漏热量 kW
            /// </summary>
            public double A_HeatLeak = double.NaN;

            #region G方法

            //添加20150908
            /// <summary>
            /// 制冷剂平均饱和温度：通过冷凝器两侧压力的平均压力对应的温度对应的温度
            /// </summary>
            //public double G_RefrigAveragSatTemp = double.NaN;

            /// <summary>
            /// 漏热系数
            /// </summary>
            public double G_HeatDissipCoe = 0;
            /// <summary>
            /// 制冷剂流量
            /// </summary>
            public double G_RefrigFlowMass = double.NaN;

            /// <summary>
            /// 制冷量
            /// </summary>
            public double G_CoolingCapacity =0;
            ///// <summary>
            ///// 性能系数
            ///// </summary>
            //public double G_COP = double.NaN;
            /// <summary>
            /// 楼热量 kW
            /// </summary>
            public double G_HeatLeakInCondenser;
            /// <summary>
            /// 冷凝器换热量 kW
            /// </summary>
            public double G_HeatExchangeInCondenser;

            #endregion G方法


            #region 公共
            /// <summary>
            /// 主辅试验误差
            /// </summary>
            public double TestErr = 0;
            /// <summary>
            /// 实际压缩机功率
            /// </summary>
            public double ActualCompressPower = double.NaN;



            /// <summary>
            /// 性能系数只有一个！取二者的平均
            /// </summary>
            public double AG_COP = 0;
            #endregion 公共

            #region 待定
            
            #endregion 待定
        }

        public static CalCar CalculateCar = new CalCar();




        public class CalChiller
        {
            /// <summary>
            /// 漏热系数W/K
            /// </summary>
            public double HeatDissipCoe = 2;
            /// <summary>
            /// 漏热量kW:20151130
            /// </summary>
            public double HeatDissipCap = double.NaN;
            /// <summary>
            /// kg/s
            /// </summary>
            public double RefrigFlowMass = double.NaN;
            /// <summary>
            /// kW
            /// </summary>
            public double CoolingCapacity = double.NaN;

            public double TestErr = double.NaN;
            /// <summary>
            /// kW
            /// </summary>
            public double ActualCompressPower = double.NaN;
            public double COP = double.NaN;

            #region 公共
            /// <summary>
            /// 量热器压力对应温度
            /// </summary>
            public double TemperaOfHeatMeas = double.NaN;
            #endregion 公共

        }

        public static CalChiller CalculateChiller = new CalChiller();
    }
}

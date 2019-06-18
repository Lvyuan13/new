using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Refprop_Wrapper;

namespace CsharpRefprop
{
    public class CommRefProp
    {
        private PureRefProp _PureRefName;// = new PureRefProp(RefpropCalc.FluidType.R22);

        public enum CommRefType
        {
            R22,R134a,R12,R141b,R123
        }
        public string RefNameStr = "";
        public CommRefProp(CommRefType RefName)
        {
            RefNameStr = RefName.ToString();
            switch (RefName)
            {
                case CommRefType.R141b:
                    _PureRefName = new PureRefProp(RefpropCalc.FluidType.R141b);
                    break;
                case CommRefType.R22:
                    _PureRefName = new PureRefProp(RefpropCalc.FluidType.R22);
                    break;
                case CommRefType.R134a:
                    _PureRefName = new PureRefProp(RefpropCalc.FluidType.R134a);
                    break;
                case CommRefType.R123:
                    _PureRefName = new PureRefProp(RefpropCalc.FluidType.R123);
                    break;
                case CommRefType.R12:
                    _PureRefName = new PureRefProp(RefpropCalc.FluidType.R12);
                    break;
            }
        }

        /// <summary>
        /// R22已知饱和压力求对应气相饱和温度
        /// </summary>
        /// <param name="Psat">饱和压力,MPa</param>
        /// <returns>饱和温度,C</returns>
        public  double Tsat_Vap(double Psat)
        {
            double res =0;

            if (CheckPressureInput(Psat))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                res = _PureRefName.FluidProp(PureRefProp.PropName.Tsat, RefpropCalc.InputUnitType.Pvap, new double[1] { Psat });
            }
            return res;
        }

        /// <summary>
        /// R22已知饱和温度求对气相应饱和压力
        /// </summary>
        /// <param name="Tsat">饱和温度,C</param>
        /// <returns>饱和压力,MPa</returns>
        public  double Psat_Vap(double Tsat)
        {
           
            double res =0;
            if (CheckTemperatureInput(Tsat))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                res = _PureRefName.FluidProp(PureRefProp.PropName.Psat, RefpropCalc.InputUnitType.Tvap, new double[1] { Tsat });
            }
            return res;
        }

        /// <summary>
        /// R22已知饱和压力求对应液相饱和温度
        /// </summary>
        /// <param name="Psat">饱和压力,MPa</param>
        /// <returns>饱和温度,C</returns>
        public  double Tsat_Liq(double Psat)
        {
            double res = 0;

            if (CheckPressureInput(Psat))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                res = _PureRefName.FluidProp(PureRefProp.PropName.Tsat, RefpropCalc.InputUnitType.Pliq, new double[1] { Psat });
            }
            return res;
        }

        /// <summary>
        /// R22已知饱和温度求对应液相饱和压力
        /// </summary>
        /// <param name="Tsat">饱和温度,C</param>
        /// <returns>饱和压力,MPa</returns>
        public  double Psat_Liq(double Tsat)
        {

            double res = 0;
            if (CheckTemperatureInput(Tsat))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                res = _PureRefName.FluidProp(PureRefProp.PropName.Psat, RefpropCalc.InputUnitType.Tliq, new double[1] { Tsat });
            }
            return res;
        }


        /// <summary>
        /// R22已知温度压力求比焓
        /// </summary>
        /// <param name="T">温度,C</param>
        /// <param name="P">压力,MPa</param>
        /// <returns>比焓,kJ/kg</returns>
        public  double TPforH(double T, double P)
        {

            double res =0;
            if (CheckPressureInput(P) && CheckTemperatureInput(T))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                res = _PureRefName.FluidProp(PureRefProp.PropName.TPforH, RefpropCalc.InputUnitType.TP, new double[2] { T, P });
            }
            return res;

        }

        public  double PsatforH_Liq(double Psat)
        {

            double res = 0;
            if (CheckPressureInput(Psat))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                res = _PureRefName.FluidProp(PureRefProp.PropName.PforH, RefpropCalc.InputUnitType.Pliq, new double[1] { Psat });
            }
            return res;
        }

        public  double PsatforH_Vap(double Psat)
        {

            double res = 0;
            if (CheckPressureInput(Psat))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                res = _PureRefName.FluidProp(PureRefProp.PropName.PforH, RefpropCalc.InputUnitType.Pvap, new double[1] { Psat });
            }
            return res;
        }

        /// <summary>
        /// 温度范围检查
        /// </summary>
        /// <param name="T">C</param>
        /// <returns></returns>
        private  bool CheckTemperatureInput(double T)
        {
            if (T > -20 && T < 150)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 压力范围检查
        /// </summary>
        /// <param name="P">MPa</param>
        /// <returns></returns>
        private  bool CheckPressureInput(double P)
        {
            if (P >=-0.01 && P < 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

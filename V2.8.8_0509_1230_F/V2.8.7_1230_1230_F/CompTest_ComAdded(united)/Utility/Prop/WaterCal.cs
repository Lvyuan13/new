using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class WaterCal
    {
        //private string mdbpath = AppDomain.CurrentDomain.BaseDirectory + "PropDatabase.mdb";
        /// <summary>
        /// 计算标准大气压下水的比热容,kJ/(kg C),适用3-70C
        /// </summary>
        /// <param name="WaterT">温度,C</param>
        /// <returns></returns>
        public double Cp(double WaterT)
        {

            double Cp = Math.Pow(WaterT, 5) * (-6.272335E-11)
                      + Math.Pow(WaterT, 4) * (1.648741E-08)
                      + Math.Pow(WaterT, 3) * (-1.772695E-06)
                      + Math.Pow(WaterT, 2) * (1.056731E-04)
                      + WaterT * (-3.273944E-03)
                      + 4.218977E+00;

            return Cp;
        }

        /// <summary>
        /// 计算标准大气压下水的密度,kg/m3,范围3-70C
        /// </summary>
        /// <param name="Temperature">温度,C</param>
        /// <returns></returns>
        public double Density(double Temperature)
        {
            double Density =
                - 0.0041031 * Math.Pow(Temperature, 2)
                - 0.0419596 * Math.Pow(Temperature, 1)
                + 1000.5270866;
            return Density;
        }
    }
}

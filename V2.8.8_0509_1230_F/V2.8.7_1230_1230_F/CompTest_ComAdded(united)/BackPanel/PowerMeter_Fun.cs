using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;


namespace BackPanel
{
    public static partial class PowerMeter
    {
        /// <summary>
        /// 从底层获得数组：20150916
        /// </summary>
        public static double[] ReadDataFromDN()
        {
            double[] WT310DataCOM3;
            //UtilityMod_Header.IsDemo
            if (UtilityMod_Header.IsDemo)
            {
                WT310DataCOM3 = new double[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            }
            else
            {
                //PowerMeter.WT310DataCOM3 = UtilityMod_Header.WTCOM3.GetWT310Data();
                WT310DataCOM3 = UtilityMod_Header.WTCOM3.GetWT310Data();

            }
            return WT310DataCOM3;
        }


    }
}

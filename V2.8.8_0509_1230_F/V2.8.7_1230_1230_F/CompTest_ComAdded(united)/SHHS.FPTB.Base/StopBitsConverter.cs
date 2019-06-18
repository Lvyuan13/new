using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace SHHS.FPTB.Base
{
    public class StopBitsConverter
    {
        /// <summary>
        /// 通过头字母转换为StopBits
        /// </summary>
        /// <param name="headerChar">
        /// N:None
        /// O:One
        /// T:Two
        /// OPF:OnePointFive
        /// </param>
        /// <returns></returns>
        public static StopBits FromHeader(string headerChar)
        {
            switch (headerChar)
            {
                case "N":
                    return StopBits.None;
                case "O":
                    return StopBits.One;
                case "OPF":
                    return StopBits.OnePointFive;
                case "T":
                    return StopBits.Two;
                default:
                    return StopBits.One;
            }
        }
    }
}

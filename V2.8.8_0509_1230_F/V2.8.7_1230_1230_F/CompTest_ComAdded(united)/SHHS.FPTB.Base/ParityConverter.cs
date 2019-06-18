using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace SHHS.FPTB.Base
{
    public class ParityConverter
    {
        /// <summary>
        /// 通过头字母转换为Parity 默认为None
        /// </summary>
        /// <param name="headerChar">
        /// N:None
        /// E:Even
        /// M:Mark
        /// O:Odd
        /// S:Space
        /// </param>
        /// <returns></returns>
        public static Parity FromHeader(string headerChar = "N")
        {
            switch (headerChar)
            {
                case "N":
                    return Parity.None;
                case "E":
                    return Parity.Even;
                case "M":
                    return Parity.Mark;
                case "O":
                    return Parity.Odd;
                case "S":
                    return Parity.Space;
                default:
                    return Parity.None;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;


namespace BackPanel
{
    public static class UtilityMod_Header
    {
        /// <summary>
        /// 是否模拟：true是模拟；false是不模拟。
        /// </summary>
        public static bool IsDemo=true;

        //安捷伦通讯
        public static  Agilent34970A Agilent_COM1;// = new Agilent34970A(1);
       
        //PLC
        public static S7200 PLC_COM2;// = new S7200("COM2", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);

        //WT功率计 
        public static YokogawaWT3XX WTCOM3;// = new YokogawaWT3XX(3);
        //public static YokogawaWT3XX WTCOM4 = new YokogawaWT3XX(4);

        //ADAM板卡 
        //public static ADAMsVia4520 ADAM_COM5 = new ADAMsVia4520("COM5", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);

        //UT
        public static UT35A Controller_COM6;// = new UT35A("COM6", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);

        //空气物性
        public static Psychrometrics WetAir;// = new Psychrometrics();

        //水物性
        public static WaterCal Water;// = new WaterCal();

        
        //制冷剂物性
        public static RefrigantProp RefNistProp;// = new RefrigantProp();

        public static void UtilityIni()
        {
            Agilent_COM1 = new Agilent34970A(1);
            //2015:com2
            PLC_COM2 = new S7200("COM2", 19200, System.IO.Ports.Parity.Even, 8, System.IO.Ports.StopBits.One);
            WTCOM3 = new YokogawaWT3XX(4);
            Controller_COM6 = new UT35A("COM3", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            WetAir = new Psychrometrics();
            RefNistProp = new RefrigantProp();
            Water = new WaterCal();

            //Agilent_COM1 = new Agilent34970A(1);
            ////2015:com2
            //PLC_COM2 = new S7200("COM2", 9600, System.IO.Ports.Parity.Even, 8, System.IO.Ports.StopBits.One);
            //WTCOM3 = new YokogawaWT3XX(4);
            //Controller_COM6 = new UT35A("COM3", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            //WetAir = new Psychrometrics();
            //RefNistProp = new RefrigantProp();
            //Water = new WaterCal();
        }

    }
}

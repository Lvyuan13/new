using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Agilent34970A_Comm;
using System.Collections;
using CsharpRefprop;
using Utility;
using System.Threading;
//using WT3XXDAQ;

namespace CommunicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //////安捷伦增加TC通道后的测试
            ////GetAgilentData("", "", "", "", "");
            ////GetWT310Data();


            CommRefProp REF = new CommRefProp(CommRefProp.CommRefType.R141b);
            double Temp = 2;
            //while (Temp < 90)
            //{
            //    Console.WriteLine(REF.RefNameStr + "TforP: " + REF.TPforH(Temp, 1.2).ToString());
            //    Temp += 0.001;
            //}
            //Console.Read();

            while(Temp<5)
            {

                Console.WriteLine(REF.Tsat_Vap(Temp).ToString());
                Temp += 0.5;
            }

            Console.Read();
            //2015.08.06安捷伦功率计通讯155测试代码
            //2015.08.06安捷伦功率计通讯155测试代码
            //Agilent34970A AGILENT = new Agilent34970A(1);
            //YokogawaWT3XX WT = new YokogawaWT3XX(2);


            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("GP1");
            //    double[] Ares1 = AGILENT.GetAgilentData("102,103,104,105,106,107", "", "", "101", "J");

            //    PrintDBArray(Ares1);
            //    Console.WriteLine("");

            //    Console.WriteLine("GP2");
            //    double[] Ares2 = AGILENT.GetAgilentData("108", "", "", "101", "J");
            //    PrintDBArray(Ares2);

            //    Console.WriteLine("");

            //    double[] WTres = WT.GetWT310Data();

            //    Console.WriteLine("WT");
            //    PrintDBArray(WTres);
            //    Console.WriteLine("");


            //}
            //2015.08.06安捷伦功率计通讯155测试代码 ebd

           // //ADAM 4024
           // ADAMsVia4520 ADAM4024 = new ADAMsVia4520("COM2",9600, System.IO.Ports.Parity.None,8, System.IO.Ports.StopBits.One );
    
           // ADAM4024.IsSimulate = false;
           // ADAM4024.Set4024("2", 0, 12);
           // double  ADAM4024CH1Current= ADAM4024.Read4024("2", 0);
           // Console.WriteLine(ADAM4024CH1Current);
           // Console.Read();
            
           //ADAM4024.Set4024("2", 0, 0);
           // Console.Read();

            //ADAM4024
        }

        private static void PrintDBArray(double[] Array)
        {
            for (int i = 0; i < Array.Count(); i++)
            {
                Console.WriteLine(Array[i].ToString());
            }
        }

       


    }
}

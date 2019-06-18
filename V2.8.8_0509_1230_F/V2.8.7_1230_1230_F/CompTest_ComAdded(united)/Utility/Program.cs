using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            RefrigantProp REF = new RefrigantProp();
            //REF.RefInUsing = REF.r141b;
            double Temp = 40;
            while (Temp < 60)
            {
                double res = REF.r141b.TPforH(Temp, 1.2);
                Console.WriteLine("TforP: " + res.ToString());
                Temp += 0.1;
            }
            
            

            //Console.WriteLine("r134a" + res.ToString());
            Console.Read();
        }
    }
}

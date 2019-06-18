using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefMDBInquiry
{
    class Program
    {
        static void Main(string[] args)
        {

            //double INP1 = 1.05;
            //double res = DBRefPropInquiry.ForSatProp
            //    ( DBRefPropInquiry.DBRefName.R134a , DBRefPropInquiry.DBRefPropName.EnthalpyL, DBRefPropInquiry.DBRefPropName.Temp, 99);
            //Console.WriteLine(res.ToString());

           

            double res2 = DBRefPropInquiry.TPForSbcOrSphProp
                (DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, true, 11, 0.853);
            Console.WriteLine(res2.ToString());

            Console.Read();
        }
    }
}

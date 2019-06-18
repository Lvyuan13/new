using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Refprop_Wrapper;

namespace CsharpRefprop
{
    class Program
    {
        static void Main(string[] args)
        {
            //PureRefProp R22 = new PureRefProp(RefpropCalc.FluidType.R22);
            //double res=R22.FluidProp(PureRefProp.PropName.PHforT,
            //                        RefpropCalc.InputUnitType.PH,
            //                        new double[2]{0.1,500}
            //                        );
            //R22.FluidProp(PureRefProp.PropName.LatentHeat,
            //                        RefpropCalc.InputUnitType.TP,
            //                        new double[2]{99.634,0.1});
            //R22.FluidProp(PureRefProp.PropName.SpecificEnthalpy,
            //                        RefpropCalc.InputUnitType.TP,
            //                        new double[2] {7,0.0999999999726}
            //                        );
            //R22.FluidProp(PureRefProp.PropName.SpecificVolumn,
            //                    RefpropCalc.InputUnitType.TP,
            //                    new double[2] { 90, 0.1 }
            //                    );
            //R22.FluidProp(PureRefProp.PropName.Density,
            //        RefpropCalc.InputUnitType.TP,
            //        new double[2] { 90, 0.1 }
            //        );
            CommRefProp REF = new CommRefProp(CommRefProp.CommRefType.R141b);
            double Temp=10;
            while (Temp < 90)
            {
                Console.WriteLine(REF.RefNameStr + "TforP: " + REF.TPforH(Temp, 1.2).ToString());
                Temp += 0.0001;
            }
            Console.Read();
        }
        
    }
}

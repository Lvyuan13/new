using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Refprop_Wrapper;
using System.Windows.Forms;

namespace CsharpRefprop
{
    public class PureRefProp
    {
        private RefpropCalc.RefpropConfig_Struct _Config;
        private double _CalResult;
        private RefpropCalc.Product_Struct _PureFluid;
        private RefpropCalc _PureFluidPropCalc = new RefpropCalc();


        /// <summary>
        /// 单位采用SI-C
        /// T--C;P--MPa;H--kJ/kg;Density--kg/m3
        /// </summary>
        public enum PropName
        {
            Tsat,
            Psat,
            TPforH,
            PforH,
            TforH,
            THforP,
            PHforT,
            
            
            SpecificVolumn,
            SpecificEnthalpy,
            SpecificHeat,
            LatentHeat,
            Density
            
        }

        //public
        public PureRefProp(RefpropCalc.FluidType FluidName)
        {
            
            //Config DLL
            _Config.EnableLogInfo = true; //输出错误日志
            
            _Config.ErrorValue = -99999; //约定返回错误的值
            _Config.UndefinedValue = -999999; //约定未定义时返回的值

            _Config.MainDir = "C:/Program Files/REFPROP/";
            _Config.FluidsDir = "C:/Program Files/REFPROP/Fluids/";
            _Config.FluidsPrefix = "Fluids/";
            _Config.MixturesDir = "C:/Program Files/REFPROP/mixtures/";
            _Config.MixturePrefix = "Mixtures/";
            _Config.LogDir = "E:/RefpropLog/";//错误日志的目录

            if (RefpropCalc.RefpropInitialize(_Config) == true)
            {
                //初始化成功，准备计算！
            }
            else
            {
                MessageBox.Show("物性计算初始化失败","物性计算",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }// if

            // Config Fluid, by 2014-6-4 only pure fluid available
            Array.Resize(ref _PureFluid.Fluids.Components, 1);
            _PureFluid.Fluids.Components[0] = FluidName;

            Array.Resize(ref _PureFluid.Fluids.MoleFractions, 1);
            _PureFluid.Fluids.MoleFractions[0] = 1;

            _PureFluid.DefaultMixture = RefpropCalc.MixtureType.NONE;

            _PureFluid.ReferenceState = RefpropCalc.RefState.DEF;

        }// public RefProp()

        public double FluidProp(PropName ReqProp,RefpropCalc.InputUnitType KnownPropNames,double[] KnownPropValue)
        {
            _PureFluid.InputUnits = KnownPropNames;
            _PureFluid.Units = RefpropCalc.UnitSystem.C;
            _PureFluidPropCalc.SetProductProperties(_PureFluid);
            GC.Collect();
            GC.WaitForFullGCComplete();
            switch (ReqProp)
            {
                case PropName.Tsat:
                    _CalResult = _PureFluidPropCalc.Temperature(KnownPropValue[0]);
                    break;
                case PropName.TPforH:
                    _CalResult=_PureFluidPropCalc.Enthalpy(KnownPropValue[0],KnownPropValue[1]);
                    break;
                case PropName.PHforT:
                    _CalResult = _PureFluidPropCalc.Temperature(KnownPropValue[0], KnownPropValue[1]);
                    break;
                case PropName.Psat:
                    _CalResult = _PureFluidPropCalc.Pressure(KnownPropValue[0]);
                    break;
                case PropName.SpecificEnthalpy:
                    _CalResult = _PureFluidPropCalc.Enthalpy(KnownPropValue[0], KnownPropValue[1]);
                    break;
                case PropName.SpecificVolumn:
                    _CalResult = _PureFluidPropCalc.Volume(KnownPropValue[0], KnownPropValue[1]);
                    break;
                case PropName.LatentHeat:
                    _CalResult = _PureFluidPropCalc.LatentHeat(KnownPropValue[0], KnownPropValue[1]);
                    break;
                case PropName.Density:
                    _CalResult = _PureFluidPropCalc.Density(KnownPropValue[0], KnownPropValue[1]);
                    break;
                case PropName.PforH:
                    _CalResult = _PureFluidPropCalc.Enthalpy(KnownPropValue[0]);
                    break;
                case PropName.TforH:
                    _CalResult = _PureFluidPropCalc.Enthalpy(KnownPropValue[0]);
                    break;
                   
            }
            //调试时控制台输出语句
            //Console.WriteLine(
            //                   _PureFluid.Fluids.Components[0].ToString() + ":"
            //                 + ReqProp.ToString() + "=" + _CalResult.ToString()
            //                 );
            return _CalResult;
        }//  PureFluidProp


    }// public class RefProp
}

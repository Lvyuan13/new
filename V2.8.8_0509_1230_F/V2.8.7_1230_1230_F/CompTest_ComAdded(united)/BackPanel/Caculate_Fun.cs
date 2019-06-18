using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackPanel
{
    public static partial class Calculate
    {

        #region Car
        /// <summary>
        /// 计算漏热系数:方法A
        /// </summary>
        /// <param name="InputHeatPower">输入加热器的电功率，W</param>
        /// <param name="SecSatTemp">对应于第二制冷剂液体压力的饱和温度，℃</param>
        /// <param name="AverageTemp">平均环境温度，℃</param>
        public static void A_CarCalOfHeatDissipCoe(double Qh, double tp, double ta)
        {
            CalculateCar.A_HeatDissipCoe = Qh / (tp - ta);
        }
        /// <summary>
        /// 计算制冷剂流量：方法A:20151013验证
        /// </summary>
        /// <param name="InputHeatMeasurePower">输入量热器或气体冷却器的热量，W；</param>
        /// <param name="SecSatTemp">第二制冷剂饱和温度，℃；</param>
        /// <param name="AverageEnTemp">平均环境温度，℃</param>
        /// <param name="EnthOfGasLeaveHeatMeasure">离开量热器或气体冷却器的被蒸发的制冷剂比焓，J/kg；</param>
        /// <param name="EnthOfInExp">进入膨胀阀的制冷剂液体比焓，J/kg；</param>
        public static double A_CarCalOfRefFlowMass(double Qi, double ts, double ta, double hg2, double hf2)
        {
            //double Qi=InputHeatMeasurePower;
            double Fl = CalculateCar.A_HeatDissipCoe;
            //double ts = SecSatTemp;
            //double ta = AverageEnTemp;
            //double hg2 = EnthOfGasLeaveHeatMeasure;
            CalculateCar.A_HeatLeak = Fl * 0.1 * (ta - ts)/1000;
            //double hf2 = EnthOfInExp;
            CalculateCar.A_RefrigFlowMass = (Qi * 1000 + Fl *0.1* (ta - ts)) / ((hg2 - hf2) * 1000);
            return (Qi * 1000 + Fl * (ta - ts)) / ((hg2 - hf2)*1000);
        }

        /// <summary>
        /// 计算制冷量：方法A：20151013验证
        /// </summary>
        /// <param name="SVOfInComp">进入压缩机的制冷剂蒸汽的实际比容，m3/kg；</param>
        /// <param name="SVOfRule">与规定基本试验工况相对应的吸入工况时制冷剂蒸汽的比容，m3/kg；</param>
        /// <param name="EnthInCompRule">在规定的基本试验工况下，进入压缩机的制冷剂比焓，J/kg；</param>
        /// <param name="EnthOutCompRul">与基本试验工况规定的压缩机排气压力相对应的液体饱和温度（或露点温度）下的制冷剂液体比焓，J/kg；</param>
        public static double A_CarCalOfCoolCap(double _vga, double _vgl, double _hgl, double _hfl)
        {
            double qmf = CalculateCar.A_RefrigFlowMass;
            double vga = _vga;
            double vgl = _vgl;
            double hgl = _hgl;
            double hfl = _hfl;
            CalculateCar.A_CoolingCapacity = qmf * vga / vgl * ((hgl - hfl) * 1000);
            return  qmf * vga / vgl * (hgl - hfl);
        }


        #region G方法

        #region 注意备注
        //G方法的漏热系数是额外确定的，不需要在计算里实现，只是赋值就可以了20150908
        #endregion 注意备注

        /// <summary>
        /// 计算漏热系数:方法G
        /// </summary>
        /// <param name="InputHeatPower">输入加热器的电功率，W</param>
        /// <param name="RefAveSatTemp">制冷剂的平均饱和温度（或露点温度），℃；</param>
        /// <param name="AverageEnTemp">平均环境温度，℃</param>
        public static void G_CarCalOfHeatDissipCoe(double InputHeatPower, double RefAveSatTemp, double AverageEnTemp)
        {
            CalculateCar.G_HeatDissipCoe = InputHeatPower / (RefAveSatTemp - AverageEnTemp);
        }

     
        /// <summary>
        /// 制冷剂流量：方法G:20151013验证
        /// </summary>
        /// <param name="WatSpeHeat">C——水比热容，J/(kg﹒K)</param>
        /// <param name="WatOut"> 水出口温度，℃</param>
        /// <param name="WatIn">水进口温度，℃</param>
        /// <param name="CoolWatMassFlow">冷却水质量流量，kg/s</param>
        /// <param name="RefAveSatTemp">制冷剂的平均饱和温度（或露点温度），℃；</param>
        /// <param name="AverageEnTemp">平均环境温度，℃</param>
        /// <param name="EnthOfInCon">进入冷凝器的制冷剂蒸汽比焓，J/kg；</param>
        /// <param name="EnthOfOutCon">离开冷凝器的液体比焓，J/kg；</param>
        public static double G_CarCalOfRefFlowMass(double WatSpeHeat, double t2, double t1, double qmc, double tr, double ta, double hg3, double hf3)
        {
            double C = WatSpeHeat;
            double Fl = CalculateCar.G_HeatDissipCoe;
            //double t2 = WatOut;
            //double t1 = WatIn;
            //double tr = RefAveSatTemp;
            //double ta = AverageEnTemp;
            //double qmc = CoolWatMassFlow;
            //double hg3 = EnthOfInCon;
            //double hf3 = EnthOfOutCon;
            //CalculateCar.G_RefrigFlowMass = (C * (t2 - t1) * qmc * 1000 / 3600 + Fl * (tr - ta)) / ((hg3 - hf3) * 1000);
            CalculateCar.G_HeatExchangeInCondenser = C * (t2 - t1) * qmc;
            CalculateCar.G_HeatLeakInCondenser = Fl * (tr - ta);

            CalculateCar.G_RefrigFlowMass = (C * (t2 - t1) * qmc * 1000  + Fl * (tr - ta)) / ((hg3 - hf3) * 1000);
            return (C * (t2 - t1) * qmc * 1000  + Fl * (tr - ta)) / (1000 * (hg3 - hf3));

        }


        /// <summary>
        /// 计算制冷量：方法G:修改：20150919:20151013验证
        /// </summary>
        /// <param name="SVOfInComp">进入压缩机的制冷剂蒸汽的实际比容，m3/kg；vga</param>
        /// <param name="SVOfRule">与规定基本试验工况相对应的吸入工况时制冷剂蒸汽的比容，m3/kg；vgl</param>
        /// <param name="EnthInCompRule">在规定的基本试验工况下，进入压缩机的制冷剂比焓，J/kg；hgl</param>
        /// <param name="EnthOutCompRul">与基本试验工况规定的压缩机排气压力相对应的饱和温度（或露点温度）下的制冷剂液体比焓，J/kg；hfl</param>
        public static double G_CarCalOfCoolCap(double vga, double vgl, double hgl, double hfl)
        {
            double qmf = CalculateCar.G_RefrigFlowMass;
            //double vga = SVOfInComp;
            //double vgl = SVOfRule;
            //double hgl = EnthInCompRule;
            //double hfl = EnthOutCompRul;
            CalculateCar.G_CoolingCapacity = qmf * vga / vgl * ((hgl - hfl) * 1000);
            return qmf * vga / vgl * (hgl - hfl);
        }
        #endregion G方法

        /// <summary>
        /// 计算AG之间的偏差：CalculateCar.TestErr ：20151013验证
        /// </summary>
        public static double AG_CarCalOfTestError()
        {

            double CC1 = CalculateCar.A_CoolingCapacity;
            double CC2 = CalculateCar.G_CoolingCapacity;
            return  Math.Abs(2 * (CC1 - CC2) / (CC1 + CC2) * 100);
        }

        /// <summary>
        /// 压缩机轴功率：20151013验证
        /// </summary>
        /// <param name="ShaftTor">压缩机轴扭矩，N﹒m；</param>
        /// <param name="Rotate">压缩机的实测转速，rpm；</param>
        public static double AG_CarCalOfShaftPower(double ShaftTor, double Rotate)
        {
            double Pz;
            double N;
            double na;
            N = ShaftTor;
            na = Rotate;
            CalculateCar.ActualCompressPower = na * (N - InformationGlo.Torque_0) / (60 * 99.33) ;
            Pz = na * (N - InformationGlo.Torque_0) / (60 * 99.33);
            return Pz;
        }
        /// <summary>
        /// COP计算：20151013验证
        /// </summary>
        /// <param name="SVOfInComp">进入压缩机的制冷剂蒸汽的实际比容，m3/kg:Vga</param>
        /// <param name="SVOfRule">与规定基本试验工况相对应的吸入工况时制冷剂蒸汽的比容，m3/kg:Vgl</param>
        public static double AG_CarCalOfCOP(double Vga, double Vgl)
        {
            double Qoa = (CalculateCar.A_CoolingCapacity+CalculateCar.G_CoolingCapacity)/2;
            double Pz = CalculateCar.ActualCompressPower*1000;
            //double Vga = SVOfInComp;
            //double Vgl = SVOfRule;
            //CalculateCar.AG_COP = Qoa / (Pz * Vga / Vgl);
            return  Qoa / (Pz * Vga / Vgl);
           
        }

        
        #endregion Car



        #region Chiller
        /// <summary>
        /// 漏热系数
        /// </summary>
        /// <param name="HeatMeaInput">量热器的输入热量，W;</param>
        /// <param name="SecSat">第二制冷剂饱和温度，℃；</param>
        /// <param name="HMEnvAve">量热器周围平均环境温度，℃。</param>
        public static void ChillerCalOfHeatDissipCoe(double HeatMeaInput, double SecSatTemp, double HMEnvAve)
        {
            
            double Qh=HeatMeaInput;
            double ts = SecSatTemp;
            double ta=HMEnvAve;
            CalculateChiller.HeatDissipCoe=Qh/(ts-ta);
        }
        
        /// <summary>
        /// 漏热量：20151013验证
        /// </summary>
        /// <param name="ts">第二制冷剂饱和温度，℃</param>
        /// <param name="ta">量热器周围平均环境温度，℃</param>
        public static void ChillerCalOfHeatDissipCap(double ts, double ta)
        {
            double KL = CalculateChiller.HeatDissipCoe;
            //double ta=SecSatTemp;
            //double ts=HMEnvAve;
            CalculateChiller.HeatDissipCap=KL*(ta-ts)/1000;
        }
        /// <summary>
        /// Chiller制冷量：20151013验证，有点问题hg1和v1
        /// </summary>
        /// <param name="Qh">量热器输入热量</param>
        /// <param name="hg1">规定</param>
        /// <param name="hg2"></param>
        /// <param name="hf1"></param>
        /// <param name="hf2"></param>
        /// <param name="v1">规定</param>
        /// <param name="vg1"></param>
        public static double ChillerCalOfCoolCap(double Qh, double hg1, double hg2, double hf1, double hf2, double v1, double vg1)
        {
            double Qa=CalculateChiller.HeatDissipCap;
            //double Qh = HeatMeaInput;
            //double hg1 = InChillerVapNorm;
            //double hg2 = EnthOutHeatMea;
            //double hf1=EnthOutChiller;
            //double hf2=EnthInExpan;
            //double v1=SpecVoluInChiller;
            //double vg1=SpecVoluInChillerNorm;

            //求制冷剂流量20150919单位都是kJ，或kW
            CalculateChiller.RefrigFlowMass=(Qh+Qa)/(hg2-hf2);
            //求制冷量kW
            CalculateChiller.CoolingCapacity = (hg1 - hf1) / (hg2 - hf2) * (Qh + Qa) * v1 / vg1;

            return (hg1 - hf1) / (hg2 - hf2) * (Qh + Qa) * v1 / vg1;
        }


        /// <summary>
        /// 压缩机实际功率：20150925改，直接通过功率计读！：20151013验证没有问题
        /// </summary>
        /// <param name="ShaftTor"></param>
        /// <param name="Rotate"></param>
        public static void ChillerCalOfInputPower(double[] WT310DataCOM3_Chiller)
        {
            //double Pz;
            //double N;
            //double na;
            //N = ShaftTor;
            //na = Rotate;
            //Pz = na * N / 99.33;
            //CalculateCar.ActualCompressPower = Pz;
            //CalculateChiller.ActualCompressPower = Pz;
            //20150925
            CalculateChiller.ActualCompressPower = WT310DataCOM3_Chiller[6];
        }
        /// <summary>
        /// 制冷机组COP：20151013
        /// </summary>
        /// <param name="SVOfInComp">进入Chiller的制冷剂蒸汽的实际比容（比体积），m3/kg； Vga:V1</param>
        /// <param name="SVOfRule">与规定基本试验工况相对应的吸入工况时制冷剂蒸汽的比容（比体积），m3/kg；Vgl：Vg1</param>
        public static void ChillerCalOfCOP(double Vga, double Vgl)
        {
            //CalculateChiller.COP = CalculateChiller.CoolingCapacity / CalculateChiller.ActualCompressPower;]
            double Qoa = CalculateChiller.CoolingCapacity;
            double Pz = CalculateChiller.ActualCompressPower;
            //double Vga = SVOfInComp;
            //double Vgl = SVOfRule;
            //CalculateCar.G_COP = Qoa / (Pz * Vga / Vgl);
            CalculateChiller.COP = Qoa / (Pz * Vga / Vgl);
        }

        #endregion Chiller


       
    }
}

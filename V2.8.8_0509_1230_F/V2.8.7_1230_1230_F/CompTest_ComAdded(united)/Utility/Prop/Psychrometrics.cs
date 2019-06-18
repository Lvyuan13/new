using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class Psychrometrics
    {
        //湿空气计算的变量定义
        private class _ASHARE09coe
        {
            public double c8 = -5.8002206e03;
            public double c9 = 1.3914993e00;
            public double c10 = -4.8640239e-02;
            public double c11 = 4.1764768e-05;
            public double c12 = -1.4452093e-08;
            public double c13 = 6.5459673e00;

            public double c14 = 6.45;
            public double c15 = 14.526;
            public double c16 = 0.7389;
            public double c17 = 0.09486;
            public double c18 = 0.4569;
        }
        private _ASHARE09coe ASHARE09coe = new _ASHARE09coe();//Ashare09系数
        private double p;        //大气总压 kPa
        private double t;        //DBT 干球温度 K
        private double t_;       //WBT 湿球温度 K
        private  double pw;       //湿空气的水蒸气分压 kPa
        private double pws;      //干球温度下对应的饱和水蒸气分压kPa
        private double pws_;     //湿球温度下对应的饱和水蒸气分压kpa
        private double Ws;       //干球温度下对应饱和湿空气的含湿率 kg_w/kg_da
        private double Ws_;      //湿球温度下对应饱和湿空气的含湿率 kg_w/kg_da
        private double viscosity = 0.0151854189093 * 10e-4; //湿空气的粘度m^2/s

        public double DBT;      //干球温度 C
        public double WBT;      //湿球温度 C
        public double AirP; //大气压力 kPa
        public double RH;       //相对含湿量 mole_w/mole_ws；在理想气体假设下亦为kpa_w/kpa_ws
        public double W;        //湿空气的含湿率kg_w/kg_da
        public double h;        //湿空气的比焓 kJ/kg_da
        public double v;        //湿空气的比体积 m^3/kg_da

        public void DBT_WBT_P_forOtherProp(double dbt, double wbt, double airp)//dbt C;wbt C;airp kPa
        {
            DBT = dbt;
            WBT = wbt;
            AirP = airp;
            
            //准备计算
            t = dbt + 273.15;//K 
            t_ = wbt + 273.15;//K 
            p = airp;//kPa

            //干球温度下的计算
            //干球温度下对应饱和湿空气的水蒸气分压pws的计算
            pws = Math.Exp(
                            ASHARE09coe.c8 * Math.Pow(t, -1)
                          + ASHARE09coe.c9 + ASHARE09coe.c10 * t
                          + ASHARE09coe.c11 * Math.Pow(t, 2)
                          + ASHARE09coe.c12 * Math.Pow(t, 3)
                          + ASHARE09coe.c13 * Math.Log(t)
                          ) / 1000;    //kPa
            //干球温度下对应饱和湿空气的含湿率Ws的计算
            Ws = 0.621945 * pws / (p - pws);    //kg_w/kg_da

            //湿球温度下的计算
            //湿球温度下对应饱和湿空气的水蒸气分压pws_的计算
            pws_ = Math.Exp(
                            ASHARE09coe.c8 * Math.Pow(t_, -1)
                           + ASHARE09coe.c9 + ASHARE09coe.c10 * t_
                           + ASHARE09coe.c11 * Math.Pow(t_, 2)
                           + ASHARE09coe.c12 * Math.Pow(t_, 3)
                           + ASHARE09coe.c13 * Math.Log(t_)
                          ) / 1000;  //kPa
            //湿球温度下对应饱和湿空气的含湿率Ws_的计算
            Ws_ = 0.621945 * pws_ / (p - pws_); //kg_w/kg_da

            //其他物性的计算
            //湿空气含湿率W的计算 注意需要C单位的DBT和WBT
            W = ((2501 - 2.326 * wbt) * Ws_ - 1.006 * (dbt - wbt))
              / (2501 + 1.86 * dbt - 4.186 * wbt);
            //湿空气中水蒸气分压pw的计算
            pw = p * W / (0.621945 + W);        //kPa
            //湿空气相对湿度RH的计算
            double u = W / Ws;
            RH = u / (1 - (1 - u) * (pw / p));
            //湿空气比焓的计算 需要C单位的DBT
            h = 1.006 * dbt + W * (2501 + 1.86 * dbt);  //kJ/kgda
            //湿空气比体积v的计算
            v = 0.287042 * (t) * (1 + 1.607858 * W) / p; //m^3/kg_da


        }



        /// <summary>
        /// 已知湿空气大气压力，干球温度，相对湿度，求湿球温度
        /// </summary>
        /// <param name="airp">大气压力 kPa</param>
        /// <param name="dbt">干球温度 C</param>
        /// <param name="rh">相对湿度 无量纲</param>
        /// <returns>湿球温度 C</returns>
        public void DBT_RH_P_ForOtherProp(double dbt, double rh,double airp)
        {
            //搜索计算出湿球温度
            double wbtStep=3;   //湿球温度搜索步长，初值为3 C
            int n = 1;          //当前步长下的搜索计数
            double rh0=1;       //上一次搜索的rh,初值为100%
            double rh1=1;         //本次搜索的rh,初值为100%
            WBT = dbt;          //湿球温度的搜索起点为干球温度
            do
            {
                rh0 = rh1;
                DBT_WBT_P_forOtherProp(dbt, WBT - wbtStep * n, airp);
                rh1 = RH;
                n++;
            } while ((rh0 - rh) * (rh1 - rh) > 0);

            wbtStep = -0.5; //第二次反向搜索的步长0.5，-代表反向
            n = 1;          //搜索计数器重置
            do
            {
                rh0 = rh1;
                DBT_WBT_P_forOtherProp(dbt, WBT - wbtStep * n, airp);
                rh1 = RH;
                n++;
            } while ((rh0 - rh) * (rh1 - rh) > 0);

            wbtStep = 0.05; //第三次正向搜索的步长0.05
            n = 1;          //搜索计数器重置
            do
            {
                rh0 = rh1;
                DBT_WBT_P_forOtherProp(dbt, WBT - wbtStep * n, airp);
                rh1 = RH;
                n++;
            } while ((rh0 - rh) * (rh1 - rh) > 0);

            wbtStep = -0.005; //第四次反向搜索的步长0.05
            n = 1;          //搜索计数器重置
            do
            {
                rh0 = rh1;
                DBT_WBT_P_forOtherProp(dbt, WBT - wbtStep * n, airp);
                rh1 = RH;
                n++;
            } while ((rh0 - rh) * (rh1 - rh) > 0);

            //最后计算有根区间中点处的wbt下的各项物性
            DBT_WBT_P_forOtherProp(dbt, WBT + 0.5 * wbtStep, airp);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Report
{
    public static class SenarioForReport
    {
        public enum Senario_ForReport
        {
            CarCooling,   //汽车空调压缩机制冷试验
            CarNoise,     //汽车空调压缩机噪声试验
            //ChillerNormialConditionTemp,    //水冷压缩冷凝机组名义工况基于出水温度
            //ChillerNormialConditionWaterFlow,//水冷压缩冷凝机组名义工况基于冷却水流量

            ChillerNormialCondition,  // 上面两个今后更改为这个为了2*4共八个场景而已20151129
            ChillerPartialCondition, //水冷压缩冷凝机组部分工况
            ChillerMaxCondition,  ////水冷压缩冷凝机组最大负荷工况
            ChillerChangCondition,  //水冷压缩冷凝机组变工况
        }
        public static Senario_ForReport senario_ForReport;

    }

    /// <summary>
    /// 储存数据路径
    /// </summary>
    public static class DBPath_ForReport
    {

        //public static string DBPath_ForReport;
        public static string DBPath_ForReportChild;
    }

}

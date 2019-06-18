using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace WpfApplication2
{
    public static class GlobelVar
    {

        

        #region  初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public static void GlobelVarIni()
        {
            ////解决报警变量初始化问题，在闪屏阶段
            //ImmediateStopIsError = false;
            //WaterTankerHeightIsError = false;
            //WaterFlowOnOffIsError = false;
            //ChillerHighAndLowPreIsError = false;
            //HeatMeasureHighPreIsError = false;

        }

        ///// <summary>
        ///// 单独报警初始化
        ///// </summary>
        //public static void GlobeVarIni_Alarm()
        //{
        //    //ImmediateStopIsError = false;
        //    //WaterTankerHeightIsError = false;
        //    //WaterFlowOnOffIsError = false;
        //    //ChillerHighAndLowPreIsError = false;
        //    //HeatMeasureHighPreIsError = false;

        //}
        #endregion


        /// <summary>
        /// 是控制流量，还是温度，Info的窗口IsControlWaterTemperature
        /// true,是控制出水温度；false是控制冷却水流量
        /// </summary>
        public static bool IsControlWaterTemperature = false;

        ///// <summary>
        ///// 不同场景（工况）
        ///// </summary>
        //public enum Senario
        //{

        //    CarCooling,   //汽车空调压缩机制冷试验
        //    CarNoise,     //汽车空调压缩机噪声试验
        //    ChillerNormialConditionTemp,    //水冷压缩冷凝机组名义工况基于出水温度
        //    ChillerNormialConditionWaterFlow,//水冷压缩冷凝机组名义工况基于冷却水流量
        //    ChillerPartialCondition, //水冷压缩冷凝机组部分工况
        //    ChillerMaxCondition,  ////水冷压缩冷凝机组最大负荷工况
        //    ChillerChangCondition  //水冷压缩冷凝机组变工况
        //}
        //public static Senario senario;

        ////主钟
        //public static  System.Windows.Threading.DispatcherTimer timer;



        //#region 报警事件的各个变量//消除20150916
        ////false是正常，true是故障
        //public static bool ImmediateStopIsError = false;
        //public static bool WaterTankerHeightIsError = false;
        //public static bool WaterFlowOnOffIsError = false;
        //public static bool ChillerHighAndLowPreIsError = false;
        //public static bool HeatMeasureHighPreIsError = false;
        ////初始化在上面初始模块
        //#endregion


        #region 稳定时间里的手动记录还是自动记录
        /// <summary>
        /// false 手动记录，true 自动记录
        /// </summary>
        public static bool StableRecordIsAuto = false;
        #endregion


        /// <summary>
        /// 改变，0是正常启动信息输入；1是在Car实验主窗口的操作！
        /// </summary>
        public static int InfoChangeCar = 0;
        /// <summary>
        /// 是在试验中改变，信息：0，是在试验之前；1，是在试验之中！
        /// </summary>
        public static int InfoChangeChiller = 0;




        #region 自动采集 总时间以及间隔
        /// <summary>
        /// 总的记录时间 min
        /// </summary>
        public static double RecordTotalTime=1000;
        /// <summary>
        /// 采样间隔 min
        /// </summary>
        public static double RecordSpan=15;

        /// <summary>
        /// 总采样记录时间 s
        /// </summary>
        public static int RecordTotalTimeSec=60;
        /// <summary>
        /// 采样间隔 s
        /// </summary>
        public static int RecordSpanSec = 900;

        /// <summary>
        /// 记录次数
        /// </summary>
        public static int RecordNum=5;

        /// <summary>
        /// 稳定记录list20151126
        /// </summary>
        public static List<string> StableRecordList_Glo = new List<string>();
        #endregion


        #region 判稳定的参数：控制参数20150919添加
        //需要的判温误差20150907
        public static double CarDischargeTemperatureRequire_Dif;
        public static double CarInputSaturateTemperatureRequire_Dif;
        public static double CarInputTemperatureRequire_Dif;
        public static double CarCompressorRotateRequire_Dif;
        public static double CarCoolingWaterRequire_Dif;

        //New 判稳条件20151126
        public static double CarDischargePressureRequire_Dif=0.01;
        public static double CarSuctionPressureRequire_Dif=0.01;
        public static double CarSuctionTemperatureRequire_Dif=3;
        public static double CarRotateRequire_Dif=3;
        public static double CarEnvironmentTemperature_Dif=0;
        public static double CarAGcoolingCapacity_Dif=0.04;   //单位 100%


        public static double ChillerEvaperator_TemperatureRequire_Dif=3;

        public static double ChillerEvaperator_PressRequire_Dif=0.01;
        public static double ChillerSuction_TemperatureRequire_Dif=3;
        public static double ChillerInputWater_TemperatureRequire_Dif=3;
        public static double ChillerOutputWater_TemperatureRequire_Dif=3;
        public static double ChillerCoolingWater_FlowRateRequire_Percent_Dif=0.5;

        //当前的判温误差20150907
        public static double CarDischargeTemperatureActual_Dif;
        public static double CarInputSaturateTemperatureActual_Dif;
        public static double CarInputTemperatureActual_Dif;
        public static double CarCompressorRotateActual_Dif;
        public static double CarCoolingWaterActual_Dif;

        public static double ChillerEvaperator_TemperatureActual_Dif;
        public static double ChillerInputWater_TemperatureActual_Dif;
        public static double ChillerOutputWater_TemperatureActual_Dif;
        public static double ChillerCoolingWater_FlowRateActual_Dif;

     

        //需要的主界面Set（button）的设定值全局变量20150907
        public static double CarDischargeTemperatureSet = 63;
        public static double CarInputSaturateTemperatureSet = -1;
        public static double CarInputTemperatureSet = 9;
        public static double CarCompressorRotateSet = 1800;
        public static double CarCoolingWaterSet = 15;


        #region 判稳专用201525
        
        /// <summary>
        /// 新添加判稳用20151225
        /// </summary>
        public static double CarDischargePressureSetOnlyStable = 1.804;
        /// <summary>
        /// 吸气压力新添加判稳用20151225
        /// </summary>
        public static double CarInputPressureSetOnlyStable = 0.282;
        /// <summary>
        /// 吸气温度20151225
        /// </summary>
        public static double CarInputTemperatureSetOnlyStable = 9;
        /// <summary>
        /// 
        /// </summary>
        public static double CarCompressorRotateSetOnlyStable = 1800;


        //Chiller
        public static double ChillerEvaperatorPressureSetOnlyStable=0.375;

        public static double ChillerSuctionTempSetOnlyStable = 18;

        public static double ChillerInWaterTempSetOnlyStable = 30;

        public static double ChillerCoolingWaterFlowRateSetOnlyStable = 1.4;

        public static double ChillerOutWaterTempSetOnlyStable = 35;
        #endregion 判稳专用

        public static double ChillerEvaperator_TemperatureSet = 7;
        public static double ChillerInputWater_TemperatureSet=30;
        public static double ChillerOutputWater_TemperatureSet = 35;
        public static double ChillerCoolingWater_FlowRateSet = 15;
        /// <summary>
        /// 新添加吸气温度20151221
        /// </summary>
        public static double ChillerSuction_TemperatureSet = 18;
        

        #region Set Button需要加的，20150919：控制参数！
        /// <summary>
        /// 排气压力
        /// </summary>
        public static double CarPressDischarge = 1.804;


        /// <summary>
        /// 吸气压力
        /// </summary>
        public static double CarPressSuction = 0.282;
        /// <summary>
        /// 压缩机环境温度
        /// </summary>
        public static double CarTempOfCompEnvir = 65;
        /// <summary>
        /// 制冷机组蒸发压力
        /// </summary>
        public static double ChillerPressEvap = 0.375;

        /// <summary>
        /// 冷水机组吸气温度20151221
        /// </summary>
        public static double ChillerSuctionTemp = 18;
        #endregion Set Button需要加的，20150919

        #region 总的平衡:状态判定量
        /// <summary>
        /// 判断汽车空调压缩机试验平衡与否
        /// </summary>
        public static bool IsStableCar = false;
        /// <summary>
        /// 判断水冷机组试验平衡与否
        /// </summary>
        public static bool IsStableChiller = false;
        #endregion 总的平衡:状态判定量
        /// <summary>
        /// 排气饱和压力
        /// </summary>
        public static double CarDischargeSatTemp_ForDBRef=63;
        /// <summary>
        /// 吸气饱和压力
        /// </summary>
        public static double CarSuctionSatTemp_ForDBRef=-1;
        /// <summary>
        /// 吸气温度
        /// </summary>
        public static double CarSuctionTem_ForDBRef=9;

        /// <summary>
        /// 蒸发温度Chiller
        /// </summary>
        public static double ChillerEvaperatorTem_ForDBRef = 7;



        #endregion


        #region 数据库信息
        /// <summary>
        /// 初始化建库时用
        /// </summary>
        public static string Dir_Glo;
        public static string FileName_Glo;
        /// <summary>
        /// 调用数据库用
        /// </summary>
        public static string DirBefore_Glo;
        /// <summary>
        /// 是否是调用之前的Car
        /// </summary>
        public static bool IsGetBeforeInfo_Car = false;
        /// <summary>
        /// 是否是调用之前的Chiller
        /// </summary>
        public static bool IsGetBeforeInfo_Chiller = false;
        /// <summary>
        /// 是否调用之前的CarAirCondition、AirCooledChiller、Condenser、Evaporator信息
        /// </summary>
        public static bool IsGetBeforeInfo_CarAirCondition = false;
        public static bool IsGetBeforeInfo_AirCooledChiller = false;
        public static bool IsGetBeforeInfo_Condenser = false;
        public static bool IsGetBeforeInfo_Evaporator = false;

        #region 初始化字段 :到时可以放到全局变量！Globel
        public class InfoVarGlo
        {
            //public bool CarOrWaterCompressor=false; //默认0，汽车空调压缩机 ；1为水冷压缩冷凝机组  

            public string[] TestInfo = new string[25];
        }
        public static InfoVarGlo TestInfoDefGlo = new InfoVarGlo();
        #endregion 初始化字段

        #endregion

        #region Agilent采集通道对应变量20150908/20150913
        /// <summary>
        /// 记住总共有22个但是是0-21；所以索引数=通道号-1；20150908
        /// </summary>
        public static double[] Array_Agilent101_122 = new double[22];
        /// <summary>
        /// 记住总共有22个但是是0-21；所以索引数=通道号-1；20150908
        /// </summary>
        public static double[] Array_Agilent201_222 = new double[22];

        #endregion Agilent采集通道对应变量

        #region 几个需要查物性的变量，都是可以查的20150908：计算calculate需要 20150908
        #region A方法需要20150908
       
        /// <summary>
        /// 对应于第二制冷剂液体压力的饱和温度:Car的和整个系统连起来的时候的对应压力的饱和温度：1
        /// </summary>
        public static double SecSatTemp_Glo_A = 1.4;//BackPanel.UtilityMod_Header.RefNistProp.R141b.Tsat_Vap(1.2);
        /// <summary>
        /// 离开量热器或气体冷却器的被蒸发的制冷剂比焓,J/kg:2
        /// </summary>
        public static double hg2_Glo=2.5;
        /// <summary>
        /// 进入膨胀阀的制冷剂液体比焓,J/kg
        /// </summary>
        public static double hf2_Glo = 1.5;
        /// <summary>
        /// 进入压缩机的制冷剂蒸汽的实际比容
        /// </summary>
        public static double vga_Glo = 2.5;
        /// <summary>
        /// 与规定基本试验工况相对应的吸入工况时制冷剂蒸汽的比容：名义工况20150918:吸气温度：9C；吸气压力对应的饱和温度-1C
        /// </summary>
        public static double vg1_Glo=1.5;
        /// <summary>
        /// 在规定的基本试验工况下，进入压缩机的制冷剂比焓
        /// </summary>
        public static double hg1_Glo = 2.5;
        /// <summary>
        /// 与基本试验工况规定的压缩机排气压力相对应的饱和温度（或露点温度）下的制冷剂液体比焓，J/kg；
        /// </summary>
        public static double hf1_Glo=1.5;
        ///// <summary>
        ///// 量热器漏热量 kW:  在bpinfo中
        ///// </summary>
        //public static double HeatDissapationOfHeatMeasure=0;

        #endregion A方法
        #region G方法需要20150908

        /// <summary>
        /// 制冷剂平均饱和温度：G方法计算制冷剂流量的时候用的！是整个系统运行时用的！
        /// </summary>
        public static double tr_Glo=20;
        /// <summary>
        /// 水的比热:KJ/(kg.K)
        /// </summary>
        public static double C_GlO=4181;
        /// <summary>
        /// 水的密度 kg/m3
        /// </summary>
        public static double WaterDensity_Glo = 1;
        /// <summary>
        /// 进入冷凝器的制冷剂蒸汽比焓
        /// </summary>
        public static double hg3_GlO=1.5;
        /// <summary>
        /// 离开冷凝器的液体比焓
        /// </summary>
        public static double hf3_GlO=2.5;
        /// <summary>
        /// 冷凝器换热量 kW：报表需要
        /// </summary>
        public static double HeatExchangeInCondenser = 0;

        /// <summary>
        /// 冷凝器漏热量 kW:报表须要 
        /// </summary>
        public static double HeatDissapInCondenser = 0;
        #endregion G方法需要

        #region Chiller需要20150908
        /// <summary>
        /// 第二制冷剂饱和温度，在试验工况下使用！不是在标定漏热量时使用！
        /// </summary>
        public static double ts_GloChiller=1.5;
        /// <summary>
        /// 在规定工况下进入Chiller的制冷剂蒸汽的比焓
        /// </summary>
        public static double hg1_GloChiller=2.5;
        /// <summary>
        /// 量热器出口的制冷剂蒸汽的比焓，J/kg
        /// </summary>
        public static double hg2_GloChiller=1.4;
        /// <summary>
        /// 离开Chiller的制冷剂液体比焓，J/kg
        /// </summary>
        public static double hf1_GloChiller=2.4;
        /// <summary>
        /// 膨胀阀进口处的制冷剂液体的比焓，J/kg
        /// </summary>
        public static double hf2_GloChiller=1.6;
        /// <summary>
        /// 进入机组制冷剂蒸汽实际比体积，m3/kg:相当于Car里的Vga
        /// </summary>
        public static double v1_GloChiller=2.3;
        /// <summary>
        /// 在规定工况下进入机组制冷剂蒸汽比体积，m3/kg：相当于Car里的Vgl：20150919改：规定工况：蒸发温度7，吸气温度18C
        /// </summary>
        public static double vg1_GloChiller=1.7;

        #endregion


        
        #endregion 几个需要查物性的变量

        #region 计算结果的核心变量自己，从CalculateHeader里移过来；20150908：因为，这样看着方便！不用太麻烦
        //double.NaN是非数字的意思20150908
        //添加double.NaN，是为了验证，这个值是否经过计算；经过计算则为数字！20150908

        /// <summary>
        /// 这个全局变量里包含所有计算结果参数
        /// </summary>
        public static BackPanel.Calculate.CalCar CalculateCar_Glo =new BackPanel.Calculate.CalCar();
        /// <summary>
        /// 这个全局变量里包含所有计算结果参数
        /// </summary>
        public static BackPanel.Calculate.CalChiller CalculateChiller_Glo = new BackPanel.Calculate.CalChiller();

        #endregion


        #region 曲线添加通道1：2在Fun里  20150910
        /// <summary>
        /// 现实的通道,在GloVar里！
        /// </summary>
        public static Dictionary<int, SHHS.UILabs.Channel> Channels;


        #endregion 曲线添加通道1：2在Fun里


        #region 各个采集变量（除了Agilent）的总的声明：20150913
      
        public static double[] Array_WT330;

        public static double[] Array_UT35A;
        #endregion 各个采集变量（除了Agilent）的总的声明：20150913


        #region 获得debug的目录20150923
        /// <summary>
        /// 获得debug的目录20150923
        /// </summary>
        public static string PathDebug;
        #endregion 获得debug的目录20150923

        #region 制冷剂改变信息20151020
        /// <summary>
        /// 制冷剂名称：R22和R134a两种；往数据库里面存与调取
        /// </summary>
        public static string RefName;

        /// <summary>
        /// 离合器电压24/12V选择20151108
        /// 
        /// </summary>
        public static string CarCompressorClutchVoltage;

        /// <summary>
        /// 冷水机组是控制冷却水流量还是出水温度：冷却水流量，或出水温度20151020
        /// </summary>
        public static string ChillerControlParameterName;
        /// <summary>
        /// 压缩机选择开启式还是电动式，冷凝器选择静压控制还是风速控制20190616
        /// </summary>
        public static string CompressorType;
        public static string CondenserType;
        #endregion 制冷剂改变信息20151020


        #region
        public static string textBox9Text;
        public static string textBox11Text;
        public static string textBox27Text;
        public static double MidTemp1_REF;
        public static double MidTemp2_REF;
        /// <summary>
        /// 冷水机组吸气温度
        /// </summary>
        public static double ChillerSuctionTem_Act;

        /// <summary>
        /// 量热器温度
        /// </summary>
        public static double ChillerHeatMeasureTemp_ForMeasure;
        /// <summary>
        /// 蒸发温度
        /// </summary>
        public static double ChillerEvapTem_ForControl;
        #endregion

        //double DischargeSatTem;

        #region 计算过滤系数中间变量
        //计算过滤系数
        public static double A_RefFlowRateCoe = 1;
        public static double A_CoolingCapacityCoe = 1;
        public static double G_RefFlowRateCoe = 1;
        public static double G_CoolingCapacityCoe = 1;

        //计算过滤系数
        public static double A_RefFlowRate_Mid=0;
        public static double A_CoolingCapacity_Mid=0;
        public static double G_RefFlowRate_Mid=0;
        public static double G_CoolingCapacity_Mid=0;

        //Chiller过滤系数
        public static double Chiller_RefFlowRateCoe = 1;
        public static double Chiller_CoolingCapacityCoe = 1;
        public static double Chiller_PowerCoe = 1;
        //中间量
        public static double Chiller_RefFlowRate_Mid = 0;
        public static double Chiller_CoolingCapacity_Mid = 0;
        public static double Chiller_Power_Mid = 0; 

        #endregion 



        #region 报表变量数组20151218
        /// <summary>
        /// Car报表数组
        /// </summary>
        public static double[] DoubleDataForCarReport=new double[39];
        /// <summary>
        /// Chiller报表数组
        /// </summary>
        public static double[] DoubleDataForChillerReport = new double[33];


        #endregion 报表变量数组20151218

        #region  被测机组开始20151223
        /// <summary>
        /// 被测机组是否开启Car
        /// </summary>
        public static bool IsCarChillerOn=false ;
        /// <summary>
        /// 被测机组是否开启Chiller
        /// </summary>
        public static bool IsChillerChillerOn = false;
        #endregion 被测机组开始20151223


        #region 主界面调用Info后传递修改的信息！
        /// <summary>
        /// 信息界面是否从主界面中调用
        /// </summary>
        public static bool IsInfoFromMain = false;
        #endregion

        /// <summary>
        /// 冷水机组info是否，是否调取记录20151226
        /// </summary>
        public static bool IsChillerInfoGetRecord = false;
        /// <summary>
        /// 是选择开启式压缩机还是电动式压缩机20190616
        /// </summary>
        public static bool IsOpenCompressorSelected = false;
        /// <summary>
        /// 冷凝器是选择静压控制还是风速控制20190616
        /// </summary>
        public static bool IsStaticPressureControlSelected = false;

    }
}

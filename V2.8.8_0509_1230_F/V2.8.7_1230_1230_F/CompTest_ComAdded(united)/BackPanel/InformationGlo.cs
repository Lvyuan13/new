using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackPanel
{
    public static class InformationGlo
    {
        /// <summary>
        /// 不同场景（工况）:转移20150917
        /// </summary>
        public enum Senario
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
        public static Senario senario;

        /// <summary>
        /// 辅机开停控制策略场景20150929
        /// </summary>
        public enum SenarioAuxi
        {
            //辅机开庭策略需要20150929
            Car,   //压缩机在Car界面
            Chiller  //冷书记组在Chiller
        }
        /// <summary>
        /// 辅机开停控制策略场景20150929
        /// </summary>
        public static SenarioAuxi senarioAuxi;



        /// <summary>
        /// 从前台INFO填写处获得：20150917：/获取路径信息20150917:这个路径是用来给数据库每次扫描操作用的，所以最重要！20150920
        /// </summary>
        public static string DBPath;

        /// <summary>
        /// 控制器的场景20150929
        /// </summary>
        public enum SenarioControl
        {
            ControlCar,  //压缩机
            ControlChillerWaterTemp, //控制出口水温
            ControlChillerWaterFlowRate //水流量
            
        }
        /// <summary>
        /// 控制器的场景
        /// </summary>
        public static SenarioControl senariocontrol = SenarioControl.ControlCar;



        //当前被测设备名义冷量
        //输入信息20151106
        public static double CurrentExpEquiqNormalCoolingCapacity;

        /// <summary>
        /// 压缩机离合器电压24V/12V
        /// </summary>
        public static double CompressorClutchVoltage = 24;


        #region 变频器开停标志和计时器20151112
        /// <summary>
        /// VFD是否开启
        /// </summary>
        public static bool IsVFDOn = false;
        /// <summary>
        /// VFD时钟
        /// </summary>
        public static int VFDOnTimer = 0;
        /// <summary>
        /// 初始扭矩
        /// </summary>
        public static double Torque_0 = 0;

        public static Queue<double> TorqueList = new Queue<double>(40);
        #endregion

        #region 添加过滤系数
        /// <summary>
        /// 过滤次数
        /// </summary>
        public static int FilterNumber = 0;

        public static int FilterNumber_ForCarCalculate = 0;

        public static int FilterNumber_ForChillerCalculate = 0;
        #endregion

        #region
        /// <summary>
        /// 构建数据库的路径：在后台的！
        /// </summary>
        public static string PathDebugOfBuildDB_Inbackpanel;
        #endregion

        /// <summary>
        /// 离合器吸合
        /// </summary>
        public static bool IsClutchOn = false;
        /// <summary>
        /// 被测压缩机轴直径
        /// </summary>
        public static double CompressorDiameter_FromInfo = 300;

        /// <summary>
        /// 报警是否重置20151125
        /// </summary>
        public static bool IsAlertingReset=false;

        /// <summary>
        /// 从主界面返回，判断是否有错误
        /// </summary>
        public static bool BackFormMainBecauseOfError = false;

        /// <summary>
        /// 冷水机组被测机组是否打开
        /// </summary>
        public static bool IsChillerOn = false;

        #region 信息界面填写后，应该给赋值到控制器！20151224
        /// <summary>
        /// 压缩机转速
        /// </summary>
        public static double CarCompressorRotateSet_ForControl = 1800;

        //public static double CarCompressorDiameter_ForControl=//有的！

        /// <summary>
        /// 冷水机组水流量
        /// </summary>
        public static double ChillerWaterFlowRate_ForControl = 1.4;
        #endregion 信息界面填写后，应该给赋值到控制器！
        
    }
}

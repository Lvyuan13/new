using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace BackPanel
{
    public static class Strategy
    {
        #region PLC报警控制策略20150922：（1）

        /// <summary>
        /// PLC报警控制策略：20150922:PLC(1): Car
        /// </summary>
        public static void StrategyPLCAlarm_ForCar(List<BackPanel.PLCMod.PLCBitVar> PLCDIList, List<BackPanel.PLCMod.PLCBitVar> PLCDOList)
        {
            //急停按钮:20150922
            if (IfDonePLCDIList0)
            { }
            else
            {
                //没有运行则：
                //急停按钮:20150922
                if (PLCDIList[0].IsAlerting == true)
                {
                    //MessageBox.Show("急停报警，点击确认，开始关闭设备！");
                    //MessageBox.Show();
                    #region 添加控制策略
                    for (int i = 0; i < PLCDOList.Count; i++)
                    {
                        BackPanel.PLCMod.SendPLCBitVar(PLCDOList[i], PLCDOList[i].Initial, true);
                    }

                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();
                    //变频器关闭指示器
                    BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList0 = true;

                    //MessageBox.Show("急停报警！已经关闭完相应设备！");
                }
                else
                {
                    //这个里面的东西在重置按钮里面写20150922：即本身不含东西！~
                }
            }
            //水箱低液位保护20150922
            if (IfDonePLCDIList1)
            { }
            else
            {

                //水箱低液位保护20150922
                if (PLCMod.PLCDIList[1].IsAlerting == true)
                {
                    //MessageBox.Show("水箱低液位保护报警，点击确认，开始关闭设备！");
                    #region 添加控制策略
                    for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
                    {
                        BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
                    }
                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();

                    //变频器关闭指示器
                    BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList1 = true;

                    //MessageBox.Show("水箱低液位保护报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //量热器低液位保护20150922
            if (IfDonePLCDIList2)
            { }
            else
            {
                //量热器低液位保护20150922
                if (PLCMod.PLCDIList[2].IsAlerting == true)
                {
                    //MessageBox.Show("水箱低液位保护报警，点击确认，开始关闭设备！");
                    #region 添加控制策略
                    //机组关闭的
                    BackPanel.PLCMod.CarCompressorOn(false);
                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    //设备关闭的
                    BackPanel.PLCMod.WaterSupply(false);
                    BackPanel.PLCMod.VFD(false);
                    BackPanel.Control.ControlLockUT6_ForCar();

                    ////量热器，报警寄存器M1.7，置为0
                    //BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[2], false, true);
                    #endregion 添加控制策略
                    IfDonePLCDIList2 = true;

                    //MessageBox.Show("量热器低液位保护报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //循环泵水流开关20150922
            if (IfDonePLCDIList3)
            { }
            else
            {
                //循环泵水流开关20150922
                if (PLCMod.PLCDIList[3].IsAlerting == true)
                {
                    #region 添加控制策略
                    for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
                    {
                        BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
                    }
                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();

                    //变频器关闭指示器
                    BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList3 = true;

                    //MessageBox.Show("循环泵水流开关报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //高压保护20150922
            if (IfDonePLCDIList4)
            { }
            else
            {
                //高压保护20150922
                if (PLCMod.PLCDIList[4].IsAlerting == true)
                {
                    #region 添加控制策略
                    for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
                    {

                        BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
                    }
                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();
                    //变频器关闭指示器
                    BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList4 = true;

                    //MessageBox.Show("高压保护报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //低压保护20150930
            if (IfDonePLCDIList5)
            { }
            else
            {
                //低压保护20150930
                if (PLCMod.PLCDIList[5].IsAlerting == true)
                {
                    #region 添加控制策略
                    for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
                    {

                        BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
                    }
                    ////M1.0 冷凝机组压缩机前面的开停继电器：在报警的时候置为0：20150930
                    //BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[8], false, true);

                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();

                    //变频器关闭指示器
                    BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList5 = true;

                    //MessageBox.Show("低压保护报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //如果有一个为真，那么，整体判断真假IsTherePLC_Error就为真！
            if (PLCMod.PLCDIList[0].IsAlerting || PLCMod.PLCDIList[1].IsAlerting || PLCMod.PLCDIList[2].IsAlerting || PLCMod.PLCDIList[3].IsAlerting || PLCMod.PLCDIList[4].IsAlerting || PLCMod.PLCDIList[5].IsAlerting)
            {
                IsTherePLC_Error = true;
                BackPanel.InformationGlo.BackFormMainBecauseOfError = true;
            }
        }

        /// <summary>
        /// 报警后响应策略：20151201 CHILLER
        /// </summary>
        /// <param name="PLCDIList"></param>
        /// <param name="PLCDOList"></param>
        public static void StrategyPLCAlarm_ForChiller(List<BackPanel.PLCMod.PLCBitVar> PLCDIList, List<BackPanel.PLCMod.PLCBitVar> PLCDOList)
        {
            //急停按钮:20150922
            if (IfDonePLCDIList0)
            { }
            else
            {
                //没有运行则：
                //急停按钮:20150922
                if (PLCDIList[0].IsAlerting == true)
                {
                    //MessageBox.Show("急停报警，点击确认，开始关闭设备！");
                    //MessageBox.Show();
                    #region 添加控制策略
                    for (int i = 0; i < PLCDOList.Count; i++)
                    {
                        BackPanel.PLCMod.SendPLCBitVar(PLCDOList[i], PLCDOList[i].Initial, true);
                    }

                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();

                    //变频器关闭指示器
                    //BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList0 = true;

                    //MessageBox.Show("急停报警！已经关闭完相应设备！");
                }
                else
                {
                    //这个里面的东西在重置按钮里面写20150922：即本身不含东西！~
                }
            }
            //水箱低液位保护20150922
            if (IfDonePLCDIList1)
            { }
            else
            {

                //水箱低液位保护20150922
                if (PLCMod.PLCDIList[1].IsAlerting == true)
                {
                    //MessageBox.Show("水箱低液位保护报警，点击确认，开始关闭设备！");
                    #region 添加控制策略
                    for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
                    {
                        BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
                    }
                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();

                    //变频器关闭指示器
                    //BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList1 = true;

                    //MessageBox.Show("水箱低液位保护报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //量热器低液位保护20150922
            if (IfDonePLCDIList2)
            { }
            else
            {
                //量热器低液位保护20150922
                if (PLCMod.PLCDIList[2].IsAlerting == true)
                {
                    //MessageBox.Show("水箱低液位保护报警，点击确认，开始关闭设备！");
                    #region 添加控制策略
                    //机组关闭的
                    BackPanel.PLCMod.ChillerOn(false);
                    BackPanel.Control.ControlLockUT1UT2_ForCar();

                    //设备关闭的
                    BackPanel.PLCMod.WaterSupply(false);
                    BackPanel.Control.ControlLockUT6_ForCar();

                    ////量热器，报警寄存器M1.7，置为0
                    //BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[2], false, true);
                    #endregion 添加控制策略
                    IfDonePLCDIList2 = true;

                    //MessageBox.Show("量热器低液位保护报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //循环泵水流开关20150922
            if (IfDonePLCDIList3)
            { }
            else
            {
                //循环泵水流开关20150922
                if (PLCMod.PLCDIList[3].IsAlerting == true)
                {
                    #region 添加控制策略
                    for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
                    {
                        BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
                    }
                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();

                    //变频器关闭指示器
                    //BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList3 = true;

                    //MessageBox.Show("循环泵水流开关报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //高压保护20150922
            if (IfDonePLCDIList4)
            { }
            else
            {
                //高压保护20150922
                if (PLCMod.PLCDIList[4].IsAlerting == true)
                {
                    #region 添加控制策略
                    for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
                    {

                        BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
                    }
                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();
                    //变频器关闭指示器
                    //BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList4 = true;

                    //MessageBox.Show("高压保护报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //低压保护20150930
            if (IfDonePLCDIList5)
            { }
            else
            {
                //低压保护20150930
                if (PLCMod.PLCDIList[5].IsAlerting == true)
                {
                    #region 添加控制策略
                    for (int i = 0; i < BackPanel.PLCMod.PLCDOList.Count; i++)
                    {

                        BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[i], BackPanel.PLCMod.PLCDOList[i].Initial, true);
                    }
                    ////M1.0 冷凝机组压缩机前面的开停继电器：在报警的时候置为0：20150930
                    //BackPanel.PLCMod.SendPLCBitVar(BackPanel.PLCMod.PLCDOList[8], false, true);

                    BackPanel.Control.ControlLockUT1UT2_ForCar();
                    BackPanel.Control.ControlLockUT6_ForCar();

                    //变频器关闭指示器
                    //BackPanel.InformationGlo.IsVFDOn = false;
                    #endregion 添加控制策略
                    IfDonePLCDIList5 = true;

                    //MessageBox.Show("低压保护报警！已经关闭完相应设备！");
                }
                else
                { }
            }
            //如果有一个为真，那么，整体判断真假IsTherePLC_Error就为真！
            if (PLCMod.PLCDIList[0].IsAlerting || PLCMod.PLCDIList[1].IsAlerting || PLCMod.PLCDIList[2].IsAlerting || PLCMod.PLCDIList[3].IsAlerting || PLCMod.PLCDIList[4].IsAlerting || PLCMod.PLCDIList[5].IsAlerting)
            {
                IsTherePLC_Error = true;
                BackPanel.InformationGlo.BackFormMainBecauseOfError = true;
            }
        }



        /// <summary>
        /// PLC控制一次性的变量初始化：20150922:在开关界面和复位时：PLC（2）
        /// </summary>
        public static void StrategyPLCIfVarIni()
        {
            IfDonePLCDIList0 = false;
            IfDonePLCDIList1 = false;
            IfDonePLCDIList2 = false;
            IfDonePLCDIList3 = false;
            IfDonePLCDIList4 = false;


            IsTherePLC_Error = false;

            IfDonePLCDIPlay = false;
        }

        /// <summary>
        /// 急停按钮报警策略运行一次，就可以：值为true时，为运行了；为false时，为没有运行。
        /// </summary>
        public static bool IfDonePLCDIList0 = false;
        public static bool IfDonePLCDIList1 = false;
        public static bool IfDonePLCDIList2 = false;
        public static bool IfDonePLCDIList3 = false;
        public static bool IfDonePLCDIList4 = false;
        public static bool IfDonePLCDIList5 = false;

        /// <summary>
        /// 只要有一个报警，这个值就为真！只有正常时，为假
        /// </summary>
        public static bool IsTherePLC_Error = false;


        /// <summary>
        /// 主钟中的，的PLC显示策略是否显示过，只要显示一次就可以了
        /// </summary>
        public static bool IfDonePLCDIPlay = false;

        /// <summary>
        /// PLC报警复位按钮，的PLC控制策略：同时把所有控制变量初始化：20150922
        /// </summary>
        public static void StrategyPLCReset()
        {
            StrategyPLCIfVarIni();
            for (int i = 0; i < PLCMod.PLCDIList.Count; i++)
            {
                PLCMod.PLCDIList[i].IsAlerting = false;
            }

            #region PLC的操作：20150922留坑

            //PLC低压寄存器复位！强制的20150928改:20150930
            BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[5], false, true);
            //量热器低液位寄存器复位！
            BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[2],false,true);


            #endregion PLC的操作20150922留坑
        }
        #endregion PLC报警控制策略20150922



        #region 中间闪屏，依据不同场景来控制PLC20150923：（2）
        /// <summary>
        /// 中间闪屏，依据不同场景来控制PLCPLC20150923
        /// </summary>
        public static void StrategyPLCBeforeMain()
        {
            switch (BackPanel.InformationGlo.senario)
            {
                //汽车空调制冷压缩机制冷
                case BackPanel.InformationGlo.Senario.CarCooling:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;


                //汽车空调压缩机噪声试验
                case BackPanel.InformationGlo.Senario.CarNoise:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：名义工况
                case BackPanel.InformationGlo.Senario.ChillerNormialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：名义工况，冷却水流量
                //case BackPanel.InformationGlo.Senario.ChillerNormialConditionWaterFlow:

                //    #region 不同场景的PLC控制策略：20150923

                //    #endregion

                    //break;
                //水冷压缩机组试验：部分负荷试验
                case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：最大负荷试验
                case BackPanel.InformationGlo.Senario.ChillerMaxCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：变工况试验
                case BackPanel.InformationGlo.Senario.ChillerChangCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
            }
        }

        #endregion 中间闪屏，依据不同场景来控制PLC20150923：（2）


        #region 菜单“实验设备”，“机组”开停控制策略：20150923：（3）
        /// <summary>
        /// 试验设备开始：20150923
        /// </summary>
        public static void StrategyPLCExperimentEquipStart()
        {
            switch (BackPanel.InformationGlo.senario)
            {
                //汽车空调制冷压缩机制冷
                case BackPanel.InformationGlo.Senario.CarCooling:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;


                //汽车空调压缩机噪声试验
                case BackPanel.InformationGlo.Senario.CarNoise:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：名义工况，出水温度
                case BackPanel.InformationGlo.Senario.ChillerNormialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                ////水冷压缩机组试验：名义工况，冷却水流量
                //case BackPanel.InformationGlo.Senario.ChillerNormialConditionWaterFlow:

                //    #region 不同场景的PLC控制策略：20150923

                //    #endregion

                //    break;
                //水冷压缩机组试验：部分负荷试验
                case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：最大负荷试验
                case BackPanel.InformationGlo.Senario.ChillerMaxCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：变工况试验
                case BackPanel.InformationGlo.Senario.ChillerChangCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
            }
        }
        /// <summary>
        /// 实验设备停止：20150923
        /// </summary>
        public static void StrategyPLCExperimentEquipStop()
        {
            switch (BackPanel.InformationGlo.senario)
            {
                //汽车空调制冷压缩机制冷
                case BackPanel.InformationGlo.Senario.CarCooling:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;


                //汽车空调压缩机噪声试验
                case BackPanel.InformationGlo.Senario.CarNoise:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                ////水冷压缩机组试验：名义工况，出水温度
                //case BackPanel.InformationGlo.Senario.ChillerNormialConditionTemp:
                //    #region 不同场景的PLC控制策略：20150923

                //    #endregion

                //    break;
                //水冷压缩机组试验：名义工况20151129改
                case BackPanel.InformationGlo.Senario.ChillerNormialCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：部分负荷试验
                case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：最大负荷试验
                case BackPanel.InformationGlo.Senario.ChillerMaxCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：变工况试验
                case BackPanel.InformationGlo.Senario.ChillerChangCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
            }
        }
        /// <summary>
        /// 被测机组开始：201509232
        /// </summary>
        public static void StrategyPLCChillerStart()
        {
            switch (BackPanel.InformationGlo.senario)
            {
                //汽车空调制冷压缩机制冷
                case BackPanel.InformationGlo.Senario.CarCooling:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;


                //汽车空调压缩机噪声试验
                case BackPanel.InformationGlo.Senario.CarNoise:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：名义工况:20151129改
                case BackPanel.InformationGlo.Senario.ChillerNormialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                ////水冷压缩机组试验：名义工况，冷却水流量
                //case BackPanel.InformationGlo.Senario.ChillerNormialConditionWaterFlow:

                //    #region 不同场景的PLC控制策略：20150923

                //    #endregion

                //    break;
                //水冷压缩机组试验：部分负荷试验
                case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：最大负荷试验
                case BackPanel.InformationGlo.Senario.ChillerMaxCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：变工况试验
                case BackPanel.InformationGlo.Senario.ChillerChangCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
            }
        }
        /// <summary>
        /// 被测机组停止：201509232
        /// </summary>
        public static void StrategyPLCChillerStop()
        {
            switch (BackPanel.InformationGlo.senario)
            {
                //汽车空调制冷压缩机制冷
                case BackPanel.InformationGlo.Senario.CarCooling:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;


                //汽车空调压缩机噪声试验
                case BackPanel.InformationGlo.Senario.CarNoise:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：名义工况
                case BackPanel.InformationGlo.Senario.ChillerNormialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：名义工况，冷却水流量
                //case BackPanel.InformationGlo.Senario.ChillerNormialConditionWaterFlow:

                //    #region 不同场景的PLC控制策略：20150923

                //    #endregion

                //    break;
                //水冷压缩机组试验：部分负荷试验
                case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：最大负荷试验
                case BackPanel.InformationGlo.Senario.ChillerMaxCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：变工况试验
                case BackPanel.InformationGlo.Senario.ChillerChangCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
            }
        }
        #endregion 菜单“实验设备”，“机组”开停控制策略：20150923：（3）


        #region 从主界面退出后菜单改为“退出试验”：20150923(4)
        /// <summary>
        /// 从主界面退出后菜单改为“退出试验”:true是返回；false是原始的
        /// </summary>
        public static bool IsReturnFromMain = false;

        #endregion 从主界面退出后菜单改为“退出试验”：20150923


        #region 最后退出试验的时候PLC的控制策略20150923（5）
        /// <summary>
        /// 最后退出试验的时候PLC的控制策略20150923：把PLC全部关闭！
        /// </summary>
        public static void StrategyPLCExit()
        {
            switch (BackPanel.InformationGlo.senario)
            {
                //汽车空调制冷压缩机制冷
                case BackPanel.InformationGlo.Senario.CarCooling:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion
                    break;


                //汽车空调压缩机噪声试验
                case BackPanel.InformationGlo.Senario.CarNoise:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：名义工况，出水温度
                case BackPanel.InformationGlo.Senario.ChillerNormialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                ////水冷压缩机组试验：名义工况，冷却水流量
                //case BackPanel.InformationGlo.Senario.ChillerNormialConditionWaterFlow:

                //    #region 不同场景的PLC控制策略：20150923

                //    #endregion

                //    break;
                //水冷压缩机组试验：部分负荷试验
                case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：最大负荷试验
                case BackPanel.InformationGlo.Senario.ChillerMaxCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
                //水冷压缩机组试验：变工况试验
                case BackPanel.InformationGlo.Senario.ChillerChangCondition:

                    #region 不同场景的PLC控制策略：20150923

                    #endregion

                    break;
            }

        }
        #endregion

        #region 辅助设备开停控制策略20150923（6）：20150929改
        //辅助设备开
        public static void StrategyPLCAuxiliaryStart()
        {
            #region PLC命令！
            switch (BackPanel.InformationGlo.senarioAuxi)
            {
                //压缩机辅机开始
                case BackPanel.InformationGlo.SenarioAuxi.Car:

                    #region 不同场景的PLC控制策略：20150929

                    #endregion
                    break;


                //制冷机组辅机开始
                case BackPanel.InformationGlo.SenarioAuxi.Chiller:

                    #region 不同场景的PLC控制策略：20150929

                    #endregion
                    break;

            }
            #endregion
        }
        //辅助设备停
        public static void StrategyPLCAuxiliaryStop()
        {
            #region PLC命令！
            switch (BackPanel.InformationGlo.senarioAuxi)
            {
                //压缩机辅机停止
                case BackPanel.InformationGlo.SenarioAuxi.Car:

                    #region 不同场景的PLC控制策略：20150929

                    #endregion
                    break;


                //制冷机组辅机停止
                case BackPanel.InformationGlo.SenarioAuxi.Chiller:

                    #region 不同场景的PLC控制策略：20150929

                    #endregion
                    break;

            }
            #endregion
        }
        #endregion 辅助设备开停控制策略20150923（6）

        #region 中间闪屏根据不同场景来控制UT35A：（7）20150923
        /// <summary>
        /// 中间闪屏根据不同场景来控制UT35A：（7）20150923
        /// </summary>
        //public static void StrategyUT35ABeforeMain()
        //{
        //    switch (BackPanel.InformationGlo.senario)
        //    {
        //        //汽车空调制冷压缩机制冷
        //        case BackPanel.InformationGlo.Senario.CarCooling:

        //            #region 不同场景的PLC控制策略：20150923

        //            #endregion

        //            break;


        //        //汽车空调压缩机噪声试验
        //        case BackPanel.InformationGlo.Senario.CarNoise:

        //            #region 不同场景的PLC控制策略：20150923

        //            #endregion

        //            break;
        //        //水冷压缩机组试验：名义工况，出水温度
        //        case BackPanel.InformationGlo.Senario.ChillerNormialConditionTemp:
        //            #region 不同场景的PLC控制策略：20150923

        //            #endregion

        //            break;
        //        //水冷压缩机组试验：名义工况，冷却水流量
        //        case BackPanel.InformationGlo.Senario.ChillerNormialConditionWaterFlow:

        //            #region 不同场景的PLC控制策略：20150923

        //            #endregion

        //            break;
        //        //水冷压缩机组试验：部分负荷试验
        //        case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
        //            #region 不同场景的PLC控制策略：20150923

        //            #endregion

        //            break;
        //        //水冷压缩机组试验：最大负荷试验
        //        case BackPanel.InformationGlo.Senario.ChillerMaxCondition:

        //            #region 不同场景的PLC控制策略：20150923

        //            #endregion

        //            break;
        //        //水冷压缩机组试验：变工况试验
        //        case BackPanel.InformationGlo.Senario.ChillerChangCondition:

        //            #region 不同场景的PLC控制策略：20150923

        //            #endregion

        //            break;
        //    }

        //}
        #endregion 中间闪屏根据不同场景来控制UT35A：（7）20150923
    }
}

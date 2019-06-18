using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackPanel
{

    public static partial class PLCMod
    {
        /// <summary>
        ///  根据数据库构造DIDOList
        /// </summary>
        public static void BuildPLCDIDOListFromDB(List<PLCBitVar> PLCDIDOList, string DBPath, string TableName)
        {
            PLCDIDOList.Clear();
            List<string[]> StringsListFromDB = Utility.MDBComm.ReadAllStringFromTable(DBPath, TableName);

            for (int i = 0; i < StringsListFromDB.Count; i++)
            {
                PLCBitVar PLCDIDOVar = new PLCBitVar();
                PLCDIDOVar.Name = StringsListFromDB[i][0];
                PLCDIDOVar.MemoryArea = StringsListFromDB[i][1];
                PLCDIDOVar.Address = StringsListFromDB[i][2];
                PLCDIDOVar.Initial = (StringsListFromDB[i][3] == "false") ? false : true;
                PLCDIDOVar.IsConverse = (StringsListFromDB[i][4] == "false") ? false : true;


                PLCDIDOList.Add(PLCDIDOVar);
            }
            PLCDOList.TrimExcess();
        }

        /// <summary>
        /// 强制通讯，PLC的通讯:赋值：20150922使用
        /// </summary>
        /// <param name="plcbitvar"></param>
        /// <param name="Value"></param>
        /// <param name="IsForce"></param>
        public static void SendPLCBitVar(PLCBitVar  plcbitvar, bool Value, bool IsForce)
        {
            if (IsForce)
            {
                SendPLC(plcbitvar.MemoryArea, plcbitvar.Address, Value, plcbitvar.IsConverse);
                plcbitvar.CurrentValue = Value;
            }
            else
            {
                if (plcbitvar.CurrentValue == Value)
                {

                }
                else
                {
                    SendPLC(plcbitvar.MemoryArea, plcbitvar.Address, Value, plcbitvar.IsConverse);
                    plcbitvar.CurrentValue = Value;
                }
            }

        }

        /// <summary>
        /// 读取20150914 :通过PLCBitVar读取！返回状态
        /// </summary>
        /// <param name="plcbitvar"></param>
        /// <returns></returns>

        public static bool ReadPLCBitVar(PLCBitVar plcbitvar)
        {
            return ReadPLC(plcbitvar.MemoryArea, plcbitvar.Address, plcbitvar.IsConverse);

        }


        #region 初始化场景和各种场景
        /// <summary>
        /// 初始化PLC函数
        /// </summary>
        //public static void Initiate(List<PLCBitVar> PLCDIDOList, string DBPath, string TableName)
        //{
        //    BuildPLCDIDOListFromDB(PLCDIDOList, DBPath, TableName);
        //}
        ///// <summary>
        ///// 初始化
        ///// </summary>
        //public static void InitialSend()
        //{
        //    //填写需要的各个PLC设置初值，并强制通讯
        //    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[0],PLCMod.PLCDOList[0].Initial,true);
        //    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[1], PLCMod.PLCDOList[1].Initial, true);
        //    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[2], PLCMod.PLCDOList[2].Initial, true);
        //    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[3], PLCMod.PLCDOList[3].Initial, true);

        //}

        /// <summary>
        /// 汽车空调压缩机制冷试验
        /// </summary>
        public static void Car_Cooling()
        {

        }

        /// <summary>
        /// 汽车空调压缩机噪声试验
        /// </summary>
        public static void Car_Noise()
        {

        }

        /// <summary>
        /// 漏热量试验
        /// </summary>
        public static void CoolContainer_HeatDissipation()
        {

        }
        /// <summary>
        /// 漏气量试验
        /// </summary>
        public static void CoolContainer_GasDissipation()
        {

        }
        /// <summary>
        /// 淋雨试验
        /// </summary>
        public static void CoolContainer_Rain()
        {

        }
        /// <summary>
        /// 手动试验
        /// </summary>
        public static void Manual_Test()
        {

        }

        #endregion



        #region 报警监视和复位

        /// <summary>
        /// 监控PLC报警函数：20150916
        /// </summary>
        /// <param name="plclist"></param>
        public static void PLCAlter(List<PLCBitVar> plclist)
        {
            for (int i = 0; i < plclist.Count; i++)
            {
                //对于循环开关；要在循环泵开启的时候，再检测
                if(i==3)
                {

                    //if (ReadPLC(BackPanel.PLCMod.PLCDOList[7].MemoryArea, BackPanel.PLCMod.PLCDOList[7].Address, BackPanel.PLCMod.PLCDOList[7].IsConverse))
                    //{}
                    //else
                    //{}
                    //对于循环开关；要在循环泵开启的时候，再检测
                    
                    //ReadPLC("M", "0.7", false);
                    if (ReadPLC("M", "0.7", false))
                    {
                        if (ReadPLC(plclist[i].MemoryArea, plclist[i].Address, plclist[i].IsConverse))
                        {
                            plclist[i].IsAlerting = true;
                            plclist[i].AlertTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            //在ReSet里面，重置；同时，发给PLC；20150922
                        }
                    }
                    else
                    { }
                }
                else
                {
                    //20150922
                    if (ReadPLC(plclist[i].MemoryArea, plclist[i].Address, plclist[i].IsConverse))
                    {
                        plclist[i].IsAlerting = true;
                        plclist[i].AlertTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        //在ReSet里面，重置；同时，发给PLC；20150922
                    }
                }
               
            }
        }


        #endregion


        #region 最底层代码

        /// <summary>
        /// 发送PLC命令
        /// </summary>
        /// <param name="addressAreas"></param>
        /// <param name="addresses"></param>
        /// <param name="value"></param>
        public static void SendPLC(string addressAreas, string addresses, bool value, bool IsConverse)
        {
            //判断是否模拟：20150915

            //if(UtilityMod_Header.IsDemo)
            //{
            //}
            //20151103 ：PLC通讯屏蔽
            //if (UtilityMod_Header.IsDemo)
            //{
            //}
            if (UtilityMod_Header.IsDemo)
            {
            }
            else
            {
                //如果是正常的，则输入true，就是true；否则是false
                if (!IsConverse)
                {
                    UtilityMod_Header.PLC_COM2.TFSet(addressAreas, addresses, value);
                }
                else
                {
                    UtilityMod_Header.PLC_COM2.TFSet(addressAreas, addresses, !value);
                }
            }

            //UtilityMod_Header.PLC_COM2.TFSet(,,);
            //UtilityMod_Header.PLC_COM2.TFRead();
        }
        /// <summary>
        /// 读出现有的plc状态，模拟状态返回的报警与否为true:20150922改
        /// </summary>
        /// <param name="addressAreas"></param>
        /// <param name="addresses"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ReadPLC(string addressAreas, string addresses, bool IsConverse)
        {
            //if(UtilityMod_Header.IsDemo)
            //{
            //    //如果是模拟，都返回真
            //    return true;
            //}
            //PLC屏蔽：20151103
            if (UtilityMod_Header.IsDemo)
            {
                //如果是模拟，都返回真
                return false;

            }
            else
            {
                //20150922添加反向的作用：真，则返回值的逆；假，则返回值的真
                if (IsConverse)
                {
                    //最底层读取
                    return !UtilityMod_Header.PLC_COM2.TFRead(addressAreas, addresses);
                }
                else
                {
                    //最底层读取
                    return UtilityMod_Header.PLC_COM2.TFRead(addressAreas, addresses);
                }
            }


            //return true;
        }
        #endregion


        #region PLC报警重置函数20150922
        /// <summary>
        /// 报警复位:20150922改
        /// </summary>
        public static void DIAlterMonitorRecover()
        {

            //for (int i = 0; i < PLCMod.PLCAlertList.Count; i++)
            //{
            //    PLCMod.PLCAlertList[i].IsAlerting = false;
            //    PLCMod.PLCAlertList[i].AlertTimeStamp = "";

            //}

            for (int i = 0; i < PLCMod.PLCDIList.Count; i++)
            {
                PLCMod.PLCDIList[i].IsAlerting = false;
                PLCMod.PLCDIList[i].AlertTimeStamp = "";
            }
            //PLCMod.PLCAlertList.Clear();
            //设置把有上升沿的PLC点位复位
            #region //设置把有上升沿的PLC点位复位20150922
            #endregion  设置把有上升沿的PLC点位复位20150922
        }
        #endregion PLC报警重置函数20150922


        #region 现场添加的PLC策略

        /// <summary>
        /// 水箱辅助系统启停
        /// 包括：
        /// 循环泵，水箱机组，水箱加热器及其控制输出
        /// 
        /// </summary>
        /// <param name="IsOn"></param>
        public static void AuxiliaryWaterSystem(bool IsOn)
        {

            PLCMod.SendPLCBitVar(PLCDOList[4], IsOn, true);  //UT5水箱出水温度控制器输入到水箱加热器 继电器K5
            //移向调压器

            if (IsOn == true)
            {
                PLCMod.SendPLCBitVar(PLCMod.PLCDOList[7], true, false);//循环泵
                PLCMod.SendPLCBitVar(PLCDOList[17], true, true); //压缩冷凝机组-电磁阀
                PLCMod.SendPLCBitVar(PLCDOList[9], true, true); //压缩冷凝机组-冷凝风机1
                PLCMod.SendPLCBitVar(PLCDOList[16], true, true); //压缩冷凝机组-冷凝风机2
                PLCMod.SendPLCBitVar(PLCDOList[8], true, true); //压缩冷凝机组-压缩机
                PLCMod.SendPLCBitVar(PLCDOList[18], true, true); //压缩冷凝机组-压缩机加热器

                PLCMod.SendPLCBitVar(PLCDOList[19], true, true); //水箱加热器
            }
            else
            {
                PLCMod.SendPLCBitVar(PLCDOList[19], false, true); //水箱加热器

                PLCMod.SendPLCBitVar(PLCDOList[8], false, true); //压缩冷凝机组-压缩机
                PLCMod.SendPLCBitVar(PLCDOList[18], false, true); //压缩冷凝机组-压缩机加热器
                PLCMod.SendPLCBitVar(PLCDOList[9], false, true); //压缩冷凝机组-冷凝风机1
                PLCMod.SendPLCBitVar(PLCDOList[16], false, true); //压缩冷凝机组-冷凝风机2
                PLCMod.SendPLCBitVar(PLCDOList[17], false, true); //压缩冷凝机组-电磁阀

                PLCMod.SendPLCBitVar(PLCMod.PLCDOList[7], false, false);//循环泵
            }


        }

        /// <summary>
        /// 供水泵开启，
        /// </summary>
        /// <param name="IsOn"></param>
        public static void WaterSupply(bool IsOn)
        {
            PLCMod.SendPLCBitVar(PLCDOList[6], IsOn, true); //开供水泵


        }
        /// <summary>
        /// 变频器开启；20151112
        /// </summary>
        /// <param name="IsOn"></param>
        public static void VFD(bool IsOn)
        {
            PLCMod.SendPLCBitVar(PLCDOList[27], IsOn, true); //被测压缩机驱动电机变频器

            if (IsOn)
            {
                BackPanel.InformationGlo.IsVFDOn = true;
            }
            else
            {
                BackPanel.InformationGlo.IsVFDOn = false;
            }
        }



        /// <summary>
        /// 根据选择压缩机或者机组
        /// 开启或关闭
        /// 水路电磁阀，制冷剂路电磁阀，控制器输入输出切换KA
        /// </summary>
        /// <param name="TF">true-选择压缩机;false-选择机组</param>
        public static void IsTestCarComp(bool TF)
        {
            //0.测试机组或者压缩机都要开启
            UTInOutCommon(true);

            //1.测试压缩机或机组互斥的设备
            SVAndUTInOutSwitch(TF);

            //2.测试压缩机需要开启K15吸合KM7给变频器供电
            PLCMod.SendPLCBitVar(PLCDOList[20], TF, true);

            ////3.压缩机离合器供电选择24V/12V选择20151108
            //if(BackPanel.InformationGlo.CompressorClutchVoltage==24)
            //{
            //    PLCMod.SendPLCBitVar(PLCDOList[31], !TF, true);
            //}
            //else
            //{
            //    PLCMod.SendPLCBitVar(PLCDOList[31], TF, true);
            //}

            //单独一个函数

        }

        /// <summary>
        /// 机组和压缩机通用的控制器输出继电器开启/关闭
        /// 水箱加热器的输出开关跟水箱加热器一起启动，在AuxiliaryWaterSystem中
        /// </summary>
        /// <param name="IsOn"></param>
        public static void UTInOutCommon(bool IsOn)
        {
            PLCMod.SendPLCBitVar(PLCDOList[0], IsOn, true);  //UT1吸气压力控制器输出到富士金电动阀继电器K1
            PLCMod.SendPLCBitVar(PLCDOList[1], IsOn, true);  //UT2吸气温度控制器输出到量热器加热器继电器K2
            PLCMod.SendPLCBitVar(PLCDOList[35], IsOn, true); //富士金电动调节阀供电电源
        }
        /// <summary>
        /// 压缩机离合器供电选择24V/12V选择20151108
        /// </summary>
        public static void CarCompressorClutchVoltage()
        {
            //3.压缩机离合器供电选择24V/12V选择20151108
            if (BackPanel.InformationGlo.CompressorClutchVoltage == 24)
            {
                PLCMod.SendPLCBitVar(PLCDOList[31], false, true);
            }
            else
            {
                PLCMod.SendPLCBitVar(PLCDOList[31], true, true);
            }

        }

        /// <summary>
        /// 水路电磁阀,制冷剂转换电磁阀，以及需要切换的控制器输入输出
        /// </summary>
        /// <param name="IsTestCar"></param>
        public static void SVAndUTInOutSwitch(bool IsTestCar)
        {
            PLCMod.SendPLCBitVar(PLCDOList[28], IsTestCar, true); //水路转换电磁阀;
            PLCMod.SendPLCBitVar(PLCDOList[29], IsTestCar, true); //制冷剂路转换电磁阀1
            PLCMod.SendPLCBitVar(PLCDOList[30], !IsTestCar, true); //制冷剂路转换电磁阀2

            PLCMod.SendPLCBitVar(PLCDOList[2], IsTestCar, true); //UT3排气供液压力控制器输出继电器K3给到K28，压缩机用
            PLCMod.SendPLCBitVar(PLCDOList[3], !IsTestCar, true); //UT4出口水温/水流量控制器输出继电器K4给到K28，机组用
            //控制器输出路的继电器
            PLCMod.SendPLCBitVar(PLCDOList[34], !IsTestCar, true); //UT3UT4输出切换，继电器K28常闭在UT3输出到卡尔调节阀，压缩机用;
            //输入给控制器的继电器
            PLCMod.SendPLCBitVar(PLCDOList[33], !IsTestCar, true); //UT5输入切换继电器K27常闭在水箱出水温度传感器上，压缩机用;

            PLCMod.SendPLCBitVar(PLCDOList[5], IsTestCar, true); //UT6压缩机转速控制器输出K6到变频器输入，压缩机用

        }

        //PLClilang
        /// <summary>
        /// 被测机组实验开停
        /// </summary>
        /// <param name="IsOn"></param>
        public static void ChillerOn(bool IsOn)
        {
            PLCMod.SendPLCBitVar(PLCDOList[43], IsOn, true); //被测冷凝机组电磁阀

            PLCMod.SendPLCBitVar(PLCDOList[23], IsOn, true); //被测冷凝机组

            //量热器加热器1，需要添加
            PLCMod.SendPLCBitVar(PLCDOList[24], IsOn, true);

            //量热器加热器2，在额定功率为8000W以上，开启20151106
            if (BackPanel.InformationGlo.CurrentExpEquiqNormalCoolingCapacity*1000 >= 7000)
            {
                PLCMod.SendPLCBitVar(PLCDOList[25], IsOn, true);
            }

            //为防止，在主界面中改变信息，而管不了第二个量热器
            if(!IsOn)
            {
                PLCMod.SendPLCBitVar(PLCDOList[25], false, true);
            }

            BackPanel.InformationGlo.IsChillerOn = IsOn;
        }


        public static void CarCompressorOn(bool IsOn)
        {


            //量热器加热器1，需要添加
            PLCMod.SendPLCBitVar(PLCDOList[24], IsOn, true);

            //量热器加热器2，在额定功率为8000W以上，开启20151106
            if (BackPanel.InformationGlo.CurrentExpEquiqNormalCoolingCapacity*1000 >= 7000)
            {
                PLCMod.SendPLCBitVar(PLCDOList[25], IsOn, true);
            }
            if (BackPanel.InformationGlo.CurrentExpEquiqNormalCoolingCapacity*1000 < 7000)
            {
                PLCMod.SendPLCBitVar(PLCDOList[25], false, true);
            }

            if (IsOn)
            {
                //PLCMod.SendPLCBitVar(PLCDOList[27], IsOn, true); //被测压缩机驱动电机变频器
                PLCMod.SendPLCBitVar(PLCDOList[31], IsOn, true); //离合器
                BackPanel.InformationGlo.IsClutchOn = true;
            }
            else
            {
                PLCMod.SendPLCBitVar(PLCDOList[31], IsOn, true); //离合器
                //PLCMod.SendPLCBitVar(PLCDOList[27], IsOn, true); //被测压缩机驱动电机变频器
                BackPanel.InformationGlo.IsClutchOn = false;

            }

            //20151106压缩机机箱加热器
            IsStartBoxHeater = IsOn;

        }


        ///// <summary>
        ///// 机组和压缩机都需要开启的设备
        ///// 包括：外机组，供水泵
        ///// </summary>
        ///// <param name="IsOn"></param>
        //public static void CommonStartEquip(bool IsOn)
        //{
        //    ////20151104开循环泵
        //    //PLCMod.SendPLCBitVar(PLCMod.PLCDOList[7], true, false);


        //    PLCMod.SendPLCBitVar(PLCDOList[6], IsOn, true); //开供水泵
        //    if (IsOn == true)
        //    {
        //        PLCMod.SendPLCBitVar(PLCDOList[17], true, true); //压缩冷凝机组-电磁阀
        //        PLCMod.SendPLCBitVar(PLCDOList[9], true, true); //压缩冷凝机组-冷凝风机1
        //        PLCMod.SendPLCBitVar(PLCDOList[16], true, true); //压缩冷凝机组-冷凝风机2
        //        PLCMod.SendPLCBitVar(PLCDOList[8], true, true); //压缩冷凝机组-压缩机

        //        PLCMod.SendPLCBitVar(PLCDOList[18], true, true); //压缩冷凝机组-压缩机加热器


        //    }
        //    else
        //    {
        //        PLCMod.SendPLCBitVar(PLCDOList[8], false, true); //压缩冷凝机组-压缩机
        //        PLCMod.SendPLCBitVar(PLCDOList[18], false, true); //压缩冷凝机组-压缩机加热器

        //        PLCMod.SendPLCBitVar(PLCDOList[9], false, true); //压缩冷凝机组-冷凝风机1
        //        PLCMod.SendPLCBitVar(PLCDOList[16], false, true); //压缩冷凝机组-冷凝风机2
        //        PLCMod.SendPLCBitVar(PLCDOList[17], false, true); //压缩冷凝机组-电磁阀
        //    }

        //    PLCMod.SendPLCBitVar(PLCDOList[19], IsOn, true); //水箱加热器

        //    PLCMod.SendPLCBitVar(PLCDOList[0], IsOn, true); //UT1,吸气压力控制器输出到富士金电动阀继电器K1
        //    PLCMod.SendPLCBitVar(PLCDOList[1], IsOn, true); //UT2,吸气温度控制器输出到量热器加热器继电器K2
        //    PLCMod.SendPLCBitVar(PLCDOList[4], IsOn, true); //UT5，水箱出水温度控制器输入到水箱加热器 继电器 K5

        //    PLCMod.SendPLCBitVar(PLCDOList[35], IsOn, true); //富士金电动调节阀供电电源


        //}


        public static void CarStartEquip(bool IsOn)
        {
            PLCMod.SendPLCBitVar(PLCDOList[28], true, true); //水路转换电磁阀
            PLCMod.SendPLCBitVar(PLCDOList[29], true, true); //制冷剂路转换电磁阀1
            PLCMod.SendPLCBitVar(PLCDOList[30], false, true); //制冷剂路转换电磁阀2

            PLCMod.SendPLCBitVar(PLCDOList[2], IsOn, true); //UT3排气供液压力输出继电器K3输出给到K28
            PLCMod.SendPLCBitVar(PLCDOList[34], false, true); //UT3UT4切换，继电器K28常闭在UT3输出到卡尔调节阀 

            PLCMod.SendPLCBitVar(PLCDOList[5], IsOn, true); //UT6压缩机转速控制器输出到 变频器K6

            PLCMod.SendPLCBitVar(PLCDOList[33], false, true); //UT5输入切换，继电器K27常闭在 水箱出水温度传感器上

        }


        public static void ChillerStartEquip(bool IsOn)
        {
            PLCMod.SendPLCBitVar(PLCDOList[28], false, true); //水路转换电磁阀
            PLCMod.SendPLCBitVar(PLCDOList[29], false, true); //制冷剂路转换电磁阀1
            PLCMod.SendPLCBitVar(PLCDOList[30], true, true); //制冷剂路转换电磁阀2

            PLCMod.SendPLCBitVar(PLCDOList[4], true, true); //水箱加热器控制UT5切换


            PLCMod.SendPLCBitVar(PLCDOList[3], IsOn, true); //UT4出口水温，或水流量控制器，输出到卡尔调节阀 K28
            PLCMod.SendPLCBitVar(PLCDOList[34], true, true); //UT3UT4切换，继电器K28 ，切换到UT4上

            PLCMod.SendPLCBitVar(PLCDOList[5], false, true); //UT6压缩机转速控制器输出到 变频器K6

            PLCMod.SendPLCBitVar(PLCDOList[33], true, true); //UT5输入切换，继电器K27,切换到进口水温传感器
        }


        public static void CloseALLEquipment()
        {
            for (int i = 0; i < PLCDOList.Count; i++)
            {
                SendPLCBitVar(PLCDOList[i], false, true);

            }

        }

        //20151106压缩机机箱加热器
        public static bool IsStartBoxHeater = false;
        public static void CompressorBoxHeater(double CurrentBoxTemperature)
        {

            if (BackPanel.InformationGlo.senario == BackPanel.InformationGlo.Senario.CarCooling)
            {
                if (IsStartBoxHeater)
                {
                    if (CurrentBoxTemperature >= 67)
                    {
                        PLCMod.SendPLCBitVar(PLCDOList[22], false, true); //压缩机加热器器1
                    }
                    if (CurrentBoxTemperature <= 60)
                    {
                        PLCMod.SendPLCBitVar(PLCDOList[22], true, true); //压缩机加热器器1

                    }

                    if (CurrentBoxTemperature >= 65)
                    {
                        PLCMod.SendPLCBitVar(PLCDOList[36], false, true); //压缩机加热器器2
                    }

                    if (CurrentBoxTemperature < 65)
                    {
                        PLCMod.SendPLCBitVar(PLCDOList[36], true, true); //压缩机加热器器2
                    }

                }
                else
                {

                    PLCMod.SendPLCBitVar(PLCDOList[22], false, false); //压缩机加热器器1
                    PLCMod.SendPLCBitVar(PLCDOList[36], false, false); //压缩机加热器器2
                }
            }


        }
        #endregion

    }
}

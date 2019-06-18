using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Controls;
using System.Windows;

using SHHS.UILabs;
using BackPanel;


namespace WpfApplication2
{
    public static class GlobleFun
    {
        #region 主钟事件分开到两个窗体里面
        //public static System.Windows.Threading.DispatcherTimer timer;
        ///// <summary>
        ///// 主钟开始运行函数
        ///// </summary>
        //public static void MainTimeBegin()
        //{
        //    //System.Windows.Threading.DispatcherTimer timer;

        //    timer = new System.Windows.Threading.DispatcherTimer();
        //    timer.Interval = new TimeSpan(0, 0, 1);
        //    timer.Tick += new EventHandler(MaintTime_Tick);
        //    timer.Start();
        //}
        ///// <summary>
        ///// 主钟结束函数
        ///// </summary>
        //public static void MainTimeClose()
        //{
        //    timer.Stop();
        //}




        ///// <summary>
        ///// 主钟运行事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public static void MaintTime_Tick(object sender, EventArgs e)
        //{
        //    switch(GlobelVar.senario )
        //    {
        //        case GlobelVar.Senario.CarCooling:

        //           // CarCooling.textbox1

        //            break;

        //    }

        //}


        ////主钟测试变量
        //public static double MainTimeTest=0;

        #endregion


        /// <summary>
        /// 输入button和对应的textbox的名称，就可以直接做出set以及down的切换！
        /// </summary>
        /// <param name="bttemp"></param>
        /// <param name="tbtemp"></param>
        public static void IsFitButtonToTextbox(Button bttemp, TextBox tbtemp)
        {
            if (bttemp.Content.ToString() == "set")
            {
                bttemp.Content = "Down";
                tbtemp.IsReadOnly = false;
            }
            else
            {
                double temp = 0;
                if (double.TryParse(tbtemp.Text, out temp))
                {
                    ////为了给每个不同的set规定的保留位数设定！
                    //switch (bttemp.Name)
                    //{
                    //    case "RotateSpeedOfCompressor":
                    //        break;
                    //    case "":
                    //        break;
                    //}

                    //set函数是一个整体所以自己只能在里面改：20150907
                    switch (bttemp.Name)
                    {
                        //Car
                        case "DischargeTempratureS":
                            GlobelVar.CarDischargeTemperatureSet = temp;
                            

                            GlobelVar.CarDischargeSatTemp_ForDBRef = temp;
                            //20150919压缩机排气压力20151012
                            GlobelVar.CarPressDischarge = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(temp);

                            GlobelVar.CarDischargePressureSetOnlyStable = GlobelVar.CarPressDischarge;
                            //20151108加添，给控制器发命令UT3
                            BackPanel.Control.Set(BackPanel.Control.Controllist[2].StackNum, "SV", GlobelVar.CarPressDischarge, 3);

                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            //报表参数
                            Report.ReportParameterMySelf.RP10DischargeSatTemp = GlobelVar.CarDischargeTemperatureSet.ToString("f2");
                            Report.ReportParameterMySelf.RP11DischargePres = GlobelVar.CarPressDischarge.ToString("f3");

                            double DischargeSatLiqEnthalpyTemp = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.PsatforH_Liq(GlobelVar.CarPressDischarge);
                            Report.ReportParameterMySelf.RP16DischargeSatLiqEnthalpy = DischargeSatLiqEnthalpyTemp.ToString("f2");

                            break;

                        case "SuctionTemperatureS":
                            GlobelVar.CarInputSaturateTemperatureSet = temp;
                            GlobelVar.CarSuctionSatTemp_ForDBRef = temp;

                            //20150919
                            GlobelVar.CarPressSuction = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(temp);

                            GlobelVar.CarInputPressureSetOnlyStable = GlobelVar.CarPressSuction;

                            //20151108加添，给控制器发命令 控制器UT1
                            BackPanel.Control.Set(BackPanel.Control.Controllist[0].StackNum, "SV", GlobelVar.CarPressSuction, 3);
                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            Report.ReportParameterMySelf.RP13SuctionSatTemp = GlobelVar.CarInputSaturateTemperatureSet.ToString("f2");
                            Report.ReportParameterMySelf.RP14SuctionPres = GlobelVar.CarPressSuction.ToString("f3");


                            double SuctionEnthTemp = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.CarInputTemperatureSet, GlobelVar.CarPressSuction);
                            double SuctonSpeVoluTemp = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(GlobelVar.CarInputTemperatureSet, GlobelVar.CarPressSuction);
                            Report.ReportParameterMySelf.RP17SuctionEnthalpy = SuctionEnthTemp.ToString("f2");
                            Report.ReportParameterMySelf.RP18SuctionSpecVolum = SuctonSpeVoluTemp.ToString("f6");

                            //Report.ReportParameterMySelf.RP12SuctionTemp;
                            //Report.ReportParameterMySelf.RP15CompRotate;
                            //Report.ReportParameterMySelf.RP19CompTemp;
                            break;

                        case "SuctionTemperature":
                            GlobelVar.CarInputTemperatureSet = temp;
                            GlobelVar.CarSuctionTem_ForDBRef = temp;

                            GlobelVar.CarInputTemperatureSetOnlyStable = GlobelVar.CarInputTemperatureSet;

                            //20151108加添，给控制器发命令 控制器UT2
                            BackPanel.Control.Set(BackPanel.Control.Controllist[1].StackNum, "SV", GlobelVar.CarInputTemperatureSet, 2);
                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            Report.ReportParameterMySelf.RP12SuctionTemp = GlobelVar.CarInputTemperatureSet.ToString();
                            //Report.ReportParameterMySelf.RP15CompRotate;
                            //Report.ReportParameterMySelf.RP19CompTemp;

                            double SuctionEnthTemp2 = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.CarInputTemperatureSet, GlobelVar.CarPressSuction);
                            double SuctonSpeVoluTemp2 = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(GlobelVar.CarInputTemperatureSet, GlobelVar.CarPressSuction);

                            Report.ReportParameterMySelf.RP17SuctionEnthalpy = SuctionEnthTemp2.ToString("f2");
                            Report.ReportParameterMySelf.RP18SuctionSpecVolum = SuctonSpeVoluTemp2.ToString("f6");

                            break;

                        case "RotateSpeedOfCompressor":
                            GlobelVar.CarCompressorRotateSet = temp;

                            GlobelVar.CarCompressorRotateSetOnlyStable = temp;

                            int ElectmotorRotateSet = Convert.ToInt32(temp * BackPanel.InformationGlo.CompressorDiameter_FromInfo / 300);
                            //20151108加添，给控制器发命令 控制器UT6
                            BackPanel.Control.Set(6, "SV", ElectmotorRotateSet, 0);
                            //保留两位小数
                            tbtemp.Text = Convert.ToInt32(temp).ToString();// temp.ToString("f0");

                            //Report.ReportParameterMySelf.RP15CompRotate = ElectmotorRotateSet.ToString("f0");
                            Report.ReportParameterMySelf.RP15CompRotate = GlobelVar.CarCompressorRotateSet.ToString("f0"); //20151225改正
                            //Report.ReportParameterMySelf.RP19CompTemp;


                            break;

                        case "TemperatureOfCoolingWater":
                            GlobelVar.CarCoolingWaterSet = temp;
                            //20151108加添，给控制器发命令 控制器UT5
                            BackPanel.Control.Set(BackPanel.Control.Controllist[3].StackNum, "SV", GlobelVar.CarCoolingWaterSet, 2);
                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            Report.ReportParameterMySelf.RP21CoolingWaterTemp = GlobelVar.CarCoolingWaterSet.ToString("f2");
                            break;

                        //Chiller
                        case "EvaporatorTemperature":
                            GlobelVar.ChillerEvaperator_TemperatureSet = temp;
                            //20150919
                            GlobelVar.ChillerPressEvap = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(temp);

                            //20151108加添，给控制器发命令 控制器UT1
                            BackPanel.Control.Set(BackPanel.Control.Controllist[0].StackNum, "SV", GlobelVar.ChillerPressEvap, 3);

                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            Report.ReportParameterMySelf_ForChiller.RP15EvaporatorTemp=temp.ToString("f2");
                            Report.ReportParameterMySelf_ForChiller.RP16EvaporatorPres = GlobelVar.ChillerPressEvap.ToString("f3");

                            double SuctionSpecVolum=BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(GlobelVar.ChillerSuctionTemp,GlobelVar.ChillerPressEvap);
                            double SuctionSpecEnthalpy = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.ChillerSuctionTemp, GlobelVar.ChillerPressEvap);

                            Report.ReportParameterMySelf_ForChiller.RP18SuctionSpecVolum=SuctionSpecVolum.ToString("f6");
                            Report.ReportParameterMySelf_ForChiller.RP19SuctionSpecEnthalpy = SuctionSpecEnthalpy.ToString("f2");

                            break;


                        case "SuctionTemperature_Chiller": //20151221添加

                            GlobelVar.ChillerSuction_TemperatureSet = temp;
                            //20150919
                            //GlobelVar.ChillerPressEvap = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(temp);
                            GlobelVar.ChillerSuctionTemp = temp;
                            //20151108加添，给控制器发命令 控制器UT1
                            BackPanel.Control.Set(2, "SV", GlobelVar.ChillerSuctionTemp, 2);
                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            Report.ReportParameterMySelf_ForChiller.RP17SuctionTemp = temp.ToString("f2");

                            double SuctionSpecVolum2=BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(GlobelVar.ChillerSuctionTemp,GlobelVar.ChillerPressEvap);
                            double SuctionSpecEnthalpy2 = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.ChillerSuctionTemp, GlobelVar.ChillerPressEvap);

                            Report.ReportParameterMySelf_ForChiller.RP18SuctionSpecVolum=SuctionSpecVolum2.ToString("f6");
                            Report.ReportParameterMySelf_ForChiller.RP19SuctionSpecEnthalpy = SuctionSpecEnthalpy2.ToString("f2");
                            break;

                        //SuctionTemperature
                        case "TemperatureOfInputWater":
                            GlobelVar.ChillerInputWater_TemperatureSet = temp;

                            //20151108加添，给控制器发命令 控制器UT5
                            BackPanel.Control.Set(BackPanel.Control.Controllist[3].StackNum, "SV", GlobelVar.ChillerInputWater_TemperatureSet, Convert.ToInt32(BackPanel.Control.Controllist[3].SDP));
                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            Report.ReportParameterMySelf_ForChiller.RP12InWaterTemperature = temp.ToString("f2");

                            
                            break;

                        case "TemperatureOfOutWater":
                            GlobelVar.ChillerOutputWater_TemperatureSet = temp;

                            //20151108加添，给控制器发命令 控制器UT4
                            BackPanel.Control.Set(BackPanel.Control.Controllist[2].StackNum, "SV", GlobelVar.ChillerOutputWater_TemperatureSet, Convert.ToInt32(BackPanel.Control.Controllist[2].SDP));
                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue=temp.ToString("f2");
                            break;

                        case "FlowRateOfCoolingWater":
                            GlobelVar.ChillerCoolingWater_FlowRateSet = temp;

                            //20151108加添，给控制器发命令 控制器UT4
                            BackPanel.Control.Set(BackPanel.Control.Controllist[2].StackNum, "SV", GlobelVar.ChillerCoolingWater_FlowRateSet, Convert.ToInt32(BackPanel.Control.Controllist[2].SDP));
                            //保留两位小数
                            tbtemp.Text = temp.ToString("f2");

                            Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue=temp.ToString("f2");
                            break;
                    }

                    bttemp.Content = "set";
                    tbtemp.IsReadOnly = true;

                }
                else
                {
                    MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }

        }
        //{
        //    if (bttemp.Content.ToString() == "set")
        //    {
        //        bttemp.Content = "Down";
        //        tbtemp.IsReadOnly = false;
        //    }
        //    else
        //    {
        //        double temp = 0;
        //        if (double.TryParse(textBox1.Text, out temp))
        //        {
        //            //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
        //            //if (!App.IsDemo)
        //            //    ControlStrategy.PID.Set(1, "SV", temp, 2);
        //            bttemp.Content = "set";
        //            tbtemp.IsReadOnly = true;
        //        }
        //        else
        //        {
        //            MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }

        //    }
        //}



        #region 判稳函数



        #endregion 判稳函数


        #region 计算函数总的：到时候只用调用这个就可以！20150908

        #region 分解Car： 检查20150919
        /// <summary>
        /// Car_A:1 返回值[0]是A制冷剂流量；[1]是A制冷量
        /// </summary>
        /// <param name="Array_Agilent101_122"></param>
        /// <param name="Array_Agilent201_222"></param>
        /// <returns></returns>
        public static double[] Car_A_Total(double[] Array_Agilent101_122, double[] Array_Agilent201_222)
        {
            double[] temp = new double[2];
            //BackPanel.Calculate.A_CarCalOfHeatDissipCoe(GlobelVar.Array_Agilent201_222[21-1],GlobelVar.SecSatTemp_Glo,GlobelVar.Array_Agilent101_122[7-1]);
            temp[0] = BackPanel.Calculate.A_CarCalOfRefFlowMass(Array_Agilent201_222[20 - 1], GlobelVar.SecSatTemp_Glo_A, Array_Agilent101_122[7 - 1], GlobelVar.hg2_Glo, GlobelVar.hf2_Glo);
            temp[1] = BackPanel.Calculate.A_CarCalOfCoolCap(GlobelVar.vga_Glo, GlobelVar.vg1_Glo, GlobelVar.hg1_Glo, GlobelVar.hf1_Glo);
            //WpfApplication2.GlobelVar.CalculateCar_Glo=

            return temp;
        }
        //Car_G:2
        public static void Car_G_Total(double[] Array_Agilent101_122, double[] Array_Agilent201_222)
        {
            BackPanel.Calculate.G_CarCalOfRefFlowMass(GlobelVar.C_GlO, Array_Agilent101_122[9 - 1], Array_Agilent101_122[8 - 1], Array_Agilent201_222[19 - 1], GlobelVar.tr_Glo, Array_Agilent101_122[7 - 1], GlobelVar.hg3_GlO, GlobelVar.hf3_GlO);
            BackPanel.Calculate.G_CarCalOfCoolCap(GlobelVar.vga_Glo, GlobelVar.vg1_Glo, GlobelVar.hg1_Glo, GlobelVar.hf1_Glo);

            //BackPanel.Calculate.G_carcalof
        }

        public static void Car_AG_Err()
        {
            BackPanel.Calculate.AG_CarCalOfTestError();
        }

        public static void Car_AG_CarCalOfShaftPower(double[] Array_Agilent101_122, double[] Array_Agilent201_222)
        {
            BackPanel.Calculate.AG_CarCalOfShaftPower(Array_Agilent201_222[4 - 1], Array_Agilent201_222[16 - 1]);
        }

        public static void CarCOP()
        {
            BackPanel.Calculate.AG_CarCalOfCOP(GlobelVar.vga_Glo, GlobelVar.vg1_Glo);
        }

        //CAR_AG：3
        public static void Car_AG_Total(double[] Array_Agilent101_122, double[] Array_Agilent201_222)
        {
            Car_AG_Err();
            Car_AG_CarCalOfShaftPower(Array_Agilent101_122, Array_Agilent201_222);
            CarCOP();
        }
        #endregion 分解
        //TODO:备注查找:CarCalculate_Total
        /// <summary>
        /// Car的计算函数把分解里的1，2，3合并:仅此一个函数调用！20150908
        /// 0:A方法制冷剂流量  1:A方法制冷量  2:G方法制冷剂流量  3:G方法制冷量; 4:A,G制冷量误差 5:A,G的轴功率  6:汽车压缩器的COP
        /// </summary>
        public static double[] CarCalculate_Total(double[] Array_Agilent101_122, double[] Array_Agilent201_222)
        {
            double[] temp = new double[10];
            //Car_A_Total(Array_Agilent101_122, Array_Agilent201_222);
            //BackPanel.Calculate.A_CarCalOfHeatDissipCoe(GlobelVar.Array_Agilent201_222[21-1],GlobelVar.SecSatTemp_Glo,GlobelVar.Array_Agilent101_122[7-1]);
            if (BackPanel.InformationGlo.FilterNumber_ForCarCalculate == 1)
            {
                //A方法制冷剂流量
                temp[0] = BackPanel.Calculate.A_CarCalOfRefFlowMass(Array_Agilent201_222[20 - 1], GlobelVar.SecSatTemp_Glo_A, Array_Agilent101_122[7 - 1], GlobelVar.hg2_Glo, GlobelVar.hf2_Glo);
                GlobelVar.A_RefFlowRate_Mid = temp[0];
                //A方法制冷量
                temp[1] = BackPanel.Calculate.A_CarCalOfCoolCap(GlobelVar.vga_Glo, GlobelVar.vg1_Glo, GlobelVar.hg1_Glo, GlobelVar.hf1_Glo);
                GlobelVar.A_CoolingCapacity_Mid = temp[1];
                //Car_G_Total(Array_Agilent101_122, Array_Agilent201_222);

                //G方法制冷剂流量:水的流量，之前一直用的是m3/s，密度默认为1；现在是随之改变的！
                double qm = Array_Agilent201_222[19 - 1] * GlobelVar.WaterDensity_Glo / 3600; //水的流量 单位kg/s
                //temp[2] = BackPanel.Calculate.G_CarCalOfRefFlowMass(GlobelVar.C_GlO, Array_Agilent101_122[9 - 1], Array_Agilent101_122[8 - 1], Array_Agilent201_222[19 - 1], GlobelVar.tr_Glo, Array_Agilent101_122[7 - 1], GlobelVar.hg3_GlO, GlobelVar.hf3_GlO);
                temp[2] = BackPanel.Calculate.G_CarCalOfRefFlowMass(GlobelVar.C_GlO, Array_Agilent101_122[9 - 1], Array_Agilent101_122[8 - 1], qm, GlobelVar.tr_Glo, Array_Agilent101_122[7 - 1], GlobelVar.hg3_GlO, GlobelVar.hf3_GlO);
                GlobelVar.G_RefFlowRate_Mid = temp[2];
                //G方法制冷量
                temp[3] = BackPanel.Calculate.G_CarCalOfCoolCap(GlobelVar.vga_Glo, GlobelVar.vg1_Glo, GlobelVar.hg1_Glo, GlobelVar.hf1_Glo);
                GlobelVar.G_CoolingCapacity_Mid = temp[3];
            }
            if (BackPanel.InformationGlo.FilterNumber_ForCarCalculate > 1)
            {
                //A方法制冷剂流量
                temp[0] = BackPanel.Calculate.A_CarCalOfRefFlowMass(Array_Agilent201_222[20 - 1], GlobelVar.SecSatTemp_Glo_A, Array_Agilent101_122[7 - 1], GlobelVar.hg2_Glo, GlobelVar.hf2_Glo);
                temp[0] = temp[0] * GlobelVar.A_RefFlowRateCoe + GlobelVar.A_RefFlowRate_Mid * (1 - GlobelVar.A_RefFlowRateCoe);
                GlobelVar.A_RefFlowRate_Mid = temp[0];
                //A方法制冷量
                temp[1] = BackPanel.Calculate.A_CarCalOfCoolCap(GlobelVar.vga_Glo, GlobelVar.vg1_Glo, GlobelVar.hg1_Glo, GlobelVar.hf1_Glo);
                temp[1] = temp[1] * GlobelVar.A_CoolingCapacityCoe + GlobelVar.A_CoolingCapacity_Mid * (1 - GlobelVar.A_CoolingCapacityCoe);
                GlobelVar.A_CoolingCapacity_Mid = temp[1];
                //Car_G_Total(Array_Agilent101_122, Array_Agilent201_222);
                //G方法制冷剂流量
                double qm = Array_Agilent201_222[19 - 1] * GlobelVar.WaterDensity_Glo / 3600;

                temp[2] = BackPanel.Calculate.G_CarCalOfRefFlowMass(GlobelVar.C_GlO, Array_Agilent101_122[9 - 1], Array_Agilent101_122[8 - 1], qm, GlobelVar.tr_Glo, Array_Agilent101_122[7 - 1], GlobelVar.hg3_GlO, GlobelVar.hf3_GlO);
                temp[2] = temp[2] * GlobelVar.G_RefFlowRateCoe + GlobelVar.G_RefFlowRate_Mid * (1 - GlobelVar.G_RefFlowRateCoe);
                GlobelVar.G_RefFlowRate_Mid = temp[2];
                //G方法制冷量
                temp[3] = BackPanel.Calculate.G_CarCalOfCoolCap(GlobelVar.vga_Glo, GlobelVar.vg1_Glo, GlobelVar.hg1_Glo, GlobelVar.hf1_Glo);
                temp[3] = temp[3] * GlobelVar.G_CoolingCapacityCoe + GlobelVar.G_CoolingCapacity_Mid * (1 - GlobelVar.G_CoolingCapacityCoe);
                GlobelVar.G_CoolingCapacity_Mid = temp[3];
            }



            //A,G制冷量误差
            temp[4] = BackPanel.Calculate.AG_CarCalOfTestError();
            //A,G的轴功率
            temp[5] = BackPanel.Calculate.AG_CarCalOfShaftPower(Array_Agilent201_222[4 - 1], Array_Agilent201_222[16 - 1]);
            //汽车压缩器的COP
            temp[6] = BackPanel.Calculate.AG_CarCalOfCOP(GlobelVar.vga_Glo, GlobelVar.vg1_Glo);

            //TODO:20151118_1_全局变量需锁定
            //GlobelVar.CalculateCar_Glo = BackPanel.Calculate.CalculateCar;
            return temp;
        }


        #region 分解Chiller
        /// <summary>
        /// Chiller则是就这一个函数的调用：20150908
        /// 0:Chiller制冷剂流量,1:Chiller制冷量,2:Chiller_COP，3：轴功率
        /// </summary>
        public static double[] Chiller_Total(double[] Array_Agilent101_122, double[] Array_Agilent201_222, double[] WT310DataCOM3_Chiller)
        {
            BackPanel.Calculate.ChillerCalOfHeatDissipCap(GlobelVar.ts_GloChiller, Array_Agilent101_122[7 - 1]);

            //20151008改
            BackPanel.Calculate.ChillerCalOfCoolCap(Array_Agilent201_222[20 - 1], GlobelVar.hg1_GloChiller, GlobelVar.hg2_GloChiller, GlobelVar.hf1_GloChiller, GlobelVar.hf2_GloChiller, GlobelVar.v1_GloChiller, GlobelVar.vg1_GloChiller);
            //20150925改
            BackPanel.Calculate.ChillerCalOfInputPower(WT310DataCOM3_Chiller);
            BackPanel.Calculate.ChillerCalOfCOP(GlobelVar.v1_GloChiller, GlobelVar.vg1_GloChiller);

            GlobelVar.CalculateChiller_Glo = BackPanel.Calculate.CalculateChiller;

            double[] Result = new double[10];

            if (BackPanel.InformationGlo.FilterNumber_ForChillerCalculate == 1)
            {
                //Chiller制冷剂流量
                Result[0] = BackPanel.Calculate.CalculateChiller.RefrigFlowMass;
                GlobelVar.Chiller_RefFlowRate_Mid = Result[0];
                //Chiller制冷量
                Result[1] = BackPanel.Calculate.CalculateChiller.CoolingCapacity;
                GlobelVar.Chiller_CoolingCapacity_Mid = Result[1];
                //Chiller_COP
                Result[2] = BackPanel.Calculate.CalculateChiller.COP;

                //轴功率
                Result[3] = BackPanel.Calculate.CalculateChiller.ActualCompressPower;
                GlobelVar.Chiller_Power_Mid = Result[3];
            }

            if (BackPanel.InformationGlo.FilterNumber_ForChillerCalculate > 1)
            {
                //Chiller制冷剂流量
                Result[0] = BackPanel.Calculate.CalculateChiller.RefrigFlowMass;
                Result[0] = Result[0] * GlobelVar.Chiller_RefFlowRateCoe + GlobelVar.Chiller_RefFlowRate_Mid * (1 - GlobelVar.Chiller_RefFlowRateCoe);
                GlobelVar.Chiller_RefFlowRate_Mid = Result[0];
                //GlobelVar.Chiller_RefFlowRate_Mid = Result[0];
                //Chiller制冷量
                Result[1] = BackPanel.Calculate.CalculateChiller.CoolingCapacity;
                Result[1] = Result[1] * GlobelVar.Chiller_CoolingCapacityCoe + GlobelVar.Chiller_CoolingCapacity_Mid * (1 - GlobelVar.Chiller_CoolingCapacityCoe);
                GlobelVar.Chiller_CoolingCapacity_Mid = Result[1];
                //Chiller_COP
                Result[2] = BackPanel.Calculate.CalculateChiller.COP;

                //轴功率
                Result[3] = BackPanel.Calculate.CalculateChiller.ActualCompressPower;
                Result[3] = Result[3] * GlobelVar.Chiller_PowerCoe + GlobelVar.Chiller_Power_Mid * (1 - GlobelVar.Chiller_PowerCoe);
                GlobelVar.Chiller_Power_Mid = Result[3];
            }

            return Result;
        }
        #endregion 分解Chiller

        #endregion


        #region 随机数产生函数给Agilent：20150916：20150924改

        /// <summary>
        /// 把采集上来的list的数，赋给全局101-122，201-222：20150916，其中还包括物性计算！！！！
        /// </summary>
        public static void AgilentDataFromBpToFrontAgilent()
        {
            //Random rdm = new Random();
            //double temp = rdm.Next(2,8);
            //for (int i = 0; i < 22;i++ )
            //{
            //    GlobelVar.Array_Agilent101_122[i] = temp;
            //    GlobelVar.Array_Agilent201_222[i] = temp;
            //}


            #region 把后台的数，传送给前台一个数组：20150916 :修改20150919：
            //101
            GlobelVar.Array_Agilent101_122[1 - 1] = BackPanel.Agilent.AgilentList[0].TargetCorr;

            GlobelVar.Array_Agilent101_122[2 - 1] = BackPanel.Agilent.AgilentList[1].TargetCorr;

            GlobelVar.Array_Agilent101_122[3 - 1] = BackPanel.Agilent.AgilentList[2].TargetCorr;

            GlobelVar.Array_Agilent101_122[4 - 1] = BackPanel.Agilent.AgilentList[3].TargetCorr;

            GlobelVar.Array_Agilent101_122[5 - 1] = BackPanel.Agilent.AgilentList[4].TargetCorr;

            GlobelVar.Array_Agilent101_122[6 - 1] = BackPanel.Agilent.AgilentList[5].TargetCorr;

            GlobelVar.Array_Agilent101_122[7 - 1] = BackPanel.Agilent.AgilentList[6].TargetCorr;

            GlobelVar.Array_Agilent101_122[8 - 1] = BackPanel.Agilent.AgilentList[7].TargetCorr;

            GlobelVar.Array_Agilent101_122[9 - 1] = BackPanel.Agilent.AgilentList[8].TargetCorr;

            //GlobelVar.Array_Agilent101_122[10 - 1] = BackPanel.Agilent.AgilentList[10 - 1].TargetCorr;

            //201-222
            GlobelVar.Array_Agilent201_222[1 - 1] = BackPanel.Agilent.AgilentList[9].TargetCorr;
            GlobelVar.Array_Agilent201_222[3 - 1] = BackPanel.Agilent.AgilentList[10].TargetCorr;

            GlobelVar.Array_Agilent201_222[4 - 1] = BackPanel.Agilent.AgilentList[11].TargetCorr;

            GlobelVar.Array_Agilent201_222[5 - 1] = BackPanel.Agilent.AgilentList[12].TargetCorr;

            GlobelVar.Array_Agilent201_222[6 - 1] = BackPanel.Agilent.AgilentList[13].TargetCorr;

            GlobelVar.Array_Agilent201_222[7 - 1] = BackPanel.Agilent.AgilentList[14].TargetCorr;

            GlobelVar.Array_Agilent201_222[8 - 1] = BackPanel.Agilent.AgilentList[15].TargetCorr;

            GlobelVar.Array_Agilent201_222[9 - 1] = BackPanel.Agilent.AgilentList[16].TargetCorr;

            GlobelVar.Array_Agilent201_222[10 - 1] = BackPanel.Agilent.AgilentList[17].TargetCorr;

            GlobelVar.Array_Agilent201_222[15 - 1] = BackPanel.Agilent.AgilentList[18].TargetCorr;

            GlobelVar.Array_Agilent201_222[16 - 1] = BackPanel.Agilent.AgilentList[19].TargetCorr;

            //20151101加
            GlobelVar.Array_Agilent201_222[18 - 1] = BackPanel.Agilent.AgilentList[20].TargetCorr;
            GlobelVar.Array_Agilent201_222[19 - 1] = BackPanel.Agilent.AgilentList[21].TargetCorr;

            //GlobelVar.Array_Agilent201_222[21 - 1] = BackPanel.Agilent.AgilentList[21].TargetCorr;
            //20151008更改
            GlobelVar.Array_Agilent201_222[20 - 1] = BackPanel.Agilent.AgilentList[22].TargetCorr;

            //GlobelVar.Array_Agilent201_222[19 - 1] = BackPanel.Agilent.AgilentList[23 - 1].TargetCorr;
            //量热器出压力是读的不是测的20150919
            //GlobelVar.Array_Agilent201_222[20 - 1] = BackPanel.Agilent.AgilentList[24 - 1].TargetCorr;

            //GlobelVar.Array_Agilent201_222[21 - 1] = BackPanel.Agilent.AgilentList[24 - 1].TargetCorr;
            #endregion 把后台的数，传送给前台一个数组：20150916
            double stopdot = 1;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            #region 物性赋值：依据采集的Agilent：20150918
            #region A
            //物性计算实例//MPa:GlobelVar.Array_Agilent101_122[5-1]   1.2左右
            GlobelVar.SecSatTemp_Glo_A = BackPanel.UtilityMod_Header.RefNistProp.r141b.Tsat_Liq(GlobelVar.Array_Agilent201_222[9 - 1]);
            stopdot = 2;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //???是不是饱和？？？20150919
            GlobelVar.hg2_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.Array_Agilent101_122[5 - 1], GlobelVar.Array_Agilent201_222[10 - 1]);
            //GlobelVar.hg2_Glo=BackPanel.UtilityMod_Header.RefNistProp.R141b？？？？
            GlobelVar.hf2_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.Array_Agilent101_122[4 - 1], GlobelVar.Array_Agilent201_222[8 - 1]); //C,MPa
            stopdot = 3;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            GlobelVar.vga_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(GlobelVar.Array_Agilent101_122[6 - 1], GlobelVar.Array_Agilent201_222[15 - 1]);

            GlobelVar.vg1_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(9, BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(-1));
            stopdot = 4;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            GlobelVar.hg1_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(9, BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(-1));
            //？？？？OK20150919
            GlobelVar.hf1_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.PsatforH_Liq(BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Liq(63));// BackPanel.UtilityMod_Header.RefNistProp.R141b.TPforH(63, BackPanel.UtilityMod_Header.RefNistProp.R141b.Psat_Liq(63));
            stopdot = 5;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            #endregion A

            #region G
            //2pt20150919
            GlobelVar.hg3_GlO = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.Array_Agilent101_122[2 - 1], GlobelVar.Array_Agilent201_222[6 - 1]);
            //3pt20150919
            GlobelVar.hf3_GlO = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.Array_Agilent101_122[3 - 1], GlobelVar.Array_Agilent201_222[7 - 1]);
            //2p,3p
            GlobelVar.tr_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap((GlobelVar.Array_Agilent201_222[6 - 1] + GlobelVar.Array_Agilent201_222[7 - 1]) / 2);
            stopdot = 6;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            #endregion G

            #region Chiller需要20150918

            #region
            stopdot = 7;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //物性计算实例//MPa:GlobelVar.Array_Agilent101_122[5-1]   1.2左右
            GlobelVar.ts_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.r141b.Tsat_Liq(GlobelVar.Array_Agilent201_222[9 - 1]);
            //GlobelVar.ts_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.R141b.Tsat_Vap(GlobelVar.Array_Agilent201_222[9 - 1]);
            #endregion

            stopdot = 8;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //修改20151013:自己是按高温工况求得的
            //GlobelVar.hg1_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.R141b.TPforH(GlobelVar.Array_Agilent101_122[6 - 1], GlobelVar.Array_Agilent201_222[15 - 1]);
            GlobelVar.hg1_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(18, BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(7));
            //6pt
            GlobelVar.hg2_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.Array_Agilent101_122[5 - 1], GlobelVar.Array_Agilent201_222[10 - 1]);
            //1p-205
            GlobelVar.hf1_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.PsatforH_Liq(GlobelVar.Array_Agilent201_222[5 - 1]);
            stopdot = 9;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //这两个和上面Car的一样
            GlobelVar.hf2_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(GlobelVar.Array_Agilent101_122[4 - 1], GlobelVar.Array_Agilent201_222[8 - 1]);
            ////GlobelVar.hg2_Glo=BackPanel.UtilityMod_Header.RefNistProp.R141b

            GlobelVar.v1_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(GlobelVar.Array_Agilent101_122[6 - 1], GlobelVar.Array_Agilent201_222[15 - 1]);
            //也是按高问工况算的20151013:20151015确认是高温工况
            GlobelVar.vg1_GloChiller = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(18, BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(7));
            stopdot = 10;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            #endregion Chiller需要20150918


            stopdot = 11;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            GlobelVar.textBox9Text = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[5 - 1]).ToString("f2");

            stopdot = 12;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            GlobelVar.textBox11Text = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]).ToString("f2");
            GlobelVar.textBox27Text = BackPanel.UtilityMod_Header.RefNistProp.r141b.Tsat_Liq(GlobelVar.Array_Agilent201_222[9 - 1]).ToString("f2");

            stopdot = 13;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            GlobelVar.MidTemp1_REF = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[5 - 1]);
            //MidTemp2 = Math.Abs(GlobelVar.CarInputSaturateTemperatureSet - UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]));
            GlobelVar.MidTemp2_REF = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]);
            stopdot = 14;
            BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            #endregion  物性赋值：依据采集的Agilent：20150918






        }
        #endregion 随机数产生函数

        #region 曲线添加通道2：20150910
        public static void AddChannel_Total()
        {
            #region 通道添加20150910

            //Agilent测量通道0-24：20150910
            //20150924改：20151008改
            Channel TestChannel0 = new Channel(0, 0, "被测机组出口制冷剂温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel0 = new Channel(0, 0, "压缩机出口制冷剂温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel1 = new Channel(1, 1, "冷凝器进口制冷剂气体温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel2 = new Channel(2, 2, "冷凝器出口制冷剂液体温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel3 = new Channel(3, 3, "膨胀阀前制冷剂液体温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //20151104
            Channel TestChannel4 = new Channel(4, 4, "量热器出口制冷剂气体温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel5 = new Channel(5, 5, "被测机组入口制冷剂温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel6 = new Channel(6, 6, "环境温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel7 = new Channel(7, 7, "冷却水进口温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel8 = new Channel(8, 8, "冷却水出口温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel9 = new Channel(9, 9, "冷凝机组冷却水进水温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4); 20150924
            Channel TestChannel10 = new Channel(10, 10, "冷凝机组冷却水出水温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel11 = new Channel(11,11, "恒温水槽供温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);   20150924
            Channel TestChannel12 = new Channel(12, 12, "压缩机箱温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel13 = new Channel(13, 13, "压缩机扭矩", 1, "Nm", "扭矩", "测量参数", 2, 100, 5, 20, 4);

            //20151008改回来尴尬
            Channel TestChannel14 = new Channel(14, 14, "被测机组出口制冷剂压力", 1, "MPa", "压力", "测量参数", 3, 100, 5, 20, 4);
            //Channel TestChannel14 = new Channel(14, 14, "压缩机出口制冷剂压力", 1, "MPa", "压力", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel15 = new Channel(15, 15, "冷凝器进口制冷剂气体压力", 1, "MPa", "压力", "测量参数", 3, 100, 5, 20, 4);
            Channel TestChannel16 = new Channel(16, 16, "冷凝器出口制冷剂液体压力", 1, "MPa", "压力", "测量参数", 3, 100, 5, 20, 4);
            Channel TestChannel17 = new Channel(17, 17, "膨胀阀前制冷剂液体压力", 1, "MPa", "压力", "测量参数", 3, 100, 5, 20, 4);
            Channel TestChannel18 = new Channel(18, 18, "量热器第二制冷剂压力", 1, "MPa", "压力", "测量参数", 3, 100, 5, 20, 4);
            Channel TestChannel19 = new Channel(19, 19, "量热器出口制冷剂气体压力", 1, "MPa", "压力", "测量参数", 3, 100, 5, 20, 4);
            Channel TestChannel20 = new Channel(20, 20, "被测机组入口制冷剂压力", 1, "MPa", "压力", "测量参数", 3, 100, 5, 20, 4);
            Channel TestChannel21 = new Channel(21, 21, "压缩机转速", 1, "rpm", "转速", "测量参数", 0, 100, 5, 20, 4);
            Channel TestChannel22 = new Channel(22, 22, "恒温水槽回水流量", 1, "m3/h", "流量", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel23 = new Channel(23, 23, "量热器输入功率", 1, "kW", "功率", "测量参数", 3, 100, 5, 20, 4);
            //20151101加
            Channel TestChannel24 = new Channel(24, 24, "进出口水压差", 1, "MPa", "压力", "测量参数", 3, 100, 5, 20, 4);
            //Channel TestChannel24 = new Channel(24, 24, "恒温水槽供液压力", 1, "Mpa", "压力", "测量参数", 2, 100, 5, 20, 4); 因为这个是度数的20150920

            //功率计WT电参数
            Channel TestChannel25 = new Channel(25, 0, "电压A", 1, "V", "电压", "电参数", 1, 20, 0, 20, 4);
            Channel TestChannel26 = new Channel(26, 1, "电压B", 1, "V", "电压", "电参数", 1, 20, 0, 20, 4);
            Channel TestChannel27 = new Channel(27, 2, "电压C", 1, "V", "电压", "电参数", 1, 20, 0, 20, 4);
            Channel TestChannel28 = new Channel(28, 3, "电流A", 1, "A", "电流", "电参数", 1, 20, 0, 20, 4);
            Channel TestChannel29 = new Channel(29, 4, "电流B", 1, "A", "电流", "电参数", 1, 20, 0, 20, 4);
            Channel TestChannel30 = new Channel(30, 5, "电流C", 1, "A", "电流", "电参数", 1, 20, 0, 20, 4);
            Channel TestChannel31 = new Channel(31, 6, "频率", 1, "Hz", "频率", "电参数", 1, 20, 0, 20, 4);
            Channel TestChannel32 = new Channel(32, 7, "功率因数", 1, "--", "因数", "电参数", 3, 1, 0, 1, 0);
            Channel TestChannel33 = new Channel(33, 7, "功率", 1, "kW", "功率", "电参数", 3, 1, 0, 1, 0);

            //控制器输出百分比
            Channel TestChannel34 = new Channel(34, 0, "UT1输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 1, 1, 0, 1, 0);
            Channel TestChannel35 = new Channel(35, 1, "UT2输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 1, 1, 0, 1, 0);
            Channel TestChannel36 = new Channel(36, 2, "UT3输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 1, 1, 0, 1, 0);
            Channel TestChannel37 = new Channel(37, 3, "UT4输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 1, 1, 0, 1, 0);
            Channel TestChannel38 = new Channel(38, 4, "UT5输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 1, 1, 0, 1, 0);
            Channel TestChannel39 = new Channel(39, 5, "UT6输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 1, 1, 0, 1, 0);

            #region 从Agilent转移过来：20150924：直接从控制器采集
            Channel TestChannel40 = new Channel(40, 6, "冷凝机组冷却水进水温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            Channel TestChannel41 = new Channel(41, 7, "恒温水槽供水温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            #endregion 从Agilent转移过来：20150924：直接从控制器采集


            //计算值
            Channel TestChannel42 = new Channel(42, 0, "压缩机主测冷量", 1, "kW", "能力", "计算参数", 3, 1, 0, 1, 0);
            Channel TestChannel43 = new Channel(43, 1, "压缩机辅测冷量", 1, "kW", "能力", "计算参数", 3, 1, 0, 1, 0);
            Channel TestChannel44 = new Channel(44, 2, "压缩机百分比偏差", 1, "%", "百分比", "计算参数", 1, 1, 0, 1, 0);
            Channel TestChannel45 = new Channel(45, 3, "压缩机COP", 1, "--", "COP", "计算参数", 2, 1, 0, 1, 0);

            #region 20151012 新添加需要的
            //Car的添加，制冷剂流量/和压缩机功率两个
            Channel TestChannel46 = new Channel(46, 4, "主测制冷剂流量", 1, "kg/s", "能力", "计算参数", 4, 1, 0, 1, 0);
            Channel TestChannel47 = new Channel(47, 5, "压缩机功率", 1, "kW", "计算参数", "计算参数", 3, 1, 0, 1, 0);


            //冷水机组新添加的20151012
            Channel TestChannel48 = new Channel(48, 6, "冷水机组制冷剂流量", 1, "kg/s", "能力", "计算参数", 4, 1, 0, 1, 0);
            Channel TestChannel49 = new Channel(49, 7, "冷水机组制冷量", 1, "kW", "能力", "计算参数", 3, 1, 0, 1, 0);
            Channel TestChannel50 = new Channel(50, 8, "冷水机组输入功率", 1, "%", "百分比", "计算参数", 1, 1, 0, 1, 0);
            Channel TestChannel51 = new Channel(51, 9, "冷水机组COP", 1, "--", "COP", "计算参数", 2, 1, 0, 1, 0);
            #endregion 新添加需要的

            #region 过程参数
            Channel TestChannel53 = new Channel(53, 10, "冷凝器前焓值hg3", 1, "J/kg", "焓值", "物性参数", 2, 1, 0, 1, 0);
            Channel TestChannel54 = new Channel(54, 11, "冷凝器后焓值hf3", 1, "J/kg", "焓值", "物性参数", 2, 1, 0, 1, 0);
            Channel TestChannel55 = new Channel(55, 12, "膨胀阀前焓值hf2", 1, "J/kg", "焓值", "物性参数", 2, 1, 0, 1, 0);
            Channel TestChannel56 = new Channel(56, 13, "量热器后焓值hg2", 1, "J/kg", "焓值", "物性参数", 2, 1, 0, 1, 0);
            Channel TestChannel57 = new Channel(57, 14, "吸气实际比容Vga", 1, "m3/kg", "比容", "物性参数", 5, 1, 0, 1, 0);
            Channel TestChannel58 = new Channel(58, 15, "吸气规定比容Vg1", 1, "m3/kg", "比容", "物性参数", 5, 1, 0, 1, 0);

            Channel TestChannel59 = new Channel(59, 16, "辅测制冷剂流量", 1, "kg/s", "能力", "计算参数", 4, 1, 0, 1, 0);


            #endregion

            GlobelVar.Channels = new Dictionary<int, Channel>();

            GlobelVar.Channels.Add(0, TestChannel0);
            GlobelVar.Channels.Add(1, TestChannel1);
            GlobelVar.Channels.Add(2, TestChannel2);
            GlobelVar.Channels.Add(3, TestChannel3);
            GlobelVar.Channels.Add(4, TestChannel4);
            GlobelVar.Channels.Add(5, TestChannel5);
            GlobelVar.Channels.Add(6, TestChannel6);
            GlobelVar.Channels.Add(7, TestChannel7);
            GlobelVar.Channels.Add(8, TestChannel8);
            //GlobelVar.Channels.Add(9, TestChannel9);20150924
            GlobelVar.Channels.Add(10, TestChannel10);
            //GlobelVar.Channels.Add(11, TestChannel11);20150924
            GlobelVar.Channels.Add(12, TestChannel12);
            GlobelVar.Channels.Add(13, TestChannel13);
            GlobelVar.Channels.Add(14, TestChannel14);
            GlobelVar.Channels.Add(15, TestChannel15);
            GlobelVar.Channels.Add(16, TestChannel16);
            GlobelVar.Channels.Add(17, TestChannel17);
            GlobelVar.Channels.Add(18, TestChannel18);
            GlobelVar.Channels.Add(19, TestChannel19);
            GlobelVar.Channels.Add(20, TestChannel20);
            GlobelVar.Channels.Add(21, TestChannel21);
            GlobelVar.Channels.Add(22, TestChannel22);
            GlobelVar.Channels.Add(23, TestChannel23);
            //GlobelVar.Channels.Add(24, TestChannel24);
            GlobelVar.Channels.Add(24, TestChannel24);
            GlobelVar.Channels.Add(25, TestChannel25);
            GlobelVar.Channels.Add(26, TestChannel26);
            GlobelVar.Channels.Add(27, TestChannel27);
            GlobelVar.Channels.Add(28, TestChannel28);
            GlobelVar.Channels.Add(29, TestChannel29);
            GlobelVar.Channels.Add(30, TestChannel30);
            GlobelVar.Channels.Add(31, TestChannel31);
            GlobelVar.Channels.Add(32, TestChannel32);
            GlobelVar.Channels.Add(33, TestChannel33);
            GlobelVar.Channels.Add(34, TestChannel34);
            GlobelVar.Channels.Add(35, TestChannel35);
            GlobelVar.Channels.Add(36, TestChannel36);
            GlobelVar.Channels.Add(37, TestChannel37);
            GlobelVar.Channels.Add(38, TestChannel38);
            GlobelVar.Channels.Add(39, TestChannel39);
            GlobelVar.Channels.Add(40, TestChannel40);
            GlobelVar.Channels.Add(41, TestChannel41);
            GlobelVar.Channels.Add(42, TestChannel42);
            GlobelVar.Channels.Add(43, TestChannel43);
            GlobelVar.Channels.Add(44, TestChannel44);
            GlobelVar.Channels.Add(45, TestChannel45);
            #region 新添
            GlobelVar.Channels.Add(46, TestChannel46);
            GlobelVar.Channels.Add(47, TestChannel47);
            GlobelVar.Channels.Add(48, TestChannel48);
            GlobelVar.Channels.Add(49, TestChannel49);
            GlobelVar.Channels.Add(50, TestChannel50);
            GlobelVar.Channels.Add(51, TestChannel51);

            GlobelVar.Channels.Add(53, TestChannel53);
            GlobelVar.Channels.Add(54, TestChannel54);
            GlobelVar.Channels.Add(55, TestChannel55);
            GlobelVar.Channels.Add(56, TestChannel56);
            GlobelVar.Channels.Add(57, TestChannel57);
            GlobelVar.Channels.Add(58, TestChannel58);
            GlobelVar.Channels.Add(59, TestChannel59);
            #endregion

            #endregion 通道添加
        }
        #endregion 曲线添加通道2：20150910


        public static bool BuildFilterCoe(List<BackPanel.Agilent.AgilentVar> AgilentList, string DBPath, string TableName)
        {
            string[] DataFormDB = BackPanel.DBOperate.ReadAllFromFilterCoeTable(DBPath, TableName);

            for (int i = 0; i < AgilentList.Count; i++)
            {
                AgilentList[i].FilterCoe = Convert.ToDouble(DataFormDB[i]);
            }

            GlobelVar.A_RefFlowRateCoe = Convert.ToDouble(DataFormDB[23]);
            GlobelVar.A_CoolingCapacityCoe = Convert.ToDouble(DataFormDB[24]);
            GlobelVar.G_RefFlowRateCoe = Convert.ToDouble(DataFormDB[25]);
            GlobelVar.G_CoolingCapacityCoe = Convert.ToDouble(DataFormDB[26]);

            GlobelVar.Chiller_RefFlowRateCoe = Convert.ToDouble(DataFormDB[27]);
            GlobelVar.Chiller_CoolingCapacityCoe = Convert.ToDouble(DataFormDB[28]);
            GlobelVar.Chiller_PowerCoe = Convert.ToDouble(DataFormDB[29]);
            return true;
        }

        /// <summary>
        /// 返回报表要的数据临时数组
        /// </summary>
        /// <param name="Array_Agilent101_122"></param>
        /// <param name="Array_Agilent201_222"></param>
        /// <returns></returns>
        public static double[] DoubleDataFor_CarReport(double[] Array_Agilent101_122, double[] Array_Agilent201_222, double[] CarCalculateResult_Car, double[] Others)
        {
            double[] data = new double[39];
            #region 报表用的数组20151218
            //data[0] = Array_Agilent201_222[4 - 1];
            if (Array_Agilent201_222[4 - 1]<0)
            {
                data[0] = 0;
            }
            else
            {
                data[0] = Array_Agilent201_222[4 - 1];
            }
            
            //data[1] = Array_Agilent201_222[16 - 1];
            if (Array_Agilent201_222[16 - 1]<0)
            {
                data[1] = 0;
            }
            else
            {
                double CompressorRotateToDisplay = Array_Agilent201_222[16 - 1] * 300 / BackPanel.InformationGlo.CompressorDiameter_FromInfo;
                //textBox14.Text = Convert.ToInt32(CompressorRotateToDisplay).ToString();// .ToString("f2");
                //data[1] = Array_Agilent201_222[16 - 1];
                data[1] = CompressorRotateToDisplay;
            }

            data[2] = Array_Agilent201_222[15 - 1];
            data[3] = Array_Agilent101_122[6 - 1];
            data[4] = Others[4];  //进入压缩机实际比体积
            data[5] = Array_Agilent201_222[5 - 1];
            data[6] = Array_Agilent101_122[1 - 1];

            data[7] = Array_Agilent201_222[6 - 1];
            data[8] = Array_Agilent101_122[2 - 1];

            data[9] = Others[9]; //冷凝器进口比焓

            data[10] = Array_Agilent201_222[7 - 1];
            data[11] = Array_Agilent101_122[3 - 1];

            data[12] = Others[12];

            data[13] = Array_Agilent201_222[8 - 1];
            data[14] = Array_Agilent101_122[4 - 1];
            data[15] = Others[15];

            data[16] = Array_Agilent201_222[9 - 1];

            data[17] = Others[17];
            data[18] = Array_Agilent101_122[7 - 1];
            data[19] = Array_Agilent201_222[10 - 1];
            data[20] = Array_Agilent101_122[5 - 1];
            data[21] = Others[21];

            data[22] = Array_Agilent101_122[8 - 1];
            data[23] = Array_Agilent101_122[9 - 1];
            data[24] = Array_Agilent201_222[19 - 1];
            data[25] = Others[25];
            data[26] = Others[26];

            data[27] = Array_Agilent201_222[2 - 1];
            data[28] = Array_Agilent201_222[20 - 1];
            data[29] = Others[29];
            data[30] = Others[30];
            data[31] = Others[31]; //冷凝器漏热量
            data[32] = CarCalculateResult_Car[0];
            data[33] = CarCalculateResult_Car[1];
            data[34] = CarCalculateResult_Car[2];
            data[35] = CarCalculateResult_Car[3];
            data[36] = CarCalculateResult_Car[5];
            data[37] = CarCalculateResult_Car[4];
            data[38] = CarCalculateResult_Car[6];
            //GlobelVar.DoubleDataForCarReport[39]=Array_Agilent101_122[];
            #endregion 报表用的数组20151218
            return data;
        }

        public static double[] DoubleDataFor_ChillerReport(double[] Array_Agilent101_122, double[] Array_Agilent201_222, double[] WT310DataCOM3_Chiller, double[] ChillerCalculateResult_Chiller, double[] Others)
        {
            double[] data = new double[39];
            #region 报表用的数组20151218
            //data[0] = WT310DataCOM3_Chiller[0];
            if (WT310DataCOM3_Chiller[0]<0)
            {
                data[0] = 0;
            }
            else
            {
                data[0] = WT310DataCOM3_Chiller[0];
            }
            
            //data[1] = WT310DataCOM3_Chiller[1];
            if (WT310DataCOM3_Chiller[1] < 0)
            {
                data[1] = 0;
            }
            else
            {
                data[1] = WT310DataCOM3_Chiller[1];
            }

            //data[2] = WT310DataCOM3_Chiller[2];
            if (WT310DataCOM3_Chiller[2] < 0)
            {
                data[1] = 0;
            }
            else
            {
                data[2] = WT310DataCOM3_Chiller[2];
            }

            //data[3] = WT310DataCOM3_Chiller[3];
            if (WT310DataCOM3_Chiller[3] < 0)
            {
                data[3] = 0;
            }
            else
            {
                data[3] = WT310DataCOM3_Chiller[3];
            }

            //data[4] = WT310DataCOM3_Chiller[4];  //进入压缩机实际比体积
            if (WT310DataCOM3_Chiller[4] < 0)
            {
                data[4] = 0;
            }
            else
            {
                data[4] = WT310DataCOM3_Chiller[4];
            }

            //data[5] = WT310DataCOM3_Chiller[5];
            if (WT310DataCOM3_Chiller[5] < 0)
            {
                data[5] = 0;
            }
            else
            {
                data[5] = WT310DataCOM3_Chiller[5];
            }

            //data[6] = WT310DataCOM3_Chiller[8];
            if (WT310DataCOM3_Chiller[8] < 0)
            {
                data[6] = 0;
            }
            else
            {
                data[6] = WT310DataCOM3_Chiller[8];
            }

            //data[7] = WT310DataCOM3_Chiller[6];
            if (WT310DataCOM3_Chiller[6] < 0)
            {
                data[7] = 0;
            }
            else
            {
                data[7] = WT310DataCOM3_Chiller[6];
            }



            data[8] = Array_Agilent201_222[15 - 1];
            data[9] = Array_Agilent101_122[6 - 1]; //冷凝器进口比焓

            data[10] = Others[0];
            data[11] = Array_Agilent201_222[5 - 1];
            data[12] = Array_Agilent101_122[1 - 1];

            data[13] = Others[1];
            data[14] = Array_Agilent201_222[8 - 1];
            data[15] = Array_Agilent101_122[4 - 1];

            data[16] = Others[2];
            data[17] = Array_Agilent201_222[9 - 1];

            data[18] = Others[3];
            data[19] = Array_Agilent101_122[7 - 1];
            data[20] = Array_Agilent201_222[10 - 1];
            data[21] = Array_Agilent101_122[5 - 1];

            data[22] = Others[4];
            data[23] = Array_Agilent101_122[8 - 1];
            data[24] = Array_Agilent101_122[9 - 1];
            data[25] = Array_Agilent201_222[19 - 1];

            data[26] = Others[5];
            data[27] = Others[6];
            data[28] = Array_Agilent201_222[20 - 1];

            data[29] = Others[7];
            data[30] = ChillerCalculateResult_Chiller[0];
            data[31] = ChillerCalculateResult_Chiller[1]; //冷凝器漏热量
            data[32] = ChillerCalculateResult_Chiller[2];

            //GlobelVar.DoubleDataForCarReport[39]=Array_Agilent101_122[];
            #endregion 报表用的数组20151218
            return data;
        }
    }
}

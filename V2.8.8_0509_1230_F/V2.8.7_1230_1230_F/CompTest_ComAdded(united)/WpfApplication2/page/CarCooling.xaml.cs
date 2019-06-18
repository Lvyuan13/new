using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BackPanel;

using System.ComponentModel;

using RefMDBInquiry;

//曲线引用
using SHHS.UILabs;
using System.Runtime.InteropServices;
using System.Windows.Interop;

using Report;
using System.Threading;

namespace WpfApplication2
{
    /// <summary>
    /// CarCooling.xaml 的交互逻辑
    /// </summary>
    public partial class CarCooling : Window
    {
        public CarCooling()
        {
            this.InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
        }



        private void QuitTrial_Click(object sender, RoutedEventArgs e)
        {
            if (BackPanel.Strategy.IsTherePLC_Error == true)
            {
                MessageBoxResult mbr = MessageBox.Show("出现报警，请首先确认故障设备解除！是否需要保存数据？", "退出试验", MessageBoxButton.OKCancel);
                if (mbr == MessageBoxResult.OK)
                {

                    

                    //PLC低压寄存器复位！强制的20150928改:20150930
                    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[5], false, true);
                    //量热器低液位寄存器复位！
                    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[2], false, true);

                    #region  保存报表数据20151224

                    IsQualifiedWindow isqualifiedwindow = new IsQualifiedWindow();
                    isqualifiedwindow.ShowDialog();

                    DBOperateForReport.ConverParatmeterToGroup();

                    switch (SenarioForReport.senario_ForReport)
                    {
                        case SenarioForReport.Senario_ForReport.CarCooling:
                        case SenarioForReport.Senario_ForReport.CarNoise:
                            //string ValueCommand= DBOperateForReport.ConvertToCommandValue(StringListForCarReport.CarStringList, ReportParameterMySelf.RPGroupForCar);
                            DBOperateForReport.AddInfoRecordToDateBase(DBPath_ForReport.DBPath_ForReportChild, StringListForCarReport.CarStringList, ReportParameterMySelf.RPGroupForCar);
                            break;

                        case SenarioForReport.Senario_ForReport.ChillerChangCondition:
                        case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                        case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                        case SenarioForReport.Senario_ForReport.ChillerPartialCondition:
                            DBOperateForReport.AddInfoRecordToDateBase(DBPath_ForReport.DBPath_ForReportChild, StringListForCarReport.ChillerStringList, ReportParameterMySelf_ForChiller.RPGroupForChiller);
                            break;
                    }

                    #endregion 保存报表数据

                    EndingSplash endsplash = new EndingSplash();
                    endsplash.Show();

                    MainTimeClose();

                    this.Close();
                }
                else
                {

                    //MainTimeClose();

                    //PLC低压寄存器复位！强制的20150928改:20150930
                    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[5], false, true);
                    //量热器低液位寄存器复位！
                    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[2], false, true);

                    EndingSplash endsplash = new EndingSplash();
                    endsplash.Show();

                    MainTimeClose();

                    this.Close();

                }

            }
            else
            {
                MessageBoxResult mbr = MessageBox.Show("是否需要保存数据？", "退出试验", MessageBoxButton.OKCancel);
                if (mbr == MessageBoxResult.OK)
                {
                    //this.Close();
                    //Car car = new Car();
                    //car.Show();
                    #region  保存报表数据20151224
                    
                    IsQualifiedWindow isqualifiedwindow = new IsQualifiedWindow();
                    isqualifiedwindow.ShowDialog();

                    DBOperateForReport.ConverParatmeterToGroup();

                    switch (SenarioForReport.senario_ForReport)
                    {
                        case SenarioForReport.Senario_ForReport.CarCooling:
                        case SenarioForReport.Senario_ForReport.CarNoise:
                            //string ValueCommand= DBOperateForReport.ConvertToCommandValue(StringListForCarReport.CarStringList, ReportParameterMySelf.RPGroupForCar);
                            DBOperateForReport.AddInfoRecordToDateBase(DBPath_ForReport.DBPath_ForReportChild, StringListForCarReport.CarStringList, ReportParameterMySelf.RPGroupForCar);
                            break;

                        case SenarioForReport.Senario_ForReport.ChillerChangCondition:
                        case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                        case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                        case SenarioForReport.Senario_ForReport.ChillerPartialCondition:
                            DBOperateForReport.AddInfoRecordToDateBase(DBPath_ForReport.DBPath_ForReportChild, StringListForCarReport.ChillerStringList, ReportParameterMySelf_ForChiller.RPGroupForChiller);
                            break;
                    }

                    #endregion 保存报表数据

                    BackPanel.Strategy.IsReturnFromMain = true;

                    Car car = new Car();
                    car.Show();

                    MainTimeClose();
                    this.Close();
                }
                else
                {
                    BackPanel.Strategy.IsReturnFromMain = true;



                    Car car = new Car();
                    car.Show();

                    MainTimeClose();
                    this.Close();

                }
            }



        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(DischargeTempratureS, textBox1);

        }

        private void SuctionTemperatureS_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(SuctionTemperatureS, textBox3);
        }

        private void SuctionTemperature_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(SuctionTemperature, textBox5);

        }


        private void RotateSpeedOfCompressor_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(RotateSpeedOfCompressor, textBox6);

        }

        private void TemperatureOfCoolingWater_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(TemperatureOfCoolingWater, textBox7);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //主钟，在操作界面打开时，开始运行！
            MainTimeBegin();

            //报警日期初始化！
            AlarmDateInitiate();


            GlobelVar.InfoChangeCar = 1;

            //取消左上角关闭按钮功能：第二部分20150917 
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            //PLC控制变量初始化20150922
            BackPanel.Strategy.StrategyPLCIfVarIni();

            if (!isReportIni)
            {
                //这个早应该在项目工程中初始化！
                Report.DoubleListForReport_FromFrontPanel.IniData_ForCar();
                isReportIni = true;
            }

            textBox6.Text = Convert.ToInt32(BackPanel.InformationGlo.CarCompressorRotateSet_ForControl).ToString("f0");



            #region 把报表list初始化20151225
            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:
                    //场景Car的
                    //StringListForCarReport.CarList[];
                    StringListForCarReport.IniStringList_ForCar();
                    
                    break;
                case SenarioForReport.Senario_ForReport.CarNoise:
                    //场景Car的
                    //StringListForCarReport.CarList[];
                    StringListForCarReport.IniStringList_ForCar();
                    
                    break;
                case SenarioForReport.Senario_ForReport.ChillerNormialCondition: //名义工况.
                    StringListForCarReport.IniStringList_ForChiller();

                    
                    break;
                case SenarioForReport.Senario_ForReport.ChillerPartialCondition: //部分工况
                    StringListForCarReport.IniStringList_ForChiller();
                    
                    break;
                case SenarioForReport.Senario_ForReport.ChillerChangCondition: //变工况
                    StringListForCarReport.IniStringList_ForChiller();

                    

                    break;
                case SenarioForReport.Senario_ForReport.ChillerMaxCondition: //最大工况
                    StringListForCarReport.IniStringList_ForChiller();
                    

                    break;


            }
            #endregion 把报表list初始化
        }
        /// <summary>
        /// 报表是否初始化
        /// </summary>
        bool isReportIni = false;

        #region 取消关闭按钮功能20150917
        //取消左上角关闭按钮功能：第一部分20150917
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        #endregion 取消关闭按钮功能20150917

        /// <summary>
        /// 这个是关闭之前的事件，所以会出现错误！
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //主钟，在操作界面关闭时，停止运行！
            //MainTimeClose();

            ////初始化！
            //GlobelVar.GlobeVarIni_Alarm();

            ////停止闪屏
            //EndingSplash endsplash = new EndingSplash();
            //endsplash.Show();
            //20150922添加：Car少  ，Chiller正常
            GlobelVar.InfoChangeChiller = 0;

            //控制变量初始化20150922
            BackPanel.Strategy.StrategyPLCIfVarIni();

            GlobelVar.IsCarChillerOn = false;
        }
        /// <summary>
        /// 关闭之后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            ////停止闪屏
            //EndingSplash endsplash = new EndingSplash();
            //endsplash.Show();
            GlobelVar.InfoChangeCar = 0;
        }

        #region 各个线程定义
        /// <summary>
        /// 稳定模块线程
        /// </summary>
        private BackgroundWorker _StableWorker = new BackgroundWorker();
        /// <summary>
        /// 安捷伦
        /// </summary>
        private BackgroundWorker _AgilentWorker = new BackgroundWorker();
        /// <summary>
        /// PLC
        /// </summary>
        private BackgroundWorker _PLCWorker = new BackgroundWorker();

        private BackgroundWorker _UT35A = new BackgroundWorker();
        /// <summary>
        /// 功率计模块20150911
        /// </summary>
        private BackgroundWorker _WT330 = new BackgroundWorker();

        /// <summary>
        /// 曲线工作
        /// </summary>
        private BackgroundWorker _CurveWorker = new BackgroundWorker();

        /// <summary>
        /// 计算模块20150911
        /// </summary>
        private BackgroundWorker _Calculate = new BackgroundWorker();

        private BackgroundWorker _RefpropList = new BackgroundWorker();
        #endregion



        #region 钟函数

        public System.Windows.Threading.DispatcherTimer timer;
        /// <summary>
        /// 主钟开始运行函数
        /// </summary>
        public void MainTimeBegin()
        {
            //System.Windows.Threading.DispatcherTimer timer;
            //线程事件
            _AgilentWorker.DoWork += new DoWorkEventHandler(_AgilentWorker_DoWork);

            _PLCWorker.DoWork += new DoWorkEventHandler(_PLCWorker_DoWork);

            _UT35A.DoWork += new DoWorkEventHandler(_UT35A_DoWork);

            _StableWorker.DoWork += new DoWorkEventHandler(_StableWorker_DoWork);

            //曲线线程20150906
            _CurveWorker.DoWork += new DoWorkEventHandler(_CurveWork_DoWork);

            _WT330.DoWork += new DoWorkEventHandler(_WT330_DoWork);
            //物性计算
            _RefpropList.DoWork += new DoWorkEventHandler(_RefpropList_DoWork);

            _Calculate.DoWork += new DoWorkEventHandler(_Calculate_DoWork);
            //曲线初始化
            CurveIni();

            #region 各个线程list初始化：20150916
            //BackPanel.Agilent.BuildAgilentList(Agilent.AgilentList, "D:\\CarChiller.mdb", "Table_Agilent");
            //BackPanel.Control.InitiateSendAllToControl();
            //BackPanel.PLCMod.BuildPLCDIDOListFromDB(PLCMod.PLCDOList, "D:\\CarChiller.mdb", "PLC_DOLIST");

            //BackPanel.PLCMod.BuildPLCDIDOListFromDB(PLCMod.PLCDIList, "D:\\CarChiller.mdb", "PLC_DILIST");
            //功率计不需要，就一个数组
            #endregion 各个线程list初始化：20150916

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(MaintTime_Tick);
            timer.Start();
        }







        #region 各个线程，具体工作
        /// <summary>
        /// 稳定模块线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _StableWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //if()
            //{}
            //throw new NotImplementedException();
            if (iniStableTimerStart == 0)
            {
                ////StableTimerStart();
                //iniStableTimerStart = 1;
                //如果稳定
                if (GlobelVar.IsStableCar)
                {
                    StableStart();
                }
                else
                {
                    StableEnd();
                }
            }
        }

        //double UT1OUT;
        void _UT35A_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            //UT1OUT = UtilityMod_Header.Controller_COM6.ReadControllerDB(1, "OUT", 2);

            //读取OUT的值

        }

        void _PLCWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            //bool TF = UtilityMod_Header.PLC_COM2.TFRead("M","2.1");
            //UtilityMod_Header.PLC_COM2.t
            ////报警函数：201500916
            //IsAlarmFound();
            //20150916这个是读取DIlist的报警值
            BackPanel.PLCMod.PLCAlter(BackPanel.PLCMod.PLCDIList);
            //这个是PLC的控制策略20150922
            BackPanel.Strategy.StrategyPLCAlarm_ForCar(BackPanel.PLCMod.PLCDIList, BackPanel.PLCMod.PLCDOList);

            double[] Array_Agilent201_222_ForPLC = new double[22];
            lock (Array_Agilent201_222_All)
            {
                Array_Agilent201_222_ForPLC = (double[])Array_Agilent201_222_All;
            }

            //BackPanel.PLCMod.CompressorBoxHeater(BackPanel.Agilent.AgilentList[10].TargetCorr);//(BackPanel.Agilent.AgilentList[10].TargetCorr);
            BackPanel.PLCMod.CompressorBoxHeater(Array_Agilent201_222_ForPLC[2 - 1]);//(BackPanel.Agilent.AgilentList[10].TargetCorr);


        }
        //TODO：全局变量定义
        #region 全局变量
        static object Controllist_All = new List<BackPanel.Control.UT35ADef>(6);
        static object WT310DataCOM3_All = new double[10];
        static object AgilentList_All = new List<BackPanel.Agilent.AgilentVar>(60);

        static object CarCalculateResult_All = new double[10];

        static object Array_Agilent101_122_All = new double[22];
        static object Array_Agilent201_222_All = new double[22];
        #endregion 全局变量


        void _AgilentWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            #region 三个程序都转到局部变量20151118
            List<BackPanel.Control.UT35ADef> Controllist_Car = BackPanel.Control.ReadControlFromDN();
            lock (Controllist_All)
            {
                Controllist_All = Controllist_Car;
            }
            //throw new NotImplementedException();
            double[] WT310DataCOM3_Car = BackPanel.PowerMeter.ReadDataFromDN();
            lock (WT310DataCOM3_All)
            {
                WT310DataCOM3_All = WT310DataCOM3_Car;
            }
            //throw new NotImplementedException();
            //UtilityMod_Header.Agilent_COM1.GetAgilentData("101,102","121,122","103,104","","J");
            //20150913
            //UtilityMod_Header.Agilent_COM1.GetAgilentData("101,102,103,104,105,106,107,108,109,110", "", "", "", "");
            BackPanel.InformationGlo.FilterNumber++;
            //读取Agilent数组
            List<BackPanel.Agilent.AgilentVar> AgilentList_Car = BackPanel.Agilent.AgilentRecTotal(Agilent.AgilentList);
            lock (AgilentList_All)
            {
                AgilentList_All = AgilentList_Car;
            }
            #endregion  三个程序都转到局部变量20151118
            //TODO:赋给全局变量（1）
            #region 赋给全局变量







            #endregion 赋给全局变量



            #region 局部Agilent数组
            double[] Array_Agilent101_122 = new double[22];
            double[] Array_Agilent201_222 = new double[22];
            #endregion

            //TODO:赋给全局变量（3）
            #region 赋给全局变量
            lock (Array_Agilent101_122_All)
            {
                Array_Agilent101_122_All = Array_Agilent101_122;
            }
            lock (Array_Agilent101_122_All)
            {
                Array_Agilent201_222_All = Array_Agilent201_222;
            }

            #endregion 赋给全局变量

            #region 把后台的数，传送给前台一个数组：20150916 :修改20150919：
            //101
            Array_Agilent101_122[1 - 1] = AgilentList_Car[0].TargetCorr;

            Array_Agilent101_122[2 - 1] = AgilentList_Car[1].TargetCorr;

            Array_Agilent101_122[3 - 1] = AgilentList_Car[2].TargetCorr;

            Array_Agilent101_122[4 - 1] = AgilentList_Car[3].TargetCorr;

            Array_Agilent101_122[5 - 1] = AgilentList_Car[4].TargetCorr;

            Array_Agilent101_122[6 - 1] = AgilentList_Car[5].TargetCorr;

            Array_Agilent101_122[7 - 1] = AgilentList_Car[6].TargetCorr;

            Array_Agilent101_122[8 - 1] = AgilentList_Car[7].TargetCorr;

            Array_Agilent101_122[9 - 1] = AgilentList_Car[8].TargetCorr;

            //Array_Agilent101_122[10 - 1] = AgilentList[10 - 1].TargetCorr;

            //201-222
            Array_Agilent201_222[1 - 1] = AgilentList_Car[9].TargetCorr;

            //Array_Agilent201_222[3 - 1] = AgilentList_Car[10].TargetCorr; 20151201改为当今
            Array_Agilent201_222[2 - 1] = AgilentList_Car[10].TargetCorr;


            Array_Agilent201_222[4 - 1] = AgilentList_Car[11].TargetCorr;

            Array_Agilent201_222[5 - 1] = AgilentList_Car[12].TargetCorr;

            Array_Agilent201_222[6 - 1] = AgilentList_Car[13].TargetCorr;

            Array_Agilent201_222[7 - 1] = AgilentList_Car[14].TargetCorr;

            Array_Agilent201_222[8 - 1] = AgilentList_Car[15].TargetCorr;

            Array_Agilent201_222[9 - 1] = AgilentList_Car[16].TargetCorr;

            Array_Agilent201_222[10 - 1] = AgilentList_Car[17].TargetCorr;

            Array_Agilent201_222[15 - 1] = AgilentList_Car[18].TargetCorr;

            Array_Agilent201_222[16 - 1] = AgilentList_Car[19].TargetCorr;

            //20151101加
            Array_Agilent201_222[18 - 1] = AgilentList_Car[20].TargetCorr;
            Array_Agilent201_222[19 - 1] = AgilentList_Car[21].TargetCorr;

            //Array_Agilent201_222[21 - 1] = AgilentList[21].TargetCorr;
            //20151008更改
            Array_Agilent201_222[20 - 1] = AgilentList_Car[22].TargetCorr;
            #endregion 把后台的数，传送给前台一个数组：20150916


            double stopdot = 1;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            #region 物性赋值：依据采集的Agilent：20150918:20151118物性计算全部改为局部变量
            #region A
            //物性计算实例//MPa:GlobelVar.Array_Agilent101_122[5-1]   1.2左右
            //GlobelVar.SecSatTemp_Glo_A = BackPanel.UtilityMod_Header.RefNistProp.r141b.Tsat_Liq(Array_Agilent201_222[9 - 1]);
            GlobelVar.SecSatTemp_Glo_A = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R141b, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[9 - 1]);

            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //???是不是饱和？？？20150919
            //GlobelVar.hg2_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(Array_Agilent101_122[5 - 1], Array_Agilent201_222[10 - 1]);
            GlobelVar.hg2_Glo = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, false, Array_Agilent101_122[5 - 1], Array_Agilent201_222[10 - 1]);

            //GlobelVar.hg2_Glo=BackPanel.UtilityMod_Header.RefNistProp.R141b？？？？
            //GlobelVar.hf2_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(Array_Agilent101_122[4 - 1], Array_Agilent201_222[8 - 1]); //C,MPa
            GlobelVar.hf2_Glo = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, true, Array_Agilent101_122[4 - 1], Array_Agilent201_222[8 - 1]);

            stopdot = 3;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //GlobelVar.vga_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(Array_Agilent101_122[6 - 1], Array_Agilent201_222[15 - 1]);
            GlobelVar.vga_Glo = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Volume, false, Array_Agilent101_122[6 - 1], Array_Agilent201_222[15 - 1]);

            //GlobelVar.vg1_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(9, BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(-1));
            //double PnormalFor_1 = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Pressure, DBRefPropInquiry.DBRefPropName.Temp, -1);

            double PnormalFor_1 = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Pressure, DBRefPropInquiry.DBRefPropName.Temp, GlobelVar.CarSuctionSatTemp_ForDBRef);
            GlobelVar.vg1_Glo = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Volume, false, GlobelVar.CarSuctionTem_ForDBRef, PnormalFor_1);

            stopdot = 4;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //GlobelVar.hg1_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(9, BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(-1));
            GlobelVar.hg1_Glo = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, false, GlobelVar.CarSuctionTem_ForDBRef, PnormalFor_1);

            //？？？？OK20150919
            //GlobelVar.hf1_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.PsatforH_Liq(BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Liq(63));// BackPanel.UtilityMod_Header.RefNistProp.R141b.TPforH(63, BackPanel.UtilityMod_Header.RefNistProp.R141b.Psat_Liq(63));
            //double P_For63=DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a,DBRefPropInquiry.DBRefPropName.Pressure,DBRefPropInquiry.DBRefPropName.Temp,63);
            double P_For63 = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Pressure, DBRefPropInquiry.DBRefPropName.Temp, GlobelVar.CarDischargeSatTemp_ForDBRef);
            GlobelVar.hf1_Glo = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.EnthalpyL, DBRefPropInquiry.DBRefPropName.Pressure, P_For63);
            stopdot = 5;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            #endregion A

            #region G
            //2pt20150919
            //GlobelVar.hg3_GlO = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(Array_Agilent101_122[2 - 1], Array_Agilent201_222[6 - 1]);
            GlobelVar.hg3_GlO = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, false, Array_Agilent101_122[2 - 1], Array_Agilent201_222[6 - 1]);

            //3pt20150919
            //GlobelVar.hf3_GlO = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(Array_Agilent101_122[3 - 1], Array_Agilent201_222[7 - 1]);
            GlobelVar.hf3_GlO = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, true, Array_Agilent101_122[3 - 1], Array_Agilent201_222[7 - 1]);
            //2p,3p

            //GlobelVar.tr_Glo = BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap((Array_Agilent201_222[6 - 1] + Array_Agilent201_222[7 - 1]) / 2);

            GlobelVar.tr_Glo = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, (Array_Agilent201_222[6 - 1] + Array_Agilent201_222[7 - 1]) / 2);
            stopdot = 6;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            #endregion G

            stopdot = 11;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //排气压力对应饱和温度
            //GlobelVar.textBox9Text = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(Array_Agilent201_222[5 - 1]).ToString("f2");
            GlobelVar.textBox9Text = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[5 - 1]).ToString("f2");

            stopdot = 12;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            //吸气压力对应饱和温度
            //GlobelVar.textBox11Text = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(Array_Agilent201_222[15 - 1]).ToString("f2");
            GlobelVar.textBox11Text = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[15 - 1]).ToString("f2");
            //量热器温度
            //GlobelVar.textBox27Text = BackPanel.UtilityMod_Header.RefNistProp.r141b.Tsat_Liq(Array_Agilent201_222[9 - 1]).ToString("f2");
            GlobelVar.textBox27Text = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R141b, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[9 - 1]).ToString("f2");

            stopdot = 13;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            //GlobelVar.MidTemp1_REF = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(Array_Agilent201_222[5 - 1]);
            GlobelVar.MidTemp1_REF = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[5 - 1]);
            //MidTemp2 = Math.Abs(GlobelVar.CarInputSaturateTemperatureSet - UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]));
            //GlobelVar.MidTemp2_REF = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(Array_Agilent201_222[15 - 1]);
            GlobelVar.MidTemp2_REF = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[15 - 1]);
            stopdot = 14;

            double CoolingWaterTemperature = (Array_Agilent101_122[8 - 1] + Array_Agilent101_122[9 - 1]) / 2;
            //GlobelVar.C_GlO = BackPanel.UtilityMod_Header.Water.Cp(CoolingWaterTemperature) * 1000;
            GlobelVar.C_GlO = BackPanel.UtilityMod_Header.Water.Cp(CoolingWaterTemperature);


            GlobelVar.WaterDensity_Glo = BackPanel.UtilityMod_Header.Water.Density(CoolingWaterTemperature);
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
            GC.Collect();
            #endregion  物性赋值：依据采集的Agilent：20150918

            #region 汽车计算结果局部变量
            BackPanel.InformationGlo.FilterNumber_ForCarCalculate++;
            //0:A方法制冷剂流量  1:A方法制冷量  2:G方法制冷剂流量  3:G方法制冷量; 4:A,G制冷量误差 5:A,G的轴功率  6:汽车压缩器的COP
            double[] CarCalculateResult_Car = GlobleFun.CarCalculate_Total(Array_Agilent101_122, Array_Agilent201_222);
            #endregion 汽车计算结果局部变量
            //throw new NotImplementedException();

            //TODO:赋给全局变量（2）
            #region 赋给全局变量
            lock (CarCalculateResult_All)
            {
                CarCalculateResult_All = CarCalculateResult_Car;
            }

            #endregion


            #region 曲线
            DateTime dt = DateTime.Now;

            foreach (var v in GlobelVar.Channels)
            {
                v.Value.Time = dt;
            }


            Random rdm = new Random();
            #region 给Channels赋值：20150917（试验）：201509120改：20150924改

            //List<BackPanel.Agilent.AgilentVar> AgilentList_Ca = (List<BackPanel.Agilent.AgilentVar>)AgilentList;

            //改为20150920 :按照Agilent点位图改:20150924按数据库修改
            GlobelVar.Channels[0].Value = AgilentList_Car[0].TargetCorr;
            GlobelVar.Channels[1].Value = AgilentList_Car[1].TargetCorr;
            GlobelVar.Channels[2].Value = AgilentList_Car[2].TargetCorr;// BackPanel.Agilent.AgilentList[2].TargetCorr;
            GlobelVar.Channels[3].Value = AgilentList_Car[3].TargetCorr;// BackPanel.Agilent.AgilentList[3].TargetCorr;
            GlobelVar.Channels[4].Value = AgilentList_Car[4].TargetCorr;
            GlobelVar.Channels[5].Value = AgilentList_Car[5].TargetCorr;
            GlobelVar.Channels[6].Value = AgilentList_Car[6].TargetCorr;
            GlobelVar.Channels[7].Value = AgilentList_Car[7].TargetCorr;

            GlobelVar.Channels[8].Value = AgilentList_Car[8].TargetCorr;
            //GlobelVar.Channels[9].Value = AgilentList[9].TargetCorr;
            GlobelVar.Channels[10].Value = AgilentList_Car[9].TargetCorr;// GlobelVar.Array_Agilent201_222[7 - 1];
            //GlobelVar.Channels[11].Value = AgilentList[11].TargetCorr;
            GlobelVar.Channels[12].Value = AgilentList_Car[10].TargetCorr;// GlobelVar.Array_Agilent201_222[7 - 1];
            GlobelVar.Channels[13].Value = AgilentList_Car[11].TargetCorr;//  GlobelVar.Array_Agilent201_222[7 - 1];
            GlobelVar.Channels[14].Value = AgilentList_Car[12].TargetCorr;// ;
            GlobelVar.Channels[15].Value = AgilentList_Car[13].TargetCorr;// ;
            GlobelVar.Channels[16].Value = AgilentList_Car[14].TargetCorr;// 
            GlobelVar.Channels[17].Value = AgilentList_Car[15].TargetCorr;
            GlobelVar.Channels[18].Value = AgilentList_Car[16].TargetCorr;
            GlobelVar.Channels[19].Value = AgilentList_Car[17].TargetCorr;
            GlobelVar.Channels[20].Value = AgilentList_Car[18].TargetCorr;
            GlobelVar.Channels[21].Value = AgilentList_Car[19].TargetCorr;
            GlobelVar.Channels[22].Value = AgilentList_Car[21].TargetCorr;
            GlobelVar.Channels[23].Value = AgilentList_Car[22].TargetCorr;
            //20151101加
            GlobelVar.Channels[24].Value = AgilentList_Car[20].TargetCorr;

            #region 20150920:功率计WT电参数25-33对应25-33：20150920
            GlobelVar.Channels[25].Value = WT310DataCOM3_Car[0];
            GlobelVar.Channels[26].Value = WT310DataCOM3_Car[1];
            GlobelVar.Channels[27].Value = WT310DataCOM3_Car[2];
            GlobelVar.Channels[28].Value = WT310DataCOM3_Car[3];
            GlobelVar.Channels[29].Value = WT310DataCOM3_Car[4];
            GlobelVar.Channels[30].Value = WT310DataCOM3_Car[5];
            GlobelVar.Channels[31].Value = WT310DataCOM3_Car[6];
            GlobelVar.Channels[32].Value = WT310DataCOM3_Car[7];
            GlobelVar.Channels[33].Value = WT310DataCOM3_Car[8];

            #endregion /功率计WT电参数

            #region 功率计输出百分比：20150920：34-39对应34-39
            //20150929改添加
            switch (BackPanel.InformationGlo.senariocontrol)
            {
                case BackPanel.InformationGlo.SenarioControl.ControlCar:
                    //UT1
                    GlobelVar.Channels[34].Value = Controllist_Car[0].OUT;
                    //UT2
                    GlobelVar.Channels[35].Value = Controllist_Car[1].OUT;
                    //UT3
                    GlobelVar.Channels[36].Value = Controllist_Car[2].OUT;
                    //UT4
                    GlobelVar.Channels[37].Value = 0;
                    //UT5
                    GlobelVar.Channels[38].Value = Controllist_Car[3].OUT;
                    //UT6
                    GlobelVar.Channels[39].Value = Controllist_Car[4].OUT;
                    break;


                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                    //UT1
                    GlobelVar.Channels[34].Value = Controllist_Car[0].OUT;
                    //UT2
                    GlobelVar.Channels[35].Value = Controllist_Car[1].OUT;
                    ////UT3
                    //默认值是5，即没有值
                    GlobelVar.Channels[36].Value = 0;
                    //UT4
                    GlobelVar.Channels[37].Value = Controllist_Car[2].OUT;
                    //UT5
                    GlobelVar.Channels[38].Value = Controllist_Car[3].OUT;
                    //UT6
                    GlobelVar.Channels[39].Value = Controllist_Car[4].OUT;
                    break;

                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:
                    //UT1
                    GlobelVar.Channels[34].Value = Controllist_Car[0].OUT;
                    //UT2
                    GlobelVar.Channels[35].Value = Controllist_Car[1].OUT;
                    //UT3
                    GlobelVar.Channels[36].Value = 0;
                    //UT4
                    GlobelVar.Channels[37].Value = Controllist_Car[2].OUT;
                    //UT5
                    GlobelVar.Channels[38].Value = Controllist_Car[3].OUT;
                    //UT6
                    GlobelVar.Channels[39].Value = Controllist_Car[4].OUT;
                    break;

            }

            //控制器UT5的当前值20150924

            GlobelVar.Channels[40].Value = Controllist_Car[3].PV; //冷凝机组冷却水出水温度
            GlobelVar.Channels[41].Value = Controllist_Car[3].PV; //恒温水槽供水温度

            #endregion 功率计输出百分比：20150920

            #region 计算值20150950
            //GlobelVar.Channels[42].Value = GlobelVar.CalculateCar_Glo.A_CoolingCapacity; //1
            //GlobelVar.Channels[43].Value = GlobelVar.CalculateCar_Glo.G_CoolingCapacity; //3
            //GlobelVar.Channels[44].Value = GlobelVar.CalculateCar_Glo.TestErr;           //4
            //GlobelVar.Channels[45].Value = GlobelVar.CalculateCar_Glo.AG_COP;            //6
            GlobelVar.Channels[42].Value = CarCalculateResult_Car[1];
            GlobelVar.Channels[43].Value = CarCalculateResult_Car[3];
            GlobelVar.Channels[44].Value = CarCalculateResult_Car[4];
            GlobelVar.Channels[45].Value = CarCalculateResult_Car[6];
            ////20151012添加
            //GlobelVar.Channels[46].Value = GlobelVar.CalculateCar_Glo.A_RefrigFlowMass; //压缩机制冷剂流量  //0
            //GlobelVar.Channels[47].Value = GlobelVar.CalculateCar_Glo.ActualCompressPower; //压缩机功率     //5
            GlobelVar.Channels[46].Value = CarCalculateResult_Car[0]; //压缩机制冷剂流量
            GlobelVar.Channels[47].Value = CarCalculateResult_Car[5]; //压缩机功率

            #endregion 计算值20150950


            //冷凝器前焓值hg3
            GlobelVar.Channels[53].Value = GlobelVar.hg3_GlO;
            //冷凝器后焓值hf3
            GlobelVar.Channels[54].Value = GlobelVar.hf3_GlO;
            //膨胀阀前焓值hf2
            GlobelVar.Channels[55].Value = GlobelVar.hf2_Glo;
            //量热器后焓值hg2
            GlobelVar.Channels[56].Value = GlobelVar.hg2_Glo;
            //吸气实际比容Vga
            GlobelVar.Channels[57].Value = GlobelVar.vga_Glo;
            //吸气规定比容Vg1
            GlobelVar.Channels[58].Value = GlobelVar.vg1_Glo;
            //辅测制冷剂流量
            GlobelVar.Channels[59].Value = CarCalculateResult_Car[2];


            //Channels[1].Value =rdm.NextDouble();
            //Channels[2].Value =rdm.NextDouble();
            #endregion 给Channels赋值：20150917（试验）
            realtimeCurves1.Refresh();
            #endregion 曲线

            #region 插入数据库
            //这个在Car无用，在Chiller里面有用
            double[] OthersForDataBase = new double[11];
            OthersForDataBase[0] = GlobelVar.vga_Glo;//进入压缩机实际比体积
            OthersForDataBase[1] = GlobelVar.hg3_GlO;//冷凝器进口比焓
            OthersForDataBase[2] = GlobelVar.hf3_GlO;
            OthersForDataBase[3] = GlobelVar.hf2_Glo; //膨胀阀进口比焓kJ/kg
            OthersForDataBase[4] = GlobelVar.SecSatTemp_Glo_A;
            OthersForDataBase[5] = GlobelVar.hg2_Glo;//量热器出口比焓kJ/kg
            OthersForDataBase[6] = GlobelVar.C_GlO;
            OthersForDataBase[7] = GlobelVar.WaterDensity_Glo;
            OthersForDataBase[8] = BackPanel.Calculate.CalculateCar.A_HeatLeak;
            OthersForDataBase[9] = BackPanel.Calculate.CalculateCar.G_HeatExchangeInCondenser;
            OthersForDataBase[10] = BackPanel.Calculate.CalculateCar.G_HeatLeakInCondenser; //冷凝器漏热量

            double[] ChillerResult = new double[5];
            BackPanel.DBOperate.InsertRecordDataTODBTotal(BackPanel.InformationGlo.DBPath, InformationGlo.senario, AgilentList_Car, WT310DataCOM3_Car, Controllist_Car, CarCalculateResult_Car, ChillerResult,OthersForDataBase);
            //);
            #endregion 插入数据库
            //throw new Not ImplementedException();

            //获得globelvar.IsStableCar的状态
            //#region 
            //#endregion
            //StableJudgeFun_ForCar(CarCalculateResult[1], CarCalculateResult[3], Array_Agilent101_122, Array_Agilent201_222, Controllist);
            //StableJudgeFun_ForCar(CarCalculateResult[1], CarCalculateResult[3], Array_Agilent101_122, Array_Agilent201_222, Controllist);

            #region 报表用的数组20151218
            //保存用其他输入
            double[] Others = new double[39];
            Others[4] = GlobelVar.vga_Glo;//进入压缩机实际比体积
            Others[9] = GlobelVar.hg3_GlO;//冷凝器进口比焓
            Others[12] = GlobelVar.hf3_GlO;
            Others[15] = GlobelVar.hf2_Glo; //膨胀阀进口比焓kJ/kg
            Others[17] = GlobelVar.SecSatTemp_Glo_A;
            Others[21] = GlobelVar.hg2_Glo;//量热器出口比焓kJ/kg
            Others[25] = GlobelVar.C_GlO;
            Others[26] = GlobelVar.WaterDensity_Glo;
            Others[29] = BackPanel.Calculate.CalculateCar.A_HeatLeak;
            Others[30] = BackPanel.Calculate.CalculateCar.G_HeatExchangeInCondenser;
            Others[31] = BackPanel.Calculate.CalculateCar.G_HeatLeakInCondenser; //冷凝器漏热量

            //最终的数组
            GlobelVar.DoubleDataForCarReport = GlobleFun.DoubleDataFor_CarReport(Array_Agilent101_122, Array_Agilent201_222, CarCalculateResult_Car, Others);

            //GlobelVar.DoubleDataForCarReport[39]=Array_Agilent101_122[];
            #endregion 报表用的数组20151218
        }

        //曲线线程,实时的值在这里写！20150906
        void _CurveWork_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        /// <summary>
        /// 计算函数20150911
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _Calculate_DoWork(object sender, DoWorkEventArgs e)
        {



        }
        /// <summary>
        /// 功率计函数20150911
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _WT330_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        //string textBox9Text;// = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[5 - 1]).ToString("f2");
        //string textBox11Text;// = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]).ToString("f2");
        //string textBox27Text;// =

        //double MidTemp1_REF;
        //double MidTemp2_REF;

        void _RefpropList_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            //GlobleFun.AgilentDataFromBpToFrontAgilent();

            //double stopdot = 11;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            //textBox9Text = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[5 - 1]).ToString("f2");

            //stopdot = 12;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            //textBox11Text = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]).ToString("f2");
            //textBox27Text = BackPanel.UtilityMod_Header.RefNistProp.r141b.Tsat_Liq(GlobelVar.Array_Agilent201_222[9 - 1]).ToString("f2");

            //stopdot = 13;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal

            //MidTemp1_REF = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[5 - 1]);
            ////MidTemp2 = Math.Abs(GlobelVar.CarInputSaturateTemperatureSet - UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]));
            //MidTemp2_REF = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]);
            //stopdot = 14;
            //BackPanel.DBOperate.StpoDot_InsertRecordDataTODBTotal("D:\\stopdot.mdb", "stopdot2", stopdot);// StpoDot_InsertRecordDataTODBTotal
        }
        #endregion 各个线程，具体工作



        /// <summary>
        /// 主钟结束函数
        /// </summary>
        public void MainTimeClose()
        {
            timer.Stop();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// 
        /// </summary>
        int MainTimerNum = 0;
        Random Mainrdm = new Random();
        /// <summary>
        /// 主钟运行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MaintTime_Tick(object sender, EventArgs e)
        {
            //TODO:全局变量转为局部变量:总
            #region 全局变局部
            List<BackPanel.Control.UT35ADef> Controllist = new List<BackPanel.Control.UT35ADef>();
            //供水温度
            double SupplyWaterTemp = 0;
            if (MainTimerNum != 0)
            {
                lock (Controllist_All)
                {
                    Controllist = (List<BackPanel.Control.UT35ADef>)Controllist_All;
                    SupplyWaterTemp = Controllist[3].PV;
                }
            }
            //TODO:...1
            if (MainTimerNum == 0)
            {
                BackPanel.Control.BuildControlerList(Controllist, BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "Table_Controller");
                SupplyWaterTemp = 2;
            }

            //List<BackPanel.Control.UT35ADef> Controllist = (List<BackPanel.Control.UT35ADef>)Controllist_All;
            //BackPanel.Control.BuildControlerList(Controllist,BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "Table_Controller");

            double[] WT310DataCOM3 = new double[10];
            lock (WT310DataCOM3_All)
            {
                WT310DataCOM3 = (double[])WT310DataCOM3_All;
            }

            List<BackPanel.Agilent.AgilentVar> AgilentList = new List<Agilent.AgilentVar>();
            lock (AgilentList_All)
            {
                AgilentList = (List<BackPanel.Agilent.AgilentVar>)AgilentList_All;
            }

            double[] CarCalculateResult = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            lock (CarCalculateResult_All)
            {
                //0:A方法制冷剂流量  1:A方法制冷量  2:G方法制冷剂流量  3:G方法制冷量 4:A,G制冷量误差  5:A,G的轴功率  6:汽车压缩器的COP
                CarCalculateResult = (double[])CarCalculateResult_All;
            }

            double[] Array_Agilent101_122 = new double[22];
            lock (Array_Agilent101_122_All)
            {
                Array_Agilent101_122 = (double[])Array_Agilent101_122_All;
            }

            double[] Array_Agilent201_222 = new double[22];
            lock (Array_Agilent201_222_All)
            {
                Array_Agilent201_222 = (double[])Array_Agilent201_222_All;
            }


            #endregion 全局变局部


            MainTimerNum++;

            if (BackPanel.InformationGlo.IsVFDOn)
            {
                BackPanel.InformationGlo.VFDOnTimer++;
                if (BackPanel.InformationGlo.TorqueList.Count < 40)
                {
                    BackPanel.InformationGlo.TorqueList.Enqueue(Array_Agilent201_222[4 - 1]);
                }
                else
                {
                    BackPanel.InformationGlo.TorqueList.Dequeue();
                    BackPanel.InformationGlo.TorqueList.Enqueue(Array_Agilent201_222[4 - 1]);
                }
            }
            else
            {
                BackPanel.InformationGlo.VFDOnTimer = 0;
            }
            //if (!_AgilentWorker.IsBusy)
            //{
            //    _AgilentWorker.RunWorkerAsync();
            //}
            if (true)
            {
                #region 各个线程的调用20150911

                if (!_AgilentWorker.IsBusy)
                {
                    _AgilentWorker.RunWorkerAsync();
                }

                if (!_PLCWorker.IsBusy)
                {
                    _PLCWorker.RunWorkerAsync();
                }


                #endregion 各个线程的调用20150911
            }



            if (true)
            {

                #region 各个控制策略Strategy操作前台控件20150922

                #region 报警策略20150922
                if (BackPanel.Strategy.IfDonePLCDIPlay)
                {
                }
                else
                {
                    //20150930修改对应
                    PlayAlarmFound();
                    //跳转到相应的界面20150922
                    if (BackPanel.Strategy.IsTherePLC_Error)
                    {
                        TIAlarm.Focus();
                        //实验设备菜单关闭
                        ExperimentEquip.IsEnabled = false;
                        //被测机组惨淡关闭
                        Chiller.IsEnabled = false;

                        QuitTrial.IsEnabled = false;

                        //保证只运行一次！
                        BackPanel.Strategy.IfDonePLCDIPlay = true;
                    }

                }
                //报警函数：20150917改动


                #endregion 报警策略


                #endregion 各个控制策略Strategy操作前台控件20150922




                //场景选择
                switch (BackPanel.InformationGlo.senario)
                {
                    case BackPanel.InformationGlo.Senario.CarCooling:

                        // CarCooling.textbox1
                        //控制参数GlobelVar.Array_Agilent201_222[5-1];GlobelVar.Array_Agilent101_122[6-1]
                        //排气饱和温度20150919
                        //textBox9.Text = UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[5 - 1]).ToString("f2");
                        //TODO:可能冲突的地方(1)
                        textBox9.Text = GlobelVar.textBox9Text;

                        //排气压力20151012
                        textBox10.Text = Array_Agilent201_222[5 - 1].ToString("f3");
                        //吸气饱和温度20150919
                        //TODO:可能冲突的地方(2)
                        textBox11.Text = GlobelVar.textBox11Text;
                        textBox12.Text = Array_Agilent201_222[15 - 1].ToString("f3");

                        //吸气温度
                        textBox13.Text = Array_Agilent101_122[6 - 1].ToString("f2");
                        double CompressorRotateToDisplay = Array_Agilent201_222[16 - 1] * 300 / BackPanel.InformationGlo.CompressorDiameter_FromInfo;
                        textBox14.Text = Convert.ToInt32(CompressorRotateToDisplay).ToString();// .ToString("f2");
                        //冷却水温度：是控制器UT5读的20151012
                        //textBox15.Text = GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
                        textBox15.Text = Controllist[3].PV.ToString("f2");// GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
                        //压缩机环境温度
                        //textBox16.Text = Array_Agilent201_222[3 - 1].ToString("f2");
                        textBox16.Text = Array_Agilent201_222[2 - 1].ToString("f2");
                        //排气压力
                        textBox2.Text = GlobelVar.CarPressDischarge.ToString("f3");// 13.ToString("f2");
                        //吸气压力
                        textBox4.Text = GlobelVar.CarPressSuction.ToString("f3");// 3.ToString("f2");
                        //压缩机环境温度，都是和，Setbuttton有关的！
                        textBox8.Text = 65.ToString("f2");

                        //测量与计算参数GlobelVar.Array_Agilent201_222[5-1];GlobelVar.Array_Agilent101_122[6-1]
                        textBox17.Text = Array_Agilent101_122[7 - 1].ToString("f2");
                        textBox18.Text = Array_Agilent101_122[8 - 1].ToString("f2");
                        textBox19.Text = Array_Agilent201_222[19 - 1].ToString("f2");
                        textBox20.Text = Array_Agilent201_222[9 - 1].ToString("f3");
                        textBox21.Text = Array_Agilent201_222[4 - 1].ToString("f2");
                        //textBox22.Text = MainTimeTest.ToString("f2");
                        //textBox23.Text = MainTimeTest.ToString("f2");
                        textBox24.Text = Array_Agilent101_122[1 - 1].ToString("f2");
                        textBox25.Text = Array_Agilent101_122[9 - 1].ToString("f2");


                        //计算部分
                        textBox26.Text = CarCalculateResult[0].ToString("f3"); // GlobelVar.CalculateCar_Glo.A_RefrigFlowMass.ToString("f3");  //0
                        //TODO:可能冲突的地方(3)
                        textBox27.Text = GlobelVar.textBox27Text; //

                        textBox28.Text = CarCalculateResult[5].ToString("f3"); // GlobelVar.CalculateCar_Glo.ActualCompressPower.ToString("f3");
                        textBox29.Text = CarCalculateResult[3].ToString("f3");// 3 GlobelVar.CalculateCar_Glo.G_CoolingCapacity.ToString("f3");
                        textBox30.Text = CarCalculateResult[6].ToString("f2");// 6 GlobelVar.CalculateCar_Glo.AG_COP.ToString("f2");

                        textBox22.Text = CarCalculateResult[1].ToString("f3");// 1 GlobelVar.CalculateCar_Glo.A_CoolingCapacity.ToString("f3");
                        textBox23.Text = CarCalculateResult[4].ToString("f1");// 4 GlobelVar.CalculateCar_Glo.TestErr.ToString("f1");
                        //MeasureSetValue(MainTimeTest);
                        break;

                    case BackPanel.InformationGlo.Senario.CarNoise:
                        textBox9.Text = GlobelVar.textBox9Text;

                        //排气压力20151012
                        textBox10.Text = Array_Agilent201_222[5 - 1].ToString("f3");
                        //吸气饱和温度20150919
                        //TODO:可能冲突的地方(2)
                        textBox11.Text = GlobelVar.textBox11Text;
                        textBox12.Text = Array_Agilent201_222[15 - 1].ToString("f3");

                        //吸气温度
                        textBox13.Text = Array_Agilent101_122[6 - 1].ToString("f2");

                        CompressorRotateToDisplay = Array_Agilent201_222[16 - 1] * 300 / BackPanel.InformationGlo.CompressorDiameter_FromInfo;
                        textBox14.Text = Convert.ToInt32(CompressorRotateToDisplay).ToString();// .ToString("f2");
                        //冷却水温度：是控制器UT5读的20151012
                        //textBox15.Text = GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
                        textBox15.Text = Controllist[3].PV.ToString("f2");// GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
                        //压缩机环境温度
                        //textBox16.Text = Array_Agilent201_222[3 - 1].ToString("f2");
                        textBox16.Text = Array_Agilent201_222[2 - 1].ToString("f2");
                        //排气压力
                        textBox2.Text = GlobelVar.CarPressDischarge.ToString("f3");// 13.ToString("f2");
                        //吸气压力
                        textBox4.Text = GlobelVar.CarPressSuction.ToString("f3");// 3.ToString("f2");
                        //压缩机环境温度，都是和，Setbuttton有关的！
                        textBox8.Text = 65.ToString("f2");

                        //测量与计算参数GlobelVar.Array_Agilent201_222[5-1];GlobelVar.Array_Agilent101_122[6-1]
                        textBox17.Text = Array_Agilent101_122[7 - 1].ToString("f2");
                        textBox18.Text = Array_Agilent101_122[8 - 1].ToString("f2");
                        textBox19.Text = Array_Agilent201_222[19 - 1].ToString("f2");
                        textBox20.Text = Array_Agilent201_222[9 - 1].ToString("f3");
                        textBox21.Text = Array_Agilent201_222[4 - 1].ToString("f2");
                        //textBox22.Text = MainTimeTest.ToString("f2");
                        //textBox23.Text = MainTimeTest.ToString("f2");
                        textBox24.Text = Array_Agilent101_122[1 - 1].ToString("f2");
                        textBox25.Text = Array_Agilent101_122[9 - 1].ToString("f2");


                        //计算部分
                        textBox26.Text = CarCalculateResult[0].ToString("f3"); // GlobelVar.CalculateCar_Glo.A_RefrigFlowMass.ToString("f3");  //0
                        //TODO:可能冲突的地方(3)
                        textBox27.Text = GlobelVar.textBox27Text; //

                        textBox28.Text = CarCalculateResult[5].ToString("f3"); // GlobelVar.CalculateCar_Glo.ActualCompressPower.ToString("f3");
                        textBox29.Text = CarCalculateResult[3].ToString("f3");// 3 GlobelVar.CalculateCar_Glo.G_CoolingCapacity.ToString("f3");
                        textBox30.Text = CarCalculateResult[6].ToString("f2");// 6 GlobelVar.CalculateCar_Glo.AG_COP.ToString("f2");

                        textBox22.Text = CarCalculateResult[1].ToString("f3");// 1 GlobelVar.CalculateCar_Glo.A_CoolingCapacity.ToString("f3");
                        textBox23.Text = CarCalculateResult[4].ToString("f1");// 4 GlobelVar.CalculateCar_Glo.TestErr.ToString("f1");

                        //噪声记录
                        //textBox31.Text = 0.ToString("f2");
                        break;

                }

                #region 系统图显示20151126
                tb208.Text = Array_Agilent201_222[8 - 1].ToString("f3");
                tb104.Text = Array_Agilent101_122[4 - 1].ToString("f2");
                tb209.Text = Array_Agilent201_222[9 - 1].ToString("f3");
                tb210.Text = Array_Agilent201_222[10 - 1].ToString("f3");
                tb105.Text = Array_Agilent101_122[5 - 1].ToString("f2");
                tb215.Text = Array_Agilent201_222[15 - 1].ToString("f3");
                tb106.Text = Array_Agilent101_122[6 - 1].ToString("f2");
                tb107.Text = Array_Agilent101_122[7 - 1].ToString("f2");
                tb207.Text = Array_Agilent201_222[7 - 1].ToString("f3");
                tb103.Text = Array_Agilent101_122[3 - 1].ToString("f2");
                tb206.Text = Array_Agilent201_222[6 - 1].ToString("f3");
                tb102.Text = Array_Agilent101_122[2 - 1].ToString("f2");

                tb205.Text = Array_Agilent201_222[5 - 1].ToString("f3");
                tb101.Text = Array_Agilent101_122[1 - 1].ToString("f2");

                tb109.Text = Array_Agilent101_122[9 - 1].ToString("f2");
                tb108.Text = Array_Agilent101_122[8 - 1].ToString("f2");

                tb219.Text = Array_Agilent201_222[19 - 1].ToString("f2");

                tbUT5.Text = SupplyWaterTemp.ToString("f2");

                tb204.Text = Array_Agilent201_222[4 - 1].ToString("f2");
                double CompressorRotateToDisplay_ForSystem = Array_Agilent201_222[16 - 1] * 300 / BackPanel.InformationGlo.CompressorDiameter_FromInfo;
                tb216.Text = Convert.ToInt32(CompressorRotateToDisplay_ForSystem).ToString();
                #endregion 系统图显示

            }

            if (GlobelVar.IsInfoFromMain)
            {
                textBox6.Text = Convert.ToInt32(BackPanel.InformationGlo.CarCompressorRotateSet_ForControl).ToString();
            }
            GlobelVar.IsInfoFromMain = false;

            //获得globelvar.IsStableCar的状态
            StableJudgeFun_ForCar(CarCalculateResult[1], CarCalculateResult[3], Array_Agilent101_122, Array_Agilent201_222, Controllist);

            #region  稳定块

            if (!_StableWorker.IsBusy)
            {
                _StableWorker.RunWorkerAsync();
            }
            //时间初始化
            if (StableTimePlayIni == true)
            {

                StableTimePlay.Text = "00:00:00";
            }

            string IsStable_TextForPlay="不稳定";
            if (GlobelVar.IsStableCar)
            {
                StableTimer++;
                sec = StableTimer % 60;
                min = (StableTimer / 60) % 60;
                hou = StableTimer / 3600;
                //StableRecordList.Add(textBox17.Text);
                //StableTimePlay.Text = string.Format("{0:00}:{1:00}:{2:00}", hou, min, sec);
                IsStable_TextForPlay = "稳定";
            }
            else
            {
                StableTimer = 0;

                sec = StableTimer % 60;
                min = (StableTimer / 60) % 60;
                hou = StableTimer / 3600;
                IsStable_TextForPlay = "不稳定";
            }

            tbkIsStable.Text = IsStable_TextForPlay;

            StableTimePlay.Text = string.Format("{0:00}:{1:00}:{2:00}", hou, min, sec);
            #endregion

            GC.Collect();
        }
        /// <summary>
        /// 由于辅助线程不能，获得UI的控件内容，所以借助这个可以：StableRecordList.Add(AddTextMidTemp);
        /// </summary>
        //private string AddTextMidTemp = "helloworld";
        /// <summary>
        /// 稳定开始函数
        /// </summary>
        private void StableStart()
        {
            StableTimePlayIni = false;

            StableTimersec = StableTimersec + 1;

            if ((StableTimersec % (GlobelVar.RecordSpanSec)) == 2)
            {
              
                if (CurrentIndex < RecordNumber)
                {
                    //StableRecordList.Add(textBox17.Text);
                    //GlobelVar.StableRecordList_Glo.Add(textBox17.Text);

                    //插入到须要的数组20151218
                    DoubleListForReport_FromFrontPanel.AddData2(GlobelVar.DoubleDataForCarReport, CurrentIndex);
                }
                else
                {
                    //MessageBox.Show("已经记录了" + CurrentIndex + "组" + "，而且最后一个是" + StableRecordList[CurrentIndex - 1]);
                    //为了只让出现一次
                    iniStableTimerStart = 1;

                    MessageBox.Show("已经记录了" + CurrentIndex + "组，请点击停止按钮，停止自动记录。");
                    return;
                }

                CurrentIndex++;
                if (CurrentIndex > 4)
                {
                    CurrentIndex = 4;
                }

            }



            //int sec, min, hou;
            //sec = StableTimersec % 60;
            //min = (StableTimersec / 60) % 60;
            //hou = StableTimersec / 3600;



        }
        /// <summary>
        /// 每次主钟扫一次，如果是true则稳定事件变为：00：00：00
        /// </summary>
        private bool StableTimePlayIni = false;
        private void StableEnd()
        {
            GlobelVar.StableRecordList_Glo.Clear();

            Report.DoubleListForReport_FromFrontPanel.IniData_ForCar();

            //StableTimePlay.Text = "00:00:00";
            StableTimePlayIni = true;

            StableTimersec = 0;
        }




        ////主钟测试变量
        //public double MainTimeTest = 0;



        //测量参数赋值textbox17-30
        public void MeasureSetValue(double temp)
        {
            textBox17.Text = temp.ToString("f2");
            textBox18.Text = temp.ToString("f2");
            textBox19.Text = temp.ToString("f2");
            textBox20.Text = temp.ToString("f2");
            textBox21.Text = temp.ToString("f2");
            textBox22.Text = temp.ToString("f2");
            textBox23.Text = temp.ToString("f2");

            textBox24.Text = temp.ToString("f2");
            textBox25.Text = temp.ToString("f2");
            textBox26.Text = temp.ToString("f2");
            textBox27.Text = temp.ToString("f2");
            textBox28.Text = temp.ToString("f2");
            textBox29.Text = temp.ToString("f2");
            textBox30.Text = temp.ToString("f2");

        }
        #endregion

        #region 出现警告后的处理

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            //GlobelVar.ImmediateStopIsError = true;
            //IsAlarmFound();
        }
        /// <summary>
        /// 出现错误：显示出来20150918：20150930修改和list对应
        /// </summary>
        private void PlayAlarmFound()
        {
            //如果"急停"找到错误：对
            if (BackPanel.PLCMod.PLCDIList[0].IsAlerting == true)
            {
                ellipse1.Visibility = Visibility.Hidden;
                ellipse2.Visibility = Visibility.Visible;
                //textBox32.Text = DateTime.Now.ToUniversalTime().ToString();
                //textBox32.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                textBox32.Text = BackPanel.PLCMod.PLCDIList[0].AlertTimeStamp;
                //BackPanel.PLCMod.PLCDIList[0].AlertTimeStamp;
                //MessageBox.Show("急停报警！正在关闭相应设备！");
            }
            else
            {
                ellipse1.Visibility = Visibility.Visible;
                ellipse2.Visibility = Visibility.Hidden;
            }

            //"水箱液位"：对
            if (BackPanel.PLCMod.PLCDIList[1].IsAlerting == true)
            {
                ellipse3.Visibility = Visibility.Hidden;
                ellipse4.Visibility = Visibility.Visible;
                //textBox33.Text = DateTime.Now.ToUniversalTime().ToString();
                textBox33.Text = BackPanel.PLCMod.PLCDIList[1].AlertTimeStamp;
                //MessageBox.Show("水箱低液位报警！正在关闭相应设备！");
            }
            else
            {
                ellipse3.Visibility = Visibility.Visible;
                ellipse4.Visibility = Visibility.Hidden;

            }

            //"水流开关"：20150930  2改3
            if (BackPanel.PLCMod.PLCDIList[3].IsAlerting == true)
            {
                ellipse5.Visibility = Visibility.Hidden;
                ellipse6.Visibility = Visibility.Visible;
                //textBox34.Text = DateTime.Now.ToUniversalTime().ToString();
                textBox34.Text = BackPanel.PLCMod.PLCDIList[3].AlertTimeStamp;
                //MessageBox.Show("水流开关报警！正在关闭相应设备！");
            }
            else
            {
                ellipse5.Visibility = Visibility.Visible;
                ellipse6.Visibility = Visibility.Hidden;
            }

            //"压缩冷凝机组高低压"20150930  3改4，5
            if (BackPanel.PLCMod.PLCDIList[4].IsAlerting == true || BackPanel.PLCMod.PLCDIList[5].IsAlerting == true)
            {
                ellipse7.Visibility = Visibility.Hidden;
                ellipse8.Visibility = Visibility.Visible;
                //textBox35.Text = DateTime.Now.ToUniversalTime().ToString();
                if (BackPanel.PLCMod.PLCDIList[4].IsAlerting == true)
                {
                    textBox35.Text = BackPanel.PLCMod.PLCDIList[4].AlertTimeStamp;
                }
                if (BackPanel.PLCMod.PLCDIList[5].IsAlerting == true)
                {
                    textBox35.Text = BackPanel.PLCMod.PLCDIList[5].AlertTimeStamp;
                }
                //textBox35.Text = BackPanel.PLCMod.PLCDIList[4].AlertTimeStamp;
                //MessageBox.Show("压缩冷凝机组高低压报警！正在关闭相应设备！");
            }
            else
            {
                ellipse7.Visibility = Visibility.Visible;
                ellipse8.Visibility = Visibility.Hidden;
            }

            //"量热器高压"20150930  4改2
            if (BackPanel.PLCMod.PLCDIList[2].IsAlerting == true)
            {
                ellipse9.Visibility = Visibility.Hidden;
                ellipse10.Visibility = Visibility.Visible;
                textBox36.Text = BackPanel.PLCMod.PLCDIList[2].AlertTimeStamp;
                //MessageBox.Show("量热器高压报警报警！正在关闭相应设备！");
            }
            else
            {
                ellipse9.Visibility = Visibility.Visible;
                ellipse10.Visibility = Visibility.Hidden;
            }

        }
        #endregion

        /// <summary>
        /// 重置报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarCoolingAlarmReset_Click(object sender, RoutedEventArgs e)
        {
            //20150929改
            MessageBoxResult mbr = MessageBox.Show("是否把报警复位？", "报警复位", MessageBoxButton.OKCancel);
            if (mbr == MessageBoxResult.OK)
            {

                //this.Close();
                //Car car = new Car();
                //car.Show();
                //20150929改
                //GlobelVar.GlobeVarIni_Alarm();
                AlarmDateInitiate();
                //20150911加！20150930改
                BackPanel.Strategy.StrategyPLCReset();

                ////添加20150929
                ////实验设备停止
                //BackPanel.Strategy.StrategyPLCExperimentEquipStop();
                ////被测机组停止
                //BackPanel.Strategy.StrategyPLCChillerStop();

                //下面是菜单操作
                ExperimentEquip.IsEnabled = false;
                //ExperimentEquipStart.IsEnabled = true;
                //ExperimentEquipStop.IsEnabled = false;

                Chiller.IsEnabled = false;

                QuitTrial.IsEnabled = true;

                //报警重置标识符
                BackPanel.InformationGlo.IsAlertingReset = true;

                QuitTrial.Focus();
            }
            else
            {
                //BackPanel.Strategy.IsReturnFromMain = true;

                //Car car = new Car();
                //car.Show();
                //this.Close();

            }

        }

        #region 函数所有需要用到的函数

        /// <summary>
        /// 报警界面，日期框的格式初始化！
        /// </summary>
        private void AlarmDateInitiate()
        {

            textBox32.Text = "00-00-00  00:00:00";
            textBox33.Text = "00-00-00  00:00:00";
            textBox34.Text = "00-00-00  00:00:00";
            textBox35.Text = "00-00-00  00:00:00";
            textBox36.Text = "00-00-00  00:00:00";

        }

        #endregion

        #region  试验设备开停逻辑
        /// <summary>
        /// 实验设备开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExperimentEquipStart_Click(object sender, RoutedEventArgs e)
        {
            QuitTrial.IsEnabled = false;
            ExperimentEquipStart.IsEnabled = false;
            ExperimentEquipStop.IsEnabled = true;
            Chiller.IsEnabled = true;
            ChillerStart.IsEnabled = true;
            ChillerStop.IsEnabled = false;

            #region PLC控制策略：20150923
            BackPanel.PLCMod.WaterSupply(true);
            BackPanel.PLCMod.VFD(true);
            BackPanel.Control.ControlUnLockUT6_ForCar();
            #endregion PLC控制策略：20150923
        }
        /// <summary>
        /// 实验设备停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExperimentEquipStop_Click(object sender, RoutedEventArgs e)
        {
            QuitTrial.IsEnabled = true;
            Chiller.IsEnabled = false;
            ExperimentEquipStop.IsEnabled = false;
            ExperimentEquipStart.IsEnabled = true;

            #region PLC控制策略：20150923
            //BackPanel.Strategy.StrategyPLCExperimentEquipStop();
            BackPanel.PLCMod.WaterSupply(false);
            BackPanel.PLCMod.VFD(false);
            BackPanel.Control.ControlLockUT6_ForCar();
            #endregion PLC控制策略：20150923
        }
        /// <summary>
        /// 机组开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChillerStart_Click(object sender, RoutedEventArgs e)
        {


            if (BackPanel.InformationGlo.VFDOnTimer < 900)
            {
                if (MessageBox.Show("电机运转未稳，是否强行开启压缩机！", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //MessageBox.Show("OK");
                    double queueTotal = 0;
                    int Number = 0;
                    double MeanValue;
                    foreach (double v in BackPanel.InformationGlo.TorqueList)
                    {
                        queueTotal += v;
                        if (v != 0)
                        {
                            Number++;
                        }
                    }
                    MeanValue = queueTotal / Number;
                    InformationGlo.Torque_0 = MeanValue;

                    //参数
                    Report.ReportParameterMySelf.RP20IniTorque = MeanValue.ToString("f2");


                    ExperimentEquip.IsEnabled = false;
                    QuitTrial.IsEnabled = false;
                    ChillerStop.IsEnabled = true;
                    ChillerStart.IsEnabled = false;
                    #region PLC控制策略：20150923
                    //BackPanel.Strategy.StrategyPLCChillerStart();
                    BackPanel.PLCMod.CarCompressorOn(true);
                    BackPanel.Control.ControlUnLockUT1UT2_ForCar();
                    #endregion PLC控制策略：20150923

                    GlobelVar.IsCarChillerOn = true;
                }
                else
                {
                    //MessageBox.Show("No");
                }
            }
            else
            {

                if (MessageBox.Show("压缩机将要启动。", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //MessageBox.Show("OK");
                    double queueTotal = 0;
                    int Number = 0;
                    double MeanValue;
                    foreach (double v in BackPanel.InformationGlo.TorqueList)
                    {
                        queueTotal += v;
                        if (v != 0)
                        {
                            Number++;
                        }
                    }
                    MeanValue = queueTotal / Number;
                    InformationGlo.Torque_0 = MeanValue;

                    ExperimentEquip.IsEnabled = false;
                    QuitTrial.IsEnabled = false;
                    ChillerStop.IsEnabled = true;
                    ChillerStart.IsEnabled = false;
                    #region PLC控制策略：20150923
                    //BackPanel.Strategy.StrategyPLCChillerStart();
                    BackPanel.PLCMod.CarCompressorOn(true);
                    BackPanel.Control.ControlUnLockUT1UT2_ForCar();
                    #endregion PLC控制策略：20150923


                    GlobelVar.IsCarChillerOn = true;
                }
                else
                {
                    //MessageBox.Show("No");
                }

            }


        }
        /// <summary>
        /// 机组停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChillerStop_Click(object sender, RoutedEventArgs e)
        {
            QuitTrial.IsEnabled = false;
            ChillerStart.IsEnabled = true;
            ChillerStop.IsEnabled = false;
            ExperimentEquip.IsEnabled = true;
            ExperimentEquipStart.IsEnabled = false;
            ExperimentEquipStop.IsEnabled = true;

            #region PLC控制策略：20150923
            //BackPanel.Strategy.StrategyPLCChillerStop();
            BackPanel.PLCMod.CarCompressorOn(false);
            BackPanel.Control.ControlLockUT1UT2_ForCar();
            #endregion PLC控制策略：20150923

            GlobelVar.IsCarChillerOn = false;
        }
        #endregion
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TS_Car_Click(object sender, RoutedEventArgs e)
        {
            //20150928改
            //Debug.MainWindow TSChiller = new Debug.MainWindow();
            _DEBUG.MainWindow TSChiller = new _DEBUG.MainWindow();
            TSChiller.ShowDialog();
        }




        #region 稳定时间模块
        /// <summary>
        /// 记录list:不用了！换成GlobelVar.StableRecordList_Glo 20151126
        /// </summary>
        private List<string> StableRecordList = new List<string>();

        //需要记录次数
        private int RecordNumber = 4;

        //当前记录
        int CurrentIndex = 0;
        /// <summary>
        /// 手动记录被激活的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManuRecord_Checked(object sender, RoutedEventArgs e)
        {
            //定义手动记录个数
            RecordNumber = 4;
            //当前指数
            CurrentIndex = 0;
            //全局变量是否自动记录，值改为假！
            GlobelVar.StableRecordIsAuto = false;

            //记录和删除两个按钮可用！
            btDelete.Visibility = Visibility.Visible;
            btRecording.Visibility = Visibility.Visible;
            btRecording.IsEnabled = true;
            btDelete.IsEnabled = true;

            btRecording.Content = "记录";
            btDelete.Content = "删除";

            //后来发现，要清空之前stablerecordlist！！！！！
            //StableRecordList.Clear();
            GlobelVar.StableRecordList_Glo.Clear();

            //显示时间永远是000000
            StableTimePlay.Text = "00:00:00";
            StableTimePlay.FontSize = 25;

            //永远是不稳定的！
            GlobelVar.IsStableCar = false;
            iniStableTimerStart = 1;

            //timer_StableTemp.Stop();
        }
        /// <summary>
        /// 自动记录被激活的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoRecordCar_Checked(object sender, RoutedEventArgs e)
        {
            //定义手动记录个数
            RecordNumber = 4;
            //当前指数
            CurrentIndex = 0;
            //全局变量是否自动记录，值改为真！
            GlobelVar.StableRecordIsAuto = true;

            //把record记录按钮隐去
            btRecording.Visibility = Visibility.Hidden;
            //把delet按钮改造
            btDelete.IsEnabled = true;
            btDelete.Content = "停止记录";

            //后来发现，要清空之前stablerecordlist！！！！！
            //StableRecordList.Clear();
            GlobelVar.StableRecordList_Glo.Clear();

            iniStableTimerStart = 0;
            GlobelVar.IsStableCar = false;
            //StableTimer_TempStart();
        }
        #endregion

        /// <summary>
        /// 记录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRecording_Click(object sender, RoutedEventArgs e)
        {
            //int temp=0;
            if (CurrentIndex < RecordNumber)
            {
                //StableRecordList.Add(textBox17.Text);
                //GlobelVar.StableRecordList_Glo.Add(textBox17.Text);

                //插入到须要的数组20151218
                DoubleListForReport_FromFrontPanel.AddData2(GlobelVar.DoubleDataForCarReport, CurrentIndex);
            }
            else
            {
                //MessageBox.Show("已经记录了" + CurrentIndex + "组" + "，而且最后一个是" + StableRecordList[CurrentIndex - 1]);
                MessageBox.Show("已经记录了" + CurrentIndex + "组");
                return;

            }

            CurrentIndex++;
            if (CurrentIndex > 4)
            {
                CurrentIndex = 4;
            }
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            //switch(btDelete.Content.ToString())
            //{
            //    case

            //}
            if (btDelete.Content.ToString() == "停止记录")
            {
                //timer_Stable.Stop();
                //timer_StableTemp.Stop();

                iniStableTimerStart = 1;

                //btDelete.IsEnabled = false;

                ManuRecord.IsChecked = true;
                AutoRecordCar.IsChecked = false;

                MessageBox.Show("自动记录停止，请重新选择!");
            }
            else
            {
                //if (GlobelVar.StableRecordList_Glo.Count > 0)
                //{
                //    //StableRecordList.RemoveAt(CurrentIndex - 1);
                //    GlobelVar.StableRecordList_Glo.RemoveAt(CurrentIndex-1);
                //    CurrentIndex--;
                //}
                //else
                //{
                //    MessageBox.Show("现在没有记录");
                //    return;
                //}
                if (CurrentIndex >= 1)
                {
                    DoubleListForReport_FromFrontPanel.DeleteData3(CurrentIndex);
                }
                else
                {
                    MessageBox.Show("现在没有记录");
                    return;
                }

                CurrentIndex--;

                if (CurrentIndex < 0)
                {
                    CurrentIndex = 0;
                }
            }

        }

        #region 暂时的函数
        //
        //System.Windows.Threading.DispatcherTimer timer_StableTemp=new System.Windows.Threading.DispatcherTimer();
        //private void StableTimer_TempStart()
        //{
        //    //timer_StableTemp = new System.Windows.Threading.DispatcherTimer();
        //    timer_StableTemp.Interval = new TimeSpan(0, 0,3);
        //    timer_StableTemp.Tick += new EventHandler(timeStableTemp_Tick);
        //    timer_StableTemp.Start();
        //}
        /// <summary>
        /// 是否运行稳定模块，0运行，1不运行
        /// </summary>
        int iniStableTimerStart = 1;

        //private void timeStableTemp_Tick(object sender, EventArgs e)
        //{
        //    ////如果稳定
        //    //if (IsStableCar)
        //    //{

        //    //    //StableRecordList.Add(textBox17.Text);
        //    //    //if (StableRecordList.Count == 5)
        //    //    //{
        //    //    //    timer_Stable.Stop();
        //    //    //    MessageBox.Show("已经记录了5条记录");

        //    //    //    return;
        //    //    //}

        //    //    //timer_StableTemp.Stop();
        //    //    if (iniStableTimerStart == 0)
        //    //    {
        //    //        StableTimerStart();
        //    //        iniStableTimerStart = 1;
        //    //    }

        //    //}
        //    //else
        //    //{
        //    //    timer_Stable.Stop();

        //    //    StableRecordList.Clear();
        //    //    StableTimePlay.Text = "00:00:00";

        //    //    StableTimersec = 0;

        //    //    iniStableTimerStart = 0;
        //    //}


        //}


        #endregion






        //
        ///// <summary>
        ///// 状态判定是否稳定
        ///// </summary>
        //private bool IsStableCar=false;


        ///// <summary>
        ///// 设定自动记录时间间隔 时间单位s
        ///// </summary>
        //private int RecordSpanSec = 5;

        #region 稳定记录时间20150911

        /// <summary>
        /// 稳定时间计时器，单位为秒：自动记录的计时器
        /// </summary>
        private int StableTimersec = 0;


        /// <summary>
        /// 前台显示时间的计时器20151224
        /// </summary>
        private int StableTimer = 0;

        /// <summary>
        /// 前台显示的钟表：把稳定时间换算为秒
        /// </summary>
        int sec, min, hou;
        #endregion 稳定记录时间20150911

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (GlobelVar.IsStableCar == true)
            {
                //bttemp.Background = new SolidColorBrush(Colors.Green);
                button2.Background = new SolidColorBrush(Colors.Red);
                GlobelVar.IsStableCar = false;
            }
            else
            {
                button2.Background = new SolidColorBrush(Colors.Green);
                GlobelVar.IsStableCar = true;
            }
        }


        /// <summary>
        /// 试验参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_info_TestCar_Click(object sender, RoutedEventArgs e)
        {

            CarInfo newCarfo = new CarInfo();
            newCarfo.CatchRecord.Visibility = Visibility.Hidden;

            #region 把数据库中的数据读上来，并且显示,20150920
            //把数据库的东西读上来
            BackPanel.DBOperate.GetLastInfoRecordFromDateBase(BackPanel.InformationGlo.DBPath, GlobelVar.TestInfoDefGlo.TestInfo);
            //GlobelVar.TestInfoDefGlo.TestInfo[0] = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM/dd");
            //GlobelVar.TestInfoDefGlo.TestInfo[1] = DateTime.Now.ToString("HH:mm:ss");
            //GlobelVar.TestInfoDefGlo.TestInfo[2] = "Car";
            newCarfo.textBox1.Text = GlobelVar.TestInfoDefGlo.TestInfo[3];
            newCarfo.textBox2.Text = GlobelVar.TestInfoDefGlo.TestInfo[4];
            newCarfo.textBox3.Text = GlobelVar.TestInfoDefGlo.TestInfo[5];
            newCarfo.textBox4.Text = GlobelVar.TestInfoDefGlo.TestInfo[6];
            newCarfo.textBox5.Text = GlobelVar.TestInfoDefGlo.TestInfo[7];
            newCarfo.textBox6.Text = GlobelVar.TestInfoDefGlo.TestInfo[8];
            newCarfo.textBox7.Text = GlobelVar.TestInfoDefGlo.TestInfo[9];
            //GlobelVar.TestInfoDefGlo.TestInfo[10] = "--";l

            #region 20151020添加读取制冷剂名称
            //制冷剂名称20151020
            GlobelVar.RefName = GlobelVar.TestInfoDefGlo.TestInfo[12];
            //制冷剂选择后前台的响应，以及动作
            if (GlobelVar.RefName == "R22")
            {
                newCarfo.RBR22.IsChecked = true;

                UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R22;
            }
            else
            {
                newCarfo.RBR134a.IsChecked = true;

                UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R134a;
            }
            //锁定在下面
            #endregion 20151020添加各个信息

            #region 20151108添加读取压缩机离合器电压读取
            GlobelVar.CarCompressorClutchVoltage = GlobelVar.TestInfoDefGlo.TestInfo[13];
            //if (GlobelVar.CarCompressorClutchVoltage == "24")
            if (GlobelVar.CarCompressorClutchVoltage == "24V")
            {
                newCarfo.RB24V.IsChecked = true;
            }
            else
            {
                newCarfo.RB12V.IsChecked = true;
            }
            #endregion

            #endregion 把数据库中的数据读上来，并且显示,20150920

            #region 锁定几个信息20150921
            newCarfo.textBox1.IsEnabled = false;
            newCarfo.textBox2.IsEnabled = false;
            newCarfo.textBox3.IsEnabled = false;


            if (GlobelVar.IsCarChillerOn)
            {
                newCarfo.textBox4.IsEnabled = false;
                newCarfo.textBox5.IsEnabled = false;
                newCarfo.textBox6.IsEnabled = false;
                newCarfo.textBox7.IsEnabled = false;

                newCarfo.button2.IsEnabled = false;
            }
            else
            {
                newCarfo.textBox4.IsEnabled = true;
                newCarfo.textBox5.IsEnabled = true;
                newCarfo.textBox6.IsEnabled = true;
                newCarfo.textBox7.IsEnabled = true;

                newCarfo.button2.IsEnabled = true;
 
            }
            

            newCarfo.CatchRecord.IsEnabled = false;
            //新添加的两个框架的锁定20151020
            newCarfo.RBR22.IsEnabled = false;
            newCarfo.RBR134a.IsEnabled = false;
            //新添加锁定
            newCarfo.RB12V.IsEnabled = false;
            newCarfo.RB24V.IsEnabled = false;
            #endregion 锁定几个信息20150921



            newCarfo.ShowDialog();

        }

        private void menu_info_RecordTime_Click(object sender, RoutedEventArgs e)
        {
            SetStableTimeWindow setstabletimewindow = new SetStableTimeWindow();
            setstabletimewindow.ShowDialog();
        }

        private void menu_Set_StableCoef_Click(object sender, RoutedEventArgs e)
        {
            StableJudgeConditionOfCar stableCar = new StableJudgeConditionOfCar();
            stableCar.ShowDialog();
        }

        #region  给GolbelVar.IsStableCar赋值
        /// <summary>
        /// 判断现在的状态是否稳定：是则GolbelVar.IsStableCar为true；否则为false；
        /// </summary>
        /// 

        private void StableJudgeFun_ForCar(double A_CoolCapacity, double G_CoolCapacity, double[] Array_Agilent101_122, double[] Array_Agilent201_222, List<BackPanel.Control.UT35ADef> Controllist)
        {
            #region  临时20151225
           
            ////double MidTemp1, MidTemp2, MidTemp3, MidTemp4, MidTemp5;
            //textBox9.Text = GlobelVar.textBox9Text;

            ////排气压力20151012
            //textBox10.Text = Array_Agilent201_222[5 - 1].ToString("f3");
            ////吸气饱和温度20150919
            ////TODO:可能冲突的地方(2)
            //textBox11.Text = GlobelVar.textBox11Text;
            //textBox12.Text = Array_Agilent201_222[15 - 1].ToString("f3");

            ////吸气温度
            //textBox13.Text = Array_Agilent101_122[6 - 1].ToString("f2");
            ////double CompressorRotateToDisplay = Array_Agilent201_222[16 - 1] * 300 / BackPanel.InformationGlo.CompressorDiameter_FromInfo;
            //textBox14.Text = Convert.ToInt32(CompressorRotateToDisplay).ToString();// .ToString("f2");
            ////冷却水温度：是控制器UT5读的20151012
            ////textBox15.Text = GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
            //textBox15.Text = Controllist[3].PV.ToString("f2");// GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
            ////压缩机环境温度
            ////textBox16.Text = Array_Agilent201_222[3 - 1].ToString("f2");
            //textBox16.Text = Array_Agilent201_222[2 - 1].ToString("f2");
            #endregion 临时20151225

            double DischargePressureDif, SuctionPressureDif, SuctionTemperatureDif, RotateDif, EnvirTempOfCompressorDif, CoolingCapacityDif;
            //double DisPressure_Set = Convert.ToDouble(textBox2.Text);
            double DisPressure_Set = GlobelVar.CarDischargePressureSetOnlyStable;
            double DisPressure_Act = Array_Agilent201_222[5 - 1];
            DischargePressureDif = Math.Abs(DisPressure_Set - DisPressure_Act);

            double SucPressure_Set = GlobelVar.CarInputPressureSetOnlyStable;
            //double SucPressure_Act = Convert.ToDouble(textBox12.Text);
            double SucPressure_Act = Array_Agilent201_222[15 - 1];
            SuctionPressureDif = Math.Abs(SucPressure_Set - SucPressure_Act);

            double SucTemperature_Set = GlobelVar.CarInputTemperatureSetOnlyStable;
            double SucTemperature_Act = Array_Agilent101_122[6 - 1];
            SuctionTemperatureDif = Math.Abs(SucTemperature_Set - SucTemperature_Act);

            double Rotate_Set = GlobelVar.CarCompressorRotateSetOnlyStable;
            //double Rotate_Act = Convert.ToDouble(textBox14.Text);
            double CompressorRotateToDisplay = Array_Agilent201_222[16 - 1] * 300 / BackPanel.InformationGlo.CompressorDiameter_FromInfo;
            double Rotate_Act = CompressorRotateToDisplay;
            RotateDif = Math.Abs(Rotate_Set - Rotate_Act);

            double EnvirtempOfCom_Set = 65;
            //double EnvirtempOfCom_Act = Convert.ToDouble(textBox16.Text);
            double EnvirtempOfCom_Act = Array_Agilent201_222[2 - 1];
            
            EnvirTempOfCompressorDif = EnvirtempOfCom_Act - EnvirtempOfCom_Set;

            double A_CoolingCapacity = A_CoolCapacity;
            double G_CoolingCapacity = G_CoolCapacity;
            CoolingCapacityDif = Math.Abs((A_CoolingCapacity - G_CoolingCapacity) / (A_CoolingCapacity + G_CoolingCapacity) * 2);

            //依据控制UT2吸气温度而定
            if (BackPanel.InformationGlo.IsClutchOn)
            {
                if (SuctionTemperatureDif <= 2)
                {
                    //小于2度，锁定
                    BackPanel.Control.Set(2, "MOD", 1, 0);
                }
                if (SuctionTemperatureDif > 2)
                {
                    BackPanel.Control.Set(2, "MOD", 0, 0);
                }
            }

            if (StableJudgeFun_ForCar_Child(DischargePressureDif, SuctionPressureDif, SuctionTemperatureDif, RotateDif, EnvirTempOfCompressorDif, CoolingCapacityDif))
            {
                GlobelVar.IsStableCar = true;
            }
            else
            {
                GlobelVar.IsStableCar = false;
            }
            //20150929改到
            //MidTemp1 = Math.Abs(GlobelVar.CarDischargeTemperatureSet - Convert.ToDouble(textBox9.Text));
            //MidTemp2 = Math.Abs(GlobelVar.CarInputSaturateTemperatureSet - Convert.ToDouble(textBox11.Text));
            //MidTemp3 = Math.Abs(GlobelVar.CarInputTemperatureSet - Convert.ToDouble(textBox13.Text));
            //MidTemp4 = Math.Abs(GlobelVar.CarCompressorRotateSet - Convert.ToDouble(textBox14.Text));
            //MidTemp5 = Math.Abs(GlobelVar.CarCoolingWaterSet - Convert.ToDouble(textBox15.Text));

            //再改正20151013，全部用后台
            //MidTemp1 = Math.Abs(GlobelVar.CarDischargeTemperatureSet - UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[5 - 1]));


            //MidTemp1 = Math.Abs(GlobelVar.CarDischargeTemperatureSet - GlobelVar.MidTemp1_REF);

            //MidTemp2 = Math.Abs(GlobelVar.CarInputSaturateTemperatureSet - GlobelVar.MidTemp2_REF);
            ////TODO:...2
            //MidTemp3 = Math.Abs(GlobelVar.CarInputTemperatureSet - Array_Agilent101_122[6 - 1]);


            //MidTemp4 = Math.Abs(GlobelVar.CarCompressorRotateSet - Array_Agilent201_222[16 - 1]);
            //MidTemp5 = Math.Abs(GlobelVar.CarCoolingWaterSet - SupplyWaterTemp);

            //GlobelVar.CarDischargeTemperatureActual_Dif = MidTemp1;
            //GlobelVar.CarInputSaturateTemperatureActual_Dif = MidTemp2;
            //GlobelVar.CarInputTemperatureActual_Dif = MidTemp3;
            //GlobelVar.CarCompressorRotateActual_Dif = MidTemp4;
            //GlobelVar.CarCoolingWaterActual_Dif = MidTemp5;

        }
        /// <summary>
        /// 为了返回现在的误差，是否在要求的误差之内：是则返回true，否则false
        /// </summary>
        /// <returns></returns>
        private bool StableJudgeFun_ForCar_Child(double DischargePressureDif, double SuctionPressureDif, double SuctionTemperatureDif, double RotateDif, double EnvirTempOfCompressorDif, double CoolingCapacityDif)
        {
            bool temp1, temp2, temp3, temp4, temp5, temp6;
            if (DischargePressureDif <= GlobelVar.CarDischargePressureRequire_Dif)
            { temp1 = true; }
            else
            { temp1 = false; }

            if (SuctionPressureDif <= GlobelVar.CarSuctionPressureRequire_Dif)
            { temp2 = true; }
            else
            { temp2 = false; }

            if (SuctionTemperatureDif <= GlobelVar.CarSuctionTemperatureRequire_Dif)
            { temp3 = true; }
            else
            { temp3 = false; }

            if (RotateDif <= GlobelVar.CarRotateRequire_Dif)
            { temp4 = true; }
            else
            { temp4 = false; }

            if (EnvirTempOfCompressorDif >= 0)
            { temp5 = true; }
            else
            { temp5 = false; }

            //添加压缩机温度判断条件20150925
            if (CoolingCapacityDif <= GlobelVar.CarAGcoolingCapacity_Dif)
            {
                temp6 = true;
            }
            else
            {
                temp6 = false;
            }


            if (temp1 && temp2 && temp3 && temp4 && temp5 && temp6)
            {
                return true;
            }
            else
            {
                return false;
            }

        }




        #endregion


        #region 辅助线程

        #region 曲线
        ///// <summary>
        ///// 现实的通道
        ///// </summary>
        //private Dictionary<int, Channel> Channels;
        /// <summary>
        /// 通道初始化函数
        /// </summary>
        private void CurveIni()
        {
            #region 通道添加20150910 ：20150911抽象出函数
            GlobleFun.AddChannel_Total();
            ////Agilent测量通道0-24：20150910
            //Channel TestChannel0 = new Channel(0, 0, "压缩机/冷水机组出口制冷剂压力", 1, "MPa", "压力", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel1 = new Channel(1, 1, "压缩机/冷水机组出口制冷剂温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel2 = new Channel(2, 2, "冷凝器进口制冷剂气体压力", 1, "MPa", "压力", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel3 = new Channel(3, 3, "冷凝器进口制冷剂气体温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel4 = new Channel(4, 4, "冷凝器出口制冷剂液体压力", 1, "MPa", "压力", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel5 = new Channel(5, 5, "冷凝器出口制冷剂液体温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel6 = new Channel(6, 6, "膨胀阀前制冷剂液体压力", 1, "MPa", "压力", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel7 = new Channel(7, 7, "膨胀阀前制冷剂液体温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel8 = new Channel(8, 8, "量热器第二制冷剂压力", 1, "MPa", "压力", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel9 = new Channel(9, 9, "量热器出口制冷剂气体压力", 1, "MPa", "压力", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel10 = new Channel(10, 10, "量热器出口制冷剂气体温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel11 = new Channel(11, 11, "压缩机/冷水机组入口制冷剂压力", 1, "MPa", "压力", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel12 = new Channel(12, 12, "压缩机/冷水机组入口制冷剂温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel13 = new Channel(13, 13, "环境温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel14 = new Channel(14, 14, "冷却水进口温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel15 = new Channel(15, 15, "冷却水出口温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel16 = new Channel(16, 16, "冷凝机组冷却水进水温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel17 = new Channel(17, 17, "冷凝机组冷却水出水温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel18 = new Channel(18, 18, "恒温水槽供温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel19 = new Channel(19, 19, "压缩机箱温度", 1, "℃", "温度", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel20 = new Channel(20, 20, "压缩机扭矩", 1, "Nm", "扭矩", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel21 = new Channel(21, 21, "压缩机转速", 1, "rpm", "转速", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel22 = new Channel(22, 22, "量热器输入功率", 1, "kW", "功率", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel23 = new Channel(23, 23, "恒温水槽回水流量", 1, "m3/h", "流量", "测量参数", 2, 100, 5, 20, 4);
            //Channel TestChannel24 = new Channel(24, 24, "恒温水槽供液压力", 1, "Mpa", "压力", "测量参数", 2, 100, 5, 20, 4);

            ////功率计WT电参数
            //Channel TestChannel25 = new Channel(25, 0, "电压A", 1, "V", "电压", "电参数", 1, 20, 0, 20, 4);
            //Channel TestChannel26 = new Channel(26, 1, "电压B", 1, "V", "电压", "电参数", 1, 20, 0, 20, 4);
            //Channel TestChannel27 = new Channel(27, 2, "电压C", 1, "V", "电压", "电参数", 1, 20, 0, 20, 4);
            //Channel TestChannel28 = new Channel(28, 3, "电流A", 1, "A", "电流", "电参数", 1, 20, 0, 20, 4);
            //Channel TestChannel29 = new Channel(29, 4, "电流B", 1, "A", "电流", "电参数", 1, 20, 0, 20, 4);
            //Channel TestChannel30 = new Channel(30, 5, "电流C", 1, "A", "电流", "电参数", 1, 20, 0, 20, 4);
            //Channel TestChannel31 = new Channel(31, 6, "频率", 1, "Hz", "频率", "电参数", 1, 20, 0, 20, 4);
            //Channel TestChannel32 = new Channel(32, 7, "功率因数", 1, "--", "因数", "电参数", 3, 1, 0, 1, 0);
            //Channel TestChannel33 = new Channel(33, 7, "功率", 1, "kW", "功率", "电参数", 3, 1, 0, 1, 0);

            ////功率计输出百分比
            //Channel TestChannel34 = new Channel(34, 0, "UT1输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 3, 1, 0, 1, 0);
            //Channel TestChannel35 = new Channel(35, 1, "UT2输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 3, 1, 0, 1, 0);
            //Channel TestChannel36 = new Channel(36, 2, "UT3输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 3, 1, 0, 1, 0);
            //Channel TestChannel37 = new Channel(37, 3, "UT4输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 3, 1, 0, 1, 0);
            //Channel TestChannel38 = new Channel(38, 4, "UT5输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 3, 1, 0, 1, 0);
            //Channel TestChannel39 = new Channel(39, 5, "UT6输出百分比", 1, "%", "输出百分比", "控制器输出百分比", 3, 1, 0, 1, 0);


            ////计算值
            //Channel TestChannel40 = new Channel(40, 0, "主测冷量", 1, "kW", "能力", "计算参数", 2, 1, 0, 1, 0);
            //Channel TestChannel41 = new Channel(41, 1, "辅测冷量", 1, "kW", "能力", "计算参数", 2, 1, 0, 1, 0);
            //Channel TestChannel42 = new Channel(42, 2, "百分比偏差", 1, "%", "百分比", "计算参数", 2, 1, 0, 1, 0);
            //Channel TestChannel43 = new Channel(43, 3, "COP", 1, "--", "COP", "计算参数", 2, 1, 0, 1, 0);

            //Channels = new Dictionary<int, Channel>();

            //Channels.Add(0, TestChannel0);
            //Channels.Add(1, TestChannel1);
            //Channels.Add(2, TestChannel2);
            //Channels.Add(3, TestChannel3);
            //Channels.Add(4, TestChannel4);
            //Channels.Add(5, TestChannel5);
            //Channels.Add(6, TestChannel6);
            //Channels.Add(7, TestChannel7);
            //Channels.Add(8, TestChannel8);
            //Channels.Add(9, TestChannel9);
            //Channels.Add(10, TestChannel10);
            //Channels.Add(11, TestChannel11);
            //Channels.Add(12, TestChannel12);
            //Channels.Add(13, TestChannel13);
            //Channels.Add(14, TestChannel14);
            //Channels.Add(15, TestChannel15);
            //Channels.Add(16, TestChannel16);
            //Channels.Add(17, TestChannel17);
            //Channels.Add(18, TestChannel18);
            //Channels.Add(19, TestChannel19);
            //Channels.Add(20, TestChannel20);
            //Channels.Add(21, TestChannel21);
            //Channels.Add(22, TestChannel22);
            //Channels.Add(23, TestChannel23);
            //Channels.Add(24, TestChannel24);
            //Channels.Add(25, TestChannel25);
            //Channels.Add(26, TestChannel26);
            //Channels.Add(27, TestChannel27);
            //Channels.Add(28, TestChannel28);
            //Channels.Add(29, TestChannel29);
            //Channels.Add(30, TestChannel30);
            //Channels.Add(31, TestChannel31);
            //Channels.Add(32, TestChannel32);
            //Channels.Add(33, TestChannel33);
            //Channels.Add(34, TestChannel34);
            //Channels.Add(35, TestChannel35);
            //Channels.Add(36, TestChannel36);
            //Channels.Add(37, TestChannel37);
            //Channels.Add(38, TestChannel38);
            //Channels.Add(39, TestChannel39);
            //Channels.Add(40, TestChannel40);
            //Channels.Add(41, TestChannel41);
            //Channels.Add(42, TestChannel42);
            //Channels.Add(43, TestChannel43);
            #endregion 通道添加

            realtimeCurves1.TestType = "FCU";
            //为什么后面的Channels一变，前面的就可以感受到？瞬时的，一定有绑定
            realtimeCurves1.Channels = GlobelVar.Channels;
            //public Channel(int no, int redNo, string name,double filter, string unit, string unitDesc, string type, int precision, double maximum, double minimum, double maxAnalog,double minAnalog)
            //Channel(no, no, "", fil, string unit, string unitDesc, string type, int precision, double maximum, double minimum, double maxAnalog,double minAnalog)
        }
        #endregion 曲线



        #endregion 辅助线程
        /// <summary>
        /// 漏热系数设定:20150918
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_Set_HeatDissipCoef_Click(object sender, RoutedEventArgs e)
        {

            CoeOfHeatDissipation coeofheatdissip = new CoeOfHeatDissipation();
            coeofheatdissip.ShowDialog();
        }

        private void menu_Set_FilterCoef_Click(object sender, RoutedEventArgs e)
        {
            FilterCoe filtercoe = new FilterCoe();
            filtercoe.Show();
        }

        private void ReportView_Click(object sender, RoutedEventArgs e)
        {
            Report.MainWindow reportwindow = new Report.MainWindow();
            reportwindow.Show();
        }

    }
}
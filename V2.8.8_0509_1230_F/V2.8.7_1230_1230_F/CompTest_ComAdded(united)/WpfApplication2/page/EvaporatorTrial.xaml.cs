using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.ComponentModel;
using SHHS.UILabs;
using BackPanel;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using RefMDBInquiry;

using System.Threading;
using Report;
namespace WpfApplication2
{
    /// <summary>
    /// ChillerTrialcommon.xaml 的交互逻辑
    /// </summary>
    public partial class EvaporatorTrial : Window
    {
        public EvaporatorTrial()
        {
            InitializeComponent();
        }

        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    Chiller chiller = new Chiller();
        //    chiller.Show();

        //    this.Close();

        //}

        private void EvaporatorTemperature_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(EvaporatorTemperature, textBox2);
        }

        private void TemperatureOfInputWater_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(TemperatureOfInputWater, textBox4);

        }

        private void FlowRateOfCoolingWater_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(FlowRateOfCoolingWater, textBox5);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ExperimentEquipStart.IsEnabled = true;
            this.ExperimentEquipStop.IsEnabled = false;

            //为了保证最开始的时候是不稳定的，谁他妈指导，有问题否20150905
            GlobelVar.IsStableChiller = false;
            //主钟，在操作界面打开时，开始运行！
            MainTimeBegin();

            //报警日期初始化
            AlarmDateInitiate();

            GlobelVar.InfoChangeChiller = 1;

            //把如果是测量水温的这个情景，把Set的名字改下：20150907
            switch (BackPanel.InformationGlo.senariocontrol)
            {

                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:
                    FlowRateOfCoolingWater.Name = "TemperatureOfOutWater";
                    textBlock4.Text = "出水温度";
                    textBox5.Text = "35.00";
                    textBlock9.Text = "℃";
                    break;

                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                    //textBox5.Text = "1.40";

                    textBox5.Text = BackPanel.InformationGlo.ChillerWaterFlowRate_ForControl.ToString("f2");
                    break;
            }

            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            //PLC控制变量初始化20150922
            BackPanel.Strategy.StrategyPLCIfVarIni();

            if (!isReportIni)
            {
                //这个早应该在项目工程中初始化！
                Report.DoubleListForReport_FromFrontPanel.IniData_ForChiller();
                isReportIni = true;
            }

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
        /// 
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //主钟，在操作界面关闭时，停止运行！
            MainTimeClose();

            //报警初始化
            //GlobelVar.GlobeVarIni_Alarm();


            //EndingSplash endsplash = new EndingSplash();
            //endsplash.Show();

            GlobelVar.InfoChangeChiller = 0;

            //控制变量初始化20150922
            BackPanel.Strategy.StrategyPLCIfVarIni();
        }



        #region 线程定义
        /// <summary>
        /// 稳定模块
        /// </summary>
        private BackgroundWorker _StableWorker2 = new BackgroundWorker();

        private BackgroundWorker _AgilentWorker = new BackgroundWorker();

        private BackgroundWorker _PLCWorker = new BackgroundWorker();

        private BackgroundWorker _UT35A = new BackgroundWorker();

        //曲线工作
        private BackgroundWorker _CurveWorker = new BackgroundWorker();

        /// <summary>
        /// 功率计模块20150911
        /// </summary>
        private BackgroundWorker _WT330 = new BackgroundWorker();

        /// <summary>
        /// 计算模块20150911
        /// </summary>
        private BackgroundWorker _Calculate = new BackgroundWorker();


        #endregion



        #region 钟函数

        public System.Windows.Threading.DispatcherTimer timer;
        /// <summary>
        /// 主钟开始运行函数
        /// </summary>
        public void MainTimeBegin()
        {
            #region 线程加事件20150911

            //System.Windows.Threading.DispatcherTimer timer;
            _StableWorker2.DoWork += new DoWorkEventHandler(_StableWorker_DoWork);

            _AgilentWorker.DoWork += new DoWorkEventHandler(_AgilentWorker_DoWork);

            _PLCWorker.DoWork += new DoWorkEventHandler(_PLCWorker_DoWork);

            _UT35A.DoWork += new DoWorkEventHandler(_UT35A_DoWork);

            //20150906
            _CurveWorker.DoWork += new DoWorkEventHandler(_CurveWorker_DoWork);

            _WT330.DoWork += new DoWorkEventHandler(_WT330_DoWork);

            _Calculate.DoWork += new DoWorkEventHandler(_Calculate_DoWork);
            #endregion 线程加事件20150911
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



        #region  辅助线程，具体工作20150906
        //曲线进程
        void _CurveWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        void _StableWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            if (iniStableTimerStart == 0)
            {
                ////StableTimerStart();
                //iniStableTimerStart = 1;
                //如果稳定
                if (GlobelVar.IsStableChiller)
                {

                    StableStart();

                }
                else
                {

                    StableEnd();
                }
            }
            //让这个，进程结束！因为不知道为什么，总有问题20150905
            e.Cancel = true;
            return;
        }

        void _UT35A_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        void _PLCWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            ////报警函数20150916
            //IsAlarmFound();
            //20150916
            BackPanel.PLCMod.PLCAlter(BackPanel.PLCMod.PLCDIList);

            //这个是PLC的控制策略20150922:ForChiller20151201
            BackPanel.Strategy.StrategyPLCAlarm_ForChiller(BackPanel.PLCMod.PLCDIList, BackPanel.PLCMod.PLCDOList);
        }

        #region 全局变量
        static object Controllist_All = new List<BackPanel.Control.UT35ADef>(6);
        static object WT310DataCOM3_All = new double[10];
        static object AgilentList_All = new List<BackPanel.Agilent.AgilentVar>(60);

        static object ChillerCalculateResult_All = new double[10];

        static object Array_Agilent101_122_All = new double[22];
        static object Array_Agilent201_222_All = new double[22];
        #endregion 全局变量
        void _AgilentWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            #region 局部转全局

            //throw new NotImplementedException();
            List<BackPanel.Control.UT35ADef> Controllist_Chiller = BackPanel.Control.ReadControlFromDN();
            lock (Controllist_All)
            {
                Controllist_All = Controllist_Chiller;
            }


            double[] WT310DataCOM3_Chiller = BackPanel.PowerMeter.ReadDataFromDN();
            lock (WT310DataCOM3_All)
            {
                WT310DataCOM3_All = WT310DataCOM3_Chiller;
            }


            List<BackPanel.Agilent.AgilentVar> AgilentList_Chiller = BackPanel.Agilent.AgilentRecTotal(Agilent.AgilentList);
            lock (AgilentList_All)
            {
                AgilentList_All = AgilentList_Chiller;
            }
            #endregion 局部转全局

            #region 局部Agilent数组
            double[] Array_Agilent101_122 = new double[22];
            double[] Array_Agilent201_222 = new double[22];
            #endregion

            #region 把后台的数，传送给前台一个数组：20150916 :修改20150919：
            //101
            Array_Agilent101_122[1 - 1] = AgilentList_Chiller[0].TargetCorr;

            Array_Agilent101_122[2 - 1] = AgilentList_Chiller[1].TargetCorr;

            Array_Agilent101_122[3 - 1] = AgilentList_Chiller[2].TargetCorr;

            Array_Agilent101_122[4 - 1] = AgilentList_Chiller[3].TargetCorr;

            Array_Agilent101_122[5 - 1] = AgilentList_Chiller[4].TargetCorr;

            Array_Agilent101_122[6 - 1] = AgilentList_Chiller[5].TargetCorr;

            Array_Agilent101_122[7 - 1] = AgilentList_Chiller[6].TargetCorr;

            Array_Agilent101_122[8 - 1] = AgilentList_Chiller[7].TargetCorr;

            Array_Agilent101_122[9 - 1] = AgilentList_Chiller[8].TargetCorr;

            //Array_Agilent101_122[10 - 1] = AgilentList[10 - 1].TargetCorr;

            //201-222
            Array_Agilent201_222[1 - 1] = AgilentList_Chiller[9].TargetCorr;
            Array_Agilent201_222[2 - 1] = AgilentList_Chiller[10].TargetCorr;

            Array_Agilent201_222[4 - 1] = AgilentList_Chiller[11].TargetCorr;

            Array_Agilent201_222[5 - 1] = AgilentList_Chiller[12].TargetCorr;

            Array_Agilent201_222[6 - 1] = AgilentList_Chiller[13].TargetCorr;

            Array_Agilent201_222[7 - 1] = AgilentList_Chiller[14].TargetCorr;

            Array_Agilent201_222[8 - 1] = AgilentList_Chiller[15].TargetCorr;

            Array_Agilent201_222[9 - 1] = AgilentList_Chiller[16].TargetCorr;

            Array_Agilent201_222[10 - 1] = AgilentList_Chiller[17].TargetCorr;

            Array_Agilent201_222[15 - 1] = AgilentList_Chiller[18].TargetCorr;

            Array_Agilent201_222[16 - 1] = AgilentList_Chiller[19].TargetCorr;

            //20151101加
            Array_Agilent201_222[18 - 1] = AgilentList_Chiller[20].TargetCorr;
            Array_Agilent201_222[19 - 1] = AgilentList_Chiller[21].TargetCorr;

            //Array_Agilent201_222[21 - 1] = AgilentList[21].TargetCorr;
            //20151008更改
            Array_Agilent201_222[20 - 1] = AgilentList_Chiller[22].TargetCorr;
            #endregion 把后台的数，传送给前台一个数组：20150916

            #region 赋给全局变量Array_Agilent101_122_All,Array_Agilent201_222_All
            lock (Array_Agilent101_122_All)
            {
                Array_Agilent101_122_All = Array_Agilent101_122;
            }

            lock (Array_Agilent101_122_All)
            {
                Array_Agilent201_222_All = Array_Agilent201_222;
            }
            #endregion 赋给全局变量Array_Agilent101_122_All,Array_Agilent201_222_All

            #region Chiller需要物性20150918

            //物性计算实例//MPa:GlobelVar.Array_Agilent101_122[5-1]   1.2左右
            //第二制冷剂对应饱和温度
            GlobelVar.ts_GloChiller = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R141b, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[9 - 1]);

            //6pt
            GlobelVar.hg2_GloChiller = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, false, Array_Agilent101_122[5 - 1], Array_Agilent201_222[10 - 1]);

            //这两个和上面Car的一样
            GlobelVar.hf2_GloChiller = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, true, Array_Agilent101_122[4 - 1], Array_Agilent201_222[8 - 1]);

            double ChillerSuctionPressureSat = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Pressure, DBRefPropInquiry.DBRefPropName.Temp, 7);
            GlobelVar.hg1_GloChiller = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy, false, 18, ChillerSuctionPressureSat);// BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.TPforH(18, BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(7));
            //1p-205
            GlobelVar.hf1_GloChiller = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Enthalpy,true, Array_Agilent101_122[1 - 1], Array_Agilent201_222[5 - 1]);// BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.PsatforH_Liq(Array_Agilent201_222[5 - 1]);


            GlobelVar.v1_GloChiller = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Volume, false, Array_Agilent101_122[6 - 1], Array_Agilent201_222[15 - 1]);// BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(Array_Agilent101_122[6 - 1], Array_Agilent201_222[15 - 1]);
            //也是按高问工况算的20151013:20151015确认是高温工况
            GlobelVar.vg1_GloChiller = DBRefPropInquiry.TPForSbcOrSphProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Volume, false, 18, ChillerSuctionPressureSat);// BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.SpecificVolumn(18, BackPanel.UtilityMod_Header.RefNistProp.RefInUsing.Psat_Vap(7));
            //stopdot = 10;

            GlobelVar.ChillerEvapTem_ForControl = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R134a, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[15 - 1]);

            GlobelVar.ChillerHeatMeasureTemp_ForMeasure = DBRefPropInquiry.ForSatProp(DBRefPropInquiry.DBRefName.R141b, DBRefPropInquiry.DBRefPropName.Temp, DBRefPropInquiry.DBRefPropName.Pressure, Array_Agilent201_222[9 - 1]);


            double CoolingWaterTemp = (Array_Agilent101_122[8 - 1] + Array_Agilent101_122[9 - 1]) / 2;
            GlobelVar.C_GlO = BackPanel.UtilityMod_Header.Water.Cp(CoolingWaterTemp);
            GlobelVar.WaterDensity_Glo = BackPanel.UtilityMod_Header.Water.Density(CoolingWaterTemp);

            #endregion Chiller需要20150918

            #region 计算20151128
            BackPanel.InformationGlo.FilterNumber_ForChillerCalculate++;
            //0:Chiller制冷剂流量,1:Chiller制冷量,2:Chiller_COP，3轴功率
            double[] ChillerCalculateResult_Chiller = GlobleFun.Chiller_Total(Array_Agilent101_122, Array_Agilent201_222, WT310DataCOM3_Chiller);
            #endregion 计算20151128
            #region 赋给全局变量
            lock (ChillerCalculateResult_All)
            {
                ChillerCalculateResult_All = ChillerCalculateResult_Chiller;
            }
            #endregion
            #region 曲线赋值
            DateTime dt = DateTime.Now;
            foreach (var v in GlobelVar.Channels)
            {
                v.Value.Time = dt;
            }
            Random rdm = new Random();
            #region 给Channels赋值：20150917（试验）：201509120改：20150924改



            //改为20150920 :按照Agilent点位图改:20150924按数据库修改
            GlobelVar.Channels[0].Value = AgilentList_Chiller[0].TargetCorr;
            GlobelVar.Channels[1].Value = AgilentList_Chiller[1].TargetCorr;
            GlobelVar.Channels[2].Value = AgilentList_Chiller[2].TargetCorr;// AgilentList_Chiller[2].TargetCorr;
            GlobelVar.Channels[3].Value = AgilentList_Chiller[3].TargetCorr;// AgilentList_Chiller[3].TargetCorr;
            GlobelVar.Channels[4].Value = AgilentList_Chiller[4].TargetCorr;
            GlobelVar.Channels[5].Value = AgilentList_Chiller[5].TargetCorr;
            GlobelVar.Channels[6].Value = AgilentList_Chiller[6].TargetCorr;
            GlobelVar.Channels[7].Value = AgilentList_Chiller[7].TargetCorr;

            GlobelVar.Channels[8].Value = AgilentList_Chiller[8].TargetCorr;
            //GlobelVar.Channels[9].Value = BackPanel.Agilent.AgilentList[9].TargetCorr;
            GlobelVar.Channels[10].Value = AgilentList_Chiller[9].TargetCorr;// GlobelVar.Array_Agilent201_222[7 - 1];
            //GlobelVar.Channels[11].Value = BackPanel.Agilent.AgilentList[11].TargetCorr;
            GlobelVar.Channels[12].Value = AgilentList_Chiller[10].TargetCorr;// GlobelVar.Array_Agilent201_222[7 - 1];
            GlobelVar.Channels[13].Value = AgilentList_Chiller[11].TargetCorr;//  GlobelVar.Array_Agilent201_222[7 - 1];
            GlobelVar.Channels[14].Value = AgilentList_Chiller[12].TargetCorr;// ;
            GlobelVar.Channels[15].Value = AgilentList_Chiller[13].TargetCorr;// ;
            GlobelVar.Channels[16].Value = AgilentList_Chiller[14].TargetCorr;// 
            GlobelVar.Channels[17].Value = AgilentList_Chiller[15].TargetCorr;
            GlobelVar.Channels[18].Value = AgilentList_Chiller[16].TargetCorr;
            GlobelVar.Channels[19].Value = AgilentList_Chiller[17].TargetCorr;
            GlobelVar.Channels[20].Value = AgilentList_Chiller[18].TargetCorr;
            GlobelVar.Channels[21].Value = AgilentList_Chiller[19].TargetCorr;
            GlobelVar.Channels[22].Value = AgilentList_Chiller[21].TargetCorr;
            GlobelVar.Channels[23].Value = AgilentList_Chiller[22].TargetCorr;

            GlobelVar.Channels[24].Value = AgilentList_Chiller[20].TargetCorr;

            #region 20150920:功率计WT电参数25-33对应25-33：20150920
            GlobelVar.Channels[25].Value = WT310DataCOM3_Chiller[0];
            GlobelVar.Channels[26].Value = WT310DataCOM3_Chiller[1];
            GlobelVar.Channels[27].Value = WT310DataCOM3_Chiller[2];
            GlobelVar.Channels[28].Value = WT310DataCOM3_Chiller[3];
            GlobelVar.Channels[29].Value = WT310DataCOM3_Chiller[4];
            GlobelVar.Channels[30].Value = WT310DataCOM3_Chiller[5];
            GlobelVar.Channels[31].Value = WT310DataCOM3_Chiller[8];
            GlobelVar.Channels[32].Value = WT310DataCOM3_Chiller[7];
            //输入功率,加过滤系数
            GlobelVar.Channels[33].Value = WT310DataCOM3_Chiller[6];

            #endregion /功率计WT电参数

            #region 控制器输出百分比：20150920：34-39对应34-39
            //20150929改添加
            switch (BackPanel.InformationGlo.senariocontrol)
            {
                case BackPanel.InformationGlo.SenarioControl.ControlCar:
                    //UT1
                    GlobelVar.Channels[34].Value = BackPanel.Control.Controllist[0].OUT;
                    //UT2
                    GlobelVar.Channels[35].Value = BackPanel.Control.Controllist[1].OUT;
                    //UT3
                    GlobelVar.Channels[36].Value = BackPanel.Control.Controllist[2].OUT;
                    //UT4
                    GlobelVar.Channels[37].Value = 0;
                    //UT5
                    GlobelVar.Channels[38].Value = BackPanel.Control.Controllist[3].OUT;
                    //UT6
                    GlobelVar.Channels[39].Value = BackPanel.Control.Controllist[4].OUT;
                    break;

                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                    //UT1
                    GlobelVar.Channels[34].Value = Controllist_Chiller[0].OUT;
                    //UT2
                    GlobelVar.Channels[35].Value = Controllist_Chiller[1].OUT;
                    //UT3
                    GlobelVar.Channels[36].Value = 0;
                    //UT4
                    GlobelVar.Channels[37].Value = Controllist_Chiller[2].OUT;
                    //UT5
                    GlobelVar.Channels[38].Value = Controllist_Chiller[3].OUT;
                    //UT6
                    GlobelVar.Channels[39].Value = Controllist_Chiller[4].OUT;
                    break;

                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:
                    //UT1
                    GlobelVar.Channels[34].Value = Controllist_Chiller[0].OUT;
                    //UT2
                    GlobelVar.Channels[35].Value = Controllist_Chiller[1].OUT;
                    //UT3
                    GlobelVar.Channels[36].Value = 0;
                    //UT4
                    GlobelVar.Channels[37].Value = Controllist_Chiller[2].OUT;
                    //UT5
                    GlobelVar.Channels[38].Value = Controllist_Chiller[3].OUT;
                    //UT6
                    GlobelVar.Channels[39].Value = Controllist_Chiller[4].OUT;
                    break;

            }
            //控制器UT5的当前值20150924
            GlobelVar.Channels[40].Value = Controllist_Chiller[3].PV;
            GlobelVar.Channels[41].Value = Controllist_Chiller[3].PV;

            #endregion 功率计输出百分比：20150920

            #region 计算值20150950：20151012改
            // 冷水机组制冷剂流量GlobelVar.CalculateCar_Glo.A_CoolingCapacity;
            GlobelVar.Channels[48].Value = ChillerCalculateResult_Chiller[0];// GlobelVar.CalculateChiller_Glo.RefrigFlowMass;
            //  冷水机组制冷量GlobelVar.CalculateCar_Glo.G_CoolingCapacity;
            GlobelVar.Channels[49].Value = ChillerCalculateResult_Chiller[1];// GlobelVar.CalculateChiller_Glo.CoolingCapacity;
            // 冷水机组输入功率GlobelVar.CalculateCar_Glo.TestErr;
            GlobelVar.Channels[50].Value = WT310DataCOM3_Chiller[8]; // GlobelVar.CalculateChiller_Glo.ActualCompressPower; 
            //冷水机组COP
            GlobelVar.Channels[51].Value = ChillerCalculateResult_Chiller[2];// GlobelVar.CalculateChiller_Glo.COP;

            #endregion 计算值20150950
            //hf2膨胀阀进口焓
            GlobelVar.Channels[55].Value = GlobelVar.hf2_GloChiller;
            //hg2量热器出口焓
            GlobelVar.Channels[56].Value = GlobelVar.hg2_GloChiller;
            //v1，进入机组制冷剂蒸汽实际比体积
            GlobelVar.Channels[57].Value = GlobelVar.v1_GloChiller;
            //vg1,进入机组
            GlobelVar.Channels[58].Value = GlobelVar.vg1_GloChiller;
            //Channels[1].Value =rdm.NextDouble();
            //Channels[2].Value =rdm.NextDouble();
            #endregion 给Channels赋值：20150917（试验）
            realtimeCurves1.Refresh();
            //throw new NotImplementedException();
            #endregion 曲线赋值
            #region 插入数据库

            double[] OthersForChillerDataBase = new double[8];
            //Others[0] = GlobelVar.vga_Glo;
            OthersForChillerDataBase[0] = GlobelVar.v1_GloChiller;

            OthersForChillerDataBase[1] = GlobelVar.hf1_GloChiller;
            OthersForChillerDataBase[2] = GlobelVar.hf2_GloChiller;
            OthersForChillerDataBase[3] = GlobelVar.ts_GloChiller;
            OthersForChillerDataBase[4] = GlobelVar.hg2_GloChiller;
            OthersForChillerDataBase[5] = GlobelVar.C_GlO;
            OthersForChillerDataBase[6] = GlobelVar.WaterDensity_Glo;
            OthersForChillerDataBase[7] = BackPanel.Calculate.CalculateChiller.HeatDissipCap;

            double[] CarCalculate = new double[5];
            //把个别的AGILENT采集数据采集到，相应试验的数据库中！20150917：Chiller20150920加
            BackPanel.DBOperate.InsertRecordDataTODBTotal(BackPanel.InformationGlo.DBPath, InformationGlo.senario, AgilentList_Chiller, WT310DataCOM3_Chiller, Controllist_Chiller, CarCalculate, ChillerCalculateResult_Chiller, OthersForChillerDataBase);
            #endregion 插入数据库

            #region 报表需要的数据20151220
            //double[] Others = new double[7];
            double[] Others = new double[8];
            //Others[0] = GlobelVar.vga_Glo;
            Others[0] = GlobelVar.v1_GloChiller;
            
            Others[1] = GlobelVar.hf1_GloChiller;
            Others[2] = GlobelVar.hf2_GloChiller;
            Others[3] = GlobelVar.ts_GloChiller;
            Others[4] = GlobelVar.hg2_GloChiller;
            Others[5] = GlobelVar.C_GlO;
            Others[6] = GlobelVar.WaterDensity_Glo;
            Others[7] = BackPanel.Calculate.CalculateChiller.HeatDissipCap;

            GlobelVar.DoubleDataForChillerReport = GlobleFun.DoubleDataFor_ChillerReport(Array_Agilent101_122, Array_Agilent201_222, WT310DataCOM3_Chiller, ChillerCalculateResult_Chiller, Others);

            //double temp = 10;
            #endregion 报表需要的数据

        }


        void _Calculate_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            //计算函数  //20150916拿走到辅助线程里

        }

        void _WT330_DoWork(object sender, DoWorkEventArgs e)
        {

            //throw new NotImplementedException();
        }
        #endregion 辅助线程，具体工作20150906

        /// <summary>
        /// 主钟结束函数
        /// </summary>
        public void MainTimeClose()
        {
            timer.Stop();
            Thread.Sleep(2000);
        }

        int MainTimeNum = 0;

        /// <summary>
        /// 主钟运行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MaintTime_Tick(object sender, EventArgs e)
        {
            #region 全局变局部
            List<BackPanel.Control.UT35ADef> Controllist = new List<BackPanel.Control.UT35ADef>();
            //进水温度
            double ChillerInWaterTemp = 0;
            if (MainTimeNum != 0)
            {
                lock (Controllist_All)
                {
                    Controllist = (List<BackPanel.Control.UT35ADef>)Controllist_All;
                    ChillerInWaterTemp = Controllist[3].PV;
                }
            }
            //TODO:...1
            if (MainTimeNum == 0)
            {
                BackPanel.Control.BuildControlerList(Controllist, BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "Table_Controller");
                ChillerInWaterTemp = 2;
            }

            double[] WT310DataCOM3 = new double[10];
            lock (WT310DataCOM3_All)
            {
                WT310DataCOM3 = (double[])WT310DataCOM3_All;
            }
            //0:Chiller制冷剂流量,1:Chiller制冷量,2:Chiller_COP，3轴功率
            double[] ChillerCalculateResult = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            lock (ChillerCalculateResult_All)
            {

                ChillerCalculateResult = (double[])ChillerCalculateResult_All;
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


            MainTimeNum++;



            #region 线程调用20150911:20150916移动

            if (!_AgilentWorker.IsBusy)
            {
                _AgilentWorker.RunWorkerAsync();
            }
            if (!_PLCWorker.IsBusy)
            {
                _PLCWorker.RunWorkerAsync();
            }


            #region 报警策略显示20150922
            if (BackPanel.Strategy.IfDonePLCDIPlay)
            {
            }
            else
            {
                PlayAlarmFound();
                //跳转到相应的界面20150922
                if (BackPanel.Strategy.IsTherePLC_Error)
                {
                    TIAlarm.Focus();

                    ExperimentEquip.IsEnabled = false;
                    //被测机组惨淡关闭
                    Chiller.IsEnabled = false;

                    quit.IsEnabled = false;

                    //保证只运行一次！
                    BackPanel.Strategy.IfDonePLCDIPlay = true;

                    BackPanel.InformationGlo.BackFormMainBecauseOfError = true;
                }
            }
            #endregion 报警策略


            #endregion 线程调用20150911:20150916

            //场景选择
            switch (BackPanel.InformationGlo.senario)
            {

                case BackPanel.InformationGlo.Senario.ChillerNormialCondition:
                    //// CarCooling.textbox1
                    SetValue(Array_Agilent101_122, Array_Agilent201_222, ChillerInWaterTemp, ChillerCalculateResult);
                    break;

                //case BackPanel.InformationGlo.Senario.ChillerNormialConditionWaterFlow:
                //    SetValue(Array_Agilent101_122, Array_Agilent201_222, ChillerInWaterTemp, ChillerCalculateResult);
                //    break;

                case BackPanel.InformationGlo.Senario.ChillerPartialCondition:
                    SetValue(Array_Agilent101_122, Array_Agilent201_222, ChillerInWaterTemp, ChillerCalculateResult);
                    break;

                case BackPanel.InformationGlo.Senario.ChillerMaxCondition:
                    SetValue(Array_Agilent101_122, Array_Agilent201_222, ChillerInWaterTemp, ChillerCalculateResult);
                    break;

                case BackPanel.InformationGlo.Senario.ChillerChangCondition:
                    SetValue(Array_Agilent101_122, Array_Agilent201_222, ChillerInWaterTemp, ChillerCalculateResult);
                    break;

            }

            #region System显示
            tb205.Text = Array_Agilent201_222[5 - 1].ToString("f3");
            tb101.Text = Array_Agilent101_122[1 - 1].ToString("f2");

            tb208.Text = Array_Agilent201_222[8 - 1].ToString("f3");
            tb104.Text = Array_Agilent101_122[4 - 1].ToString("f2");

            tb209.Text = Array_Agilent201_222[9 - 1].ToString("f3");

            tb210.Text = Array_Agilent201_222[10 - 1].ToString("f3");
            tb105.Text = Array_Agilent101_122[5 - 1].ToString("f2");

            tb215.Text = Array_Agilent201_222[15 - 1].ToString("f3");
            tb106.Text = Array_Agilent101_122[6 - 1].ToString("f2");

            tb201.Text = Array_Agilent201_222[1 - 1].ToString("f2");

            tb219.Text = Array_Agilent201_222[19 - 1].ToString("f2");

            //tbUT5PV1.Text = ChillerInWaterTemp.ToString("f2");
            tbUT5PV2.Text = ChillerInWaterTemp.ToString("f2");
            #endregion System显示

            MainTimeTest++;


            if(GlobelVar.IsInfoFromMain)
            {
                switch (BackPanel.InformationGlo.senariocontrol)
                {
                    case InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                        textBox5.Text = BackPanel.InformationGlo.ChillerWaterFlowRate_ForControl.ToString("f2");
                        break;
                    case InformationGlo.SenarioControl.ControlChillerWaterTemp:
                        break;
                }
            }
            GlobelVar.IsInfoFromMain = false;

            StableJudgeFun(Array_Agilent101_122, Array_Agilent201_222,ChillerInWaterTemp);
            #region 稳定模块运行
            AddTextMidTemp = textBox17.Text;
            if (!_StableWorker2.IsBusy)
            {
                _StableWorker2.RunWorkerAsync();
            }

            //时间初始化
            if (StableTimePlayIni == true)
            {
                StableTimePlay.Text = "00:00:00";
            }

            string IsStable_ForDisplay="不稳定";

            if (GlobelVar.IsStableChiller)
            {
                IsStable_ForDisplay = "稳定";

                StableTimer_ForDisplay++;

                sec = StableTimer_ForDisplay % 60;
                min = (StableTimer_ForDisplay / 60) % 60;
                hou = StableTimer_ForDisplay / 3600;
                //StableRecordList.Add(textBox17.Text);



            }
            else
            {
                IsStable_ForDisplay = "不稳定";
                StableTimer_ForDisplay = 0;

                sec = StableTimer_ForDisplay % 60;
                min = (StableTimer_ForDisplay / 60) % 60;
                hou = StableTimer_ForDisplay / 3600;
 
            }

            tbkIsStable.Text = IsStable_ForDisplay;

            StableTimePlay.Text = string.Format("{0:00}:{1:00}:{2:00}", hou, min, sec);
            #endregion 稳定模块运行

        }


        #region 最新的稳定模块
        /// <summary>
        /// 由于辅助线程不能，获得UI的控件内容，所以借助这个可以：StableRecordList.Add(AddTextMidTemp);
        /// </summary>
        private string AddTextMidTemp;
        /// <summary>
        /// 是否运行稳定模块，0运行，1不运行
        /// </summary>
        private int iniStableTimerStart=1;

        /// <summary>
        /// 记录稳定时间
        /// </summary>
        private int StableTimersec = 0;

        /// <summary>
        /// 稳定时间，前台显示！20151224
        /// </summary>
        private int StableTimer_ForDisplay = 0;

        //时分秒
        int sec, min, hou;

        ///// <summary>
        ///// 判断是否稳定
        ///// </summary>
        //private bool IsStableChiller = false;
        /// <summary>
        /// 每次主钟扫一次，如果是true则稳定事件变为：00：00：00
        /// </summary>
        private bool StableTimePlayIni = false;


        private void StableStart()
        {
            StableTimePlayIni = false;


            StableTimersec = StableTimersec + 1;
            //在指定的时间间隔内记录！

            if ((StableTimersec % GlobelVar.RecordSpanSec) == 2)
            {
                //StableRecordList.Add(AddTextMidTemp);
                //if (StableRecordList.Count == GlobelVar.RecordNum)
                //{

                //    StableTimersec = 0;
                //    //记录完成后，就不会再走稳定这一模块
                //    iniStableTimerStart = 1;
                //    MessageBox.Show("已经记录了" + GlobelVar.RecordNum.ToString() + "条记录");
                //    return;

                //}

                //int temp=0;
                if (CurrentIndex < RecordNumber)
                {
                    //StableRecordList.Add(textBox17.Text);
                    //DoubleListForReport_FromFrontPanel.AddData2(GlobelVar.DoubleDataForCarReport, CurrentIndex);

                    DoubleListForReport_FromFrontPanel.AddData2(GlobelVar.DoubleDataForChillerReport, CurrentIndex);
                }
                else
                {
                    //保证只出现一次！
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

            //sec = StableTimersec % 60;
            //min = (StableTimersec / 60) % 60;
            //hou = StableTimersec / 3600;

            //"{0:00}:{1:00}:{2:00}"
            //StableTimePlay.Text = string.Format("{0:00}:{1:00}:{2:00}", hou, min, sec);

            //timer_StableTemp.Stop();
        }

        private void StableEnd()
        {

            Report.DoubleListForReport_FromFrontPanel.IniData_ForChiller();

            StableRecordList.Clear();
            StableTimePlayIni = true;


            StableTimersec = 0;
        }

        #endregion

        //主钟测试变量
        public double MainTimeTest = 0;

        /// <summary>
        /// 抽出来一个赋值函数：把后台的给前台，一个采集参数，一个计算参数,因为四个场景都是一样的所以这个比较方便：20150910
        /// </summary>
        /// <param name="temp"></param>
        public void SetValue(double[] Array_Agilent101_122, double[] Array_Agilent201_222, double ChillerInWaterTemp, double[] CalculateChiller)
        {
            // CarCooling.textbox1
            ////控制参数
            //蒸发温度20150919
            textBox6.Text = Array_Agilent101_122[6 - 1].ToString("f2");

            textBox7.Text = GlobelVar.ChillerEvapTem_ForControl.ToString("f2");// UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]).ToString("f2");// 7.ToString("f2");
            textBox8.Text = Array_Agilent201_222[15 - 1].ToString("f3");// 3.ToString("f2");
            //textBox9.Text = BackPanel.Control.Controllist[3].PV.ToString("f2");// GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
            textBox9.Text = ChillerInWaterTemp.ToString("f2");// GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");


            switch (BackPanel.InformationGlo.senariocontrol)
            {
                //这个是使用出水温度的时候
                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:

                    //控制器 是需要：出水温度还是冷却水流量20151008
                    textBox10.Text = Array_Agilent201_222[1 - 1].ToString("f2");
                    break;
                //这个是其余的情况
                default:
                    //控制器 是需要：出水温度还是冷却水流量20151008
                    textBox10.Text = Array_Agilent201_222[19 - 1].ToString("f2");
                    break;
            }
            ////设定的蒸发压力有蒸发温度确定20150919
            textBox3.Text = GlobelVar.ChillerPressEvap.ToString("f3");// 3.ToString("f2");

            //测量参数：20150919G
            //供液压力20150919
            textBox12.Text = Array_Agilent201_222[5 - 1].ToString("f3");
            //20151012修改
            textBox13.Text = ChillerInWaterTemp.ToString("f2");// GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
            textBox14.Text = Array_Agilent201_222[19 - 1].ToString("f2");
            textBox15.Text = Array_Agilent201_222[9 - 1].ToString("f3");

            //供液温度20150919
            //textBox18.Text = GlobelVar.Array_Agilent201_222[1 - 1].ToString("f2");
            textBox18.Text = Array_Agilent101_122[1 - 1].ToString("f2");

            //出水温度20151008
            //textBox19.Text = GlobelVar.Array_Agilent101_122[9 - 1].ToString("f2");
            textBox19.Text = Array_Agilent201_222[1 - 1].ToString("f2");

            #region  计算部分20151012
            //计算参数
            textBox16.Text = CalculateChiller[1].ToString("f3");
            textBox17.Text = CalculateChiller[2].ToString("f2");
            //制冷剂流量，公式修改
            textBox20.Text = CalculateChiller[0].ToString("f3");
            //这个量热器温度可以直接得出:不用了，还是这样吧：20151012改
            //textBox21.Text = GlobelVar.ts_GloChiller.ToString("f2");
            textBox21.Text = GlobelVar.ChillerHeatMeasureTemp_ForMeasure.ToString("f2");  // BackPanel.UtilityMod_Header.RefNistProp.r141b.Tsat_Liq(Array_Agilent201_222[9 - 1]).ToString("f2");
            textBox22.Text = CalculateChiller[3].ToString("f3");
            #endregion 计算部分20151012

        }
        #endregion



        #region
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //GlobelVar.ImmediateStopIsError = true;
            //IsAlarmFound();
        }

        /// <summary>
        /// 出现错误
        /// 验证后放到主钟里:改名字，要显示20150918
        /// </summary>
        private void PlayAlarmFound()
        {
            //如果"急停"找到错误 //原来ImmediateStopIsError = false;20150916
            if (BackPanel.PLCMod.PLCDIList[0].IsAlerting == true)
            {
                ellipse1.Visibility = Visibility.Hidden;
                ellipse2.Visibility = Visibility.Visible;
                textBox32.Text = BackPanel.PLCMod.PLCDIList[0].AlertTimeStamp;
                //MessageBox.Show("急停报警！正在关闭相应设备！");
            }
            else
            {
                ellipse1.Visibility = Visibility.Visible;
                ellipse2.Visibility = Visibility.Hidden;
            }
            //"水箱液位"  //原来WaterTankerHeightIsError = false; ;20150916
            if (BackPanel.PLCMod.PLCDIList[1].IsAlerting == true)
            {
                ellipse3.Visibility = Visibility.Hidden;
                ellipse4.Visibility = Visibility.Visible;
                textBox33.Text = BackPanel.PLCMod.PLCDIList[1].AlertTimeStamp;
                //MessageBox.Show("水箱低液位报警！正在关闭相应设备！");
            }
            else
            {
                ellipse3.Visibility = Visibility.Visible;
                ellipse4.Visibility = Visibility.Hidden;

            }

            //"水流开关"  //原来WaterFlowOnOffIsError = false; ;20150916
            if (BackPanel.PLCMod.PLCDIList[3].IsAlerting == true)
            {
                ellipse5.Visibility = Visibility.Hidden;
                ellipse6.Visibility = Visibility.Visible;
                textBox34.Text = BackPanel.PLCMod.PLCDIList[3].AlertTimeStamp;
                //MessageBox.Show("水流开关报警！正在关闭相应设备！");
            }
            else
            {
                ellipse5.Visibility = Visibility.Visible;
                ellipse6.Visibility = Visibility.Hidden;
            }

            //"压缩冷凝机组高低压" //原来ChillerHighAndLowPreIsError = false; ;20150916
            if (BackPanel.PLCMod.PLCDIList[4].IsAlerting == true || BackPanel.PLCMod.PLCDIList[5].IsAlerting == true)
            {
                ellipse7.Visibility = Visibility.Hidden;
                ellipse8.Visibility = Visibility.Visible;
                textBox35.Text = BackPanel.PLCMod.PLCDIList[4].AlertTimeStamp;
                //MessageBox.Show("压缩冷凝机组高低压报警！正在关闭相应设备！");
            }
            else
            {
                ellipse7.Visibility = Visibility.Visible;
                ellipse8.Visibility = Visibility.Hidden;
            }

            //"量热器高压" //原来HeatMeasureHighPreIsError = false;;20150916
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
        /// 重置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
        /// <summary>
        /// 报警重置按钮是否按下：默认是没有
        /// </summary>
        public bool IsAlarmResetDone = false;

        /// <summary>
        /// 重置后,将只能退出试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlarmReset_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult mbr = MessageBox.Show("报警是否重置？", "报警重置", MessageBoxButton.OKCancel);
            if (mbr == MessageBoxResult.OK)
            {
                //GlobelVar.GlobeVarIni_Alarm();
                AlarmDateInitiate();

                BackPanel.Strategy.StrategyPLCReset();

                //添加20150929
                //实验设备停止
                //BackPanel.Strategy.StrategyPLCExperimentEquipStop();
                //被测机组停止
                //BackPanel.Strategy.StrategyPLCChillerStop();
                //菜单操作
                Chiller.IsEnabled = false;
                ExperimentEquip.IsEnabled = false;
                quit.IsEnabled = true;

                ExperimentEquipStart.IsEnabled = true;
                ExperimentEquipStop.IsEnabled = false;

                IsAlarmResetDone = true;
                //报警重置标识符、、用在info界面上的
                BackPanel.InformationGlo.IsAlertingReset = true;
            }
            else
            {

            }
        }



        /// <summary>
        /// 实验设备——开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExperimentEquipStart_Click(object sender, RoutedEventArgs e)
        {
            ExperimentEquipStart.IsEnabled = false;
            ExperimentEquipStop.IsEnabled = true;
            Chiller.IsEnabled = true;
            ChillerStart.IsEnabled = true;
            ChillerStop.IsEnabled = false;
            quit.IsEnabled = false;

            #region PLC控制策略：20150923
            //BackPanel.Strategy.StrategyPLCExperimentEquipStart();
            BackPanel.PLCMod.WaterSupply(true);
            BackPanel.Control.ControlUnLockUT4UT5_ForChiller();
            #endregion PLC控制策略：20150923
        }



        private void ExperimentEquipStop_Click(object sender, RoutedEventArgs e)
        {
            quit.IsEnabled = true;
            ExperimentEquipStart.IsEnabled = true;
            ExperimentEquipStop.IsEnabled = false;
            Chiller.IsEnabled = false;

            #region PLC控制策略：20150923
            BackPanel.PLCMod.WaterSupply(false);
            BackPanel.Control.ControlLockUT4UT5_ForChiller();
            #endregion PLC控制策略：20150923
        }

        /// <summary>
        /// 测试机组 开始试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChillerStart_Click(object sender, RoutedEventArgs e)
        {
            ExperimentEquip.IsEnabled = false;
            ChillerStart.IsEnabled = false;
            ChillerStop.IsEnabled = true;
            quit.IsEnabled = false;
            #region PLC控制策略：20150923
            BackPanel.PLCMod.ChillerOn(true);
            BackPanel.Control.ControlUnLockUT1UT2_ForChiller();
            #endregion PLC控制策略：20150923

            GlobelVar.IsChillerChillerOn = true;
        }
        /// <summary>
        /// 测试机组 停止试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChillerStop_Click(object sender, RoutedEventArgs e)
        {
            ChillerStop.IsEnabled = false;
            ChillerStart.IsEnabled = true;
            ExperimentEquip.IsEnabled = true;
            ExperimentEquipStart.IsEnabled = false;
            ExperimentEquipStop.IsEnabled = true;
            quit.IsEnabled = false;

            #region PLC控制策略：20150923
            //BackPanel.Strategy.StrategyPLCChillerStop();
            BackPanel.PLCMod.ChillerOn(false);
            BackPanel.Control.ControlLockUT1UT2_ForChiller();
            #endregion PLC控制策略：20150923

            GlobelVar.IsChillerChillerOn = false ;
        }


        private void quit_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("是否需要保存数据？", "退出试验", MessageBoxButton.OKCancel);
            if (mbr == MessageBoxResult.OK)
            {
                //出现报警直接退出20151201
                if (BackPanel.Strategy.IsTherePLC_Error)
                {
                    MainTimeClose();

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
                    this.Close();
                }
                else
                {
                    MainTimeClose();


                    #region  保存报表数据20151224

                    IsQualifiedWindow isqualifiedwindow2 = new IsQualifiedWindow();
                    isqualifiedwindow2.ShowDialog();

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

                    //timer.Sto();
                    BackPanel.Strategy.IsReturnFromMain = true;

                    Chiller Chill = new WpfApplication2.Chiller();
                    Chill.Show();
                    //Car car = new Car();
                    //car.Show();
                    this.Close();
                }
                
            }
            else
            {
                //出现报警直接退出20151201
                if (BackPanel.Strategy.IsTherePLC_Error)
                {
                    MainTimeClose();

                    EndingSplash endsplash = new EndingSplash();
                    endsplash.Show();
                    this.Close();
                }
                else
                {
                    MainTimeClose();
                    //timer.Sto();
                    BackPanel.Strategy.IsReturnFromMain = true;

                    Chiller Chill = new WpfApplication2.Chiller();
                    Chill.Show();
                    //Car car = new Car();
                    //car.Show();
                    this.Close();
                }


            }

        }
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TS_Chiller_Click(object sender, RoutedEventArgs e)
        {
            _DEBUG.MainWindow TSChiller = new _DEBUG.MainWindow();
            //Debug.MainWindow TSChiller = new Debug.MainWindow();
            TSChiller.ShowDialog();
        }

        //需要记录次数
        private int RecordNumber = 4;

        //当前记录
        int CurrentIndex = 0;

        /// <summary>
        /// 记录list
        /// </summary>
        private List<string> StableRecordList = new List<string>();



        private void ManuRecordChiller_Checked(object sender, RoutedEventArgs e)
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
            StableRecordList.Clear();

            //显示时间永远是000000
            StableTimePlay.Text = "00:00:00";
            StableTimePlay.FontSize = 25;

            //永远是不稳定的！
            //IsStableCar = false;
            GlobelVar.IsStableChiller = false;
            iniStableTimerStart = 1;

            //timer_StableTemp.Stop();
        }

        private void AutoRecordChiller_Checked(object sender, RoutedEventArgs e)
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
            StableRecordList.Clear();

            iniStableTimerStart = 0;
            //StableTimer_TempStart();
        }

        private void btRecording_Click(object sender, RoutedEventArgs e)
        {
            //int temp=0;
            if (CurrentIndex < RecordNumber)
            {
                //StableRecordList.Add(textBox17.Text);
                //DoubleListForReport_FromFrontPanel.AddData2(GlobelVar.DoubleDataForCarReport, CurrentIndex);

                DoubleListForReport_FromFrontPanel.AddData2(GlobelVar.DoubleDataForChillerReport, CurrentIndex);
            }
            else
            {
                MessageBox.Show("已经记录了" + CurrentIndex + "组");
                return;

            }
            CurrentIndex++;

            if (CurrentIndex > 4)
            {
                CurrentIndex = 4;
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (btDelete.Content.ToString() == "停止记录")
            {
                //timer_Stable.Stop();
                //timer_StableTemp.Stop();

                ManuRecordChiller.IsEnabled = true;

                //AutoRecordChiller.IsEnabled = false;
                ManuRecordChiller.IsChecked = true;
                AutoRecordChiller.IsChecked = false;


                iniStableTimerStart = 1;

                MessageBox.Show("自动记录停止，请重新选择!");
            }
            else
            {
                if (CurrentIndex >= 1)
                {
                    DoubleListForReport_FromFrontPanel.DeleteData3(CurrentIndex);
                    CurrentIndex--;
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
        ////
        //System.Windows.Threading.DispatcherTimer timer_StableTemp = new System.Windows.Threading.DispatcherTimer();
        //private void StableTimer_TempStart()
        //{
        //    //timer_StableTemp = new System.Windows.Threading.DispatcherTimer();
        //    timer_StableTemp.Interval = new TimeSpan(0, 0, 1);
        //    timer_StableTemp.Tick += new EventHandler(timeStableTemp_Tick);
        //    timer_StableTemp.Start();
        //}

        //private void timeStableTemp_Tick(object sender, EventArgs e)
        //{
        //    //如果稳定
        //    if (IsStableChiller)
        //    {

        //        //StableRecordList.Add(textBox17.Text);
        //        //if (StableRecordList.Count == 5)
        //        //{
        //        //    timer_Stable.Stop();
        //        //    MessageBox.Show("已经记录了5条记录");

        //        //    return;
        //        //}

        //        //timer_StableTemp.Stop();
        //        StableTimerStart();
        //    }


        //}


        #endregion

        ////时间函数！
        //System.Windows.Threading.DispatcherTimer timer_Stable = new System.Windows.Threading.DispatcherTimer();
        ///// <summary>
        ///// 稳定时间函数开始！
        ///// </summary>
        //private void StableTimerStart()
        //{
        //    //timer_Stable = new System.Windows.Threading.DispatcherTimer();
        //    timer_Stable.Interval = new TimeSpan(0, 0, 1);
        //    timer_Stable.Tick += new EventHandler(timeStable_Tick);
        //    timer_Stable.Start();
        //}


        ///// <summary>
        ///// 稳定时间事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void timeStable_Tick(object sender, EventArgs e)
        //{
        //    sec++;
        //    if (GlobelVar.IsStableChiller)
        //    {

        //        StableRecordList.Add(textBox17.Text);
        //        if (StableRecordList.Count == 5)
        //        {
        //            timer_Stable.Stop();
        //            MessageBox.Show("已经记录了5条记录");

        //            return;
        //        }
        //    }
        //    //StableTimePlay.Text=sec.ToString();
        //    else
        //    {
        //        timer_Stable.Stop();
        //        StableRecordList.Clear();
        //        StableTimePlay.Text = "00:00:00";

        //        //StableTimer_TempStart();
        //    }

        //}



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (GlobelVar.IsStableChiller == true)
            {
                button1.Background = new SolidColorBrush(Colors.Red);
                GlobelVar.IsStableChiller = false;
            }
            else
            {
                button1.Background = new SolidColorBrush(Colors.Green);
                GlobelVar.IsStableChiller = true;
            }
        }


        private void menu_info_TestChiller_Click(object sender, RoutedEventArgs e)
        {
            ChillerInfo chillerinfo = new ChillerInfo();
            chillerinfo.CatchRecord.Visibility = Visibility.Hidden;

            #region 把信息给读上来并显示20150920
            BackPanel.DBOperate.GetLastInfoRecordFromDateBase(BackPanel.InformationGlo.DBPath, GlobelVar.TestInfoDefGlo.TestInfo);
            //把数据库的东西读上来
            //GlobelVar.TestInfoDefGlo.TestInfo[0] = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM/dd");
            //GlobelVar.TestInfoDefGlo.TestInfo[1] = DateTime.Now.ToString("HH:mm:ss");
            //GlobelVar.TestInfoDefGlo.TestInfo[2] = "Car";
            chillerinfo.textBox1.Text = GlobelVar.TestInfoDefGlo.TestInfo[3];
            chillerinfo.textBox2.Text = GlobelVar.TestInfoDefGlo.TestInfo[4];
            chillerinfo.textBox3.Text = GlobelVar.TestInfoDefGlo.TestInfo[5];
            chillerinfo.textBox4.Text = GlobelVar.TestInfoDefGlo.TestInfo[6];
            chillerinfo.textBox5.Text = GlobelVar.TestInfoDefGlo.TestInfo[7];
            chillerinfo.textBox6.Text = GlobelVar.TestInfoDefGlo.TestInfo[10];

            #region 控制参数的读取20151020
            //Chiller添加20151020
            //控制参数的读取
            GlobelVar.ChillerControlParameterName = GlobelVar.TestInfoDefGlo.TestInfo[11];
            //制冷剂名称的读取
            GlobelVar.RefName = GlobelVar.TestInfoDefGlo.TestInfo[12];

            //控制参数读取后前台的反应20151020
            if (GlobelVar.ChillerControlParameterName == "出水温度")
            {
                chillerinfo.WaterTemperature.IsChecked = true;

                BackPanel.InformationGlo.senariocontrol = BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp;
                //3锁定在下面
                //this.WaterTemperature.IsEnabled=false;
            }
            else
            {
                chillerinfo.CoolingWater.IsChecked = true;

                BackPanel.InformationGlo.senariocontrol = BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate;
            }
            //制冷剂选择后前台的响应，以及动作
            if (GlobelVar.RefName == "R22")
            {
                chillerinfo.RBR22.IsChecked = true;

                UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R22;
            }
            else
            {
                chillerinfo.RBR134a.IsChecked = true;

                UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R134a;
            }

            #endregion 控制参数的读取20151020

            #endregion 把信息给读上来并显示20150920

            #region 锁定几个信息20150921：20151020增添
            chillerinfo.textBox1.IsEnabled = false;
            chillerinfo.textBox2.IsEnabled = false;
            chillerinfo.textBox3.IsEnabled = false;

            //20151020锁定增加项，即RidioBox，需要锁定不可以
            chillerinfo.WaterTemperature.IsEnabled = false;
            chillerinfo.CoolingWater.IsEnabled = false;
            chillerinfo.RBR22.IsEnabled = false;
            chillerinfo.RBR134a.IsEnabled = false;

            chillerinfo.CatchRecord.IsEnabled = false;

            if (GlobelVar.IsChillerChillerOn)
            {
                chillerinfo.textBox4.IsEnabled = false;
                chillerinfo.textBox5.IsEnabled = false;
                chillerinfo.textBox6.IsEnabled = false;

                chillerinfo.button2.IsEnabled = false;
            }
            else
            {
                chillerinfo.textBox4.IsEnabled = true;
                chillerinfo.textBox5.IsEnabled = true;
                chillerinfo.textBox6.IsEnabled = true;

                chillerinfo.button2.IsEnabled = true;
            }
            #endregion 锁定几个信息20150921

            chillerinfo.ShowDialog();
        }

        private void menu_Set_StableCoef_Click(object sender, RoutedEventArgs e)
        {
            StableJudgeConditionOfChiller stableChiller = new StableJudgeConditionOfChiller();
            stableChiller.ShowDialog();
        }

        private void menu_info_RecordTime_Click(object sender, RoutedEventArgs e)
        {
            SetStableTimeWindow setstabletimewindow = new SetStableTimeWindow();
            setstabletimewindow.ShowDialog();
        }

        #region 给GolbelVar.IsStableChiller赋值
        /// <summary>
        /// 判断现在的状态是否稳定：是则GolbelVar.IsStableCar为true；否则为false；
        /// </summary>
        private void StableJudgeFun(double[] Array_Agilent101_122, double[] Array_Agilent201_222, double ChillerInWaterTemp)
        {
            //double MidTemp1, MidTemp2, MidTemp3, MidTemp4;

            double EvaporatorPressDif, SuctionTempDif, InCoolingWaterTempDif, OutCoolingWaterTempDif=10, WaterFlowRateDif_Percent=10;
            //MidTemp1 = Math.Abs(GlobelVar.ChillerEvaperator_TemperatureSet - UtilityMod_Header.RefNistProp.R141b.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]));
            //MidTemp2 = Math.Abs(GlobelVar.ChillerInputWater_TemperatureSet - Convert.ToDouble(textBox9.Text));
            ////出水温度
            //MidTemp3 = Math.Abs(GlobelVar.ChillerOutputWater_TemperatureSet  - Convert.ToDouble(textBox10.Text));
            ////冷却水流量
            //MidTemp4 = Math.Abs(GlobelVar.ChillerCoolingWater_FlowRateSet  - Convert.ToDouble(textBox10.Text));

            #region 临时temp
            //textBox6.Text = Array_Agilent101_122[6 - 1].ToString("f2");

            //textBox7.Text = GlobelVar.ChillerEvapTem_ForControl.ToString("f2");// UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]).ToString("f2");// 7.ToString("f2");
            //textBox8.Text = Array_Agilent201_222[15 - 1].ToString("f3");// 3.ToString("f2");
            ////textBox9.Text = BackPanel.Control.Controllist[3].PV.ToString("f2");// GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");
            //textBox9.Text = ChillerInWaterTemp.ToString("f2");// GlobelVar.Array_Agilent101_122[8 - 1].ToString("f2");


            //switch (BackPanel.InformationGlo.senariocontrol)
            //{
            //    //这个是使用出水温度的时候
            //    case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:

            //        //控制器 是需要：出水温度还是冷却水流量20151008
            //        textBox10.Text = Array_Agilent201_222[1 - 1].ToString("f2");
            //        break;
            //    //这个是其余的情况
            //    default:
            //        //控制器 是需要：出水温度还是冷却水流量20151008
            //        textBox10.Text = Array_Agilent201_222[19 - 1].ToString("f2");
            //        break;
            //}
            #endregion

            //20151013更改
            double EvaporatorPress_Set = GlobelVar.ChillerEvaperatorPressureSetOnlyStable;
            double EvaporatorPress_Act = Array_Agilent201_222[15 - 1]; //ok
            EvaporatorPressDif = Math.Abs(EvaporatorPress_Set - EvaporatorPress_Act);

            double SuctionTemp_Set = GlobelVar.ChillerSuctionTempSetOnlyStable;
            double SuctionTemp_Act = Array_Agilent101_122[6 - 1]; //ok
            SuctionTempDif = Math.Abs(SuctionTemp_Set - SuctionTemp_Act);

            double InCoolingWaterTemp_Set =GlobelVar.ChillerInWaterTempSetOnlyStable;
            double InCoolingWaterTemp_Act = ChillerInWaterTemp;
            InCoolingWaterTempDif = Math.Abs(InCoolingWaterTemp_Set - InCoolingWaterTemp_Act);

            switch (BackPanel.InformationGlo.senariocontrol)
            {
                case InformationGlo.SenarioControl.ControlChillerWaterTemp:
                    double OutCoolingWaterTemp_Set = GlobelVar.ChillerOutWaterTempSetOnlyStable;
                    double OutCoolingWaterTemp_Act = Array_Agilent201_222[1 - 1];
                    OutCoolingWaterTempDif = Math.Abs(OutCoolingWaterTemp_Set - OutCoolingWaterTemp_Act);
                    break;
                case InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                    double WaterFlowRate_Set =GlobelVar.ChillerCoolingWaterFlowRateSetOnlyStable;// Convert.ToDouble(textBox5.Text);
                    double WaterFlowRate_Act = Array_Agilent201_222[19 - 1];
                    WaterFlowRateDif_Percent = Math.Abs((WaterFlowRate_Set - WaterFlowRate_Act) / WaterFlowRate_Set*100);
                    break;
            }


            //依据控制UT2吸气温度而定
            if (BackPanel.InformationGlo.IsChillerOn)
            {
                if (SuctionTempDif <= 2)
                {
                    //小于2度，锁定
                    BackPanel.Control.Set(2, "MOD", 1, 0);
                }
                if (SuctionTempDif > 2)
                {
                    BackPanel.Control.Set(2, "MOD", 0, 0);
                }
            }

            //MidTemp1 = Math.Abs(GlobelVar.ChillerEvaperator_TemperatureSet - UtilityMod_Header.RefNistProp.RefInUsing.Tsat_Vap(GlobelVar.Array_Agilent201_222[15 - 1]));
            //MidTemp2 = Math.Abs(GlobelVar.ChillerInputWater_TemperatureSet - BackPanel.Control.Controllist[3].PV);
            ////出水温度
            //MidTemp3 = Math.Abs(GlobelVar.ChillerOutputWater_TemperatureSet - GlobelVar.Array_Agilent201_222[1 - 1]);
            ////冷却水流量
            //MidTemp4 = Math.Abs(GlobelVar.ChillerCoolingWater_FlowRateSet - GlobelVar.Array_Agilent201_222[19 - 1]);


            if (StableJudgeFun_Child(EvaporatorPressDif, SuctionTempDif, InCoolingWaterTempDif, OutCoolingWaterTempDif, WaterFlowRateDif_Percent))
            {
                GlobelVar.IsStableChiller = true;
            }
            else
            {
                GlobelVar.IsStableChiller = false;
            }

        }

        /// <summary>
        /// 为了返回现在的误差，是否在要求的误差之内：是则返回true，否则false
        /// </summary>
        /// <returns></returns>
        private bool StableJudgeFun_Child(double EvaporatorPressDif, double SuctionTempDif, double InCoolingWaterTempDif, double OutCoolingWaterTempDif, double WaterFlowRateDif_Percent)
        {
            bool temp1, temp2, temp3, temp4=false;
            if (EvaporatorPressDif <= GlobelVar.ChillerEvaperator_PressRequire_Dif)
            { temp1 = true; }
            else
            { temp1 = false; }

            if (SuctionTempDif <= GlobelVar.ChillerSuction_TemperatureRequire_Dif)
            { temp2 = true; }
            else
            { temp2 = false; }

            if (InCoolingWaterTempDif <= GlobelVar.ChillerInputWater_TemperatureRequire_Dif)
            { temp3 = true; }
            else
            { temp3 = false; }

            switch (BackPanel.InformationGlo.senariocontrol)
            {
                case InformationGlo.SenarioControl.ControlChillerWaterTemp:
                    if (OutCoolingWaterTempDif <= GlobelVar.ChillerOutputWater_TemperatureRequire_Dif)
                    { temp4 = true; }
                    else
                    { temp4 = false; }
                    break;
                case InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                    if (WaterFlowRateDif_Percent <= GlobelVar.ChillerCoolingWater_FlowRateRequire_Percent_Dif)
                    { temp4 = true; }
                    else
                    { temp4 = false; }
                    break;
            }


            if (temp1 && temp2 && temp3 && temp4)
            {
                return true;
            }
            else
            {
                return false;
            }


            //if (GlobelVar.IsControlWaterTemperature == true)
            //{
            //    if (temp1 && temp2 && temp3)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    if (temp1 && temp2 && temp4)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}

            //switch (GlobelVar.IsControlWaterTemperature)
            //{
            //    case false:
            //        if (temp1 && temp2 && temp3)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;

            //        }

            //    case true:
            //        if (temp1 && temp2 && temp4)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }


            //}

        }
        #endregion


        #region 辅助线程

        #region 曲线


        private void CurveIni()
        {
            #region 通道添加20150910 ：20150911抽象出函数
            GlobleFun.AddChannel_Total();

            #endregion 通道添加





            realtimeCurves1.TestType = "FCU";
            //为什么后面的Channels一变，前面的就可以感受到？瞬时的，一定有绑定
            realtimeCurves1.Channels = GlobelVar.Channels;
            //public Channel(int no, int redNo, string name,double filter, string unit, string unitDesc, string type, int precision, double maximum, double minimum, double maxAnalog,double minAnalog)
            //Channel(no, no, "", fil, string unit, string unitDesc, string type, int precision, double maximum, double minimum, double maxAnalog,double minAnalog)
        }
        #endregion 曲线

        private void menu_Set_HeatDissipCoef_Click(object sender, RoutedEventArgs e)
        {
            CoeOfHeatDissipation coeofheatdissip = new CoeOfHeatDissipation();
            coeofheatdissip.ShowDialog();
        }

        #endregion 辅助线程

        private void menu_Set_FilterCoe_Click(object sender, RoutedEventArgs e)
        {
            FilterCoe filtercoe = new FilterCoe();
            filtercoe.Show();
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ReportView_Click(object sender, RoutedEventArgs e)
        {
            Report.MainWindow reportview = new Report.MainWindow();
            reportview.Show();
        }

        private void SuctionTemperature_Click(object sender, RoutedEventArgs e)
        {
            GlobleFun.IsFitButtonToTextbox(SuctionTemperature_Chiller, textBox1);
        }

        private void EvaporatorTemperature_AirCooledChiller_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EvaporatorPressure_AirCooledChiller_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SuctionTemperature_AirCooledChiller_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InTdbOfCondenser_AirCooledChiller_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InTdbOfEvaporator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InWdbOfEvaporator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OutStaticPresserOfEvaporator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FanTerminalVoltageOfEvaporator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InWaterTemperatureOfEvaporator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WaterFlowOfEvaporator_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

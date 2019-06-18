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

using System.IO;
using Microsoft.Win32;
using BackPanel;
using System.Windows.Interop;
using System.Runtime.InteropServices;


using System.ComponentModel;

using Report;

namespace WpfApplication2
{
    /// <summary>
    /// ChillerInfo.xaml 的交互逻辑
    /// </summary>
    public partial class CondenserInfo: Window
    {
        public CondenserInfo()
        {
            InitializeComponent();
        }



        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (GlobelVar.InfoChangeChiller == 1)
            {
                this.Close();
            }
            else
            {
                this.Close();
                MainWindow newmainwindow = new MainWindow();
                //new MainWindow().Show();
                newmainwindow.Show();
            }

            //this.Close();
            //MainWindow newmainwindow = new MainWindow();
            ////new MainWindow().Show();
            //newmainwindow.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            bool temp = IsInputRightOfChillerInfo();
            if (GlobelVar.InfoChangeChiller == 1)
            {
                if (temp)
                {
                    #region 为了保证可以把再次输入的信息插入到调出数据库的最后一条：20150920
                    //从前台获取的信息储存到数组GlobelVar.TestInfoDefGlo.TestInfo【】里
                    PutInfoIntoTestInfoDefGlo();

                    #region 主界面调用时，把更改存到前台显示的地方
                    GlobelVar.IsInfoFromMain = true;
                    switch (BackPanel.InformationGlo.senariocontrol)
                    {
                        case InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                            BackPanel.Control.Set(4, "SV", BackPanel.InformationGlo.ChillerWaterFlowRate_ForControl, 2);
                            break;
                        case InformationGlo.SenarioControl.ControlChillerWaterTemp:
                            break;
                    }
                    
                    //Thread.Sleep(sleeptime);
                    //BackPanel.InformationGlo.ChillerWaterFlowRate_ForControl
                    #endregion


                    //从前台转移到后台
                    BackPanel.DBOperate.TestInfoDef.TestInfo = GlobelVar.TestInfoDefGlo.TestInfo;
                    //后台进行数据插入到相应的数据库
                    BackPanel.DBOperate.AddInfoRecordToDateBase(BackPanel.InformationGlo.DBPath);
                    #endregion 为了保证可以把再次输入的信息插入到调出数据库的最后一条：20150920

                    BackPanel.InformationGlo.CurrentExpEquiqNormalCoolingCapacity = Convert.ToDouble(textBox4.Text);

                    //如果没有重置则会正常走
                    if (BackPanel.InformationGlo.IsAlertingReset)
                    {
                    }
                    else
                    {
                        //if (BackPanel.InformationGlo.CurrentExpEquiqNormalCoolingCapacity * 1000 >= 7000)
                        //{
                        //    //量热器加热器1，需要添加
                        //    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[24], true, true);
                        //    //量热器加热器2，在额定功率为7000W以上，开启20151106
                        //    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[25], true, true);
                        //}
                        //if (BackPanel.InformationGlo.CurrentExpEquiqNormalCoolingCapacity * 1000 < 7000)
                        //{
                        //    //量热器加热器1，需要添加
                        //    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[24], true, true);
                        //    //量热器加热器2，在额定功率为7000W以上，开启20151106
                        //    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[25], false, true);
                        //}
                    }

                    //Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue = textBox6.Text;
                    if ((bool)this.CoolingWater.IsChecked)
                    {
                        Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue = textBox6.Text;
                    }
                    this.Close();
                }
            }
            else
            {



                if (temp)
                {
                    #region 在PLC操作的时候，不允许其他动作
                    //this.button2.IsEnabled = false;
                    //this.button3.IsEnabled = false;
                    //this.CatchRecord.IsEnabled = false;
                    #endregion

                    bool IsSelectCtrlWaterOutTemp = (bool)WaterTemperature.IsChecked;
                    //出口水温/流量计转换-UT4输入切换，点位常闭在控制出口水温 即为false；
                    //BackPanel.PLCMod.CommonStartEquip(true);
                    //BackPanel.PLCMod.ChillerStartEquip(true);

                    BackPanel.PLCMod.IsTestCarComp(false);
                    BackPanel.PLCMod.AuxiliaryWaterSystem(true);


                    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[32], !IsSelectCtrlWaterOutTemp, true);
                    //20151104

                    //控制器选择控制水流量还是温度：20151129
                    BackPanel.Control.BuildControlerList(BackPanel.Control.Controllist, GlobelVar.PathDebug + "CarChiller.mdb", "Table_Controller");
                    //锁住所有的控制器
                    BackPanel.Control.ControlLockAll_ForChiller(BackPanel.Control.Controllist);

                    //BackPanel.Control.ControlInitiate_ForChiller(BackPanel.Control.Controllist);
                    BackPanel.Control.ControlInitiate_ForChiller();
                    //是否调用之前记录分支20150906
                    if (GlobelVar.IsGetBeforeInfo_Chiller == true)
                    {
                        #region 为了保证可以把再次输入的信息插入到调出数据库的最后一条：20150920
                        //从前台获取的信息储存到数组GlobelVar.TestInfoDefGlo.TestInfo【】里
                        PutInfoIntoTestInfoDefGlo();
                        //从前台转移到后台
                        BackPanel.DBOperate.TestInfoDef.TestInfo = GlobelVar.TestInfoDefGlo.TestInfo;
                        //后台进行数据插入到相应的数据库
                        BackPanel.DBOperate.AddInfoRecordToDateBase(GlobelVar.DirBefore_Glo);
                        #endregion 为了保证可以把再次输入的信息插入到调出数据库的最后一条：20150920

                        //这个一显示下面的对话框，这个IsGetBeforeInfo_Chiller就变为假；否则这个试验以后就一直只能走这一路了！20150906
                        GlobelVar.IsGetBeforeInfo_Chiller = false;

                        Chiller chiller = new Chiller();
                        //if (WaterTemperature.IsChecked == true)
                        //{
                        //    GlobelVar.IsControlWaterTemperature = true;
                        //    //chiller.Cnn.IsChecked = true;
                        //}
                        //else
                        //{
                        //    GlobelVar.IsControlWaterTemperature = false;
                        //}

                        //给辅机开停场景赋值20150929
                        BackPanel.InformationGlo.senarioAuxi = BackPanel.InformationGlo.SenarioAuxi.Chiller;


                        chiller.Show();


                        if ((bool)this.CoolingWater.IsChecked)
                        {
                            Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue = textBox6.Text;
                        }
                        this.Close();
                    }
                    else
                    {
                        DateTime dt = DateTime.Now;
                        string dir = "D:\\MyData\\" + dt.Year.ToString("d4") + dt.Month.ToString("d2");
                        GlobelVar.Dir_Glo = dir;
                        //唯一确定那个的数据库
                        GlobelVar.FileName_Glo =dt.ToString("yyyyMMdd_HHmmss")+ "Chiller" + textBox3.Text;
                        GlobelVar.FileName_Glo = GlobelVar.FileName_Glo +  ".mdb";
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }

                        SaveFileDialog openDialog = new SaveFileDialog()
                        {
                            Title = "打开\"水冷压缩机组\"测试数据",
                            Filter = "\"水冷压缩机组\"数据文件(*.mdb)|*.mdb",
                            InitialDirectory = dir,
                            AddExtension = true,
                            FileName = GlobelVar.FileName_Glo
                        };

                        if (openDialog.ShowDialog() == true)
                        {
                            //第一步建立数据库
                            BackPanel.DBOperate.CreateDateBase(GlobelVar.Dir_Glo + "\\" + GlobelVar.FileName_Glo);
                            //20151222报表数据库路径
                            Report.DBPath_ForReport.DBPath_ForReportChild = GlobelVar.Dir_Glo + "\\" + GlobelVar.FileName_Glo;
                            #region 第二步插入输入的信息
                            //第二步插入输入的信息

                            //从前台获取GlobelVar.TestInfoDefGlo.TestInfo信息
                            PutInfoIntoTestInfoDefGlo();

                            BackPanel.DBOperate.TestInfoDef.TestInfo = GlobelVar.TestInfoDefGlo.TestInfo;
                            BackPanel.DBOperate.AddInfoRecordToDateBase(GlobelVar.Dir_Glo + "\\" + GlobelVar.FileName_Glo);
                            #endregion 第二步插入输入的信息

                            //把新建的数据库路径储存起来：20150917:20150920新家，这个之前忘了加！
                            BackPanel.InformationGlo.DBPath = GlobelVar.Dir_Glo + "\\" + GlobelVar.FileName_Glo;

                            Chiller chiller = new Chiller();

                            //移到上方20151129
                            //if (WaterTemperature.IsChecked == true)
                            //{
                            //    GlobelVar.IsControlWaterTemperature = true;
                            //    //chiller.Cnn.IsChecked = true;
                            //}
                            //else
                            //{
                            //    GlobelVar.IsControlWaterTemperature = false;
                            //}

                            //给辅机开停场景赋值20150929
                            BackPanel.InformationGlo.senarioAuxi = BackPanel.InformationGlo.SenarioAuxi.Chiller;

                            chiller.Show();


                            if ((bool)this.CoolingWater.IsChecked)
                            {
                                Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue = textBox6.Text;
                            }
                            //Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue = textBox6.Text;
                            this.Close();
                        }
                        else
                        {
                            return;
                        }
                    }





                }
                else
                {

                }
            }


        }
        /// <summary>
        /// 从前台获取信息储存到全局变量GlobelVar.TestInfoDefGlo.TestInfo20150907：提取的
        /// </summary>
        private void PutInfoIntoTestInfoDefGlo()
        {
            GlobelVar.TestInfoDefGlo.TestInfo[0] = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM/dd");
            GlobelVar.TestInfoDefGlo.TestInfo[1] = DateTime.Now.ToString("HH:mm:ss");
            GlobelVar.TestInfoDefGlo.TestInfo[2] = "Chiller";
            GlobelVar.TestInfoDefGlo.TestInfo[3] = textBox1.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[4] = textBox2.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[5] = textBox3.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[6] = textBox4.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[7] = textBox5.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[8] = "--";
            GlobelVar.TestInfoDefGlo.TestInfo[9] = "--";
            GlobelVar.TestInfoDefGlo.TestInfo[10] = textBox6.Text;
            //20151020选择控制参数
            GlobelVar.TestInfoDefGlo.TestInfo[11] = GlobelVar.ChillerControlParameterName;
            //选择制冷剂
            GlobelVar.TestInfoDefGlo.TestInfo[12] = GlobelVar.RefName;

            GlobelVar.TestInfoDefGlo.TestInfo[13] = "--";

            //被测设备名义冷量
            BackPanel.InformationGlo.CurrentExpEquiqNormalCoolingCapacity = Convert.ToDouble(textBox4.Text);

            Report.ReportParameterMySelf_ForChiller.RP2Manufacturer=textBox1.Text;
            Report.ReportParameterMySelf_ForChiller.RP3ModelIdentity = textBox2.Text;
            Report.ReportParameterMySelf_ForChiller.RP4OutNumb = textBox3.Text;
            Report.ReportParameterMySelf_ForChiller.RP5Refrig="R134a";
            Report.ReportParameterMySelf_ForChiller.RP6NormalCoolingCapacity = textBox4.Text;
            Report.ReportParameterMySelf_ForChiller.RP7NormalPower = textBox5.Text;
            Report.ReportParameterMySelf_ForChiller.RP8NormalWaterFlow=textBox6.Text;

            BackPanel.InformationGlo.ChillerWaterFlowRate_ForControl = Convert.ToDouble(textBox6.Text);
            //ChillerWaterFlowRate_ForControl

            GlobelVar.ChillerCoolingWaterFlowRateSetOnlyStable = Convert.ToDouble(textBox6.Text);
        }

        /// <summary>
        /// 判断信息输入，是否错误！
        /// 如果都没有错误，则返回true；如果有一个有错误，则返回假
        /// </summary>
        /// <returns></returns>
        private bool IsInputRightOfChillerInfo()
        {
            //TextError是false时，没有错误；是true时，有错误
            bool TextError = false;
            if (textBox1.Text == "")
            {
                TextError = true;
                //MessageBox.Show("当前输入为空，请输入正确类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (textBox2.Text == "")
            {
                TextError = true;
                //MessageBox.Show("当前输入为空，请输入正确类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (textBox4.Text == "")
            {
                TextError = true;
                //MessageBox.Show("当前输入为空，请输入正确类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            bool NumError = false;
            double temp4 = 0;
            if (double.TryParse(textBox4.Text, out temp4))
            {

                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
            }
            else
            {
                NumError = true;
                //MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            double temp5 = 0;
            if (double.TryParse(textBox5.Text, out temp5))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
            }
            else
            {
                NumError = true;
                //MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            double temp6 = 0;
            if (double.TryParse(textBox6.Text, out temp6))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
            }
            else
            {
                NumError = true;
                //MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            if (!(TextError || NumError))
            {
                return true;
            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        private void CatchRecord_Click(object sender, RoutedEventArgs e)
        {
            GlobelVar.IsChillerInfoGetRecord = true;

            DateTime dt = DateTime.Now;
            string dir = "D:\\MyData\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            OpenFileDialog openDialog = new OpenFileDialog()
            {
                Title = "打开\"水冷压缩机组\"测试数据",
                Filter = "\"水冷压缩机组\"数据文件(*.mdb)|*.mdb",
                InitialDirectory = dir,
                AddExtension = true,
            };

            if (openDialog.ShowDialog() == true)
            {
                //确实调用之前的，为了确定，一个分支而定，20150906
                GlobelVar.IsGetBeforeInfo_Chiller = true;

                GlobelVar.DirBefore_Glo = openDialog.FileName;
                //获取路径信息20150917:这个路径是用来给数据库每次扫描操作用的，所以最重要！20150920
                BackPanel.InformationGlo.DBPath = openDialog.FileName;

                //20151222报表数据库路径
                Report.DBPath_ForReport.DBPath_ForReportChild = GlobelVar.DirBefore_Glo;

                BackPanel.DBOperate.GetLastInfoRecordFromDateBase(GlobelVar.DirBefore_Glo, GlobelVar.TestInfoDefGlo.TestInfo);

                #region 20151020添加如果用了Car的数据库要提示，并且返回
                if (GlobelVar.TestInfoDefGlo.TestInfo[2] == "Car")
                {
                    MessageBoxResult mbr = MessageBox.Show("您选择的是压缩机的数据库，请更换为冷水机组数据库！", "选择数据库错误", MessageBoxButton.OK);
                    if (mbr == MessageBoxResult.OK)
                    {
                        //this.Close();
                        //Car car = new Car();
                        //car.Show();
                        return;
                    }
                }
                #endregion 20151020添加如果用了Car的数据库要提示，并且返回

                //把数据库的东西读上来
                //GlobelVar.TestInfoDefGlo.TestInfo[0] = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM/dd");
                //GlobelVar.TestInfoDefGlo.TestInfo[1] = DateTime.Now.ToString("HH:mm:ss");
                //GlobelVar.TestInfoDefGlo.TestInfo[2] = "Car";
                textBox1.Text = GlobelVar.TestInfoDefGlo.TestInfo[3];
                textBox2.Text = GlobelVar.TestInfoDefGlo.TestInfo[4];
                textBox3.Text = GlobelVar.TestInfoDefGlo.TestInfo[5];
                textBox4.Text = GlobelVar.TestInfoDefGlo.TestInfo[6];
                textBox5.Text = GlobelVar.TestInfoDefGlo.TestInfo[7];
                textBox6.Text = GlobelVar.TestInfoDefGlo.TestInfo[10];

                #region 控制参数的读取20151020
                //Chiller添加20151020
                //控制参数的读取
                GlobelVar.ChillerControlParameterName = GlobelVar.TestInfoDefGlo.TestInfo[11];
                //制冷剂名称的读取
                GlobelVar.RefName = GlobelVar.TestInfoDefGlo.TestInfo[12];

                //控制参数读取后前台的反应20151020
                if (GlobelVar.ChillerControlParameterName == "出水温度")
                {
                    WaterTemperature.IsChecked = true;

                    BackPanel.InformationGlo.senariocontrol = BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp;
                    //3锁定在下面
                    //this.WaterTemperature.IsEnabled=false;
                }
                else
                {
                    CoolingWater.IsChecked = true;

                    BackPanel.InformationGlo.senariocontrol = BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate;
                }
                //制冷剂选择后前台的响应，以及动作
                if (GlobelVar.RefName == "R22")
                {
                    RBR22.IsChecked = true;

                    UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R22;
                }
                else
                {
                    RBR134a.IsChecked = true;

                    UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R134a;
                }

                #endregion 控制参数的读取20151020


                //GlobelVar.TestInfoDefGlo.TestInfo[10] = "--";l
                #region 调取记录后锁定前三项20150921
                this.textBox1.IsEnabled = false;
                this.textBox2.IsEnabled = false;
                this.textBox3.IsEnabled = false;

                //20151020锁定增加项，即RidioBox，需要锁定不可以
                this.WaterTemperature.IsEnabled = false;
                this.CoolingWater.IsEnabled = false;
                this.RBR22.IsEnabled = false;
                this.RBR134a.IsEnabled = false;

                #endregion 调取记录后锁定前三项20150921

            }
            else
            {
                return;
            }

            
            //public static bool IsChillerInfoGetRecord = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaterTemperature_Checked(object sender, RoutedEventArgs e)
        {
            //给控制器场景赋值20150929
            BackPanel.InformationGlo.senariocontrol = BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp;
            //20151020加
            GlobelVar.ChillerControlParameterName = "出水温度";

            textBox6.Text = "1.400";
            textBox6.IsEnabled = false;

            Report.ReportParameterMySelf_ForChiller.RP9ControlVar = "出水温度";
            Report.ReportParameterMySelf_ForChiller.RP13ControlVarName = "出水温度 ℃：";
            Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue = "35.00";


        }

        BackgroundWorker _PLCAlert = new BackgroundWorker();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler(MaintTime_Tick);
            timer.Start();

            _PLCAlert.DoWork += new DoWorkEventHandler(_PLCAlert_DoWork);
            //if (BackPanel.Strategy.IsTherePLC_Error == true)
            //{
            //    this.ProjectSelect.IsEnabled = false;
            //}

        }

        void _PLCAlert_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            BackPanel.PLCMod.PLCAlter(BackPanel.PLCMod.PLCDIList);

            //这个是PLC的控制策略20150922:ForChiller20151201
            BackPanel.Strategy.StrategyPLCAlarm_ForChiller(BackPanel.PLCMod.PLCDIList, BackPanel.PLCMod.PLCDOList);
        }

        public void MaintTime_Tick(object sender, EventArgs e)
        {
            if (!_PLCAlert.IsBusy)
            {
                _PLCAlert.RunWorkerAsync();
            }

            if (BackPanel.Strategy.IsTherePLC_Error)
            {
                this.CatchRecord.IsEnabled = false;
                this.button2.IsEnabled = false;
                //MessageBox.Show("设备出现问题，请检查无误后退出试验！重新开启！");
                //MessageBoxResult mbr = MessageBox.Show("设备出现问题，请检查无误后退出试验！重新开启！", "报警", MessageBoxButton.OK);
                //if (mbr == MessageBoxResult.OK)
                if (true)
                {
                    BackPanel.Strategy.IsTherePLC_Error = false;
                    //初始化PLC报警
                    BackPanel.Strategy.StrategyPLCReset();
                }
                //else
                //{
                //    BackPanel.Strategy.IsReturnFromMain = true;

                //    Chiller Chill = new WpfApplication2.Chiller();
                //    Chill.Show();
                //    //Car car = new Car();
                //    //car.Show();
                //    this.Close();
                //}
            }
        }

        #region 取消关闭按钮功能20150917
        //取消左上角关闭按钮功能：第一部分20150917
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        #endregion 取消关闭按钮功能20150917

        private void CoolingWater_Checked(object sender, RoutedEventArgs e)
        {
            //给控制器场景赋值20150929
            BackPanel.InformationGlo.senariocontrol = BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate;
            //20151020加
            GlobelVar.ChillerControlParameterName = "冷却水流量";
            if (GlobelVar.InfoChangeChiller == 1)
            {

            }
            else if(GlobelVar.IsChillerInfoGetRecord==true)
            {}
            else
            {
                textBox6.Text = "";
            }
            textBox6.IsEnabled = true;


            Report.ReportParameterMySelf_ForChiller.RP9ControlVar = "冷却水流量";
            Report.ReportParameterMySelf_ForChiller.RP13ControlVarName = "冷却水流量 m3/h：";
            //Report.ReportParameterMySelf_ForChiller.RP14ControlVarValue = textBox6.Text; 20151224
        }

        #region 20151020添加制冷剂选择CheckBox:改为RadioBox

        //private void CBR22_Checked(object sender, RoutedEventArgs e)
        //{
        //    //CBR134a.IsChecked = false;

        //    UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R22;

        //    //数据库存储信息
        //    GlobelVar.RefName = "R22";
        //}

        //private void CBR134a_Checked(object sender, RoutedEventArgs e)
        //{
        //    //CBR22.IsChecked = false;

        //    UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R134a;
        //    //数据库存储信息
        //    GlobelVar.RefName = "R134a";
        //}


        private void RBR22_Checked(object sender, RoutedEventArgs e)
        {
            UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R22;

            //数据库存储信息
            GlobelVar.RefName = "R22";
        }

        private void RBR134a_Checked(object sender, RoutedEventArgs e)
        {
            UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R134a;
            //数据库存储信息
            GlobelVar.RefName = "R134a";
        }
        #endregion 20151020添加制冷剂选择CheckBox

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            GlobelVar.IsChillerInfoGetRecord = false;

            timer.Stop();
        }
    }
}

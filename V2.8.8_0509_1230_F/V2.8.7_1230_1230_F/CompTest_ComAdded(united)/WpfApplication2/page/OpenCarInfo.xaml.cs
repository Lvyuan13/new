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

using Microsoft.Win32;
using System.IO;
using BackPanel;
using System.Windows.Interop;
using System.Runtime.InteropServices;

using System.ComponentModel;


namespace WpfApplication2
{
    /// <summary>
    /// CarInfo.xaml 的交互逻辑
    /// </summary>
    public partial class OpenCarInfo : Window
    {
        public OpenCarInfo()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            //检验输入是否正确
            bool temp = IsInputRight();

            if (GlobelVar.InfoChangeCar == 1)
            {
                if (temp)
                {
                    #region 为了保证可以把再次输入的信息插入到调出数据库的最后一条：20150920
                    //从前台获取的信息储存到数组GlobelVar.TestInfoDefGlo.TestInfo【】里
                    PutInfoIntoTestInfoDefGlo();

                    #region 试验中要改转速的20151225
                    //为了在实验中改动可以传到前台
                    GlobelVar.IsInfoFromMain = true;

                    double RotateOfElecMotor = Convert.ToInt32(BackPanel.InformationGlo.CarCompressorRotateSet_ForControl * BackPanel.InformationGlo.CompressorDiameter_FromInfo / 300);
                    BackPanel.Control.Set(6, "SV", RotateOfElecMotor, 0);
                    #endregion 试验中要改转速的20151225



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

                    this.Close();
                }
            }
            else
            {
                if (temp)
                {
                    #region 在PLC操作的时候，不允许其他动作
                    this.btConfirm.IsEnabled = false;
                    this.btReturn.IsEnabled = false;
                    this.CatchRecord.IsEnabled = false;
                    #endregion


                    //BackPanel.PLCMod.CommonStartEquip(true);
                    //BackPanel.PLCMod.CarStartEquip(true);
                    BackPanel.PLCMod.IsTestCarComp(true);

                    BackPanel.PLCMod.AuxiliaryWaterSystem(true);

                    ////3.压缩机离合器供电选择24V/12V选择20151108
                    //PLCMod.SendPLCBitVar(PLCDOList[20], TF, true);
                    BackPanel.PLCMod.CarCompressorClutchVoltage();
                    //4.K26 UT4控制器，测量打到水流量侧20151125
                    PLCMod.SendPLCBitVar(PLCMod.PLCDOList[32], true, true);

                    //是否调用之前记录分支20150906
                    if (GlobelVar.IsGetBeforeInfo_Car == true)
                    {
                        #region 为了保证可以把再次输入的信息插入到调出数据库的最后一条：20150920
                        //从前台获取的信息储存到数组GlobelVar.TestInfoDefGlo.TestInfo【】里
                        PutInfoIntoTestInfoDefGlo();
                        //从前台转移到后台
                        BackPanel.DBOperate.TestInfoDef.TestInfo = GlobelVar.TestInfoDefGlo.TestInfo;
                        //后台进行数据插入到相应的数据库
                        BackPanel.DBOperate.AddInfoRecordToDateBase(GlobelVar.DirBefore_Glo);
                        #endregion 为了保证可以把再次输入的信息插入到调出数据库的最后一条：20150920

                        //给辅机开停场景赋值20150929
                        BackPanel.InformationGlo.senarioAuxi = BackPanel.InformationGlo.SenarioAuxi.Car;

                        //给控制器场景赋值20150929
                        BackPanel.InformationGlo.senariocontrol = BackPanel.InformationGlo.SenarioControl.ControlCar;
                        //控制list初始化20151122
                        BackPanel.Control.BuildControlerList(BackPanel.Control.Controllist, GlobelVar.PathDebug + "CarChiller.mdb", "Table_Controller");
                        BackPanel.Control.ControlLockAll_ForCar(BackPanel.Control.Controllist);

                        BackPanel.Control.ControlInitiate_ForCar(BackPanel.Control.Controllist);

                        Car car = new Car();
                        car.Show();
                        //这个一显示下面的对话框，这个IsGetBeforeInfo_Car就变为假；否则这个试验以后就一直只能走这一路了！20150906
                        GlobelVar.IsGetBeforeInfo_Car = false;




                        this.Close();
                    }
                    else
                    {
                        DateTime dt = DateTime.Now;
                        string dir = "D:\\MyData\\" + dt.Year.ToString("d4") + dt.Month.ToString("d2");
                        //全局目录等于当前目录
                        GlobelVar.Dir_Glo = dir;


                        //GlobelVar.FileName;
                        //全局文件名等于当前文件名 :
                        //添加的唯一变量是出厂编号！20150920
                        GlobelVar.FileName_Glo = dt.ToString("yyyyMMdd_HHmmss") + "CarCompressor" + textBox3.Text;
                        GlobelVar.FileName_Glo = GlobelVar.FileName_Glo + ".mdb";

                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }

                        SaveFileDialog openDialog = new SaveFileDialog()
                        {
                            Title = "打开\"汽车空调压缩机\"测试数据",
                            Filter = "\"汽车空调压缩机\"数据文件(*.mdb)|*.mdb",
                            InitialDirectory = dir,
                            AddExtension = true,
                            FileName = GlobelVar.FileName_Glo
                        };

                        if (openDialog.ShowDialog() == true)
                        {

                            //第一步建立数据库
                            BackPanel.DBOperate.CreateDateBase(GlobelVar.Dir_Glo + "\\" + GlobelVar.FileName_Glo);
                            //20151222
                            Report.DBPath_ForReport.DBPath_ForReportChild = GlobelVar.Dir_Glo + "\\" + GlobelVar.FileName_Glo;

                            #region 第二步插入输入的信息
                            //第二步插入输入的信息

                            //从前台获取的信息储存到数组GlobelVar.TestInfoDefGlo.TestInfo【】里
                            PutInfoIntoTestInfoDefGlo();
                            //从前台转移到后台
                            BackPanel.DBOperate.TestInfoDef.TestInfo = GlobelVar.TestInfoDefGlo.TestInfo;
                            //后台进行数据插入到相应的数据库
                            BackPanel.DBOperate.AddInfoRecordToDateBase(GlobelVar.Dir_Glo + "\\" + GlobelVar.FileName_Glo);
                            #endregion 第二步插入输入的信息

                            //把新建的数据库路径储存起来：20150917
                            BackPanel.InformationGlo.DBPath = GlobelVar.Dir_Glo + "\\" + GlobelVar.FileName_Glo;

                            //给辅机开停场景赋值20150929
                            BackPanel.InformationGlo.senarioAuxi = BackPanel.InformationGlo.SenarioAuxi.Car;

                            //给控制器场景赋值20150929
                            BackPanel.InformationGlo.senariocontrol = BackPanel.InformationGlo.SenarioControl.ControlCar;
                            //给控制器list初始化20151122
                            BackPanel.Control.BuildControlerList(BackPanel.Control.Controllist, GlobelVar.PathDebug + "CarChiller.mdb", "Table_Controller");
                            BackPanel.Control.ControlLockAll_ForCar(BackPanel.Control.Controllist);
                            BackPanel.Control.ControlInitiate_ForCar(BackPanel.Control.Controllist); //控制器初始化
                            Car car = new Car();
                            car.Show();

                            this.Close();
                        }
                        else
                        {
                            return;
                        }
                    }



                }
            }

        }
        /// <summary>
        ///  从前台获取信息储存到全局变量GlobelVar.TestInfoDefGlo.TestInfo：20150907提取的
        /// </summary>
        private void PutInfoIntoTestInfoDefGlo()
        {

            GlobelVar.TestInfoDefGlo.TestInfo[0] = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM/dd");
            GlobelVar.TestInfoDefGlo.TestInfo[1] = DateTime.Now.ToString("HH:mm:ss");
            GlobelVar.TestInfoDefGlo.TestInfo[2] = "Car";
            GlobelVar.TestInfoDefGlo.TestInfo[3] = textBox1.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[4] = textBox2.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[5] = textBox3.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[6] = textBox4.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[7] = textBox5.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[8] = textBox6.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[9] = textBox7.Text;
            GlobelVar.TestInfoDefGlo.TestInfo[10] = "--";

            //chiller里要用的
            GlobelVar.TestInfoDefGlo.TestInfo[11] = "--";
            //制冷剂名称
            GlobelVar.TestInfoDefGlo.TestInfo[12] = GlobelVar.RefName;

            //压缩机离合器电压选择24V/12V
            if (RB24V.IsChecked.Value == true)
            {
                GlobelVar.TestInfoDefGlo.TestInfo[13] = "24V";
            }
            else
            {
                GlobelVar.TestInfoDefGlo.TestInfo[13] = "12V";
            }

            BackPanel.InformationGlo.CurrentExpEquiqNormalCoolingCapacity = Convert.ToDouble(textBox4.Text);
            BackPanel.InformationGlo.CompressorDiameter_FromInfo = Convert.ToDouble(textBox7.Text);


            //报表参数 info那一块
            Report.ReportParameterMySelf.RP2Manufacturer = textBox1.Text;
            Report.ReportParameterMySelf.RP3ModelIdentity=textBox2.Text;
            Report.ReportParameterMySelf.RP4OutNumb=textBox3.Text;
            Report.ReportParameterMySelf.RP5Refrig="R134a";
            Report.ReportParameterMySelf.RP6NormalCoolingCapacity=textBox4.Text;
            Report.ReportParameterMySelf.RP7NormalPower=textBox5.Text;
            Report.ReportParameterMySelf.RP8RollDiameter=textBox7.Text;
            if (RB24V.IsChecked == true)
            {
                Report.ReportParameterMySelf.RP9ClutchVoltage = "24V";
            }
            else
            {
                Report.ReportParameterMySelf.RP9ClutchVoltage="12V";
            }

            //GlobelVar.CarCompressorRotateSet = Convert.ToDouble(textBox6.Text);
            BackPanel.InformationGlo.CarCompressorRotateSet_ForControl = Convert.ToDouble(textBox6.Text);
            
            
        }

        

        private bool IsInputRight()
        {
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

            double temp7 = 0;
            if (double.TryParse(textBox7.Text, out temp7))
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

        //System.Windows.Threading.DispatcherTimer _PLCAlert = new System.Windows.Threading.DispatcherTimer();
        BackgroundWorker _PLCAlert = new BackgroundWorker();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //取消左上角关闭按钮功能：第二部分20150917 
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            _PLCAlert.DoWork += new DoWorkEventHandler(_PLCAlert_DoWork);

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler(MaintTime_Tick);
            timer.Start();

            if(GlobelVar.IsCarChillerOn)
            {

            }
        }

        void _PLCAlert_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            BackPanel.PLCMod.PLCAlter(BackPanel.PLCMod.PLCDIList);
            //这个是PLC的控制策略20150922
            BackPanel.Strategy.StrategyPLCAlarm_ForCar(BackPanel.PLCMod.PLCDIList, BackPanel.PLCMod.PLCDOList);
        }

        public void MaintTime_Tick(object sender, EventArgs e)
        {
            if (!_PLCAlert.IsBusy)
            {
                _PLCAlert.RunWorkerAsync();
            }

            if (BackPanel.Strategy.IsTherePLC_Error == true)
            {
                CatchRecord.IsEnabled = false;
                btConfirm.IsEnabled = false;
                groupBox3.IsEnabled = false;
                groupBox2.IsEnabled = false;

                //MessageBoxResult mbr = MessageBox.Show("请确认设备关闭,故障排除后点击确认!", "报警", MessageBoxButton.OK);
                //if (mbr == MessageBoxResult.OK)
                if (true)
                {
                    BackPanel.Strategy.IsTherePLC_Error = false;
                    //PLC低压寄存器复位！强制的20150928改:20150930
                    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[5], false, true);
                    //量热器低液位寄存器复位！
                    BackPanel.PLCMod.SendPLCBitVar(PLCMod.PLCDIList[2], false, true);
                }
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
        //调取记录
        private void CatchRecord_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            string dir = "D:\\MyData\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            OpenFileDialog openDialog = new OpenFileDialog()
            {
                Title = "打开\"汽车空调压缩机\"测试数据",
                Filter = "\"汽车空调压缩机\"数据文件(*.mdb)|*.mdb",
                InitialDirectory = dir,
                AddExtension = true,
            };

            if (openDialog.ShowDialog() == true)
            {

                //确实调用之前的，为了确定，一个分支而定，20150906
                GlobelVar.IsGetBeforeInfo_Car = true;
                //获取包括文件名在内的路径
                GlobelVar.DirBefore_Glo = openDialog.FileName;
                //获取路径信息20150917:这个路径是用来给数据库每次扫描操作用的，所以最重要！
                BackPanel.InformationGlo.DBPath = openDialog.FileName;


                //20151222报表
                Report.DBPath_ForReport.DBPath_ForReportChild = BackPanel.InformationGlo.DBPath;

                //把数据库的东西读上来
                BackPanel.DBOperate.GetLastInfoRecordFromDateBase(GlobelVar.DirBefore_Glo, GlobelVar.TestInfoDefGlo.TestInfo);

                #region 20151020添加如果用了Chiller的数据库要提示，并且返回
                if (GlobelVar.TestInfoDefGlo.TestInfo[2] == "Chiller")
                {
                    MessageBoxResult mbr = MessageBox.Show("您选择的是冷水机组的数据库，请更换为压缩机数据库！", "选择数据库错误", MessageBoxButton.OK);
                    if (mbr == MessageBoxResult.OK)
                    {
                        //this.Close();
                        //Car car = new Car();
                        //car.Show();
                        return;
                    }
                }
                #endregion 20151020添加如果用了Chiller的数据库要提示，并且返回


                //GlobelVar.TestInfoDefGlo.TestInfo[0] = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM/dd");
                //GlobelVar.TestInfoDefGlo.TestInfo[1] = DateTime.Now.ToString("HH:mm:ss");
                //GlobelVar.TestInfoDefGlo.TestInfo[2] = "Car";
                textBox1.Text = GlobelVar.TestInfoDefGlo.TestInfo[3];
                textBox2.Text = GlobelVar.TestInfoDefGlo.TestInfo[4];
                textBox3.Text = GlobelVar.TestInfoDefGlo.TestInfo[5];
                textBox4.Text = GlobelVar.TestInfoDefGlo.TestInfo[6];
                textBox5.Text = GlobelVar.TestInfoDefGlo.TestInfo[7];
                textBox6.Text = GlobelVar.TestInfoDefGlo.TestInfo[8];
                textBox7.Text = GlobelVar.TestInfoDefGlo.TestInfo[9];
                //GlobelVar.TestInfoDefGlo.TestInfo[10] = "--";l



                #region 20151020添加读取制冷剂名称
                //制冷剂名称20151020
                GlobelVar.RefName = GlobelVar.TestInfoDefGlo.TestInfo[12];

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
                //锁定在下面
                #endregion 20151020添加各个信息


                #region 20151108添加读取压缩机离合器电压读取
                GlobelVar.CarCompressorClutchVoltage = GlobelVar.TestInfoDefGlo.TestInfo[13];
                if (GlobelVar.CarCompressorClutchVoltage == "24V")
                {
                    RB24V.IsChecked = true;
                }
                else
                {
                    RB12V.IsChecked = true;
                }

                #endregion
                #region 调取记录后锁定前三项20150921
                this.textBox1.IsEnabled = false;
                this.textBox2.IsEnabled = false;
                this.textBox3.IsEnabled = false;

                //新添加的两个框架的锁定
                this.RBR22.IsEnabled = false;
                this.RBR134a.IsEnabled = false;

                //新添加锁定
                this.RB12V.IsEnabled = false;
                this.RB24V.IsEnabled = false;
                #endregion 调取记录后锁定前三项20150921
            }
            else
            {
                return;
            }
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
            //CBR22.IsChecked = false;

            UtilityMod_Header.RefNistProp.RefInUsing = UtilityMod_Header.RefNistProp.R134a;
            //数据库存储信息
            GlobelVar.RefName = "R134a";
        }
        #endregion 20151020添加制冷剂选择CheckBox

        private void RB24V_Checked(object sender, RoutedEventArgs e)
        {
            BackPanel.InformationGlo.CompressorClutchVoltage = 24;
        }

        private void RB12V_Checked(object sender, RoutedEventArgs e)
        {
            BackPanel.InformationGlo.CompressorClutchVoltage = 12;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            timer.Stop();
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            if (GlobelVar.InfoChangeCar == 1)
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
        }


    }
}

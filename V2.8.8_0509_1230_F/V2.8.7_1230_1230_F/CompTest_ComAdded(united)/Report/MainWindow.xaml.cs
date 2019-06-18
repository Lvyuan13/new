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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace Report
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            ////这个早应该在项目工程中初始化！
            //DoubleListForReport_FromFrontPanel.IniData_ForCar();



            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:
                    //场景Car的
                    //StringListForCarReport.CarList[];
                    StringListForCarReport.IniStringList_ForCar();
                    if (Infomation.IsPreview)
                    {
                       //public static string RP25TestResult = "待定";
                        Report.ReportParameterMySelf.RP25TestResult = "待定";
                        Report.ReportParameterMySelf_ForChiller.RP23TestResult = "待定";
                        DBOperateForReport.GetLastInfoRecordFromDateBase(DBPath_ForReport.DBPath_ForReportChild);
                    }
                    else
                    {
                        //这个是通用的
                        StringListForCarReport.GetDoubleDataFromFrontPanel_ForCar(DoubleListForReport_FromFrontPanel.DoubleDataForReport);
                    }

                    break;
                case SenarioForReport.Senario_ForReport.CarNoise:
                    //场景Car的
                    //StringListForCarReport.CarList[];
                    StringListForCarReport.IniStringList_ForCar();
                    if (Infomation.IsPreview)
                    {
                        Report.ReportParameterMySelf.RP25TestResult = "待定";
                        Report.ReportParameterMySelf_ForChiller.RP23TestResult = "待定";
                        DBOperateForReport.GetLastInfoRecordFromDateBase(DBPath_ForReport.DBPath_ForReportChild);
                    }
                    else
                    {
                        //这个是通用的
                        StringListForCarReport.GetDoubleDataFromFrontPanel_ForCar(DoubleListForReport_FromFrontPanel.DoubleDataForReport);
                    }

                    break;
                case SenarioForReport.Senario_ForReport.ChillerNormialCondition: //名义工况.
                    StringListForCarReport.IniStringList_ForChiller();

                    ReportParameterMySelf_ForChiller.RP20PartialLoad = "";
                    ReportParameterMySelf_ForChiller.RP21PartialLoadName = "";

                    if (Infomation.IsPreview)
                    {
                        Report.ReportParameterMySelf.RP25TestResult = "待定";
                        Report.ReportParameterMySelf_ForChiller.RP23TestResult = "待定";
                        DBOperateForReport.GetLastInfoRecordFromDateBase(DBPath_ForReport.DBPath_ForReportChild);
                    }
                    else
                    {
                        StringListForCarReport.GetDoubleDataFromFrontPanel_ForChiller(DoubleListForReport_FromFrontPanel.DoubleDataForReport);
                    }

                    break;
                case SenarioForReport.Senario_ForReport.ChillerPartialCondition: //部分工况
                    StringListForCarReport.IniStringList_ForChiller();
                    if (Infomation.IsPreview)
                    {
                        Report.ReportParameterMySelf.RP25TestResult = "待定";
                        Report.ReportParameterMySelf_ForChiller.RP23TestResult = "待定";
                        DBOperateForReport.GetLastInfoRecordFromDateBase(DBPath_ForReport.DBPath_ForReportChild);
                    }
                    else
                    {
                        StringListForCarReport.GetDoubleDataFromFrontPanel_ForChiller(DoubleListForReport_FromFrontPanel.DoubleDataForReport);
                    }

                    break;
                case SenarioForReport.Senario_ForReport.ChillerChangCondition: //变工况
                    StringListForCarReport.IniStringList_ForChiller();

                    ReportParameterMySelf_ForChiller.RP20PartialLoad = "";
                    ReportParameterMySelf_ForChiller.RP21PartialLoadName = "";

                    if (Infomation.IsPreview)
                    {
                        Report.ReportParameterMySelf.RP25TestResult = "待定";
                        Report.ReportParameterMySelf_ForChiller.RP23TestResult = "待定";
                        DBOperateForReport.GetLastInfoRecordFromDateBase(DBPath_ForReport.DBPath_ForReportChild);
                    }
                    else
                    {
                        StringListForCarReport.GetDoubleDataFromFrontPanel_ForChiller(DoubleListForReport_FromFrontPanel.DoubleDataForReport);
                    }

                    break;
                case SenarioForReport.Senario_ForReport.ChillerMaxCondition: //最大工况
                    StringListForCarReport.IniStringList_ForChiller();
                    ReportParameterMySelf_ForChiller.RP20PartialLoad = "";
                    ReportParameterMySelf_ForChiller.RP21PartialLoadName = "";


                    if (Infomation.IsPreview)
                    {
                        Report.ReportParameterMySelf.RP25TestResult = "待定";
                        Report.ReportParameterMySelf_ForChiller.RP23TestResult = "待定";
                        DBOperateForReport.GetLastInfoRecordFromDateBase(DBPath_ForReport.DBPath_ForReportChild);
                    }
                    else
                    {
                        StringListForCarReport.GetDoubleDataFromFrontPanel_ForChiller(DoubleListForReport_FromFrontPanel.DoubleDataForReport);
                    }

                    break;


            }


            BuildReport(1);


        }

        public void BuildReport(int projectCode)
        {
            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:
                    this.reportView.LocalReport.ReportEmbeddedResource = "Report.ReportCarCooling.rdlc";
                    break;
                case SenarioForReport.Senario_ForReport.CarNoise:
                    this.reportView.LocalReport.ReportEmbeddedResource = "Report.ReportCarNoise.rdlc";
                    break;

                case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                    this.reportView.LocalReport.ReportEmbeddedResource = "Report.ReportChiller.rdlc";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerPartialCondition:
                    this.reportView.LocalReport.ReportEmbeddedResource = "Report.ReportChiller.rdlc";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerChangCondition:
                    this.reportView.LocalReport.ReportEmbeddedResource = "Report.ReportChiller.rdlc";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                    this.reportView.LocalReport.ReportEmbeddedResource = "Report.ReportChiller.rdlc";
                    break;

            }
            //this.reportView.LocalReport.ReportEmbeddedResource = "Report.ReportCarCooling.rdlc";

            #region 添加参数20151218

            List<ReportParameter> ReportPara = new List<ReportParameter>();

            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:

                case SenarioForReport.Senario_ForReport.CarNoise:
                    ReportPara = SetParameterForReport_ForCar();
                    break;

                case SenarioForReport.Senario_ForReport.ChillerNormialCondition:

                case SenarioForReport.Senario_ForReport.ChillerPartialCondition:

                case SenarioForReport.Senario_ForReport.ChillerChangCondition:

                case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                    ReportPara = SetParameterForReport_ForChiller();
                    break;
            }

            this.reportView.LocalReport.SetParameters(ReportPara);
            #endregion 添加参数20151218

            ////Reports\WcutReport.rdlc
            #region 数据源
            //Dictionary<int, double?[]> channels = AvgListTable.Load(App.WorkDB, "AvgList", projectCode);

            //#region 试验数据列表
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Num", typeof(int)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("Avg1", typeof(string)));
            dt.Columns.Add(new DataColumn("Avg2", typeof(string)));
            dt.Columns.Add(new DataColumn("Avg3", typeof(string)));
            dt.Columns.Add(new DataColumn("Avg4", typeof(string)));
            dt.Columns.Add(new DataColumn("Avg", typeof(string)));


            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:

                case SenarioForReport.Senario_ForReport.CarNoise:
                    for (int i = 0; i < 39; i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Name"] = StringListForCarReport.CarStringList[i][0];
                        //dr["Unit"] =ListForReport.CarCoolingList[i][];
                        dr["Avg1"] = StringListForCarReport.CarStringList[i][1];
                        dr["Avg2"] = StringListForCarReport.CarStringList[i][2];
                        dr["Avg3"] = StringListForCarReport.CarStringList[i][3];
                        dr["Avg4"] = StringListForCarReport.CarStringList[i][4];
                        dr["Avg"] = StringListForCarReport.CarStringList[i][5];
                        dr["Num"] = StringListForCarReport.CarStringList[i][6];

                        dt.Rows.Add(dr);
                    }
                    break;

                case SenarioForReport.Senario_ForReport.ChillerNormialCondition:

                case SenarioForReport.Senario_ForReport.ChillerPartialCondition:

                case SenarioForReport.Senario_ForReport.ChillerChangCondition:

                case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                    for (int i = 0; i < 33; i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Name"] = StringListForCarReport.ChillerStringList[i][0];
                        //dr["Unit"] =ListForReport.CarCoolingList[i][];
                        dr["Avg1"] = StringListForCarReport.ChillerStringList[i][1];
                        dr["Avg2"] = StringListForCarReport.ChillerStringList[i][2];
                        dr["Avg3"] = StringListForCarReport.ChillerStringList[i][3];
                        dr["Avg4"] = StringListForCarReport.ChillerStringList[i][4];
                        dr["Avg"] = StringListForCarReport.ChillerStringList[i][5];
                        dr["Num"] = StringListForCarReport.ChillerStringList[i][6];

                        dt.Rows.Add(dr);
                    }
                    break;
            }

            #endregion 数据源

            this.reportView.LocalReport.DataSources.Clear();

            //数据源
            ReportDataSource rds = new ReportDataSource("DataSetRS", dt);
            this.reportView.LocalReport.DataSources.Add(rds);



            //this.reportViewer.LocalReport.DisplayName = App.FileName;
            this.reportView.RefreshReport();

            //this.reportView.Refresh(); 这个不可以按钮更新，上面的哪个可以按钮更新！
            this.reportView.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportView.ZoomMode = ZoomMode.PageWidth;

        }


        public static List<ReportParameter> SetParameterForReport_ForCar()
        {
            List<ReportParameter> reportPars = new List<ReportParameter>() 
            {
                //new ReportParameter("RP1",""),
                new ReportParameter("RP1CarExpType",ReportParameterMySelf.RP1CarExpType),
                new ReportParameter("RP2Manufacturer",ReportParameterMySelf.RP2Manufacturer),
                new ReportParameter("RP3ModelIdentity",ReportParameterMySelf.RP3ModelIdentity),
                new ReportParameter("RP4OutNumb",ReportParameterMySelf.RP4OutNumb),
                new ReportParameter("RP5Refrig",ReportParameterMySelf.RP5Refrig),
                new ReportParameter("RP6NormalCoolingCapacity",ReportParameterMySelf.RP6NormalCoolingCapacity),//样品编号
                new ReportParameter("RP7NormalPower",ReportParameterMySelf.RP7NormalPower),//额定电压
                new ReportParameter("RP8RollDiameter",ReportParameterMySelf.RP8RollDiameter),//额定冷量
                new ReportParameter("RP9ClutchVoltage",ReportParameterMySelf.RP9ClutchVoltage), //额定电压
                new ReportParameter("RP10DischargeSatTemp",ReportParameterMySelf.RP10DischargeSatTemp),//
                new ReportParameter("RP11DischargePres",ReportParameterMySelf.RP11DischargePres),//冷凝器污垢面积
                new ReportParameter("RP12SuctionTemp",ReportParameterMySelf.RP12SuctionTemp),//额定频率
                new ReportParameter("RP13SuctionSatTemp",ReportParameterMySelf.RP13SuctionSatTemp),//额定热量
                new ReportParameter("RP14SuctionPres",ReportParameterMySelf.RP14SuctionPres), 
                new ReportParameter("RP15CompRotate",ReportParameterMySelf.RP15CompRotate),
                new ReportParameter("RP16DischargeSatLiqEnthalpy",ReportParameterMySelf.RP16DischargeSatLiqEnthalpy),//冷凝器污垢系数
                new ReportParameter("RP17SuctionEnthalpy",ReportParameterMySelf.RP17SuctionEnthalpy),
                new ReportParameter("RP18SuctionSpecVolum", ReportParameterMySelf.RP18SuctionSpecVolum),//冷凝压力
                new ReportParameter("RP19CompTemp",ReportParameterMySelf.RP19CompTemp),//蒸发压力
                new ReportParameter("RP20IniTorque",ReportParameterMySelf.RP20IniTorque),//电加热功率
                new ReportParameter("RP21CoolingWaterTemp",ReportParameterMySelf.RP21CoolingWaterTemp),//蒸发器污垢面积

                //if(App.Infos.Refrigerant=="R134A")
                new ReportParameter("RP22TestMan",ReportParameterMySelf.RP22TestMan),//制冷剂

                new ReportParameter("RP23TestDate",ReportParameterMySelf.RP23TestDate),//冷凝器表面积
                new ReportParameter("RP24SendCompany",ReportParameterMySelf.RP24SendCompany),//蒸发器表面积
                new ReportParameter("RP25TestResult",ReportParameterMySelf.RP25TestResult),//蒸发器表面积
                new ReportParameter("RP26BaseUnderNoise",ReportParameterMySelf.RP26BaseUnderNoise),//蒸发器表面积

                //new ReportParameter("RP26",Repo),//蒸发器污垢系数
                //new ReportParameter("RP27",App.Cdts[projectCode].ColdWaterTemp.ToString("f2")+" ℃"),//冷水温度
                //new ReportParameter("RP28",App.Cdts[projectCode].ColdWaterFlow.ToString("f2")+" m^3/h"),//冷水流量
                //new ReportParameter("RP29",App.Cdts[projectCode].HotWaterTemp.ToString("f2")+" ℃"),//热水温度
                //new ReportParameter("RP30",App.Cdts[projectCode].HotWaterFlow.ToString("f2")+" m^3/h"),//热水流量
                //new ReportParameter("RP31",""),//
                //new ReportParameter("RP32",""),//
                //new ReportParameter("RP33",""),
                //new ReportParameter("RP34",""),
                //new ReportParameter("RP35",""),
                //new ReportParameter("RP36",""),
                //new ReportParameter("RP37",""),
                //new ReportParameter("RP38",""),
                //new ReportParameter("RP39",""),
                //new ReportParameter("RP40","")
            };
            return reportPars;

            //this.reportView.LocalReport.SetParameters(reportPars);
        }

        public static List<ReportParameter> SetParameterForReport_ForChiller()
        {
            List<ReportParameter> reportPars = new List<ReportParameter>() 
            {
                //new ReportParameter("RP1",""),
                new ReportParameter("RP1ChillerExpType",ReportParameterMySelf_ForChiller.RP1ChillerExpType),
                new ReportParameter("RP2Manufacturer",ReportParameterMySelf_ForChiller.RP2Manufacturer),
                new ReportParameter("RP3ModelIdentity",ReportParameterMySelf_ForChiller.RP3ModelIdentity),
                new ReportParameter("RP4OutNumb",ReportParameterMySelf_ForChiller.RP4OutNumb),
                new ReportParameter("RP5Refrig",ReportParameterMySelf_ForChiller.RP5Refrig),
                new ReportParameter("RP6NormalCoolingCapacity",ReportParameterMySelf_ForChiller.RP6NormalCoolingCapacity),//样品编号
                new ReportParameter("RP7NormalPower",ReportParameterMySelf_ForChiller.RP7NormalPower),//额定电压
                new ReportParameter("RP8NormalWaterFlow",ReportParameterMySelf_ForChiller.RP8NormalWaterFlow),//额定冷量
                new ReportParameter("RP9ControlVar",ReportParameterMySelf_ForChiller.RP9ControlVar), //额定电压
                new ReportParameter("RP11Voltage",ReportParameterMySelf_ForChiller.RP11Voltage),//
                new ReportParameter("RP12InWaterTemperature",ReportParameterMySelf_ForChiller.RP12InWaterTemperature),//冷凝器污垢面积
                new ReportParameter("RP13ControlVarName",ReportParameterMySelf_ForChiller.RP13ControlVarName),//额定频率
                new ReportParameter("RP14ControlVarValue",ReportParameterMySelf_ForChiller.RP14ControlVarValue),//额定热量
                new ReportParameter("RP15EvaporatorTemp",ReportParameterMySelf_ForChiller.RP15EvaporatorTemp), 
                new ReportParameter("RP16EvaporatorPres",ReportParameterMySelf_ForChiller.RP16EvaporatorPres),
                new ReportParameter("RP17SuctionTemp",ReportParameterMySelf_ForChiller.RP17SuctionTemp),//冷凝器污垢系数
                new ReportParameter("RP18SuctionSpecVolum",ReportParameterMySelf_ForChiller.RP18SuctionSpecVolum),
                new ReportParameter("RP19SuctionSpecEnthalpy", ReportParameterMySelf_ForChiller.RP19SuctionSpecEnthalpy),//冷凝压力

                new ReportParameter("RP20PartialLoad",ReportParameterMySelf_ForChiller.RP20PartialLoad),//蒸发压力
                new ReportParameter("RP21PartialLoadName",ReportParameterMySelf_ForChiller.RP21PartialLoadName),//蒸发压力


                new ReportParameter("RP23TestResult",ReportParameterMySelf_ForChiller.RP23TestResult),//电加热功率
                new ReportParameter("RP24TestMan",ReportParameterMySelf_ForChiller.RP24TestMan),//蒸发器污垢面积
                //if(App.Infos.Refrigerant=="R134A")
                new ReportParameter("RP25TestDate",ReportParameterMySelf_ForChiller.RP25TestDate),//制冷剂


            };
            return reportPars;

            //this.reportView.LocalReport.SetParameters(reportPars);
        }

        private void ExitExperiement_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult mbr = MessageBox.Show("是否需要保存报表数据？", "保存数据", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
               
                IsQualifiedForReport isqualifiedforreport = new IsQualifiedForReport();
                isqualifiedforreport.ShowDialog();

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

            }
            else
            {

                this.Close();

            }
            //StringListForCarReport.CarStringList;
            //StringListForCarReport.ChillerStringList;

            //MessageBox.Show();

            //switch()
            //{}

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Infomation.IsPreview = false;

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

            ReportParameterMySelf_ForChiller.RP20PartialLoad = "--";

            ReportParameterMySelf_ForChiller.RP21PartialLoadName = "部分负荷率 %:";
        }
    }
}

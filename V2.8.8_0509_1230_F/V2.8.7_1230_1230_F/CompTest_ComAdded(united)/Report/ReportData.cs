using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Report
{
    public class ReportData
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 第一阶段平均值
        /// </summary>
        public string Avg1 { get; set; }
        /// <summary>
        /// 第二阶段平均值
        /// </summary>
        public string Avg2 { get; set; }
        //public double Avg2 { get; set; }
        /// <summary>
        /// 第三阶段平均值
        /// </summary>
        public string Avg3 { get; set; }
        //public double Avg3 { get; set; }
        /// <summary>
        /// 第四阶段平均值
        /// </summary>
        public string Avg4 { get; set; }

        /// <summary>
        /// 总平均值
        /// </summary>
        public string Avg { get; set; }
    }


    public static class StringListForCarReport
    {
        /// <summary>
        /// CarStinglist
        /// </summary>
        public static List<string[]> CarStringList = new List<string[]>();

        public static List<string[]> ChillerStringList = new List<string[]>();

        public static void IniStringList_ForCar()
        {
            CarStringList.Clear();
            if (CarStringList.Count == 0)
            {
                CarStringList.Add(new string[] { "扭矩 Nm", "--", "--", "--", "--", "--", "0" });
                //CarStringList.Add(new string[] { "皮重 Nm", "--", "--", "--", "--", "--", "1" });
                CarStringList.Add(new string[] { "转速 rpm", "--", "--", "--", "--", "--", "1" });

                CarStringList.Add(new string[] { "吸气压力 MPa ", "--", "--", "--", "--", "--", "2" });
                CarStringList.Add(new string[] { "吸气温度 ℃", "--", "--", "--", "--", "--", "3" });
                CarStringList.Add(new string[] { "吸气实际比体积 m3/kg", "--", "--", "--", "--", "--", "4" });
                CarStringList.Add(new string[] { "排气压力 MPa", "--", "--", "--", "--", "--", "5" });
                CarStringList.Add(new string[] { "排气温度 ℃", "--", "--", "--", "--", "--", "6" });
                CarStringList.Add(new string[] { "冷凝器进口压力 MPa", "--", "--", "--", "--", "--", "7" });
                CarStringList.Add(new string[] { "冷凝器进口温度 ℃", "--", "--", "--", "--", "--", "8" });
                CarStringList.Add(new string[] { "冷凝器进口比焓 kJ/kg", "--", "--", "--", "--", "--", "9" });
                CarStringList.Add(new string[] { "冷凝器出口压力 MPa", "--", "--", "--", "--", "--", "10" });
                CarStringList.Add(new string[] { "冷凝器出口温度 ℃", "--", "--", "--", "--", "--", "11" });
                CarStringList.Add(new string[] { "冷凝器出口比焓 kJ/kg", "--", "--", "--", "--", "--", "12" });
                CarStringList.Add(new string[] { "膨胀阀进口压力 MPa", "--", "--", "--", "--", "--", "13" });
                CarStringList.Add(new string[] { "膨胀阀进口温度 ℃", "--", "--", "--", "--", "--", "14" });
                CarStringList.Add(new string[] { "膨胀阀进口比焓 kJ/kg", "--", "--", "--", "--", "--", "15" });
                CarStringList.Add(new string[] { "第二制冷剂压力 MPa", "--", "--", "--", "--", "--", "16" });

                CarStringList.Add(new string[] { "第二制冷剂温度 ℃", "--", "--", "--", "--", "--", "17" });
                CarStringList.Add(new string[] { "量热器外环境温度 ℃", "--", "--", "--", "--", "--", "18" });

                CarStringList.Add(new string[] { "量热器出口压力 MPa", "--", "--", "--", "--", "--", "19" });
                CarStringList.Add(new string[] { "量热器出口温度 ℃", "--", "--", "--", "--", "--", "20" });
                //string[] ForAdd = new string[5] { "扭矩", "--", "--", "--", "--" };
                //ForAdd={"扭矩";"--"; "--"; "--"};
                CarStringList.Add(new string[] { "量热器出口比焓 kJ/kg", "--", "--", "--", "--", "--", "21" });
                CarStringList.Add(new string[] { "冷却水进水温度 ℃", "--", "--", "--", "--", "--", "22" });
                CarStringList.Add(new string[] { "冷却水出水温度 ℃", "--", "--", "--", "--", "--", "23" });
                CarStringList.Add(new string[] { "水流量 m3/h ", "--", "--", "--", "--", "--", "24" });


                CarStringList.Add(new string[] { "水比热容 kJ/(kg ℃)", "--", "--", "--", "--", "--", "25" });
                CarStringList.Add(new string[] { "水密度 kg/m3", "--", "--", "--", "--", "--", "26" });
                CarStringList.Add(new string[] { "压缩机环境温度 ℃", "--", "--", "--", "--", "--", "27" });

                //CarStringList.Add(new string[] { "环境温度 ℃", "--", "--", "--", "--", "--", "28" });
                //CarStringList.Add(new string[] { "压缩机环境温度 ℃", "--", "--", "--", "--", "--", "28" });
                CarStringList.Add(new string[] { "量热器输入功率 kW", "--", "--", "--", "--", "--", "28" });
                CarStringList.Add(new string[] { "量热器漏热量 kW", "--", "--", "--", "--", "--", "29" });
                CarStringList.Add(new string[] { "冷凝器换热量 kW", "--", "--", "--", "--", "--", "30" });
                CarStringList.Add(new string[] { "冷凝器漏热量 kW", "--", "--", "--", "--", "--", "31" });
                CarStringList.Add(new string[] { "A方法制冷剂流量 kg/s", "--", "--", "--", "--", "--", "32" });
                CarStringList.Add(new string[] { "A方法制冷量 kW", "--", "--", "--", "--", "--", "33" });
                CarStringList.Add(new string[] { "G方法制冷剂流量 kg/s", "--", "--", "--", "--", "--", "34" });
                CarStringList.Add(new string[] { "G方法制冷量 kW", "--", "--", "--", "--", "--", "35" });
                CarStringList.Add(new string[] { "压缩机功率功率 kW", "--", "--", "--", "--", "--", "36" });
                CarStringList.Add(new string[] { "AG方法制冷量偏差 %", "--", "--", "--", "--", "--", "37" });
                CarStringList.Add(new string[] { "COP", "--", "--", "--", "--", "--", "38" });
                //GlobelVar.Channel_ForCar.Add(new string[] { "量热器出口温度 ℃", "--", "--", "--", "--", "20" });
            }

        }

        public static void IniStringList_ForChiller()
        {
            ChillerStringList.Clear();
            if (ChillerStringList.Count == 0)
            {
                ChillerStringList.Add(new string[] { "电压UV V", "--", "--", "--", "--", "--", "0" });
                //CarStringList.Add(new string[] { "皮重 Nm", "--", "--", "--", "--", "--", "1" });
                ChillerStringList.Add(new string[] { "电压VW V", "--", "--", "--", "--", "--", "1" });

                ChillerStringList.Add(new string[] { "电压WU V", "--", "--", "--", "--", "--", "2" });
                ChillerStringList.Add(new string[] { "电流U A", "--", "--", "--", "--", "--", "3" });
                ChillerStringList.Add(new string[] { "电流V A", "--", "--", "--", "--", "--", "4" });
                ChillerStringList.Add(new string[] { "电流W A", "--", "--", "--", "--", "--", "5" });

                ChillerStringList.Add(new string[] { "频率 Hz", "--", "--", "--", "--", "--", "6" });
                ChillerStringList.Add(new string[] { "输入功率 kW", "--", "--", "--", "--", "--", "7" });
                ChillerStringList.Add(new string[] { "吸气压力 MPa", "--", "--", "--", "--", "--", "8" });
                ChillerStringList.Add(new string[] { "吸气温度 ℃", "--", "--", "--", "--", "--", "9" });
                ChillerStringList.Add(new string[] { "吸气实际比体积 m3/kg", "--", "--", "--", "--", "--", "10" });
                ChillerStringList.Add(new string[] { "供液压力 MPa", "--", "--", "--", "--", "--", "11" });
                ChillerStringList.Add(new string[] { "供液温度 ℃", "--", "--", "--", "--", "--", "12" });

                ChillerStringList.Add(new string[] { "供液比焓 kJ/kg", "--", "--", "--", "--", "--", "13" });
                ChillerStringList.Add(new string[] { "膨胀阀进口压力 MPa", "--", "--", "--", "--", "--", "14" });
                ChillerStringList.Add(new string[] { "膨胀阀进口温度 ℃", "--", "--", "--", "--", "--", "15" });
                ChillerStringList.Add(new string[] { "膨胀阀进口比焓 kJ/kg", "--", "--", "--", "--", "--", "16" });

                ChillerStringList.Add(new string[] { "第二制冷剂压力 MPa", "--", "--", "--", "--", "--", "17" });
                ChillerStringList.Add(new string[] { "第二制冷剂温度 ℃", "--", "--", "--", "--", "--", "18" });

                ChillerStringList.Add(new string[] { "量热器外环境温度 ℃", "--", "--", "--", "--", "--", "19" });
                ChillerStringList.Add(new string[] { "量热器出口压力 MPa", "--", "--", "--", "--", "--", "20" });
                //string[] ForAdd = new string[5] { "扭矩", "--", "--", "--", "--" };
                //ForAdd={"扭矩";"--"; "--"; "--"};
                ChillerStringList.Add(new string[] { "量热器出口温度 ℃", "--", "--", "--", "--", "--", "21" });
                ChillerStringList.Add(new string[] { "量热器出口比焓 kJ/kg", "--", "--", "--", "--", "--", "22" });
                ChillerStringList.Add(new string[] { "冷却水进水温度 ℃", "--", "--", "--", "--", "--", "23" });
                ChillerStringList.Add(new string[] { "冷却水出水温度 ℃", "--", "--", "--", "--", "--", "24" });
                ChillerStringList.Add(new string[] { "水流量 m3/h", "--", "--", "--", "--", "--", "25" });
                ChillerStringList.Add(new string[] { "水比热容 kJ/(kg ℃)", "--", "--", "--", "--", "--", "26" });
                ChillerStringList.Add(new string[] { "水密度 kg/m3", "--", "--", "--", "--", "--", "27" });

                //ChillerStringList.Add(new string[] { "环境温度 ℃", "--", "--", "--", "--", "--", "28" });
                //ChillerStringList.Add(new string[] { "压缩机环境温度 ℃", "--", "--", "--", "--", "--", "28" });
                ChillerStringList.Add(new string[] { "量热器输入功率 kW", "--", "--", "--", "--", "--", "28" });
                ChillerStringList.Add(new string[] { "量热器漏热量 kW", "--", "--", "--", "--", "--", "29" });

                ChillerStringList.Add(new string[] { "制冷剂流量 kg/s", "--", "--", "--", "--", "--", "30" });
                ChillerStringList.Add(new string[] { "制冷量 kW", "--", "--", "--", "--", "--", "31" });
                ChillerStringList.Add(new string[] { "COP", "--", "--", "--", "--", "--", "32" });

            }

        }

        /// <summary>
        /// 把前台的Doublelist转换储存到报表用的stringlist，报表调用直接用stringlist
        /// 注意：这个，虽然写的是CarCooling，但是都是通用的！20151218:只对Car而言
        /// </summary>
        /// <param name="CarCoolingListFromBp"></param>
        public static void GetDoubleDataFromFrontPanel_ForCar(List<double[]> CarCoolingDoublelist)
        {


            for (int i = 0; i < 5; i++)
            {
                StringListForCarReport.CarStringList[0][i + 1] = CarCoolingDoublelist[0][i].ToString("f2");
                StringListForCarReport.CarStringList[1][i + 1] = CarCoolingDoublelist[1][i].ToString("f0");
                StringListForCarReport.CarStringList[2][i + 1] = CarCoolingDoublelist[2][i].ToString("f3");
                StringListForCarReport.CarStringList[3][i + 1] = CarCoolingDoublelist[3][i].ToString("f2");
                StringListForCarReport.CarStringList[4][i + 1] = CarCoolingDoublelist[4][i].ToString("f6");
                StringListForCarReport.CarStringList[5][i + 1] = CarCoolingDoublelist[5][i].ToString("f3");
                StringListForCarReport.CarStringList[6][i + 1] = CarCoolingDoublelist[6][i].ToString("f2");
                StringListForCarReport.CarStringList[7][i + 1] = CarCoolingDoublelist[7][i].ToString("f3");
                StringListForCarReport.CarStringList[8][i + 1] = CarCoolingDoublelist[8][i].ToString("f2");
                StringListForCarReport.CarStringList[9][i + 1] = CarCoolingDoublelist[9][i].ToString("f2");
                StringListForCarReport.CarStringList[10][i + 1] = CarCoolingDoublelist[10][i].ToString("f3");
                StringListForCarReport.CarStringList[11][i + 1] = CarCoolingDoublelist[11][i].ToString("f2");
                StringListForCarReport.CarStringList[12][i + 1] = CarCoolingDoublelist[12][i].ToString("f2");
                StringListForCarReport.CarStringList[13][i + 1] = CarCoolingDoublelist[13][i].ToString("f3");
                StringListForCarReport.CarStringList[14][i + 1] = CarCoolingDoublelist[14][i].ToString("f2");
                StringListForCarReport.CarStringList[15][i + 1] = CarCoolingDoublelist[15][i].ToString("f2");
                StringListForCarReport.CarStringList[16][i + 1] = CarCoolingDoublelist[16][i].ToString("f3");
                StringListForCarReport.CarStringList[17][i + 1] = CarCoolingDoublelist[17][i].ToString("f2");
                StringListForCarReport.CarStringList[18][i + 1] = CarCoolingDoublelist[18][i].ToString("f2");
                StringListForCarReport.CarStringList[19][i + 1] = CarCoolingDoublelist[19][i].ToString("f3");
                StringListForCarReport.CarStringList[20][i + 1] = CarCoolingDoublelist[20][i].ToString("f2");
                StringListForCarReport.CarStringList[21][i + 1] = CarCoolingDoublelist[21][i].ToString("f2");
                StringListForCarReport.CarStringList[22][i + 1] = CarCoolingDoublelist[22][i].ToString("f2");
                StringListForCarReport.CarStringList[23][i + 1] = CarCoolingDoublelist[23][i].ToString("f2");
                StringListForCarReport.CarStringList[24][i + 1] = CarCoolingDoublelist[24][i].ToString("f2");
                StringListForCarReport.CarStringList[25][i + 1] = CarCoolingDoublelist[25][i].ToString("f3");
                StringListForCarReport.CarStringList[26][i + 1] = CarCoolingDoublelist[26][i].ToString("f3");
                StringListForCarReport.CarStringList[27][i + 1] = CarCoolingDoublelist[27][i].ToString("f2");
                StringListForCarReport.CarStringList[28][i + 1] = CarCoolingDoublelist[28][i].ToString("f3");
                StringListForCarReport.CarStringList[29][i + 1] = CarCoolingDoublelist[29][i].ToString("f3");
                StringListForCarReport.CarStringList[30][i + 1] = CarCoolingDoublelist[30][i].ToString("f3");
                StringListForCarReport.CarStringList[31][i + 1] = CarCoolingDoublelist[31][i].ToString("f3");
                StringListForCarReport.CarStringList[32][i + 1] = CarCoolingDoublelist[32][i].ToString("f3");
                StringListForCarReport.CarStringList[33][i + 1] = CarCoolingDoublelist[33][i].ToString("f3");
                StringListForCarReport.CarStringList[34][i + 1] = CarCoolingDoublelist[34][i].ToString("f3");
                StringListForCarReport.CarStringList[35][i + 1] = CarCoolingDoublelist[35][i].ToString("f3");
                StringListForCarReport.CarStringList[36][i + 1] = CarCoolingDoublelist[36][i].ToString("f3");
                StringListForCarReport.CarStringList[37][i + 1] = CarCoolingDoublelist[37][i].ToString("f1");
                StringListForCarReport.CarStringList[38][i + 1] = CarCoolingDoublelist[38][i].ToString("f2");
                //StringListForCarReport.CarStringList[39][i + 1] = CarCoolingDoublelist[39][i].ToString("2");
            }

            for (int i = 0; i < StringListForCarReport.CarStringList.Count; i++)
            {

                //StringListForCarReport.CarStringList[i][1] = CarCoolingDoublelist[i][0].ToString();
                if (CarCoolingDoublelist[i][0] == 8888)
                {
                    StringListForCarReport.CarStringList[i][1] = "--";
                }

                //StringListForCarReport.CarStringList[i][2] = CarCoolingDoublelist[i][1].ToString();
                if (CarCoolingDoublelist[i][1] == 8888)
                {
                    StringListForCarReport.CarStringList[i][2] = "--";
                }

                //StringListForCarReport.CarStringList[i][3] = CarCoolingDoublelist[i][2].ToString();
                if (CarCoolingDoublelist[i][2] == 8888)
                {
                    StringListForCarReport.CarStringList[i][3] = "--";
                }

                //StringListForCarReport.CarStringList[i][4] = CarCoolingDoublelist[i][3].ToString();
                if (CarCoolingDoublelist[i][3] == 8888)
                {
                    StringListForCarReport.CarStringList[i][4] = "--";
                }

                //StringListForCarReport.CarStringList[i][5] = CarCoolingDoublelist[i][4].ToString();
                if (CarCoolingDoublelist[i][4] == 8888)
                {
                    StringListForCarReport.CarStringList[i][5] = "--";
                }

            }

        }

        public static void GetDoubleDataFromFrontPanel_ForChiller(List<double[]> ChillerDoublelist)
        {


            for (int i = 0; i < 5; i++)
            {
                StringListForCarReport.ChillerStringList[0][i + 1] = ChillerDoublelist[0][i].ToString("f2");
                StringListForCarReport.ChillerStringList[1][i + 1] = ChillerDoublelist[1][i].ToString("f2");
                StringListForCarReport.ChillerStringList[2][i + 1] = ChillerDoublelist[2][i].ToString("f2");
                StringListForCarReport.ChillerStringList[3][i + 1] = ChillerDoublelist[3][i].ToString("f2");
                StringListForCarReport.ChillerStringList[4][i + 1] = ChillerDoublelist[4][i].ToString("f2");
                StringListForCarReport.ChillerStringList[5][i + 1] = ChillerDoublelist[5][i].ToString("f2");
                StringListForCarReport.ChillerStringList[6][i + 1] = ChillerDoublelist[6][i].ToString("f2");
                StringListForCarReport.ChillerStringList[7][i + 1] = ChillerDoublelist[7][i].ToString("f3");
                StringListForCarReport.ChillerStringList[8][i + 1] = ChillerDoublelist[8][i].ToString("f3");
                StringListForCarReport.ChillerStringList[9][i + 1] = ChillerDoublelist[9][i].ToString("f2");
                StringListForCarReport.ChillerStringList[10][i + 1] = ChillerDoublelist[10][i].ToString("f6");
                StringListForCarReport.ChillerStringList[11][i + 1] = ChillerDoublelist[11][i].ToString("f3");
                StringListForCarReport.ChillerStringList[12][i + 1] = ChillerDoublelist[12][i].ToString("f2");
                StringListForCarReport.ChillerStringList[13][i + 1] = ChillerDoublelist[13][i].ToString("f3");
                StringListForCarReport.ChillerStringList[14][i + 1] = ChillerDoublelist[14][i].ToString("f3");
                StringListForCarReport.ChillerStringList[15][i + 1] = ChillerDoublelist[15][i].ToString("f2");
                StringListForCarReport.ChillerStringList[16][i + 1] = ChillerDoublelist[16][i].ToString("f3");
                StringListForCarReport.ChillerStringList[17][i + 1] = ChillerDoublelist[17][i].ToString("f3");
                StringListForCarReport.ChillerStringList[18][i + 1] = ChillerDoublelist[18][i].ToString("f2");
                StringListForCarReport.ChillerStringList[19][i + 1] = ChillerDoublelist[19][i].ToString("f2");
                StringListForCarReport.ChillerStringList[20][i + 1] = ChillerDoublelist[20][i].ToString("f3");
                StringListForCarReport.ChillerStringList[21][i + 1] = ChillerDoublelist[21][i].ToString("f2");
                StringListForCarReport.ChillerStringList[22][i + 1] = ChillerDoublelist[22][i].ToString("f3");
                StringListForCarReport.ChillerStringList[23][i + 1] = ChillerDoublelist[23][i].ToString("f2");
                StringListForCarReport.ChillerStringList[24][i + 1] = ChillerDoublelist[24][i].ToString("f2");
                StringListForCarReport.ChillerStringList[25][i + 1] = ChillerDoublelist[25][i].ToString("f2");
                StringListForCarReport.ChillerStringList[26][i + 1] = ChillerDoublelist[26][i].ToString("f3");
                StringListForCarReport.ChillerStringList[27][i + 1] = ChillerDoublelist[27][i].ToString("f3");
                StringListForCarReport.ChillerStringList[28][i + 1] = ChillerDoublelist[28][i].ToString("f3");
                StringListForCarReport.ChillerStringList[29][i + 1] = ChillerDoublelist[29][i].ToString("f3");
                StringListForCarReport.ChillerStringList[30][i + 1] = ChillerDoublelist[30][i].ToString("f3");
                StringListForCarReport.ChillerStringList[31][i + 1] = ChillerDoublelist[31][i].ToString("f3");
                StringListForCarReport.ChillerStringList[32][i + 1] = ChillerDoublelist[32][i].ToString("f2");
                //StringListForCarReport.CarStrin + 1] = CarCoolingDoublelist[39][i].ToString("2");
            }

            for (int i = 0; i < StringListForCarReport.ChillerStringList.Count; i++)
            {

                //StringListForCarReport.CarStringList[i][1] = CarCoolingDoublelist[i][0].ToString();
                if (ChillerDoublelist[i][0] == 8888)
                {
                    StringListForCarReport.ChillerStringList[i][1] = "--";
                }

                //StringListForCarReport.CarStringList[i][2] = CarCoolingDoublelist[i][1].ToString();
                if (ChillerDoublelist[i][1] == 8888)
                {
                    StringListForCarReport.ChillerStringList[i][2] = "--";
                }

                //StringListForCarReport.CarStringList[i][3] = ChillerDoublelist[i][2].ToString();
                if (ChillerDoublelist[i][2] == 8888)
                {
                    StringListForCarReport.ChillerStringList[i][3] = "--";
                }

                //StringListForCarReport.CarStringList[i][4] = CarCoolingDoublelist[i][3].ToString();
                if (ChillerDoublelist[i][3] == 8888)
                {
                    StringListForCarReport.ChillerStringList[i][4] = "--";
                }

                //StringListForCarReport.CarStringList[i][5] = CarCoolingDoublelist[i][4].ToString();
                if (ChillerDoublelist[i][4] == 8888)
                {
                    StringListForCarReport.ChillerStringList[i][5] = "--";
                }

            }

        }
    }

    /// <summary>
    /// 前台获取的数据储存到这里就可以了，报表项目，启动自动从这里调用！
    /// </summary>
    public static class DoubleListForReport_FromFrontPanel
    {
        /// <summary>
        /// 报表用数据：0，1，2，3是采集值；4是平均值20151217
        /// </summary>
        public static List<double[]> DoubleDataForReport = new List<double[]>(40);

        /// <summary>
        /// 用来求平均值20151217
        /// </summary>
        public static List<Stack<double>> StackDataForReport = new List<Stack<double>>(40);
        //static List<Queue<double>> DataForDataBP = new List<Queue<double>>
        public static void IniData_ForCar()
        {
            //为防止，从Chiller转到Car
            DoubleDataForReport.Clear();
            StackDataForReport.Clear();

            for (int i = 0; i < 39; i++)
            {
                DoubleDataForReport.Add(new double[5] { 8888, 8888, 8888, 8888, 8888 });
                StackDataForReport.Add(new Stack<double>());
            }
        }

        public static void IniData_ForChiller()
        {
            //为防止，从Chiller转到Car
            DoubleDataForReport.Clear();
            StackDataForReport.Clear();

            for (int i = 0; i < 33; i++)
            {
                DoubleDataForReport.Add(new double[5] { 8888, 8888, 8888, 8888, 8888 });
                StackDataForReport.Add(new Stack<double>());
            }
        }


        public static void AddData2(double[] Source, int InsertIndex)
        {

            if (InsertIndex < 4)
            {
                for (int i = 0; i < DoubleDataForReport.Count; i++)
                {
                    DoubleDataForReport[i][InsertIndex] = Source[i];
                }
                for (int i = 0; i < StackDataForReport.Count; i++)
                {
                    //DataForDataBP[i].Enqueue(Source[i]);
                    StackDataForReport[i].Push(Source[i]);
                    //DataForDataBP[i].Pop();
                    DoubleDataForReport[i][4] = StackDataForReport[i].Average();
                }




            }


        }


        public static void DeleteData3(int InsertIndex)
        {
            if (InsertIndex < 5 && InsertIndex >= 1)
            {
                for (int i = 0; i < DoubleDataForReport.Count; i++)
                {
                    DoubleDataForReport[i][InsertIndex - 1] = 8888;
                }
                for (int i = 0; i < StackDataForReport.Count; i++)
                {
                    StackDataForReport[i].Pop();
                    if (StackDataForReport[i].Count != 0)
                    {
                        DoubleDataForReport[i][4] = StackDataForReport[i].Average();
                    }
                    else
                    {
                        DoubleDataForReport[i][4] = 8888;
                    }

                }

            }
        }


    }


    public static class ReportParameterMySelf
    {
        public static string[] RPGroupForCar = new string[26];

        //Car的参数1-21
        public static string RP1CarExpType = "汽车空调压缩机制冷性能试验原始记录表";
        public static string RP2Manufacturer = "--";
        public static string RP3ModelIdentity = "--";
        public static string RP4OutNumb = "--";
        public static string RP5Refrig = "--";
        public static string RP6NormalCoolingCapacity = "--";
        public static string RP7NormalPower = "--";
        public static string RP8RollDiameter = "--";
        public static string RP9ClutchVoltage = "--";

        public static string RP10DischargeSatTemp = "63.00";
        public static string RP11DischargePres = "1.804";
        public static string RP12SuctionTemp = "9.00";
        public static string RP13SuctionSatTemp = "-1.00";
        public static string RP14SuctionPres = "0.282";
        public static string RP15CompRotate = "1800";
        public static string RP16DischargeSatLiqEnthalpy = "292.43";
        public static string RP17SuctionEnthalpy = "406.89"; //kJ/kg
        public static string RP18SuctionSpecVolum = "0.075418"; // m3/kg
        public static string RP19CompTemp = ">=65.00";
        public static string RP20IniTorque = "0";
        public static string RP21CoolingWaterTemp = "30.00";

        public static string RP22TestMan = "--";
        public static string RP23TestDate = "--";
        public static string RP24SendCompany = "--";

        public static string RP25TestResult = "待定";
        public static string RP26BaseUnderNoise = "30.0";


        //public static string RP27NoChange
    }

    public static class ReportParameterMySelf_ForChiller
    {
        public static string[] RPGroupForChiller = new string[23];

        //Chiller的参数1-21
        public static string RP1ChillerExpType = "冷水机组名义工况性能试验原始记录表";
        public static string RP2Manufacturer = "--";
        public static string RP3ModelIdentity = "--";
        public static string RP4OutNumb = "--";
        public static string RP5Refrig = "--";
        public static string RP6NormalCoolingCapacity = "--";
        public static string RP7NormalPower = "--";
        public static string RP8NormalWaterFlow = "--";
        public static string RP9ControlVar = "--";

        public static string RP11Voltage = "380";
        public static string RP12InWaterTemperature = "30.00";
        public static string RP13ControlVarName = "冷却水流量";
        public static string RP14ControlVarValue = "1.400";
        public static string RP15EvaporatorTemp = "7.00";
        public static string RP16EvaporatorPres = "0.375";

        public static string RP17SuctionTemp = "18.00";

        public static string RP18SuctionSpecVolum = "0.0576"; //
        public static string RP19SuctionSpecEnthalpy = "412.75"; //
        public static string RP20PartialLoad = "--";

        public static string RP21PartialLoadName = "部分负荷率 %:";

        public static string RP23TestResult = "待定";
        public static string RP24TestMan = "--";
        public static string RP25TestDate = "--";


        public static string RP31NoChange="供电电压 V";
        public static string RP32NoChange = "进水温度 ℃";
        public static string RP33NoChange = "蒸发温度 ℃";
        public static string RP34NoChange = "蒸发压力 ℃";
        public static string RP35NoChange = "吸气温度 ℃";
        public static string RP36NoChange="吸气比体积 m3/kg";
        public static string RP37NoChange="吸气气体比焓 kJ/kg";
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//using SHHS.FPTB.Base;
using System.Windows.Shapes;
using System.Windows.Data;
using System.IO;

namespace SHHS.UILabs.FitCurves
{
    /// <summary>
    /// FitCurves.xaml 的交互逻辑
    /// </summary>
    public partial class FitCurves : UserControl
    {
        List<Ellipse> y1Ponts = new List<Ellipse>();
        List<Ellipse> y2Ponts = new List<Ellipse>();
        List<Ellipse> y3Ponts = new List<Ellipse>();
        List<Ellipse> y4Ponts = new List<Ellipse>();
        double legendSize = 6;

        public FitCurves()
        {
            InitializeComponent();
            this.Resources = new ResourceDictionary() { Source = new Uri(@"/SHHS.UILabs;component/FitCurves/zh-CN.xaml", UriKind.Relative) };

            //InitAxis();
            gridBase.DataContext = this.AxisArray;
        }

        #region 字段
        /// <summary>
        /// 通道列表
        /// </summary>
        public Dictionary<int, Channel> Channels { get; set; }
        /// <summary>
        /// 阶段数据列表
        /// </summary>
        public ObservableCollection<double[]> Steps { get; set; }
        /// <summary>
        /// 阶段数据列表描述
        /// </summary>
        public ObservableCollection<string[]> StepsRemark { get; set; }
        /// <summary>
        /// 轴列表
        /// </summary>
        public Axis[] AxisArray
        {
            get { return axis; }
            set
            {
                axis = value;
                this.gridBase.DataContext = axis;
            }
        }
        private Axis[] axis;// = new Axis[] 
        //{
        //    new Axis(true, 8,"功率[W]", 400, 0, 10){ Foreground=Brushes.Green, Unit="W", LinestNumber=2},           //第1Y轴
        //    new Axis(true, 3,"静压[Pa]", 800, 0, 10){ Foreground=Brushes.Blue, Unit="Pa", LinestNumber=2},                //第2Y轴
        //    new Axis(true, 11,"静压效率[%]", 50, 0, 10){ Foreground=Brushes.Red, Unit="%", LinestNumber=2},                //第3Y轴
        //    new Axis(true, 5,"转速[rpm]", 6000, 0, 10){ Foreground=Brushes.Violet, Unit="rpm", LinestNumber=2},         //第4Y轴
        //    new Axis(true, 10,"标况风量[m^3/h]", 5000, 0, 10){ Foreground=Brushes.Black, Unit="m^3/h", LinestNumber=2},// X轴
        //};
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return tbk_Title.Text; }
            set { tbk_Title.Text = value; }
        }

        public string Culture
        {
            get { return _culture; }
            set
            {
                if (_culture != value)
                {
                    _culture = value;
                    if (_culture == "zh-CN")
                    {
                        this.Resources.Clear();
                        this.Resources = new ResourceDictionary() { Source = new Uri(@"/SHHS.UILabs;component/FitCurves/zh-CN.xaml", UriKind.Relative) };
                    }
                    else
                    {
                        this.Resources.Clear();
                        this.Resources = new ResourceDictionary() { Source = new Uri(@"/SHHS.UILabs;component/FitCurves/en-US.xaml", UriKind.Relative) };
                    }

                    foreach (var v in axis)
                    {
                        v.Name = Channels[v.ChannelNo].Name + "[" + this.Channels[v.ChannelNo].Unit + "]";
                    }
                }
            }
        }
        private string _culture = "zh-CN";

        private bool _isDisplayGraph = true;
        /// <summary>
        /// 是否显示曲线
        /// </summary>
        public bool IsDisplayGraph
        {
            get { return _isDisplayGraph; }
            set { _isDisplayGraph = value; }
        }
        private bool _isDisplayPoint = true;
        /// <summary>
        /// 是否显示数据点
        /// </summary>
        public bool IsDisplayPoint
        {
            get { return _isDisplayPoint; }
            set { _isDisplayPoint = value; }
        }

        private TipConverter TipConverter = new TipConverter();
        #endregion

        #region Method
        private void InitAxis()
        {
            string fitFile = AppDomain.CurrentDomain.BaseDirectory + "fit.ax";
            if (File.Exists(fitFile))
            {
                StreamReader readTestData = new StreamReader(fitFile);
                string str = readTestData.ReadLine();
                List<string> datas = new List<string>();
                Axis[] ax = new Axis[5];
                while (str != null)
                {
                    datas.Add(str);
                    str = readTestData.ReadLine();
                }
                readTestData.Close();

                //for (int i = 0; i < 5; i++)
                //{ 
                //    ax[i]=new Axis(bool.Parse(datas
                //}
                int i = 0;
                ax = new SHHS.UILabs.FitCurves.Axis[5]
                {
                    new Axis(bool.Parse(datas[i++]), int.Parse(datas[i++]),datas[i++], double.Parse(datas[i++]), double.Parse(datas[i++]), int.Parse(datas[i++]))
                    { Foreground=new SolidColorBrush((Color)ColorConverter.ConvertFromString(datas[i++])), Unit=datas[i++], LinestNumber=int.Parse(datas[i++])},           //第1Y轴
                    new Axis(bool.Parse(datas[i++]), int.Parse(datas[i++]),datas[i++], double.Parse(datas[i++]), double.Parse(datas[i++]), int.Parse(datas[i++]))
                    { Foreground=new SolidColorBrush((Color)ColorConverter.ConvertFromString(datas[i++])), Unit=datas[i++], LinestNumber=int.Parse(datas[i++])},                //第2Y轴
                    new Axis(bool.Parse(datas[i++]), int.Parse(datas[i++]),datas[i++], double.Parse(datas[i++]), double.Parse(datas[i++]), int.Parse(datas[i++]))
                    { Foreground=new SolidColorBrush((Color)ColorConverter.ConvertFromString(datas[i++])), Unit=datas[i++], LinestNumber=int.Parse(datas[i++])},                //第3Y轴
                    new Axis(bool.Parse(datas[i++]), int.Parse(datas[i++]),datas[i++], double.Parse(datas[i++]), double.Parse(datas[i++]), int.Parse(datas[i++]))
                    { Foreground=new SolidColorBrush((Color)ColorConverter.ConvertFromString(datas[i++])), Unit=datas[i++], LinestNumber=int.Parse(datas[i++])},         //第4Y轴
                    new Axis(bool.Parse(datas[i++]), int.Parse(datas[i++]),datas[i++], double.Parse(datas[i++]), double.Parse(datas[i++]), int.Parse(datas[i++]))
                    { Foreground=new SolidColorBrush((Color)ColorConverter.ConvertFromString(datas[i++])), Unit=datas[i++], LinestNumber=int.Parse(datas[i++])},// X轴
                };

                AxisArray = ax;
            }
            else
            {
                AxisArray = new SHHS.UILabs.FitCurves.Axis[]
                {
                    new Axis(true, 8,"功率[W]", 400, 0, 10){ Foreground=Brushes.Green, Unit="W", LinestNumber=3},           //第1Y轴
                    new Axis(true, 3,"静压[Pa]", 800, 0, 10){ Foreground=Brushes.Blue, Unit="Pa", LinestNumber=3},                //第2Y轴
                    new Axis(true, 11,"静压效率[%]", 50, 0, 10){ Foreground=Brushes.Red, Unit="%", LinestNumber=3},                //第3Y轴
                    new Axis(true, 5,"转速[rpm]", 6000, 0, 10){ Foreground=Brushes.Violet, Unit="rpm", LinestNumber=3},         //第4Y轴
                    new Axis(true, 10,"标况风量[m^3/h]", 5000, 0, 10){ Foreground=Brushes.Black, Unit="m^3/h", LinestNumber=3},// X轴
                };
            }
            

            //if (File.Exists(fitFile))
            //{
            //    StreamReader sr=File.

            //    //public static Axis[] AxisArray = new SHHS.UILabs.FitCurves.Axis[5]
            //    //{
            //    //    new Axis(true, 8,"功率[W]", 400, 0, 10){ Foreground=Brushes.Green, Unit="W", LinestNumber=3},           //第1Y轴
            //    //    new Axis(true, 3,"静压[Pa]", 800, 0, 10){ Foreground=Brushes.Blue, Unit="Pa", LinestNumber=3},                //第2Y轴
            //    //    new Axis(true, 11,"静压效率[%]", 50, 0, 10){ Foreground=Brushes.Red, Unit="%", LinestNumber=3},                //第3Y轴
            //    //    new Axis(true, 5,"转速[rpm]", 6000, 0, 10){ Foreground=Brushes.Violet, Unit="rpm", LinestNumber=3},         //第4Y轴
            //    //    new Axis(true, 10,"标况风量[m^3/h]", 5000, 0, 10){ Foreground=Brushes.Black, Unit="m^3/h", LinestNumber=3},// X轴
            //    //};
            //}
        }

        public void Refresh()
        {
            try
            {
                if (_isDisplayGraph)
                {
                    if (this.Steps == null || this.Steps.Count < 2)
                    {
                        path_1.Data = Geometry.Empty;
                        path_2.Data = Geometry.Empty;
                        path_3.Data = Geometry.Empty;
                        path_4.Data = Geometry.Empty;
                        return;
                    }
                    int i, j, n = 101;

                    double[] xAxis = new double[this.Steps.Count];
                    double[] yAxis1 = new double[this.Steps.Count];
                    double[] yAxis2 = new double[this.Steps.Count];
                    double[] yAxis3 = new double[this.Steps.Count];
                    double[] yAxis4 = new double[this.Steps.Count];

                    for (i = 0; i < this.Steps.Count; i++)
                    {
                        xAxis[i] = this.Steps[i][this.AxisArray[4].ChannelNo];
                        yAxis1[i] = this.Steps[i][this.AxisArray[0].ChannelNo];
                        yAxis2[i] = this.Steps[i][this.AxisArray[1].ChannelNo];
                        yAxis3[i] = this.Steps[i][this.AxisArray[2].ChannelNo];
                        yAxis4[i] = this.Steps[i][this.AxisArray[3].ChannelNo];
                    }
                    double xMax = xAxis.Max(), xMin = xAxis.Min();

                    double[] a1 = Linest(xAxis, yAxis1, this.AxisArray[0].LinestNumber);
                    double[] a2 = Linest(xAxis, yAxis2, this.AxisArray[1].LinestNumber);
                    double[] a3 = Linest(xAxis, yAxis3, this.AxisArray[2].LinestNumber);
                    double[] a4 = Linest(xAxis, yAxis4, this.AxisArray[3].LinestNumber);
                    double xScale = (xMax - xMin) / (n - 1.0);

                    double[] xNewDT = new double[n];
                    double[] yNewDT1 = new double[n];
                    double[] yNewDT2 = new double[n];
                    double[] yNewDT3 = new double[n];
                    double[] yNewDT4 = new double[n];

                    for (i = 0; i < n; i++)
                    {
                        xNewDT[i] = xMin + xScale * i;
                        for (j = 0; j < a1.Length; j++)
                        {
                            yNewDT1[i] += a1[j] * Math.Pow(xNewDT[i], a1.Length - j - 1);
                        }
                        for (j = 0; j < a2.Length; j++)
                        {
                            yNewDT2[i] += a2[j] * Math.Pow(xNewDT[i], a2.Length - j - 1);
                        }
                        for (j = 0; j < a3.Length; j++)
                        {
                            yNewDT3[i] += a3[j] * Math.Pow(xNewDT[i], a3.Length - j - 1);
                        }
                        for (j = 0; j < a4.Length; j++)
                        {
                            yNewDT4[i] += a4[j] * Math.Pow(xNewDT[i], a4.Length - j - 1);
                        }
                    }

                    string data1 = "M ";
                    string data2 = "M ";
                    string data3 = "M ";
                    string data4 = "M ";
                    double x = (xMin - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
                    double y1 = (this.AxisArray[0].Maximum - yNewDT1[0]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
                    double y2 = (this.AxisArray[1].Maximum - yNewDT2[0]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
                    double y3 = (this.AxisArray[2].Maximum - yNewDT3[0]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
                    double y4 = (this.AxisArray[3].Maximum - yNewDT4[0]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
                    data1 += x.ToString() + "," + y1.ToString();
                    data2 += x.ToString() + "," + y2.ToString();
                    data3 += x.ToString() + "," + y3.ToString();
                    data4 += x.ToString() + "," + y4.ToString();

                    n = n / 2;
                    for (i = 0; i < n; i++)
                    {
                        x = (xNewDT[i * 2 + 1] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
                        y1 = (this.AxisArray[0].Maximum - yNewDT1[i * 2 + 1]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
                        y2 = (this.AxisArray[1].Maximum - yNewDT2[i * 2 + 1]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
                        y3 = (this.AxisArray[2].Maximum - yNewDT3[i * 2 + 1]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
                        y4 = (this.AxisArray[3].Maximum - yNewDT4[i * 2 + 1]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
                        data1 += " S " + x.ToString() + "," + y1.ToString();
                        data2 += " S " + x.ToString() + "," + y2.ToString();
                        data3 += " S " + x.ToString() + "," + y3.ToString();
                        data4 += " S " + x.ToString() + "," + y4.ToString();
                        x = (xNewDT[i * 2 + 2] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
                        y1 = (this.AxisArray[0].Maximum - yNewDT1[i * 2 + 2]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
                        y2 = (this.AxisArray[1].Maximum - yNewDT2[i * 2 + 2]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
                        y3 = (this.AxisArray[2].Maximum - yNewDT3[i * 2 + 2]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
                        y4 = (this.AxisArray[3].Maximum - yNewDT4[i * 2 + 2]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
                        data1 += " " + x.ToString() + "," + y1.ToString();
                        data2 += " " + x.ToString() + "," + y2.ToString();
                        data3 += " " + x.ToString() + "," + y3.ToString();
                        data4 += " " + x.ToString() + "," + y4.ToString();
                    }

                    path_1.Data = Geometry.Parse(data1);
                    path_2.Data = Geometry.Parse(data2);
                    path_3.Data = Geometry.Parse(data3);
                    path_4.Data = Geometry.Parse(data4);
                }
            }
            catch
            {
                path_1.Data = Geometry.Empty;
                path_2.Data = Geometry.Empty;
                path_3.Data = Geometry.Empty;
                path_4.Data = Geometry.Empty;

                //MessageBox.Show("拟合失败");
            }
            finally
            {
                if (_isDisplayPoint)
                {
                    this.BuildPoints();
                }
            }

            #region 以前版本
            //if (this.Steps != null && this.Steps.Count >= 1)
            //{
            //    string data1 = "M ";
            //    string data2 = "M ";
            //    string data3 = "M ";
            //    string data4 = "M ";
            //    double x = (this.Steps[0][this.AxisArray[4].ChannelNo] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
            //    double y1 = (this.AxisArray[0].Maximum - this.Steps[0][this.AxisArray[0].ChannelNo]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
            //    double y2 = (this.AxisArray[1].Maximum - this.Steps[0][this.AxisArray[1].ChannelNo]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
            //    double y3 = (this.AxisArray[2].Maximum - this.Steps[0][this.AxisArray[2].ChannelNo]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
            //    double y4 = (this.AxisArray[3].Maximum - this.Steps[0][this.AxisArray[3].ChannelNo]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
            //    data1 += x.ToString() + "," + y1.ToString();
            //    data2 += x.ToString() + "," + y2.ToString();
            //    data3 += x.ToString() + "," + y3.ToString();
            //    data4 += x.ToString() + "," + y4.ToString();
            //    if (this.Steps.Count % 2 == 1)
            //    {
            //        for (int i = 0; i < this.Steps.Count / 2; i++)
            //        {
            //            x = (this.Steps[1 + i * 2][this.AxisArray[4].ChannelNo] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
            //            y1 = (this.AxisArray[0].Maximum - this.Steps[1 + i * 2][this.AxisArray[0].ChannelNo]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
            //            y2 = (this.AxisArray[1].Maximum - this.Steps[1 + i * 2][this.AxisArray[1].ChannelNo]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
            //            y3 = (this.AxisArray[2].Maximum - this.Steps[1 + i * 2][this.AxisArray[2].ChannelNo]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
            //            y4 = (this.AxisArray[3].Maximum - this.Steps[1 + i * 2][this.AxisArray[3].ChannelNo]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
            //            data1 += " S " + x.ToString() + "," + y1.ToString();
            //            data2 += " S " + x.ToString() + "," + y2.ToString();
            //            data3 += " S " + x.ToString() + "," + y3.ToString();
            //            data4 += " S " + x.ToString() + "," + y4.ToString();
            //            x = (this.Steps[2 + i * 2][this.AxisArray[4].ChannelNo] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
            //            y1 = (this.AxisArray[0].Maximum - this.Steps[2 + i * 2][this.AxisArray[0].ChannelNo]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
            //            y2 = (this.AxisArray[1].Maximum - this.Steps[2 + i * 2][this.AxisArray[1].ChannelNo]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
            //            y3 = (this.AxisArray[2].Maximum - this.Steps[2 + i * 2][this.AxisArray[2].ChannelNo]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
            //            y4 = (this.AxisArray[3].Maximum - this.Steps[2 + i * 2][this.AxisArray[3].ChannelNo]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
            //            data1 += " " + x.ToString() + "," + y1.ToString();
            //            data2 += " " + x.ToString() + "," + y2.ToString();
            //            data3 += " " + x.ToString() + "," + y3.ToString();
            //            data4 += " " + x.ToString() + "," + y4.ToString();
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < this.Steps.Count / 2 - 1; i++)
            //        {
            //            x = (this.Steps[1 + i * 2][this.AxisArray[4].ChannelNo] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
            //            y1 = (this.AxisArray[0].Maximum - this.Steps[1 + i * 2][this.AxisArray[0].ChannelNo]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
            //            y2 = (this.AxisArray[1].Maximum - this.Steps[1 + i * 2][this.AxisArray[1].ChannelNo]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
            //            y3 = (this.AxisArray[2].Maximum - this.Steps[1 + i * 2][this.AxisArray[2].ChannelNo]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
            //            y4 = (this.AxisArray[3].Maximum - this.Steps[1 + i * 2][this.AxisArray[3].ChannelNo]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
            //            data1 += " S " + x.ToString() + "," + y1.ToString();
            //            data2 += " S " + x.ToString() + "," + y2.ToString();
            //            data3 += " S " + x.ToString() + "," + y3.ToString();
            //            data4 += " S " + x.ToString() + "," + y4.ToString();
            //            x = (this.Steps[2 + i * 2][this.AxisArray[4].ChannelNo] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
            //            y1 = (this.AxisArray[0].Maximum - this.Steps[2 + i * 2][this.AxisArray[0].ChannelNo]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
            //            y2 = (this.AxisArray[1].Maximum - this.Steps[2 + i * 2][this.AxisArray[1].ChannelNo]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
            //            y3 = (this.AxisArray[2].Maximum - this.Steps[2 + i * 2][this.AxisArray[2].ChannelNo]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
            //            y4 = (this.AxisArray[3].Maximum - this.Steps[2 + i * 2][this.AxisArray[3].ChannelNo]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
            //            data1 += " " + x.ToString() + "," + y1.ToString();
            //            data2 += " " + x.ToString() + "," + y2.ToString();
            //            data3 += " " + x.ToString() + "," + y3.ToString();
            //            data4 += " " + x.ToString() + "," + y4.ToString();
            //        }
            //        x = (this.Steps.Last()[this.AxisArray[4].ChannelNo] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
            //        y1 = (this.AxisArray[0].Maximum - this.Steps.Last()[this.AxisArray[0].ChannelNo]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
            //        y2 = (this.AxisArray[1].Maximum - this.Steps.Last()[this.AxisArray[1].ChannelNo]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
            //        y3 = (this.AxisArray[2].Maximum - this.Steps.Last()[this.AxisArray[2].ChannelNo]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
            //        y4 = (this.AxisArray[3].Maximum - this.Steps.Last()[this.AxisArray[3].ChannelNo]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
            //        data1 += " S " + x.ToString() + "," + y1.ToString() + " " + x.ToString() + "," + y1.ToString();
            //        data2 += " S " + x.ToString() + "," + y2.ToString() + " " + x.ToString() + "," + y2.ToString();
            //        data3 += " S " + x.ToString() + "," + y3.ToString() + " " + x.ToString() + "," + y3.ToString();
            //        data4 += " S " + x.ToString() + "," + y4.ToString() + " " + x.ToString() + "," + y4.ToString();
            //    }

            //    path_1.Data = Geometry.Parse(data1);
            //    path_2.Data = Geometry.Parse(data2);
            //    path_3.Data = Geometry.Parse(data3);
            //    path_4.Data = Geometry.Parse(data4);
            //}
            //else
            //{
            //    path_1.Data = Geometry.Empty;
            //    path_2.Data = Geometry.Empty;
            //    path_3.Data = Geometry.Empty;
            //    path_4.Data = Geometry.Empty;
            //}
            #endregion
        }

        public void BuildPoints()
        {
            if (this.Steps != null)
            {
                if (this.Steps.Count > this.y1Ponts.Count)
                {
                    for (int i = this.y1Ponts.Count; i < this.Steps.Count; i++)
                    {
                        Ellipse ell = new Ellipse();
                        ell.Height = legendSize;
                        ell.Width = legendSize;
                        ell.HorizontalAlignment = HorizontalAlignment.Left;
                        ell.VerticalAlignment = VerticalAlignment.Top;
                        ell.ToolTip = "";
                        ell.ToolTipOpening += new ToolTipEventHandler(ell_ToolTipOpening);
                        y1Ponts.Add(ell);
                        gridGraph.Children.Add(ell);

                        Ellipse ell2 = new Ellipse();
                        ell2.Height = legendSize;
                        ell2.Width = legendSize;
                        ell2.HorizontalAlignment = HorizontalAlignment.Left;
                        ell2.VerticalAlignment = VerticalAlignment.Top;
                        ell2.ToolTip = "";
                        ell2.ToolTipOpening += new ToolTipEventHandler(ell_ToolTipOpening);
                        y2Ponts.Add(ell2);
                        gridGraph.Children.Add(ell2);

                        Ellipse ell3 = new Ellipse();
                        ell3.Height = legendSize;
                        ell3.Width = legendSize;
                        ell3.HorizontalAlignment = HorizontalAlignment.Left;
                        ell3.VerticalAlignment = VerticalAlignment.Top;
                        ell3.ToolTip = "";
                        ell3.ToolTipOpening += new ToolTipEventHandler(ell_ToolTipOpening);
                        y3Ponts.Add(ell3);
                        gridGraph.Children.Add(ell3);

                        Ellipse ell4 = new Ellipse();
                        ell4.Height = legendSize;
                        ell4.Width = legendSize;
                        ell4.HorizontalAlignment = HorizontalAlignment.Left;
                        ell4.VerticalAlignment = VerticalAlignment.Top;
                        ell4.ToolTip = "";
                        ell4.ToolTipOpening += new ToolTipEventHandler(ell_ToolTipOpening);
                        y4Ponts.Add(ell4);
                        gridGraph.Children.Add(ell4);
                    }
                }
                else
                {
                    for (int i = this.Steps.Count; i < this.y1Ponts.Count; i++)
                    {
                        gridGraph.Children.Remove(y1Ponts[i]);
                        gridGraph.Children.Remove(y2Ponts[i]);
                        gridGraph.Children.Remove(y3Ponts[i]);
                        gridGraph.Children.Remove(y4Ponts[i]);
                    }
                    y1Ponts.RemoveRange(this.Steps.Count, this.y1Ponts.Count - this.Steps.Count);
                    y2Ponts.RemoveRange(this.Steps.Count, this.y2Ponts.Count - this.Steps.Count);
                    y3Ponts.RemoveRange(this.Steps.Count, this.y3Ponts.Count - this.Steps.Count);
                    y4Ponts.RemoveRange(this.Steps.Count, this.y4Ponts.Count - this.Steps.Count);
                }

                for (int i = 0; i < this.Steps.Count; i++)
                {
                    double x = (this.Steps[i][this.AxisArray[4].ChannelNo] - this.AxisArray[4].Minimum) / (this.AxisArray[4].Maximum - this.AxisArray[4].Minimum) * gridGraph.ActualWidth;
                    double y1 = (this.AxisArray[0].Maximum - this.Steps[i][this.AxisArray[0].ChannelNo]) / (this.AxisArray[0].Maximum - this.AxisArray[0].Minimum) * gridGraph.ActualHeight;
                    double y2 = (this.AxisArray[1].Maximum - this.Steps[i][this.AxisArray[1].ChannelNo]) / (this.AxisArray[1].Maximum - this.AxisArray[1].Minimum) * gridGraph.ActualHeight;
                    double y3 = (this.AxisArray[2].Maximum - this.Steps[i][this.AxisArray[2].ChannelNo]) / (this.AxisArray[2].Maximum - this.AxisArray[2].Minimum) * gridGraph.ActualHeight;
                    double y4 = (this.AxisArray[3].Maximum - this.Steps[i][this.AxisArray[3].ChannelNo]) / (this.AxisArray[3].Maximum - this.AxisArray[3].Minimum) * gridGraph.ActualHeight;
                    y1Ponts[i].Margin = new Thickness(x - legendSize / 2.0, y1 - legendSize / 2.0, 0, 0);
                    y1Ponts[i].Fill = path_1.Stroke;
                    y1Ponts[i].Visibility = path_1.Visibility;
                    y1Ponts[i].Tag = "0," + i;
                    //y1Ponts[i].ToolTip = "[" + this.Steps[i][this.AxisArray[4].ChannelNo] + "," + this.Steps[i][this.AxisArray[0].ChannelNo] + "]";

                    y2Ponts[i].Margin = new Thickness(x - legendSize / 2.0, y2 - legendSize / 2.0, 0, 0);
                    y2Ponts[i].Fill = path_2.Stroke;
                    y2Ponts[i].Visibility = path_2.Visibility;
                    y2Ponts[i].Tag = "1," + i;
                    //y2Ponts[i].ToolTip = "[" + this.Steps[i][this.AxisArray[4].ChannelNo] + "," + this.Steps[i][this.AxisArray[1].ChannelNo] + "]";

                    y3Ponts[i].Margin = new Thickness(x - legendSize / 2.0, y3 - legendSize / 2.0, 0, 0);
                    y3Ponts[i].Fill = path_3.Stroke;
                    y3Ponts[i].Visibility = path_3.Visibility;
                    y3Ponts[i].Tag = "2," + i;
                    //y3Ponts[i].ToolTip = "[" + this.Steps[i][this.AxisArray[4].ChannelNo] + "," + this.Steps[i][this.AxisArray[2].ChannelNo] + "]";

                    y4Ponts[i].Margin = new Thickness(x - legendSize / 2.0, y4 - legendSize / 2.0, 0, 0);
                    y4Ponts[i].Fill = path_4.Stroke;
                    y4Ponts[i].Visibility = path_4.Visibility;
                    y4Ponts[i].Tag = "3," + i;
                    //y4Ponts[i].ToolTip = "[" + this.Steps[i][this.AxisArray[4].ChannelNo] + "," + this.Steps[i][this.AxisArray[3].ChannelNo] + "]";
                }
            }
        }

        void ell_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            Ellipse ell = sender as Ellipse;
            string[] str = ell.Tag.ToString().Split(',');
            int index1 = int.Parse(str[0]);
            int index2 = int.Parse(str[1]);
            ell.ToolTip = //(this.StepsRemark[index2][12] == null ? "" : this.StepsRemark[index2][12]) +
                "[" + this.Steps[index2][this.AxisArray[4].ChannelNo] + "," + this.Steps[index2][this.AxisArray[index1].ChannelNo] + "]";
        }
        #endregion

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            switch (mi.Name)
            {
                case "menuItem_Set":
                    new FitCurvesSetting(this).ShowDialog();
                    break;
                case "menuItem_Optimize":

                    break;
                case "menuItem_Refresh":
                    break;
            }
        }

        private void gridGraph_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// 回归参数计算方法
        /// </summary>
        /// <param name="x">x轴数据集合</param>
        /// <param name="y">y轴数据集合</param>
        /// <param name="n">多项式拟合的次方数</param>
        /// <returns>返回回归参数</returns>
        public double[] Linest(double[] x, double[] y, int n)
        {
            if (x.Length != y.Length || x.Length < 2)
            {
                //throw new Exception("x轴与y轴数据不对称,双方需数据一一对应,数据个数应相等！");
                return new double[] { -1 };
            }

            if (n > x.Length - 1)
                n = x.Length - 1;

            int i = 0, j = 0, m = n + 1;
            double[] a = new double[m];
            double[] sum = new double[3 * n + 2];
            double[,] dt = new double[m, n + 2];
            //取出所有数据
            for (i = 0; i < x.Length; i++)
            {
                for (j = 0; j < 2 * n + 1; j++)
                {
                    sum[j] += Math.Pow(x[i], j);
                }
                for (j = 2 * n + 1; j < sum.Length; j++)
                {
                    sum[j] += y[i] * Math.Pow(x[i], j - 2 * n - 1);
                }
            }
            //用取出的数据放入到矩阵中
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < m; j++)
                {
                    dt[i, j] = sum[2 * n - i - j];
                }
                dt[i, m] = sum[3 * n + 1 - i];
            }
            //使用消元法使矩阵呈现下三角全为0的情况
            for (int k = 0; k < m; k++)
            {
                for (i = k; i < m; i++)
                {
                    for (j = m; j >= k; j--)
                    {
                        dt[i, j] /= dt[i, k];
                        if (i != k)
                        {
                            dt[i, j] -= dt[k, j];
                        }
                    }
                }
            }
            //返回求出所有参数
            for (int k = 0; k < m; k++)
            {
                a[n - k] = dt[n - k, m];
                for (i = 0; i < k; i++)
                {
                    a[n - k] = a[n - k] - dt[n - k, n - i] * a[n - i];
                }

                a[n - k] /= dt[n - k, n - k];
            }

            return a;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Refresh();
        //}
    }
}
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
using System.Globalization;
////using SHHS.FPTB.Base;
using System.Reflection;
using System.IO;

namespace SHHS.UILabs.RealtimeCurves
{
    /// <summary>
    /// RealtimeCurve.xaml 的交互逻辑
    /// </summary>
    public partial class RealtimeCurves : UserControl
    {
        private List<Curve> _curves = new List<Curve>();        //曲线的实例集合
        private List<Axis> _yAxises = new List<Axis>();         //Y轴的实例集合
        private Axis _xAxis;                                    //X轴实例
        private Dictionary<int, Channel> _channels;     //用于接收通道数据
        private double _gridCurveLeft = 10;             //设置GridCurve的Margin.Left
        private double _gridCurveTop = 22;              //设置GridCurve的Margin.Top
        private double _gridCurveRight = 248;           //设置GridCurve的Margin.Right
        private double _gridCurveBottom = 40;           //设置GridCurve的Margin.Bottom
        private BtnMode _btnMode = BtnMode.ComeBack;    //记录按钮的状态
        private BtnMode _tempBtnMode;
        private SolidColorBrush _btnColor = new SolidColorBrush();  //按钮状态变化的颜色
        private bool _isCursor1 = false;        //光标一是否显示
        private bool _isCursor2 = false;        //光标二是否显示
        private bool IsLineYCursor1 = false;    //光标一的纵轴是否选中
        private bool IsLineXCursor1 = false;    //光标一的横轴是否选中
        private bool IsLineYCursor2 = false;    //光标二的纵轴是否选中
        private bool IsLineXCursor2 = false;    //光标二的横轴是否选中
        private double _choosedSpan = 2.111;    //鼠标点击与曲线的距离在多少之内,视为选中曲线
        private double _shortcut = 0;           //用于判断 拖拽横线的时候 横线离哪条线的距离最短
        private int _indexCursor1;              //记录光标一所在曲线  _curves[_indexCursor1]
        private int _indexCursor2;              //记录光标二所在曲线  _curves[_indexCursor2]
        private int _indexPoint1 = 0;           //记录光标一在曲线的位置  _curves[_indexCursor1].Points[_indexPoints1]
        private int _indexPoint2 = 0;           //记录光标二在曲线的位置  _curves[_indexCursor2].Points[_indexPoints2]
        private string _language = "zh-CN";     //语言
        private List<double> _comeBackDatas = new List<double>();   //为恢复键准备的数据
        private double _zoomCoefficient = 0.1;  //缩放的系数(+，- 按钮所用系数) 不能小于0
        private Point _startPoint;              //鼠标左键单击GridCurve的起始点
        private Point _endPoint;                //鼠标左键单击GridCurve的结束点
        private bool ONorOFF = false;           //判断鼠标是否被按下
        private List<double> _choosedDatas = new List<double>();   //被放大框选中的数据集合
        private List<CheckBox> _curveCheckBoxs = new List<CheckBox>();      //控制曲线显示与隐藏并显示曲线名的选择框集合
        private List<Button> _curveButtons = new List<Button>();            //显示曲线颜色并控制曲线高亮的按钮的集合
        private List<TextBlock> _curveTextBlocks = new List<TextBlock>();   //显示曲线的最新数据值的
        private List<AppendControl> _appendControls=new List<AppendControl>();  //为曲线添加的附加控件的集合
        private Dictionary<string, List<string>> _channelMapping = new Dictionary<string, List<string>>();  //记录通道信息 keys中保存通道分类(Items),每个key对应的List<string>中保存该key类下所有的通道名
        private Dictionary<string, string> _channelUnits = new Dictionary<string, string>(); //所有Y轴对应的单位 国际标准符号,单位描述
        private double _moveCoefficient = 0.3;      //曲线移动系数 
        private MoveMode _moveMode = MoveMode.Compression;    //曲线的移动方式
        private bool _bool = false;   
        private List<ResourceDictionary> _resources = new List<ResourceDictionary>();   //存储资源文件
        private CultureInfo _currentUICulture = new CultureInfo("zh-CN");    //当前语言
        private bool _sliderMapping = true;         //控制滑轴是否对曲线产生作用
        private double _axisminTime = 0;        //记录手动拖动开始的时候,时间轴的最小值
        private double _axismaxTime = 0;        //记录手动拖动开始的时候,时间轴的最打值

        public string TestType = "Compositive";   //试验类型


        /// <summary>
        /// Monitor的构造函数
        /// </summary>
        public RealtimeCurves()
        {
            InitializeComponent();

            for (int i = 0; i < this.Resources.MergedDictionaries.Count; i++)
            {
                _resources.Add(this.Resources.MergedDictionaries[i]);
            }

            _xAxis = new Axis(12, 1800, 0, "", new Point(_gridCurveLeft - 5, GridBase.ActualHeight - _gridCurveBottom + 2), new Point(GridBase.ActualWidth - _gridCurveRight + 45, GridBase.ActualHeight - 20), AxisMode.Level, Brushes.Black, GridBase);
            _xAxis.IsVisibility = true;
            _xAxis.Unit = "时间:分钟";
            _btnColor = Brushes.YellowGreen;
        }

        /// <summary>
        /// Monitor的加载事件
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockMaxTime.Text = Convert.ToInt32(sliderMaxTime.Value / 60).ToString();
            textBlockMinTime.Text = Convert.ToInt32(sliderMinTime.Value / 60).ToString();
        }

        /// <summary>
        /// GridBase的SizeChanged事件
        /// </summary>
        private void GridBase_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _xAxis.StartPoint = new Point(_gridCurveLeft - 5, GridBase.ActualHeight - _gridCurveBottom + 2);
            _xAxis.EndPoint = new Point(GridBase.ActualWidth - _gridCurveRight + 45, GridBase.ActualHeight - 20);
            for (int i = 0; i < _yAxises.Count; i++)
            {
                _yAxises[i].StartPoint= new Point(50 * i, 13);
                _yAxises[i].EndPoint = new Point(50 * i + 50, GridBase.ActualHeight - 31);
                _yAxises[i].RefreshLocation();
            }
        }

        /// <summary>
        /// GridCurve的SizeChanged事件
        /// </summary>
        private void GridCurve_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //网格
            //_gridding.DisplayGridding();
            //固定按钮 
            downY.Margin = new Thickness(_gridCurveLeft, 1, GridBase.ActualWidth - _gridCurveLeft - 24, GridBase.ActualHeight - 21);
            upY.Margin = new Thickness(_gridCurveLeft + 26, 1, GridBase.ActualWidth - _gridCurveLeft - 50, GridBase.ActualHeight - 21);
            downXY.Margin = new Thickness(_gridCurveLeft, GridBase.ActualHeight - 23, GridBase.ActualWidth - _gridCurveLeft - 24, 3);
            upXY.Margin = new Thickness(_gridCurveLeft + 26, GridBase.ActualHeight - 23, GridBase.ActualWidth - _gridCurveLeft - 50, 3);
            //曲线
            foreach (Curve cur in this._curves)
            {
                cur.Build();
            }
        }

        /// <summary>
        /// 滑行轴的ValueChanged事件的处理程序
        /// </summary>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        { 
            switch (((Slider)sender).Name)
            {
                case "sliderMaxTime":
                    textBlockMaxTime.Text = Convert.ToInt32(sliderMaxTime.Value / 60).ToString();
                    MinSliderMaxValue.Text = textBlockMaxTime.Text;
                    this._xAxis.MaxValue = Convert.ToInt32(sliderMaxTime.Value);
                    sliderMinTime.Maximum = Convert.ToInt32(sliderMaxTime.Value);
                    if (sliderMaxTime.Value > 0)
                    {
                        sliderMinTime.IsEnabled = true;
                    }
                    else
                    {
                        sliderMinTime.Maximum = 1;
                        sliderMinTime.Value = 0;
                        sliderMinTime.IsEnabled = false;
                    }
                    if (sliderMaxTime.Value <= sliderMinTime.Value)
                    {
                        if (_sliderMapping)
                        {
                            _sliderMapping = false;
                            sliderMinTime.Value = sliderMaxTime.Value;
                            _sliderMapping = true;
                            foreach (Curve cur in this._curves)
                            {
                                cur.MaxTime = this._xAxis.MaxValue;
                                cur.MinTime = this._xAxis.MinValue;
                                cur.Build();
                            }
                        }
                        else
                        {
                            sliderMinTime.Value = sliderMaxTime.Value;
                        }
                    }
                    else
                    {
                        if (_sliderMapping)
                        {
                            foreach (Curve cur in this._curves)
                            {
                                cur.MaxTime = this._xAxis.MaxValue;
                                cur.Build();
                            }
                        }
                    }
                    //this._xAxis.MaxValue = Convert.ToInt32(sliderMaxTime.Value);
                    break;
                case "sliderMinTime":
                    textBlockMinTime.Text = Convert.ToInt32(sliderMinTime.Value / 60).ToString();
                    this._xAxis.MinValue = Convert.ToInt32(sliderMinTime.Value);
                    if (_sliderMapping)
                    {
                        foreach (Curve cur in this._curves)
                        {
                            cur.MinTime = this._xAxis.MinValue;
                            cur.Build();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 所有Button的点击事件处理程序
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "ComeBack":
                    ComeBack.Background = _btnColor;
                    ZoomIn.Background = Setter.Background;
                    ManualScroll.Background = Setter.Background;
                    _btnMode = BtnMode.ComeBack;
                    if (_isCursor1)
                    {
                        _isCursor1 = false;
                        Cursor1.Background = Setter.Background;
                        lineYCursor1.Visibility = Visibility.Collapsed;
                        lineXCursor1.Visibility = Visibility.Collapsed;
                        ellCursor1.Visibility = Visibility.Collapsed;
                        tbkCursor1.Visibility = Visibility.Collapsed;
                    }
                    if (_isCursor2)
                    {
                        _isCursor2 = false;
                        Cursor2.Background = Setter.Background;
                        lineYCursor2.Visibility = Visibility.Collapsed;
                        lineXCursor2.Visibility = Visibility.Collapsed;
                        ellCursor2.Visibility = Visibility.Collapsed;
                        tbkCursor2.Visibility = Visibility.Collapsed;
                    }
                    //曲线X轴数据
                    if (_channels != null && _comeBackDatas.Count != 0)
                    {
                        _sliderMapping = false;
                        sliderMinTime.Value = _comeBackDatas[0];
                        double tempdata = _comeBackDatas[1];
                        foreach (Curve curve in this._curves)
                        {
                            if (curve.SourceData.Count >= 1)
                            {
                                if (tempdata < MomentData.GetTimeSpan(curve.SourceData, curve.SourceData.Count - 1).TotalMinutes)
                                {
                                    tempdata = MomentData.GetTimeSpan(curve.SourceData, curve.SourceData.Count - 1).TotalMinutes + 1;
                                    _comeBackDatas[1] = tempdata;
                                }
                            }
                        }
                        sliderMaxTime.Value = _comeBackDatas[1] * 60;
                        _sliderMapping = true;
                    }
                    else
                    {
                        sliderMinTime.Value = 0;
                        sliderMaxTime.Value = 30 * 60;
                    }
                    foreach (Curve cur in this._curves)
                    {
                        cur.MinTime = _xAxis.MinValue;
                        cur.MaxTime = _xAxis.MaxValue;
                    }
                    //曲线Y轴数据
                    for (int i = 0; i < _yAxises.Count; i++)
                    {
                        _yAxises[i].MinValue = _comeBackDatas[2 + i * 2];
                        _yAxises[i].MaxValue = _comeBackDatas[3 + i * 2];
                    }
                    bool _curveMappingAxis = false;
                    foreach (Curve curve in _curves)
                    {
                        _curveMappingAxis = false;
                        foreach (Axis axis in _yAxises)
                        {
                            if (curve.DataUnits.Equals(axis.Unit.Split(' ').ElementAt(2)))
                            {
                                _curveMappingAxis = true;
                                curve.MaxData = axis.MaxValue;
                                curve.MinData = axis.MinValue;
                                curve.Build();
                                break;
                            }
                        }
                        if (!_curveMappingAxis)
                        {
                            
                            //curve.MaxData = this._channels[curve.ChannelIndex].MaxValue;
                            //curve.MinData = this._channels[curve.ChannelIndex].MinValue;
                            curve.MaxData = this._channels[curve.ChannelIndex].Maximum;
                            curve.MinData = this._channels[curve.ChannelIndex].Minimum;
                            curve.Build();
                        }
                    }
                    break;
                case "ZoomIn":
                    ComeBack.Background = Setter.Background;
                    ZoomIn.Background = _btnColor;
                    ManualScroll.Background = Setter.Background;
                    _btnMode = BtnMode.ZoomIn;
                    if (_isCursor1)
                    {
                        _isCursor1 = false;
                        Cursor1.Background = Setter.Background;
                        lineYCursor1.Visibility = Visibility.Collapsed;
                        lineXCursor1.Visibility = Visibility.Collapsed;
                        ellCursor1.Visibility = Visibility.Collapsed;
                        tbkCursor1.Visibility = Visibility.Collapsed;
                    }
                    if (_isCursor2)
                    {
                        _isCursor2 = false;
                        Cursor2.Background = Setter.Background;
                        lineYCursor2.Visibility = Visibility.Collapsed;
                        lineXCursor2.Visibility = Visibility.Collapsed;
                        ellCursor2.Visibility = Visibility.Collapsed;
                        tbkCursor2.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "Cursor1":
                    if (lineYCursor1.Visibility != Visibility.Visible)
                    {
                        _isCursor1 = true;
                        if (!_isCursor2)
                        {
                            _tempBtnMode = _btnMode;
                        }
                        _btnMode = BtnMode.Cursor;

                        Cursor1.Background = _btnColor;
                        int _count = 0;
                        foreach (Curve cur in this._curves)
                        {
                            if (cur.IsHighlight && cur.IsVisible)
                            {
                                _count += 1;
                            }
                        }
                        if (_count >= 1)
                        {
                            for (int i = 0; i < this._curves.Count; i++)
                            {
                                if (this._curves[i].IsVisible && this._curves[i].IsHighlight)
                                {
                                    for (int n = 0; n < this._curves[i].Points.Count - 1; n++)
                                    {
                                        if (_curves[i].Points[n].X <= 0 && _curves[i].Points[n + 1].X > 0)
                                        {
                                            _indexCursor1 = i;
                                            _indexPoint1 = n + 1;
                                            RefreshCursor(lineYCursor1, lineXCursor1, ellCursor1, tbkCursor1, _indexCursor1, _indexPoint1);
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < this._curves.Count; i++)
                            {
                                if (this._curves[i].IsVisible)
                                {
                                    for (int n = 0; n < this._curves[i].Points.Count - 1; n++)
                                    {
                                        if (_curves[i].Points[n].X <= 0 && _curves[i].Points[n + 1].X > 0)
                                        {
                                            _indexCursor1 = i;
                                            _indexPoint1 = n + 1;
                                            RefreshCursor(lineYCursor1, lineXCursor1, ellCursor1, tbkCursor1, _indexCursor1, _indexPoint1);
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        _isCursor1 = false;
                        Cursor1.Background = Setter.Background;
                        lineYCursor1.Visibility = Visibility.Collapsed;
                        lineXCursor1.Visibility = Visibility.Collapsed;
                        ellCursor1.Visibility = Visibility.Collapsed;
                        tbkCursor1.Visibility = Visibility.Collapsed;
                        if (!_isCursor2)
                        {
                            _btnMode = _tempBtnMode;
                        }
                    }
                    break;
                case "Cursor2":
                    if (lineYCursor2.Visibility != Visibility.Visible)
                    {
                        _isCursor2 = true;
                        if (!_isCursor1)
                        {
                            _tempBtnMode = _btnMode;
                        }
                        _btnMode = BtnMode.Cursor;
                        Cursor2.Background = _btnColor;
                        int _count = 0;
                        for (int i = 0; i < this._curves.Count; i++)
                        {
                            if (_curves[i].IsVisible && _curves[i].IsHighlight)
                            {
                                _count += 1;
                                if (_count == 2)
                                {
                                    _indexCursor2 = i;
                                }
                            }
                        }
                        if (_count >= 2)
                        {
                            for (int i = 0; i < _curves[_indexCursor2].Points.Count - 1; i++)
                            {
                                if (_curves[_indexCursor2].Points[i].X <= 0 && _curves[_indexCursor2].Points[i + 1].X > 0)
                                {
                                    _indexPoint2 = i + 1;
                                    RefreshCursor(lineYCursor2, lineXCursor2, ellCursor2, tbkCursor2, _indexCursor2, _indexPoint2);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (_count == 1)
                            {
                                for (int i = 0; i < this._curves.Count; i++)
                                {
                                    if (this._curves[i].IsVisible && this._curves[i].IsHighlight)
                                    {
                                        for (int n = 0; n < this._curves[i].Points.Count - 1; n++)
                                        {
                                            if (_curves[i].Points[n].X <= 0 && _curves[i].Points[n + 1].X > 0)
                                            {
                                                _indexCursor2 = i;
                                                _indexPoint2 = n + 1;
                                                RefreshCursor(lineYCursor2, lineXCursor2, ellCursor2, tbkCursor2, _indexCursor2, _indexPoint2);
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                int number = 0;
                                for (int i = 0; i < _curves.Count; i++)
                                {
                                    if (_curves[i].IsVisible)
                                    {
                                        number += 1;
                                    }
                                }
                                if (number == 1)
                                {
                                    for (int i = 0; i < _curves.Count; i++)
                                    {
                                        if (_curves[i].IsVisible)
                                        {
                                            for (int n = 0; n < _curves[i].Points.Count - 1; n++)
                                            {
                                                if (_curves[i].Points[n].X <= 0 && _curves[i].Points[n + 1].X > 0)
                                                {
                                                    _indexCursor2 = i;
                                                    _indexPoint2 = n + 1;
                                                    RefreshCursor(lineYCursor2, lineXCursor2, ellCursor2, tbkCursor2, _indexCursor2, _indexPoint2);
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (number >= 2)
                                    {
                                        int j = 0;
                                        for (int i = 0; i < _curves.Count; i++)
                                        {
                                            if (_curves[i].IsVisible)
                                            {
                                                j += 1;
                                                if (j == 2)
                                                {
                                                    for (int n = 0; n < _curves[i].Points.Count - 1; n++)
                                                    {
                                                        if (_curves[i].Points[n].X <= 0 && _curves[i].Points[n + 1].X > 0)
                                                        {
                                                            _indexCursor2 = i;
                                                            _indexPoint2 = n + 1;
                                                            RefreshCursor(lineYCursor2, lineXCursor2, ellCursor2, tbkCursor2, _indexCursor2, _indexPoint2);
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _isCursor2 = false;
                        Cursor2.Background = Setter.Background;
                        lineYCursor2.Visibility = Visibility.Collapsed;
                        lineXCursor2.Visibility = Visibility.Collapsed;
                        ellCursor2.Visibility = Visibility.Collapsed;
                        tbkCursor2.Visibility = Visibility.Collapsed;
                        if (!_isCursor1)
                        {
                            _btnMode = _tempBtnMode;
                        }
                    }
                    break;
                case "Setter":
                    new MultiSetter(this).ShowDialog();
                    break;
                case "ManualScroll":
                    ComeBack.Background = Setter.Background;
                    ZoomIn.Background = Setter.Background;
                    ManualScroll.Background = _btnColor;
                    _btnMode = BtnMode.ManualScroll;
                    if (_isCursor1)
                    {
                        _isCursor1 = false;
                        Cursor1.Background = Setter.Background;
                        lineYCursor1.Visibility = Visibility.Collapsed;
                        lineXCursor1.Visibility = Visibility.Collapsed;
                        ellCursor1.Visibility = Visibility.Collapsed;
                        tbkCursor1.Visibility = Visibility.Collapsed;
                    }
                    if (_isCursor2)
                    {
                        _isCursor2 = false;
                        Cursor2.Background = Setter.Background;
                        lineYCursor2.Visibility = Visibility.Collapsed;
                        lineXCursor2.Visibility = Visibility.Collapsed;
                        ellCursor2.Visibility = Visibility.Collapsed;
                        tbkCursor2.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "downX":
                    sliderMaxTime.Value = sliderMaxTime.Value + (sliderMaxTime.Value - sliderMinTime.Value) * _zoomCoefficient;
                    break;
                case "upX":
                    sliderMaxTime.Value = sliderMaxTime.Value - (sliderMaxTime.Value - sliderMinTime.Value) * _zoomCoefficient;
                    break;
                case "downY":
                    foreach (Axis axis in _yAxises)
                    {
                        axis.MaxValue = axis.MaxValue + (axis.MaxValue - axis.MinValue) * _zoomCoefficient;
                    }
                    foreach (Curve curve in _curves)
                    {
                        curve.MaxData = curve.MaxData + (curve.MaxData - curve.MinData) * _zoomCoefficient;
                        curve.Build();
                    }
                    break;
                case "upY":
                    foreach (Axis axis in _yAxises)
                    {
                        axis.MaxValue = axis.MaxValue - (axis.MaxValue - axis.MinValue) * _zoomCoefficient;
                    }
                    foreach (Curve curve in _curves)
                    {
                        curve.MaxData = curve.MaxData - (curve.MaxData - curve.MinData) * _zoomCoefficient;
                        curve.Build();
                    }
                    break;
                case "downXY":
                    foreach (Axis axis in _yAxises)
                    {
                        axis.MaxValue = axis.MaxValue + (axis.MaxValue - axis.MinValue) * _zoomCoefficient;
                    }
                    foreach (Curve curve in _curves)
                    {
                        curve.MaxData = curve.MaxData + (curve.MaxData - curve.MinData) * _zoomCoefficient;
                    }
                    sliderMaxTime.Value = sliderMaxTime.Value + (sliderMaxTime.Value - sliderMinTime.Value) * _zoomCoefficient;
                    break;
                case "upXY":
                    foreach (Axis axis in _yAxises)
                    {
                        axis.MaxValue = axis.MaxValue - (axis.MaxValue - axis.MinValue) * _zoomCoefficient;
                    }
                    foreach (Curve curve in _curves)
                    {
                        curve.MaxData = curve.MaxData - (curve.MaxData - curve.MinData) * _zoomCoefficient;
                    }
                    sliderMaxTime.Value = sliderMaxTime.Value - (sliderMaxTime.Value - sliderMinTime.Value) * _zoomCoefficient;
                    break;
                default:
                    MessageBox.Show("这个按钮居然没写代码,我汗!!!");
                    break;
            }
        }
        
        /// <summary>
        /// 鼠标左键点击事件(放大,滚动,拖动光标)
        /// </summary>
        private void GridCurve_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(GridCurve);
            switch (_btnMode)
            { 
                case BtnMode.ComeBack:
                    break;
                case BtnMode.Cursor:
                    if (_isCursor1 == true && _isCursor2 == false)
                    {
                        if (Math.Abs(_startPoint.X - lineYCursor1.X1) <= _choosedSpan && Math.Abs(_startPoint.Y - lineXCursor1.Y1) > _choosedSpan)
                        {
                            this.Cursor = Cursors.ScrollWE; //2010.04.04添加
                            IsLineYCursor1 = true;
                        }
                        else
                        {
                            if (Math.Abs(_startPoint.X - lineYCursor1.X1) > _choosedSpan && Math.Abs(_startPoint.Y - lineXCursor1.Y1) <= _choosedSpan)
                            {
                                this.Cursor = Cursors.ScrollNS; //2010.04.04添加
                                IsLineXCursor1 = true;
                            }
                        }
                    }
                    if (_isCursor1 == false && _isCursor2 == true)
                    {
                        if (Math.Abs(_startPoint.X - lineYCursor2.X1) <= _choosedSpan && Math.Abs(_startPoint.Y - lineXCursor2.Y1) > _choosedSpan)
                        {
                            this.Cursor = Cursors.ScrollWE; //2010.04.04添加
                            IsLineYCursor2 = true;
                        }
                        else
                        {
                            if (Math.Abs(_startPoint.X - lineYCursor2.X1) > _choosedSpan && Math.Abs(_startPoint.Y - lineXCursor2.Y1) <= _choosedSpan)
                            {
                                this.Cursor = Cursors.ScrollNS; //2010.04.04添加
                                IsLineXCursor2 = true;
                            }
                        }
                    }
                    if (_isCursor1 == true && _isCursor2 == true)
                    {
                        if (Math.Abs(_startPoint.X - lineYCursor1.X1) <= _choosedSpan
                            && Math.Abs(_startPoint.X - lineYCursor1.X1) < Math.Abs(_startPoint.Y - lineXCursor1.Y1)
                            && Math.Abs(_startPoint.X - lineYCursor1.X1) < Math.Abs(_startPoint.Y - lineXCursor2.Y1)
                            && Math.Abs(_startPoint.X - lineYCursor1.X1) < Math.Abs(_startPoint.X - lineYCursor2.X1))
                        {
                            this.Cursor = Cursors.ScrollWE; //2010.04.04添加
                            IsLineYCursor1 = true;
                        }
                        if (Math.Abs(_startPoint.X - lineYCursor2.X1) <= _choosedSpan
                            && Math.Abs(_startPoint.X - lineYCursor2.X1) < Math.Abs(_startPoint.Y - lineXCursor1.Y1)
                            && Math.Abs(_startPoint.X - lineYCursor2.X1) < Math.Abs(_startPoint.Y - lineXCursor2.Y1)
                            && Math.Abs(_startPoint.X - lineYCursor2.X1) < Math.Abs(_startPoint.X - lineYCursor1.X1))
                        {
                            this.Cursor = Cursors.ScrollWE; //2010.04.04添加
                            IsLineYCursor2 = true;
                        }
                        if (Math.Abs(_startPoint.X - lineYCursor1.X1) <= _choosedSpan
                            && Math.Abs(_startPoint.X - lineYCursor1.X1) < Math.Abs(_startPoint.Y - lineXCursor1.Y1)
                            && Math.Abs(_startPoint.X - lineYCursor1.X1) < Math.Abs(_startPoint.Y - lineXCursor2.Y1)
                            && Math.Abs(_startPoint.X - lineYCursor1.X1) == Math.Abs(_startPoint.X - lineYCursor2.X1))
                        {
                            this.Cursor = Cursors.ScrollWE; //2010.04.04添加
                            IsLineYCursor1 = true;
                        }
                        if (Math.Abs(_startPoint.Y - lineXCursor1.Y1) <= _choosedSpan
                            && Math.Abs(_startPoint.Y - lineXCursor1.Y1) < Math.Abs(_startPoint.X - lineYCursor1.X1)
                            && Math.Abs(_startPoint.Y - lineXCursor1.Y1) < Math.Abs(_startPoint.X - lineYCursor2.X1)
                            && Math.Abs(_startPoint.Y - lineXCursor1.Y1) < Math.Abs(_startPoint.Y - lineXCursor2.Y1))
                        {
                            this.Cursor = Cursors.ScrollNS; //2010.04.04添加
                            IsLineXCursor1 = true;
                        }
                        if (Math.Abs(_startPoint.Y - lineXCursor2.Y1) <= _choosedSpan
                            && Math.Abs(_startPoint.Y - lineXCursor2.Y1) < Math.Abs(_startPoint.X - lineYCursor1.X1)
                            && Math.Abs(_startPoint.Y - lineXCursor2.Y1) < Math.Abs(_startPoint.X - lineYCursor2.X1)
                            && Math.Abs(_startPoint.Y - lineXCursor2.Y1) < Math.Abs(_startPoint.Y - lineXCursor1.Y1))
                        {
                            this.Cursor = Cursors.ScrollNS; //2010.04.04添加
                            IsLineXCursor2 = true;
                        }
                        if (Math.Abs(_startPoint.Y - lineXCursor1.Y1) <= _choosedSpan
                            && Math.Abs(_startPoint.Y - lineXCursor1.Y1) < Math.Abs(_startPoint.X - lineYCursor1.X1)
                            && Math.Abs(_startPoint.Y - lineXCursor1.Y1) < Math.Abs(_startPoint.X - lineYCursor2.X1)
                            && Math.Abs(_startPoint.Y - lineXCursor1.Y1) == Math.Abs(_startPoint.Y - lineXCursor2.Y1))
                        {
                            this.Cursor = Cursors.ScrollNS; //2010.04.04添加
                            IsLineXCursor1 = true; 
                        }
                    }
                    break;
                case BtnMode.ZoomIn:
                    if (_startPoint.X >= 0 && _startPoint.Y >= 0 && _startPoint.X <= GridCurve.ActualWidth && _startPoint.Y <= GridCurve.ActualHeight)
                    {
                        ONorOFF = true;
                        this.Cursor = Cursors.Cross;
                    }
                    break;
                case BtnMode.ManualScroll:
                    //手动拖动曲线画面的按下事件
                    this.Cursor = Cursors.ScrollAll;
                    _axisminTime = _xAxis.MinValue;
                    _axismaxTime = _xAxis.MaxValue;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 鼠标左键点击事件(放大,滚动,拖动光标)
        /// </summary>
        private void GridCurve_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _endPoint = e.GetPosition(GridCurve);
            switch (_btnMode)
            {
                case BtnMode.ComeBack:
                    break;
                case BtnMode.Cursor:
                    //拖拽光标
                    if (IsLineYCursor1)
                    {
                        IsLineYCursor1 = false;
                    }
                    if (IsLineYCursor2)
                    {
                        IsLineYCursor2 = false;
                    }
                    if (IsLineXCursor1)
                    {
                        IsLineXCursor1 = false;
                    }
                    if (IsLineXCursor2)
                    {
                        IsLineXCursor2 = false;
                    }
                    this.Cursor = Cursors.Arrow;
                    break;
                case BtnMode.ZoomIn:
                    ONorOFF = false;
                    rectZoomIn.Visibility = Visibility.Collapsed;

                    
                    double maxX = 0;
                    double minX = 0;
                    double maxY = 0;
                    double minY = 1000000000;

                    #region  设置X轴
                    if (_endPoint.X < 0)
                    {
                        _endPoint.X = 0;
                    }
                    if (_endPoint.X > GridCurve.ActualWidth)
                    {
                        _endPoint.Y = GridCurve.ActualWidth;
                    }
                    if (_endPoint.Y < 0)
                    {
                        _endPoint.Y = 0;
                    }
                    if (_endPoint.Y > GridCurve.ActualHeight)
                    {
                        _endPoint.Y = GridCurve.ActualHeight;
                    }
                    _choosedDatas.Clear();
                    minX = _startPoint.X / GridCurve.ActualWidth * (_xAxis.MaxValue - _xAxis.MinValue) + _xAxis.MinValue;
                    maxX = _endPoint.X / GridCurve.ActualWidth * (_xAxis.MaxValue - _xAxis.MinValue) + _xAxis.MinValue;
                    
                    //_sliderMapping = false;
                    //if (minX > maxX)
                    //{
                    //    sliderMaxTime.Value = minX;
                    //    sliderMinTime.Value = maxX;
                    //}
                    //else
                    //{
                    //    sliderMaxTime.Value = maxX;
                    //    sliderMinTime.Value = minX;
                    //}
                    //_sliderMapping = true;
                    #endregion

                    #region 设定Y轴
                    //minY = (GridCurve.ActualHeight - _startPoint.Y) / GridCurve.ActualHeight;
                    //maxY = (GridCurve.ActualHeight - _endPoint.Y) / GridCurve.ActualHeight;

                    //if (minY > maxY)
                    //{
                    //    double tempData = minY;
                    //    minY = maxY;
                    //    maxY = tempData;
                    //}

                    //foreach (Axis axis in this._yAxises)
                    //{
                    //    double tempSpan = axis.MaxValue - axis.MinValue;
                    //    axis.MaxValue = maxY * tempSpan + axis.MinValue;
                    //    axis.MinValue = minY * tempSpan + axis.MinValue;
                    //}
                    //foreach (Curve cur in this._curves)
                    //{
                    //    double tempSpan = cur.MaxData - cur.MinData;
                    //    cur.MaxData = maxY * tempSpan + cur.MinData;
                    //    cur.MinData = minY * tempSpan + cur.MinData;
                    //    cur.Transform();
                    //}
                    #endregion

                    #region 显示全部点
                    bool b = false;
                    //判断可有线条高亮 b：true(选中) false(没选中)
                    foreach (Curve cur in this._curves)
                    {
                        if (cur.IsHighlight && cur.IsVisible)
                        {
                            b = true;
                            break;
                        }
                    }
                    //有线条高亮，将其作为主线
                    if (b)
                    {
                        for (int i = 0; i < this._curves.Count; i++)
                        {
                            if (this._curves[i].IsVisible && this._curves[i].IsHighlight)
                            {
                                for (int j = 0; j < this._curves[i].Points.Count; j++)
                                {
                                    if (_startPoint.X > _endPoint.X)
                                    {
                                        if (this._curves[i].Points[j].X >= _endPoint.X && this._curves[i].Points[j].X <= _startPoint.X)
                                        {
                                            _choosedDatas.Add(this._curves[i].Points[j].Y);
                                        }
                                    }
                                    else
                                    {
                                        if (this._curves[i].Points[j].X >= _startPoint.X && this._curves[i].Points[j].X <= _endPoint.X)
                                        {
                                            _choosedDatas.Add(this._curves[i].Points[j].Y);
                                        }
                                    }
                                }
                            }
                        }
                        if (_choosedDatas.Count >= 1)
                        {
                            b = false;
                            maxY = _choosedDatas.Max();
                            minY = _choosedDatas.Min();
                            if (maxY != minY)
                            {
                                double tempData = 0;
                                foreach (Curve cur in this._curves)
                                {
                                    tempData = (cur.MaxData - cur.MinData) / GridCurve.ActualHeight;
                                    cur.MinData = cur.MaxData - maxY * tempData;
                                    cur.MaxData = cur.MaxData - minY * tempData;
                                }
                                foreach (Axis axis in _yAxises)
                                {
                                    tempData = (axis.MaxValue - axis.MinValue) / GridCurve.ActualHeight;
                                    axis.MaxValue = axis.MaxValue - minY * tempData;
                                    axis.MinValue = axis.MaxValue - maxY * tempData;
                                }
                            }
                            if (minX > maxX)
                            {
                                sliderMaxTime.Value = minX;
                                sliderMinTime.Value = maxX;
                            }
                            else
                            {
                                sliderMaxTime.Value = maxX;
                                sliderMinTime.Value = minX;
                            }
                        }
                    }
                    else          //没有曲线高亮时 全部放大
                    {
                        for (int i = 0; i < this._curves.Count; i++)
                        {
                            for (int j = 0; j < this._curves[i].Points.Count; j++)
                            {
                                if (_startPoint.X > _endPoint.X)
                                {
                                    if (this._curves[i].Points[j].X >= _endPoint.X && this._curves[i].Points[j].X <= _startPoint.X)
                                    {
                                        _choosedDatas.Add(this._curves[i].Points[j].Y);
                                    }
                                }
                                else
                                {
                                    if (this._curves[i].Points[j].X >= _startPoint.X && this._curves[i].Points[j].X <= _endPoint.X)
                                    {
                                        _choosedDatas.Add(this._curves[i].Points[j].Y);
                                    }
                                }
                            }
                        }
                        if (_choosedDatas.Count >= 1)
                        {
                            b = false;
                            maxY = _choosedDatas.Max();
                            minY = _choosedDatas.Min();
                            if (maxY != minY)
                            {
                                double tempData = 0;
                                foreach (Curve cur in this._curves)
                                {
                                    tempData = (cur.MaxData - cur.MinData) / GridCurve.ActualHeight;
                                    cur.MinData = cur.MaxData - maxY * tempData;
                                    cur.MaxData = cur.MaxData - minY * tempData;

                                }
                                foreach (Axis axis in _yAxises)
                                {
                                    tempData = (axis.MaxValue - axis.MinValue) / GridCurve.ActualHeight;
                                    axis.MinValue = axis.MaxValue - maxY * tempData;
                                    axis.MaxValue = axis.MaxValue - minY * tempData;
                                }
                            }
                            if (minX > maxX)
                            {
                                sliderMaxTime.Value = minX;
                                sliderMinTime.Value = maxX;
                            }
                            else
                            {
                                sliderMaxTime.Value = maxX;
                                sliderMinTime.Value = minX;
                            }
                        }
                    }
                    #endregion

                    this.Cursor = Cursors.Arrow;
                    break;
                case BtnMode.ManualScroll:
                    this.Cursor = Cursors.Arrow;
                    //this.Cursor
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        private void GridCurve_MouseMove(object sender, MouseEventArgs e)
        {

            _endPoint = e.GetPosition(GridCurve);
            switch (_btnMode)
            {
                case BtnMode.ComeBack:
                    break;
                case BtnMode.Cursor:
                    if (IsLineYCursor1 == true)
                    {
                        for (int i = 0; i < _curves[_indexCursor1].Points.Count - 1; i++)
                        {
                            if (_endPoint.X > _curves[_indexCursor1].Points[i].X && _endPoint.X < _curves[_indexCursor1].Points[i + 1].X)
                            {
                                if (Math.Abs(_endPoint.X - _curves[_indexCursor1].Points[i].X) <= Math.Abs(_endPoint.X - _curves[_indexCursor1].Points[i + 1].X))
                                {
                                    _indexPoint1 = i;
                                    RefreshCursor(lineYCursor1, lineXCursor1, ellCursor1, tbkCursor1, _indexCursor1, _indexPoint1);
                                }
                                else
                                {
                                    _indexPoint1 = i + 1;
                                    RefreshCursor(lineYCursor1, lineXCursor1, ellCursor1, tbkCursor1, _indexCursor1, _indexPoint1);
                                }
                            }
                        }
                    }
                    if (IsLineYCursor2)
                    {
                        for (int i = 0; i < _curves[_indexCursor2].Points.Count - 1; i++)
                        {
                            if (_endPoint.X > _curves[_indexCursor2].Points[i].X && _endPoint.X < _curves[_indexCursor2].Points[i + 1].X)
                            {
                                if (Math.Abs(_endPoint.X - _curves[_indexCursor2].Points[i].X) <= Math.Abs(_endPoint.X - _curves[_indexCursor2].Points[i + 1].X))
                                {
                                    _indexPoint2 = i;
                                    RefreshCursor(lineYCursor2, lineXCursor2, ellCursor2, tbkCursor2, _indexCursor2, _indexPoint2);
                                }
                                else
                                {
                                    _indexPoint2 = i + 1;
                                    RefreshCursor(lineYCursor2, lineXCursor2, ellCursor2, tbkCursor2, _indexCursor2, _indexPoint2);
                                }
                            }
                        }

                    }
                    if (IsLineXCursor1)
                    {
                        _shortcut = Math.Abs(_endPoint.Y - this._curves[_indexCursor1].Points[_indexPoint1].Y);
                        for (int j = 0; j < _curves.Count; j++)
                        {
                            //08.09.03修改后
                            if (_curves[j].IsVisible)
                            {
                                if (_shortcut > Math.Abs(_endPoint.Y - this._curves[j].Points[_indexPoint1].Y))
                                {
                                    _shortcut = Math.Abs(_endPoint.Y - this._curves[j].Points[_indexPoint1].Y);
                                    _indexCursor1 = j;
                                }
                            }
                        }
                        RefreshCursor(lineYCursor1, lineXCursor1, ellCursor1, tbkCursor1, _indexCursor1, _indexPoint1);
                    }
                    if (IsLineXCursor2)
                    {
                        _shortcut = Math.Abs(_endPoint.Y - this._curves[_indexCursor2].Points[_indexPoint2].Y);
                        for (int j = 0; j < _curves.Count; j++)
                        {
                            //08.09.03修改后
                            if (_curves[j].IsVisible)
                            {
                                if (_shortcut > Math.Abs(_endPoint.Y - this._curves[j].Points[_indexPoint2].Y))
                                {
                                    _shortcut = Math.Abs(_endPoint.Y - this._curves[j].Points[_indexPoint2].Y);
                                    _indexCursor2 = j;
                                }
                            }
                        }
                        RefreshCursor(lineYCursor2, lineXCursor2, ellCursor2, tbkCursor2, _indexCursor2, _indexPoint2);
                    }
                    break;
                case BtnMode.ZoomIn:
                    if (ONorOFF)
                    {
                        if (_endPoint.X < 0)
                        {
                            _endPoint.X = 0;
                        }
                        if (_endPoint.X > GridCurve.ActualWidth)
                        {
                            _endPoint.Y = GridCurve.ActualWidth;
                        }
                        if (_endPoint.Y < 0)
                        {
                            _endPoint.Y = 0;
                        }
                        if (_endPoint.Y > GridCurve.ActualHeight)
                        {
                            _endPoint.Y = GridCurve.ActualHeight;
                        }
                        if (_endPoint != _startPoint)
                        {
                            if (_endPoint.X <= _startPoint.X && _endPoint.Y <= _startPoint.Y)
                            {
                                rectZoomIn.Visibility = Visibility.Visible;
                                rectZoomIn.Margin = new Thickness(_endPoint.X, _endPoint.Y, GridCurve.ActualWidth - _startPoint.X, GridCurve.ActualHeight - _startPoint.Y);
                            }
                            else
                            {
                                if (_endPoint.X <= _startPoint.X && _endPoint.Y >= _startPoint.Y)
                                {
                                    rectZoomIn.Visibility = Visibility.Visible;
                                    rectZoomIn.Margin = new Thickness(_endPoint.X, _startPoint.Y, GridCurve.ActualWidth - _startPoint.X, GridCurve.ActualHeight - _endPoint.Y);
                                }
                                else
                                {
                                    if (_endPoint.Y <= _startPoint.Y && _endPoint.X >= _startPoint.X)
                                    {
                                        rectZoomIn.Visibility = Visibility.Visible;
                                        rectZoomIn.Margin = new Thickness(_startPoint.X, _endPoint.Y, GridCurve.ActualWidth - _endPoint.X, GridCurve.ActualHeight - _startPoint.Y);
                                    }
                                    else
                                    {
                                        rectZoomIn.Visibility = Visibility.Visible;
                                        rectZoomIn.Margin = new Thickness(_startPoint.X, _startPoint.Y, GridCurve.ActualWidth - _endPoint.X, GridCurve.ActualHeight - _endPoint.Y);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case BtnMode.ManualScroll:
                    //手动拖移曲线画面的移动事件
                    if (this.Cursor == Cursors.ScrollAll)
                    {
                        if ((_endPoint.X - _startPoint.X) != 0)
                        {
                            double _tempSpan = (_xAxis.MaxValue - _xAxis.MinValue) * ((_endPoint.X - _startPoint.X) / GridCurve.ActualWidth);
                            _sliderMapping = false;
                            sliderMinTime.Value = _axisminTime - _tempSpan;
                            sliderMaxTime.Value = _axismaxTime - _tempSpan;
                            _sliderMapping = true;
                            foreach (Curve curve in _curves)
                            {
                                curve.MinTime = _xAxis.MinValue;
                                curve.MaxTime = _xAxis.MaxValue;
                                curve.Build();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 刷新曲线
        /// </summary>
        public void Refresh()
        {
            try
            {
                //foreach (Curve cur in _curves)
                //{
                //    //try
                //    //{
                //    //    double midVar = (double)_channels[cur.ChannelIndex].Value;
                //    //    cur.SourceData.Add(new MomentData((DateTime)_channels[cur.ChannelIndex].Time, midVar));
                //    //}
                //    //catch
                //    //{ 

                //    //}
                //    cur.AppendPoint();
                //}
                //foreach (AppendControl ac in _appendControls)
                //{
                //    ac.RefreshTextBlock();
                //}

                for (int i = 0; i < _curves.Count; i++)
                {
                    _curves[i].AppendPoint();
                }

                for (int i = 0; i < _appendControls.Count; i++)
                {
                    _appendControls[i].RefreshTextBlock();
                }

                this.GridBase.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(this.AutoMove));
            }
            catch
            { }
        }

        /// <summary>
        /// 使曲线时间轴达到或超过最大时，曲线自动移动
        /// </summary>
        public void AutoMove()
        {
            if (_btnMode == BtnMode.ComeBack)
            {
                double tempdata = 0;
                foreach (Curve curve in this._curves)
                {
                    if (curve.SourceData.Count >= 1)
                    {
                        if (tempdata < MomentData.GetTimeSpan(curve.SourceData, curve.SourceData.Count - 1).TotalSeconds)
                        {
                            tempdata = MomentData.GetTimeSpan(curve.SourceData, curve.SourceData.Count - 1).TotalSeconds;
                        }
                    }
                }
                if (tempdata != 0 && tempdata > _xAxis.MaxValue)
                {
                    double _moveSpan = _xAxis.MaxValue - _xAxis.MinValue;

                    if (this.sliderMaxTime.Maximum < (int)(tempdata + _moveSpan * _moveCoefficient))
                    {
                        this.sliderMaxTime.Maximum = (int)(tempdata + _moveSpan * _moveCoefficient);
                    }

                    this._sliderMapping = true;
                    this.sliderMaxTime.Value = (int)(tempdata + _moveSpan * _moveCoefficient);

                    if (_moveMode == MoveMode.Parallel)
                    {
                        this.sliderMinTime.Value = this.sliderMaxTime.Value - _moveSpan;
                    }
                }
            }
        }

        /// <summary>
        /// 创建光标点
        /// </summary>
        /// <param name="colline">纵线</param>
        /// <param name="rowline">横线</param>
        /// <param name="ellipse">光标对应的点</param>
        /// <param name="textBlock">显示当前光标点的值</param>
        /// <param name="curveIndex">当前光标点所在的曲线 _curves[curveIndex]</param>
        /// <param name="pointIndex">光标点在曲线的哪个点上 _curves[curveIndex].Points[pointIndex]</param>
        private void RefreshCursor(Line colline, Line rowline, Ellipse ellipse, TextBlock textBlock, int curveIndex, int pointIndex)
        {
            colline.Visibility = Visibility.Visible;
            rowline.Visibility = Visibility.Visible;
            textBlock.Visibility = Visibility.Visible;
            ellipse.Visibility = Visibility.Visible;

            colline.X1 = _curves[curveIndex].Points[pointIndex].X;
            colline.Y1 = 0;
            colline.X2 = _curves[curveIndex].Points[pointIndex].X;
            colline.Y2 = GridCurve.ActualHeight;

            rowline.X1 = 0;
            rowline.Y1 = _curves[curveIndex].Points[pointIndex].Y;
            rowline.X2 = GridCurve.ActualWidth;
            rowline.Y2 = _curves[curveIndex].Points[pointIndex].Y;

            ellipse.Margin = new Thickness(
                    _curves[curveIndex].Points[pointIndex].X - 2,
                    _curves[curveIndex].Points[pointIndex].Y - 2,
                    GridCurve.ActualWidth - _curves[curveIndex].Points[pointIndex].X - 2,
                    GridCurve.ActualHeight - _curves[curveIndex].Points[pointIndex].Y - 2);
            if ((GridCurve.ActualWidth - _curves[curveIndex].Points[pointIndex].X) >= 160)
            {
                textBlock.TextAlignment = TextAlignment.Left;
                if ((GridCurve.ActualHeight - _curves[curveIndex].Points[pointIndex].Y) >= 40)
                {
                    textBlock.Margin = new Thickness(
                            _curves[curveIndex].Points[pointIndex].X,
                            _curves[curveIndex].Points[pointIndex].Y,
                            GridCurve.ActualWidth - _curves[curveIndex].Points[pointIndex].X - 160,
                            GridCurve.ActualHeight - _curves[curveIndex].Points[pointIndex].Y - 40);
                }
                else
                {
                    textBlock.Margin = new Thickness(
                            _curves[curveIndex].Points[pointIndex].X,
                            _curves[curveIndex].Points[pointIndex].Y - 40,
                            GridCurve.ActualWidth - _curves[curveIndex].Points[pointIndex].X - 160,
                            GridCurve.ActualHeight - _curves[curveIndex].Points[pointIndex].Y);
                }
            }
            else
            {
                textBlock.TextAlignment = TextAlignment.Right;
                if ((GridCurve.ActualHeight - _curves[curveIndex].Points[pointIndex].Y) >= 40)
                {
                    textBlock.Margin = new Thickness(
                            _curves[curveIndex].Points[pointIndex].X - 160,
                            _curves[curveIndex].Points[pointIndex].Y,
                            GridCurve.ActualWidth - _curves[curveIndex].Points[pointIndex].X,
                            GridCurve.ActualHeight - _curves[curveIndex].Points[pointIndex].Y - 40);
                }
                else
                {
                    textBlock.Margin = new Thickness(
                            _curves[curveIndex].Points[pointIndex].X - 160,
                            _curves[curveIndex].Points[pointIndex].Y - 40,
                            GridCurve.ActualWidth - _curves[curveIndex].Points[pointIndex].X,
                            GridCurve.ActualHeight - _curves[curveIndex].Points[pointIndex].Y);
                }
            }
            textBlock.Text = _curves[curveIndex].Name + "\n" +
                    TimeSpan.FromSeconds(Convert.ToInt32(MomentData.GetTimeSpan(_curves[curveIndex].SourceData, pointIndex).TotalSeconds)).ToString()
                    + "\n" + _curves[curveIndex].SourceData[pointIndex].Value + " " + _curves[curveIndex].DataUnits;
            if (_isCursor1 == true && _isCursor2 == true && _indexCursor1 == _indexCursor2)
            {
                tbkCursorSpan.Visibility = Visibility.Visible;
                for (int i = 0; i < _curves[_indexCursor1].Points.Count; i++)
                {
                    if (this._curves[_indexCursor1].Points[i].X == lineYCursor1.X1)
                    {
                        _indexPoint1 = i;
                    }
                    if (this._curves[_indexCursor1].Points[i].X == lineYCursor2.X1)
                    {
                        _indexPoint2 = i;
                    }
                }
                if (_indexPoint1 >= _indexPoint2)
                {
                    tbkCursorSpan.Text = (this._curves[_indexCursor1].SourceData[_indexPoint1].Time - this._curves[_indexCursor1].SourceData[_indexPoint2].Time).Hours.ToString()
                            + ":" + (this._curves[_indexCursor1].SourceData[_indexPoint1].Time - this._curves[_indexCursor1].SourceData[_indexPoint2].Time).Minutes.ToString()
                            + ":" + (this._curves[_indexCursor1].SourceData[_indexPoint1].Time - this._curves[_indexCursor1].SourceData[_indexPoint2].Time).Seconds.ToString()
                            + " " + string.Format("{0:f}", (Math.Abs(this._curves[_indexCursor1].SourceData[_indexPoint1].Value - this._curves[_indexCursor1].SourceData[_indexPoint2].Value)));
                }
                else
                {
                    tbkCursorSpan.Text = (this._curves[_indexCursor1].SourceData[_indexPoint2].Time - this._curves[_indexCursor1].SourceData[_indexPoint1].Time).Hours.ToString()
                            + ":" + (this._curves[_indexCursor1].SourceData[_indexPoint2].Time - this._curves[_indexCursor1].SourceData[_indexPoint1].Time).Minutes.ToString()
                            + ":" + (this._curves[_indexCursor1].SourceData[_indexPoint2].Time - this._curves[_indexCursor1].SourceData[_indexPoint1].Time).Seconds.ToString()
                            + " " + string.Format("{0:f}", (Math.Abs(this._curves[_indexCursor1].SourceData[_indexPoint2].Value - this._curves[_indexCursor1].SourceData[_indexPoint1].Value)));
                }
            }
            else
            {
                tbkCursorSpan.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 改变语言
        /// </summary>
        /// <param name="str">语言的种类 en-US:英语  zh-CN:中文</param>
        public void ChangeLanguage(string languageCode)
        {
            if (languageCode == "en-US" || languageCode == "zh-CN")
            {
                if (string.Equals(languageCode, "en-US"))
                {
                    this.Resources = _resources[0];
                    _xAxis.Unit = "Time:Minute";
                    _language = "en-US";
                }
                if (string.Equals(languageCode, "zh-CN"))
                {
                    this.Resources = _resources[1];
                    _xAxis.Unit = "时间:分钟";
                    _language = "zh-CN";
                }
                foreach (AppendControl ac in _appendControls)
                {
                    ac.Curve.Name = this._channels[ac.Curve.ChannelIndex].Name;
                    ac.CheckBox.Content = ac.Curve.Name;
                }
                //重新生成ChannelMapping和ChannelUnits
                if (this._channels != null)
                {
                    _channelMapping.Clear();
                    _channelUnits.Clear();
                    if (this._currentUICulture.Name.Equals("zh-CN"))
                    {
                        _channelMapping.Add("<全部>", new List<string>());
                    }
                    else
                    {
                        _channelMapping.Add("<ALL>", new List<string>());
                    }
                    foreach (Channel channel in this._channels.Values)
                    {
                        if (!_channelUnits.Keys.Contains(channel.Unit))
                        {
                            _channelUnits.Add(channel.Unit, channel.UnitDescription);
                        }
                        if (_channelMapping.Keys.Contains(channel.Type))
                        {
                            _channelMapping[channel.Type].Add(channel.Name);
                        }
                        else
                        {
                            _channelMapping.Add(channel.Type, new List<string>());
                            _channelMapping[channel.Type].Add(channel.Name);
                        }
                        _channelMapping.First().Value.Add(channel.Name);
                    }
                    _channelMapping.Add("", new List<string>());
                    string tempUnit;
                    foreach (Axis axis in this._yAxises)
                    {
                        tempUnit = axis.Unit.Split(' ').ElementAt(2);
                        axis.Unit = _channelUnits[tempUnit] + " [ " + tempUnit + " ]";
                    }
                }
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 给曲线添加附加控件 显示隐藏(CheckBox) 高亮(Button) 最新数据(TextBlock)
        /// </summary>
        /// <param name="curves">一个曲线的List集合</param>
        public void CreateAppendControl(List<Curve> curves)
        {
            if (_appendControls.Count >= curves.Count)
            {
                int countspan = _appendControls.Count - curves.Count;
                for (int i = 0; i < countspan; i++)
                {
                    _appendControls[i].Delete();
                    _appendControls.RemoveAt(i);
                }
                for (int i = 0; i < curves.Count; i++)
                {
                    _appendControls[i].Curve = curves[i];
                    _appendControls[i].CheckBox.Content = curves[i].Name;
                    _appendControls[i].CheckBox.Margin = new Thickness(0, 240 + i * 17, 83, 0);
                    _appendControls[i].CheckBox.IsChecked = true;
                    _appendControls[i].Button.Background = curves[i].OriginalColor;
                    _appendControls[i].Button.Margin = new Thickness(0, 239 + i * 17, 64, 0);
                    _appendControls[i].TextBlock.Margin = new Thickness(0, 240 + i * 17, 5, 0);
                    _appendControls[i].RefreshTextBlock();
                }
            }
            else
            {
                int _tempData = curves.Count - _appendControls.Count;
                for (int i = 0; i < _tempData; i++)
                {
                    AppendControl appendControl = new AppendControl(new CheckBox(), new Button(), new TextBlock(), GridBase);
                    appendControl.Display();
                    _appendControls.Add(appendControl);
                }
                for (int i = 0; i < curves.Count; i++)
                {
                    _appendControls[i].Curve = curves[i];
                    _appendControls[i].CheckBox.Content = curves[i].Name;
                    _appendControls[i].CheckBox.Margin = new Thickness(0, 240 + i * 17, 83, 0);
                    _appendControls[i].CheckBox.IsChecked = true;
                    _appendControls[i].Button.Background = curves[i].OriginalColor;
                    _appendControls[i].Button.Margin = new Thickness(0, 239 + i * 17, 64, 0);
                    _appendControls[i].TextBlock.Margin = new Thickness(0, 240 + i * 17, 5, 0);
                    _appendControls[i].RefreshTextBlock();
                }
            }
        }

        //********************************************************************************************************
        //属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性
        //********************************************************************************************************

        /// <summary>
        /// 控制GridCurve中的网格
        /// </summary>
        //public Gridding Gridding
        //{
        //    get { return _gridding; }
        //    set { _gridding = value; }
        //}

        /// <summary>
        /// 接收通道数据
        /// </summary>
        public Dictionary<int, Channel> Channels
        {
            get { return _channels; }
            set
            {
                _channels = value;
                string assemPath = Assembly.GetExecutingAssembly().Location;
                string currentDic = assemPath.Substring(0, assemPath.LastIndexOf("\\") + 1);
                try
                {
                    #region
                    //if (!_bool)
                    //{
                    //    _bool = true;
                        //生成ChannelMapping和ChannelUnits
                        _channelMapping.Clear();
                        _channelUnits.Clear();
                        if (this._currentUICulture.Name.Equals("zh-CN"))
                        {
                            _channelMapping.Add("<全部>", new List<string>());
                        }
                        else
                        {
                            _channelMapping.Add("<ALL>", new List<string>());
                        }
                        foreach (Channel channel in this._channels.Values)
                        {
                            //if (channel.DeviceID != 3)
                            //{
                                if (!_channelUnits.Keys.Contains(channel.Unit))
                                {
                                    _channelUnits.Add(channel.Unit, channel.UnitDescription);
                                }
                                if (_channelMapping.Keys.Contains(channel.Type))
                                {
                                    _channelMapping[channel.Type].Add(channel.Name);
                                }
                                else
                                {
                                    _channelMapping.Add(channel.Type, new List<string>());
                                    _channelMapping[channel.Type].Add(channel.Name);
                                }
                                _channelMapping.First().Value.Add(channel.Name);
                            //}
                        }
                        _channelMapping.Add("", new List<string>());
                        //读取前次的配置文件
                        if (File.Exists(currentDic + "monitors/" + this.Name + "_" + this.TestType))
                        {  
                            List<string> _fileDatas = new List<string>();
                            StreamReader _reader = File.OpenText(currentDic + "monitors/" + this.Name + "_" + this.TestType);
                            string str = _reader.ReadLine();
                            while (str != null)
                            {
                                _fileDatas.Add(str);
                                str = _reader.ReadLine();
                            }
                            _reader.Close();


                            //恢复键的数据
                            this._comeBackDatas.Clear();
                            this._comeBackDatas.Add(int.Parse(_fileDatas[0]));
                            this._comeBackDatas.Add(int.Parse(_fileDatas[1]));
                            //X轴
                            sliderMaxTime.Value = int.Parse(_fileDatas[1].ToString()) * 60;
                            sliderMinTime.Value = int.Parse(_fileDatas[0].ToString()) * 60;

                            //Y轴
                            foreach (Axis axis in this._yAxises)
                            {
                                axis.IsVisibility = false;
                            }
                            this._yAxises.Clear();

                            var q = from u in this._channelUnits
                                    orderby u.Key
                                    select u;
                            Dictionary<string, string> tempDictionary = q.ToDictionary(pair => pair.Key, pair => pair.Value);

                            int count = 0;
                            for (int i = 0; i < tempDictionary.Count; i++)
                            {
                                if (bool.Parse(_fileDatas[2 + i * 4]))
                                {
                                    Axis axis = new Axis(12,
                                                        double.Parse(_fileDatas[3 + i * 4]),
                                                        double.Parse(_fileDatas[4 + i * 4]),
                                                        tempDictionary.Values.ElementAt(i) + " [ " + tempDictionary.Keys.ElementAt(i) + " ]",
                                                        new Point(50 * count, 13),
                                                        new Point(50 * count + 50, this.GridBase.ActualHeight - 31),
                                                        AxisMode.Vertical,
                                                        new SolidColorBrush((Color)ColorConverter.ConvertFromString(_fileDatas[5 + i * 4])),
                                                        this.GridBase);
                                    count += 1;
                                    this._comeBackDatas.Add(axis.MinValue);
                                    this._comeBackDatas.Add(axis.MaxValue);
                                    this._yAxises.Add(axis);
                                    axis.IsVisibility = true;
                                }
                            }

                            //for (int i = 0; i < _channelUnits.Count; i++)
                            //{
                            //    if (bool.Parse(_fileDatas[2 + i * 4]))
                            //    {
                            //        Axis axis = new Axis(12,
                            //                            double.Parse(_fileDatas[3 + i * 4]),
                            //                            double.Parse(_fileDatas[4 + i * 4]),
                            //                            _channelUnits.Values.ElementAt(i) + " [ " + _channelUnits.Keys.ElementAt(i) + " ]",
                            //                            new Point(50 * i, 13),
                            //                            new Point(50 * i + 50, this.GridBase.ActualHeight - 31),
                            //                            AxisMode.Vertical,
                            //                            new SolidColorBrush((Color)ColorConverter.ConvertFromString(_fileDatas[5 + i * 4])),
                            //                            this.GridBase);
                            //        this._comeBackDatas.Add(axis.MinValue);
                            //        this._comeBackDatas.Add(axis.MaxValue);
                            //        this._yAxises.Add(axis);
                            //        axis.IsVisibility = true;
                            //    }
                            //}
                            
                            if (this._yAxises.Count > 0)
                            {
                                this.GridCurveLeft = 50 * this._yAxises.Count;
                            }
                            else
                            {
                                this.GridCurveLeft = 5;
                            }
                            //曲线
                            foreach (Curve curve in this._curves)
                            {
                                curve.IsVisible = false;
                            }
                            this._curves.Clear();
                            string channelName;
                            bool _istrue = false;
                            for (int i = 0; i < 20; i++)
                            {
                                if (_fileDatas[_channelUnits.Count * 4 + 2 + i * 3] != (_channelMapping.Count - 1).ToString())
                                {
                                    _istrue = false;
                                    channelName = _channelMapping[_channelMapping.Keys.ElementAt(int.Parse(_fileDatas[_channelUnits.Count * 4 + 2 + i * 3]))].ElementAt(int.Parse(_fileDatas[_channelUnits.Count * 4 + 3 + i * 3]));
                                    foreach (int _channelKey in this._channels.Keys)
                                    {
                                        if (channelName.Equals(this._channels[_channelKey].Name))
                                        {
                                            foreach (Axis axis in this._yAxises)
                                            {
                                                if (string.Equals(this._channels[_channelKey].Unit, axis.Unit.Split(' ').ElementAt(2)))
                                                {
                                                    _istrue = true;
                                                    Curve curve = new Curve();
                                                    curve.Name = this._channels[_channelKey].Name;
                                                    curve.DataUnits = this._channels[_channelKey].Unit;
                                                    curve.SourceData = this._channels[_channelKey].DataList;
                                                    curve.OriginalColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_fileDatas[_channelUnits.Count * 4 + 4 + i * 3]));
                                                    curve.MaxTime = this._xAxis.MaxValue;
                                                    curve.MinTime = this._xAxis.MinValue;
                                                    curve.MaxData = axis.MaxValue;
                                                    curve.MinData = axis.MinValue;
                                                    curve.ChannelIndex = _channelKey;
                                                    curve.Grid = this.GridCurve;
                                                    curve.IsVisible = true;
                                                    curve.Build();
                                                    this._curves.Add(curve);
                                                    break;
                                                }
                                            }
                                            if (!_istrue)
                                            {
                                                Curve cur = new Curve();
                                                cur.Name = this._channels[_channelKey].Name;
                                                cur.DataUnits = this._channels[_channelKey].Unit;
                                                cur.SourceData = this._channels[_channelKey].DataList;
                                                cur.OriginalColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_fileDatas[_channelUnits.Count * 4 + 4 + i * 3]));
                                                cur.MaxTime = this._xAxis.MaxValue;
                                                cur.MinTime = this._xAxis.MinValue;
                                                cur.MaxData = this._channels[_channelKey].Maximum;
                                                cur.MinData = this._channels[_channelKey].Minimum;
                                                cur.ChannelIndex = _channelKey;
                                                cur.Grid = this.GridCurve;
                                                cur.IsVisible = true;
                                                cur.Build();
                                                this._curves.Add(cur);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            this.CreateAppendControl(this._curves);
                        }
                    //}
                    #endregion
                }
                catch
                {
                    //if (File.Exists(currentDic + "monitors/" + this.Name + "_" + this.TestType))
                    //{
                    //    File.Delete(currentDic + "monitors/" + this.Name + "_" + this.TestType);
                    //}
                    //if (!_bool)
                    //{
                    //    _bool = true;
                        //生成ChannelMapping和ChannelUnits
                        _channelMapping.Clear();
                        _channelUnits.Clear();
                        if (this._currentUICulture.Name.Equals("zh-CN"))
                        {
                            _channelMapping.Add("<全部>", new List<string>());
                        }
                        else
                        {
                            _channelMapping.Add("<ALL>", new List<string>());
                        }
                        foreach (Channel channel in this._channels.Values)
                        {
                            if (!_channelUnits.Keys.Contains(channel.Unit))
                            {
                                _channelUnits.Add(channel.Unit, channel.UnitDescription);
                            }
                            if (_channelMapping.Keys.Contains(channel.Type))
                            {
                                _channelMapping[channel.Type].Add(channel.Name);
                            }
                            else
                            {
                                _channelMapping.Add(channel.Type, new List<string>());
                                _channelMapping[channel.Type].Add(channel.Name);
                            }
                            _channelMapping.First().Value.Add(channel.Name);
                        }
                        _channelMapping.Add("", new List<string>());
                    //}
                }
            }
        }
       
        /// <summary>
        /// 曲线集合
        /// </summary>
        public List<Curve> Curves
        {
            get { return _curves; }
            set { _curves = value; }
        }

        /// <summary>
        /// Y轴集合
        /// </summary>
        public List<Axis> YAxises
        {
            get { return _yAxises; }
            set { _yAxises = value; }
        }
    
        /// <summary>
        /// 记录通道信息 keys中保存通道分类(Items),每个key对应的List<string>中保存该key类下所有的通道名
        /// </summary>
        public Dictionary<string, List<string>> ChannelMapping
        {
            get { return _channelMapping; }
        }

        /// <summary>
        /// 所有Y轴对应的单位 国际标准符号,单位描述
        /// </summary>
        public Dictionary<string, string> ChannelUnits
        {
            get { return _channelUnits; }
        }

        /// <summary>
        /// 时间轴
        /// </summary>
        public Axis XAxis
        {
            get { return _xAxis; }
            set { _xAxis = value; }
        }

        /// <summary>
        /// 设置GridCurve的Margin.Left
        /// </summary>
        public double GridCurveLeft
        {
            get { return _gridCurveLeft; }
            set
            {
                if (_gridCurveLeft != value)
                {
                    _gridCurveLeft = value;
                    GridCurve.Margin = new Thickness(_gridCurveLeft, _gridCurveTop, _gridCurveRight, _gridCurveBottom);
                    downY.Margin = new Thickness(_gridCurveLeft, 1, GridBase.ActualWidth - _gridCurveLeft - 24, GridBase.ActualHeight - 21);
                    upY.Margin = new Thickness(_gridCurveLeft + 26, 1, GridBase.ActualWidth - _gridCurveLeft - 50, GridBase.ActualHeight - 21);
                    downXY.Margin = new Thickness(_gridCurveLeft, GridBase.ActualHeight - 23, GridBase.ActualWidth - _gridCurveLeft - 24, 3);
                    upXY.Margin = new Thickness(_gridCurveLeft + 26, GridBase.ActualHeight - 23, GridBase.ActualWidth - _gridCurveLeft - 50, 3);
                    _xAxis.StartPoint = new Point(_gridCurveLeft - 5, GridBase.ActualHeight - 38);
                    tbkCursorSpan.Margin = new Thickness(_gridCurveLeft + 80, GridBase.ActualHeight - 24, GridBase.ActualWidth - _gridCurveLeft - 160, 3);
                }
            }
        }

        /// <summary>
        /// 设置GridCurve的Margin.Top
        /// </summary>
        public double GridCurveTop
        {
            get { return _gridCurveTop; }
            set
            {
                _gridCurveTop = value;
                GridCurve.Margin = new Thickness(_gridCurveLeft, _gridCurveTop, _gridCurveRight, _gridCurveBottom);
            }
        }

        /// <summary>
        /// 设置GridCurve的Margin.Right
        /// </summary>
        public double GridCurveRight
        {
            get { return _gridCurveRight; }
            set
            {
                _gridCurveRight = value;
                GridCurve.Margin = new Thickness(_gridCurveLeft, _gridCurveTop, _gridCurveRight, _gridCurveBottom);
            }
        }

        /// <summary>
        /// 设置GridCurve的Margin.Bottom
        /// </summary>
        public double GridCurveBottom
        {
            get { return _gridCurveBottom; }
            set
            {
                _gridCurveBottom = value;
                GridCurve.Margin = new Thickness(_gridCurveLeft, _gridCurveTop, _gridCurveRight, _gridCurveBottom);
            }
        }

        /// <summary>
        /// 语言  en-US:英语  zh-CN:中文
        /// </summary>
        public string MonitorLanguage
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    this.ChangeLanguage(_language);
                }
            }
        }

        /// <summary>
        /// 当前语言 只支持中文(zh-CN)和英文(en-US)
        /// </summary>
        public CultureInfo CurrentUICulture
        {
            get { return _currentUICulture; }
            set 
            {  
                if (_currentUICulture != value)
                {
                    _currentUICulture = value;
                    this.ChangeLanguage(_currentUICulture.Name);
                }
            }
        }

        /// <summary>
        /// 恢复键需要用到的数据
        /// </summary>
        public List<double> ComeBackDatas
        {
            get { return _comeBackDatas; }
            set
            {
                _comeBackDatas = value;
            }
        }

        /// <summary>
        /// 曲线每次放大缩小的系数,不能小于0 应用于"+" "-"按钮
        /// </summary>
        public double ZoomCoefficient
        {
            get { return _zoomCoefficient; }
            set
            {
                if (value >= 0)
                {
                    _zoomCoefficient = value;
                }
                else
                {
                    MessageBox.Show("曲线缩放系数设置有误,不能小于“0”");
                }
            }
        }

        /// <summary>
        /// 曲线自动移动的系数
        /// </summary>
        public double MoveCoefficient
        {
            get { return _moveCoefficient; }
            set { _moveCoefficient = value; }
        }

        /// <summary>
        /// 曲线自动移动的方式 有平移和压缩两种方式
        /// </summary>
        public MoveMode CurveMoveMode
        {
            get { return _moveMode; }
            set { _moveMode = value; }
        }

        /// <summary>
        /// 用于确定滑轴是否对曲线起作用
        /// </summary>
        public bool SliderMapping
        {
            get { return _sliderMapping; }
            set { _sliderMapping = value; }
        }
    }
}
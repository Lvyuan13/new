using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace SHHS.UILabs.RealtimeCurves
{
    public class Axis
    {
        private byte _labelsNumber = 12;                //轴的标签个数
        private double _maxValue = 100;                //轴的最大值
        private double _minValue = 0;                   //轴的最小值
        private string _unit = "";                     //轴的单位
        private Point _startPoint;                      //轴的起点(左上角)
        private Point _endPoint;                        //轴的终点(右下角)
        private AxisMode _axisMode = AxisMode.Vertical; //轴的显示方式
        private bool _isVisibility = false;             //轴是否显示
        private SolidColorBrush _labelsFontColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));     //标签中字体颜色
        private List<TextBlock> _labels = new List<TextBlock>();    //轴的标签集合
        private double _labelHeight = 18;    //单个标签高度
        private double _labelWidth = 50;     //单个标签宽度
        private Grid _grid;             //轴的显示平台
        private double _locationSpan;   //标签之间的间距   
        private double _valueSpan;      //各个标签值之间的差值

        /// <summary>
        /// Axis的构造方法(无参数的)
        /// </summary>
        public Axis()
        {
        }

        /// <summary>
        /// Axis构造方法(有参数的)
        /// </summary>
        /// <param name="labelsNumber">标签数量[3-126]</param>
        /// <param name="maxValue">轴的最大值</param>
        /// <param name="minValue">轴的最小值</param>
        /// <param name="units">轴的单位</param>
        /// <param name="startPoint">轴的起点(左上角)</param>
        /// <param name="endPoint">轴的终点(右下角)</param>
        /// <param name="axisMode">轴的显示方式(垂直、水平两种,默认是垂直)</param>
        /// <param name="labelsFontColor">标签的字体颜色</param>
        /// <param name="grid">轴的显示平台</param>
        public Axis(byte labelsNumber, double maxValue, double minValue, string units, Point startPoint, Point endPoint, AxisMode axisMode, SolidColorBrush labelsFontColor, Grid grid)
        {
            this._labelsNumber = labelsNumber;
            this._maxValue = maxValue;
            this._minValue = minValue;
            this._unit = units;
            this._startPoint = startPoint;
            this._endPoint = endPoint;
            this._axisMode = axisMode;
            this._labelsFontColor = labelsFontColor;
            this._grid = grid;
        }

        /// <summary>
        /// 刷新标签的位置
        /// </summary>
        public void RefreshLocation()
        {
            if (_axisMode == AxisMode.Vertical)
            {
                _locationSpan = (_endPoint.Y - _startPoint.Y - _labelHeight * (_labels.Count - 1)) / (_labels.Count - 2) + _labelHeight;
                for (int i = 0; i < _labels.Count; i++)
                {
                    if (i != _labels.Count - 1)
                    {
                        _labels[i].Margin = new Thickness(_startPoint.X, _endPoint.Y - _labels[i].Height - _locationSpan * i, _grid.ActualWidth - _endPoint.X, _grid.ActualHeight - _endPoint.Y + _locationSpan * i);
                    }
                    else
                    {
                        _labels[i].Margin = new Thickness(_startPoint.X, _endPoint.Y - (_endPoint.Y - _startPoint.Y) * 0.4, _grid.ActualWidth - _endPoint.X - 100, _grid.ActualHeight - _endPoint.Y + (_endPoint.Y - _startPoint.Y) * 0.4 - _labelHeight);
                    }
                }
            }
            else
            {
                _locationSpan = (_endPoint.X - _startPoint.X - _labelWidth * (_labels.Count - 1)) / (_labels.Count - 2) + _labelWidth;
                for (int i = 0; i < _labels.Count; i++)
                {
                    if (i != _labels.Count - 1)
                    {
                        _labels[i].Margin = new Thickness(_startPoint.X + _locationSpan * i, _startPoint.Y, _grid.ActualWidth - _startPoint.X - _locationSpan * i - _labelWidth, _grid.ActualHeight - _endPoint.Y);
                    }
                    else
                    {
                        _labels[i].Margin = new Thickness(_startPoint.X + (_endPoint.X - _startPoint.X) * 0.6, _startPoint.Y + 20, _grid.ActualWidth - _startPoint.X - (_endPoint.X - _startPoint.X) * 0.6 - 90, _grid.ActualHeight - _endPoint.Y - 20);
                    }
                }
            }
        }

        /// <summary>
        /// 刷新标签的值
        /// </summary>
        public void RefreshValue()
        {
            if (this._axisMode == AxisMode.Vertical)
            {
                _valueSpan = (_maxValue - _minValue) / (_labels.Count - 2);
                for (int i = 0; i < _labels.Count - 1; i++)
                {
                    if (_valueSpan >= 1)
                    {
                        _labels[i].Text = ((int)(_minValue + _valueSpan * i)).ToString();
                    }
                    else
                    {
                        if (_valueSpan >= 0.1)
                        {
                            _labels[i].Text = string.Format("{0:f1}", _minValue + _valueSpan * i);
                        }
                        else
                        {
                            if (_valueSpan >= 0.01)
                            {
                                _labels[i].Text = string.Format("{0:f2}", _minValue + _valueSpan * i);
                            }
                            else
                            {
                                if (_valueSpan >= 0.001)
                                {
                                    _labels[i].Text = string.Format("{0:f3}", _minValue + _valueSpan * i);
                                }
                                else
                                {
                                    if (_valueSpan >= 0.0001)
                                    {
                                        _labels[i].Text = string.Format("{0:f4}", _minValue + _valueSpan * i);
                                    }
                                    else
                                    {
                                        _labels[i].Text = string.Format("{0:G}", _minValue + _valueSpan * i);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (this._axisMode == AxisMode.Level)
            {
                _valueSpan = (_maxValue - _minValue) / 60 / (_labels.Count - 2);

                for (int i = 0; i < _labels.Count - 1; i++)
                {
                    if (_valueSpan >= 1)
                    {
                        _labels[i].Text = ((int)(_minValue / 60 + _valueSpan * i)).ToString();
                    }
                    else
                    {
                        if (_valueSpan >= 0.1)
                        {
                            _labels[i].Text = string.Format("{0:f1}", _minValue / 60 + _valueSpan * i);
                        }
                        else
                        {
                            if (_valueSpan >= 0.01)
                            {
                                _labels[i].Text = string.Format("{0:f2}", _minValue / 60 + _valueSpan * i);
                            }
                            else
                            {
                                if (_valueSpan >= 0.001)
                                {
                                    _labels[i].Text = string.Format("{0:f3}", _minValue / 60 + _valueSpan * i);
                                }
                                else
                                {
                                    if (_valueSpan >= 0.0001)
                                    {
                                        _labels[i].Text = string.Format("{0:f4}", _minValue / 60 + _valueSpan * i);
                                    }
                                    else
                                    {
                                        _labels[i].Text = string.Format("{0:G}", _minValue / 60 + _valueSpan * i);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 轴的显示
        /// </summary>
        private void DisplayAxis()
        {
            foreach (TextBlock tb in _labels)
            {
                _grid.Children.Remove(tb);
            }
            _labels.Clear();

            //轴的显示方式为“垂直”时的方法
            if (_axisMode == AxisMode.Vertical)
            {
                _locationSpan = (_endPoint.Y - _startPoint.Y - _labelHeight * (_labelsNumber - 1)) / (_labelsNumber - 2) + _labelHeight;
                for (int i = 0; i < _labelsNumber; i++)
                {
                    TextBlock tb = new TextBlock();
                    tb.Height = _labelHeight;
                    tb.Foreground = _labelsFontColor;
                    if (i != _labelsNumber - 1)
                    {
                        tb.Width = _endPoint.X - _startPoint.X;
                        tb.TextAlignment = TextAlignment.Right;
                        //tb.Text = (_minValue + (_maxValue - _minValue) / (_labelsNumber - 2) * i).ToString();
                        tb.Margin = new Thickness(_startPoint.X, _endPoint.Y - _labelHeight - _locationSpan * i, _grid.ActualWidth - _endPoint.X, _grid.ActualHeight - _endPoint.Y + _locationSpan * i);
                    }
                    else
                    {
                        tb.Width = 100;
                        tb.Text = _unit;
                        tb.RenderTransform = new RotateTransform(-90);
                        tb.Margin = new Thickness(_startPoint.X, _endPoint.Y - (_endPoint.Y - _startPoint.Y) * 0.4, _grid.ActualWidth - _endPoint.X, _grid.ActualHeight - _endPoint.Y + (_endPoint.Y - _startPoint.Y) * 0.4 - _labelHeight);

                    }
                    _labels.Add(tb);
                    _grid.Children.Add(_labels[i]);
                }
            }
            else   //轴的显示方式为“水平”时的方法
            {
                _locationSpan = (_endPoint.X - _startPoint.X - 45 * (_labelsNumber - 1)) / (_labelsNumber - 2) + 45;
                for (int i = 0; i < _labelsNumber; i++)
                {
                    TextBlock tb = new TextBlock();
                    tb.Height = _labelHeight;
                    tb.Width = _labelWidth;
                    tb.Foreground = _labelsFontColor;
                    if (i != _labelsNumber - 1)
                    {
                        //tb.Text = (_minValue + (_maxValue - _minValue) / (_labelsNumber - 2) * i).ToString();
                        tb.Margin = new Thickness(_startPoint.X + _locationSpan * i, _startPoint.Y, _grid.ActualWidth - _startPoint.X - _locationSpan * i - _labelWidth, _grid.ActualHeight - _endPoint.Y);

                    }
                    else
                    {
                        tb.Text = _unit;
                        tb.Margin = new Thickness(_startPoint.X + (_endPoint.X - _startPoint.X) * 0.6, _startPoint.Y + 20, _grid.ActualWidth - _startPoint.X - (_endPoint.X - _startPoint.X) * 0.6 - 70, _grid.ActualHeight - _endPoint.Y - 20);
                    }
                    _labels.Add(tb);
                    _grid.Children.Add(_labels[i]);
                }
            }
            RefreshValue();
        }

        //********************************************************************************************************
        //属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性
        //********************************************************************************************************
        /// <summary>
        /// 轴的显示与隐藏  true:显示  false:隐藏
        /// </summary>
        public bool IsVisibility
        {
            get { return _isVisibility; }
            set
            {
                if (_isVisibility != value)
                {
                    _isVisibility = value;
                    if (_isVisibility)
                    {
                        //轴的显示方式为“垂直”时的方法
                        if (_axisMode == AxisMode.Vertical)
                        {
                            _locationSpan = (_endPoint.Y - _startPoint.Y - _labelHeight * (_labelsNumber - 1)) / (_labelsNumber - 2) + _labelHeight;
                            for (int i = 0; i < _labelsNumber; i++)
                            {
                                TextBlock tb = new TextBlock();
                                tb.Height = _labelHeight;
                                tb.Foreground = _labelsFontColor;
                                if (i != _labelsNumber - 1)
                                {
                                    tb.Width = _endPoint.X - _startPoint.X;
                                    tb.TextAlignment = TextAlignment.Right;
                                    tb.Margin = new Thickness(_startPoint.X, _endPoint.Y - _labelHeight - _locationSpan * i, _grid.ActualWidth - _endPoint.X, _grid.ActualHeight - _endPoint.Y + _locationSpan * i);
                                }
                                else
                                {
                                    tb.Width = 150;
                                    tb.Text = _unit;
                                    tb.RenderTransform = new RotateTransform(-90);
                                    tb.Margin = new Thickness(_startPoint.X, _endPoint.Y - (_endPoint.Y - _startPoint.Y) * 0.4, _grid.ActualWidth - _endPoint.X - 70, _grid.ActualHeight - _endPoint.Y + (_endPoint.Y - _startPoint.Y) * 0.4 - _labelHeight);

                                }
                                _labels.Add(tb);
                                _grid.Children.Add(_labels[i]);
                            }
                        }
                        else   //轴的显示方式为“水平”时的方法
                        {
                            _locationSpan = (_endPoint.X - _startPoint.X - 45 * (_labelsNumber - 1)) / (_labelsNumber - 2) + 45;
                            for (int i = 0; i < _labelsNumber; i++)
                            {
                                TextBlock tb = new TextBlock();
                                tb.Height = _labelHeight;
                                tb.Width = _labelWidth;
                                tb.Foreground = _labelsFontColor;
                                if (i != _labelsNumber - 1)
                                {
                                    tb.Margin = new Thickness(_startPoint.X + _locationSpan * i, _startPoint.Y, _grid.ActualWidth - _startPoint.X - _locationSpan * i - _labelWidth, _grid.ActualHeight - _endPoint.Y);

                                }
                                else
                                {
                                    tb.Text = _unit;
                                    tb.Width = _labelWidth * 2;
                                    tb.Margin = new Thickness(_startPoint.X + (_endPoint.X - _startPoint.X) * 0.6, _startPoint.Y + 20, _grid.ActualWidth - _startPoint.X - (_endPoint.X - _startPoint.X) * 0.6 - 70, _grid.ActualHeight - _endPoint.Y - 20);
                                }
                                _labels.Add(tb);
                                _grid.Children.Add(_labels[i]);
                            }
                        }
                        RefreshValue();
                    }
                    else
                    {
                        foreach (TextBlock tb in _labels)
                        {
                            _grid.Children.Remove(tb);
                        }
                        _labels.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// 设置轴的标签数量(范围在3-126之间的整数,包括3和126,因为最小、大值和单位是必须有的)，如想把轴分为n段,则把LabelsNumber设为n+1
        /// </summary>
        public byte LabelsNumber
        {
            get { return _labelsNumber; }
            set
            {
                try
                {
                    if (_labelsNumber != value)
                    {
                        if (value >= 3 && value <= 126)
                        {
                            if (_labelsNumber > value)
                            {
                                int span = _labelsNumber - value;
                                _labelsNumber = value;
                                if (_isVisibility)
                                {
                                    for (int i = 0; i < span; i++)
                                    {
                                        _grid.Children.Remove(_labels[i]);
                                        _labels.RemoveAt(i);
                                    }
                                    RefreshLocation();
                                    RefreshValue();
                                }
                            }
                            if (_labelsNumber < value)
                            {
                                int span = value - _labelsNumber;
                                _labelsNumber = value;
                                if (_isVisibility)
                                {
                                    for (int i = 0; i < span; i++)
                                    {
                                        TextBlock tb = new TextBlock();
                                        tb.Height = _labelHeight;
                                        tb.Width = _labelWidth;
                                        _labels.Insert(0, tb);
                                        _grid.Children.Add(_labels[0]);
                                    }
                                    RefreshLocation();
                                    RefreshValue();
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    System.Windows.MessageBox.Show("请输入3-126之间的整数，包括3和126!谢谢!");
                }
            }
        }

        /// <summary>
        /// 轴的最大值
        /// </summary>
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (_maxValue != value)
                {
                    _maxValue = value;
                    if (_isVisibility)
                    {
                        RefreshValue();
                    }
                }
            }
        }

        /// <summary>
        /// 轴的最小值
        /// </summary>
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                if (_minValue != value)
                {
                    _minValue = value;
                    if (_isVisibility)
                    {
                        RefreshValue();
                    }
                }
            }
        }

        /// <summary>
        /// 轴的单位
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    if (_isVisibility)
                    {
                        _labels[_labels.Count - 1].Text = _unit;
                    }
                }
            }
        }

        /// <summary>
        /// 轴的起点(左上角)
        /// </summary>
        public Point StartPoint
        {
            get { return _startPoint; }
            set
            {
                if (_startPoint != value)
                {
                    _startPoint = value;
                    if (_isVisibility)
                    {
                        RefreshLocation();
                    }
                }
            }
        }

        /// <summary>
        /// 轴的终点(右下角)
        /// </summary>
        public Point EndPoint
        {
            get { return _endPoint; }
            set
            {
                if (_endPoint != value)
                {
                    _endPoint = value;
                    if (_isVisibility)
                    {
                        RefreshLocation();
                    }
                }
            }
        }

        /// <summary>
        /// 标签中字体颜色
        /// </summary>
        public SolidColorBrush LabelFontColor
        {
            get { return _labelsFontColor; }
            set
            {
                if (_labelsFontColor != value)
                {
                    _labelsFontColor = value;
                    foreach (TextBlock tb in _labels)
                    {
                        tb.Foreground = _labelsFontColor;
                    }
                }
            }
        }

        /// <summary>
        /// 轴的显示方式
        /// </summary>
        public AxisMode AxisMode
        {
            get { return _axisMode; }
            set
            {
                if (_axisMode != value)
                {
                    _axisMode = value;
                    if (_isVisibility)
                    {
                        RefreshLocation();
                    }
                }
            }
        }

        /// <summary>
        /// 轴的显示平台
        /// </summary>
        public Grid Grid
        {
            get { return _grid; }
            set
            {
                _grid = value;
            }
        }
    }
}

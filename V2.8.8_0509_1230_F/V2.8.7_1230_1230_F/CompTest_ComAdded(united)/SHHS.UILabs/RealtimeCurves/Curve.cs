using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
//using SHHS.FPTB.Base;

namespace SHHS.UILabs.RealtimeCurves
{
    public class Curve
    {
        private SolidColorBrush _curveColor = new SolidColorBrush();     //曲线颜色
        private SolidColorBrush _lightColor = new SolidColorBrush();     //曲线高亮颜色
        private double _curveBreadth = 1;           //曲线宽度
        //private string _englishName;                //曲线的英文名称
        //private string _chineseName;                //曲线的中文名称
        private string _curveName;                  //曲线名称
        private string _unitsTime;                  //曲线的时间单位
        private string _unitsData;                  //曲线的数据单位
        private double _maxTime = 100;              //曲线的最大时间
        private double _minTime = 0;                //曲线的最小时间
        private double _maxData = 100;              //曲线的最大数据
        private double _minData = 0;                //曲线的最小数据
        private bool _visibility = false;        //曲线是否可见
        private bool _isHeightLight = false;     //曲线是否高亮
        private bool _isLineChoosed = false;     //曲线是否被鼠标选中
        private List<MomentData> _sourceDatas;   //曲线的原始数据
        private Polyline _polyline = new Polyline();                //曲线的线条
        private Grid _grid;                                         //曲线显示的平台
        public int ChannelIndex = -1;

        /// <summary>
        /// 曲线的构造函数
        /// </summary>
        public Curve()
        {
            _curveColor = Brushes.OrangeRed;
            _lightColor = Brushes.Yellow;

            _polyline.StrokeLineJoin = PenLineJoin.Bevel;
            _polyline.Stroke = _curveColor;
            _polyline.StrokeThickness = _curveBreadth;
            _polyline.MouseLeftButtonDown += delegate
            {
                _isLineChoosed = true;
            };
            _polyline.MouseLeftButtonUp += delegate
            {
                if (_isLineChoosed)
                {
                    if (_isHeightLight)
                    {
                        IsHighlight = false;
                    }
                    else
                    {
                        IsHighlight = true;
                    }
                    _isLineChoosed = false;
                }
            };
        }



        //把数据坐标转换成显示的图像坐标
        /// <summary>
        /// 刷新曲线
        /// </summary>
        public void Build()
        {
            _polyline.Points.Clear();
            for (int i = 0; i < _sourceDatas.Count; i++)
            {
                _polyline.Points.Add(new Point((MomentData.GetTimeSpan(_sourceDatas, i).TotalSeconds - _minTime) * (_grid.ActualWidth / (_maxTime - _minTime)), _grid.ActualHeight - (_sourceDatas[i].Value - _minData) * (_grid.ActualHeight / (_maxData - _minData))));
                //(_sourceDatas[i].Time-_sourceDatas[0].Time).TotalSeconds
            }
        }

        ////把数据坐标转换成显示的图像坐标
        ///// <summary>
        ///// 刷新曲线
        ///// </summary>
        //public void Build()
        //{
        //    try
        //    {
        //        this.Grid.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(this.BuildViaDispatcher));
        //    }
        //    catch
        //    {

        //    }
        //}
        ////把数据坐标转换成显示的图像坐标
        ///// <summary>
        ///// 刷新曲线
        ///// </summary>
        //private void BuildViaDispatcher()
        //{
        //    _polyline.Points.Clear();
        //    for (int i = 0; i < _sourceDatas.Count; i++)
        //    {
        //        _polyline.Points.Add(new Point((MomentData.GetTimeSpan(_sourceDatas, i).TotalSeconds - _minTime) * (_grid.ActualWidth / (_maxTime - _minTime)), _grid.ActualHeight - (_sourceDatas[i].Value - _minData) * (_grid.ActualHeight / (_maxData - _minData))));
        //    }
        //}

        //public void AppendPoint()
        //{
        //    if (_sourceDatas.Count >= 1)
        //    {
        //        _polyline.Points.Add(new Point((MomentData.GetTimeSpan(_sourceDatas, _sourceDatas.Count - 1).TotalSeconds - _minTime) * (_grid.ActualWidth / (_maxTime - _minTime)), _grid.ActualHeight - (_sourceDatas.Last().Value - _minData) * (_grid.ActualHeight / (_maxData - _minData))));
        //    }
        //}

        public void AppendPoint()
        {
            this.Grid.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(this.AppendPointViaDispatcher));
        }

        public void AppendPointViaDispatcher()
        {
            if (_sourceDatas.Count >= 1)
            {
                _polyline.Points.Add(new Point((MomentData.GetTimeSpan(_sourceDatas, _sourceDatas.Count - 1).TotalSeconds - _minTime) * (_grid.ActualWidth / (_maxTime - _minTime)), _grid.ActualHeight - (_sourceDatas.Last().Value - _minData) * (_grid.ActualHeight / (_maxData - _minData))));
            }
        }

        //*************************************************************************************************************
        //属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性 属性
        //*************************************************************************************************************
        /// <summary>
        /// 曲线是否可见 true:可见   false:不可见
        /// </summary>
        public bool IsVisible
        {
            get { return _visibility; }
            set
            {
                if (_visibility != value)
                {
                    _visibility = value;
                    if (_visibility)
                    {
                        if (!_grid.Children.Contains(_polyline))
                        {
                            _grid.Children.Add(_polyline);

                        }
                    }
                    else
                    {
                        _grid.Children.Remove(_polyline);
                    }
                }
            }
        }

        /// <summary>
        /// 曲线是否高亮 true:高亮  false:正常
        /// </summary>
        public bool IsHighlight
        {
            get { return _isHeightLight; }
            set
            {
                if (_isHeightLight != value)
                {
                    _isHeightLight = value;
                    if (_isHeightLight)
                    {
                        _polyline.Stroke = _lightColor;
                        _polyline.StrokeThickness = _curveBreadth * 2;
                    }
                    else
                    {
                        _polyline.Stroke = _curveColor;
                        _polyline.StrokeThickness = _curveBreadth;
                    }
                }
            }
        }

        /// <summary>
        /// 曲线本来颜色
        /// </summary>
        public SolidColorBrush OriginalColor
        {
            get { return _curveColor; }
            set
            {
                if (_curveColor != value)
                {
                    _curveColor = value;
                    if (!_isHeightLight)
                    {
                        _polyline.Stroke = _curveColor;
                    }
                }
            }
        }

        /// <summary>
        /// 曲线高亮颜色
        /// </summary>
        public SolidColorBrush HighlightColor
        {
            get { return _lightColor; }
            set
            {
                if (_lightColor != value)
                {
                    _lightColor = value;
                    if (_isHeightLight)
                    {
                        _polyline.Stroke = _lightColor;
                    }
                }
            }
        }

        /// <summary>
        /// 曲线宽度
        /// </summary>
        public double Thickness
        {
            get { return _curveBreadth; }
            set
            {
                if (_curveBreadth != value)
                {
                    _curveBreadth = value;
                    if (_isHeightLight)
                    {
                        _polyline.StrokeThickness = _curveBreadth * 2;
                    }
                    else
                    {
                        _polyline.StrokeThickness = _curveBreadth;
                    }
                }
            }
        }

        ///// <summary>
        ///// 曲线的英文名
        ///// </summary>
        //public string EnglishName
        //{
        //    get { return _englishName; }
        //    set { _englishName = value; }
        //}
        ///// <summary>
        ///// 曲线的中文名
        ///// </summary>
        //public string ChineseName
        //{
        //    get { return _chineseName; }
        //    set { _chineseName = value; }
        //}

        /// <summary>
        /// 曲线的名称
        /// </summary>
        public string Name
        {
            get { return _curveName; }
            set { _curveName = value; }
        }

        /// <summary>
        /// 曲线显示的平台
        /// </summary>
        public Grid Grid
        {
            get { return _grid; }
            set
            {
                _grid = value;
            }
        }

        /// <summary>
        /// 曲线的原始数据
        /// </summary>
        public List<MomentData> SourceData
        {
            get { return _sourceDatas; }
            set
            {
                _sourceDatas = value;
            }
        }

        /// <summary>
        /// 曲线的点的集合
        /// </summary>
        public PointCollection Points
        {
            get { return _polyline.Points; }
            set { _polyline.Points = value; }
        }

        /// <summary>
        /// 曲线的数据单位
        /// </summary>
        public string DataUnits
        {
            get { return _unitsData; }
            set { _unitsData = value; }
        }

        /// <summary>
        /// 曲线的时间单位
        /// </summary>
        public string TimeUnits
        {
            get { return _unitsTime; }
            set { _unitsTime = value; }
        }

        /// <summary>
        /// 最大时间
        /// </summary>
        public double MaxTime
        {
            get { return _maxTime; }
            set
            {
                if (_maxTime != value)
                {
                    _maxTime = value;
                }
            }
        }

        /// <summary>
        /// 最小时间
        /// </summary>
        public double MinTime
        {
            get { return _minTime; }
            set
            {
                if (_minTime != value)
                {
                    _minTime = value;
                }
            }
        }

        /// <summary>
        /// 最大数据
        /// </summary>
        public double MaxData
        {
            get { return _maxData; }
            set
            {
                if (_maxData != value)
                {
                    _maxData = value;
                }
            }
        }

        /// <summary>
        /// 最小数据
        /// </summary>
        public double MinData
        {
            get { return _minData; }
            set
            {
                if (_minData != value)
                {
                    _minData = value;
                }
            }
        }

    }
}

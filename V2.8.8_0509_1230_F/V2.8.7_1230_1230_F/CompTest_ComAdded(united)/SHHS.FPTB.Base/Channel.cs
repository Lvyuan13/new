using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SHHS.FPTB.Base
{
    public class Channel : INotifyPropertyChanged
    {
        public Channel()
        {
            this._dataList = new List<MomentData>();
        }

        

        public Channel(int no, int redNo, string name,double filter, string unit, string unitDesc, string type, int precision, double maximum, double minimum, double maxAnalog,double minAnalog)
        {
            _no = no;
            _redNo = redNo;
            _name = name;
            _filter = filter;
            _unit = unit;
            _unitDescription = unitDesc;
            _type = type;
            _precision = precision;
            _maximum = maximum;
            _minimum = minimum;
            _maxAnalog = maxAnalog;
            _minAnalog = minAnalog;
            this._dataList = new List<MomentData>();
        }

        #region 属性更改事件
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            //switch (name)
            //{
            //    case "Maximum":
            //    case "Minimum":
            //    case "MaxVoltage":
            //    case "MinVoltage":
            //        this.UpdateSlope();
            //        this.UpdateVoltageForZero();
            //        break;
            //    case "Voltage":
            //        this.UpdateValue();
            //        break;
            //}

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        #region 属性

        #region 显示序号
        /// <summary>
        /// 显示序号
        /// </summary>
        public int No
        {
            get { return _no; }
            set { _no = value; OnPropertyChanged("No"); }
        }
        private int _no;
        #endregion

        #region 记录仪通道号
        /// <summary>
        /// 对应记录仪通道号
        /// </summary>
        public int RedNo
        {
            get { return _redNo; }
            set { _redNo = value; OnPropertyChanged("RedNo"); }
        }
        private int _redNo;
        #endregion

        #region 英文名称
        ///// <summary>
        ///// 英文名称
        ///// </summary>
        //public string EnglishName
        //{
        //    get { return _englishName; }
        //    set { _englishName = value; OnPropertyChanged("EnglishName"); }
        //}
        //private string _englishName;
        #endregion

        #region 中文名称
        ///// <summary>
        ///// 中文名称
        ///// </summary>
        //public string ChineseName
        //{
        //    get { return _chineseName; }
        //    set { _chineseName = value; OnPropertyChanged("ChineseName"); }
        //}
        //private string _chineseName;
        #endregion

        #region 名称
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;
        #endregion

        #region 单位
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set
            {
                if (_unit != value)
                {
                    _unit = value; OnPropertyChanged("Unit");
                }
            }
        }
        private string _unit;
        #endregion

        #region 最小模拟量
        /// <summary>
        /// 最小模拟量
        /// </summary>
        public double MinAnalog
        {
            get { return _minAnalog; }
            set
            {
                if (_minAnalog != value)
                {
                    _minAnalog = value; OnPropertyChanged("MinAnalog");
                }
            }
        }
        private double _minAnalog;
        #endregion

        #region 最大模拟量
        /// <summary>
        /// 最大模拟量
        /// </summary>
        public double MaxAnalog
        {
            get { return _maxAnalog; }
            set
            {
                if (_maxAnalog != value)
                {
                    _maxAnalog = value; OnPropertyChanged("MaxAnalog");
                }
            }
        }
        private double _maxAnalog;
        #endregion

        #region 最小值
        /// <summary>
        /// 最小数字量
        /// </summary>
        public double Minimum
        {
            get { return _minimum; }
            set
            {
                if (_minimum != value)
                {
                    _minimum = value; OnPropertyChanged("Minimum");
                }
            }
        }
        private double _minimum;
        #endregion

        #region 最大值
        /// <summary>
        /// 最大数字量
        /// </summary>
        public double Maximum
        {
            get { return _maximum; }
            set { _maximum = value; OnPropertyChanged("Maximum"); }
        }
        private double _maximum;
        #endregion

        #region Filter
        /// <summary>
        /// 过滤器
        /// </summary>
        public double Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged("Filter");
            }
        }
        private double _filter;
        #endregion

        #region OldValve
        /// <summary>
        /// 上次值
        /// </summary>
        private double OldValve
        {
            get { return _oldValve; }
            set
            {
                _oldValve = value;
                OnPropertyChanged("OldValve");
            }
        }
        private double _oldValve;
        #endregion

        #region 当前模拟量
        /// <summary>
        /// 当前模拟量
        /// </summary>
        public double Analog
        {
            get { return _analog; }
            set
            {
                _analog = value;
                _actualValue = (value - _minAnalog) * (_maximum - _minimum) / (_maxAnalog - _minAnalog) + _minimum;
                _value = Math.Round(_actualValue, this.Precision, MidpointRounding.ToEven);
                _dataList.Add(new MomentData(_time, _value));
                //OnPropertyChanged("Analog");
                OnPropertyChanged("Value");
            }
        }
        private double _analog;
        #endregion

        /// <summary>
        /// 最新显示值
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                {
                    _value = this._actualValue = 0;
                    _dataList.Add(new MomentData(_time, _value));
                    OnPropertyChanged("Value");
                }
                else
                {
                    this._actualValue = value;
                    _value = Math.Round(value, this.Precision, MidpointRounding.ToEven);
                    _dataList.Add(new MomentData(_time, _value));
                    OnPropertyChanged("Value");
                }
            }
        }
        private double _value;
        /// <summary>
        /// 最新实际值
        /// </summary>
        public double ActualValue
        {
            get { return _actualValue; }
            private set
            {
                if (_actualValue != value)
                {
                    _actualValue = value;
                }
            }
        }
        private double _actualValue;
        /// <summary>
        /// 最新时间
        /// </summary>
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
        private DateTime _time;

        #region 小数位
        /// <summary>
        /// 数据精度
        /// </summary>
        public int Precision
        {
            get { return _precision; }
            set
            {
                _precision = value;
                OnPropertyChanged("Precision");
            }
        }
        private int _precision;
        #endregion

        #region 斜率
        ///// <summary>
        ///// 斜率
        ///// </summary>
        //private double Slope
        //{
        //    get { return _slope; }
        //    set { _slope = value; }
        //}
        //private double _slope;
        #endregion

        #region 偏移值
        ///// <summary>
        ///// 偏移值
        ///// </summary>
        //private double VoltageForZero
        //{
        //    get { return _voltageForZero; }
        //    set { _voltageForZero = value; }
        //}
        //private double _voltageForZero;
        #endregion

        /// <summary>
        /// 单位描述
        /// </summary>
        public string UnitDescription
        {
            get { return _unitDescription; }
            set { _unitDescription = value; OnPropertyChanged("UnitDescription"); }
        }
        private string _unitDescription;
        /// <summary>
        /// 通道类别
        /// </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value; OnPropertyChanged("Type");
                }
            }
        }
        private string _type;
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<MomentData> DataList
        {
            get { return _dataList; }
            protected set { _dataList = value; }
        }
        private List<MomentData> _dataList = null;
        #endregion

        /// <summary>
        /// 平均值的列表
        /// </summary>
        public List<PhaseAvg> PhaseAvgList
        {
            get { return _phaseAvgList; }
            set { _phaseAvgList = value; }
        }
        private List<PhaseAvg> _phaseAvgList = new List<PhaseAvg>() 
        {
            new PhaseAvg(),
            new PhaseAvg(),
            new PhaseAvg(),
            new PhaseAvg(),
            new PhaseAvg(),
            new PhaseAvg(),
            new PhaseAvg(),
        };

        /// <summary>
        /// 总平均值
        /// </summary>
        public double? SumAvg
        {
            get { return _sumAvg; }
            set { _sumAvg = value; OnPropertyChanged("SumAvg"); }
        }
        private double? _sumAvg = null;

        #region 方法
        /// <summary>
        /// 把模拟信号转换为当前值
        /// </summary>
        /// <param name="analog">模拟量</param>
        /// <returns>返回当前值</returns>
        //public double ConvertToValue(double analog)
        //{
        //    double value;
        //    value = this._slope * analog - this._slope * _minAnalog + _minimum;
        //    return value;
        //}

        /// <summary>
        /// 通过值和电压的范围，重新更新斜率，并返回斜率
        /// </summary>
        /// <returns>斜率值</returns>
        //private void UpdateSlope()
        //{
        //    //double slope;
        //    if (_maxAnalog - _minAnalog != 0)
        //    {
        //        this._slope = (_maximum - _minimum) / (_maxAnalog - _minAnalog);
        //    }
        //    else
        //    {
        //        this._slope = 0;
        //    }
        //    //this.Slope = slope;
        //}

        ///// <summary>
        ///// 更新值
        ///// </summary>
        //private void UpdateValue()
        //{
        //    //double value;
        //    //value = this._slope * _currentVoltage - this._slope * _minVoltage + _minimum;
        //    //this.Value = Math.Round(value, this._precision, MidpointRounding.ToEven);
        //    this.Value = this._slope * _Analog - this._slope * _minAnalog + _minimum;
        //}

        ///// <summary>
        ///// 更新偏移值
        ///// </summary>
        //private void UpdateVoltageForZero()
        //{
        //    double offset;
        //    if (_maximum - _minimum != 0)
        //    {
        //        offset = _minAnalog - ((_maxAnalog - _minAnalog) / (_maximum - _minimum)) * _minimum;
        //    }
        //    else
        //    {
        //        offset = 0;
        //    }
        //    this.VoltageForZero = offset;
        //}
        #endregion
    }

    /// <summary>
    /// 平均数据
    /// </summary>
    public class PhaseAvg : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        //private bool _isActive = false;
        ///// <summary>
        ///// 是否活跃的
        ///// </summary>
        //public bool IsActive
        //{
        //    get { return _isActive; }
        //    set { _isActive = value; OnPropertyChanged("IsActive"); }
        //}

        private double? _avg = null;
        /// <summary>
        /// 平均值
        /// </summary>
        public double? Avg
        {
            get { return _avg; }
            set { _avg = value; OnPropertyChanged("Avg"); }
        }

        private double _sum;
        /// <summary>
        /// 数据总和
        /// </summary>
        public double Sum
        {
            get { return _sum; }
            set { _sum = value; OnPropertyChanged("Sum"); }
        }

        private int _count = 0;
        /// <summary>
        /// 数据计数
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged("Count"); }
        }
    }
}

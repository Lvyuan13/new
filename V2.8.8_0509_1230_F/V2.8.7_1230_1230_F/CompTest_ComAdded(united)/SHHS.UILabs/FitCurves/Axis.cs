using System.ComponentModel;
using System.Windows.Media;

namespace SHHS.UILabs.FitCurves
{
    public class Axis : INotifyPropertyChanged
    {
        #region Filed
        /// <summary>
        /// 名称
        /// </summary>
        private string _name;
        /// <summary>
        /// 前景色
        /// </summary>
        private SolidColorBrush _foreground;
        /// <summary>
        /// 最大值
        /// </summary>
        private double _maximum;
        /// <summary>
        /// 最小值
        /// </summary>
        private double _minimum;
        /// <summary>
        /// 单位
        /// </summary>
        private string _unit;
        /// <summary>
        /// 是否有效
        /// </summary>
        private bool _isActive;
        /// <summary>
        /// 对应通道号
        /// </summary>
        private int _channelNo;
        /// <summary>
        /// 刻度集合
        /// </summary>
        private Scale[] _scales;
        /// <summary>
        /// 单位描述
        /// </summary>
        private string _decription;
        /// <summary>
        /// 曲线拟合次方数
        /// </summary>
        private int _linestNumber;

        #endregion

        #region Property
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
        /// <summary>
        /// 前景色
        /// </summary>
        public SolidColorBrush Foreground
        {
            get
            {
                return _foreground;
            }
            set
            {
                if (_foreground != value)
                {
                    _foreground = value;
                    OnPropertyChanged("Foreground");
                }
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public double Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                if (_maximum != value)
                {
                    _maximum = value;
                    this.RefreshScale();
                    OnPropertyChanged("Maximum");
                }
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public double Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                if (_minimum != value)
                {
                    _minimum = value;
                    this.RefreshScale();
                    OnPropertyChanged("Minimum");
                }
            }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    OnPropertyChanged("Unit");
                }
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }

        /// <summary>
        /// 对应通道号
        /// </summary>
        public int ChannelNo
        {
            get
            {
                return _channelNo;
            }
            set
            {
                if (_channelNo != value)
                {
                    _channelNo = value;
                    OnPropertyChanged("ChannelNo");
                }
            }
        }

        /// <summary>
        /// 刻度集合
        /// </summary>
        public Scale[] Scales
        {
            get
            {
                return _scales;
            }
            set
            {
                _scales = value;
                OnPropertyChanged("Scales");
            }
        }

        /// <summary>
        /// 单位描述
        /// </summary>
        public string Decription
        {
            get
            {
                return _decription;
            }
            set
            {
                if (_decription != value)
                {
                    _decription = value;
                    OnPropertyChanged("Decription");
                }
            }
        }

        /// <summary>
        /// 曲线拟合次方数
        /// </summary>
        public int LinestNumber
        {
            get { return _linestNumber; }
            set
            {
                if (_linestNumber != value)
                {
                    _linestNumber = value;
                    OnPropertyChanged("LinestNumber");
                }
            }
        }
        #endregion

        #region Method
        public Axis(bool isActive, int channelNo, string name, double maximum, double minimum, int scaleNumber)
        {
            this.Name = name;
            this.IsActive = isActive;
            this.ChannelNo = channelNo;
            this.Maximum = maximum;
            this.Minimum = minimum;

            this.Scales = new Scale[scaleNumber + 1];
            for (int i = 0; i <= scaleNumber; i++)
            {
                this.Scales[i] = new Scale((_maximum - _minimum) / scaleNumber * i + _minimum);
            }
        }
        /// <summary>
        /// 刷新刻度值
        /// </summary>
        private void RefreshScale()
        {
            if (this._scales != null)
            {
                for (int i = 0; i < _scales.Length; i++)
                {
                    _scales[i].Value = (_maximum - _minimum) / (_scales.Length - 1) * i + _minimum;
                }
            }
        }
        #endregion

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
    }
}

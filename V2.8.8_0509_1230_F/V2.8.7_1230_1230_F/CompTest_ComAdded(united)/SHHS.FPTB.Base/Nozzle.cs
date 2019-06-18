
using System.ComponentModel;
namespace SHHS.FPTB.Base
{
    public class Nozzle : INotifyPropertyChanged
    {
        #region 属性更改事件
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        /// <summary>
        /// 喷嘴的直径
        /// </summary>
        public double Diameter
        {
            get { return _dia; }
            set
            {
                if (_dia != value)
                {
                    _dia = value;
                    OnPropertyChanged("Diameter");
                }
            }
        }
        private double _dia;
        /// <summary>
        /// 获取和设置 喷嘴打开与关闭
        /// </summary>
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    OnPropertyChanged("IsOpen");
                }
            }
        }
        private bool _isOpen;
        /// <summary>
        /// 喷嘴最小风量
        /// </summary>
        public double Minimum
        {
            get { return _min; }
            set
            {
                if (_min != value)
                {
                    _min = value;
                    OnPropertyChanged("Minimum");
                }
            }
        }
        private double _min;
        /// <summary>
        /// 喷嘴最大风量
        /// </summary>
        public double Maximum
        {
            get { return _max; }
            set
            {
                if (_max != value)
                {
                    _max = value;
                    OnPropertyChanged("Maximum");
                }
            }
        }
        private double _max;
        /// <summary>
        /// PLC 地址
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// 风量单位
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    OnPropertyChanged("Unit");
                }
            }
        }
        private string _unit;
    }
}

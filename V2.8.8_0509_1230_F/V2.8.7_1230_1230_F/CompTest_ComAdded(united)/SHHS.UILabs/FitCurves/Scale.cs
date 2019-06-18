using System.ComponentModel;

namespace SHHS.UILabs.FitCurves
{
    /// <summary>
    /// 刻度值
    /// </summary>
    public class Scale : INotifyPropertyChanged
    {
        #region Filed
        /// <summary>
        /// 刻度值
        /// </summary>
        private double _value;
        #endregion

        #region Property
        /// <summary>
        /// 刻度值
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
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

        #region Method
        /// <summary>
        /// 构造方法
        /// </summary>
        public Scale()
        { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="value">刻度值</param>
        public Scale(double value)
        {
            this.Value = value;
        }
        #endregion
    }
}

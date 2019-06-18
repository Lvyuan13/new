using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SHHS.FPTB.Base
{
    public class Valve : INotifyPropertyChanged
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


        private double _minFlow;
        /// <summary>
        /// 最小流量
        /// </summary>
        public double MinFlow
        {
            get { return _minFlow; }
            set { _minFlow = value; OnPropertyChanged("MinFlow"); }
        }
        private double _maxFlow;
        /// <summary>
        /// 最大流量
        /// </summary>
        public double MaxFlow
        {
            get { return _maxFlow; }
            set { _maxFlow = value; OnPropertyChanged("MaxFlow"); }
        }
        private double _valveRatio;
        /// <summary>
        /// 开合角度
        /// </summary>
        public double ValveRatio
        {
            get { return _valveRatio; }
            set { _valveRatio = value; OnPropertyChanged("ValveRatio"); }
        }
    }
}

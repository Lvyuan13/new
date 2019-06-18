using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SHHS.FPTB.Base
{
    public class Coef : INotifyPropertyChanged
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

        public Coef()
        {

        }
        public Coef(double svalue, double avlue, double k, double b)
        {
            SValue = svalue;
            AValue = avlue;
            K = k;
            B = b;
            //OnPropertyChanged("SValue");
            //OnPropertyChanged("AValue");
            //OnPropertyChanged("K");
            //OnPropertyChanged("B");
        }

        private double _svalue;
        /// <summary>
        /// 基准值
        /// </summary>
        public double SValue
        {
            get { return _svalue; }
            set { _svalue = value; OnPropertyChanged("SValue"); }
        }

        private double _avalue;
        /// <summary>
        /// 实测值
        /// </summary>
        public double AValue
        {
            get { return _avalue; }
            set { _avalue = value; OnPropertyChanged("AValue"); }
        }

        private double _k = 1;
        /// <summary>
        /// 一次方系数
        /// </summary>
        public double K
        {
            get { return _k; }
            set { _k = value; OnPropertyChanged("K"); }
        }

        private double _b = 0;
        /// <summary>
        /// 1次方系数
        /// </summary>
        public double B
        {
            get { return _b; }
            set { _b = value; OnPropertyChanged("B"); }
        }
    }
}

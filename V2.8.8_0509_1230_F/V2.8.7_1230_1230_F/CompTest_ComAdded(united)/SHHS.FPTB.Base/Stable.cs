using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SHHS.FPTB.Base
{
    public class Stable : INotifyPropertyChanged
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

        public Stable(int id, int channelNo, string name, double value)
        {
            ID = id;
            ChannelNo = channelNo;
            Name = name;
            Value = value;
        }

        private int _id;
        /// <summary>
        /// 条件名称
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("ID"); }
        }

        private int _channelNo;
        /// <summary>
        /// 条件名称
        /// </summary>
        public int ChannelNo
        {
            get { return _channelNo; }
            set { _channelNo = value; OnPropertyChanged("ChannelNo"); }
        }

        private string _name;
        /// <summary>
        /// 条件名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private double _value;
        /// <summary>
        /// 条件名称
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }
    }
}

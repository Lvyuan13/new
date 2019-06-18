using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SHHS.FPTB.Base
{
    public class Cdt : INotifyPropertyChanged
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
        /// 序号
        /// </summary>
        public int No { get; set; }
        private bool _isDone = false;
        /// <summary>
        /// 是否已完成
        /// </summary>
        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private bool _isActive = false;
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive 
        {
            get { return _isActive; }
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
        /// 热水侧进口温度
        /// </summary>
        private double _hotWaterTemp;
        /// <summary>
        /// 热水侧进口温度
        /// </summary>
        public double HotWaterTemp
        {
            get { return _hotWaterTemp; }
            set { _hotWaterTemp = value; OnPropertyChanged("HotWaterTemp"); }
        }
        /// <summary>
        /// 热水侧流量
        /// </summary>
        private double _hotWaterFlow;
        /// <summary>
        /// 热水侧流量
        /// </summary>
        public double HotWaterFlow
        {
            get { return _hotWaterFlow; }
            set { _hotWaterFlow = value; OnPropertyChanged("HotWaterFlow"); }
        }
        /// <summary>
        /// 冷水侧进口温度
        /// </summary>
        private double _coldWaterTemp;
        /// <summary>
        /// 冷水侧进口温度
        /// </summary>
        public double ColdWaterTemp
        {
            get { return _coldWaterTemp; }
            set { _coldWaterTemp = value; OnPropertyChanged("ColdWaterTemp"); }
        }
        /// <summary>
        /// 冷水侧流量
        /// </summary>
        private double _coldWaterFlow;
        /// <summary>
        /// 冷水侧流量
        /// </summary>
        public double ColdWaterFlow
        {
            get { return _coldWaterFlow; }
            set { _coldWaterFlow = value; OnPropertyChanged("ColdWaterFlow"); }
        }
    }
}

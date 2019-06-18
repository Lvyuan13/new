using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SHHS.FPTB.Base
{
    public class TestCondition// : INotifyPropertyChanged
    {
        #region 属性更改事件
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}
        #endregion

        public TestCondition(bool isActive = false, double sp = 0, double af = 0, double vol = 0)
        {
            IsActive = isActive;
            SP = sp;
            AF = af;
            VOL = vol;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 转速
        /// </summary>
        public double SP { get; set; }
        /// <summary>
        /// 转速
        /// </summary>
        public double AF { get; set; }
        /// <summary>
        /// 转速
        /// </summary>
        public double VOL { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SHHS.UILabs
{
    /// <summary>
    /// 瞬时值 包含时间、数据、对应开始时间的时间跨度
    /// </summary>
    public struct MomentData
    {
        private DateTime _time;
        private double _value;

        public MomentData(DateTime time, double value)
        {
            _time = time;
            _value = value;
        }
        /// <summary>
        /// 时间值
        /// </summary>
        public DateTime Time
        {
            get { return _time; }
        }
        /// <summary>
        /// 数据值
        /// </summary>
        public double Value
        {
            get { return _value; }
        }

        public static TimeSpan GetTimeSpan(List<MomentData> momentDatas, int index)
        {
            if (momentDatas == null || momentDatas.Count < 1 || index > momentDatas.Count)
                throw new Exception("数据列表为空,或者要查找的索引超出了数据列表的范围!");

            return momentDatas[index].Time - momentDatas[0].Time;
        }
    }
}

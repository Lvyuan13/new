using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackPanel
{
    public static partial class Control
    {
        /// <summary>
        /// 单个控制器的输入结构变量定义
        /// </summary>
        public class UT35ADef
        {

            public double P;
            public double I;
            public double D;

            // SH:最大值 SL:最小值 DR:方向 OUT:输入值 MOD:模式(1、手动)，这些东西都是给CMD的！！
            public string Name = "";
            /// <summary>
            /// 方向
            /// </summary>
            public bool DR = false;
            /// <summary>
            /// 栈号，就是第几个控制器
            /// </summary>
            public int StackNum;
            //public void Set(int ZD, string CMD, double ZValue, int SDP)
            public int SDP = 2;
            /// <summary>
            /// 上限
            /// </summary>
            public double SH;
            
            /// <summary>
            /// 下限
            /// </summary>
            public double SL;
            /// <summary>
            /// 输出百分比
            /// </summary>
            public double OUT;

            //设定值20150922
            public double SV;
            /// <summary>
            ///当前值
            /// </summary>
            public double PV;
            /// <summary>
            /// 定义控制器的手动状态
            /// </summary>
            public bool MannualModOn; public bool MannualModOnPrev;//这应该是之前值的意思，但是参考没有引用！几乎无用
            /// <summary>
            /// 定义控制器的手动输出
            /// </summary>
            public double MannualOut; public double MannualOutPrev;
            /// <summary>
            /// 设定值
            /// </summary>
            public double SetValue; public double SetValuePrev;
            
            //public string SDP = "";
            //public bool IsInverse;
            //public double MaxNum;
            //public double MinNum;

            //public double SV;
            //public double CorrectedValue;
            //public double FloatValue;

        }

        /// <summary>
        /// 控制list
        /// </summary>
        public static List<UT35ADef> Controllist = new List<UT35ADef>();

        

        ///// <summary>
        ///// 控制器输出结构变量的定义
        ///// </summary>
        //public class UT35AOutputDef
        //{
        //    public string StackNum=" ";

        //    public double P;
        //    public double I;
        //    public double D;
        //    /// <summary>
        //    /// 输出百分比
        //    /// </summary>
        //    public double OUT;
        //}
        //public static UT35AOutputDef OutputUT35A = new UT35AOutputDef();

    }
}

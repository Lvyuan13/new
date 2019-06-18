using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackPanel
{

    public static partial class PLCMod
    {
        public class PLCBitVar
        {
            /// <summary>
            /// PLC对应的名称
            /// </summary>
            public string Name = "LL";
            /// <summary>
            /// PLC存储地点
            /// </summary>
            public string MemoryArea = "XX";
            // Other field defined below 
            /// <summary>
            /// PLC地址位
            /// </summary>
            public string Address = "";
            /// <summary>
            /// PLC初始值
            /// </summary>
            public bool Initial = false;
            /// <summary>
            /// PLC是否反转
            /// </summary>
            public bool IsConverse = false;

            #region 下面的都不用出现在数据库里
            /// <summary>
            /// PLC当前值
            /// </summary>
            public bool CurrentValue = false;
            /// <summary>
            /// 是否报警
            /// </summary>
            public bool IsAlerting = false;
            /// <summary>
            /// 报警时间戳
            /// </summary>
            public string AlertTimeStamp = "";
            #endregion

        }

        //DO list
        public static List<PLCBitVar> PLCDOList = new List<PLCBitVar>();

        //DI list
        public static List<PLCBitVar> PLCDIList = new List<PLCBitVar>();

        ///// <summary>
        ///// 报警 list
        ///// </summary>
        //public static List<PLCBitVar> PLCAlertList = new List<PLCBitVar>();
        /// <summary>
        /// 只要至少存在一个错误，这个就为真：20150922
        /// </summary>
        public static bool IsError = false;


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHHS.FPTB.Base
{
    /// <summary>
    /// 冷却模式
    /// </summary>
    public enum CoolMode : int
    {
        /// <summary>
        /// 未选择任何模式
        /// </summary>
        None = 0,
        /// <summary>
        /// 冷却塔模式 =1
        /// </summary>
        CoolTower = 1,
        /// <summary>
        /// 冷凝机组模式 =2
        /// </summary>
        CondUnit = 2
    }
}

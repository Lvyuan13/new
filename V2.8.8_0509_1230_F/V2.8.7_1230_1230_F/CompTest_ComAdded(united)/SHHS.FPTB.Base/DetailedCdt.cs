
namespace SHHS.FPTB.Base
{
    public class DetailedCdt
    {
        public DetailedCdt()
        {
        }
        /// <summary>
        /// 状态
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 静压
        /// </summary>
        public double SP { get; set; }
        /// <summary>
        /// 风量
        /// </summary>
        public double AF { get; set; }
    }
}

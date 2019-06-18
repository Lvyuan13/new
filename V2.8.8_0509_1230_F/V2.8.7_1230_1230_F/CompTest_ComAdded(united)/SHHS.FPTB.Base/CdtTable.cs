using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace SHHS.FPTB.Base
{
    public class CdtTable
    {
        /// <summary>
        /// 加载试验工况
        /// </summary>
        /// <param name="dbFile">数据库名</param>
        /// <param name="table">表名 Cdts</param>
        /// <returns>返回试验工况集合</returns>
        public static Dictionary<int, Cdt> Load(string dbFile, string table)
        {
            Dictionary<int, Cdt> _conditions = new Dictionary<int, Cdt>();
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + dbFile))
            {
                SqlCeCommand cmd = new SqlCeCommand("Select * from " + table + " order by No", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    _conditions.Add(Convert.ToInt32(dr[0]), new Cdt() { No = Convert.ToInt32(dr[0]),
                                                                        Name = dr[1].ToString(),
                                                                        IsActive = Convert.ToBoolean(dr[2]),
                                                                        HotWaterTemp = Convert.ToDouble(dr[3]),
                                                                        HotWaterFlow = Convert.ToDouble(dr[4]),
                                                                        ColdWaterTemp = Convert.ToDouble(dr[5]),
                                                                        ColdWaterFlow = Convert.ToDouble(dr[6]),
                                                                        IsDone = Convert.ToBoolean(dr[7].ToString())
                    });
                }
                dr.Close();
                conn.Close();
            }
            return _conditions;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="dbFile"></param>
        /// <param name="table">表名 Cdts</param>
        /// <param name="conditions"></param>
        public static void Update(string dbFile, string table, Dictionary<int, Cdt> conditions)
        {
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + dbFile))
            {
                SqlCeCommand cmd = conn.CreateCommand();
                conn.Open();
                foreach (var c in conditions.Values)
                {
                    cmd.CommandText = "update " + table + " set HotWaterTemp=" + c.HotWaterTemp
                                                            + ",HotWaterFlow=" + c.HotWaterFlow
                                                            + ",ColdWaterTemp=" + c.ColdWaterTemp
                                                            + ",ColdWaterFlow=" + c.ColdWaterFlow
                                                            + ",IsDone=" + Convert.ToInt32(c.IsDone)
                                                            + " where No=" + c.No;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace SHHS.FPTB.Base
{
    public class StableTable
    {
        /// <summary>
        /// 加载判稳条件
        /// </summary>
        /// <param name="dbFile">数据库名</param>
        /// <param name="table">表名 Stable</param>
        /// <returns>返回判稳条件集合</returns>
        public static Dictionary<int, Stable> Load(string dbFile, string table)
        {
            Dictionary<int, Stable> _stables = new Dictionary<int, Stable>();
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + dbFile))
            {
                SqlCeCommand cmd = new SqlCeCommand("Select * from " + table + " order by ID", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    _stables.Add(Convert.ToInt32(dr[0]), new Stable(Convert.ToInt32(dr[0]), Convert.ToInt32(dr[1]), dr[2].ToString(),
                                                            Convert.ToDouble(dr[3])));
                }
                dr.Close();
                conn.Close();
            }
            return _stables;
        }

        /// <summary>
        /// 更新判稳条件
        /// </summary>
        /// <param name="dbFile"></param>
        /// <param name="table">表名 Stable</param>
        /// <param name="stables"></param>
        public static void Update(string dbFile, string table, Dictionary<int, Stable> stables)
        {
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + dbFile))
            {
                SqlCeCommand cmd = conn.CreateCommand();
                conn.Open();
                foreach (var c in stables)
                {
                    cmd.CommandText = "update " + table + " set [Value]='" + c.Value.Value + "' where ID=" + c.Key;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}

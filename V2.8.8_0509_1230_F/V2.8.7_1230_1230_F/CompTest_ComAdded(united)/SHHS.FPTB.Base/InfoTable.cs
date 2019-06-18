using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace SHHS.FPTB.Base
{
    public class InfoTable
    {
        /// <summary>
        /// 加载试验工况
        /// </summary>
        /// <param name="dbFile">数据库名</param>
        /// <param name="table">表名 Infos</param>
        /// <returns>返回试验工况集合</returns>
        public static Info Load(string dbFile, string table)
        {
            Info info = new Info();
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + dbFile))
            {
                SqlCeCommand cmd = new SqlCeCommand("Select * from " + table + " order by No", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                List<string> result = new List<string>();
                while (dr.Read())
                {
                    result.Add(dr[2].ToString());
                }
                dr.Close();
                conn.Close();

                info.FileName = result[0];
                //info.StartTime = result[1];
                //info.EndTime = result[2];
                info.Operator = result[3];
                info.Maker = result[4];
                info.Model = result[5];
                info.SN = result[6];
                info.RatedVoltage = double.Parse(result[7]);
                info.RatedFrequency = double.Parse(result[8]);
                info.RatedPower = double.Parse(result[9]);
                info.RatedCold = double.Parse(result[10]);
                info.RatedHot = double.Parse(result[11]);
                info.EleHotPower = double.Parse(result[12]);
                info.Refrigerant = result[13];
                info.CondPress = double.Parse(result[14]);
                info.EvapPress = double.Parse(result[15]);
                info.CondArea = double.Parse(result[16]);
                info.EvapArea = double.Parse(result[17]);
                info.CondLeakage = double.Parse(result[18]);
                info.EvapLeakage = double.Parse(result[19]);
                info.PowerPhaseWire = result[20];
                info.EvapDirtyArea = double.Parse(result[21]);
                info.EvapDirtyCoef = double.Parse(result[22]);
                info.CondDirtyArea = double.Parse(result[23]);
                info.CondDirtyCoef = double.Parse(result[24]);
            }
            return info;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="dbFile"></param>
        /// <param name="table">表名 Infos</param>
        /// <param name="conditions"></param>
        public static void Update(string dbFile, string table, Info info)
        {
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + dbFile))
            {
                SqlCeCommand cmd = conn.CreateCommand();
                conn.Open();
                List<string> infos = new List<string>() 
                {
                    info.FileName,
                    "", //info.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    "", //info.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    info.Operator,
                    info.Maker,
                    info.Model,
                    info.SN,
                    info.RatedVoltage.ToString(),
                    info.RatedFrequency.ToString() ,
                    info.RatedPower.ToString() ,
                    info.RatedCold.ToString() ,
                    info.RatedHot.ToString(),
                    info.EleHotPower.ToString() ,
                    info.Refrigerant ,
                    info.CondPress.ToString(),
                    info.EvapPress.ToString() ,
                    info.CondArea.ToString() ,
                    info.EvapArea.ToString() ,
                    info.CondLeakage.ToString() ,
                    info.EvapLeakage.ToString() ,
                    info.PowerPhaseWire.ToString(),
                    info.EvapDirtyArea.ToString(),
                    info.EvapDirtyCoef.ToString(),
                    info.CondDirtyArea.ToString(),
                    info.CondDirtyCoef.ToString()
                };
                for (int i = 1; i <= infos.Count; i++)
                {
                    cmd.CommandText = "update " + table + " set [Value]='" + infos[i - 1] + "' where No=" + i;
                    cmd.ExecuteNonQuery();
                }


                //foreach (var c in conditions.Values)
                //{
                //    cmd.CommandText = "update " + table + " set HotWaterTemp=" + c.HotWaterTemp
                //                                            + ",HotWaterFlow=" + c.HotWaterFlow
                //                                            + ",ColdWaterTemp=" + c.ColdWaterTemp
                //                                            + ",ColdWaterFlow=" + c.ColdWaterFlow
                //                                            + " where No=" + c.No;
                //    cmd.ExecuteNonQuery();
                //}
                conn.Close();
            }
        }
    }
}

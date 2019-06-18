using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace SHHS.FPTB.Base
{
    public class AvgListTable
    {
        /// <summary>
        /// 保存平均值数据
        /// </summary>
        /// <param name="workerDB">数据库文件名 全路径</param>
        /// <param name="table">表名 AvgList</param>
        /// <param name="channels">通道列表</param>
        public static void Store(string workerDB, string table, Dictionary<int, Channel> channels, int projectCode)
        {
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + workerDB))
            {
                SqlCeCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = "delete " + table + " where ProjectCode=" + projectCode;
                cmd.ExecuteNonQuery();

                foreach (var v in channels.Values)
                {
                    cmd.CommandText = "insert into " + table + " values(" + v.No + ",'" +
                                                                    v.SumAvg.ToString() + "','" +
                                                                    v.PhaseAvgList[0].Avg.ToString() + "','" +
                                                                    v.PhaseAvgList[1].Avg.ToString() + "','" +
                                                                    v.PhaseAvgList[2].Avg.ToString() + "','" +
                                                                    v.PhaseAvgList[3].Avg.ToString() + "','" +
                                                                    v.PhaseAvgList[4].Avg.ToString() + "','" +
                                                                    v.PhaseAvgList[5].Avg.ToString() + "','" +
                                                                    v.PhaseAvgList[6].Avg.ToString() + "'," +
                                                                    projectCode + ")";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        /// <summary>
        /// 加载平均值
        /// </summary>
        /// <param name="workerDB">工作数据库</param>
        /// <param name="table">表名 AvgList</param>
        /// <param name="channels">通道列表</param>
        public static void Load(string workerDB, string table, Dictionary<int, Channel> channels, int projectCode)
        {
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + workerDB))
            {
                SqlCeCommand cmd = new SqlCeCommand("select * from " + table + " where ProjectCode = " + projectCode, conn);
                conn.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int key = Convert.ToInt32(reader["ChannelNo"]);
                    string str = reader["SumAvg"].ToString();
                    if (str == "")
                        channels[key].SumAvg = null;
                    else
                        channels[key].SumAvg = Convert.ToDouble(reader["SumAvg"]);
                    if (reader["Avg1"].ToString() != "")
                        channels[key].PhaseAvgList[0].Avg = Convert.ToDouble(reader["Avg1"]);
                    if (reader["Avg2"].ToString() != "")
                        channels[key].PhaseAvgList[1].Avg = Convert.ToDouble(reader["Avg2"]);
                    if (reader["Avg3"].ToString() != "")
                        channels[key].PhaseAvgList[2].Avg = Convert.ToDouble(reader["Avg3"]);
                    if (reader["Avg4"].ToString() != "")
                        channels[key].PhaseAvgList[3].Avg = Convert.ToDouble(reader["Avg4"]);
                    if (reader["Avg5"].ToString() != "")
                        channels[key].PhaseAvgList[4].Avg = Convert.ToDouble(reader["Avg5"]);
                    if (reader["Avg6"].ToString() != "")
                        channels[key].PhaseAvgList[5].Avg = Convert.ToDouble(reader["Avg6"]);
                    if (reader["Avg7"].ToString() != "")
                        channels[key].PhaseAvgList[6].Avg = Convert.ToDouble(reader["Avg7"]);


                    //channels[key].PhaseAvgList[0].Avg = Convert.ToDouble(reader["PhaseAvg1"]);  //reader["PhaseAvg1"].ToString() != "" ? null : Convert.ToDouble(reader["PhaseAvg1"]);
                    //channels[key].PhaseAvgList[1].Avg = Convert.ToDouble(reader["PhaseAvg2"]);
                    //channels[key].PhaseAvgList[2].Avg = Convert.ToDouble(reader["PhaseAvg3"]);
                    //channels[key].PhaseAvgList[3].Avg = Convert.ToDouble(reader["PhaseAvg4"]);
                    //channels[key].PhaseAvgList[4].Avg = Convert.ToDouble(reader["PhaseAvg5"]);
                    //channels[key].PhaseAvgList[5].Avg = Convert.ToDouble(reader["PhaseAvg6"]);
                    //channels[key].PhaseAvgList[6].Avg = Convert.ToDouble(reader["PhaseAvg7"]);
                }
                reader.Close();
                conn.Close();
            }
        }

        /// <summary>
        /// 加载平均值
        /// </summary>
        /// <param name="workerDB">工作数据库</param>
        /// <param name="table">表名 AvgList</param>
        /// <param name="projectCode">项目号</param>
        public static Dictionary<int, double[]> Load(string workerDB, string table, int projectCode)
        {
            Dictionary<int, double[]> AvgList = new Dictionary<int, double[]>();
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + workerDB))
            {
                SqlCeCommand cmd = new SqlCeCommand("select * from " + table + " where ProjectCode = " + projectCode, conn);
                conn.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int key = Convert.ToInt32(reader["ChannelNo"]);
                    double[] avgData = new double[8];
                    avgData[0] = Convert.ToDouble(reader["SumAvg"]);
                    avgData[1] = Convert.ToDouble(reader["Avg1"]);
                    avgData[2] = Convert.ToDouble(reader["Avg2"]);
                    avgData[3] = Convert.ToDouble(reader["Avg3"]);
                    avgData[4] = Convert.ToDouble(reader["Avg4"]);
                    avgData[5] = Convert.ToDouble(reader["Avg5"]);
                    avgData[6] = Convert.ToDouble(reader["Avg6"]);
                    avgData[7] = Convert.ToDouble(reader["Avg7"]);
                    AvgList.Add(key, avgData);


                    //string str = reader["SumAvg"].ToString();
                    //if (str == "")
                    //    channels[key].SumAvg = null;
                    //else
                    //    channels[key].SumAvg = Convert.ToDouble(reader["SumAvg"]);
                    //if (reader["Avg1"].ToString() != "")
                    //    channels[key].PhaseAvgList[0].Avg = Convert.ToDouble(reader["Avg1"]);
                    //if (reader["Avg2"].ToString() != "")
                    //    channels[key].PhaseAvgList[1].Avg = Convert.ToDouble(reader["Avg2"]);
                    //if (reader["Avg3"].ToString() != "")
                    //    channels[key].PhaseAvgList[2].Avg = Convert.ToDouble(reader["Avg3"]);
                    //if (reader["Avg4"].ToString() != "")
                    //    channels[key].PhaseAvgList[3].Avg = Convert.ToDouble(reader["Avg4"]);
                    //if (reader["Avg5"].ToString() != "")
                    //    channels[key].PhaseAvgList[4].Avg = Convert.ToDouble(reader["Avg5"]);
                    //if (reader["Avg6"].ToString() != "")
                    //    channels[key].PhaseAvgList[5].Avg = Convert.ToDouble(reader["Avg6"]);
                    //if (reader["Avg7"].ToString() != "")
                    //    channels[key].PhaseAvgList[6].Avg = Convert.ToDouble(reader["Avg7"]);
                }
                reader.Close();
                conn.Close();
            }

            return AvgList;
        }
    }
}

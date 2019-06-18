using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace SHHS.FPTB.Base
{
    public class CoefTable
    {
        public static List<double> Load(string databaseFile, string tableName)
        {
            List<double> coefs = new List<double>();
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + databaseFile))
            {
                SqlCeCommand cmd = new SqlCeCommand("Select * from " + tableName + " order by SValue", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    coefs.Add(double.Parse(dr[4].ToString()));
                    coefs.Add(double.Parse(dr[0].ToString()));
                    coefs.Add(double.Parse(dr[1].ToString()));
                    coefs.Add(double.Parse(dr[2].ToString()));
                    coefs.Add(double.Parse(dr[3].ToString()));
                }
                dr.Close();
                conn.Close();
            }
            return coefs;
        }

        public static void Update(string databaseFile, string tableName, List<Coef> coldIn, List<Coef> coldOut, List<Coef> hotIn, List<Coef> hotOut)
        {
            using (SqlCeConnection conn = new SqlCeConnection("Data Source=" + databaseFile))
            {
                SqlCeCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = "DELETE FROM " + tableName;
                cmd.ExecuteNonQuery();

                for (int i = 0; i < coldIn.Count; i++)
                {
                    cmd.CommandText = "insert into " + tableName + " values(" + coldIn[i].SValue
                                                                             + "," + coldIn[i].AValue
                                                                             + "," + coldIn[i].K
                                                                             + "," + coldIn[i].B + ",1)";
                    cmd.ExecuteNonQuery();
                } 
                for (int i = 0; i < coldOut.Count; i++)
                {
                    cmd.CommandText = "insert into " + tableName + " values(" + coldOut[i].SValue
                                                                             + "," + coldOut[i].AValue
                                                                             + "," + coldOut[i].K
                                                                             + "," + coldOut[i].B + ",2)";
                    cmd.ExecuteNonQuery();
                }

                for (int i = 0; i < hotIn.Count; i++)
                {
                    cmd.CommandText = "insert into " + tableName + " values(" + hotIn[i].SValue
                                                                             + "," + hotIn[i].AValue
                                                                             + "," + hotIn[i].K
                                                                             + "," + hotIn[i].B + ",3)";
                    cmd.ExecuteNonQuery();
                } 
                for (int i = 0; i < hotOut.Count; i++)
                {
                    cmd.CommandText = "insert into " + tableName + " values(" + hotOut[i].SValue
                                                                             + "," + hotOut[i].AValue
                                                                             + "," + hotOut[i].K
                                                                             + "," + hotOut[i].B + ",4)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

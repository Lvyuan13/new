using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOX;
using System.Data.OleDb;
using System.Windows.Forms;

namespace RefMDBInquiry
{
    public  class DBRefDBOperation
    {



        /// <summary>
        /// 通过关键字线性插值查表计算
        /// </summary>
        /// <param name="_Path">mdb的路径</param>
        /// <param name="TabNameInSql">表名</param>
        /// <param name="TargetColumnName">目标列名</param>
        /// <param name="KeyColumnNameInSql">关键字列名</param>
        /// <param name="Key">DB数组:[0]-KeyValue;[1]-KeyDn;[2]-KeyUp;[3]-TargetDn;[4]-TargetUp;</param>
        /// <returns></returns>
        public static double[] SrcTabForPar(string DataBasePath, string DataBaseName, string TabName, string TargetColumnName, string KeyColumnName, double Key)
        {
            double[] ResDBArray = new double[5];//[0]-KeyValue;[1]-KeyDn;[2]-KeyUp;[3]-TargetDn;[4]-TargetUp;
            double TargetValue = 0;
            string KeyColumnNameInSql = "[" + KeyColumnName + "]";
            string TabNameInSql = "[" + TabName + "]";
            //try
            {
                //建立起数据库连接并进行查询
                //1.建立连接 
                string strConn
                    = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBasePath + DataBaseName + ".mdb";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2.打开连接 
                odcConnection.Open();
                //3.建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                odCommand.CommandText = "SELECT MIN(" + KeyColumnNameInSql + ") FROM " + TabNameInSql + " WHERE " + KeyColumnNameInSql + ">=" + Key.ToString();
                var KeyUp = odCommand.ExecuteScalar();
                odCommand.CommandText = "SELECT MAX(" + KeyColumnNameInSql + ") FROM " + TabNameInSql + " WHERE " + KeyColumnNameInSql + "<=" + Key.ToString();
                var KeyDn = odCommand.ExecuteScalar();




                if (KeyUp is DBNull)//key超过上限，查最大
                {
                    odCommand.CommandText = "SELECT " + TargetColumnName + " FROM " + TabNameInSql + " WHERE " + KeyColumnNameInSql + "=" + KeyDn.ToString();
                    TargetValue = (double)odCommand.ExecuteScalar();
                    odcConnection.Close();
                    ResDBArray[0] = TargetValue;
                    ResDBArray[3] = TargetValue;
                    ResDBArray[4] = TargetValue;
                    ResDBArray[1] = (double)KeyDn;
                    ResDBArray[2] = (double)KeyDn;
                    //return TargetValue;
                }// if KeyUp is DBNull
                else
                {
                    if (KeyDn is DBNull)//key超过下线，查最小
                    {
                        odCommand.CommandText = "SELECT " + TargetColumnName + " FROM " + TabNameInSql + " WHERE " + KeyColumnNameInSql + "=" + KeyUp.ToString();
                        TargetValue = (double)odCommand.ExecuteScalar();
                        odcConnection.Close();
                        //return TargetValue;
                        ResDBArray[0] = TargetValue;
                        ResDBArray[3] = TargetValue;
                        ResDBArray[4] = TargetValue;
                        ResDBArray[1] = (double)KeyUp;
                        ResDBArray[2] = (double)KeyUp;
                    }// if KeyDn is DBNull
                    else
                    {
                        if ((double)KeyDn == (double)KeyUp)//key刚好等于表内某个值，不插值，查一次表即可
                        {
                            odCommand.CommandText = "SELECT " + TargetColumnName + " FROM " + TabNameInSql + " WHERE " + KeyColumnNameInSql + "=" + KeyUp.ToString();
                            TargetValue = (double)odCommand.ExecuteScalar();
                            odcConnection.Close();
                            //return TargetValue;
                            ResDBArray[0] = TargetValue;
                            ResDBArray[3] = TargetValue;
                            ResDBArray[4] = TargetValue;
                     
                        }// if KeyDn equals to KeyUp
                        else
                        {
                            odCommand.CommandText = "SELECT " + TargetColumnName + " FROM " + TabNameInSql + " WHERE " + KeyColumnNameInSql + "=" + KeyUp.ToString();
                            var TargetUp = odCommand.ExecuteScalar();
                            odCommand.CommandText = "SELECT " + TargetColumnName + " FROM " + TabNameInSql + " WHERE " + KeyColumnNameInSql + "=" + KeyDn.ToString();
                            var TargetDn = odCommand.ExecuteScalar();
                            odcConnection.Close();
                            TargetValue = LinearInterpol(Key, (double)KeyUp, (double)KeyDn, (double)TargetUp, (double)TargetDn);
                            //return TargetValue;
                            ResDBArray[0] = TargetValue;
                            ResDBArray[3] = (double)TargetDn;
                            ResDBArray[4] = (double)TargetUp;
                        }// else KeyDn equals to KeyUp
                        ResDBArray[1] = (double)KeyDn;
                        ResDBArray[2] = (double)KeyUp;
                    }// else KeyDn is DBNull
                }// else KeyUp is DBNull
                return ResDBArray;
            }//try          
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
            //return ResDBArray;
        }// fun

        public static double LinearInterpol
            (double x, double xUp, double xDn, double yUp, double yDn)
        {
            if (xUp == xDn)
            {
                return yDn;
            }
            else
            {
                return (yDn + (yUp - yDn) * (x - xDn) / (xUp - xDn));
            }
        }//LinearInterpol end

        public static bool IsAmongTheRange(double x, double xUp, double xDn)
        {
            if (x >= xDn && x <= xUp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        


    }
}

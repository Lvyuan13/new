using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Utility
{
    public static class MDBComm
    {
        public static List<string[]> ReadAllStringFromTable(string DBPath, string TableName)
        {
            List<string[]> ResList = new List<string[]>();
            //try
            //{
            //string tableName = "试验信息";

            //1、建立连接 
            string strConn
                = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 

            OleDbCommand odCommand = odcConnection.CreateCommand();
            //3、输入查询语句,注意SQL语句的空格是不要少了
            //查询最后一条记录
            odCommand.CommandText = "SELECT COUNT(*) FROM " + TableName;  //要执行的 SQL 语句或存储过程。 默认值为空字符串。
            int RecordTotalCount = (int)odCommand.ExecuteScalar();


            odCommand.CommandText = "select * from " + TableName + " order by 顺序"; //+ " where 序号 = (select max(序号) from "+tableName+" )";


            //建立读取 
            OleDbDataReader odrReader = odCommand.ExecuteReader();
            int RecordCount = 0;
            //string StrFCUModel;
            //string StrFCUOwner;
            int ColNum = odrReader.FieldCount;
            while (odrReader.Read())//把结果集内的每一条记录给dt,odrReader.Read()表示记录指针成功指向下一条
            {

                RecordCount += 1;
                //if (RecordCount == RecordTotalCount)
                //{
                //StrFCUModel = odrReader[0].ToString();
                //StrFCUOwner = odrReader[1].ToString();
                string[] ResInLine = new string[ColNum];
                //2015

                //20151105
                if (TableName == "PLC_DOLIST" || TableName == "PLC_DILIST")
                {
                    for (int i = 0; i < ColNum - 1; i++)
                    {

                        ResInLine[i] = (string)odrReader[i];
                    }
                }
                //20151105
                if (TableName == "Table_Agilent")
                {
                    for (int i = 1; i < ColNum; i++)
                    {

                        ResInLine[i] = (string)odrReader[i];
                    }
                }
                //20151105
                if (TableName == "Table_Controller")
                {
                    for (int i = 0; i < ColNum - 1; i++)
                    {

                        ResInLine[i] = (string)odrReader[i];
                    }
                }


                ResList.Add(ResInLine);
            }
            //}
            //catch(Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}

            return ResList;
        }



        public static List<string[]> SendAllStringToTable(string DBPath, string TableName)
        {
            List<string[]> ResList = new List<string[]>();


            //1、建立连接 
            string strConn
                = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 



            return ResList;
        }
    }
}

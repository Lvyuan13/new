using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOX;

using System.Data.OleDb;

namespace BackPanel
{
    public static class DBOperate
    {


        #region 1新建数据库并添加相应的字段

        /// <summary>
        /// 根据获取的路径，新建数据库
        /// </summary>
        /// <param name="DBPath"></param>
        public static void CreateDateBase(string DBPath)
        {
            ADOX.CatalogClass cat = new CatalogClass();
            cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + DBPath + ";");
            //信息
            AddInfoTable(ref cat);
            //数据
            AddDataTable(ref cat, "CarCooling");
            AddDataTable(ref cat, "CarNoise");
            AddDataTable(ref cat, "ChillerNormialCondition");
            //AddDataTable(ref cat, "CompNormialConditionWaterFlow");
            AddDataTable(ref cat, "CompPartialCondition");
            AddDataTable(ref cat, "CompMaxCondition");
            AddDataTable(ref cat, "CompChangCondition");

            //报表
            AddReportDataTable(ref cat, "Report_CarCooling", 221);
            AddReportDataTable(ref cat, "Report_CarNoise", 221);
            AddReportDataTable(ref cat, "Report_CompNorminalCondition", 188);
            //AddReportDataTable(ref cat, "Report_CompNormialConditionWaterFlow", 18);
            AddReportDataTable(ref cat, "Report_CompPartialCondition", 188);
            AddReportDataTable(ref cat, "Report_CompMaxCondition", 188);
            AddReportDataTable(ref cat, "Report_CompChangCondition", 188);
            cat = null;

        }

        public static void AddColumn(ref CatalogClass Catlog, ref TableClass Table, string ColName, DataTypeEnum DataType)
        {

            //增加一个自动增长的字段
            ADOX.ColumnClass _col = new ColumnClass();
            _col.ParentCatalog = Catlog;
            _col.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
            _col.Name = ColName;

            if (DataType == DataTypeEnum.adDouble)
            {
                _col.Type = DataTypeEnum.adDouble;
                Table.Columns.Append(_col, DataTypeEnum.adDouble);
            }
            if (DataType == DataTypeEnum.adVarChar)
            {
                Table.Columns.Append(_col, DataTypeEnum.adVarChar, 50);
            }
            if (DataType == DataTypeEnum.adBoolean)
            {
                _col.Type = DataTypeEnum.adBoolean;
                Table.Columns.Append(_col, DataTypeEnum.adBoolean);
                _col = null;
            }
            if (DataType == DataTypeEnum.adInteger)
            {
                _col.Type = DataTypeEnum.adInteger;
                _col.Properties["AutoIncrement"].Value = true;
                Table.Columns.Append(_col, ADOX.DataTypeEnum.adInteger);
            }


        }


        #region 三种表：信息表，数据表，报表表三种
        /// <summary>
        /// 添加试验信息所有字段，包含压缩机和冷凝机组
        /// </summary>
        /// <param name="Catlog"></param>
        public static void AddInfoTable(ref CatalogClass Catlog)
        {
            ADOX.TableClass tbl = new TableClass();
            tbl.ParentCatalog = Catlog;
            tbl.Name = "试验信息";

            //添加字段
            AddColumn(ref Catlog, ref tbl, "试验日期", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "试验时间", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "机组类型", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "制造商", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "型号规格", DataTypeEnum.adVarChar);
            //AddCoulmn(ref Catlog, ref tbl, "操作", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "出厂编号", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "名义冷量", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "名义功率", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "名义转速", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "皮带轮直径", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "名义水流量", DataTypeEnum.adVarChar);
            //20151020添加
            AddColumn(ref Catlog, ref tbl, "冷水机组冷却水流量/出水温度", DataTypeEnum.adVarChar);
            AddColumn(ref Catlog, ref tbl, "制冷剂", DataTypeEnum.adVarChar);
            //20151108
            AddColumn(ref Catlog, ref tbl, "离合器电压", DataTypeEnum.adVarChar);


            //把表加入数据库
            Catlog.Tables.Append(tbl);
            //MessageBox.Show("数据库表：" + tbl.Name + "已经创建成功！");
            tbl = null;

        }
        /// <summary>
        /// 添加数据表：包含实验数据的储存
        /// </summary>
        /// <param name="Catlog">试验场景名称：CarCooling，CarNoise，CompNormialConditionTemp, CompNormialConditionWaterFlow,CompPartialCondition,CompMaxCondition,CompChangCondition</param>
        /// <param name="TableName"></param>
        private static void AddDataTable(ref CatalogClass Catlog, string TableName)
        {
            //试验场景名称：CarCooling，CarNoise，CompNormialConditionTemp, CompNormialConditionWaterFlow,CompPartialCondition,CompMaxCondition,CompChangCondition
            //    string TableName = TestName;

            //建立运行数据表
            ADOX.TableClass tbl = new ADOX.TableClass();
            tbl.ParentCatalog = Catlog;
            tbl.Name = TableName;
            //ADOX.ColumnClass _col0 = new ADOX.ColumnClass();
            //添加字段
            AddColumn(ref Catlog, ref tbl, "时间戳", DataTypeEnum.adVarChar);//字段1
            AddColumn(ref Catlog, ref tbl, "0压缩机/冷水机组出口制冷剂温度℃", DataTypeEnum.adVarChar);//字段2
            AddColumn(ref Catlog, ref tbl, "1冷凝器进口制冷剂气体温度℃", DataTypeEnum.adVarChar);//字段3
            AddColumn(ref Catlog, ref tbl, "2冷凝器出口制冷剂液体温度℃", DataTypeEnum.adVarChar);//字段4
            AddColumn(ref Catlog, ref tbl, "3膨胀阀前制冷剂液体温度℃", DataTypeEnum.adVarChar);//字段5
            AddColumn(ref Catlog, ref tbl, "4量热器出口制冷剂气体温度℃", DataTypeEnum.adVarChar);//字段6
            AddColumn(ref Catlog, ref tbl, "5压缩机/冷水机组入口制冷剂温度℃", DataTypeEnum.adVarChar);//字段7
            AddColumn(ref Catlog, ref tbl, "6环境温度℃", DataTypeEnum.adVarChar);//字段8
            AddColumn(ref Catlog, ref tbl, "7冷却水进口温度℃", DataTypeEnum.adVarChar);//字段9
            AddColumn(ref Catlog, ref tbl, "8冷却水出口温度℃", DataTypeEnum.adVarChar);//字段10
            AddColumn(ref Catlog, ref tbl, "9冷凝机组冷却水出水温度℃", DataTypeEnum.adVarChar);//字段11
            AddColumn(ref Catlog, ref tbl, "10冷凝机组冷却水进水温度℃", DataTypeEnum.adVarChar);//字段12
            AddColumn(ref Catlog, ref tbl, "11恒温水槽供温度℃", DataTypeEnum.adVarChar);//字段13
            AddColumn(ref Catlog, ref tbl, "12压缩机箱温度℃", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "13压缩机扭矩Nm", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "14压缩机/冷水机组出口制冷剂压力MPa", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "15冷凝器进口制冷剂气体压力MPa", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "16冷凝器出口制冷剂液体压力MPa", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "17膨胀阀前制冷剂液体压力MPa", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "18量热器第二制冷剂压力MPa", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "19量热器出口制冷剂气体压力MPa", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "20压缩机/冷水机组入口制冷剂压力MPa", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "21压缩机转速rpm", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "22恒温水槽回水流量m3/h", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "23量热器输入功率kW", DataTypeEnum.adVarChar);//字段14

            AddColumn(ref Catlog, ref tbl, "25电压A_V", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "26电压B_V", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "27电压C_V", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "28电流A_V", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "29电流B_V", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "30电流C_V", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "31功率kW", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "32功率因数", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "33频率Hz", DataTypeEnum.adVarChar);//字段14

            AddColumn(ref Catlog, ref tbl, "34UT1输出百分比%", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "35UT2输出百分比%", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "36UT3输出百分比%", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "37UT4输出百分比%", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "38UT5输出百分比%", DataTypeEnum.adVarChar);//字段14
            AddColumn(ref Catlog, ref tbl, "39UT6输出百分比%", DataTypeEnum.adVarChar);//字段14

            switch (TableName)
            {
                case "CarCooling":

                case "CarNoise":
                    AddColumn(ref Catlog, ref tbl, "40A方法制冷剂流量 kg/s", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "41A方法制冷量 kW", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "42G方法制冷剂流量 kg/s", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "43G方法制冷量 kW", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "44AG方法偏差 %", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "45压缩机功率 kW", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "46COP", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "47吸气实际比体积 m3/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "48冷凝器进口比焓 kJ/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "49冷凝器出口比焓 kJ/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "50膨胀阀进口比焓 kJ/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "51第二制冷剂温度 ℃", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "52量热器出口比焓 kJ/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "53水比热容 kJ/(kg ℃)", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "54水密度kg/m3", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "55量热器漏热量 kW", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "56冷凝器换热量 kW", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "57冷凝器漏热量 kW", DataTypeEnum.adVarChar);//字段14
                    break;
                case "ChillerNormialCondition":

                //case "CompNormialConditionWaterFlow":
                //    break;
                case "CompPartialCondition":

                case "CompMaxCondition":

                case "CompChangCondition":
                    AddColumn(ref Catlog, ref tbl, "40制冷剂流量kg/s", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "41制冷量kW", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "42COP", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "43吸气实际比体积 m3/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "44供液比焓 kJ/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "45膨胀阀进口比焓 kJ/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "46第二制冷剂温度℃", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "47量热器出库比焓 kJ/kg", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "48水比热容 kJ/(kg ℃)", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "49水密度kg/m3", DataTypeEnum.adVarChar);//字段14
                    AddColumn(ref Catlog, ref tbl, "50量热器漏热量 kW", DataTypeEnum.adVarChar);//字段14




                    break;


            }
            //"CarCooling");
            //AddDataTable(ref cat, "CarNoise");
            //AddDataTable(ref cat, "CompNormialConditionTemp");
            //AddDataTable(ref cat, "CompNormialConditionWaterFlow");
            //AddDataTable(ref cat, "CompPartialCondition");
            //AddDataTable(ref cat, "CompMaxCondition");
            //AddDataTable(ref cat, "CompChangCondition");


            Catlog.Tables.Append(tbl);
            //MessageBox.Show("数据库表：" + tbl.Name + "已经创建成功！");
            tbl = null;

        }
        ///// <summary>
        ///// 添加试验数据表！包括各个试验的数据信息********无用！！！
        ///// </summary>
        ///// <param name="DBPath"></param>
        ///// <param name="TestName">试验场景名称：CarCooling，CarNoise，CompNormialConditionTemp, CompNormialConditionWaterFlow,CompPartialCondition,CompMaxCondition,CompChangCondition</param>
        //public static void AddDataRecordToDateBase(string DBPath, string TestName)
        //{
        //    //试验场景名称：CarCooling，CarNoise，CompNormialConditionTemp, CompNormialConditionWaterFlow,CompPartialCondition,CompMaxCondition,CompChangCondition
        //    string TableName = TestName;

        //    //试验名称：CarCoolingTest; CarNoiseTest; WaterNormalLoadTest; WaterPartialLoadTest;  WaterMaxLoadTest; WaterChangeLoad;
        //    //if (TestName == "CarCoolingTest")
        //    //{
        //    //    TableName = "汽车空调压缩机制冷试验";
        //    //}
        //    //if (TestName == "CarNoiseTest")
        //    //{
        //    //    TableName = "汽车空调压缩机噪声试验";
        //    //}
        //    //if (TestName == "WaterNormalLoadTest")
        //    //{
        //    //    TableName = "水冷压缩冷凝机组名义工况试验";
        //    //}
        //    //if (TestName == "WaterPartialLoadTest")
        //    //{
        //    //    TableName = "水冷压缩冷凝机组部分负荷试验";
        //    //}
        //    //if (TestName == "WaterMaxLoadTest")
        //    //{
        //    //    TableName = "水冷压缩冷凝机组最大负荷试验";
        //    //}
        //    //if (TestName == "WaterChangeLoad")
        //    //{
        //    //    TableName = "水冷压缩冷凝机组变工况试验";
        //    //}

        //    try
        //    //1.建立连接
        //    {
        //        string strConn
        //             = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath;
        //        OleDbConnection odcConnection = new OleDbConnection(strConn);
        //        //2、打开连接 
        //        odcConnection.Open();
        //        //建立Command实例 
        //        OleDbCommand odCommand = odcConnection.CreateCommand();
        //        //3、输入SQL语句,注意SQL语句的空格是不要少了
        //        //odCommand.CommandText = "INSERT INTO 试验信息 VALUES ('2014-5-21','15:27','Unknown','Unknown','Unknown','开始',0,0,0,0,0,0,0,0)";    
        //        odCommand.CommandText = "INSERT INTO " + TableName + " VALUES("
        //                               + "'" + TestInfoDef.TestInfo[0] + "',"
        //                               + "'" + TestInfoDef.TestInfo[1] + "',"
        //                               + "'" + TestInfoDef.TestInfo[2] + "',"
        //                               + "'" + TestInfoDef.TestInfo[3] + "',"
        //                               + "'" + TestInfoDef.TestInfo[4] + "',"
        //                               + "'" + TestInfoDef.TestInfo[5] + "',"
        //                               + "'" + TestInfoDef.TestInfo[6] + "',"
        //                               + "'" + TestInfoDef.TestInfo[7] + "',"
        //                               + "'" + TestInfoDef.TestInfo[8] + "',"
        //                               + "'" + TestInfoDef.TestInfo[9] + "',"
        //                               + "'" + TestInfoDef.TestInfo[10] + "',"
        //                               + "'" + TestInfoDef.TestInfo[11] + "',"
        //                               + "'" + TestInfoDef.TestInfo[12] + "',"
        //                               + "'" + TestInfoDef.TestInfo[13] + "'"
        //                               + ")";

        //        //执行语句
        //        OleDbDataReader odrReader = odCommand.ExecuteReader();

        //        //关闭连接
        //        odrReader.Close();
        //        odcConnection.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());

        //    }

        //}
        /// <summary>
        /// 添加报表：表
        /// </summary>
        /// <param name="Catlog"></param>
        /// <param name="TableName"></param>
        /// <param name="ColTotalNum"></param>
        private static void AddReportDataTable(ref CatalogClass Catlog, string TableName, int ColTotalNum)
        {
            //建立报表数据表
            ADOX.TableClass tbl = new ADOX.TableClass();
            tbl.ParentCatalog = Catlog;
            tbl.Name = TableName;
            //ADOX.ColumnClass _col0 = new ADOX.ColumnClass();
            //添加字段
            for (int ColNum = 1; ColNum <= ColTotalNum; ColNum++)
            {
                AddColumn(ref Catlog, ref tbl, ColNum.ToString(), DataTypeEnum.adVarChar);//字段1
            }
            Catlog.Tables.Append(tbl);
            tbl = null;
        }
        #endregion 三种表：信息表，数据表，报表表三种

        #endregion

        #region 2获取最后一条记录
        /// <summary>
        /// 获取最后一条记录的所有字段，包括压缩机和冷凝机组
        /// </summary>
        /// <param name="DBPath"></param>
        /// <param name="TableName"></param>
        public static void GetLastInfoRecordFromDateBase(string DBPath, string[] InfoArray)
        {
            //以下方法为调取数据库所有记录，然后选择最后一条记录
            //List<string[]> InfoRecrdListFromDB = Utility.MDBComm.ReadAllStringFromTable(DBPath,TableName);  //从读取字符串表中建立List
            //int ListCount = InfoRecrdListFromDB.Count -1;

            ////获取最后一条记录的所有字段
            //for(int i=0;i<11;i++)
            //{
            //    InfoArray[i] = InfoRecrdListFromDB[ListCount][i];
            //}

            string tableName = "试验信息";
            // 读取mdb数据 
            //try
            //{
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
            odCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
            int RecordTotalCount = (int)odCommand.ExecuteScalar();  //ExecuteScalar用法  //总的读取记录数

            odCommand.CommandText = "select * from " + tableName;

            //建立读取 
            OleDbDataReader odrReader = odCommand.ExecuteReader(); //读取数据库
            int ColumnNum = odrReader.FieldCount;
            int RecordCount = 0;
            while (odrReader.Read())
            {

                RecordCount += 1;
                if (RecordCount == RecordTotalCount)
                {
                    for (int i = 0; i < ColumnNum; i++)
                    {
                        InfoArray[i] = (string)odrReader[i];
                    }

                }

            }

            //关闭连接 
            odrReader.Close();
            odcConnection.Close();  //OleDbConnection实例

        }
        #endregion

        #region 3插入记录

        #region 初始化字段 :到时可以放到全局变量！Globel
        //现在自己在全局变量也见了一个InfoVarGlo，到时把他赋给这个变量，20150906
        public class InfoVar
        {
            //public bool CarOrWaterCompressor=false; //默认0，汽车空调压缩机 ；1为水冷压缩冷凝机组  

            public string[] TestInfo = new string[15];
        }
        public static InfoVar TestInfoDef = new InfoVar();
        #endregion 初始化字段


        /// <summary>
        /// 在试验信息表中添加内容
        /// </summary>
        /// <param name="DBPath"></param>
        /// <param name="operate"></param>
        public static void AddInfoRecordToDateBase(string DBPath)
        {
            string tableName = "试验信息";
            // 读取mdb数据 
            //try
            //{
            //1、建立连接 
            string strConn
                = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立Command实例 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            //3、输入SQL语句,注意SQL语句的空格是不要少了
            //odCommand.CommandText = "INSERT INTO 试验信息 VALUES ('2014-5-21','15:27','Unknown','Unknown','Unknown','开始',0,0,0,0,0,0,0,0)";               
            odCommand.CommandText = "INSERT INTO " + tableName + " VALUES("
                                     + "'" + TestInfoDef.TestInfo[0] + "',"
                                     + "'" + TestInfoDef.TestInfo[1] + "',"
                                     + "'" + TestInfoDef.TestInfo[2] + "',"
                                     + "'" + TestInfoDef.TestInfo[3] + "',"
                                     + "'" + TestInfoDef.TestInfo[4] + "',"
                                     + "'" + TestInfoDef.TestInfo[5] + "',"
                                     + "'" + TestInfoDef.TestInfo[6] + "',"
                                     + "'" + TestInfoDef.TestInfo[7] + "',"
                                     + "'" + TestInfoDef.TestInfo[8] + "',"
                                     + "'" + TestInfoDef.TestInfo[9] + "',"
                                     + "'" + TestInfoDef.TestInfo[10] + "',"
                                     + "'" + TestInfoDef.TestInfo[11] + "',"
                                     + "'" + TestInfoDef.TestInfo[12] + "',"   //
                                     + "'" + TestInfoDef.TestInfo[13] + "'"   //压缩机离合器电压24V/12V
                                     + ")";

            //执行语句
            OleDbDataReader odrReader = odCommand.ExecuteReader();

            //关闭连接
            odrReader.Close();
            odcConnection.Close();

            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}

        }

        /// <summary>
        /// 把个别的AGILENT采集数据采集到，相应试验的数据库中！20150917（试验）
        /// </summary>
        /// <param name="DBPath"></param>
        /// <param name="TableName"></param>
        public static void InsertRecordDataTODBTotal(string DBPath, InformationGlo.Senario TestName, List<BackPanel.Agilent.AgilentVar> AgilentList, double[] WT310DataCOM3, List<BackPanel.Control.UT35ADef> Controllist, double[] CarCalculateResult_Car, double[] ChillerCalculateResult_Chiller, double[] Others)
        {
            //try
            //{
            //1、建立连接 
            string strConn
                = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立Command实例 
            OleDbCommand odCommand = odcConnection.CreateCommand();

            string TableName = "";
            switch (TestName)
            {
                case InformationGlo.Senario.CarCooling:
                    TableName = "CarCooling";
                    //3、输入SQL语句,注意SQL语句的空格是不要少了
                    //odCommand.CommandText = "INSERT INTO 试验信息 VALUES ('2014-5-21','15:27','Unknown','Unknown','Unknown','开始',0,0,0,0,0,0,0,0)";
                    odCommand.CommandText = "INSERT INTO " + TableName + " VALUES ("
                                                + "'" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString() + "',"
                                                + "'" + AgilentList[0].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[1].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[2].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[3].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[4].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[5].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[6].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[7].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[8].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"   // AgilentList[9].TargetCorr.ToString() 9和11被取消，换为UT的pv20150924
                                                + "'" + AgilentList[9].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"
                                                + "'" + AgilentList[10].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[11].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[12].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[13].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[14].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[15].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[16].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[17].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[18].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[19].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[20].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[21].TargetCorr.ToString() + "',"

                                                + "'" + WT310DataCOM3[0].ToString() + "',"
                                                + "'" + WT310DataCOM3[1].ToString() + "',"
                                                + "'" + WT310DataCOM3[2].ToString() + "',"
                                                + "'" + WT310DataCOM3[3].ToString() + "',"
                                                + "'" + WT310DataCOM3[4].ToString() + "',"
                                                + "'" + WT310DataCOM3[5].ToString() + "',"
                                                + "'" + WT310DataCOM3[6].ToString() + "',"
                                                + "'" + WT310DataCOM3[7].ToString() + "',"
                                                + "'" + WT310DataCOM3[8].ToString() + "',"

                                                + "'" + Controllist[0].OUT.ToString() + "',"
                                                + "'" + Controllist[1].OUT.ToString() + "',"
                                                + "'" + Controllist[2].OUT.ToString() + "',"
                                                + "'" + Controllist[3].OUT.ToString() + "',"
                                                + "'" + Controllist[4].OUT.ToString() + "',"
                                                + "'" + Controllist[3].PV.ToString() + "',"

                                                 + "'" + CarCalculateResult_Car[0].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[1].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[2].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[3].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[4].ToString() + "',"
                                                 + "'" + CarCalculateResult_Car[5].ToString() + "',"
                                                  + "'" + CarCalculateResult_Car[6].ToString() + "',"

                                                   + "'" + Others[0].ToString() + "',"
                                                    + "'" + Others[1].ToString() + "',"
                                                     + "'" + Others[2].ToString() + "',"
                                                      + "'" + Others[3].ToString() + "',"
                                                       + "'" + Others[4].ToString() + "',"
                                                        + "'" + Others[5].ToString() + "',"
                                                        + "'" + Others[6].ToString() + "',"
                                                         + "'" + Others[7].ToString() + "',"
                                                          + "'" + Others[8].ToString() + "',"
                                                          + "'" + Others[9].ToString() + "',"
                                                          + "'" + Others[10].ToString() + "'"
                        //+ "'" + Others[11].ToString() + "'"

                                                + ")";
                    break;
                case InformationGlo.Senario.CarNoise:
                    TableName = "CarNoise";
                    //3、输入SQL语句,注意SQL语句的空格是不要少了
                    //odCommand.CommandText = "INSERT INTO 试验信息 VALUES ('2014-5-21','15:27','Unknown','Unknown','Unknown','开始',0,0,0,0,0,0,0,0)";
                    odCommand.CommandText = "INSERT INTO " + TableName + " VALUES ("
                                                + "'" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString() + "',"
                                                + "'" + AgilentList[0].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[1].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[2].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[3].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[4].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[5].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[6].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[7].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[8].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"   // AgilentList[9].TargetCorr.ToString() 9和11被取消，换为UT的pv20150924
                                                + "'" + AgilentList[9].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"
                                                + "'" + AgilentList[10].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[11].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[12].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[13].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[14].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[15].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[16].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[17].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[18].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[19].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[20].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[21].TargetCorr.ToString() + "',"

                                                + "'" + WT310DataCOM3[0].ToString() + "',"
                                                + "'" + WT310DataCOM3[1].ToString() + "',"
                                                + "'" + WT310DataCOM3[2].ToString() + "',"
                                                + "'" + WT310DataCOM3[3].ToString() + "',"
                                                + "'" + WT310DataCOM3[4].ToString() + "',"
                                                + "'" + WT310DataCOM3[5].ToString() + "',"
                                                + "'" + WT310DataCOM3[6].ToString() + "',"
                                                + "'" + WT310DataCOM3[7].ToString() + "',"
                                                + "'" + WT310DataCOM3[8].ToString() + "',"

                                                + "'" + Controllist[0].OUT.ToString() + "',"
                                                + "'" + Controllist[1].OUT.ToString() + "',"
                                                + "'" + Controllist[2].OUT.ToString() + "',"
                                                + "'" + Controllist[3].OUT.ToString() + "',"
                                                + "'" + Controllist[4].OUT.ToString() + "',"
                                                + "'" + Controllist[3].PV.ToString() + "',"

                                                + "'" + CarCalculateResult_Car[0].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[1].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[2].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[3].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[4].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[5].ToString() + "',"
                                                + "'" + CarCalculateResult_Car[6].ToString() + "',"

                                                + "'" + Others[0].ToString() + "',"
                                                + "'" + Others[1].ToString() + "',"
                                                + "'" + Others[2].ToString() + "',"
                                                + "'" + Others[3].ToString() + "',"
                                                + "'" + Others[4].ToString() + "',"
                                                + "'" + Others[5].ToString() + "',"
                                                + "'" + Others[6].ToString() + "',"
                                                + "'" + Others[7].ToString() + "',"
                                                + "'" + Others[8].ToString() + "',"
                                                + "'" + Others[9].ToString() + "',"
                                                + "'" + Others[10].ToString() + "'"
                        //+ "'" + Others[11].ToString() + "'"
                        // + "'" + CarCalculateResult_Car[1].ToString() + "',"
                        //+ "'" + CarCalculateResult_Car[3].ToString() + "',"d
                        //+ "'" + CarCalculateResult_Car[4].ToString() + "',"
                        //+ "'" + CarCalculateResult_Car[6].ToString() + "'"
                                                + ")";
                    break;
                case InformationGlo.Senario.ChillerChangCondition:
                    TableName = "CompChangCondition";

                    odCommand.CommandText = "INSERT INTO " + TableName + " VALUES ("
                                                + "'" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString() + "',"
                                                + "'" + AgilentList[0].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[1].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[2].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[3].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[4].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[5].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[6].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[7].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[8].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"   // AgilentList[9].TargetCorr.ToString() 9和11被取消，换为UT的pv20150924
                                                + "'" + AgilentList[9].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"
                                                + "'" + AgilentList[10].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[11].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[12].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[13].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[14].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[15].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[16].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[17].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[18].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[19].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[20].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[21].TargetCorr.ToString() + "',"

                                                + "'" + WT310DataCOM3[0].ToString() + "',"
                                                + "'" + WT310DataCOM3[1].ToString() + "',"
                                                + "'" + WT310DataCOM3[2].ToString() + "',"
                                                + "'" + WT310DataCOM3[3].ToString() + "',"
                                                + "'" + WT310DataCOM3[4].ToString() + "',"
                                                + "'" + WT310DataCOM3[5].ToString() + "',"
                                                + "'" + WT310DataCOM3[6].ToString() + "',"
                                                + "'" + WT310DataCOM3[7].ToString() + "',"
                                                + "'" + WT310DataCOM3[8].ToString() + "',"

                                                + "'" + Controllist[0].OUT.ToString() + "',"
                                                + "'" + Controllist[1].OUT.ToString() + "',"
                                                + "'" + Controllist[2].OUT.ToString() + "',"
                                                + "'" + Controllist[3].OUT.ToString() + "',"
                                                + "'" + Controllist[4].OUT.ToString() + "',"
                                                + "'" + Controllist[3].PV.ToString() + "',"


                                                //+ "'" + Calculate.CalculateCar.A_CoolingCapacity.ToString() + "',"  //1
                        //+ "'" + Calculate.CalculateCar.G_CoolingCapacity.ToString() + "',"  //3
                        //+ "'" + Calculate.CalculateCar.TestErr.ToString() + "',"    //4
                        //+ "'" + Calculate.CalculateCar.AG_COP.ToString() + "'"      //6
                                                 + "'" + ChillerCalculateResult_Chiller[0].ToString() + "',"
                                                + "'" + ChillerCalculateResult_Chiller[1].ToString() + "',"
                                                + "'" + ChillerCalculateResult_Chiller[2].ToString() + "',"


                                                + "'" + Others[0].ToString() + "',"
                                                + "'" + Others[1].ToString() + "',"
                                                + "'" + Others[2].ToString() + "',"
                                                + "'" + Others[3].ToString() + "',"
                                                + "'" + Others[4].ToString() + "',"
                                                + "'" + Others[5].ToString() + "',"
                                                + "'" + Others[6].ToString() + "',"
                                                + "'" + Others[7].ToString() + "'"

                                                //+ "'" + ChillerCalculateResult_Chiller[1].ToString() + "',"
                        //+ "'" + ChillerCalculateResult_Chiller[1].ToString() + "',"
                        //+ "'" + "--" + "',"
                        //+ "'" + ChillerCalculateResult_Chiller[2].ToString() + "'"
                        //+ "1" + ","
                        //+ "1" + ","
                        //+ "1"
                                                + ")";
                    break;
                case InformationGlo.Senario.ChillerMaxCondition:
                    TableName = "CompMaxCondition";
                    odCommand.CommandText = "INSERT INTO " + TableName + " VALUES ("
                                                + "'" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString() + "',"
                                                + "'" + AgilentList[0].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[1].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[2].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[3].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[4].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[5].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[6].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[7].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[8].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"   // AgilentList[9].TargetCorr.ToString() 9和11被取消，换为UT的pv20150924
                                                + "'" + AgilentList[9].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"
                                                + "'" + AgilentList[10].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[11].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[12].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[13].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[14].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[15].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[16].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[17].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[18].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[19].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[20].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[21].TargetCorr.ToString() + "',"

                                                + "'" + WT310DataCOM3[0].ToString() + "',"
                                                + "'" + WT310DataCOM3[1].ToString() + "',"
                                                + "'" + WT310DataCOM3[2].ToString() + "',"
                                                + "'" + WT310DataCOM3[3].ToString() + "',"
                                                + "'" + WT310DataCOM3[4].ToString() + "',"
                                                + "'" + WT310DataCOM3[5].ToString() + "',"
                                                + "'" + WT310DataCOM3[6].ToString() + "',"
                                                + "'" + WT310DataCOM3[7].ToString() + "',"
                                                + "'" + WT310DataCOM3[8].ToString() + "',"

                                                + "'" + Controllist[0].OUT.ToString() + "',"
                                                + "'" + Controllist[1].OUT.ToString() + "',"
                                                + "'" + Controllist[2].OUT.ToString() + "',"
                                                + "'" + Controllist[3].OUT.ToString() + "',"
                                                + "'" + Controllist[4].OUT.ToString() + "',"
                                                + "'" + Controllist[3].PV.ToString() + "',"

                                                //+ "'" + Calculate.CalculateCar.A_CoolingCapacity.ToString() + "',"  //1
                        //+ "'" + Calculate.CalculateCar.G_CoolingCapacity.ToString() + "',"  //3
                        //+ "'" + Calculate.CalculateCar.TestErr.ToString() + "',"    //4
                        //+ "'" + Calculate.CalculateCar.AG_COP.ToString() + "'"      //6
                                                 + "'" + ChillerCalculateResult_Chiller[0].ToString() + "',"
                                                + "'" + ChillerCalculateResult_Chiller[1].ToString() + "',"
                                                + "'" + ChillerCalculateResult_Chiller[2].ToString() + "',"


                                                + "'" + Others[0].ToString() + "',"
                                                + "'" + Others[1].ToString() + "',"
                                                + "'" + Others[2].ToString() + "',"
                                                + "'" + Others[3].ToString() + "',"
                                                + "'" + Others[4].ToString() + "',"
                                                + "'" + Others[5].ToString() + "',"
                                                + "'" + Others[6].ToString() + "',"
                                                + "'" + Others[7].ToString() + "'"
                        //+ "1" + ","
                        //+ "1" + ","
                        //+ "1"
                                                + ")";
                    break;
                case InformationGlo.Senario.ChillerNormialCondition:
                    //TableName = "CompNormialConditionTemp";
                    TableName = "ChillerNormialCondition";
                    odCommand.CommandText = "INSERT INTO " + TableName + " VALUES ("
                                                + "'" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString() + "',"
                                                + "'" + AgilentList[0].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[1].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[2].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[3].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[4].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[5].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[6].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[7].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[8].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"   // AgilentList[9].TargetCorr.ToString() 9和11被取消，换为UT的pv20150924
                                                + "'" + AgilentList[9].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"
                                                + "'" + AgilentList[10].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[11].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[12].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[13].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[14].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[15].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[16].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[17].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[18].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[19].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[20].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[21].TargetCorr.ToString() + "',"

                                                + "'" + WT310DataCOM3[0].ToString() + "',"
                                                + "'" + WT310DataCOM3[1].ToString() + "',"
                                                + "'" + WT310DataCOM3[2].ToString() + "',"
                                                + "'" + WT310DataCOM3[3].ToString() + "',"
                                                + "'" + WT310DataCOM3[4].ToString() + "',"
                                                + "'" + WT310DataCOM3[5].ToString() + "',"
                                                + "'" + WT310DataCOM3[6].ToString() + "',"
                                                + "'" + WT310DataCOM3[7].ToString() + "',"
                                                + "'" + WT310DataCOM3[8].ToString() + "',"

                                                + "'" + Controllist[0].OUT.ToString() + "',"
                                                + "'" + Controllist[1].OUT.ToString() + "',"
                                                + "'" + Controllist[2].OUT.ToString() + "',"
                                                + "'" + Controllist[3].OUT.ToString() + "',"
                                                + "'" + Controllist[4].OUT.ToString() + "',"
                                                + "'" + Controllist[3].PV.ToString() + "',"

                                                //+ "'" + Calculate.CalculateCar.A_CoolingCapacity.ToString() + "',"  //1
                        //+ "'" + Calculate.CalculateCar.G_CoolingCapacity.ToString() + "',"  //3
                        //+ "'" + Calculate.CalculateCar.TestErr.ToString() + "',"    //4
                        //+ "'" + Calculate.CalculateCar.AG_COP.ToString() + "'"      //6
                         + "'" + ChillerCalculateResult_Chiller[0].ToString() + "',"
                                                + "'" + ChillerCalculateResult_Chiller[1].ToString() + "',"
                                                + "'" + ChillerCalculateResult_Chiller[2].ToString() + "',"


                                                + "'" + Others[0].ToString() + "',"
                                                + "'" + Others[1].ToString() + "',"
                                                + "'" + Others[2].ToString() + "',"
                                                + "'" + Others[3].ToString() + "',"
                                                + "'" + Others[4].ToString() + "',"
                                                + "'" + Others[5].ToString() + "',"
                                                + "'" + Others[6].ToString() + "',"
                                                + "'" + Others[7].ToString() + "'"
                        // + "'" + ChillerCalculateResult_Chiller[0].ToString() + "',"
                        //+ "'" + ChillerCalculateResult_Chiller[1].ToString() + "',"
                        //+ "'" + "--" + "',"
                        //+ "'" + ChillerCalculateResult_Chiller[2].ToString() + "'"
                        //+ "1" + ","
                        //+ "1" + ","
                        //+ "1"
                                                + ")";
                    break;

                case InformationGlo.Senario.ChillerPartialCondition:
                    TableName = "CompPartialCondition";
                    odCommand.CommandText = "INSERT INTO " + TableName + " VALUES ("
                                                + "'" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString() + "',"
                                                + "'" + AgilentList[0].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[1].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[2].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[3].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[4].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[5].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[6].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[7].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[8].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"   // AgilentList[9].TargetCorr.ToString() 9和11被取消，换为UT的pv20150924
                                                + "'" + AgilentList[9].TargetCorr.ToString() + "',"
                                                + "'" + Controllist[4].PV.ToString() + "',"
                                                + "'" + AgilentList[10].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[11].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[12].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[13].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[14].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[15].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[16].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[17].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[18].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[19].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[20].TargetCorr.ToString() + "',"
                                                + "'" + AgilentList[21].TargetCorr.ToString() + "',"

                                                + "'" + WT310DataCOM3[0].ToString() + "',"
                                                + "'" + WT310DataCOM3[1].ToString() + "',"
                                                + "'" + WT310DataCOM3[2].ToString() + "',"
                                                + "'" + WT310DataCOM3[3].ToString() + "',"
                                                + "'" + WT310DataCOM3[4].ToString() + "',"
                                                + "'" + WT310DataCOM3[5].ToString() + "',"
                                                + "'" + WT310DataCOM3[6].ToString() + "',"
                                                + "'" + WT310DataCOM3[7].ToString() + "',"
                                                + "'" + WT310DataCOM3[8].ToString() + "',"

                                                + "'" + Controllist[0].OUT.ToString() + "',"
                                                + "'" + Controllist[1].OUT.ToString() + "',"
                                                + "'" + Controllist[2].OUT.ToString() + "',"
                                                + "'" + Controllist[3].OUT.ToString() + "',"
                                                + "'" + Controllist[4].OUT.ToString() + "',"
                                                + "'" + Controllist[3].PV.ToString() + "',"

                                                //+ "'" + Calculate.CalculateCar.A_CoolingCapacity.ToString() + "',"  //1
                        //+ "'" + Calculate.CalculateCar.G_CoolingCapacity.ToString() + "',"  //3
                        //+ "'" + Calculate.CalculateCar.TestErr.ToString() + "',"    //4
                        //+ "'" + Calculate.CalculateCar.AG_COP.ToString() + "'"      //6
                         + "'" + ChillerCalculateResult_Chiller[0].ToString() + "',"
                                                + "'" + ChillerCalculateResult_Chiller[1].ToString() + "',"
                                                + "'" + ChillerCalculateResult_Chiller[2].ToString() + "',"


                                                + "'" + Others[0].ToString() + "',"
                                                + "'" + Others[1].ToString() + "',"
                                                + "'" + Others[2].ToString() + "',"
                                                + "'" + Others[3].ToString() + "',"
                                                + "'" + Others[4].ToString() + "',"
                                                + "'" + Others[5].ToString() + "',"
                                                + "'" + Others[6].ToString() + "',"
                                                + "'" + Others[7].ToString() + "'"
                        // + "'" + ChillerCalculateResult_Chiller[0].ToString() + "',"
                        //+ "'" + ChillerCalculateResult_Chiller[1].ToString() + "',"
                        //+ "'" + "--" + "',"
                        //+ "'" + ChillerCalculateResult_Chiller[2].ToString() + "'"
                        //+ "1" + ","
                        //+ "1" + ","
                        //+ "1"
                                                + ")";
                    break;
            }



            //执行语句
            OleDbDataReader odrReader = odCommand.ExecuteReader();


            //关闭连接 
            odrReader.Close();
            odcConnection.Close();
            //}
            //catch (Exception e)
            //{
            //    //MessageBox.Show(e.ToString());
            //}

        }
        #endregion

        #region 4.其他的都可以在骏SQI中找到，现在的就以上1，2，3：需要再补充！
        #endregion



        #region 控制器读取20150929

        /// <summary>
        /// 这个是为Control专门的20150929
        /// </summary>
        /// <param name="DBPath"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static List<string[]> ReadAllStringFromTable_ForControlOnly(string DBPath, string TableName, InformationGlo.SenarioControl _senariocontrol)
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

            string seniro = "ControlCar";
            switch (_senariocontrol)
            {
                case InformationGlo.SenarioControl.ControlCar:
                    seniro = "ControlCar";
                    break;
                case InformationGlo.SenarioControl.ControlChillerWaterFlowRate:
                    seniro = "ControlChillerWaterFlowRate";
                    break;
                case InformationGlo.SenarioControl.ControlChillerWaterTemp:
                    seniro = "ControlChillerWaterTemp";
                    break;
            }

            OleDbCommand odCommand = odcConnection.CreateCommand();
            //3、输入查询语句,注意SQL语句的空格是不要少了
            //查询最后一条记录
            odCommand.CommandText = "SELECT COUNT(*) FROM " + TableName + " where " + seniro + "='1'";  //要执行的 SQL 语句或存储过程。 默认值为空字符串。
            int RecordTotalCount = (int)odCommand.ExecuteScalar();

            //odCommand.CommandText = "select * from " + TableName+" where "+seniro+"='1'"; //+ " where 序号 = (select max(序号) from "+tableName+" )";
            odCommand.CommandText = "select * from " + TableName + " where " + seniro + "='1'" + " order by 顺序"; //+ " where 序号 = (select max(序号) from "+tableName+" )";

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
        #endregion 控制器读取

        /// <summary>
        /// 教研断电
        /// </summary>
        /// <param name="DBPath"></param>
        /// <param name="TestName"></param>
        /// <param name="STOP"></param>
        public static void StpoDot_InsertRecordDataTODBTotal(string DBPath, string TestName, double STOP)
        {
            string TableName = "";

            TableName = TestName;


            //try
            //{
            //1、建立连接 
            string strConn
                = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立Command实例 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            //3、输入SQL语句,注意SQL语句的空格是不要少了
            //odCommand.CommandText = "INSERT INTO 试验信息 VALUES ('2014-5-21','15:27','Unknown','Unknown','Unknown','开始',0,0,0,0,0,0,0,0)";
            odCommand.CommandText = "INSERT INTO " + TableName + " VALUES ("
                + "'" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString() + "',"
                                        + "'" + STOP.ToString() + "'"
                                        + ")";
            //执行语句
            OleDbDataReader odrReader = odCommand.ExecuteReader();


            //关闭连接 
            odrReader.Close();
            odcConnection.Close();
            //}
            //catch (Exception e)
            //{
            //    //MessageBox.Show(e.ToString());
            //}

        }


        public static void FilterCoeUpdateToDateBase_ForCar(string DBpath, string TableName, string[] Coedata)
        {
            //string tableName = "update";

            //string DBPath = "D:\\bookmanage.mdb";
            // 读取mdb数据 
            //try
            //{
            //1、建立连接 
            string strConn
                = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBpath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立Command实例 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            //删除
            odCommand.CommandText = "delete * from " + TableName;
            odCommand.ExecuteNonQuery();
            //odCommand.CommandText = "update wuyu set 第一='LAJI2',第二='LAJI'";

            odCommand.CommandText = "INSERT INTO " + TableName + " VALUES("
                                    + "'" + Coedata[0] + "',"
                                    + "'" + Coedata[1] + "',"
                                    + "'" + Coedata[2] + "',"
                                    + "'" + Coedata[3] + "',"
                                    + "'" + Coedata[4] + "',"
                                    + "'" + Coedata[5] + "',"
                                    + "'" + Coedata[6] + "',"
                                    + "'" + Coedata[7] + "',"
                                    + "'" + Coedata[8] + "',"
                                    + "'" + Coedata[9] + "',"
                                    + "'" + Coedata[10] + "',"
                                    + "'" + Coedata[11] + "',"
                                    + "'" + Coedata[12] + "',"
                                    + "'" + Coedata[13] + "',"
                                    + "'" + Coedata[14] + "',"
                                    + "'" + Coedata[15] + "',"
                                    + "'" + Coedata[16] + "',"
                                    + "'" + Coedata[17] + "',"
                                    + "'" + Coedata[18] + "',"
                                    + "'" + Coedata[19] + "',"
                                    + "'" + Coedata[20] + "',"
                                    + "'" + Coedata[21] + "',"
                                    + "'" + Coedata[22] + "',"
                                    + "'" + Coedata[23] + "',"
                                    + "'" + Coedata[24] + "',"
                                    + "'" + Coedata[25] + "',"
                                    + "'" + Coedata[26] + "',"
                                    + "'" + Coedata[27] + "',"
                                    + "'" + Coedata[28] + "',"
                                    + "'" + Coedata[29] + "'"   //压缩机离合器电压24V/12V
                                    + ")";
            odCommand.ExecuteNonQuery();
            //关闭连接
            //odrReader.Close();
            odcConnection.Close();

        }

        public static string[] ReadAllFromFilterCoeTable(string DBPath, string TableName)
        {
            //List<string[]> ResList = new List<string[]>();

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
            //odCommand.CommandText = "select * from " + TableName+" where "+seniro+"='1'"; //+ " where 序号 = (select max(序号) from "+tableName+" )";
            odCommand.CommandText = "select * from " + TableName; //+ " where 序号 = (select max(序号) from "+tableName+" )";

            //建立读取 
            OleDbDataReader odrReader = odCommand.ExecuteReader();
            //int RecordCount = 0;
            int ColNum = odrReader.FieldCount;
            string[] ResList = new string[ColNum];
            while (odrReader.Read())//把结果集内的每一条记录给dt,odrReader.Read()表示记录指针成功指向下一条
            {
                //RecordCount += 1;
                string[] ResInLine = new string[ColNum];
                //2015


                for (int i = 0; i < ColNum; i++)
                {
                    ResInLine[i] = (string)odrReader[i];
                }

                ResList = ResInLine;
            }
            //}
            return ResList;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Report
{
    public static class DBOperateForReport
    {

        public static string[] ReportParameter;

        public static void AddInfoRecordToDateBase(string DBPath, List<string[]> ReportStringList, string[] ReportParameter)
        {
            string tableName = "试验信息";
            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:
                    tableName = "Report_CarCooling";
                    break;
                case SenarioForReport.Senario_ForReport.CarNoise:
                    tableName = "Report_CarNoise";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerChangCondition:
                    tableName = "Report_CompChangCondition";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                    tableName = "Report_CompMaxCondition";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                    tableName = "Report_CompNorminalCondition";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerPartialCondition:
                    tableName = "Report_CompPartialCondition";
                    break;

            }

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
            string CommandValue = ConvertToCommandValue(ReportStringList, ReportParameter);
            odCommand.CommandText = "INSERT INTO " + tableName + " VALUES(" + CommandValue + ")";



            //执行语句20151223
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
        /// 返回的是commandtext中value（----）里面的全部字符串！20151222
        /// </summary>
        /// <param name="ReportStringList"></param>
        /// <param name="ReportParameter"></param>
        /// <returns></returns>
        public static string ConvertToCommandValue(List<string[]> ReportStringList, string[] ReportParameter)
        {
            string CommandText = "'";
            for (int i = 0; i < ReportStringList.Count; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    CommandText += ReportStringList[i][j] + "','";
                }
            }
            //CommandText += "'";

            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:
                case SenarioForReport.Senario_ForReport.CarNoise:
                    for (int k = 0; k < 25; k++)
                    {
                        CommandText += ReportParameter[k] + "','";
                    }
                    CommandText += ReportParameter[25] + "'";
                    break;

                case SenarioForReport.Senario_ForReport.ChillerChangCondition:
                case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                case SenarioForReport.Senario_ForReport.ChillerPartialCondition:
                    for (int h = 0; h < 22; h++)
                    {
                        CommandText += ReportParameter[h] + "','";
                    }
                    CommandText += ReportParameter[22] + "'";
                    break;
            }

            return CommandText;

        }


        /// <summary>
        /// 把参数转换成一个字符串数组
        /// </summary>
        /// <returns></returns>
        public static void ConverParatmeterToGroup()
        {
            //string[] RPGroup=new string[26];
            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:
                case SenarioForReport.Senario_ForReport.CarNoise:
                    ReportParameterMySelf.RPGroupForCar[0] = ReportParameterMySelf.RP1CarExpType;
                    ReportParameterMySelf.RPGroupForCar[1] = ReportParameterMySelf.RP2Manufacturer;
                    ReportParameterMySelf.RPGroupForCar[2] = ReportParameterMySelf.RP3ModelIdentity;
                    ReportParameterMySelf.RPGroupForCar[3] = ReportParameterMySelf.RP4OutNumb;
                    ReportParameterMySelf.RPGroupForCar[4] = ReportParameterMySelf.RP5Refrig;
                    ReportParameterMySelf.RPGroupForCar[5] = ReportParameterMySelf.RP6NormalCoolingCapacity;
                    ReportParameterMySelf.RPGroupForCar[6] = ReportParameterMySelf.RP7NormalPower;
                    ReportParameterMySelf.RPGroupForCar[7] = ReportParameterMySelf.RP8RollDiameter;
                    ReportParameterMySelf.RPGroupForCar[8] = ReportParameterMySelf.RP9ClutchVoltage;
                    ReportParameterMySelf.RPGroupForCar[9] = ReportParameterMySelf.RP10DischargeSatTemp;
                    ReportParameterMySelf.RPGroupForCar[10] = ReportParameterMySelf.RP11DischargePres;
                    ReportParameterMySelf.RPGroupForCar[11] = ReportParameterMySelf.RP12SuctionTemp;
                    ReportParameterMySelf.RPGroupForCar[12] = ReportParameterMySelf.RP13SuctionSatTemp;
                    ReportParameterMySelf.RPGroupForCar[13] = ReportParameterMySelf.RP14SuctionPres;
                    ReportParameterMySelf.RPGroupForCar[14] = ReportParameterMySelf.RP15CompRotate;
                    ReportParameterMySelf.RPGroupForCar[15] = ReportParameterMySelf.RP16DischargeSatLiqEnthalpy;
                    ReportParameterMySelf.RPGroupForCar[16] = ReportParameterMySelf.RP17SuctionEnthalpy;
                    ReportParameterMySelf.RPGroupForCar[17] = ReportParameterMySelf.RP18SuctionSpecVolum;
                    ReportParameterMySelf.RPGroupForCar[18] = ReportParameterMySelf.RP19CompTemp;
                    ReportParameterMySelf.RPGroupForCar[19] = ReportParameterMySelf.RP20IniTorque;
                    ReportParameterMySelf.RPGroupForCar[20] = ReportParameterMySelf.RP21CoolingWaterTemp;
                    ReportParameterMySelf.RPGroupForCar[21] = ReportParameterMySelf.RP22TestMan;
                    ReportParameterMySelf.RPGroupForCar[22] = ReportParameterMySelf.RP23TestDate;
                    ReportParameterMySelf.RPGroupForCar[23] = ReportParameterMySelf.RP24SendCompany;
                    ReportParameterMySelf.RPGroupForCar[24] = ReportParameterMySelf.RP25TestResult;
                    ReportParameterMySelf.RPGroupForCar[25] = ReportParameterMySelf.RP26BaseUnderNoise;


                    //RPGroup= ReportParameterMySelf.RPGroupForCar;
                    //tableName = "Report_CarNoise";s
                    break;
                case SenarioForReport.Senario_ForReport.ChillerChangCondition:

                case SenarioForReport.Senario_ForReport.ChillerMaxCondition:

                case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[0] = ReportParameterMySelf_ForChiller.RP1ChillerExpType;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[1] = ReportParameterMySelf_ForChiller.RP2Manufacturer;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[2] = ReportParameterMySelf_ForChiller.RP3ModelIdentity;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[3] = ReportParameterMySelf_ForChiller.RP4OutNumb;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[4] = ReportParameterMySelf_ForChiller.RP5Refrig;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[5] = ReportParameterMySelf_ForChiller.RP6NormalCoolingCapacity;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[6] = ReportParameterMySelf_ForChiller.RP7NormalPower;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[7] = ReportParameterMySelf_ForChiller.RP8NormalWaterFlow;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[8] = ReportParameterMySelf_ForChiller.RP9ControlVar;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[9] = ReportParameterMySelf_ForChiller.RP11Voltage;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[10] = ReportParameterMySelf_ForChiller.RP12InWaterTemperature;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[11] = ReportParameterMySelf_ForChiller.RP13ControlVarName;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[12] = ReportParameterMySelf_ForChiller.RP14ControlVarValue;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[13] = ReportParameterMySelf_ForChiller.RP15EvaporatorTemp;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[14] = ReportParameterMySelf_ForChiller.RP16EvaporatorPres;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[15] = ReportParameterMySelf_ForChiller.RP17SuctionTemp;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[16] = ReportParameterMySelf_ForChiller.RP18SuctionSpecVolum;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[17] = ReportParameterMySelf_ForChiller.RP19SuctionSpecEnthalpy;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[18] = "--";
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[19] = "--";
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[20] = ReportParameterMySelf_ForChiller.RP23TestResult;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[21] = ReportParameterMySelf_ForChiller.RP24TestMan;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[22] = ReportParameterMySelf_ForChiller.RP25TestDate;

                    //RPGroup=ReportParameterMySelf_ForChiller.RPGroupForChiller;    //23能不赋给26个的数组
                    break;
                case SenarioForReport.Senario_ForReport.ChillerPartialCondition:

                    ReportParameterMySelf_ForChiller.RPGroupForChiller[0] = ReportParameterMySelf_ForChiller.RP1ChillerExpType;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[1] = ReportParameterMySelf_ForChiller.RP2Manufacturer;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[2] = ReportParameterMySelf_ForChiller.RP3ModelIdentity;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[3] = ReportParameterMySelf_ForChiller.RP4OutNumb;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[4] = ReportParameterMySelf_ForChiller.RP5Refrig;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[5] = ReportParameterMySelf_ForChiller.RP6NormalCoolingCapacity;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[6] = ReportParameterMySelf_ForChiller.RP7NormalPower;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[7] = ReportParameterMySelf_ForChiller.RP8NormalWaterFlow;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[8] = ReportParameterMySelf_ForChiller.RP9ControlVar;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[9] = ReportParameterMySelf_ForChiller.RP11Voltage;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[10] = ReportParameterMySelf_ForChiller.RP12InWaterTemperature;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[11] = ReportParameterMySelf_ForChiller.RP13ControlVarName;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[12] = ReportParameterMySelf_ForChiller.RP14ControlVarValue;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[13] = ReportParameterMySelf_ForChiller.RP15EvaporatorTemp;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[14] = ReportParameterMySelf_ForChiller.RP16EvaporatorPres;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[15] = ReportParameterMySelf_ForChiller.RP17SuctionTemp;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[16] = ReportParameterMySelf_ForChiller.RP18SuctionSpecVolum;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[17] = ReportParameterMySelf_ForChiller.RP19SuctionSpecEnthalpy;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[18] = ReportParameterMySelf_ForChiller.RP20PartialLoad;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[19] = ReportParameterMySelf_ForChiller.RP21PartialLoadName;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[20] = ReportParameterMySelf_ForChiller.RP23TestResult;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[21] = ReportParameterMySelf_ForChiller.RP24TestMan;
                    ReportParameterMySelf_ForChiller.RPGroupForChiller[22] = ReportParameterMySelf_ForChiller.RP25TestDate;

                    //RPGroup=ReportParameterMySelf_ForChiller.RPGroupForChiller;    //23能不赋给26个的数组
                    break;

            }


            //return RPGroup;

        }


        #region 从数据库中读取报表数据然后加载20151222
        public static void GetLastInfoRecordFromDateBase(string DBPath)
        {
            //以下方法为调取数据库所有记录，然后选择最后一条记录
            //List<string[]> InfoRecrdListFromDB = Utility.MDBComm.ReadAllStringFromTable(DBPath,TableName);  //从读取字符串表中建立List
            //int ListCount = InfoRecrdListFromDB.Count -1;

            ////获取最后一条记录的所有字段
            //for(int i=0;i<11;i++)
            //{
            //    InfoArray[i] = InfoRecrdListFromDB[ListCount][i];
            //}

            string[] InfoArray = new string[225];

            //string tableName = "试验信息";
            string tableName = "试验信息";
            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:
                    tableName = "Report_CarCooling";
                    break;
                case SenarioForReport.Senario_ForReport.CarNoise:
                    tableName = "Report_CarNoise";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerChangCondition:
                    tableName = "Report_CompChangCondition";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                    tableName = "Report_CompMaxCondition";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                    tableName = "Report_CompNorminalCondition";
                    break;
                case SenarioForReport.Senario_ForReport.ChillerPartialCondition:
                    tableName = "Report_CompPartialCondition";
                    break;

            }
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

                    switch (SenarioForReport.senario_ForReport)
                    {
                        case SenarioForReport.Senario_ForReport.CarCooling:
                        case SenarioForReport.Senario_ForReport.CarNoise:

                            int indexNum = 0;
                            for (int i = 0; i < StringListForCarReport.CarStringList.Count; i++)
                            {
                                for (int j = 1; j <= 5; j++)
                                {
                                    StringListForCarReport.CarStringList[i][j] = InfoArray[indexNum];

                                    indexNum++;
                                }
                            }
                            string[] ParameterGroup_ForCar=new string[26];
                            for (int i = 0; i < 26;i++ )
                            {
                                ParameterGroup_ForCar[i]=InfoArray[indexNum];
                                indexNum++;
                            }
                            ConverGroupToParatmeter(ParameterGroup_ForCar);

                            break;
                        case SenarioForReport.Senario_ForReport.ChillerChangCondition:
                        case SenarioForReport.Senario_ForReport.ChillerMaxCondition:
                        case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                        case SenarioForReport.Senario_ForReport.ChillerPartialCondition:
                            int indexNum1 = 0;
                            for (int i = 0; i < StringListForCarReport.ChillerStringList.Count; i++)
                            {
                                for (int j = 1; j <= 5; j++)
                                {
                                    StringListForCarReport.ChillerStringList[i][j] = InfoArray[indexNum1];

                                    indexNum1++;
                                }
                            }
                            string[] ParameterGroup_ForChiller=new string[26];
                            for (int i = 0; i < 23;i++ )
                            {
                                ParameterGroup_ForChiller[i] = InfoArray[indexNum1];
                                indexNum1++;
                            }
                            ConverGroupToParatmeter(ParameterGroup_ForChiller);

                            break;

                    }

                }

            }

            //关闭连接 
            odrReader.Close();
            odcConnection.Close();  //OleDbConnection实例

        }

        public static void ConverGroupToParatmeter(string[] Group)
        {
            //string[] RPGroup=new string[26];
            switch (SenarioForReport.senario_ForReport)
            {
                case SenarioForReport.Senario_ForReport.CarCooling:
                case SenarioForReport.Senario_ForReport.CarNoise:
                    ReportParameterMySelf.RP1CarExpType = Group[0];
                    ReportParameterMySelf.RP2Manufacturer = Group[1];
                    ReportParameterMySelf.RP3ModelIdentity = Group[2];
                    ReportParameterMySelf.RP4OutNumb = Group[3];
                    ReportParameterMySelf.RP5Refrig = Group[4];
                    ReportParameterMySelf.RP6NormalCoolingCapacity = Group[5];
                    ReportParameterMySelf.RP7NormalPower = Group[6];
                    ReportParameterMySelf.RP8RollDiameter = Group[7];
                    ReportParameterMySelf.RP9ClutchVoltage = Group[8];
                    ReportParameterMySelf.RP10DischargeSatTemp = Group[9];
                    ReportParameterMySelf.RP11DischargePres = Group[10];
                    ReportParameterMySelf.RP12SuctionTemp = Group[11];
                    ReportParameterMySelf.RP13SuctionSatTemp = Group[12];
                    ReportParameterMySelf.RP14SuctionPres = Group[13];
                    ReportParameterMySelf.RP15CompRotate = Group[14];
                    ReportParameterMySelf.RP16DischargeSatLiqEnthalpy = Group[15];
                    ReportParameterMySelf.RP17SuctionEnthalpy = Group[16];
                    ReportParameterMySelf.RP18SuctionSpecVolum = Group[17];
                    ReportParameterMySelf.RP19CompTemp = Group[18];
                    ReportParameterMySelf.RP20IniTorque = Group[19];
                    ReportParameterMySelf.RP21CoolingWaterTemp = Group[20];
                    ReportParameterMySelf.RP22TestMan = Group[21];
                    ReportParameterMySelf.RP23TestDate = Group[22];
                    ReportParameterMySelf.RP24SendCompany = Group[23];
                    ReportParameterMySelf.RP25TestResult = Group[24];
                    ReportParameterMySelf.RP26BaseUnderNoise = Group[25];


                    //RPGroup= ReportParameterMySelf.RPGroupForCar;
                    //tableName = "Report_CarNoise";s
                    break;
                case SenarioForReport.Senario_ForReport.ChillerChangCondition:

                case SenarioForReport.Senario_ForReport.ChillerMaxCondition:

                case SenarioForReport.Senario_ForReport.ChillerNormialCondition:
                    ReportParameterMySelf_ForChiller.RP1ChillerExpType = Group[0];
                    ReportParameterMySelf_ForChiller.RP2Manufacturer = Group[1];
                    ReportParameterMySelf_ForChiller.RP3ModelIdentity = Group[2];
                    ReportParameterMySelf_ForChiller.RP4OutNumb = Group[3];
                    ReportParameterMySelf_ForChiller.RP5Refrig = Group[4];
                    ReportParameterMySelf_ForChiller.RP6NormalCoolingCapacity = Group[5];
                    ReportParameterMySelf_ForChiller.RP7NormalPower = Group[6];
                    ReportParameterMySelf_ForChiller.RP8NormalWaterFlow = Group[7];
                    ReportParameterMySelf_ForChiller.RP9ControlVar = Group[8];
                    ReportParameterMySelf_ForChiller.RP11Voltage = Group[9];
                    ReportParameterMySelf_ForChiller.RP12InWaterTemperature = Group[10];
                    ReportParameterMySelf_ForChiller.RP13ControlVarName = Group[11];
                    ReportParameterMySelf_ForChiller.RP14ControlVarValue = Group[12];
                    ReportParameterMySelf_ForChiller.RP15EvaporatorTemp = Group[13];
                    ReportParameterMySelf_ForChiller.RP16EvaporatorPres = Group[14];
                    ReportParameterMySelf_ForChiller.RP17SuctionTemp = Group[15];
                    ReportParameterMySelf_ForChiller.RP18SuctionSpecVolum = Group[16];
                    ReportParameterMySelf_ForChiller.RP19SuctionSpecEnthalpy = Group[17];
                    ReportParameterMySelf_ForChiller.RP20PartialLoad = "";
                    //Group[18] = "--";
                    ReportParameterMySelf_ForChiller.RP21PartialLoadName = "";
                    //Group[19] = "--";
                    ReportParameterMySelf_ForChiller.RP23TestResult = Group[20];
                    ReportParameterMySelf_ForChiller.RP24TestMan = Group[21];
                    ReportParameterMySelf_ForChiller.RP25TestDate = Group[22];

                    //RPGroup=ReportParameterMySelf_ForChiller.RPGroupForChiller;    //23能不赋给26个的数组
                    break;
                case SenarioForReport.Senario_ForReport.ChillerPartialCondition:

                    ReportParameterMySelf_ForChiller.RP1ChillerExpType = Group[0];
                    ReportParameterMySelf_ForChiller.RP2Manufacturer = Group[1];
                    ReportParameterMySelf_ForChiller.RP3ModelIdentity = Group[2];
                    ReportParameterMySelf_ForChiller.RP4OutNumb = Group[3];
                    ReportParameterMySelf_ForChiller.RP5Refrig = Group[4];
                    ReportParameterMySelf_ForChiller.RP6NormalCoolingCapacity = Group[5];
                    ReportParameterMySelf_ForChiller.RP7NormalPower = Group[6];
                    ReportParameterMySelf_ForChiller.RP8NormalWaterFlow = Group[7];
                    ReportParameterMySelf_ForChiller.RP9ControlVar = Group[8];
                    ReportParameterMySelf_ForChiller.RP11Voltage = Group[9];
                    ReportParameterMySelf_ForChiller.RP12InWaterTemperature = Group[10];
                    ReportParameterMySelf_ForChiller.RP13ControlVarName = Group[11];
                    ReportParameterMySelf_ForChiller.RP14ControlVarValue = Group[12];
                    ReportParameterMySelf_ForChiller.RP15EvaporatorTemp = Group[13];
                    ReportParameterMySelf_ForChiller.RP16EvaporatorPres = Group[14];
                    ReportParameterMySelf_ForChiller.RP17SuctionTemp = Group[15];
                    ReportParameterMySelf_ForChiller.RP18SuctionSpecVolum = Group[16];
                    ReportParameterMySelf_ForChiller.RP19SuctionSpecEnthalpy = Group[17];

                    ReportParameterMySelf_ForChiller.RP20PartialLoad=Group[18];
                    ReportParameterMySelf_ForChiller.RP21PartialLoadName = Group[19];

                    ReportParameterMySelf_ForChiller.RP23TestResult = Group[20];
                    ReportParameterMySelf_ForChiller.RP24TestMan = Group[21];
                    ReportParameterMySelf_ForChiller.RP25TestDate = Group[22];

                    //RPGroup=ReportParameterMySelf_ForChiller.RPGroupForChiller;    //23能不赋给26个的数组
                    break;

            }


            //return RPGroup;

        }
        #endregion 从数据库中读取报表数据然后加载20151222

    }
}

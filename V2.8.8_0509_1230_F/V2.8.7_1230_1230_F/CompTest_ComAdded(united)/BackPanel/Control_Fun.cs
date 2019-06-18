using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
namespace BackPanel
{
    public static partial class Control
    {


        /// <summary>
        /// 从数据库中读取控制器的各参数
        /// </summary>
        /// <param name="ControlerList"></param>
        /// <param name="DBPath"></param>
        /// <param name="TableName"></param>
        public static void BuildControlerList(List<UT35ADef> ControlerList, string DBPath, string TableName)
        {
            ControlerList.Clear();
            //List<string[]> StringsListFromDB = Utility.MDBComm.ReadAllStringFromTable(DBPath, TableName);  //从读取字符串表中建立List
            List<string[]> StringsListFromDB = BackPanel.DBOperate.ReadAllStringFromTable_ForControlOnly(DBPath, TableName, BackPanel.InformationGlo.senariocontrol);// Utility.MDBComm.ReadAllStringFromTable(DBPath, TableName);  //从读取字符串表中建立List
            for (int i = 0; i < StringsListFromDB.Count; i++)
            {
                UT35ADef ControlerVar = new UT35ADef();
                ControlerVar.Name = StringsListFromDB[i][0];
                ControlerVar.DR = Convert.ToBoolean(StringsListFromDB[i][1]);
                ControlerVar.StackNum = Convert.ToInt32(StringsListFromDB[i][2]);
                ControlerVar.SDP = Convert.ToInt32(StringsListFromDB[i][3]);
                ControlerVar.SH = Convert.ToDouble(StringsListFromDB[i][4]);
                ControlerVar.SL = Convert.ToDouble(StringsListFromDB[i][5]);
                ControlerVar.P = Convert.ToDouble(StringsListFromDB[i][6]);
                ControlerVar.I = Convert.ToDouble(StringsListFromDB[i][7]);
                ControlerVar.D = Convert.ToDouble(StringsListFromDB[i][8]);
                ControlerVar.SV = Convert.ToDouble(StringsListFromDB[i][9]);
                ControlerVar.OUT = Convert.ToDouble(StringsListFromDB[i][10]);

                ControlerList.Add(ControlerVar);

            }
            ControlerList.TrimExcess();
        }



        #region 通讯底层

        ///<summary>
        /// 设置UT351
        /// </summary>
        /// <param name="ZD">栈号</param>
        /// <param name="CMD">命令字符 SV:设定值 BS:误差值</param>
        /// <param name="ZValue">设置的值</param>
        /// <param name="SDP">小数位数</param>
        public static void Set(int ZD, string CMD, double ZValue, int SDP)
        {
            //if (UtilityMod_Header.IsDemo)
            //{
            //    //如果模拟

            //}
            if (UtilityMod_Header.IsDemo)
            {
                //如果模拟

            }
            else
            {
                //如果真实
                UtilityMod_Header.Controller_COM6.SetControllerDB(ZD, CMD, ZValue, SDP);
            }
        }

        /// <summary>
        /// 读取仪表的显示值
        /// </summary>
        /// <param name="ZD">栈号</param>
        /// <param name="CMD">命令字符 PV:当前值 SV:设定值 BS:误差值 DR:正反动作</param>
        /// <param name="SDP">小数位</param>
        //public static double Read(int ZD, string CMD, int SDP)
        //{

        //    return 0;
        //}
        #endregion

        ///// <summary>
        ///// 输入参数
        ///// </summary>
        //public static List<UT35ADef> Controllist = new List<UT35ADef>();

        /// <summary>
        /// 初始化把所有从数据库读取的值都送给控制器
        /// </summary>
        public static void InitiateSendAllToControl()
        {

            //BuildControlerList(Control.Controllist, "D:\\CarChiller.mdb", "Table_Controller");
            for (int i = 0; i < Control.Controllist.Count; i++)
            {
                Set(Convert.ToInt32(Control.Controllist[i].StackNum), "SV", Control.Controllist[i].SetValue, Control.Controllist[i].SDP);
            }
        }



        /// <summary>
        /// 从底层读取数据存到List中：20150916
        /// </summary>
        public static List<UT35ADef> ReadControlFromDN()
        {
            //if (UtilityMod_Header.IsDemo)
            //{

            List<UT35ADef> Controllist = new List<UT35ADef>(6);
            BackPanel.Control.BuildControlerList(Controllist, BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "Table_Controller");

            if (UtilityMod_Header.IsDemo)
            {
                Controllist[0].OUT = 10;
                Controllist[1].OUT = 11;
                Controllist[2].OUT = 12;
                Controllist[3].OUT = 13;//UT5
                Controllist[4].OUT = 14;

                //20150924加：给控制器的两个读取量值:20150929这个是UT5的当前值
                Controllist[3].PV = 14;

            }
            else
            {
                Controllist[0].OUT = UtilityMod_Header.Controller_COM6.ReadControllerDB(Controllist[0].StackNum, "OUT", 1);
                Controllist[1].OUT = UtilityMod_Header.Controller_COM6.ReadControllerDB(Controllist[1].StackNum, "OUT", 1);
                Controllist[2].OUT = UtilityMod_Header.Controller_COM6.ReadControllerDB(Controllist[2].StackNum, "OUT", 1);
                Controllist[3].OUT = UtilityMod_Header.Controller_COM6.ReadControllerDB(Controllist[3].StackNum, "OUT", 1);
                Controllist[4].OUT = UtilityMod_Header.Controller_COM6.ReadControllerDB(Controllist[4].StackNum, "OUT", 1);

                //Control.Controllist[5].OUT = UtilityMod_Header.Controller_COM6.ReadControllerDB(6, "OUT", 1);
                //20150924加：给控制器的两个读取量值20150929这个是UT5的当前值
                Controllist[3].PV = UtilityMod_Header.Controller_COM6.ReadControllerDB(Control.Controllist[3].StackNum, "PV", Control.Controllist[3].SDP);



            }

            return Controllist;


        }




        /// <summary>
        /// 把读取的数据传给数据库
        /// </summary>
        public static void SendAllToDB()
        {

        }
        static int sleeptime = 200;
        /// <summary>
        ///初始三个都锁住
        /// </summary>
        /// <param name="Controllist"></param>
        /// <returns></returns>
        public static bool ControlLockAll_ForCar(List<UT35ADef> Controllist)
        {

            BackPanel.Control.Set(1, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(1, "OUT", 30, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "OUT", 30, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(6, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(6, "OUT", 10, 1);

            return true;
        }
        /// <summary>
        /// 初始三个都锁住20151215
        /// </summary>
        /// <param name="Controllist"></param>
        /// <returns></returns>
        public static bool ControlLockAll_ForChiller(List<UT35ADef> Controllist)
        {

            BackPanel.Control.Set(1, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(1, "OUT", 50, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "OUT", 0, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(4, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(4, "OUT", 50, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(5, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(5, "OUT", 0, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(6, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(6, "OUT", 0, 1);

            return true;
        }
        public static bool ControlUnLockUT1UT2_ForCar()
        {

            BackPanel.Control.Set(1, "MOD", 0, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "MOD", 0, 0);

            return true;
        }

        public static bool ControlUnLockUT1UT2_ForChiller()
        {

            BackPanel.Control.Set(1, "MOD", 0, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "MOD", 0, 0);

            return true;
        }

        public static bool ControlUnLockUT4UT5_ForChiller()
        {

            BackPanel.Control.Set(4, "MOD", 0, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(5, "MOD", 0, 0);

            return true;
        }
        public static bool ControlLockUT4UT5_ForChiller()
        {

            BackPanel.Control.Set(4, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(4, "OUT", 50, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(5, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(5, "OUT", 0, 1);
            Thread.Sleep(sleeptime);
            return true;
        }
        public static bool ControlLockUT1UT2_ForCar()
        {

            BackPanel.Control.Set(1, "MOD", 1, 0);
            Thread.Sleep(sleeptime);

            BackPanel.Control.Set(1, "OUT", 30, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "OUT", 30, 1);

            return true;
        }

        public static bool ControlLockUT1UT2_ForChiller()
        {

            BackPanel.Control.Set(1, "MOD", 1, 0);
            Thread.Sleep(sleeptime);

            BackPanel.Control.Set(1, "OUT", 50, 1);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(2, "OUT", 0, 1);

            return true;
        }


        public static bool ControlUnLockUT6_ForCar()
        {

            BackPanel.Control.Set(6, "MOD", 0, 0);

            return true;

        }
        public static bool ControlLockUT6_ForCar()
        {

            BackPanel.Control.Set(6, "MOD", 1, 0);
            Thread.Sleep(sleeptime);
            BackPanel.Control.Set(6, "OUT", 10, 1);
            return true;
        }

        public static bool ControlInitiate_ForCar(List<UT35ADef> Controllist)
        {
            //吸气压力
            BackPanel.Control.Set(1, "SV", 0.282, 3);
            Thread.Sleep(sleeptime);
            //吸气温度
            BackPanel.Control.Set(2, "SV", 9, 2);
            Thread.Sleep(sleeptime);
            //排气压力
            BackPanel.Control.Set(3, "SV", 1.804, 3);
            Thread.Sleep(sleeptime);
            //排气压力OUT
            BackPanel.Control.Set(3, "OUT", 30, 1);
            Thread.Sleep(sleeptime);

            //供水温度
            BackPanel.Control.Set(5, "SV", 30, 2);
            Thread.Sleep(sleeptime);

            BackPanel.Control.Set(5, "MOD", 0, 0);
            Thread.Sleep(sleeptime);

            //压缩机转速 
            int RotateOfElecMotor = Convert.ToInt32(BackPanel.InformationGlo.CarCompressorRotateSet_ForControl * BackPanel.InformationGlo.CompressorDiameter_FromInfo / 300);
            BackPanel.Control.Set(6, "SV", RotateOfElecMotor, 0);
            return true;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static bool ControlInitiate_ForChiller()
        {
            //吸气压力
            BackPanel.Control.Set(1, "SV", 0.622, 3);
            Thread.Sleep(sleeptime);
            //吸气温度
            BackPanel.Control.Set(2, "SV", 18, 2);
            Thread.Sleep(sleeptime);
            ////排气压力
            //BackPanel.Control.Set(3, "SV", 1.804, 3);
            ////排气压力OUT
            //BackPanel.Control.Set(3, "OUT", 5, 1);
            //BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp;

            switch (BackPanel.InformationGlo.senariocontrol)
            {
                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterFlowRate:

                    // SH:最大值 SL:最小值 DR:方向 OUT:输入值 MOD:模式(1、手动)
                    //BackPanel.Control.Set(4, "SH", 6, 2);
                    BackPanel.Control.Set(4, "SH", 3, 2);
                    Thread.Sleep(sleeptime);
                    BackPanel.Control.Set(4, "SL", 0, 2);
                    Thread.Sleep(sleeptime);

                    //水流量
                    //BackPanel.Control.Set(4, "SV", 1.4, 2);

                    BackPanel.Control.Set(4, "SV", BackPanel.InformationGlo.ChillerWaterFlowRate_ForControl, 2);
                    Thread.Sleep(sleeptime);

                    BackPanel.Control.Set(4, "P", 50, 1);
                    Thread.Sleep(sleeptime);
                    BackPanel.Control.Set(4, "I", 60, 0);
                    Thread.Sleep(sleeptime);
                    BackPanel.Control.Set(4, "D", 0, 0);
                    Thread.Sleep(sleeptime);

                    break;
                case BackPanel.InformationGlo.SenarioControl.ControlChillerWaterTemp:

                    // SH:最大值 SL:最小值 DR:方向 OUT:输入值 MOD:模式(1、手动)
                    BackPanel.Control.Set(4, "SH", 50, 2);
                    Thread.Sleep(sleeptime);
                    BackPanel.Control.Set(4, "SL", 0, 2);
                    Thread.Sleep(sleeptime);
                    //水温
                    BackPanel.Control.Set(4, "SV", 35, 2);


                    Thread.Sleep(sleeptime);
                    BackPanel.Control.Set(4, "P", 51, 1);
                    Thread.Sleep(sleeptime);
                    BackPanel.Control.Set(4, "I", 61, 0);
                    Thread.Sleep(sleeptime);
                    BackPanel.Control.Set(4, "D", 1, 0);
                    Thread.Sleep(sleeptime);
                    break;

            }

            //进水温度
            BackPanel.Control.Set(5, "SV", 30, 2);
            Thread.Sleep(sleeptime);
            ////压缩机转速
            //int RotateOfElecMotor = Convert.ToInt32(1800 * BackPanel.InformationGlo.CompressorDiameter_FromInfo / 300);
            //BackPanel.Control.Set(6, "SV", RotateOfElecMotor, 0);
            return true;
        }
        public static bool ControlInitiate_ForChiller(List<UT35ADef> Controllist)
        {
            //吸气压力
            BackPanel.Control.Set(1, "SV", 0.282, 3);
            Thread.Sleep(sleeptime);
            //吸气温度
            BackPanel.Control.Set(2, "SV", 9, 2);
            Thread.Sleep(sleeptime);
            //排气压力
            BackPanel.Control.Set(3, "SV", 1.804, 3);
            //排气压力OUT
            BackPanel.Control.Set(3, "OUT", 5, 1);
            Thread.Sleep(sleeptime);
            //供水温度
            BackPanel.Control.Set(5, "SV", 30, 2);
            Thread.Sleep(sleeptime);
            //压缩机转速
            int RotateOfElecMotor = Convert.ToInt32(1800 * BackPanel.InformationGlo.CompressorDiameter_FromInfo / 300);
            BackPanel.Control.Set(6, "SV", RotateOfElecMotor, 0);
            return true;
        }
    }
}

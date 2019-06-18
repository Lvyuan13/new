using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Windows.Forms;
//using System.IO;

namespace WT3XXDAQ
{
   public class SimpleDAQ
    {
       public string PhaseWire = "P3W3";
       public string CT = "1";

        public void GetWT3XXValue(int COMPortNum,ref ArrayList IOArraylist)
        // 函数，获取横河WT3XX测量值
        // 输入通道号，以及IO列表
        // IOArrayList的格式举例
        //  "U,1"---电压，第一相
        //  "I,1"---电流，第一相
         {
            //建立232通讯连接 
            Connection connection = new Connection();//什么连接类对象
            connection.wireType = 2;//2--RS232
            connection.devAddress=Convert.ToString(COMPortNum)+",2,0,2";//ComPortNum;3-BaudRate,9600，2-BaundRate,4800;0-Format0;2-Handshake2;
            
            connection.Initialize();//连接初始化
            
            //创建list用来装输入输出
            ArrayList Valuelist = new ArrayList(IOArraylist);

            SetWT330State(ref connection, PhaseWire, CT);

            //从功率计读取测量值
            GetMeasureData(ref connection, IOArraylist, ref Valuelist);
            
            //关闭本次通讯连接
            connection.Finish();
            
            //IOArrayList已从要求的测量量名称转为对应的测量值，导出成double[]
            for (int i = 0; i <Valuelist.Count;i++ )
            {
                //if (Convert.ToString(Valuelist[i])!= "INF")//如果不是INF
                //{
                //    IOArraylist[i] = Convert.ToDouble(Valuelist[i]);//转成DOUBLE
                //}
                //else
                //{
                //    IOArraylist[i] = 9.9E99;//否转成极大值
                //}
                switch (Convert.ToString(Valuelist[i]))
                {
                    case "INF":
                        IOArraylist[i] = 88888.8;
                        break;
                    default:
                        try
                        {
                            IOArraylist[i] = Convert.ToDouble(Valuelist[i]);
                        }
                        catch(FormatException)
                        {
                            IOArraylist[i] = -88888.8;
                            //MessageBox.Show("收到未知的WT310数据",
                            //                "WT310",
                            //                MessageBoxButtons.OK,
                            //                MessageBoxIcon.Warning);
                        }
                        break;
                }


                            



            }
        }

        #region Function: GetMeasureData

       /// <summary>
       /// 设定功率计的线位和电流倍率 By YJW
       /// </summary>
       /// <param name="WireMod">电源接线 eg "P3W3"</param>
       /// <param name="CT">电流倍率 eg "4"</param>
        void SetWT330State(ref Connection Connection,string WireMod, string CT)
        {
            if (WireMod != "")
            {
                string WireModCommand = ":INPUT:WIRING " + WireMod;
                Connection.Send(WireModCommand);
            }
            if (CT != "")
            {
                string ScaleOnCommand = ":INPUT:SCALING:STATE ON";
                string CTCommand = ":INPUT:SCALING:CT:ALL " + CT;
                Connection.Send(CTCommand);
                Connection.Send(ScaleOnCommand);
            }


        }


        void GetMeasureData(ref Connection Connection, ArrayList ItermName, ref ArrayList ItermValue)
        //发送命令字符串读取测量值的函数
        {
            //拼出ITEM字符串
            string str = "";
            for (int i = 0; i < ItermName.Count; i++)
            {
                str = str + "ITEM" + Convert.ToString(i + 1) + " " + ItermName[i] + ",";
                if (i<ItermName.Count-1)
                {
                    str=str+";";
                }

            }

            str = ":NUMERIC:NORMAL:" + str;//拼出完整的NUMERIC：NORMAL字符串
            string RecData="";int Realength=0;//初始化返回值字符串
            Connection.Send(":NUMERIC:FORMAT ASCII");//发送命令，要求返回ASCII的值
            Connection.Send(":NUMERIC:NORMAL:NUMBER " + Convert.ToString(ItermName.Count));//发送命令，配置需要返回多少个值
            Connection.Send(str);//发送NUMERIC:NORMAL字符串，设定要读的变量

            Connection.Send(":NUMERIC:NORMAL:VALUE?");//发送命令要读的变量的值

            //Connection.Send("*IDN?");//test


            Connection.Receive(ref RecData, 200, ref Realength);//读回那些变量的值的字符串
            for (int i = 0; i < ItermName.Count; i++)//使用cutleft函数分割字符串
            {
                ItermValue[i] = CutLeft(",", ref RecData);
            }


        }
        #endregion

        #region Function: CutTool
        static string CutLeft(string symbol, ref string inData)
        {
            string outData = inData;
            int pos = inData.IndexOf(symbol);
            if (pos == -1)
            {
                //if no symbol, cut with LF.
                pos = inData.IndexOf((char)10);
            }
            if (pos != -1)
            {
                outData = inData.Substring(0, pos);
                inData = inData.Substring(pos + 1);
            }

            //cut data when harmonics mode
            pos = outData.IndexOf(" ");
            if (pos != -1)
            {
                outData = outData.Substring(pos + 1);
            }
            return outData;
        }
        #endregion
    }
    }


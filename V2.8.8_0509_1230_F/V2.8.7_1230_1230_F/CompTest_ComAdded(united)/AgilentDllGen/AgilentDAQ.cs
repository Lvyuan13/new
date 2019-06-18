using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
//using Agilent34970A_Comm;

namespace Agilent34970A_Comm
{
	public class SimpleDAQ
	{
        public class ChDef //通道定义用结构变量的定义
        {
            public string ChDelay,FRTDChNum,VoltageDCChNum, CurrentDCChNum, FRTDalpha,TCChNum,TCChsType;
            public int COMPortNum;
            public ChDef
                (string ChDelayStr,string VoltageDCChNumStr, string CurrentDCChNumStr,
                 string FRTDChNumStr, string FRTDalphaStr, 
                 string TCChNumStr, string TCChsTypeStr, int COMPortNumStr)
            {
                ChDelay=ChDelayStr;
                FRTDChNum=FRTDChNumStr;
                VoltageDCChNum=VoltageDCChNumStr;
                CurrentDCChNum = CurrentDCChNumStr;
                FRTDalpha = FRTDalphaStr;
                COMPortNum = COMPortNumStr;
                TCChNum = TCChNumStr;
                TCChsType = TCChsTypeStr;
            }
        }
        public void GetMeasurement(ChDef ChDefine,ref ArrayList reading,ref ArrayList timestamp,ref ArrayList channelnum )
        // 从安捷伦获读取测量值的函数
        {
            List<string[]> GlobalConfig = new List<string[]>();//全局配置list定义
            List<string[]> ChannelConfig = new List<string[]>();//通道配置list定义


            string AllChsNum = "";//ChDefine.VoltageDCChNum + "," +ChDefine.FRTDChNum + "," + ChDefine.CurrentDCChNum + "," + ChDefine.TCChNum;//全部通道的字符串,目前支持VDC，ADC,FRTD,TC
            
            
            
            //string SCANstr = "SCAN (@" + AllChsNum + ")";//扫描通道字符串
            //string DELAYstr = "CHANnel:DELay "+ChDefine.ChDelay+", (@"+AllChsNum+")";//通道延时字符串
          
            
            //GlobalConfig.Add(new string[] { "ROUTe:", SCANstr, DELAYstr, });//配置扫描路径和通道间隔            
            //GlobalConfig.Add(new string[] { "TRIGger:", "COUNt 1", "SOURce TIMer", "TIMer 0" });//配置触发  
            //GlobalConfig.Add(new string[] { "FORMat:", "READing:TIME ON", "READing:TIME:TYPE ABS", "READing:CHANnel ON" });//配置读取的相关数据



            if (ChDefine.VoltageDCChNum!="")//如果有直流电压测量
            {
                string UDCstr = "(@" + ChDefine.VoltageDCChNum + ")";//拼出所在的通道
                 ChannelConfig.Add(new string[] { "CONFigure:", "VOLTage:DC AUTO", UDCstr});//配置相关通道
                 AllChsNum = ChDefine.VoltageDCChNum;
            }



            if (ChDefine.TCChNum != "" && ChDefine.TCChsType != "")
            {
                string TCCstr = "(@" + ChDefine.TCChNum + ")";//拼出所在的通道
                ChannelConfig.Add(new string[] { "CONFigure:", "TEMPerature TC," + ChDefine.TCChsType, TCCstr });
                ChannelConfig.Add(new string[] { "UNIT:", "TEMPerature C", TCCstr });//配置单位
                ChannelConfig.Add(new string[] { "TEMP:", "TRAN:TC:RJUN:TYPE INT", TCCstr });//配置热电偶的参考端为板卡内部
                if (AllChsNum != "")
                {
                    AllChsNum += "," + ChDefine.TCChNum;
                }
                else
                {
                    AllChsNum =  ChDefine.TCChNum;
                }

            }

            if (ChDefine.FRTDChNum != "")//如果有四线制热电阻
            {
                string RTDstr = "(@" + ChDefine.FRTDChNum + ")";//拼出所在的通道
                ChannelConfig.Add(new string[] { "CONFigure:", "TEMPerature FRTD, " + ChDefine.FRTDalpha, RTDstr });//配置相关通道
                ChannelConfig.Add(new string[] { "UNIT:", "TEMPerature C", RTDstr });//配置单位
                if (AllChsNum != "")
                {
                    AllChsNum += "," + ChDefine.FRTDChNum;
                }
                else
                {
                    AllChsNum =  ChDefine.FRTDChNum;
                }
            }

            if (ChDefine.CurrentDCChNum != "")//如果有直流电流测量通道
            {
                string IDCstr = "(@" + ChDefine.CurrentDCChNum + ")";//拼出所在的通道
                ChannelConfig.Add(new string[] { "CONFigure:", "CURRent:DC AUTO", IDCstr }); //配置相关通道

                if (AllChsNum != "")
                {
                    AllChsNum += "," + ChDefine.CurrentDCChNum;
                }
                else
                {
                    AllChsNum = ChDefine.CurrentDCChNum;
                }
            }


            string SCANstr = "SCAN (@" + AllChsNum + ")";//扫描通道字符串
            string DELAYstr = "CHANnel:DELay " + ChDefine.ChDelay + ", (@" + AllChsNum + ")";//通道延时字符串
     

            GlobalConfig.Add(new string[] { "ROUTe:", SCANstr, DELAYstr, });//配置扫描路径和通道间隔            
            GlobalConfig.Add(new string[] { "TRIGger:", "COUNt 1", "SOURce TIMer", "TIMer 0" });//配置触发  
            GlobalConfig.Add(new string[] { "FORMat:", "READing:TIME ON", "READing:TIME:TYPE ABS", "READing:CHANnel ON" });//配置读取的相关数据



            GlobalConfig.TrimExcess(); ChannelConfig.TrimExcess();//去掉list多余的内存空间


            DAQ TestDAQ = new DAQ();//声明DAQ类实例
            TestDAQ.OpenRS232Port(ChDefine.COMPortNum);//开端口
            TestDAQ.Config34970A(GlobalConfig,ChannelConfig);//配置主机
            //Thread.Sleep(1000);
            TestDAQ.Readings(ref reading,ref timestamp,ref channelnum);//获取读数
        }
	}
}

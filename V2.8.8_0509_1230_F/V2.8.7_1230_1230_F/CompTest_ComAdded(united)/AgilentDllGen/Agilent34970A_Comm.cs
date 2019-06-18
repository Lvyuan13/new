using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Ivi.Visa.Interop;//YJW:引用了打包好的COM组件Ivi.Visa.Interop dll
using System.Windows.Forms;
using System.Threading;

namespace Agilent34970A_Comm 

{
	class DAQ   //YJW:安捷伦通讯程序类
	{            
        private Ivi.Visa.Interop.FormattedIO488 data_logger;

		public class Globls
			//"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
			// Sets global variables to be used in different functions of the program.
            // YJW:Agilent通讯程序的全局变量定义
			//"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
		{ 
			public static bool connected = false;  // Used to determine if there is connection with the instrument
			public static string addrtype;		// Used for the address type, GPIB or ASRL

			public static double NumRdgs;		// Used for the number of readings taken
			public static double TotTime;		// Used to calculate total measurement time
			public static double TrigCount;		// Used to determine number of scans
			public static double NumChan;		// Used to determine the number of channels scanned
		}

        public bool OpenRS232Port(int ComPortNum)
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        // This function opens a port (the communication between the instrument and
        // computer).
        // YJW: 函数，打开232通讯口并且验证安捷伦和计算机的连接是否正常，读取安捷伦设备的身份字符串
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        {
            string addr,            //通讯地址
                    msg,            //消息
                    option_string;  //通讯设定字符串

            try// YJW: 放在TRY模块里，可以捕获异常并且处理
            {
                //create the formatted io object
                //YJW: 声明488通讯类实例
                data_logger = new FormattedIO488Class();

                //create the resource manager
                //YJW: 声明Ivi.Visa.Interop中的ResourceManger类实例
                ResourceManager mgr = new ResourceManager();

                // Get address and type (ASRL), and do an initial check
                //YJW: 使用232通讯，即ASRL
                addr="ASRL"+Convert.ToString(ComPortNum);
                Globls.addrtype = "ASRL";

                // If address is Serial, we have 34970 and must set some things
                //YJW: 设定232通讯
                //YJW: 设定串口通讯的COM口号
                addr = "ASRL"+Convert.ToString(ComPortNum) + "::INSTR";
                // Use the optionstring to setup the RS-232 parameters
                //YJW: 设定串口通讯的参数
                //option_string = "Timeout = 10000 ; SendEndEnabled = TRUE ; TerminationCharacter = 10 ; TerminationCharacterEnabled = TRUE ; BaudRate = 9600 ; DataBits = 8 ; EndIn = ASRL_END_TERMCHAR ; EndOut = ASRL_END_NONE ; FlowControl = ASRL_FLOW_DTR_DSR ; Parity = ASRL_PAR_NONE  ; StopBits = ASRL_STOP_ONE ";
                option_string = "Timeout = 10000  ;  BaudRate = 9600 ; DataBits = 8  ;  FlowControl = ASRL_FLOW_DTR_DSR ; Parity = ASRL_PAR_NONE  ; StopBits = ASRL_STOP_ONE ";

                // Open the I/O session with the driver
                //YJW: 打开通讯口
                data_logger.IO = (IMessage)mgr.Open(addr, AccessMode.NO_LOCK, 4000, option_string);
                
                Thread.Sleep(500);
                 
                // Set the instrument to remote
                //YJW: 通过通讯口发送COMMAND STRING，要求远程控制
                data_logger.WriteString("SYSTem:REMote", true);


                // Check and make sure the correct instrument is addressed
                //YJW: 设别连接设别的身份，确认是正确的设备
                data_logger.WriteString("*IDN?", true);
                msg = data_logger.ReadString();

                // if not 34970 or 34972 then error and return
                //YJW: 如果不是正确的设备，抛出错误
                if ((msg.IndexOf("34972A") < 0) && (msg.IndexOf("34970A") < 0))
                {
                    MessageBox.Show("Incorrect instrument addressed; use the correct address.", "VISACom");
                    //ioType.Text = "GPIB0::9";
                    //ioType.Refresh();
                    Globls.connected = false;
                    return false;
                }

                // Remove cr and or lf character
                //YJW: 除去身份识别字符串最后的结尾字符串
                msg = msg.Remove((msg.Length - 1), 1);

                // Check and make sure the 34901A Module is installed in slot 100;
                //YJW: 确认安装在机内的模块是34901A，否则抛出错误
                data_logger.WriteString("SYSTem:CTYPe? 100", true);
                msg = data_logger.ReadString();
                if (msg.IndexOf("34901A") < 0)
                {
                    MessageBox.Show("Incorrect Module Installed in slot 100!", "VISACom");
                }

                // Check if the DMM is installed; convert returned ASCII string to number.
                //YJW: 检查主机是否安装了DMM，即数字万用表，否则抛出错误
                data_logger.WriteString("INSTrument:DMM:INSTalled?", true);
                msg = data_logger.ReadString();
                if (Convert.ToInt16(msg) == 0)
                {
                    MessageBox.Show("DMM not installed; unable to make measurements.", "VISACom");
                }

                // Check if the DMM is enabled;; convert returned ASCII string to number.
                // Enable the DMM, if not enabled
                //YJW: 检查主机的DMM是否启用，否则启动DMM
                data_logger.WriteString("INSTrument:DMM?", true);
                msg = data_logger.ReadString();
                if (Convert.ToInt16(msg) == 0)
                    data_logger.WriteString("INSTrument:DMM ON", true);
                
                //YJW: 至此主机和PC的通讯已正常建立
                Globls.connected = true;

                return true;
            }
            catch (Exception)//YJW: 异常捕获处理代码块
            {
                //MessageBox.Show(e.Message + "\nin function: OpenPort. Likely due to bad IO address. \nVerify address field and press Select I/O button.",
                //    "VISACom");
                return false;
            }
        }

        public double Config34970A(List<string[]> GlobalConfigStrList, List<string[]> ChannelConfigStrList)
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        // This function performs the instrument setup.
        //YJW: 函数 配置安捷伦
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        {
            double DelayVal, TrigTime;
            string rdg;
            string msg;
            string NormalMsg="";


            // Check for exceptions
			//YJW: 使用TRY捕获异常
            try
            {
                // Reset instrument to turn-on condition
                // YJW: reset设备
                data_logger.WriteString("*RST", true);
                Thread.Sleep(750);
                //----------YJW CODE BEGIN----------
                //拼出通道设定字符串
                for (int k = 0; k < ChannelConfigStrList.Count; k++)
                {
                    for (int i = 1; i < ChannelConfigStrList[k].Length; i = i + 2)
                    {
                        NormalMsg = ChannelConfigStrList[k][0] + ChannelConfigStrList[k][i] + ", " + ChannelConfigStrList[k][i + 1];
                        //Console.WriteLine(NormalMsg);//若调试字符串取消此注释控制台输出
                        data_logger.WriteString(NormalMsg, true);//每拼出一条字符串，就送主机执行
                    }
                }
                NormalMsg = "";//清空

                //拼出全局设定字符串
                for (int k = 0; k < GlobalConfigStrList.Count; k++)
                {
                    for (int i = 1; i < GlobalConfigStrList[k].Length; i++)
                    {
                        NormalMsg = GlobalConfigStrList[k][0] + GlobalConfigStrList[k][i];
                        //Console.WriteLine(NormalMsg);//若调试字符串取消此注释控制台输出
                        data_logger.WriteString(NormalMsg, true);//每拼出一条字符串，就送主机执行
                    }
                }
                //----------YJW CODE END----------

                // Wait for instrument to setup
                //YJW: 确认上述设置已经完成
                data_logger.WriteString("*OPC?", true);
                rdg = data_logger.ReadString();

                // Gets the number of channels to be scanned and is used to determine 
                // the number of readings.
                //YJW: 从主机读回扫描的通道总数，传全局变量
                data_logger.WriteString("ROUTe:SCAN:SIZE?", true);
                Globls.NumChan = (double)data_logger.ReadNumber(IEEEASCIIType.ASCIIType_R8, true);

                // Gets the number of triggers; 34970A/34972A returns a floating-point number.
                //YJW: 从主机读回扫描触发的次数，传全局变量
                data_logger.WriteString("TRIGger:COUNt?", true);
                Globls.TrigCount = (double)data_logger.ReadNumber(IEEEASCIIType.ASCIIType_R8, true);

                // Get the delay; for future use; 34970A/34972A returns a floating-point number
                //YJW: 从主机读回通道间隔
                data_logger.WriteString("ROUTe:CHANnel:DELay? (@101)", true);
                DelayVal = (double)data_logger.ReadNumber(IEEEASCIIType.ASCIIType_R8, true);

                // Get the trigger time; 34970A/34972A returns a floating-point number
                //YJW: 从主机读回扫描触发间隔
                data_logger.WriteString("TRIGger:TIMer?", true);
                TrigTime = (double)data_logger.ReadNumber(IEEEASCIIType.ASCIIType_R8, true);

                // Calculate total number of readings
                //YJW: 计算正在扫描的总时间
                Globls.NumRdgs = Globls.NumChan * Globls.TrigCount;

                // Calculate total time
                //YJW: 计算整个测量的总时间
                Globls.TotTime = (TrigTime * Globls.TrigCount) - TrigTime + (Globls.NumChan * DelayVal);

                // Check and make sure the correct instrument is addressed
                //YJW: 再次确认主机身份
                data_logger.WriteString("*IDN?", true);
                msg = data_logger.ReadString();

                //Check for errors
                //YJW: 设定查错句柄，因为原来函数名称为Setup,所以传变量是"Setup"
                Check_Error("Setup");

                return Globls.TotTime;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nin function: Setup", "VISACom");
                return 0;
            }
        }


        public void Readings(ref ArrayList reading, ref ArrayList timestamp, ref ArrayList channelnum)
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        // This function triggers the instrument and takes readings.
        //YJW: 函数，读取安捷伦测量值
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        {
            string Dateval,	                // Returns the date value //YJW: 日期
                   Timeval;	                // Returns the time value //YJW: 时间
            int i;			                // Used for a loop        //YJW: 循环计数器
            object[] rdgs = new object[3];  // Returns reading from memory //YJW: OBJ容器，

            // Check for exceptions
			//YJW: 放在TRY里面捕获异常
            try
            {
                // Trigger instrument
                //YJW: 开始测量
                data_logger.WriteString("INITiate", true);

                // Get the date at which the scan was started
                //YJW: 从主机读测量开始的日期
                data_logger.WriteString("SYSTem:DATE?", true);
                Dateval = data_logger.ReadString();

                // Remove cr and or linefeed character
                //YJW: 去掉末尾标记用于显示
                if (Globls.addrtype == "ASRL")
                    Dateval = Dateval.Remove((Dateval.Length - 2), 2);
                else
                    Dateval = Dateval.Remove((Dateval.Length - 1), 1);

                // Get the time at which the scan was started
                //YJW: 从主机读测量开始的时间
                data_logger.WriteString("SYSTem:TIME?", true);
                Timeval = data_logger.ReadString();

                // Remove cr and or lf character
                //YJW：去掉末尾标记用于显示
                if (Globls.addrtype == "ASRL")
                    Timeval = Timeval.Remove((Timeval.Length - 2), 2);
                else
                    Timeval = Timeval.Remove((Timeval.Length - 1), 1);

                // Wait until instrument is finished taken readings. The instrument is queried 
                // until all channels are measured.
                //YJW: 等待主机测量完毕，主机内存中的数据记录数目等于设定应有的记录数目
                do
                {
                    data_logger.WriteString("DATA:POINTS?", true);
                    i = (Convert.ToInt16((double)data_logger.ReadNumber(IEEEASCIIType.ASCIIType_R8, true)));
                }
                while (i != Globls.NumRdgs);

                // Check for errors
                //YJW: 错误处理函数
                Check_Error("Readings");

                // Take readings out of memory one reading at a time. The "FETCh?" can also be used.
                // It reads all readings in memory, but leaves the readings in memory. The
                // "DATA:REMove?" command removes and erases the readings in memory.
                //YJW: 使用DATA:REMove?逐条从主机内存中读取并擦除记录,也可以使用FETCh,但是FETCh只读取不擦除
                reading.Clear(); timestamp.Clear(); channelnum.Clear();//YJW: 清空用来装测量数据的各个ARRAYLIST
                for (i = 0; i < Globls.NumRdgs; i++)//YJW: 逐条地
                {
                    data_logger.WriteString("DATA:REMove? 1", true);//READ AND REMOVE THE DATA FROM THE MENORY //YJW: 发送命令读取并且删除一条数据
                    rdgs = (object[])data_logger.ReadList(IEEEASCIIType.ASCIIType_BSTR, ",");//YJW: 用rdgs这个对象指针指向data_logger.Readlist这个对象

                    // Get reading
                    //YJW: 读取测量值
                    reading.Add(Convert.ToDouble(rdgs[0].ToString()));

                    //----------YJW CODE BEGIN----------
                    //YJW: 读取测量值的时间戳，时间戳是分散的，要拼起来
                      string temptimestamp = "";
                      for (int ind = 1; ind < rdgs.Length - 2; ind++)
                          temptimestamp += rdgs[ind].ToString();
                      timestamp.Add(temptimestamp);
                    //----------YJW CODE END----------

                    // Get channel number
                    //YJW: 读取测量值对应的通道号
                    channelnum.Add( Convert.ToInt16 (rdgs[rdgs.Length-1].ToString().Substring(0,3)));
                }
            }
            catch (Exception)//YJW: 异常捕获处理块
            {
                //MessageBox.Show(e.Message + "\nin function: Readings", "VISACom");
                //End_Prog();
            }
        }

        public void Check_Error(string msg)
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        // Checks for syntax and other errors.
        //YJW: 函数，用来检查主机是否报错
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        {
            int err_num;                    //YJW: 错误代码
            bool exit_code = false;         //YJW: 是否退出通讯，初值FALSE
            string err_msg, show_msg;       //YJW: 错误消息

            // Check for exceptions	
		    //YJW:放在TRY语句内捕获异常
            try
            {
                // Read error queue
                //YJW：向主机发送命令要求返回错误情况
                data_logger.WriteString("SYSTem:ERRor?", true);
                err_msg = data_logger.ReadString();

                // Get error number and message
                //YJW：读取主机返回的错误代码和错误消息 
                err_num = Convert.ToInt16(err_msg.Substring(0, err_msg.IndexOf(",")));
                err_msg = err_msg.Substring((err_msg.IndexOf(",") + 1), (err_msg.Length - (Convert.ToInt16(err_msg.IndexOf(","))) - 1));

                // If error found, check for more errors and exit program
                //YJW:如果错误代码不为0，即有错误
                while (err_num != 0)
                {
                    exit_code = true;//YJW: 退出通讯置TRUE
                   
                    //YJW: 拼出错误所在的函数字符串并显示之
                    //YJW：拼出错误讯息并且显示之
                    show_msg = "Error in: \"" + msg + "\" function\n";
                    show_msg += "Error: " + err_num.ToString() + "," + err_msg;

                    //20150424 屏蔽安捷伦报错弹出信息
                    //MessageBox.Show(show_msg, "VISACom");

                    //YJW:再次读取错误代码和错误讯息，直到无错误，退出while 
                    // Read error queue
                    data_logger.WriteString("SYSTem:ERRor?", true);
                    err_msg = data_logger.ReadString();

                    // Get error number and message
                    err_num = Convert.ToInt16(err_msg.Substring(0, err_msg.IndexOf(",")));
                    err_msg = err_msg.Substring((err_msg.IndexOf(",") + 1), (err_msg.Length - (Convert.ToInt16(err_msg.IndexOf(","))) - 1));
                }

                if (exit_code)//YJW: 退出通讯，主机清除错误队列
                {
                    data_logger.WriteString("*CLS", true);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nin function: CheckError", "VISACom");
                //End_Prog();
            }

        }

     }
}


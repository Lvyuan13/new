using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;


namespace Utility
{
    /// <summary>
    /// 西门子S7-200PLC通讯程序
    /// </summary>
    public class S7200
    {
        private object _lock = new object();
        #region 端口
        private SerialPort _port;
        /// <summary>
        /// 设备所用端口
        /// </summary>
        public SerialPort Port
        {
            get { return _port; }
            set { _port = value; }
        }
        #endregion

        #region 构造方法
        public S7200()
        {

        }

        public S7200(SerialPort serialPort)
        {
            this._port = serialPort;
        }

        public S7200(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            this._port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }
        #endregion

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="area">读取区域 Q I M 等</param>
        /// <param name="addrS">数据开始地址 格式：0.0</param>
        /// <param name="length">读取数据长度 默认：1</param>
        private byte Read(string area, string addrS, int length = 1)
        {
            lock (_lock)
            {
                byte[] PIDfx = new byte[33];
                PIDfx[0] = 0x68;//开始符
                PIDfx[1] = 0x1B;//长度
                PIDfx[2] = 0x1B;//长度
                PIDfx[3] = 0x68;//开始符
                PIDfx[4] = 0x02;//站好
                PIDfx[5] = 0x00;//源地址
                PIDfx[6] = 0x6C;//功能码 6C:读取 7C:写入
                PIDfx[7] = 0x32;//协议识别
                PIDfx[8] = 0x01;//远程控制
                PIDfx[9] = 0x00;//冗余识别
                PIDfx[10] = 0x00;//冗余识别
                PIDfx[11] = 0x00;//协议数据
                PIDfx[12] = 0x00;//单元参考
                PIDfx[13] = 0x00;//参数长度
                PIDfx[14] = 0x0E;//参数长度
                PIDfx[15] = 0x00;//数据长度
                PIDfx[16] = 0x00;//数据长度
                PIDfx[17] = 0x04;//读写 04：读  05：写
                PIDfx[18] = 0x01;//变量地址数
                PIDfx[19] = 0x12;
                PIDfx[20] = 0x0A;
                PIDfx[21] = 0x10;

                PIDfx[22] = 0x01;//读取长度 01:1bit 02:1byte 04:1word 06:double word
                PIDfx[23] = 0x00;//读取长度
                PIDfx[24] = 0x01;//数据个数 
                PIDfx[25] = 0x00;//数据个数
                PIDfx[26] = 0x00;//存储器类型 01:V存储器 00:其他
                switch (area)
                {
                    case "Q":
                        PIDfx[27] = 0x82;
                        break;
                    case "I":
                        PIDfx[27] = 0x81;
                        break;
                    case "M":
                        PIDfx[27] = 0x83;
                        break;
                }
                //PIDfx[27] = 0x83;//存储器类型 04:S 05:SM 06:AI 07:AQ 1E:C 81:I 82:Q 83:M 84:V 1F:T
                string[] addr = addrS.Split('.');
                byte temp2 = (byte)(double.Parse(addr[0]) * 8 + double.Parse(addr[1]));
                PIDfx[28] = 0x00;//偏移量
                PIDfx[29] = 0x00;//偏移量
                PIDfx[30] = temp2;//偏移量

                int temp = 0;
                for (int i = 4; i <= 30; i++)
                {
                    temp += PIDfx[i];
                }
                temp = temp % 256;

                PIDfx[31] = (byte)temp;//效验码
                PIDfx[32] = 0x16;//结束符

                byte[] PIDfx2 = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };//执行命令

                if (!_port.IsOpen)
                {
                    _port.Open();
                }
                _port.ReadTimeout = 1000;
                _port.DiscardInBuffer();
                _port.DiscardOutBuffer();
                _port.Write(PIDfx, 0, 33);
                //System.Threading.Thread.Sleep(200);
                int startTick = Environment.TickCount;
                while (_port.BytesToRead < 1 && Environment.TickCount - startTick < 500)
                {
                    System.Threading.Thread.Sleep(20);
                }
                _port.Write(PIDfx2, 0, 6);
                startTick = Environment.TickCount;
                while (_port.BytesToRead < 29 && Environment.TickCount - startTick < 500)
                {
                    System.Threading.Thread.Sleep(20);
                }
                byte[] results = new byte[29];
                _port.Read(results, 0, 29);
                System.Threading.Thread.Sleep(20);
                _port.Close();
                return results[26];
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="area">读取区域 Q I M 等</param>
        /// <param name="addrS">数据开始地址 格式：0.0</param>
        /// <param name="value">读取数据长度</param>------------------
        private void Set(string area, string addrS, byte value)
        {
            lock (_lock)
            {
                byte[] PIDfx = new byte[38];
                PIDfx[0] = 0x68;//开始符
                PIDfx[1] = 0x20;//长度
                PIDfx[2] = 0x20;//长度
                PIDfx[3] = 0x68;//开始符
                PIDfx[4] = 0x02;//站好
                PIDfx[5] = 0x00;//源地址
                PIDfx[6] = 0x7C;//功能码 6C:读取 7C:写入
                PIDfx[7] = 0x32;//协议识别
                PIDfx[8] = 0x01;//远程控制
                PIDfx[9] = 0x00;//冗余识别
                PIDfx[10] = 0x00;//冗余识别
                PIDfx[11] = 0x00;//协议数据
                PIDfx[12] = 0x00;//单元参考
                PIDfx[13] = 0x00;//参数长度
                PIDfx[14] = 0x0E;//参数长度
                PIDfx[15] = 0x00;//数据长度
                PIDfx[16] = 0x05;//数据长度 05:位或字节 06:字 08:双字 0C:8个字节
                PIDfx[17] = 0x05;//读写 04：读  05：写
                PIDfx[18] = 0x01;//变量地址数
                PIDfx[19] = 0x12;
                PIDfx[20] = 0x0A;
                PIDfx[21] = 0x10;

                PIDfx[22] = 0x01;//读取长度 01:1bit 02:1byte 04:1word 06:double word
                PIDfx[23] = 0x00;//读取长度
                PIDfx[24] = 0x01;//数据个数 
                PIDfx[25] = 0x00;//数据个数
                PIDfx[26] = 0x00;//存储器类型 01:V存储器 00:其他
                switch (area)
                {
                    case "Q":
                        PIDfx[27] = 0x82;
                        break;
                    case "I":
                        PIDfx[27] = 0x81;
                        break;
                    case "M":
                        PIDfx[27] = 0x83;
                        break;
                }
                //PIDfx[27] = 0x83;//存储器类型 04:S 05:SM 06:AI 07:AQ 1E:C 81:I 82:Q 83:M 84:V 1F:T

                string[] addr = addrS.Split('.');
                byte temp2 = (byte)(double.Parse(addr[0]) * 8 + double.Parse(addr[1]));

                PIDfx[28] = 0x00;//偏移量
                PIDfx[29] = 0x00;//偏移量
                PIDfx[30] = temp2;//偏移量
                PIDfx[31] = 0x00;//数据形式
                PIDfx[32] = 0x03;//数据形式 03:位 04:其他
                PIDfx[33] = 0x00;//数据位数
                PIDfx[34] = 0x01;//数据位数
                PIDfx[35] = value;//写入值 0或者1

                int temp = 0;
                for (int i = 4; i <= 35; i++)
                {
                    temp += PIDfx[i];
                }
                temp = temp % 256;

                PIDfx[36] = (byte)temp;//效验码
                PIDfx[37] = 0x16;//结束符

                byte[] PIDfx2 = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };//执行命令

                if (!_port.IsOpen)
                {
                    
                    _port.Open();
                }
                _port.ReadTimeout = 1000;
                _port.DiscardInBuffer();
                _port.DiscardOutBuffer();
                _port.Write(PIDfx2, 0, 6);
                int startTick = Environment.TickCount;
                while (_port.BytesToRead < 28 && Environment.TickCount - startTick < 200)
                {
                    System.Threading.Thread.Sleep(20);
                }
                _port.Write(PIDfx, 0, 38);
                System.Threading.Thread.Sleep(100);
                _port.Close();
                this.Read("M", "4.6");//因为set后第一句READ出问题，添加假的读取
                this.Read("M", "4.7");//因为set第一句READ出问题，添加假的读取
            }
        }

        /// <summary>
        /// 读取Word数据
        /// </summary>
        /// <param name="area">读取区域 VM </param>
        /// <param name="addr">数据开始地址 格式：100-140</param>
        /// <param name="digit">小数位</param>
        public double ReadWord(string area, string addr, int digit)
        {
            lock (_lock)
            {
                byte[] PIDfx = new byte[33];
                PIDfx[0] = 0x68;//开始符
                PIDfx[1] = 0x1B;//长度
                PIDfx[2] = 0x1B;//长度
                PIDfx[3] = 0x68;//开始符
                PIDfx[4] = 0x02;//站好
                PIDfx[5] = 0x00;//源地址
                PIDfx[6] = 0x6C;//功能码 6C:读取 7C:写入
                PIDfx[7] = 0x32;//协议识别
                PIDfx[8] = 0x01;//远程控制
                PIDfx[9] = 0x00;//冗余识别
                PIDfx[10] = 0x00;//冗余识别
                PIDfx[11] = 0x00;//协议数据
                PIDfx[12] = 0x00;//单元参考
                PIDfx[13] = 0x00;//参数长度
                PIDfx[14] = 0x0E;//参数长度
                PIDfx[15] = 0x00;//数据长度
                PIDfx[16] = 0x00;//数据长度
                PIDfx[17] = 0x04;//读写 04：读  05：写
                PIDfx[18] = 0x01;//变量地址数
                PIDfx[19] = 0x12;
                PIDfx[20] = 0x0A;
                PIDfx[21] = 0x10;

                PIDfx[22] = 0x04;//读取长度 01:1bit 02:1byte 04:1word 06:double word
                PIDfx[23] = 0x00;//读取长度

                PIDfx[24] = 0x01;//数据个数
                PIDfx[25] = 0x00;//数据个数
                PIDfx[26] = 0x01;//存储器类型 01:V存储器 00:其他
                PIDfx[27] = 0x84;
                //PIDfx[27] = 0x83;//存储器类型 04:S 05:SM 06:AI 07:AQ 1E:C 81:I 82:Q 83:M 84:V 1F:T
                //string[] addr = addrS.Split('.');
                int temp2 = int.Parse(addr) * 8;   //(double.Parse(addr[0]) * 8 + double.Parse(addr[1]));
                PIDfx[28] = (byte)(temp2 / 65536);//偏移量
                PIDfx[29] = (byte)(temp2 / 256);//偏移量
                PIDfx[30] = (byte)(temp2 % 256);//偏移量

                int temp = 0;
                for (int i = 4; i <= 30; i++)
                {
                    temp += PIDfx[i];
                }
                temp = temp % 256;

                PIDfx[31] = (byte)temp;//效验码
                PIDfx[32] = 0x16;//结束符

                byte[] PIDfx2 = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };//执行命令

                if (!_port.IsOpen)
                {
                    _port.Open();
                }
                //_port.ReadTimeout = 1000;
                _port.DiscardInBuffer();
                _port.DiscardOutBuffer();
                _port.Write(PIDfx, 0, 33);
                //System.Threading.Thread.Sleep(200);
                int startTick = Environment.TickCount;
                while (_port.BytesToRead < 1 && Environment.TickCount - startTick < 500)
                {
                    System.Threading.Thread.Sleep(20);
                }
                _port.Write(PIDfx2, 0, 6);
                startTick = Environment.TickCount;
                while (_port.BytesToRead < 30 && Environment.TickCount - startTick < 500)
                {
                    System.Threading.Thread.Sleep(20);
                }
                byte[] results = new byte[31];
                _port.Read(results, 0, 31);
                System.Threading.Thread.Sleep(100);
                _port.Close();
                return (results[26] * 256 + results[27]) / Math.Pow(10, digit);
            }
        }

        /// <summary>
        /// 读取Words数据
        /// </summary>
        /// <param name="area">读取区域 VM </param>
        /// <param name="addr">数据开始地址 格式：100-140</param>
        /// <param name="wordNum">数据量</param>
        public double[] ReadWords(string area, string addr, int wordNum)
        {
            lock (_lock)
            {
                byte[] PIDfx = new byte[33];
                PIDfx[0] = 0x68;//开始符
                PIDfx[1] = 0x1B;//长度
                PIDfx[2] = 0x1B;//长度
                PIDfx[3] = 0x68;//开始符
                PIDfx[4] = 0x02;//站好
                PIDfx[5] = 0x00;//源地址
                PIDfx[6] = 0x6C;//功能码 6C:读取 7C:写入
                PIDfx[7] = 0x32;//协议识别
                PIDfx[8] = 0x01;//远程控制
                PIDfx[9] = 0x00;//冗余识别
                PIDfx[10] = 0x00;//冗余识别
                PIDfx[11] = 0x00;//协议数据
                PIDfx[12] = 0x00;//单元参考
                PIDfx[13] = 0x00;//参数长度
                PIDfx[14] = 0x0E; //(byte)(4 + wordNum * 10);//参数长度  4+数据块数*10
                PIDfx[15] = 0x00;//数据长度
                PIDfx[16] = 0x00;//数据长度
                PIDfx[17] = 0x04;//读写 04：读  05：写
                PIDfx[18] = 0x01;//变量地址数
                PIDfx[19] = 0x12;
                PIDfx[20] = 0x0A;
                PIDfx[21] = 0x10;

                PIDfx[22] = 0x02;//读取长度 01:1bit 02:1byte 04:1word 06:double word
                PIDfx[23] = 0x00;//读取长度

                PIDfx[24] = (byte)(wordNum * 2);//数据个数
                PIDfx[25] = 0x00;//数据个数
                PIDfx[26] = 0x01;//存储器类型 01:V存储器 00:其他
                PIDfx[27] = 0x84;
                //PIDfx[27] = 0x83;//存储器类型 04:S 05:SM 06:AI 07:AQ 1E:C 81:I 82:Q 83:M 84:V 1F:T
                //string[] addr = addrS.Split('.');
                int temp2 = int.Parse(addr) * 8;   //(double.Parse(addr[0]) * 8 + double.Parse(addr[1]));
                PIDfx[28] = (byte)(temp2 / 65536);//偏移量
                PIDfx[29] = (byte)(temp2 / 256);//偏移量
                PIDfx[30] = (byte)(temp2 % 256);//偏移量

                int temp = 0;
                for (int i = 4; i <= 30; i++)
                {
                    temp += PIDfx[i];
                }
                temp = temp % 256;

                PIDfx[31] = (byte)temp;//效验码
                PIDfx[32] = 0x16;//结束符

                byte[] PIDfx2 = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };//执行命令

                if (!_port.IsOpen)
                {
                    _port.Open();
                }
                //_port.ReadTimeout = 1000;
                _port.DiscardInBuffer();
                _port.DiscardOutBuffer();
                _port.Write(PIDfx, 0, 33);
                //System.Threading.Thread.Sleep(200);
                int startTick = Environment.TickCount;
                while (_port.BytesToRead < 1 && Environment.TickCount - startTick < 500)
                {
                    System.Threading.Thread.Sleep(20);
                }
                _port.Write(PIDfx2, 0, 6);
                int readCount = 28 + wordNum * 2;
                startTick = Environment.TickCount;
                while (_port.BytesToRead < readCount && Environment.TickCount - startTick < 500)
                {
                    System.Threading.Thread.Sleep(20);
                }
                byte[] results = new byte[readCount];
                _port.Read(results, 0, readCount);
                _port.Close();

                double[] res = new double[wordNum];
                for (int i = 0; i < wordNum; i++)
                {
                    res[i] = results[26 + i * 2] * 256 + results[27 + i * 2];
                }
                return res;
                //return (results[26] * 256 + results[27]) / Math.Pow(10, wordNum);
            }
        }

        /// <summary>
        /// 设定Word数据
        /// </summary>
        /// <param name="area">区域 VM</param>
        /// <param name="addr">数据开始地址 格式：100-140</param>
        /// <param name="value">设定值</param>
        public void SetWord(string area, string addr, int value)
        {
            lock (_lock)
            {
                byte[] PIDfx = new byte[39];
                PIDfx[0] = 0x68;//开始符
                PIDfx[1] = 0x21;//长度
                PIDfx[2] = 0x21;//长度
                PIDfx[3] = 0x68;//开始符
                PIDfx[4] = 0x02;//站好
                PIDfx[5] = 0x00;//源地址
                PIDfx[6] = 0x7C;//功能码 6C:读取 7C:写入
                PIDfx[7] = 0x32;//协议识别
                PIDfx[8] = 0x01;//远程控制
                PIDfx[9] = 0x00;//冗余识别
                PIDfx[10] = 0x00;//冗余识别
                PIDfx[11] = 0x00;//协议数据
                PIDfx[12] = 0x00;//单元参考
                PIDfx[13] = 0x00;//参数长度
                PIDfx[14] = 0x0E;//参数长度
                PIDfx[15] = 0x00;//数据长度
                PIDfx[16] = 0x06;//数据长度 05:位或字节 06:字 08:双字 0C:8个字节
                PIDfx[17] = 0x05;//读写 04：读  05：写
                PIDfx[18] = 0x01;//变量地址数
                PIDfx[19] = 0x12;
                PIDfx[20] = 0x0A;
                PIDfx[21] = 0x10;

                PIDfx[22] = 0x04;//读取长度 01:1bit 02:1byte 04:1word 06:double word
                PIDfx[23] = 0x00;//读取长度
                PIDfx[24] = 0x01;//数据个数 
                PIDfx[25] = 0x00;//数据个数
                PIDfx[26] = 0x01;//存储器类型 01:V存储器 00:其他
                PIDfx[27] = 0x84;//存储器类型 04:S 05:SM 06:AI 07:AQ 1E:C 81:I 82:Q 83:M 84:V 1F:T

                //string[] addr = addrS.Split('.');
                //byte temp2 = (byte)(double.Parse(addr[0]) * 8 + double.Parse(addr[1]));

                int temp2 = int.Parse(addr) * 8;   //(double.Parse(addr[0]) * 8 + double.Parse(addr[1]));
                PIDfx[28] = (byte)(temp2 / 65536);//偏移量
                PIDfx[29] = (byte)(temp2 / 256);//偏移量
                PIDfx[30] = (byte)(temp2 % 256);//偏移量
                PIDfx[31] = 0x00;//数据形式
                PIDfx[32] = 0x04;//数据形式 03:位 04:其他
                PIDfx[33] = 0x00;//数据位数
                PIDfx[34] = 0x10;//数据位数 01:1bit  08:1byte  10H:1Word  20H:1Double Word
                PIDfx[35] = (byte)(value / 256);//value
                PIDfx[36] = (byte)(value % 256);//value

                int temp = 0;
                for (int i = 4; i <= 36; i++)
                {
                    temp += PIDfx[i];
                }
                temp = temp % 256;

                PIDfx[37] = (byte)temp;//效验码
                PIDfx[38] = 0x16;//结束符

                byte[] PIDfx2 = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };//执行命令

                if (!_port.IsOpen)
                {
                    _port.Open();
                }
                _port.ReadTimeout = 1000;
                _port.DiscardInBuffer();
                _port.DiscardOutBuffer();
                _port.Write(PIDfx2, 0, 6);
                int startTick = Environment.TickCount;
                while (_port.BytesToRead < 69 && Environment.TickCount - startTick < 200)
                {
                    System.Threading.Thread.Sleep(20);
                }
                _port.Write(PIDfx, 0, 39);
                System.Threading.Thread.Sleep(100);
                this.Read("M", "4.6");//因为set后第一句READ出问题，添加假的读取
                this.Read("M", "4.7");//因为set第一句READ出问题，添加假的读取
                _port.Close();
            }
        }

        ///<summary>
        /// 阀件专用的报警批量读取函数
        /// 读取所有报警 M2.1-2.7 M3.0-3.7 M4.0-4.7 返回值也是按照这个顺序的
        /// </summary>
        /// <returns></returns>
        public byte[] BatReadAlarm()
        {
            lock (_lock)
            {
                byte[] PIDfx = new byte[33];
                PIDfx[0] = 0x68;//开始符
                PIDfx[1] = 0x1B;//长度
                PIDfx[2] = 0x1B;//长度
                PIDfx[3] = 0x68;//开始符
                PIDfx[4] = 0x02;//站好
                PIDfx[5] = 0x00;//源地址
                PIDfx[6] = 0x6C;//功能码 6C:读取 7C:写入
                PIDfx[7] = 0x32;//协议识别
                PIDfx[8] = 0x01;//远程控制
                PIDfx[9] = 0x00;//冗余识别
                PIDfx[10] = 0x00;//冗余识别
                PIDfx[11] = 0x00;//协议数据
                PIDfx[12] = 0x00;//单元参考
                PIDfx[13] = 0x00;//参数长度
                PIDfx[14] = 0x0E;//参数长度
                PIDfx[15] = 0x00;//数据长度
                PIDfx[16] = 0x00;//数据长度
                PIDfx[17] = 0x04;//读写 04：读  05：写
                PIDfx[18] = 0x01;//变量地址数
                PIDfx[19] = 0x12;
                PIDfx[20] = 0x0A;
                PIDfx[21] = 0x10;

                PIDfx[22] = 0x02;//读取长度 01:1bit 02:1byte 04:1word 06:double word
                PIDfx[23] = 0x00;//读取长度

                PIDfx[24] = 0x03;//数据个数
                PIDfx[25] = 0x00;//数据个数
                PIDfx[26] = 0x00;//存储器类型 01:V存储器 00:其他
                PIDfx[27] = 0x83;//存储器类型 04:S 05:SM 06:AI 07:AQ 1E:C 81:I 82:Q 83:M 84:V 1F:T
                PIDfx[28] = 0x00;//偏移量
                PIDfx[29] = 0x00;//偏移量
                PIDfx[30] = 17;//偏移量

                int temp = 0;
                for (int i = 4; i <= 30; i++)
                {
                    temp += PIDfx[i];
                }
                temp = temp % 256;

                PIDfx[31] = (byte)temp;//效验码
                PIDfx[32] = 0x16;//结束符

                byte[] PIDfx2 = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };//执行命令

                if (!_port.IsOpen)
                {
                    _port.Open();
                }
                _port.ReadTimeout = 1000;
                _port.DiscardInBuffer();
                _port.DiscardOutBuffer();
                _port.Write(PIDfx, 0, 33);
                int startTick = Environment.TickCount;
                while (_port.BytesToRead < 1 && Environment.TickCount - startTick < 500)
                {
                    System.Threading.Thread.Sleep(1);
                }
                _port.Write(PIDfx2, 0, 6);
                startTick = Environment.TickCount;
                while (_port.BytesToRead < 31 && Environment.TickCount - startTick < 500)
                {
                    System.Threading.Thread.Sleep(1);
                }

                byte[] res = new byte[17];
                if (_port.BytesToRead == 31)
                {
                    byte[] results = new byte[31];
                    _port.Read(results, 0, 31);
                    string t1 = Convert.ToString(results[28], 2).PadLeft(8, '0');
                    t1 = t1 + Convert.ToString(results[27], 2).PadLeft(8, '0');
                    t1 = t1 + Convert.ToString(results[26], 2).PadLeft(8, '0');

                    char[] t2 = t1.ToArray();
                    Array.Reverse(t2);
                    for (int i = 0; i < t2.Length; i++)
                    {
                        res[i] = Convert.ToByte(t2[i].ToString());
                    }
                }
                _port.Close();
                return res;
            }
        }
        
        ////----------实际使用的PLC类和函数----------//
        //版本：阀件版本
        //解决set后第一句read不正确的问题，在set函数内进行了修改

        

        private bool _IsSimulate = false;
        public bool IsSimulate
        {
            get { return _IsSimulate; }
            set { _IsSimulate = value; }
        }

        /// <summary>
        /// 方法：PLC读回寄存器状态
        /// </summary>
        /// <param name="area">寄存器区名称</param>
        /// <param name="addrS">寄存器位地址</param>
        /// <returns>寄存器状态bool</returns>
        public bool TFRead(string area, string addrS)
        {
            bool tf=false;
            if (!_IsSimulate)
            {

                byte res = 0;
                //this.Read("M", "4.6",1);//因为set后第一句READ出问题，添加假的读取
                //this.Read("M", "4.7 ",1);//因为set第一句READ出问题，添加假的读取
                res = this.Read(area, addrS, 1);

                if (res == 1)
                    tf = true;
                else
                    tf = false;
            }
            else
            {
                System.Threading.Thread.Sleep(20);
            }
                return tf;
        }

        public void TFSet(string area, string addrS, bool TFvalue)
        {
            if (!_IsSimulate)
            {
                byte IntINP = 0;
                if (TFvalue)
                    IntINP = 1;
                else
                    IntINP = 0;
                this.Set(area, addrS, IntINP);
            }
            else
            {
                System.Threading.Thread.Sleep(20);
            }
        }







    }
}

using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace SHHS.FPTB.Base
{
    public class Test
    {
        //计时器
        public DispatcherTimer _timer;
        /// <summary>
        /// 判断试验是否正在运行
        /// </summary>
        private bool _isRunning = false;
        /// <summary>
        /// 判断试验是否结束
        /// </summary>
        private bool _isFinished = false;
        /// <summary>
        /// 是否暂停状态
        /// </summary>
        private bool _isPausing = false;
        /// <summary>
        /// 获取 当前试验是否暂停状态
        /// </summary>
        public bool IsPausing
        {
            get { return _isPausing; }
        }
        /// <summary>
        /// 采样频率
        /// </summary>
        private double _samplingSpeed = 1;

        private DateTime _startTime;
        /// <summary>
        /// 试验开始时间 只读
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
        }
        

        #region Method
        /// <summary>
        /// 构造方法
        /// </summary>
        public Test()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Tick += new EventHandler(timer_Tick);
        }
        /// <summary>
        /// 计时器事件处理程序
        /// </summary>
        protected void timer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            try
            {
                CancelEventArgs cea = new CancelEventArgs(false);
                if (this.Beginning != null)
                    this.Beginning(this, cea);
                if (cea.Cancel)
                {
                    return;
                }
                _timer.Start();
                _startTime = DateTime.Now;
                _isRunning = true;
                _isPausing = false;
            }
            catch (Exception ex)
            {
                this.OnErrorHappen(new ErrorEventArgs(ex, "Testing.Start()"));
            }
        }
        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            try
            {
                _timer.Stop();
                _isPausing = true;
                if (this.Pausing != null)
                {
                    this.Pausing(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                this.OnErrorHappen(new ErrorEventArgs(ex, "Testing.Pause()"));
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        public void Accept()
        {
            try
            {
                //_timer.Stop();
                if (this.Accepted != null)
                {
                    this.Accepted(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                this.OnErrorHappen(new ErrorEventArgs(ex, "Testing.Accept()"));
            }
        }
        /// <summary>
        /// 结束
        /// </summary>
        public void Finish()
        {
            try
            {
                this._timer.Stop();

                _isRunning = false;
                _isFinished = true;
                if (this.Finished != null)
                    this.Finished(this, new EventArgs());
            }
            catch (Exception ex)
            {
                this.OnErrorHappen(new ErrorEventArgs(ex, "Testing.Finish()"));
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        protected void Refresh()
        {
            try
            {
                if (this.Refreshed != null)
                {
                    this.Refreshed(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                this.OnErrorHappen(new ErrorEventArgs(ex, "Testing.Refresh()"));
            }
        }
        /// <summary>
        /// 错误发生时
        /// </summary>
        /// <param name="error">异常情况</param>
        protected void OnErrorHappen(ErrorEventArgs error)
        {
            if (this.ErrorHappened != null)
            {
                this.ErrorHappened(this, error);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// 开始前引发的事件
        /// </summary>
        public event CancelEventHandler Beginning;

        /// <summary>
        /// 暂停引发的事件
        /// </summary>
        public event EventHandler Pausing;

        /// <summary>
        /// 结束后引发的事件
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// 接收方法使用后引发的事件
        /// </summary>
        public event EventHandler Accepted;

        /// <summary>
        /// 每次刷新引发的事件
        /// </summary>
        public event EventHandler Refreshed;

        /// <summary>
        /// 发生错误异常引发的事件
        /// </summary>
        public event ErrorEventHandler ErrorHappened;
        #endregion
    }

    public delegate void ErrorEventHandler(object sender, ErrorEventArgs error);

    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs()
        {

        }

        public ErrorEventArgs(Exception exception, string errorPosition)
        {
            this.Error = exception;
        }

        public Exception Error { get; set; }

        public string ErrorPosition { get; set; }
    }
}

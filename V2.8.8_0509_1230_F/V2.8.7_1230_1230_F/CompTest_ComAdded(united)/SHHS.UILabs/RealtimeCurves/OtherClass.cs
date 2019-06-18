using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Threading;

namespace SHHS.UILabs.RealtimeCurves
{
     /// <summary>
    /// 用于确定轴(Axis类)的方向  Level:水平的   Vertical:垂直的
    /// </summary>
    public enum AxisMode
    {
        /// <summary>
        /// 水平的
        /// </summary>
        Level,
        /// <summary>
        /// 垂直的
        /// </summary>
        Vertical  
    }

    public enum BtnMode
    {
        /// <summary>
        /// 放大
        /// </summary>
        ZoomIn,
        /// <summary>
        /// 光标
        /// </summary>
        Cursor,
        /// <summary>
        /// 手动滚动
        /// </summary>
        ManualScroll,
        /// <summary>
        /// 恢复键
        /// </summary>
        ComeBack
    }

    /// <summary>
    /// 曲线移动的方式
    /// </summary>
    public enum MoveMode
    {
        /// <summary>
        /// 平移
        /// </summary>
        Parallel,
        /// <summary>
        /// 压缩
        /// </summary>
        Compression 
    }

    /// <summary>
    /// 记录MonitorSetter一条Y轴控件
    /// </summary>
    public class YAxisData
    {
        public CheckBox YAxisName;
        public TextBox YAxisMaximum;
        public TextBox YAxisMinimum;
        public Button YAxisColor;
        public string Unit;
        public YAxisData(CheckBox yAxisName, TextBox yAxisMaximum, TextBox yAxisMinimum, Button yAxisColor, string unit)
        {
            this.YAxisName = yAxisName;
            this.YAxisMaximum = yAxisMaximum;
            this.YAxisMinimum = yAxisMinimum;
            this.YAxisColor = yAxisColor;
            this.Unit = unit;
        }
    }

    /// <summary>
    /// 保存MonitorSetter所有的通道控件
    /// </summary>
    public class ChannelData
    {
        public ComboBox ItemCBox;
        public ComboBox ChannelCBox;
        public Button ChannelColor;
        public ChannelData(ComboBox item, ComboBox channel, Button channelColor)
        {
            this.ItemCBox = item;
            this.ChannelCBox = channel;
            this.ChannelColor = channelColor;
        }
    }

    /// <summary>
    /// 保存为每条曲线附加的控件,包括CheckBox:控制曲线显示或隐藏并显示曲线名字 Button:显示曲线颜色并控制曲线是否高亮 TextBlack:显示曲线最新的数据值
    /// </summary>
    public class AppendControl
    {
        private CheckBox _checkBox;
        private Button _button;
        private TextBlock _textBlock;
        private Grid _grid;
        private Curve _curve;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="checkbox">控制曲线显示或隐藏并显示曲线名字</param>
        /// <param name="button">显示曲线颜色并控制曲线是否高亮</param>
        /// <param name="textblock">显示曲线最新的数据值</param>
        /// <param name="grid">控件显示的平台</param>
        /// <param name="curve">附加控件对应的曲线</param>
        public AppendControl(CheckBox checkbox, Button button, TextBlock textblock, Grid grid)
        {
            this._grid = grid;

            this._checkBox = checkbox;
            this._checkBox.HorizontalAlignment = HorizontalAlignment.Right;
            this._checkBox.VerticalAlignment = VerticalAlignment.Top;
            this._checkBox.Height = 16;
            this._checkBox.Width = 162;
            this._checkBox.Checked += new RoutedEventHandler(CheckBox_Checked);
            this._checkBox.Unchecked += new RoutedEventHandler(CheckBox_Unchecked);

            this._button = button;
            this._button.HorizontalAlignment = HorizontalAlignment.Right;
            this._button.VerticalAlignment = VerticalAlignment.Top;
            this._button.Click += new RoutedEventHandler(Button_Click);
            this._button.Height = 16;
            this._button.Width = 16;

            this._textBlock = textblock;
            this._textBlock.HorizontalAlignment = HorizontalAlignment.Right;
            this._textBlock.VerticalAlignment = VerticalAlignment.Top;
            this._textBlock.TextAlignment = TextAlignment.Center;
            this._textBlock.Height = 16;
            this._textBlock.Width = 57;
            this._textBlock.Background = Brushes.WhiteSmoke;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this._curve.IsVisible = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this._curve.IsVisible = false;             
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (this._curve.IsVisible)
            {
                if (btn.Background == this._curve.OriginalColor)
                {
                    btn.Background = this._curve.HighlightColor;
                    this._curve.IsHighlight = true;
                }
                else
                {
                    btn.Background = this._curve.OriginalColor;
                    this._curve.IsHighlight = false;
                }
            }
        }

        ///// <summary>
        ///// 刷新TextBlock,用以显示曲线最新的数据
        ///// </summary>
        //public void RefreshTextBlock()
        //{
        //    if (this._curve.SourceData.Count > 0)
        //    {
        //        this._textBlock.Text = this._curve.SourceData.Last().Value.ToString();
        //    }
        //    else
        //    {
        //        this._textBlock.Text = "";
        //    }
        //}
        /// <summary>
        /// 刷新TextBlock,用以显示曲线最新的数据
        /// </summary>
        public void RefreshTextBlock()
        {
            this.Grid.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new ThreadStart(this.RefreshTextBlockViaDispatcher));
        }
        /// <summary>
        /// 刷新TextBlock,用以显示曲线最新的数据
        /// </summary>
        public void RefreshTextBlockViaDispatcher()
        {
            if (this._curve.SourceData.Count > 0)
            {
                this._textBlock.Text = this._curve.SourceData.Last().Value.ToString();
            }
            else
            {
                this._textBlock.Text = "";
            }
        }

        /// <summary>
        /// 显示附加控件
        /// </summary>
        public void Display()
        {
            try
            {
                _grid.Children.Add(_checkBox);
                _grid.Children.Add(_button);
                _grid.Children.Add(_textBlock);
            }
            catch
            {
                MessageBox.Show("控件已经在显示了!");
            }
        }

        /// <summary>
        /// 删除附加控件
        /// </summary>
        public void Delete()
        {
            _grid.Children.Remove(_checkBox);
            _grid.Children.Remove(_button);
            _grid.Children.Remove(_textBlock);
        }

        /// <summary>
        /// 控制曲线显示或隐藏并显示曲线名字
        /// </summary>
        public CheckBox CheckBox
        {
            get { return _checkBox; }
            set { _checkBox = value; }
        }

        /// <summary>
        /// 显示曲线颜色并控制曲线是否高亮
        /// </summary>
        public Button Button
        {
            get { return _button; }
            set { _button = value; }
        }

        /// <summary>
        /// 显示曲线最新的数据值
        /// </summary>
        public TextBlock TextBlock
        {
            get { return _textBlock; }
            set { _textBlock = value; }
        }

        /// <summary>
        /// 控件显示的平台
        /// </summary>
        public Grid Grid
        {
            get { return _grid; }
            set { _grid = value; }
        }

        /// <summary>
        /// 附加控件对应的曲线
        /// </summary>
        public Curve Curve
        {
            get { return _curve; }
            set { _curve = value; }
        }
    }
}
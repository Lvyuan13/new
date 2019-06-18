using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SHHS.UILabs.RealtimeCurves
{
    /// <summary>
    /// MultiSetter.xaml 的交互逻辑
    /// </summary>
    public partial class MultiSetter : Window
    {
        private List<RealtimeCurves> _monitors;
        private List<RealtimeCurvesSetting> _monitorSetters = new List<RealtimeCurvesSetting>();
        private SolidColorBrush _tempColor = new SolidColorBrush();
        private bool ONorOFF = false;

        public MultiSetter(params RealtimeCurves[] monitorArray)
        {
            InitializeComponent();
            this._monitors = monitorArray.ToList();
            DisplaySetter();
        }

        /// <summary>
        /// 分页设置类的构造方法
        /// </summary>
        /// <param name="monitors">传入一个Monitor的List数组</param>
        public MultiSetter(List<RealtimeCurves> monitors)
        {
            InitializeComponent();
            this._monitors = monitors;
            DisplaySetter();
        }

        private void DisplaySetter()
        {
            if (this._monitors.Count >= 1)
            {
                this.Resources = this._monitors[0].Resources;
            }
            for (int i = 0; i < this._monitors.Count; i++)
            {
                TabItem tabItem = new TabItem();
                RealtimeCurvesSetting monSet = new RealtimeCurvesSetting(_monitors[i]);
                Grid grid = new Grid();
                tabItem.Name = "MonitorSetter" + (i + 1).ToString();

                if (this._monitors.Count > 1)
                {
                    tabItem.Header = "Monitor" + (i + 1).ToString();
                }
                else
                {
                    tabItem.Header = "Monitor";
                }
                grid.Children.Add(monSet);
                _monitorSetters.Add(monSet);
                tabItem.Content = grid;
                tabControl.Items.Add(tabItem);
                foreach (YAxisData yad in monSet.YAxisDatas.Values)
                {
                    yad.YAxisColor.Click += new RoutedEventHandler(SetColor_Click);

                    //yad.YAxisColor.AllowDrop = true;    //08.13 用于添加 拖拽改变颜色 事件
                    //yad.YAxisColor.PreviewDrop += new DragEventHandler(YAxisColor_PreviewDrop);   //08.13 用于添加 拖拽改变颜色 事件
                    //((yad.YAxisColor.Parent) as Label).PreviewDrop += new DragEventHandler(MultiSetter_PreviewDrop);

                    Label lbl = LogicalTreeHelper.GetParent(yad.YAxisColor) as Label;
                    lbl.AllowDrop = true;
                    lbl.PreviewDrop += new DragEventHandler(MultiSetter_PreviewDrop);
                }
                foreach (ChannelData chd in monSet.ChannelDatas.Values)
                {
                    chd.ChannelColor.Click += new RoutedEventHandler(SetColor_Click);

                    //chd.ChannelColor.AllowDrop = true;   //08.13 用于添加 拖拽改变颜色 事件
                    //chd.ChannelColor.PreviewDrop += new DragEventHandler(ChannelColor_PreviewDrop);   //08.13 用于添加 拖拽改变颜色 事件
                    //((chd.ChannelColor.Parent) as Label).PreviewDrop += new DragEventHandler(MultiSetter_PreviewDrop);

                    Label lbl = LogicalTreeHelper.GetParent(chd.ChannelColor) as Label;
                    lbl.AllowDrop = true;
                    lbl.PreviewDrop += new DragEventHandler(MultiSetter_PreviewDrop);
                }
            }
            for (int i = 1; i <= 44; i++)
            {
                Button btn = (FindName("buttonColor" + i) as Button);
                btn.Click += new RoutedEventHandler(GetColor_Click);
                btn.PreviewMouseMove += new MouseEventHandler(btn_PreviewMouseMove); //08.13 用于添加 拖拽改变颜色 事件

            }
        }

        private void MultiSetter_PreviewDrop(object sender, DragEventArgs e)
        {
            ((sender as Label).Content as Button).Background = (SolidColorBrush)e.Data.GetData(typeof(SolidColorBrush));
        }

        //private void ChannelColor_PreviewDrop(object sender, DragEventArgs e)
        //{
        //    (sender as Button).Background = (SolidColorBrush)e.Data.GetData(typeof(SolidColorBrush));
        //}

        //private void YAxisColor_PreviewDrop(object sender, DragEventArgs e)
        //{
        //    (sender as Button).Background = (SolidColorBrush)e.Data.GetData(typeof(SolidColorBrush));
        //}

        private void btn_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(sender as DependencyObject, ((Button)sender).Background, DragDropEffects.Move);
            }
        }


        /// <summary>
        /// 设置颜色
        /// </summary>
        private void SetColor_Click(object sender, RoutedEventArgs e)
        {
            if (ONorOFF)
            {
                (sender as Button).Background = _tempColor;
                ONorOFF = false;
            }
        }

        /// <summary>
        /// 选取颜色
        /// </summary>
        private void GetColor_Click(object sender, RoutedEventArgs e)
        {
            _tempColor = (SolidColorBrush)((sender as Button).Background);
            ONorOFF = true;
        }

        /// <summary>
        /// 确定、取消、应用 3个按钮的事件
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnOK":
                    try
                    {
                        foreach (RealtimeCurvesSetting monSet in _monitorSetters)
                        {
                            monSet.SaveFile();
                            monSet.MappingMonitor();
                        }
                        foreach (RealtimeCurves monitor in _monitors)
                        {
                            monitor.CreateAppendControl(monitor.Curves);
                        }
                        this.Close();
                    }
                    catch
                    {

                    }
                    break;
                case "btnCancel":
                    this.Close();
                    break;
                case "btnApply":
                    foreach (RealtimeCurvesSetting monSet in _monitorSetters)
                    {
                        monSet.SaveFile();
                        monSet.MappingMonitor();
                    }
                    foreach (RealtimeCurves monitor in _monitors)
                    {
                        monitor.CreateAppendControl(monitor.Curves);
                    }
                    break;
                default:
                    MessageBox.Show("这个按钮居然没写代码...");
                    break;
            }
        }
    }
}
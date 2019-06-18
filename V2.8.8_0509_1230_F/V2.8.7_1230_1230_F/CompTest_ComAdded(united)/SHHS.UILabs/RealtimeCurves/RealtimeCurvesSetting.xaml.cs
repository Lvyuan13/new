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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace SHHS.UILabs.RealtimeCurves
{
    /// <summary>
    /// RealtimeCurvesSetting.xaml 的交互逻辑
    /// </summary>
    public partial class RealtimeCurvesSetting : UserControl
    {
        private RealtimeCurves _monitor;   //RealtimeCurvesSetting对应的RealtimeCurves
        private Dictionary<int,YAxisData> _yAxisDatas = new Dictionary<int,YAxisData>();   //记录所有Y轴 
        private Dictionary<int, ChannelData> _channelDatas = new Dictionary<int, ChannelData>(); //记录所有通道控件
        private List<string> _fileDatas = new List<string>();   //用于存储 读取的文件数据
        private double _yAxisSpace = 25;    //每个轴所占的高度
        private int _channelNumber = 20;    //通道的数量

        public RealtimeCurvesSetting(RealtimeCurves monitor)
        {
            InitializeComponent();
            this._monitor = monitor;
            this.Resources = _monitor.Resources;
            //读取当前程序运行目录
            string assemPath = Assembly.GetExecutingAssembly().Location;
            string currentDic = assemPath.Substring(0, assemPath.LastIndexOf("\\") + 1);
            //动态生成Y轴的控件


            //对单位数据集合进行排序
            var q = from u in _monitor.ChannelUnits
                    orderby u.Key
                    select u;
            Dictionary<string, string> tempDictionary = q.ToDictionary(pair => pair.Key, pair => pair.Value);

            for (int i = 0; i < tempDictionary.Count; i++)
            {
                CheckBox cbox = new CheckBox();
                TextBox tboxMax = new TextBox();
                TextBox tboxMin = new TextBox();
                Label lbl = new Label();
                Button btn = new Button();

                cbox.Width = 150;
                cbox.Height = 15;
                cbox.VerticalAlignment = VerticalAlignment.Top;
                cbox.HorizontalAlignment = HorizontalAlignment.Left;
                cbox.Content = tempDictionary.Values.ElementAt(i) + " [ " + tempDictionary.Keys.ElementAt(i) + " ]";
                cbox.Margin = new Thickness(8, 44 + (_yAxisSpace * i), 0, 0);

                tboxMax.Width = 48;
                tboxMax.Height = 19;
                tboxMax.VerticalAlignment = VerticalAlignment.Top;
                tboxMax.HorizontalAlignment = HorizontalAlignment.Left;
                tboxMax.Text = "100";
                tboxMax.Margin = new Thickness(150, 44 + (_yAxisSpace * i), 0, 0);

                tboxMin.Width = 48;
                tboxMin.Height = 19;
                tboxMin.VerticalAlignment = VerticalAlignment.Top;
                tboxMin.HorizontalAlignment = HorizontalAlignment.Left;
                tboxMin.Text = "0";
                tboxMin.Margin = new Thickness(205, 44 + (_yAxisSpace * i), 0, 0);

                //<Label Grid.Row="15" Grid.Column="3" Name="label30" 
                //Padding="1" VerticalContentAlignment="Center" HorizontalContentAlignment="Center" 
                //Background="White" Height="16" Width="35" 
                //BorderBrush="{Binding Path=Background,ElementName=ChannelColor15}" BorderThickness="1">
                //    <Button Height="8" Background="Black" Name="ChannelColor15" Width="25"/>
                //</Label>

                lbl.Width = 37;
                lbl.Height = 19;
                lbl.VerticalAlignment = VerticalAlignment.Top;
                lbl.HorizontalAlignment = HorizontalAlignment.Left;
                lbl.Padding = new Thickness(1);
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                Binding bin = new Binding();
                bin.Source = btn;
                bin.Path = new PropertyPath("Background");
                lbl.SetBinding(Label.BorderBrushProperty, bin);
                lbl.BorderThickness = new Thickness(1);
                lbl.Background = Brushes.White;
                lbl.Content = btn;
                lbl.Margin = new Thickness(255, 44 + (_yAxisSpace * i), 0, 0);

                btn.Width = 25;
                btn.Height = 9;
                btn.Background = Brushes.Black;

                _yAxisDatas.Add(i, new YAxisData(cbox, tboxMax, tboxMin, btn, tempDictionary.Keys.ElementAt(i)));
                GridAxis.Children.Add(cbox);
                GridAxis.Children.Add(tboxMax);
                GridAxis.Children.Add(tboxMin);
                GridAxis.Children.Add(lbl);
            }

            //收集通道相关控件放在_channelDatas中
            for (int i = 1; i <= _channelNumber; i++)
            {
                _channelDatas.Add(i, new ChannelData(FindName("ItemComboBox" + i) as ComboBox, FindName("ChannelComBox" + i) as ComboBox, FindName("ChannelColor" + i) as Button));
                _channelDatas[i].ItemCBox.ItemsSource = _monitor.ChannelMapping.Keys;
                _channelDatas[i].ItemCBox.SelectedIndex = _monitor.ChannelMapping.Keys.Count - 1;
                _channelDatas[i].ChannelCBox.Visibility = Visibility.Hidden;
                _channelDatas[i].ItemCBox.SelectionChanged += new SelectionChangedEventHandler(ItemCBox_SelectionChanged);
            }
            //读取设置页面的数据
            if (File.Exists(currentDic + "monitors/" + this._monitor.Name + "_" + this._monitor.TestType))
            {
                //StreamReader readFile = File.OpenText(currentDic + "monitors/" + this._monitor.Name);
                StreamReader readFile = File.OpenText(currentDic + "monitors/" + this._monitor.Name + "_" + this._monitor.TestType);
                string str = readFile.ReadLine();
                while (str != null)
                {
                    _fileDatas.Add(str);
                    str = readFile.ReadLine();
                }
                readFile.Close();
                //读取文件数据后,对MonitorSetter进行初始化

                //初始化时间轴--最大、最小时间
                MinTimeTextBox.Text = _fileDatas[0];
                MaxTimeTextBox.Text = _fileDatas[1];
                //初始化数据轴(YAxis)
                for (int i = 0; i < _yAxisDatas.Count; i++)
                {
                    _yAxisDatas[i].YAxisName.IsChecked = bool.Parse(_fileDatas[2 + 4 * i]);
                    _yAxisDatas[i].YAxisMaximum.Text = _fileDatas[3 + 4 * i];
                    _yAxisDatas[i].YAxisMinimum.Text = _fileDatas[4 + 4 * i];
                    _yAxisDatas[i].YAxisColor.Background = new SolidColorBrush((Color)(ColorConverter.ConvertFromString(_fileDatas[5 + 4 * i])));
                }
                //初始化通道(Channel)
                if (_monitor.Channels != null)
                {
                    for (int i = 1; i <= _channelDatas.Count; i++)
                    {
                        _channelDatas[i].ItemCBox.SelectedIndex = int.Parse(_fileDatas[2 + _monitor.ChannelUnits.Count * 4 + 3 * (i - 1)]);
                        _channelDatas[i].ChannelCBox.SelectedIndex = int.Parse(_fileDatas[3 + _monitor.ChannelUnits.Count * 4 + 3 * (i - 1)]);
                        _channelDatas[i].ChannelColor.Background = new SolidColorBrush((Color)(ColorConverter.ConvertFromString(_fileDatas[4 + _monitor.ChannelUnits.Count * 4 + 3 * (i - 1)])));
                    }
                }
            }
        }

        /// <summary>
        /// MonitorSetter加载事件
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// 通道分类的Select
        /// </summary>
        private void ItemCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbox = sender as ComboBox;
            int _index = int.Parse(cbox.Name.Substring(12));
            if (cbox.SelectedIndex != _monitor.ChannelMapping.Count - 1)
            {
                _channelDatas[_index].ChannelCBox.Visibility = Visibility.Visible;
                _channelDatas[_index].ChannelCBox.ItemsSource = _monitor.ChannelMapping.Values.ElementAt(cbox.SelectedIndex);
                _channelDatas[_index].ChannelCBox.SelectedIndex = 0;
            }
            else
            {
                _channelDatas[_index].ChannelCBox.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 保存设置数据到文件
        /// </summary>
        public void SaveFile()
        {
            if (_monitor.Channels != null)
            {
                string assemPath = Assembly.GetExecutingAssembly().Location;
                string currentDic = assemPath.Substring(0, assemPath.LastIndexOf("\\") + 1);
                if (!Directory.Exists(currentDic + "monitors/"))
                {
                    System.IO.Directory.CreateDirectory(currentDic + "monitors/");
                }
                //StreamWriter writerFile = File.CreateText(currentDic + "monitors/" + this._monitor.Name);
                StreamWriter writerFile = File.CreateText(currentDic + "monitors/" + this._monitor.Name + "_" + this._monitor.TestType);
                writerFile.WriteLine(MinTimeTextBox.Text);
                writerFile.WriteLine(MaxTimeTextBox.Text);
                foreach (YAxisData yad in _yAxisDatas.Values)
                {
                    writerFile.WriteLine(yad.YAxisName.IsChecked.Value);
                    writerFile.WriteLine(yad.YAxisMaximum.Text);
                    writerFile.WriteLine(yad.YAxisMinimum.Text);
                    writerFile.WriteLine(yad.YAxisColor.Background.ToString());
                }
                foreach (ChannelData cd in _channelDatas.Values)
                {
                    writerFile.WriteLine(cd.ItemCBox.SelectedIndex);
                    writerFile.WriteLine(cd.ChannelCBox.SelectedIndex);
                    writerFile.WriteLine(cd.ChannelColor.Background.ToString());
                }
                writerFile.Close();
            }
        }

        /// <summary>
        /// 映射Monitor
        /// </summary>
        public void MappingMonitor()
        {
            _monitor.ComeBackDatas.Clear();
            _monitor.ComeBackDatas.Add(double.Parse(MinTimeTextBox.Text));
            _monitor.ComeBackDatas.Add(double.Parse(MaxTimeTextBox.Text));
            
            //Y轴
            foreach (Axis axis in _monitor.YAxises)
            {
                axis.IsVisibility = false;
            }
            _monitor.YAxises.Clear();
            int _yaxisCount = 0;
            foreach (YAxisData yad in _yAxisDatas.Values)
            {
                if (yad.YAxisName.IsChecked.Value)
                {
                    Axis axis = new Axis(12,
                                        double.Parse(yad.YAxisMaximum.Text),
                                        double.Parse(yad.YAxisMinimum.Text),
                                        _monitor.ChannelUnits[yad.Unit] + " [ " + yad.Unit + " ]",
                                        new Point(50 * _yaxisCount, 13),
                                        new Point(50 * _yaxisCount + 50, _monitor.GridBase.ActualHeight - 31),
                                        AxisMode.Vertical,
                                        (SolidColorBrush)yad.YAxisColor.Background,
                                        _monitor.GridBase);
                    _monitor.ComeBackDatas.Add(axis.MinValue);
                    _monitor.ComeBackDatas.Add(axis.MaxValue);
                    _monitor.YAxises.Add(axis);
                    axis.IsVisibility = true;
                    _yaxisCount += 1;
                }
            }
            if (_yaxisCount > 0)
            {
                _monitor.GridCurveLeft = 50 * _yaxisCount;
            }
            else
            {
                _monitor.GridCurveLeft = 10;
            }
            //X轴
            _monitor.SliderMapping = false;
            _monitor.sliderMinTime.Value = double.Parse(MinTimeTextBox.Text) * 60;
            _monitor.sliderMaxTime.Value = double.Parse(MaxTimeTextBox.Text) * 60;
            _monitor.SliderMapping = true;
            //曲线
            foreach (Curve curve in _monitor.Curves)
            {
                curve.IsVisible = false;
            }
            _monitor.Curves.Clear();
            bool _istrue = false;   //用于判断曲线可有对应的Y轴单位 如果有 则为true
            foreach (ChannelData cd in _channelDatas.Values)
            {
                if (cd.ChannelCBox.Visibility == Visibility.Visible)
                {
                    _istrue = false;
                    foreach(int _key in _monitor.Channels.Keys)
                    {
                        if (cd.ChannelCBox.Text.Equals(_monitor.Channels[_key].Name))
                        {
                            foreach (YAxisData yaxisData in _yAxisDatas.Values)
                            {
                                if (_monitor.Channels[_key].Unit.Equals(yaxisData.Unit))
                                {
                                    _istrue = true;
                                    Curve cur = new Curve();
                                    cur.Name = _monitor.Channels[_key].Name;
                                    cur.DataUnits = _monitor.Channels[_key].Unit;
                                    cur.SourceData = _monitor.Channels[_key].DataList;
                                    cur.OriginalColor = (SolidColorBrush)cd.ChannelColor.Background;
                                    cur.MaxTime = _monitor.XAxis.MaxValue;
                                    cur.MinTime = _monitor.XAxis.MinValue;
                                    cur.MaxData = double.Parse(yaxisData.YAxisMaximum.Text);
                                    cur.MinData = double.Parse(yaxisData.YAxisMinimum.Text);
                                    cur.ChannelIndex = _key;
                                    cur.Grid = _monitor.GridCurve;
                                    cur.IsVisible = true;
                                    cur.Build();
                                    _monitor.Curves.Add(cur);
                                    break;
                                }
                            }
                            if (!_istrue)
                            { 
                                Curve cur = new Curve();
                                cur.Name = _monitor.Channels[_key].Name;
                                cur.DataUnits = _monitor.Channels[_key].Unit;
                                cur.SourceData = _monitor.Channels[_key].DataList;
                                cur.OriginalColor = (SolidColorBrush)cd.ChannelColor.Background;
                                cur.MaxTime = _monitor.XAxis.MaxValue;
                                cur.MinTime = _monitor.XAxis.MinValue;
                                cur.MaxData = _monitor.Channels[_key].Maximum;
                                cur.MinData = _monitor.Channels[_key].Minimum;
                                cur.ChannelIndex = _key;
                                cur.Grid = _monitor.GridCurve;
                                cur.IsVisible = true;
                                cur.Build();
                                _monitor.Curves.Add(cur);
                            }
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 记录所有Y轴控件
        /// </summary>
        public Dictionary<int, YAxisData> YAxisDatas
        {
            get { return _yAxisDatas; }
        }

        /// <summary>
        /// 记录所有通道控件
        /// </summary>
        public Dictionary<int, ChannelData> ChannelDatas
        {
            get { return _channelDatas; }
        }
    }
}
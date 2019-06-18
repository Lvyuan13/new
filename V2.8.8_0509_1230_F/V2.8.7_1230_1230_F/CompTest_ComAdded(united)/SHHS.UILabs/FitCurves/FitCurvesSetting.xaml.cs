using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System;

namespace SHHS.UILabs.FitCurves
{
    /// <summary>
    /// FitCarvesSetting.xaml 的交互逻辑
    /// </summary>
    public partial class FitCurvesSetting : Window
    {
        public FitCurves View { get; set; }
        public FitCurvesSetting(FitCurves view)
        {
            InitializeComponent();
            this.Load(view);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Label tbk = sender as Label;
                System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
                //colorDialog.FullOpen = true;
                SolidColorBrush scb = (SolidColorBrush)tbk.Background;

                colorDialog.Color = System.Drawing.Color.FromArgb(scb.Color.A, scb.Color.R, scb.Color.G, scb.Color.B);
                System.Windows.Forms.DialogResult dr = colorDialog.ShowDialog();

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    scb = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A,
                                                                                colorDialog.Color.R,
                                                                                colorDialog.Color.G,
                                                                                colorDialog.Color.B));
                    tbk.Background = scb;
                }
            }
        }

        private void cbBoxAxis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbk = sender as ComboBox;
            switch (cbk.Name)
            {
                case "cbBoxAxis1":
                    tbkUnit1.Text = this.View.Channels[cbk.SelectedIndex].Unit;
                    break;
                case "cbBoxAxis2":
                    tbkUnit2.Text = this.View.Channels[cbk.SelectedIndex].Unit;
                    break;
                case "cbBoxAxis3":
                    tbkUnit3.Text = this.View.Channels[cbk.SelectedIndex].Unit;
                    break;
                case "cbBoxAxis4":
                    tbkUnit4.Text = this.View.Channels[cbk.SelectedIndex].Unit;
                    break;
                case "cbBoxAxis5":
                    tbkUnit5.Text = this.View.Channels[cbk.SelectedIndex].Unit;
                    break;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            #region 验证数据
            double temp = 0;
            if (!ValidValue(txtAxis_1_Min, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_1_Max, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_2_Min, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_2_Max, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_3_Min, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_3_Max, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_4_Min, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_4_Max, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_5_Min, temp))
            {
                return;
            }
            if (!ValidValue(txtAxis_5_Max, temp))
            {
                return;
            }
            #endregion

            foreach (var v in gridBase.Children)
            {
                if (v.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)v).GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
                }
                else if (v.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)v).GetBindingExpression(ComboBox.SelectedIndexProperty).UpdateSource();
                }
                else if (v.GetType() == typeof(TextBox))
                {
                    ((TextBox)v).GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
                else if (v.GetType() == typeof(Label))
                {
                    ((Label)v).GetBindingExpression(Label.BackgroundProperty).UpdateSource();
                }
                else if (v.GetType() == typeof(ScrollBar))
                {
                    ((ScrollBar)v).GetBindingExpression(ScrollBar.ValueProperty).UpdateSource();
                }
            }
            try
            {
                foreach (var v in this.View.AxisArray)
                {
                    v.Unit = this.View.Channels[v.ChannelNo].Unit;
                    v.Name = this.View.Channels[v.ChannelNo].Name + "[" + v.Unit + "]";
                }
            }
            catch
            { }
            //this.DialogResult = true;
            this.View.Refresh();
            //this.SaveConfig();

            this.Close();
        }

        private bool ValidValue(TextBox txt, double temp)
        {
            if (double.TryParse(txt.Text, out temp))
            {
                txt.Background = Brushes.White;
                return true;
            }
            else
            {
                MessageBox.Show("错误:\n    位置: \"" + txt.Text + "\"\n    信息: 请输入正确的数值格式!", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt.Background = Brushes.Cyan;
                Keyboard.Focus(txt);
                return false;
            }
        }

        void Load(FitCurves view)
        {
            this.Resources = view.Resources;
            this.View = view;
            //this.Background = view.Background;

            if (this.View.Channels != null && this.View.Channels.Count >= 1)
            {
                var v = from c in View.Channels.Values
                        select c.Name;
                this.cbBoxAxis1.ItemsSource = v;
                this.cbBoxAxis2.ItemsSource = v;
                this.cbBoxAxis3.ItemsSource = v;
                this.cbBoxAxis4.ItemsSource = v;
                this.cbBoxAxis5.ItemsSource = v;
            }

            gridBase.DataContext = this.View.AxisArray;
        }

        void SaveConfig()
        {
            StreamWriter writeDatas = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "fit.ax");
            foreach (var v in this.View.AxisArray)
            {
                writeDatas.WriteLine(v.IsActive.ToString());
                writeDatas.WriteLine(v.ChannelNo.ToString());
                writeDatas.WriteLine(v.Name.ToString());
                writeDatas.WriteLine(v.Maximum.ToString());
                writeDatas.WriteLine(v.Minimum.ToString());
                writeDatas.WriteLine((v.Scales.Length-1).ToString());
                writeDatas.WriteLine(v.Foreground.ToString());
                writeDatas.WriteLine(v.Unit.ToString());
                writeDatas.WriteLine(v.LinestNumber.ToString());
            }
            writeDatas.Close();
        }

        //private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    switch ((sender as ScrollBar).Name)
        //    {
        //        case "scrollBar_1":
        //            txtAxis_1_Linest.Text = scrollBar_1.Value.ToString();
        //            break;
        //        case "scrollBar_2":
        //            break;
        //        case "scrollBar_3":
        //            break;
        //        case "scrollBar_4":
        //            break;
        //        default:
        //            break;
        //    }
        //    //path1_Optimize.Text = (sender as ScrollBar).Value.ToString();
        //}
    }
}

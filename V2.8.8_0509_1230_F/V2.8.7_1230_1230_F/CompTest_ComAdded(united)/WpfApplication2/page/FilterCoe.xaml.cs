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

namespace WpfApplication2
{
    /// <summary>
    /// FilterCoe.xaml 的交互逻辑
    /// </summary>
    public partial class FilterCoe : Window
    {
        public FilterCoe()
        {
            InitializeComponent();
        }

        string[] AllDataFromDispaly_TEMP;

        private void list0_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb0.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    tb0.Focus();
                }
                else
                {
                    BackPanel.Agilent.AgilentList[0].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                tb0.Focus();
            }

        }

        private void list1_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb1.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[1].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list2_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb2.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[2].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list3_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb3.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[3].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list4_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb4.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[4].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list5_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb5.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[5].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list6_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb6.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[6].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list7_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb7.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[7].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list8_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb8.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[8].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list9_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb9.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[9].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list10_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb10.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[10].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list11_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb11.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[11].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list12_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb12.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[12].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list13_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb13.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[13].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list14_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb14.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[14].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list15_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb15.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[15].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list16_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb16.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[16].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list17_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb17.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[17].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list18_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb18.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[18].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list19_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb19.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[19].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list20_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb20.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[20].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list21_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb21.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[21].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void list22_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tb22.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BackPanel.Agilent.AgilentList[22].FilterCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btA_RefFlowrate_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tbA_RefFlowrate.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    GlobelVar.A_RefFlowRateCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btA_CoolingCapacity_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tbA_CoolingCapacity.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    GlobelVar.A_CoolingCapacityCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btG_RefFlowrate_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tbG_RefFlowrate.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    GlobelVar.G_RefFlowRateCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);
                }

            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btG_CoolingCapacity_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tbG_CoolingCapacity.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    GlobelVar.G_CoolingCapacityCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);

                }
            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private string[] GetAllFromDisplay()
        {
            string[] AllFormTextbox = new string[30];
            AllFormTextbox[0] = tb0.Text;
            AllFormTextbox[1] = tb1.Text;
            AllFormTextbox[2] = tb2.Text;
            AllFormTextbox[3] = tb3.Text;
            AllFormTextbox[4] = tb4.Text;
            AllFormTextbox[5] = tb5.Text;
            AllFormTextbox[6] = tb6.Text;
            AllFormTextbox[7] = tb7.Text;
            AllFormTextbox[8] = tb8.Text;
            AllFormTextbox[9] = tb9.Text;
            AllFormTextbox[10] = tb10.Text;
            AllFormTextbox[11] = tb11.Text;
            AllFormTextbox[12] = tb12.Text;
            AllFormTextbox[13] = tb13.Text;
            AllFormTextbox[14] = tb14.Text;
            AllFormTextbox[15] = tb15.Text;
            AllFormTextbox[16] = tb16.Text;
            AllFormTextbox[17] = tb17.Text;
            AllFormTextbox[18] = tb18.Text;
            AllFormTextbox[19] = tb19.Text;
            AllFormTextbox[20] = tb20.Text;
            AllFormTextbox[21] = tb21.Text;
            AllFormTextbox[22] = tb22.Text;
            AllFormTextbox[23] = tbA_RefFlowrate.Text;
            AllFormTextbox[24] = tbA_CoolingCapacity.Text;
            AllFormTextbox[25] = tbG_RefFlowrate.Text;
            AllFormTextbox[26] = tbG_CoolingCapacity.Text;

            AllFormTextbox[27] = tbChilllerRefFlowRate.Text;
            AllFormTextbox[28] = tbChilllerCoolingCapacity.Text;
            AllFormTextbox[29] = tbChillerPower.Text;
            return AllFormTextbox;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AllDataFromDispaly_TEMP = BackPanel.DBOperate.ReadAllFromFilterCoeTable(GlobelVar.PathDebug + "CarChiller.mdb", "FilterCoe");
            tb0.Text = AllDataFromDispaly_TEMP[0];
            tb1.Text = AllDataFromDispaly_TEMP[1];
            tb2.Text = AllDataFromDispaly_TEMP[2];
            tb3.Text = AllDataFromDispaly_TEMP[3];
            tb4.Text = AllDataFromDispaly_TEMP[4];
            tb5.Text = AllDataFromDispaly_TEMP[5];
            tb6.Text = AllDataFromDispaly_TEMP[6];
            tb7.Text = AllDataFromDispaly_TEMP[7];
            tb8.Text = AllDataFromDispaly_TEMP[8];
            tb9.Text = AllDataFromDispaly_TEMP[9];
            tb10.Text = AllDataFromDispaly_TEMP[10];
            tb11.Text = AllDataFromDispaly_TEMP[11];
            tb12.Text = AllDataFromDispaly_TEMP[12];
            tb13.Text = AllDataFromDispaly_TEMP[13];
            tb14.Text = AllDataFromDispaly_TEMP[14];
            tb15.Text = AllDataFromDispaly_TEMP[15];
            tb16.Text = AllDataFromDispaly_TEMP[16];
            tb17.Text = AllDataFromDispaly_TEMP[17];
            tb18.Text = AllDataFromDispaly_TEMP[18];
            tb19.Text = AllDataFromDispaly_TEMP[19];
            tb20.Text = AllDataFromDispaly_TEMP[20];
            tb21.Text = AllDataFromDispaly_TEMP[21];
            tb22.Text = AllDataFromDispaly_TEMP[22];
            //A .Text = AllDataFromDispaly_TEMP[23];
            tbA_RefFlowrate.Text = AllDataFromDispaly_TEMP[23];
            tbA_CoolingCapacity.Text = AllDataFromDispaly_TEMP[24];
            tbG_RefFlowrate.Text = AllDataFromDispaly_TEMP[25];
            tbG_CoolingCapacity.Text = AllDataFromDispaly_TEMP[26];

            tbChilllerRefFlowRate.Text = AllDataFromDispaly_TEMP[27];
            tbChilllerCoolingCapacity.Text = AllDataFromDispaly_TEMP[28];
            tbChillerPower.Text = AllDataFromDispaly_TEMP[29];
        }

        private void btChilllerRefFlowRate_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tbChilllerRefFlowRate.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    GlobelVar.Chiller_RefFlowRateCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);

                }
            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btChilllerCoolingCapacity_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tbChilllerCoolingCapacity.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    GlobelVar.Chiller_CoolingCapacityCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);

                }
            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btChillerPower_Click(object sender, RoutedEventArgs e)
        {
            double temp = 0;

            if (double.TryParse(tbChillerPower.Text, out temp))
            {
                //App.Cdts[App.ProjectCode].HotWaterTemp = temp;
                //if (!App.IsDemo)
                //    ControlStrategy.PID.Set(1, "SV", temp, 2);
                if (temp > 1 || temp < 0)
                {
                    MessageBox.Show("请输入0到1之间的数！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    GlobelVar.Chiller_PowerCoe = Convert.ToDouble(temp);
                    AllDataFromDispaly_TEMP = GetAllFromDisplay();
                    BackPanel.DBOperate.FilterCoeUpdateToDateBase_ForCar(BackPanel.InformationGlo.PathDebugOfBuildDB_Inbackpanel + "CarChiller.mdb", "FilterCoe", AllDataFromDispaly_TEMP);

                }
            }
            else
            {
                MessageBox.Show("当前数值输入错误，请输入正确的数值类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

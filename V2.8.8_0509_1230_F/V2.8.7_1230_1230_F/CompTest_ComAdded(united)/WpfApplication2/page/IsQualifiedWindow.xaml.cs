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
    /// IsQualifiedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IsQualifiedWindow : Window
    {
        public IsQualifiedWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Waitting_Checked(object sender, RoutedEventArgs e)
        {
            Report.ReportParameterMySelf.RP25TestResult = "待定";
            Report.ReportParameterMySelf_ForChiller.RP23TestResult = "待定";
        }

        private void Quilified_Checked(object sender, RoutedEventArgs e)
        {
            Report.ReportParameterMySelf.RP25TestResult = "合格";
            Report.ReportParameterMySelf_ForChiller.RP23TestResult = "合格";
        }

        private void NoQuilified_Checked(object sender, RoutedEventArgs e)
        {
            Report.ReportParameterMySelf.RP25TestResult = "不合格";
            Report.ReportParameterMySelf_ForChiller.RP23TestResult = "不合格";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

}

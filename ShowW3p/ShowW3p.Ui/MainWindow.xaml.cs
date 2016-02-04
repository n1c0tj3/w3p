using ShowW3p.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShowW3p.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtProces.Text = "w3wp.exe";
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            ProcessRepository rep = new ProcessRepository();
            var proceslist = String.IsNullOrEmpty(txtMachine.Text)? rep.FindProcess(txtProces.Text) : rep.FindProcess(txtProces.Text, txtMachine.Text);
            var result = (String.IsNullOrEmpty(txtNeedle.Text) ? proceslist : proceslist.Where(x => x.Name.ToLower().Contains(txtNeedle.Text.ToLower())));
            gridResult.ItemsSource = result;
            tabMenu.SelectedIndex = 1;
            string needle = String.IsNullOrEmpty(txtNeedle.Text) ? "" : String.Format(" filtered by '{0}'", txtNeedle.Text);
            tabitemResult.Header = String.Format("{0} process found on {1} {2}", result.Count(), String.IsNullOrEmpty(txtMachine.Text)? "Your local machine" : txtMachine.Text, needle);
        }
    }
}

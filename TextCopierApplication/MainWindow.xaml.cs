using Microsoft.Win32;
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

namespace TextCopierApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FromButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                FileName = "Text file",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                FromTextBox.Text = dialog.FileName;
                StartButton.IsEnabled = true;
            }
        }
        private void ToButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                FileName = "Text file",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                ToTextBox.Text = dialog.FileName;
                StartButton.IsEnabled = true;
            }
        }
    }
}

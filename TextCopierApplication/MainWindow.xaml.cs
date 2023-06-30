using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace TextCopierApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread thread;
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
                if (FromTextBox.Text != string.Empty && ToTextBox.Text != string.Empty)
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
                if (FromTextBox.Text != string.Empty && ToTextBox.Text != string.Empty)
                    StartButton.IsEnabled = true;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(FromTextBox.Text))
                {
                    SuspendButton.IsEnabled = true;
                    string text = File.ReadAllText(FromTextBox.Text);
                    ProgressBar.Maximum = text.Length;
                    thread = new Thread(
                        () =>
                        {
                            StringBuilder writeText = new StringBuilder();
                            foreach (var c in text)
                            {
                                writeText.Append(c);
                                Dispatcher.Invoke(() =>
                                {
                                    File.WriteAllText(ToTextBox.Text, writeText.ToString());
                                    ProgressBar.Value++;
                                });
                                Thread.Sleep(20);
                            }

                            MessageBox.Show("File copied succesfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            Dispatcher.Invoke(() =>
                            {
                                ProgressBar.Value = 0;
                                FromTextBox.Clear();
                                ToTextBox.Clear();
                                StartButton.IsEnabled = false;
                            });
                        });
                    thread.Start();
                    StartButton.IsEnabled = false;
                }
                else
                {
                    throw new NotImplementedException("File path not exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                FromTextBox.Clear();
                ToTextBox.Clear();
            }
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            thread.Resume();
            SuspendButton.IsEnabled = true;
            ResumeButton.IsEnabled = false;
        }

        private void SuspendButton_Click(object sender, RoutedEventArgs e)
        {
            thread.Suspend();
            ResumeButton.IsEnabled = true;
            SuspendButton.IsEnabled = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FromTextBox.Text != string.Empty && ToTextBox.Text != string.Empty)
                StartButton.IsEnabled = true;
        }
    }
}

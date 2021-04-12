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


using Microsoft.Win32;

using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace PasswordManage
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButonNewFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            Nullable<bool> result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("");
            }

            

            

        }

        private void ButonOpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            Nullable<bool> result = openFileDialog.ShowDialog();

            if(result == false)
                return;


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("LOL");
        }
    }
}

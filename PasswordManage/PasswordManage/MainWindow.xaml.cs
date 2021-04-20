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

        private FilePassword filePassword;
        private string PathFile;
        private bool fielSelected = false;

        public MainWindow()
        {
            InitializeComponent();

            UpdateWindow();

        }

        private void UpdateWindow()
        {
            foreach (UIElement item in Grid.Children)
            {

                if(item is Menu)
                {
                    continue;
                }

                if (filePassword != null)
                    item.IsEnabled = true;
                else
                    item.IsEnabled = false;

            }
        }

        private void NewFile(object sender, RoutedEventArgs e)
        {
            var newW = new FileСreation();

            Nullable<bool> fileСreated = newW.ShowDialog();

            if(fileСreated != false)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(FilePassword));

                using (FileStream fs = new FileStream(newW.PathFile, FileMode.OpenOrCreate))
                {
                    filePassword = (FilePassword)formatter.Deserialize(fs);
                }
            }

            UpdateWindow();


        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".xml";
           
            openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            Nullable<bool> result = openFileDialog.ShowDialog();

            if(result != true)
                return;

            var newW = new OpenFile();
            string temp = openFileDialog.FileName;
            newW.PathFile = openFileDialog.FileName;


            Nullable<bool> fileOpen = newW.ShowDialog();

            if (fileOpen != false)
            {
                filePassword = newW.filePassword;
                
            }

            UpdateWindow();

        }
    }
}

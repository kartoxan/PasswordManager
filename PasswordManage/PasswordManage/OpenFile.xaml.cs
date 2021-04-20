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
using System.Windows.Shapes;

using Microsoft.Win32;

using System.IO;
using System.Xml.Serialization;

namespace PasswordManage
{
    /// <summary>
    /// Логика взаимодействия для OpenFile.xaml
    /// </summary>
    public partial class OpenFile : Window
    {

        public bool fileOpen = false;
        public string password;
        public string PathFile;

        public FilePassword filePassword;

        public OpenFile()
        {
            InitializeComponent();
         
        }
       
        private void Confirm(object sender, RoutedEventArgs e)
        {

            password = passwordBox.Password;

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(FilePassword));

                using (FileStream fs = new FileStream(PathFile, FileMode.OpenOrCreate))
                {
                    filePassword = (FilePassword)formatter.Deserialize(fs);
                }
            }
            catch
            {
                MessageBox.Show("Неудалось открить фаил");
            }

            DialogResult = true;

            this.Close();

        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            label2.Content = PathFile;
        }
    }
}

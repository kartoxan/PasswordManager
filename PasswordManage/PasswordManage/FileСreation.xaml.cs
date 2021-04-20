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
    /// Логика взаимодействия для FileСreation.xaml
    /// </summary>
    public partial class FileСreation : Window
    {
        public string PathFile;
        public string password;
        public FileСreation()
        {
            InitializeComponent();

            PathFile = "PasswordFile.xml";
            label2.Content = PathFile;

        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".xml";

            saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;

            saveFileDialog.FileName = PathFile;

            Nullable<bool> result = saveFileDialog.ShowDialog();

            if (result == false)
                return;
            
            PathFile = saveFileDialog.FileName;


            label2.Content = PathFile;

        }

        private void CreateFile(object sender, RoutedEventArgs e)
        {

            if(passwordBox.Password != passwordBox_Copy.Password)
            {
                MessageBox.Show("Пароли не совпвдают");
                return;
            }

            password = passwordBox.Password;
            

            FilePassword file = new FilePassword();
            XmlSerializer formatter = new XmlSerializer(typeof(FilePassword));

            using (FileStream fs = new FileStream(PathFile, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, file);
            }

            this.DialogResult = true;

            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

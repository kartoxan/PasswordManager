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
using System.Security.Cryptography;

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


            setPathFile();

        }

        private void setPathFile()
        {
            if (PathFile.Length > 75)
            {

                string fileName = PathFile.Split(@"\"[0]).Last<string>();
                string temp = PathFile.Substring(0, 3) + "..." + @"\" + fileName;
                label2.Content = temp;
            }
            else
            {
                label2.Content = PathFile;
            }


        }

        private void CreateFile(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password.Length < 16)
            {
                MessageBox.Show("Пароль повинен містити мінімум 16 символів");
                return;
            }

            if(passwordBox.Password != passwordBox_Copy.Password)
            {
                MessageBox.Show("Паролі не співпадають");
                return;
            }

            

            password = passwordBox.Password;
            

            PasswordContainer file = new PasswordContainer();
            XmlSerializer formatter = new XmlSerializer(typeof(PasswordContainer));





            using (FileStream fileStream = new FileStream(PathFile, FileMode.Create))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] key = Encoding.UTF8.GetBytes(password);

                    aes.Key = key;

                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);


                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter encryptWriter = new StreamWriter(cryptoStream))
                        {
                            formatter.Serialize(encryptWriter, file);

                        }
                    }

                }

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

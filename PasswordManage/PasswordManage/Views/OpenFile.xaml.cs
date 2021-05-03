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
    /// Логика взаимодействия для OpenFile.xaml
    /// </summary>
    public partial class OpenFile : Window
    {

        public bool fileOpen = false;
        public string password;
        public string PathFile;

        public PasswordContainer filePassword;

        public OpenFile()
        {
            InitializeComponent();
         
        }
       
        private void Confirm(object sender, RoutedEventArgs e)
        {

            password = passwordBox.Password.Trim();

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(PasswordContainer));

                using (FileStream fileStream = new FileStream(PathFile, FileMode.Open))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] iv = new byte[aes.IV.Length];
                        int numBytesToRead = aes.IV.Length;
                        int numBytesRead = 0;
                        while (numBytesToRead > 0)
                        {
                            int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                            if (n == 0) break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }

                        byte[] key = Encoding.UTF8.GetBytes(password);

                        using (CryptoStream cryptoStream = new CryptoStream(
                           fileStream,
                           aes.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                        {
                            using (StreamReader decryptReader = new StreamReader(cryptoStream))
                            {
                                filePassword = (PasswordContainer)formatter.Deserialize(decryptReader);
                            }
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("Пароль не вірний");
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

    }
}

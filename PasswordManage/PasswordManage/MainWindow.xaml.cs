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


using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;

namespace PasswordManage
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FilePassword filePassword;
        string password;
        private string PathFile;

        private BindingList<Password> passwords;

        public MainWindow()
        {
            InitializeComponent();
            
            

            UpdateWindow();

        }

        private void UpdateWindow()
        {
            foreach (UIElement item in Grid.Children)
            {
                textBox_Site.Text = "";
                textBox_Password.Text = "";
                textBox_Login.Text = "";

                if (item is Menu)
                {
                    continue;
                }

                if (filePassword != null)
                {
                    item.IsEnabled = true;

                    
                }          
                else
                {
                    item.IsEnabled = false;
                }                   
            }

            if(filePassword != null)
            {
                passwords = new BindingList<Password>(filePassword.PasswordData.passwords);
                passwordGrid.ItemsSource = passwords;
                menuItem_Save.IsEnabled = true;
            }
            else
            {
                menuItem_Save.IsEnabled = false;
            }

            if(passwordGrid.SelectedItem == null)
            {
                button_DeletePassword.IsEnabled = false;
                button_СhangePassword.IsEnabled = false;
            }
            else
            {
                button_DeletePassword.IsEnabled = true;
                button_СhangePassword.IsEnabled = true;
            }

        }

        private void NewFile(object sender, RoutedEventArgs e)
        {
            var newW = new FileСreation();

            Nullable<bool> fileСreated = newW.ShowDialog();

            if(fileСreated == true)
            {
                PathFile = newW.PathFile;
                password = newW.password;
                XmlSerializer formatter = new XmlSerializer(typeof(FilePassword));
                /*
                using (FileStream fs = new FileStream(PathFile, FileMode.OpenOrCreate))
                {
                    filePassword = (FilePassword)formatter.Deserialize(fs);
                }
                */
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
                                filePassword = (FilePassword)formatter.Deserialize(decryptReader);
                            }
                        }
                    }
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

            

            if (fileOpen == true)
            {
                password = newW.password;
                filePassword = newW.filePassword;
                PathFile = newW.PathFile;
            }

            UpdateWindow();

        }

        private void button_NewPassword_Click(object sender, RoutedEventArgs e)
        {
            var NewPasswordWindow = new NewPassword();

            Nullable<bool> result = NewPasswordWindow.ShowDialog();

            if(result == true)
            {
                filePassword.PasswordData.passwords.Add(NewPasswordWindow.password);
                            
            }

            UpdateWindow();

        }

        private void button_СhangePassword_Click(object sender, RoutedEventArgs e)
        {
            var NewPasswordWindow = new NewPassword();

            NewPasswordWindow.password = (Password)passwordGrid.SelectedItem;

            int i = passwords.IndexOf(NewPasswordWindow.password);

            Nullable<bool> result = NewPasswordWindow.ShowDialog();

            if (result == true)
            {
                
                filePassword.PasswordData.passwords[i] = NewPasswordWindow.password;

            }

            UpdateWindow();
        }

        private void button_DeletePassword_Click(object sender, RoutedEventArgs e)
        {
            Password password = (Password)passwordGrid.SelectedItem;
            //int i = passwords.IndexOf(password);

            filePassword.PasswordData.passwords.Remove(password);

            UpdateWindow();

        }

        private void passwordGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Password password = (Password)passwordGrid.SelectedItem;
            if(password != null)
            {
                textBox_Site.Text = password.Site;
                textBox_Password.Text = password.password;
                textBox_Login.Text = password.login;
            }

            if (passwordGrid.SelectedItem == null)
            {
                button_DeletePassword.IsEnabled = false;
                button_СhangePassword.IsEnabled = false;
            }
            else
            {
                button_DeletePassword.IsEnabled = true;
                button_СhangePassword.IsEnabled = true;
            }

        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
           
            XmlSerializer formatter = new XmlSerializer(typeof(FilePassword));
            /*
            using (FileStream fs = new FileStream(PathFile, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, filePassword);
            }
            */

            using (FileStream fileStream = new FileStream(PathFile, FileMode.OpenOrCreate))
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
                            formatter.Serialize(encryptWriter, filePassword);

                        }
                    }

                }

            }
        }

        private void button3_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, (Object)textBox_Login.Text);
        }
    }
}

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

namespace PasswordManage
{
    /// <summary>
    /// Логика взаимодействия для NewPassword.xaml
    /// </summary>
    public partial class NewPassword : Window
    {
        public Password password;

        public NewPassword()
        {
            InitializeComponent();
        }

        private void button_Сancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {

            if(textBox_Site.Text == "" && textBox_Login.Text == "" && textBox_Password.Text == "")
            {
                MessageBox.Show("Заовніть поля");
                return;
            }

            password = new Password(textBox_Site.Text, textBox_Login.Text, textBox_Password.Text);
            this.DialogResult = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(password != null)
            {
                textBox_Site.Text = password.Site;
                textBox_Login.Text = password.login;
                textBox_Password.Text = password.password;
                StrongPassword(password.password);
            }
        }

        private bool StrongPassword(string password)
        {
            bool strong = true;

            List<char> chars = new List<char>();
            for (char i = '!'; i <= '~'; i++)
            {
                chars.Add(i);
            }

            bool capitalLetter = false;
            for (char i = 'A'; i <= 'Z'; i++)
            {
                if(password.Contains(i))
                {
                    capitalLetter = true;
                }
                chars.Remove(i);
            }

            bool smallLetter = false;
            for (char i = 'a'; i <= 'z'; i++)
            {
                if (password.Contains(i))
                {
                    smallLetter = true;
                }
                chars.Remove(i);
            }

            bool numeral = false;
            for (char i = '0'; i <= '9'; i++)
            {
                if (password.Contains(i))
                {
                    numeral = true;
                }
                chars.Remove(i);
            }

            bool specialSymbols = false;
            foreach(char c in chars)
            {
                if(password.Contains(c))
                {
                    specialSymbols = true;
                }
            }

            bool quantity = false;
            if(password.Length > 10)
            {
                quantity = true;
            }

            checkBox_letter.IsChecked = capitalLetter && smallLetter;
            checkBox_numeral.IsChecked = numeral;
            checkBox_quantity.IsChecked = quantity;
            checkBox_specialSymbols.IsChecked = specialSymbols;

            strong = capitalLetter && smallLetter && numeral && specialSymbols && quantity;

            return strong;

        }

        private void button_GeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            string password = "";

            Random random = new Random();


            do
            {
                password = "";
                for (int i = 0; i < 15; i++)
                {
                    password += (char)random.Next((int)'!', (int)'~');
                }
            } while (!StrongPassword(password));



            textBox_Password.Text = password;

        }

        private void textBox_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textBox_Password.Text.Length > 0)
            {
                string pas = textBox_Password.Text.Trim();
                StrongPassword(pas);
            }
            else
            {
                checkBox_letter.IsChecked = false;
                checkBox_numeral.IsChecked = false;
                checkBox_quantity.IsChecked = false;
                checkBox_specialSymbols.IsChecked = false;
            }

        }
    }
}

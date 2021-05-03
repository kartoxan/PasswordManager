using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManage
{
    class PasswordGenerator
    {

        private List<char> availableСharacters;

        private bool аvailabilityCapitalLetter;
        private bool аvailabilityNumeral;
        private bool аvailabilitySpecialSymbols;



        PasswordGenerator()
        {

            availableСharacters = new List<char>();

            for (char i = '!'; i <= '~'; i++)
            {
                availableСharacters.Add(i);
            }

            аvailabilityCapitalLetter = true;
            аvailabilityNumeral = true;
            аvailabilitySpecialSymbols = true;
        }

        PasswordGenerator(bool аvailabilityCapitalLetter, bool аvailabilityNumeral, bool аvailabilitySpecialSymbols)
        {
            this.аvailabilityCapitalLetter = аvailabilityCapitalLetter;
            this.аvailabilityNumeral = аvailabilityNumeral;
            this.аvailabilitySpecialSymbols = аvailabilitySpecialSymbols;

            for (char i = 'a'; i <= 'z'; i++)
            {
                availableСharacters.Add(i);
            }

            if (аvailabilityCapitalLetter)
            {
                for (char i = 'A'; i <= 'Z'; i++)
                {
                    availableСharacters.Add(i);
                }
            }




            if (аvailabilityNumeral)
            {
                for (char i = '0'; i <= '9'; i++)
                {
                    availableСharacters.Add(i);
                }
            }

            if (аvailabilitySpecialSymbols)
            {
                List<char> chars = new List<char>();
                for (char i = '!'; i <= '~'; i++)
                {
                    chars.Add(i);
                }
                for (char i = 'A'; i <= 'Z'; i++)
                {
                    chars.Remove(i);
                }
                for (char i = 'a'; i <= 'z'; i++)
                {
                    chars.Remove(i);
                }
                for (char i = '0'; i <= '9'; i++)
                {
                    chars.Remove(i);
                }

                foreach (char c in chars)
                {
                    availableСharacters.Add(c);
                }
            }
        }

        public string nextPassword(int length)
        {
            string password = "";

            Random random = new Random();

            do
            {
                password = "";
                for (int i = 0; i < length; i++)
                {
                    password += availableСharacters[random.Next(0, availableСharacters.Count)];
                }
            } while (!verificationPassword(password));

            return password;
        }

        private bool verificationPassword(string pasword)
        {
            bool ver = smallLetter(pasword);

            if (аvailabilityCapitalLetter)
            {
                ver = ver && capitalLetter(pasword);
            }
            if (аvailabilityNumeral)
            {
                ver = ver && numeral(pasword);
            }
            if (аvailabilitySpecialSymbols)
            {
                ver = ver && specialSymbols(pasword);
            }

            return ver;

        }

        public static bool capitalLetter(string password)
        {
            bool capitalLetter = false;

            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (password.Contains(i))
                {
                    capitalLetter = true;
                }
            }

            return capitalLetter;
        }

        public static bool smallLetter(string password)
        {
            bool smallLetter = false;
            for (char i = 'a'; i <= 'z'; i++)
            {
                if (password.Contains(i))
                {
                    smallLetter = true;
                }
            }


            return smallLetter;
        }

        public static bool numeral(string password)
        {
            bool numeral = false;
            for (char i = '0'; i <= '9'; i++)
            {
                if (password.Contains(i))
                {
                    numeral = true;
                }
            }
            return numeral;
        }

        public static bool specialSymbols(string password)
        {
            bool specialSymbols = false;

            List<char> chars = new List<char>();
            for (char i = '!'; i <= '~'; i++)
            {
                chars.Add(i);
            }
            for (char i = 'A'; i <= 'Z'; i++)
            {
                chars.Remove(i);
            }
            for (char i = 'a'; i <= 'z'; i++)
            {
                chars.Remove(i);
            }
            for (char i = '0'; i <= '9'; i++)
            {
                chars.Remove(i);
            }



            foreach (char c in chars)
            {
                if (password.Contains(c))
                {
                    specialSymbols = true;
                }
            }
            return specialSymbols;
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
                if (password.Contains(i))
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
            foreach (char c in chars)
            {
                if (password.Contains(c))
                {
                    specialSymbols = true;
                }
            }

            bool quantity = false;
            if (password.Length > 10)
            {
                quantity = true;
            }

            strong = capitalLetter && smallLetter && numeral && specialSymbols && quantity;

            return strong;

        }

    }
}

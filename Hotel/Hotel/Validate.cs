using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel
{
    public class Validate
    {
        public int ValidateLogin(string username)
        {

            if (username.Contains(" "))
            {
                return 1;
            }
            else if (string.IsNullOrEmpty(username))
            {
                return 2;
            }
            else
            {
                foreach (char c in username)
                {
                    if (!char.IsLetterOrDigit(c))
                    {
                        return 3;
                    }
                }
            }
            return 4;
        }

        public int ValidateName(string surname)
        {

            if (surname.Length > 20 || surname.Length <= 1)
            {
                return 1;
            }
            else if (surname.Contains(" "))
            {
                return 3;
            }
            else
            {
                foreach (char c in surname)
                {
                    if (!char.IsLetter(c))
                    {
                        return 4;
                    }
                }
            }
            return 5;
        }
        public int ValidatePassword(string password)
        {
            if(password.Length > 50 || password.Length < 6)
            {
                return 0;
            }
            else if (password.Contains(" "))
            {
                return 1;
            }
            else if (string.IsNullOrEmpty(password))
            {
                return 2;
            }
            return 3;
        }
    }
}

using System.Text.RegularExpressions;

namespace Hotel
{
    public class Validate
    {
        public string message = "";
        public int ValidateLogin(string login)
        {
            if (login.Length>40||login.Length<1)
            {
                return 0;
            }
            else if (login.Contains(" "))
            {
                return 1;
            }
            else
            {
                foreach (char c in login)
                {
                    if (!char.IsLetterOrDigit(c))
                    {
                        return 2;
                    }
                }
            }
            return 3;
        }

        public int ValidateSurnameOrName(string surname)
        {
            if (surname.Length > 40 || surname.Length < 1)
            {
                return 1;
            }
            else if (surname.Contains(" "))
            {
                return 2;
            }
            else
            {
                foreach (char c in surname)
                {
                    if (!char.IsLetter(c))
                    {
                        return 3;
                    }
                }
            }
            return 4;
        }
        public int ValidatePatronymic(string patronymic)
        {
            if (patronymic.Length == 0)
            {
                return 4;
            }
            if (patronymic.Length > 40)
            {
                return 1;
            }
            else if (patronymic.Contains(" "))
            {
                return 2;
            }
            else
            {
                foreach (char c in patronymic)
                {
                    if (!char.IsLetter(c))
                    {
                        return 3;
                    }
                }
            }
            return 4;
        }
        public int ValidatePassword(string password)
        {
            if (( password.Length < 6) || (password.Length > 40))
            {

                return 0;
            }
            else if (password.Contains(" "))
            {
                return 1;
            }
            return 2;

        }
        public int ValidateTelephone(string telephone)
        {
            string numbersOnly = Regex.Replace(telephone, "[^0-9#]", "");
            if (numbersOnly.Length != 11)
            {
                return 0;
            }
            return 1;
        }
        public int ValidatePassportSeria(string seria)
        {
            if (seria.Length < 4)
            {
                return 0;
            }
            else if (!int.TryParse(seria, out int s)|| seria.Contains(" "))
            {
                return 1;
            }
            return 2;
        }
        public int ValidatePassportNumber(string number)
        {
            if (number.Length < 6)
            {
                return 0;
            }
            else if (!int.TryParse(number, out int s) || number.Contains(" "))
            {
                return 1;
            }
            return 2;
        }
        public int ValidateEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                return 0;
            }  
            return 1;
            
        }
        public bool ValidateAll(string Surname, string Name, string Patronymic, string Telephone, string Email, string PassportSeria, string PassportNumber)
        {
            int s = ValidateSurnameOrName(Surname);
            int n = ValidateSurnameOrName(Name);
            int ptr = ValidatePatronymic(Patronymic);
            int t = ValidateTelephone(Telephone);
            int e = ValidateEmail(Email);
            int pS = ValidatePassportSeria(PassportSeria);
            int pN = ValidatePassportNumber(PassportNumber);


            if (s != 4 || n != 4 || ptr!=4 || t != 1 || e != 1 || pS!=2 || pN!=2)
            {
                switch (s)
                {
                    case 1:
                        message += "Фамилия должна содержать 1 до 40 символов\n";
                        break;
                    case 2:
                        message += "Фамилия не должна содержать пробелы\n";
                        break;
                    case 3:
                        message += "Фамилия должна содержать только буквы\n";
                        break;
                }
                switch (n)
                {
                    case 1:
                        message += "Имя должно содержать 1 до 40 символов\n";
                        break;
                    case 2:
                        message += "Имя не должно содержать пробелы\n";
                        break;
                    case 3:
                        message += "Имя должно содержать только буквы\n";
                        break;
                }
                switch (ptr)
                {
                    case 1:
                        message += "Отчество должно содержать до 40 символов\n";
                        break;
                    case 2:
                        message += "Отчество не должно содержать пробелы\n";
                        break;
                    case 3:
                        message += "Отчество должно содержать только буквы\n";
                        break;
                }
                switch (t)
                {
                    case 0:
                        message += "Неверный формат номера телефона\n";
                        break;
                }
                switch (e)
                {
                    case 0:
                        message += "Неверный формат почты\n";
                        break;
                }
                switch (pS)
                {
                    case 0:
                        message += "Серия паспорта должна содержать 4 цифры\n";
                        break;
                    case 1:
                        message += "Серия паспорта должна содержать только цифры\n";
                        break;
                }
                switch (pN)
                {
                    case 0:
                        message += "Номер паспорта должен содержать 6 цифр\n";
                        break;
                    case 1:
                        message += "Номер паспорта должен содержать только цифры\n";
                        break;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidateAll(string Login, string Password)
        {
            int l = ValidateLogin(Login);
            int p = ValidatePassword(Password);


            if (l != 3 || p != 2)
            {
                switch (p)
                {
                    case 0:
                        message += "Пароль должен содержать от 6 до 40 символов\n";
                        break;
                    case 1:
                        message += "Пароль не должен содержать пробелы\n";
                        break;

                }
                switch (l)
                {
                    case 0:
                        message += "Логин должен содержать от 1 до 40 символов\n";
                        break;
                    case 1:
                        message += "Логин не должен содержать пробелы\n";
                        break;
                    case 2:
                        message += "Логин должен содержать только буквы и цифры\n";
                        break;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidateOnlyPassword(string Password)
        {
            int p = ValidatePassword(Password);

            if (p != 2)
            {
                switch (p)
                {
                    case 0:
                        message += "Пароль должен содержать от 6 до 40 символов\n";
                        break;
                    case 1:
                        message += "Пароль не должен содержать пробелы\n";
                        break;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

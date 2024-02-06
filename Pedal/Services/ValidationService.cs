using System.Text.RegularExpressions;

namespace Pedal.Services
{
    public static class ValidationService
    {
        public static bool IsValidEmail(string email)
        {
            string emailRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 8;
        }
    }
}

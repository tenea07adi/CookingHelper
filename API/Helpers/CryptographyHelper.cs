using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public static class CryptographyHelper
    {
        private static readonly string _highCaseLettersCh = "QWERTYUIOPLKJHGFDSAZXCVBNM";
        private static readonly string _lowerCaseLettersCh = _highCaseLettersCh.ToLower();
        private static readonly string _digitsCh = "1234567890";
        private static readonly string _specialCh = ",./;'[]";

        private static readonly string _defaultAlphabet = _highCaseLettersCh + _lowerCaseLettersCh + _digitsCh + _specialCh;

        public static bool IsSafePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if(password.Length < 6)
            {
                return false;
            }

            var containsHC = _highCaseLettersCh.ToCharArray().Intersect(password.ToCharArray()).Count() > 0;
            var containsLC = _lowerCaseLettersCh.ToCharArray().Intersect(password.ToCharArray()).Count() > 0;
            var containsDC = _digitsCh.ToCharArray().Intersect(password.ToCharArray()).Count() > 0;
            var containsSC = _specialCh.ToCharArray().Intersect(password.ToCharArray()).Count() > 0;

            if (!containsHC || !containsLC || !containsDC || !containsSC)
            {
                return false;
            }

            return true;
        }

        public static bool IsCorrectPassword(string password, string passwrdHash, string passwordSalt)
        {
            string newHash = HashPassword(password, passwordSalt);

            return newHash == passwrdHash;
        }

        public static string HashPassword(string password, string passwordSalt)
        {
            byte[] byteSalt = Encoding.ASCII.GetBytes(passwordSalt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, byteSalt, 100000);

            byte[] byteHash = pbkdf2.GetBytes(20);

            string hash = Convert.ToBase64String(byteHash);

            return hash;
        }

        public static string GeneratePasswordSalt()
        {
            return GenerateRandomString(10);
        }

        public static string GenerateRandomString(int length)
        {
            return GenerateRandomString(_defaultAlphabet, length);
        }

        public static string GenerateRandomString(string alphabet, int length)
        {
            string result = string.Empty;

            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                result += alphabet[rnd.Next(0, alphabet.Length)];
            }

            return result;
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace Homuai.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        private readonly string _keyAdditional;

        public PasswordEncripter(string keyAdditional)
        {
            _keyAdditional = keyAdditional;
        }

        public string Encrypt(string s)
        {
            s += _keyAdditional;

            var bytes = Encoding.UTF8.GetBytes(s);
            var sha512 = SHA512.Create();
            byte[] hashBytes = sha512.ComputeHash(bytes);
            return StringBytes(hashBytes);
        }

        private string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}

using Homuai.Application.Services.Cryptography;

namespace Useful.ToTests.Builders.Encripter
{
    public class PasswordEncripterBuilder
    {
        private static PasswordEncripterBuilder _instance;
        private readonly PasswordEncripter _encripter;

        private PasswordEncripterBuilder()
        {
            if (_encripter == null)
            {
                _encripter = new PasswordEncripter("keyAddtional");
            }
        }

        public static PasswordEncripterBuilder Instance()
        {
            _instance = new PasswordEncripterBuilder();
            return _instance;
        }

        public PasswordEncripter Build()
        {
            return _encripter;
        }
    }
}

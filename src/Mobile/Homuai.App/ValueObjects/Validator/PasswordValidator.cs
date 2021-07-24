using Homuai.Exception.Exceptions;

namespace Homuai.App.ValueObjects.Validator
{
    public class PasswordValidator
    {
        public void IsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();

            if (password.Length < 6)
                throw new InvalidPasswordException();
        }
    }
}

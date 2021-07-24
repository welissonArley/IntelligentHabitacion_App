namespace Homuai.App.Services
{
    public class ShortNameConverter
    {
        public string Converter(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";
            else
            {
                var words = value.Split(' ');
                return (words.Length == 1 ? $"{words[0][0]}" : $"{words[0][0]}{words[1][0]}").ToUpper();
            }
        }
    }
}

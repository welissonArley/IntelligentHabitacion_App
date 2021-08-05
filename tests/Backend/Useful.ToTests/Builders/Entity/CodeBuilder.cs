using Bogus;
using Homuai.Domain.Entity;

namespace Useful.ToTests.Builders.Entity
{
    public class CodeBuilder
    {
        private static CodeBuilder _instance;

        public static CodeBuilder Instance()
        {
            _instance = new CodeBuilder();
            return _instance;
        }

        public Code Build(long userId)
        {
            return new Faker<Code>()
                .RuleFor(u => u.Value, (f) => f.Internet.Random.AlphaNumeric(6).ToUpper())
                .RuleFor(u => u.UserId, () => userId);
        }
    }
}

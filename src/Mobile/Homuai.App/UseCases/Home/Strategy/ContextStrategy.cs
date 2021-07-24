using Homuai.App.Model;
using Homuai.App.ValueObjects.Enum;

namespace Homuai.App.UseCases.Home.Strategy
{
    public class ContextStrategy
    {
        public HomeRegisterStrategy GetStrategy(CountryModel country)
        {
            switch (country.Id)
            {
                case CountryEnum.BRAZIL:
                    {
                        return new BrazilHomeRegisterStrategy();
                    }
                default:
                    {
                        return new OthersHomeRegisterStrategy();
                    }
            }
        }
    }
}

using Homuai.App.Model;
using Homuai.App.Services.Communication.Home;
using Homuai.Exception;
using Homuai.Exception.Exceptions;
using Refit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Home.RegisterHome.Brazil
{
    public class RequestCEPUseCase : IRequestCEPUseCase
    {
        private readonly IZipCodeService _restService;

        public RequestCEPUseCase()
        {
            _restService = RestService.For<IZipCodeService>("https://viacep.com.br/ws/");
        }

        public async Task<HomeModel> Execute(string cep)
        {
            Validate(cep);

            var result = await _restService.GetLocationBrazilByZipCode(cep.Replace(".", "").Replace("-", ""));

            return new HomeModel
            {
                City = new CityModel
                {
                    Name = result.Localidade,
                    StateProvinceName = StateAbbreviationToFullNameState(result.Uf)
                },
                Address = result.Logradouro,
                Neighborhood = result.Bairro,
            };
        }

        public void Validate(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ZipCodeEmptyException();

            Regex regex = new Regex(RegexExpressions.CEP);
            if (!regex.Match(zipCode).Success)
                throw new ZipCodeInvalidException(ResourceTextException.ZIPCODE_INVALID_BRAZIL);
        }

        private string StateAbbreviationToFullNameState(string abbreviation)
        {
            switch (abbreviation.ToUpper())
            {
                case "AC":
                    {
                        return "Acre";
                    }
                case "AL":
                    {
                        return "Alagoas";
                    }
                case "AP":
                    {
                        return "Amapá";
                    }
                case "AM":
                    {
                        return "Amazonas";
                    }
                case "BA":
                    {
                        return "Bahia";
                    }
                case "CE":
                    {
                        return "Ceará";
                    }
                case "ES":
                    {
                        return "Espírito Santo";
                    }
                case "GO":
                    {
                        return "Goiás";
                    }
                case "MA":
                    {
                        return "Maranhão";
                    }
                case "MT":
                    {
                        return "Mato Grosso";
                    }
                case "MS":
                    {
                        return "Mato Grosso do Sul";
                    }
                case "MG":
                    {
                        return "Minas Gerais";
                    }
                case "PA":
                    {
                        return "Pará";
                    }
                case "PB":
                    {
                        return "Paraíba";
                    }
                case "PR":
                    {
                        return "Paraná";
                    }
                case "PE":
                    {
                        return "Pernambuco";
                    }
                case "PI":
                    {
                        return "Piauí";
                    }
                case "RJ":
                    {
                        return "Rio de Janeiro";
                    }
                case "RN":
                    {
                        return "Rio Grande do Norte";
                    }
                case "RS":
                    {
                        return "Rio Grande do Sul";
                    }
                case "RO":
                    {
                        return "Rondônia";
                    }
                case "RR":
                    {
                        return "Roraima";
                    }
                case "SC":
                    {
                        return "Santa Catarina";
                    }
                case "SP":
                    {
                        return "São Paulo";
                    }
                case "SE":
                    {
                        return "Sergipe";
                    }
                case "TO":
                    {
                        return "Tocantins";
                    }
                case "DF":
                    {
                        return "Distrito Federal";
                    }
                default:
                    {
                        return "";
                    }
            }
        }
    }
}

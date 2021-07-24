using Homuai.App.Model;
using Homuai.App.ValueObjects.Enum;
using System.Collections.Generic;
using System.Linq;

namespace Homuai.App.ValueObjects
{
    public class CoutriesAvaliables
    {
        public IList<CountryModel> Get()
        {
            return new List<CountryModel>
            {
                new CountryModel{Id = CountryEnum.AUSTRALIA, PhoneCode = "+61", Name = ResourceText.AUSTRALIA, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Flag_of_Australia.svg/23px-Flag_of_Australia.svg.png" },
                new CountryModel{Id = CountryEnum.BELGIUM, PhoneCode = "+32", Name = ResourceText.BELGIUM, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/Flag_of_Belgium_%28civil%29.svg/23px-Flag_of_Belgium_%28civil%29.svg.png" },
                new CountryModel{Id = CountryEnum.BRAZIL, PhoneCode = "+55", Name = ResourceText.BRAZIL, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/05/Flag_of_Brazil.svg/22px-Flag_of_Brazil.svg.png" },
                new CountryModel{Id = CountryEnum.CANADA, PhoneCode = "+1", Name = ResourceText.CANADA, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Flag_of_Canada_%28Pantone%29.svg/23px-Flag_of_Canada_%28Pantone%29.svg.png" },
                new CountryModel{Id = CountryEnum.SPAIN, PhoneCode = "+34", Name = ResourceText.SPAIN, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Flag_of_Spain.svg/23px-Flag_of_Spain.svg.png" },
                new CountryModel{Id = CountryEnum.UNITED_STATES, PhoneCode = "+1", Name = ResourceText.UNITED_STATES, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Flag_of_the_United_States.svg/23px-Flag_of_the_United_States.svg.png" },
                new CountryModel{Id = CountryEnum.FRANCE, PhoneCode = "+33", Name = ResourceText.FRANCE, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/Flag_of_France.svg/23px-Flag_of_France.svg.png" },
                new CountryModel{Id = CountryEnum.HONG_KONG, PhoneCode = "+852", Name = ResourceText.HONG_KONG, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5b/Flag_of_Hong_Kong.svg/23px-Flag_of_Hong_Kong.svg.png" },
                new CountryModel{Id = CountryEnum.MALDIVES, PhoneCode = "+960", Name = ResourceText.MALDIVES, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0f/Flag_of_Maldives.svg/23px-Flag_of_Maldives.svg.png" },
                new CountryModel{Id = CountryEnum.ITALY, PhoneCode = "+39", Name = ResourceText.ITALY, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Flag_of_Italy.svg/23px-Flag_of_Italy.svg.png" },
                new CountryModel{Id = CountryEnum.JAPAN, PhoneCode = "+81", Name = ResourceText.JAPAN, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/Flag_of_Japan.svg/23px-Flag_of_Japan.svg.png" },
                new CountryModel{Id = CountryEnum.LUXEMBOURG, PhoneCode = "+352", Name = ResourceText.LUXEMBOURG, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/Flag_of_Luxembourg.svg/23px-Flag_of_Luxembourg.svg.png" },
                new CountryModel{Id = CountryEnum.MEXICO, PhoneCode = "+52", Name = ResourceText.MEXICO, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Flag_of_Mexico.svg/23px-Flag_of_Mexico.svg.png" },
                new CountryModel{Id = CountryEnum.MONACO, PhoneCode = "+377", Name = ResourceText.MONACO, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ea/Flag_of_Monaco.svg/19px-Flag_of_Monaco.svg.png" },
                new CountryModel{Id = CountryEnum.NETHERLANDS, PhoneCode = "+31", Name = ResourceText.NETHERLANDS, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Flag_of_the_Netherlands.svg/23px-Flag_of_the_Netherlands.svg.png" },
                new CountryModel{Id = CountryEnum.PUERTO_RICO, PhoneCode = "+1", Name = ResourceText.PUERTO_RICO, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/28/Flag_of_Puerto_Rico.svg/23px-Flag_of_Puerto_Rico.svg.png" },
                new CountryModel{Id = CountryEnum.PORTUGAL, PhoneCode = "+351", Name = ResourceText.PORTUGAL, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/Flag_of_Portugal.svg/23px-Flag_of_Portugal.svg.png" },
                new CountryModel{Id = CountryEnum.UNITED_KINGDOM, PhoneCode = "+44", Name = ResourceText.UNITED_KINGDOM, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ae/Flag_of_the_United_Kingdom.svg/23px-Flag_of_the_United_Kingdom.svg.png" },
                new CountryModel{Id = CountryEnum.RUSSIA, PhoneCode = "+7", Name = ResourceText.RUSSIA, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f3/Flag_of_Russia.svg/23px-Flag_of_Russia.svg.png" },
                new CountryModel{Id = CountryEnum.IRELAND, PhoneCode = "+353", Name = ResourceText.IRELAND, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/45/Flag_of_Ireland.svg/23px-Flag_of_Ireland.svg.png" },
                new CountryModel{Id = CountryEnum.NEW_ZEALAND, PhoneCode = "+64", Name = ResourceText.NEW_ZEALAND, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Flag_of_New_Zealand.svg/23px-Flag_of_New_Zealand.svg.png" },
                new CountryModel{Id = CountryEnum.SWITZERLAND, PhoneCode = "+41", Name = ResourceText.SWITZERLAND, Flag = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f3/Flag_of_Switzerland.svg/16px-Flag_of_Switzerland.svg.png" }
            }.OrderBy(c => c.Name).ToList();
        }
    }
}

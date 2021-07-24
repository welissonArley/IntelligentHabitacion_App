using AutoMapper;
using Homuai.Application.Services.AutoMapper;
using Useful.ToTests.Builders.Hashids;

namespace Useful.ToTests.Builders.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            var hashids = HashidsBuilder.Instance().Build();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping(hashids));
            });
            return mockMapper.CreateMapper();
        }
    }
}

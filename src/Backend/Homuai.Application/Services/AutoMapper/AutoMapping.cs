using AutoMapper;
using HashidsNet;
using System.Linq;

namespace Homuai.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        private readonly IHashids _hashids;

        public AutoMapping(IHashids hashids)
        {
            _hashids = hashids;

            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<Communication.Request.RequestRegisterUserJson, Domain.Entity.User>();

            CreateMap<string, Domain.Entity.Phonenumber>()
                .ForMember(c => c.Number, opt => opt.MapFrom(src => src));

            CreateMap<Communication.Request.RequestEmergencyContactJson, Domain.Entity.EmergencyContact>();

            CreateMap<Communication.Request.RequestRegisterHomeJson, Domain.Entity.Home>();

            CreateMap<Communication.Request.RequestUpdateHomeJson, Domain.Entity.Home>()
                .ForMember(c => c.Rooms, opt => opt.MapFrom(w => w.Rooms.Distinct().Where(k => !string.IsNullOrWhiteSpace(k)).Select(k => new Domain.Entity.Room { Name = k })));

            CreateMap<Communication.Request.RequestProductJson, Domain.Entity.MyFood>();
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entity.User, Communication.Response.ResponseUserInformationsJson>();
            CreateMap<Domain.Entity.Phonenumber, Communication.Response.ResponsePhonenumberJson>();
            CreateMap<Domain.Entity.EmergencyContact, Communication.Response.ResponseEmergencyContactJson>();
            CreateMap<Domain.Entity.User, Communication.Response.ResponseLoginJson>()
                .ForMember(c => c.Id, opt => opt.MapFrom(w => _hashids.EncodeLong(w.Id)))
                .ForMember(c => c.IsPartOfOneHome, opt => opt.MapFrom(w => w.IsPartOfHome()))
                .ForMember(c => c.IsAdministrator, opt => opt.MapFrom(w => w.IsAdministrator()));

            CreateMap<Domain.Entity.User, Communication.Response.ResponseUserRegisteredJson>()
                .ForMember(c => c.Id, opt => opt.MapFrom(w => _hashids.EncodeLong(w.Id)));

            CreateMap<Domain.Entity.Home, Communication.Response.ResponseHomeInformationsJson>()
                .ForMember(c => c.NetWork, opt => opt.MapFrom(w => new Communication.Response.ResponseWifiNetworkJson
                { Name = w.NetworksName, Password = w.NetworksPassword }));

            CreateMap<Domain.Entity.Room, Communication.Response.ResponseRoomJson>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => _hashids.EncodeLong(c.Id)));

            CreateMap<Domain.Entity.MyFood, Communication.Response.ResponseMyFoodJson>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => _hashids.EncodeLong(c.Id)));

            CreateMap<Domain.Entity.User, Communication.Response.ResponseFriendJson>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => _hashids.EncodeLong(c.Id)))
                .ForMember(c => c.JoinedOn, opt => opt.MapFrom(c => c.HomeAssociation.JoinedOn));

            CreateMap<Domain.Entity.User, Communication.Response.ResponseUserSimplifiedJson>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => _hashids.EncodeLong(c.Id)));
        }
    }
}

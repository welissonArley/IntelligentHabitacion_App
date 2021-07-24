using AutoMapper;
using FluentAssertions;
using Homuai.Application.Services.Cryptography;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.User.RegisterUser;
using Homuai.Communication.Response;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.User.RegisterUser
{
    public class RegisterUserUseCaseTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IMapper _mapper;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        public RegisterUserUseCaseTest()
        {
            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            _userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();
            _userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var user = RequestRegisterUser.Instance().Build();

            var useCase = new RegisterUserUseCase(_mapper, _unitOfWork, _homuaiUseCase, _userWriteOnlyRepository, _userReadOnlyRepository, _passwordEncripter);

            var validationResult = await useCase.Execute(user);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<ResponseUserRegisteredJson>();

            var responseJson = validationResult.ResponseJson.As<ResponseUserRegisteredJson>();
            responseJson.ProfileColorLightMode.Should().NotBeNullOrEmpty().And.StartWith("#");
            responseJson.ProfileColorDarkMode.Should().NotBeNullOrEmpty().And.StartWith("#");
            responseJson.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Validade_Empty_PhoneNumbersAndEmergencyContacts()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Phonenumbers.Clear();
            user.EmergencyContacts.Clear();

            var useCase = new RegisterUserUseCase(_mapper, _unitOfWork, _homuaiUseCase, _userWriteOnlyRepository, _userReadOnlyRepository, _passwordEncripter);

            Func<Task> act = async () => { await useCase.Execute(user); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 2 &&
                    e.ErrorMensages.Contains(ResourceTextException.PHONENUMBER_EMPTY)
                    && e.ErrorMensages.Contains(ResourceTextException.EMERGENCYCONTACT_EMPTY));
        }
    }
}

using AutoMapper;
using FluentAssertions;
using Homuai.Application.Services.LoggedUser;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.Application.UseCases.User.UpdateUserInformations;
using Homuai.Communication.Boolean;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using Moq;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.User.UpdateUserInformations
{
    public class UpdateUserInformationsUseCaseTest
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly Homuai.Domain.Entity.User _user;

        public UpdateUserInformationsUseCaseTest()
        {
            _user = UserBuilder.Instance().WithoutHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            _userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(_user).Build();
            _loggedUser = LoggedUserBuilder.Instance().User(_user).Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var request = RequestUpdateUser.Instance().Build();

            var emailAlreadyBeenRegisteredUseCase = new Mock<IEmailAlreadyBeenRegisteredUseCase>();
            emailAlreadyBeenRegisteredUseCase.Setup(c => c.Execute(request.Email)).ReturnsAsync(new BooleanJson { Value = false });

            var useCase = new UpdateUserInformationsUseCase(_loggedUser, _mapper, _userUpdateOnlyRepository, _unitOfWork, emailAlreadyBeenRegisteredUseCase.Object, _homuaiUseCase);

            var validationResult = await useCase.Execute(request);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();

            _user.Email.Should().Equals(request.Email);
            _user.Name.Should().Equals(request.Name);
        }

        [Fact]
        public async Task Validade_NewEmailRegistered()
        {
            var request = RequestUpdateUser.Instance().Build();

            var emailAlreadyBeenRegisteredUseCase = new Mock<IEmailAlreadyBeenRegisteredUseCase>();
            emailAlreadyBeenRegisteredUseCase.Setup(c => c.Execute(request.Email)).ReturnsAsync(new BooleanJson { Value = true });

            var useCase = new UpdateUserInformationsUseCase(_loggedUser, _mapper, _userUpdateOnlyRepository, _unitOfWork, emailAlreadyBeenRegisteredUseCase.Object, _homuaiUseCase);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.EMAIL_ALREADYBEENREGISTERED));
        }

        [Fact]
        public async Task Validade_EmptyName()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.Name = "";

            var emailAlreadyBeenRegisteredUseCase = new Mock<IEmailAlreadyBeenRegisteredUseCase>();
            emailAlreadyBeenRegisteredUseCase.Setup(c => c.Execute(request.Email)).ReturnsAsync(new BooleanJson { Value = false });

            var useCase = new UpdateUserInformationsUseCase(_loggedUser, _mapper, _userUpdateOnlyRepository, _unitOfWork, emailAlreadyBeenRegisteredUseCase.Object, _homuaiUseCase);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.NAME_EMPTY));
        }
    }
}

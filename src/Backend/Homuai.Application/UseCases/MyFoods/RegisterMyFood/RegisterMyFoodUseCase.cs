using AutoMapper;
using HashidsNet;
using Homuai.Application.Services.LoggedUser;
using Homuai.Application.UseCases.MyFoods.Validator;
using Homuai.Communication.Request;
using Homuai.Domain.Entity;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.MyFoods;
using Homuai.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.RegisterMyFood
{
    public class RegisterMyFoodUseCase : IRegisterMyFoodUseCase
    {
        private readonly IHashids _hashIds;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodsWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterMyFoodUseCase(IMyFoodsWriteOnlyRepository repository, IUnitOfWork unitOfWork,
            ILoggedUser loggedUser, IMapper mapper, HomuaiUseCase homuaiUseCase,
            IHashids hashIds)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _homuaiUseCase = homuaiUseCase;
            _hashIds = hashIds;
        }

        public async Task<ResponseOutput> Execute(RequestProductJson requestMyFood)
        {
            Validate(requestMyFood);

            var loggedUser = await _loggedUser.User();

            var foodModel = _mapper.Map<MyFood>(requestMyFood);
            foodModel.UserId = loggedUser.Id;

            await _repository.Add(foodModel);

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            response.ResponseJson = _hashIds.EncodeLong(foodModel.Id);

            return response;
        }

        private void Validate(RequestProductJson requestMyFood)
        {
            var validation = new MyFoodValidation().Validate(requestMyFood);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}

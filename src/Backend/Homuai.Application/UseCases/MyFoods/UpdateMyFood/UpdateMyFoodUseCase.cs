using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Application.UseCases.MyFoods.Validator;
using Homuai.Communication.Request;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.MyFoods;
using Homuai.Exception.Exceptions;
using Homuai.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.UpdateMyFood
{
    public class UpdateMyFoodUseCase : IUpdateMyFoodUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodsUpdateOnlyRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMyFoodUseCase(IMyFoodsUpdateOnlyRepository repository, IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase,
            ILoggedUser loggedUser, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _homuaiUseCase = homuaiUseCase;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseOutput> Execute(long myFoodId, RequestProductJson editMyFood)
        {
            Validate(editMyFood);

            var loggedUser = await _loggedUser.User();

            var model = await _repository.GetById_Update(myFoodId, loggedUser.Id);
            if (model is null)
                throw new ProductNotFoundException();

            _mapper.Map(editMyFood, model);

            _repository.Update(model);

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

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

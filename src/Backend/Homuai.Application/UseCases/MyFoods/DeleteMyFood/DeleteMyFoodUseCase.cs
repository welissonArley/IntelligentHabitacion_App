using Homuai.Application.Services.LoggedUser;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.MyFoods;
using Homuai.Exception.Exceptions;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.DeleteMyFood
{
    public class DeleteMyFoodUseCase : IDeleteMyFoodUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodsReadOnlyRepository _repositoryReadOnly;
        private readonly IMyFoodsWriteOnlyRepository _repository;

        public DeleteMyFoodUseCase(IMyFoodsReadOnlyRepository repositoryReadOnly,
            IMyFoodsWriteOnlyRepository repository, IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase,
            ILoggedUser loggedUser)
        {
            _repositoryReadOnly = repositoryReadOnly;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _homuaiUseCase = homuaiUseCase;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseOutput> Execute(long myFoodId)
        {
            var loggedUser = await _loggedUser.User();

            await Validate(myFoodId, loggedUser);

            var model = await _repositoryReadOnly.GetById(myFoodId, loggedUser.Id);

            _repository.Delete(model);

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private async Task Validate(long id, Domain.Entity.User loggedUser)
        {
            var model = await _repositoryReadOnly.GetById(id, loggedUser.Id);
            if (model is null)
                throw new ProductNotFoundException();
        }
    }
}

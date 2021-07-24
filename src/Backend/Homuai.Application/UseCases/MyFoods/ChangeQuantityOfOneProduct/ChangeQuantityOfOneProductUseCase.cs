using Homuai.Application.Services.LoggedUser;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.MyFoods;
using Homuai.Exception.Exceptions;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.ChangeQuantityOfOneProduct
{
    public class ChangeQuantityOfOneProductUseCase : IChangeQuantityOfOneProductUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodsReadOnlyRepository _repositoryReadOnly;
        private readonly IMyFoodsWriteOnlyRepository _repository;

        public ChangeQuantityOfOneProductUseCase(IMyFoodsReadOnlyRepository repositoryReadOnly,
            IMyFoodsWriteOnlyRepository repository, IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase,
            ILoggedUser loggedUser)
        {
            _repositoryReadOnly = repositoryReadOnly;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _homuaiUseCase = homuaiUseCase;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseOutput> Execute(long id, decimal amount)
        {
            var loggedUser = await _loggedUser.User();

            await Validate(id, loggedUser);

            var model = await _repositoryReadOnly.GetById(id, loggedUser.Id);

            if (amount <= 0)
                _repository.Delete(model);
            else
                await _repository.ChangeAmount(model.Id, amount);
            
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

using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Response;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.MyFoods;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.GetMyFoods
{
    public class GetMyFoodsUseCase : IGetMyFoodsUseCase
    {
        private readonly IMapper _mapper;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyFoodsReadOnlyRepository _repository;

        public GetMyFoodsUseCase(IMyFoodsReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser,
            IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase)
        {
            _repository = repository;
            _mapper = mapper;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseOutput> Execute()
        {
            var user = await _loggedUser.User();

            var myFoods = await _repository.GetByUserId(user.Id);

            var json = _mapper.Map<List<ResponseMyFoodJson>>(myFoods);

            var response = await _homuaiUseCase.CreateResponse(user.Email, user.Id, json);

            await _unitOfWork.Commit();

            return response;
        }
    }
}

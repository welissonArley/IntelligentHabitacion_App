using Homuai.Communication.Boolean;
using Homuai.Domain.Repository.User;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered
{
    public class EmailAlreadyBeenRegisteredUseCase : IEmailAlreadyBeenRegisteredUseCase
    {
        private readonly IUserReadOnlyRepository _repository;

        public EmailAlreadyBeenRegisteredUseCase(IUserReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<BooleanJson> Execute(string email)
        {
            return new BooleanJson
            {
                Value = await _repository.ExistActiveUserWithEmail(email)
            };
        }
    }
}

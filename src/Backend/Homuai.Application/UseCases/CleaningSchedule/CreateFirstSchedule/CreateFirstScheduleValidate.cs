using FluentValidation;
using Homuai.Communication.Request;
using Homuai.Domain.Repository.CleaningSchedule;
using Homuai.Exception;
using System.Collections.Generic;
using System.Linq;

namespace Homuai.Application.UseCases.CleaningSchedule.CreateFirstSchedule
{
    public class CreateFirstScheduleValidate : AbstractValidator<IList<RequestUpdateCleaningScheduleJson>>
    {
        public CreateFirstScheduleValidate(ICleaningScheduleReadOnlyRepository repository, long homeId)
        {
            RuleFor(c => homeId).MustAsync(async (idHome, cancelationToken) =>
            {
                var homeHasCleaningScheduleCreated = await repository.HomeHasCleaningScheduleCreated(idHome);
                return !homeHasCleaningScheduleCreated;

            }).WithMessage(ResourceTextException.CLEANING_SCHEDULE_ALREADY_CREATED);
            
            RuleFor(c => c).Must(c => c.Select(k => k.UserId).Distinct().Count() == c.Count()).WithMessage(ResourceTextException.THERE_ARE_DUPLICATE_USERS_REQUEST);
            
            RuleForEach(c => c).ChildRules(users =>
            {
                users.RuleFor(c => c.Rooms).Must(c => c.Distinct().Count() == c.Count()).WithMessage(ResourceTextException.THERE_ARE_USERS_DUPLICATE_TASKS_REQUEST);
            });
            
            RuleFor(c => c).Must(c => c.SelectMany(k => k.Rooms).Any()).WithMessage(ResourceTextException.ALL_USER_WITHOUT_CLEANING_TASKS);
        }
    }
}

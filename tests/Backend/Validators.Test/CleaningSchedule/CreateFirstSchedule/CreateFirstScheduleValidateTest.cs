using FluentAssertions;
using Homuai.Application.UseCases.CleaningSchedule.CreateFirstSchedule;
using Homuai.Communication.Request;
using Homuai.Domain.Repository.CleaningSchedule;
using Homuai.Exception;
using System.Collections.Generic;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace Validators.Test.CleaningSchedule.CreateFirstSchedule
{
    public class CreateFirstScheduleValidateTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            const long HomeId = 1;

            var validator = CreateValidator(HomeId);

            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1",
                    Rooms = new List<string> { "Living Room" }
                }
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_Home_Has_Schedule()
        {
            const long HomeId = 1;

            var validator = CreateValidator(HomeId, true);

            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1",
                    Rooms = new List<string> { "Living Room" }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CLEANING_SCHEDULE_ALREADY_CREATED));
        }

        [Fact]
        public async Task Validade_User_Duplicated()
        {
            const long HomeId = 1;

            var validator = CreateValidator(HomeId);

            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1",
                    Rooms = new List<string> { "Living Room" }
                },
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1",
                    Rooms = new List<string> { "Bathroom" }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THERE_ARE_DUPLICATE_USERS_REQUEST));
        }

        [Fact]
        public async Task Validade_User_Tasks_Duplicated()
        {
            const long HomeId = 1;

            var validator = CreateValidator(HomeId);

            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1",
                    Rooms = new List<string> { "Living Room", "Living Room" }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THERE_ARE_USERS_DUPLICATE_TASKS_REQUEST));
        }

        [Fact]
        public async Task Validade_User_Without_Tasks()
        {
            const long HomeId = 1;

            var validator = CreateValidator(HomeId);

            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1"
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ALL_USER_WITHOUT_CLEANING_TASKS));
        }

        private CreateFirstScheduleValidate CreateValidator(long homeId, bool hasCleaningScheduleCreated = false)
        {
            ICleaningScheduleReadOnlyRepository repository;

            if (hasCleaningScheduleCreated)
                repository = CleaningScheduleReadOnlyRepositoryBuilder.Instance().HomeHasCleaningScheduleCreated(homeId).Build();
            else
                repository = CleaningScheduleReadOnlyRepositoryBuilder.Instance().Build();

            return new CreateFirstScheduleValidate(repository, homeId);
        }
    }
}

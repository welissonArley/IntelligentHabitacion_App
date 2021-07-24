using Homuai.Domain.Repository.CleaningSchedule;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class CleaningScheduleReadOnlyRepositoryBuilder
    {
        private static CleaningScheduleReadOnlyRepositoryBuilder _instance;
        private readonly Mock<ICleaningScheduleReadOnlyRepository> _repository;

        private CleaningScheduleReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ICleaningScheduleReadOnlyRepository>();
        }

        public static CleaningScheduleReadOnlyRepositoryBuilder Instance()
        {
            _instance = new CleaningScheduleReadOnlyRepositoryBuilder();
            return _instance;
        }

        public CleaningScheduleReadOnlyRepositoryBuilder HomeHasCleaningScheduleCreated(long homeId)
        {
            _repository.Setup(x => x.HomeHasCleaningScheduleCreated(homeId)).ReturnsAsync(true);
            return this;
        }

        public ICleaningScheduleReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}

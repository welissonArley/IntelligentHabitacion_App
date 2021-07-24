using Homuai.App.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.HistoryOfTheDay
{
    public interface IHistoryOfTheDayUseCase
    {
        Task<IList<DetailsTaskCleanedOnDayModelGroup>> Execute(DateTime date, string room = null);
    }
}

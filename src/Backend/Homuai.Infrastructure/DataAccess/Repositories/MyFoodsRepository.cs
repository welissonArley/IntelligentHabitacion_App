using Homuai.Domain.Entity;
using Homuai.Domain.Repository.MyFoods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Infrastructure.DataAccess.Repositories
{
    public class MyFoodsRepository : IMyFoodsReadOnlyRepository, IMyFoodsWriteOnlyRepository, IMyFoodsUpdateOnlyRepository
    {
        private readonly HomuaiContext _context;

        public MyFoodsRepository(HomuaiContext context) => _context = context;

        public async Task<IList<MyFood>> GetByUserId(long userId)
        {
            return await _context.MyFoods
                .AsNoTracking()
                .Where(c => c.UserId == userId && c.Active).ToListAsync();
        }

        public async Task Add(MyFood myFood)
        {
            await _context.MyFoods.AddAsync(myFood);
        }

        public async Task<MyFood> GetById(long myFoodId, long userId)
        {
            return await _context.MyFoods
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Active && c.Id == myFoodId);
        }

        public void Delete(MyFood myFood)
        {
            _context.MyFoods.Remove(myFood);
        }

        public async Task ChangeAmount(long myFoodId, decimal amount)
        {
            var foodModel = await _context.MyFoods.FirstOrDefaultAsync(c => c.Id == myFoodId);
            foodModel.Quantity = amount;

            _context.Update(foodModel);
        }

        public async Task<MyFood> GetById_Update(long myFoodId, long userId)
        {
            return await _context.MyFoods
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Active && c.Id == myFoodId);
        }

        public void Update(MyFood myFood)
        {
            _context.MyFoods.Update(myFood);
        }

        public async Task<IList<MyFood>> GetExpiredOrCloseToDueDate()
        {
            var today = DateTime.UtcNow.Date;

            var list = await _context.MyFoods.AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.DueDate.HasValue && c.DueDate.Value.Date <= today.AddDays(7)).ToListAsync();

            return list.Where(c => (c.DueDate.Value - today).TotalDays == 7
                || (c.DueDate.Value - today).TotalDays == 3
                || (c.DueDate.Value - today).TotalDays <= 1).ToList();
        }

        public void DeleteAllFromTheUser(long userId)
        {
            var list = _context.MyFoods.Where(c => c.UserId == userId);

            _context.MyFoods.RemoveRange(list);
        }
    }
}

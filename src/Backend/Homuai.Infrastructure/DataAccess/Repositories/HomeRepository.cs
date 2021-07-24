using Homuai.Domain.Entity;
using Homuai.Domain.Repository.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Homuai.Infrastructure.DataAccess.Repositories
{
    public class HomeRepository : IHomeWriteOnlyRepository, IHomeUpdateOnlyRepository
    {
        private readonly HomuaiContext _context;

        public HomeRepository(HomuaiContext context) => _context = context;

        public async Task Add(User administrator, Home home)
        {
            var userToBeAdministrator = await _context.Users
                .Include(c => c.Phonenumbers)
                .Include(c => c.EmergencyContacts)
                .FirstOrDefaultAsync(c => c.Id == administrator.Id && c.Active);

            home.AdministratorId = administrator.Id;
            userToBeAdministrator.HomeAssociation = new HomeAssociation
            {
                JoinedOn = DateTime.UtcNow,
                Home = home,
                UserIdentity = administrator.Id
            };

            _context.Users.Update(userToBeAdministrator);
        }

        public async Task<Home> GetById_Update(long homeId)
        {
            return await _context.Homes
                .Include(c => c.Rooms)
                .FirstOrDefaultAsync(c => c.Id == homeId);
        }

        public void Update(Home home)
        {
            _context.Homes.Update(home);
        }
    }
}

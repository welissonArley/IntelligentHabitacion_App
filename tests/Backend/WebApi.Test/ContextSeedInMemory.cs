using Homuai.Infrastructure.DataAccess;
using WebApi.Test.Builder;

namespace WebApi.Test
{
    public class ContextSeedInMemory
    {
        public static void Seed(HomuaiContext context)
        {
            EntityBuilder.Start();
            
            AddUserWithoutHome(context);

            context.SaveChanges();
        }

        private static void AddUserWithoutHome(HomuaiContext context)
        {
            context.Users.Add(EntityBuilder.UserWithoutHome);
            context.Tokens.Add(new Homuai.Domain.Entity.Token
            {
                Id = 1,
                UserId = EntityBuilder.UserWithoutHome.Id,
                Value = EntityBuilder.Token_UserWithoutHome
            });
        }
    }
}

using Useful.ToTests.Builders.TokenController;
using WebApi.Test.Builder.Entities;

namespace WebApi.Test.Builder
{
    public static class EntityBuilder
    {
        public static string Token_UserWithoutHome { get; set; }

        public static Homuai.Domain.Entity.User UserWithoutHome { get; set; }

        public static void Start()
        {
            var tokenController = TokenControllerBuilder.Instance().Build();

            if (UserWithoutHome == null)
            {
                UserWithoutHome = UserBuilder.Instance().WithoutHome();
                Token_UserWithoutHome = tokenController.Generate(UserWithoutHome.Email);
            }
        }
    }
}

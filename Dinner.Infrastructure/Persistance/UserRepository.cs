using Dinner.Application.Common.Interfaces.Persistance;
using Dinner.Domain.Entities;


namespace Dinner.Infrastructure.Persistance
{
    public  class UserRepository : IUserRepository
    {
        private static readonly List<User> users = new();

        public void Add(User user)
        {
            users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return users.SingleOrDefault(user => user.Email == email);
        }

    }
}

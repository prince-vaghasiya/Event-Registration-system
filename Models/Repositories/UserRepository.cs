using EventRegistrationSystem.Models;

namespace EventRegistrationSystem.Models.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public User Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
        public User Delete(int id)
        {
            User user = context.Users.Find(id)!;
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user!;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users;
        }

        public User GetUser(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }
        public User GetUserByUsername(string username)
        {
            return context.Users.FirstOrDefault(x => x.UserName == username);
        }
        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(x => x.Email == email);
        }

        public User Update(User user)
        {
            var UUser = context.Users.Attach(user);
            UUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return user;
        }
    }
}

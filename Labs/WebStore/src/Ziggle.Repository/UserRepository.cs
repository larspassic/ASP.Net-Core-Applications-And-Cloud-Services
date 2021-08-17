using System.Linq;

namespace Ziggle.Repository
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserRepository : IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.User
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserHashedPassword == password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        public UserModel Register(string email, string password)
        {
            //If the email address that was passed in to the register method was found by checking the database, store it in userFound
            var userFound = DatabaseAccessor.Instance.User.FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower());

            //If userFound is not null, exit the register method to avoid duplicate registration
            if (userFound != null)
            {
                return null;
            }

            
            var user = DatabaseAccessor.Instance.User
                    .Add(new Ziggle.ProductDatabase.User
                    {
                        UserEmail = email,
                        UserHashedPassword = password
                    });

            DatabaseAccessor.Instance.SaveChanges();

            return new UserModel { Id = user.Entity.UserId, Name = user.Entity.UserEmail };
        }
    }
}
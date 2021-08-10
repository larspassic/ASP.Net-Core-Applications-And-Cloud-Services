//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Website
//{
//    public interface IUserRepository
//    {
//        UserModel LogIn(string email, string password);
//        UserModel Register(string email, string password);
//    }


//    public class UserModel
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }

//    }

//    //This is a class that derives from an interface
//    public class UserRepository : IUserRepository
//    {
//        //This is a method
//        public UserModel LogIn(string email, string password)
//        {
//            var user = DatabaseAccessor.Instance.user.FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower() && t.UserHashedPassword == password);

//            if (user == null)
//            {
//                return null;
//            }

//            return new UserModel { Id = user.UserId, Name = user.UserEmail };
//        }

//        //This is a method
//        public UserModel Register(string email, string password)
//        {
//            var user = DatabaseAccessor.Instance.User.Add(new Database.User
//            {
//                UserEmail = email,
//                UserHashedPassword = password
//            });

//            DatabaseAccessor.Instance.SaveChanges();

//            return new UserModel { Id = user.Entity.UserId, Name = user.Entity.UserEmail };
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website
{
    public interface IEnrollRepository
    {
        EnrollModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        EnrollModel[] GetAll(int userId);
    }

    //EnrollModel is the object that we will use to denote a user/class registration relationship.
    public class EnrollModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
    }

    public class EnrollRepository : IEnrollRepository
    {
        public EnrollModel Add(int userId, int classId)
        {
            var item = DatabaseAccessor.Instance.UserClass.Add(
                new Website.Db.UserClass
                {
                    UserId = userId,
                    ClassId = classId
                });

            DatabaseAccessor.Instance.SaveChanges();

            return new EnrollModel
            {
                UserId = item.Entity.UserId,
                ClassId = item.Entity.ClassId
            };
        }

        public EnrollModel[] GetAll(int userId)
        {
            //UserClass is the name of the table that we are pulling from
            var items = DatabaseAccessor.Instance.UserClass
                .Where(t => t.UserId == userId)
                .Select(t => new EnrollModel
                {
                    UserId = t.UserId,
                    ClassId = t.ClassId
                }).ToArray();

            return items;
        }

        public void zz(){

            //get a list of all the classes and put it into a map


            }
        

        public bool Remove(int userId, int classId)
        {

            //For items, use the database accessor, to reach in to the UserClass table
            var items = DatabaseAccessor.Instance.UserClass

                //And in the table, look in the UserId column and find a match for the userId that was passed in as a parameter.
                //And look in the ClassId column and find a match for the classId that was passed in as a parameter.
                .Where(t => t.UserId == userId && t.ClassId == classId);

            if (items.Count() == 0)
            {
                return false;
            }

            //Actually process the remove from the table
            DatabaseAccessor.Instance.UserClass.Remove(items.First());

            //Save the changes to the table
            DatabaseAccessor.Instance.SaveChanges();

            return true;
        }
    }

}

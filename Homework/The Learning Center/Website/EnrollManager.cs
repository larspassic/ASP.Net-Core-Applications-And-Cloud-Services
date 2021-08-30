using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website
{
    public interface IEnrollManager
    {
        EnrollModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        EnrollModel[] GetAll(int userId);
    }

    public class EnrollManager : IEnrollManager
    {
        private readonly IEnrollRepository enrollRepository;
        
        public EnrollManager(IEnrollRepository enrollRepository)
        {
            this.enrollRepository = enrollRepository;
        }
        
        public EnrollModel Add(int userId, int classId)
        {
            var item = enrollRepository.Add(userId, classId);

            return new EnrollModel
            {
                UserId = item.UserId,
                ClassId = item.ClassId
            };
        }

        public EnrollModel[] GetAll(int userId)
        {
            //Calling the method from the EnrollRepository
            var items = enrollRepository.GetAll(userId)
                .Select(t => new EnrollModel
                {
                    UserId = t.UserId,
                    ClassId = t.ClassId
                }).ToArray();

            return items;
        }

        public bool Remove(int userId, int classId)
        {
            return enrollRepository.Remove(userId, classId);
        }
    }
}

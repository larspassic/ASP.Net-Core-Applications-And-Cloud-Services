using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website
{
    public interface IClassRepository
    {
        ClassModel[] GetAllClasses();
    }



    //Defines the ClassModel
    public class ClassModel
    {

        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }


    //Convert SQL objects in to Csharp objects - maybe
    public class ClassRepository : IClassRepository
    {
        public ClassModel[] GetAllClasses()
        {
            var classes = DatabaseAccessor.Instance.Class;

            return classes
                    .Select(t => new ClassModel
                    {
                        ClassId = t.ClassId,
                        ClassName = t.ClassName,
                        ClassPrice = t.ClassPrice,
                        ClassDescription = t.ClassDescription
                    })
                    .ToArray();
        }
    }
}

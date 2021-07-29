using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEF
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database accessor. This opens the database automatically
            var school = new SchoolEntities();

            //This accesses the ClassMaster table
            //This is a class roster
            Console.WriteLine($"########## Class Roster ##########\n\n");
            foreach(var classMaster in school.ClassMasters)
            {
                Console.WriteLine($"ClassId: {classMaster.ClassId}\nClassName: {classMaster.ClassName}\nClass Description: {classMaster.ClassDescription}\nClass Price: {classMaster.ClassPrice}");
                Console.Write($"Registered students: ");
                //Print everyone who is registered for that class
                foreach (var user in classMaster.Users)
                {
                    Console.Write($"{user.UserName} ");
                }
                Console.WriteLine("\n\n");
            }

            //Exercise
            //This accesses the Users table
            //This lists out the students and which classes they are registered for
            Console.WriteLine($"########## Student's Classes ##########\n\n");
            foreach (var student in school.Users)
            {
                //List the user name
                Console.WriteLine($"{student.UserName}\n");



                //List each individual class for that student by looking in to student.ClassMasters
                Console.WriteLine($"Easy method of listing classes out for a student");
                foreach (var individualClass in student.ClassMasters)
                {
                    Console.WriteLine($"ClassID: {individualClass.ClassId} ClassName: {individualClass.ClassName}");
                }

                Console.WriteLine();

                //Alternative method
                //Use the SQL stored procedure!!
                var classes = school.RetrieveClassesForStudent(student.UserId);
                Console.WriteLine($"Alternative method of listing classes out for a student");
                foreach (var item in classes)
                {
                    var classMaster = school.ClassMasters.First(t => t.ClassId == item.ClassId);
                    Console.WriteLine($"ClassID: {classMaster.ClassId} ClassName: {classMaster.ClassName}");
                }

                //Extra space
                Console.WriteLine();
            }

            Console.WriteLine("Done.");

            Console.ReadLine();


        }
    }
}

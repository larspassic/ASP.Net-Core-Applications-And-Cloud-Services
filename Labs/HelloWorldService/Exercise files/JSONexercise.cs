using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldService
{
    public class Person
    {
        public string firstName;
        public string lastName;
        public bool isAlive;
        public phoneNumber[] phoneNumbers;
        public child[] children;
        public Person spouse;
    }

    public class child
    {
        public string firstName;
        public string lastName;
        public int age;
    }

    public class phoneNumber
    {
        public string type;
        public string number;
    }
}

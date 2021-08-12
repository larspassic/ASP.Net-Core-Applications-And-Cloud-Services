using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Db;

namespace Website
{
    public class DatabaseAccessor
    {
        //I think this is the default constructor
        static DatabaseAccessor()
        {
            Instance = new minicstructorContext();
        }

        public static minicstructorContext Instance { get; private set; }
    }
}

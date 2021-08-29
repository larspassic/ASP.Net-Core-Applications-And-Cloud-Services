﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website
{
    
    //Interface 
    public interface IClassManager
    {
        ClassManagerModel[] GetAllClasses();
    }

    
    //Object definition
    public class ClassManagerModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }

    
    //Class
    public class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        
        //Constructor
        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        
        //Method
        public ClassManagerModel[] GetAllClasses()
        {
            return classRepository.GetAllClasses().Select(t =>
                            new ClassManagerModel
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
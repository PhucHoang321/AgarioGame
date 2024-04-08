using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TowardAgarioStepTwo {

    
    internal class Student : Person {
        public float GPA {get; private set; }
        public Student(string name,int id, float gpa) : base(name) {
             GPA = gpa; 
            

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TowardAgarioStepTwo {
    [JsonDerivedType(typeof(Person), typeDiscriminator: "Person")]
    [JsonDerivedType(typeof(Student), typeDiscriminator: "Student")]

    internal class Person {
        public int ID { get; protected set; }
     
        public string Name { get;  protected set; }
        private static int latestID = 3;
        public Person(string name) {
            ID = latestID++;
          
            Name = name;
            
        }



        
    }
}

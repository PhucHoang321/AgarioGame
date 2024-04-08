using System;
using System.Text.Json;

namespace TowardAgarioStepTwo {
    class Program {
        static void Main(string[] args) {
            //  string name = "Aun";
            //  float GPA = 4f;
            //  Person p = new Person(name, GPA);

            //  var message = JsonSerializer.Serialize<Person>(p);
            //  Console.WriteLine(message);

            //  Person? temp = null;
            //   temp  ??= JsonSerializer.Deserialize<Person>(message);


            //List<Person> list = new List<Person>();
            //  list.Add(new Person("Jim", 3.2f));
            //  list.Add(new Person("Dav", 3.3f));
            //  list.Add(new Person("Erin", 3.6f));
            //  list.Add(new Person("Mary", 3.8f));
            //  list.Add(new Person("Pat", 3.5f));

            //  //“Jim”, “Dav”, “Erin”, “Mary”, “Pat”, with GPAs from 3.0 to 3.8.
            //  var tempList = JsonSerializer.Serialize<List<Person>>(list, new JsonSerializerOptions { WriteIndented = true })   ;

            //  Console.WriteLine(tempList);

            Person studentA = new Student("Aun", 2 ,4.0f);
            var messagae = JsonSerializer.Serialize<Person>(studentA); 
            Console.WriteLine(messagae);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinder_DZ
{
    internal class Person
    {
        public string Name { get; set; }

        public bool? Sex { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }


        public Person()
        {

        }

        public Person(string name, char sex, int age, string country)
        {
            Name = name;
            Age = age;
            Country = country;
            Sex = ReadSex(sex);
        }


        static bool? ReadSex(char sex)
        {
            if (sex == 'f') { return true; }
            else if (sex == 'm') { return false; }
            else { return null; }
        }

        static string PrintSex(bool? sex)
        {
            if (sex == true) { return "f"; }
            else if (sex == false ) { return "m"; }
            else { return "-"; }
        }



        public void PrintInformation()
        {
            Console.WriteLine($"Name: {Name}    Age: {Age}    Country: {Country}    Sex: {PrintSex(Sex)}");
            Console.WriteLine();
        }

        public void PrintShortInformation()
        {
            Console.WriteLine($"Name: {Name}    Age: {Age}");
        }
    }
}

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
        
        public int Height { get; set; }
        
        public int Weight { get; set; }
        
        public bool? SmokeAt { get; set; }
        
        public bool? AlcAt { get; set; }
        
        public List<MusicGenre> MusicPref { get; set; }
        public string Country { get; set; }


        public Person()
        {

        }

        public Person(string name, char sex, int age, int height, int weight, bool? smokeat, bool? alcat, List<MusicGenre> mpref, string country)
        {
            Name = name;
            Age = age;
            Height = height;
            Weight = weight;
            SmokeAt = smokeat;
            AlcAt = alcat;
            MusicPref = mpref;
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


        public void PresentMusicPrefs()
        {
            Console.Write("Music preferencies: ");
            foreach (var pref in MusicPref)
            {
                if (pref == MusicGenre.HipHop)
                {
                    Console.Write("Hip-Hop ");
                }
                else if (pref == MusicGenre.Rap)
                {
                    Console.Write("Rap ");
                }
                else if (pref == MusicGenre.Rock)
                {
                    Console.Write("Rock");
                }
            }
        }
        public void PrintInformation()
        {
            Console.WriteLine($"Name: {Name}    Age: {Age}    Height: {Height}    Weight: {Weight}    Country: {Country}    Sex: {PrintSex(Sex)}");
            if (MusicPref.Count != 0)
            {
                PresentMusicPrefs();
            }
            Console.WriteLine();
        }

        public void PrintShortInformation()
        {
            Console.WriteLine($"Name: {Name}    Age: {Age}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 Задать через enum список атрибутов, таких как любимые жанры в музыке, в кино.
 Отношение к алкоголю, Курению. Рост, вес. 
 */


namespace Tinder_DZ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numAdditionalCandidates = 2;


            void InitializingInterface()
            {
                List<Person> candidates = PredefinedCandidates().Union(AdditionalCandidates()).ToList();

                candidates = ShuffleCandidates(candidates);

                ShowingCandidates(candidates);
            }


            List<Person> PredefinedCandidates()
            {
                List<Person> predefinedCandidates = new List<Person>()
                {
                    new Person("Dmitry", 'm', 20, "Russia"),
                    new Person("Ann", 'f', 31, "UK"),
                    new Person("Genry", 'm', 18, "Germany")
                };

                return predefinedCandidates;
            }


            List<Person> AdditionalCandidates()
            {
                List<Person> additionalCandidates = new List<Person>();

                for (int i = 0; i < numAdditionalCandidates; i++)
                {
                    additionalCandidates.Add(ReadCandidate(i));
                }

                return additionalCandidates;
            }

            Person ReadCandidate(int number)
            {
                Console.WriteLine($"Information about candidate number {number + 1}:");


                Console.WriteLine("Enter the name of the candidate:");

                var name = Console.ReadLine();


                Console.WriteLine("Enter the sex of the candidate (m, f, -):");

                var sex = char.Parse(Console.ReadLine());


                Console.WriteLine("Enter the age of the candidate:");

                var age = int.Parse(Console.ReadLine());


                Console.WriteLine("Enter the country of the candidate:");

                var country = Console.ReadLine();


                Console.WriteLine();
                Console.WriteLine();


                return new Person(name, sex, age, country);
            }



            List<Person> ShuffleCandidates(List<Person> candidates)
            {
                Random rand = new Random();

                for (int i = candidates.Count - 1; i >= 1; i--)
                {
                    int j = rand.Next(i + 1);

                    var tmp = candidates[j];
                    candidates[j] = candidates[i];
                    candidates[i] = tmp;
                }

                return candidates;
            }



            void ShowingCandidates(List<Person> candidates)
            {
                Console.WriteLine("We are starting to show candidates for you");
                Console.WriteLine();

                bool? sexualPreferences = SexualPreferences();

                List<Person> chosenCandidates;

                if (sexualPreferences != null)
                {
                    chosenCandidates = candidates
                        .Where(c => (c.Sex == true || c.Sex == false) && c.Sex == sexualPreferences)
                        .ToList();
                }

                else
                {
                    chosenCandidates = candidates
                        .Where(c => true)
                        .ToList();
                }

                EvaluateCandidates(chosenCandidates, out List<Person> acceptedCandidates, out List<Person> rejectedCandidates);

                ShowAnalytics(acceptedCandidates, rejectedCandidates);
            }


            bool? SexualPreferences()
            {                
                Console.WriteLine("Press '->' for showing men");
                Console.WriteLine("Press '<-' for showing women");
                Console.WriteLine("Press any other button for showing all");

                var choice = Console.ReadKey(true).Key;

                Console.WriteLine();
                Console.WriteLine();

                if (choice == ConsoleKey.LeftArrow) { return true; }
                else if (choice == ConsoleKey.RightArrow) { return false; }
                else { return null; }
            }



            void EvaluateCandidates(List<Person> candidates, out List<Person> acceptedCandidates, out List<Person> rejectedCandidates)
            {
                Console.WriteLine("Candidates are shown below:");
                Console.WriteLine("Press '->' if you like the candidate");
                Console.WriteLine("Press '<-' if you dont like the candidate");

                Console.WriteLine();

                acceptedCandidates = new List<Person>();
                rejectedCandidates = new List<Person>();

                foreach (var candidate in candidates)
                {
                    candidate.PrintInformation();

                    if (Evaluation()) { acceptedCandidates.Add(candidate); }
                    else { rejectedCandidates.Add(candidate); }
                }

                Console.WriteLine();
            }

            bool Evaluation()
            {
                var choice = Console.ReadKey(true).Key;

                if (choice == ConsoleKey.RightArrow) { return true; }
                else { return false; }
            }



            void ShowAnalytics(List<Person> acceptedCandidates, List<Person> rejectedCandidates)
            {
                Console.WriteLine("Results of your choice:");
                Console.WriteLine();

                PrintAllSelectedCandidatesInformation(acceptedCandidates, "None of the candidates are liked", "Candidates which you liked");

                Console.WriteLine();

                PrintAllSelectedCandidatesInformation(rejectedCandidates, "None of the candidates are rejected", "Candidates which you rejected");
            }

            void PrintAllSelectedCandidatesInformation(List<Person> candidates, string textIfEmpty, string textIfNotEmpty)
            {
                if (candidates.Any())
                {
                    Console.WriteLine(textIfNotEmpty);

                    foreach (var candidate in candidates)
                    {
                        candidate.PrintShortInformation();
                    }
                }

                else
                {
                    Console.WriteLine(textIfEmpty);
                }
            }



            InitializingInterface();
            Console.ReadKey();
        }
    }
}

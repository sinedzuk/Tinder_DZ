using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 Задать через enum список атрибутов, таких как любимые жанры в музыке.
 Отношение к алкоголю, Курению. Рост, вес. 
 */


namespace Tinder_DZ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numAdditionalCandidates = 2;
            var userprefs = new UserPreferences((0,150), (0,500), (0,500), null, null, new List<MusicGenre>());


            void InitializingInterface()
            {
                List<Person> candidates = PredefinedCandidates().Union(AdditionalCandidates()).ToList();
                ChoosePreferences();

                candidates = ShuffleCandidates(candidates);

                ShowingCandidates(candidates);
            }


            List<Person> PredefinedCandidates()
            {
                List<Person> predefinedCandidates = new List<Person>()
                {
                    new Person("Dmitry", 'm', 20, 180, 75, null, null, new List<MusicGenre>() {MusicGenre.HipHop}, "Russia"),
                    new Person("Ann", 'f', 31, 160, 53, null, null, new List<MusicGenre>(), "UK"),
                    new Person("Genry", 'm', 18, 185, 80, null, null, new List<MusicGenre>(), "Germany")
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
                
                Console.WriteLine("Enter the height of the candidate:");

                var height = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the weight of the candidate:");

                var weight = int.Parse(Console.ReadLine());

                var smokeat = HabitPref("курению");

                var alcat = HabitPref("алкоголю");

                var mpref = ChooseMusicPrefs();

                Console.WriteLine("Enter the country of the candidate:");

                var country = Console.ReadLine();


                Console.WriteLine();
                Console.WriteLine();


                return new Person(name, sex, age, height, weight, smokeat, alcat, mpref ,country);
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
                        .Where(c => (c.Sex == true || c.Sex == false) && (c.Sex == sexualPreferences))
                        .Where(c => (userprefs.Age.Item1 <= c.Age & c.Age <= userprefs.Age.Item2))
                        .Where(c => (userprefs.Height.Item1 <= c.Height & c.Height <= userprefs.Height.Item2))
                        .Where(c => (userprefs.Weight.Item1 <= c.Weight & c.Weight <= userprefs.Weight.Item2))
                        .Where(c => (c.SmokeAt == userprefs.SmokeAt) || (userprefs.SmokeAt == null) || (userprefs.SmokeAt == true && c.SmokeAt == null))
                        .Where(c => (c.AlcAt == userprefs.AlcAt) || (userprefs.AlcAt == null) || (userprefs.AlcAt == true && c.AlcAt == null))
                        .Where(c => userprefs.MusicPref.Any(x => c.MusicPref.Any(y => y == x)) || userprefs.MusicPref.Count == 0)
                        .ToList();
                }
//(nums1.Any(x => nums2.Any(y => y == x)))
                else
                {
                    chosenCandidates = candidates
                        .Where(c => (userprefs.Age.Item1 <= c.Age & c.Age <= userprefs.Age.Item2))
                        .Where(c => (userprefs.Height.Item1 <= c.Height & c.Height <= userprefs.Height.Item2))
                        .Where(c => (userprefs.Weight.Item1 <= c.Weight & c.Weight <= userprefs.Weight.Item2))
                        .Where(c => (c.SmokeAt == userprefs.SmokeAt) || (userprefs.SmokeAt == null) || (userprefs.SmokeAt == true && c.SmokeAt == null))
                        .Where(c => (c.AlcAt == userprefs.AlcAt) || (userprefs.AlcAt == null) || (userprefs.AlcAt == true && c.AlcAt == null))
                        .Where(c => userprefs.MusicPref.Any(x => c.MusicPref.Any(y => y == x)) || userprefs.MusicPref.Count == 0)
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

            (int min, int max) ChooseMinMax(string parameter)
            {
                Console.WriteLine($"Введите минимальное желмаемое значение {parameter}");
                var min = int.Parse(Console.ReadLine());
                
                Console.WriteLine($"Введите максимальное желаемое значение {parameter}");
                var max = int.Parse(Console.ReadLine());

                return (min, max);

            }

            bool? HabitPref(string habit)
            {
                bool? pref = null;
                Console.WriteLine($"Выберите отношение к {habit} " +
                                  "\n1 - Положительное" +
                                  "\n2 - Отрицательное" +
                                  "\n3 - Нейтральное");
                var userchoice = int.Parse(Console.ReadLine());
                if (userchoice == 1)
                {
                    pref = true;
                }
                else if (userchoice == 2)
                {
                    pref = false;
                }
                else if (userchoice == 3)
                {
                    pref = null;
                }

                return pref;
            }



            List<MusicGenre> ChooseMusicPrefs()
            {
                var userlist = new List<MusicGenre>();

                while (true)
                {
                    Console.WriteLine("Выберите жанр который вы хотите добавить в свои предпочтения" +
                                      "\n1 - Хип-хоп" +
                                      "\n2 - Рэп" +
                                      "\n3 - Рок" +
                                      "\n4 - Закончить выбор");
                    
                    var userchoice = int.Parse(Console.ReadLine());
                    
                    if (userchoice == 1)
                    {
                        userlist.Add(MusicGenre.HipHop);
                    }
                    else if (userchoice == 2)
                    {
                        userlist.Add(MusicGenre.Rap);
                    }
                    else if (userchoice == 3)
                    {
                        userlist.Add(MusicGenre.Rock);
                    }
                    else if (userchoice == 4)
                    {
                        return userlist;
                    }
                }
            }
            


            void ChoosePreferences()
            {
                while (true)
                {
                    Console.WriteLine("Выберите какие предпочтения по кандидатам вы хотите задать:" +
                                      "\n1 - Минамальный и максимальный возраст" +
                                      "\n2 - Минимальный и максимальный рост" +
                                      "\n3 - Минимальный и максимальный вес" +
                                      "\n4 - Отношение к курению" +
                                      "\n5 - Отношение к алкоголю" +
                                      "\n6 - Предпочитаемые жанры в музыке" +
                                      "\n7 - Закончить выбор предпочтений");
                    var userchoice = int.Parse(Console.ReadLine());
                    if (userchoice == 1)
                    {
                        userprefs.Age = ChooseMinMax("возраста");
                    }
                    else if (userchoice == 2)
                    {
                        userprefs.Height = ChooseMinMax("роста");
                    }
                    else if (userchoice == 3)
                    {
                        userprefs.Weight = ChooseMinMax("веса");
                    }
                    else if (userchoice == 4)
                    {
                        userprefs.SmokeAt = HabitPref("курению");
                    }
                    else if (userchoice == 5)
                    {
                        userprefs.AlcAt = HabitPref("алкоголю");
                    }
                    else if (userchoice == 6)
                    {
                        userprefs.MusicPref = ChooseMusicPrefs();
                    }
                    else if (userchoice == 7)
                    {
                        return;
                    }
                }
            }



            InitializingInterface();
            Console.ReadKey();
        }
    }
}

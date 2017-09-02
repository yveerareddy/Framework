using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Today.AddDays(7).Date);
            Console.ReadLine();
            while (true)
            {
                Task.Run(ExampleAsync);

                string result = Console.ReadLine();
                Console.WriteLine("Running in Main ....");
            }

            //TaskExample();
            //ObservableExample();


        }

        static async Task ExampleAsync()
        {
            int result = await  Allocate();
            Console.WriteLine("Compuete {0} ", result);
            
        }

        


        static Task<int> Allocate()
        {
            TaskCompletionSource<int> tcs=new TaskCompletionSource<int>();

            // Compute total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 2000; i++)
                {
                    string value = i.ToString();
                    if (value == null)
                    {
                        tcs.SetResult(0);
                        return tcs.Task;
                    }
                    size += value.Length;
                }
            }
            tcs.SetResult(size);
            return  tcs.Task;
        }


        static void ObservableExample()
        {
            var o = Observable.Start(() =>
            {
                Console.WriteLine("From background thread");
                Console.WriteLine("Calculating");
                Thread.Sleep(1000);
                Console.WriteLine("Background thread completed");
            }).Finally(() =>
            {
                Console.WriteLine("Main thread completed");
            });

            Console.WriteLine("From Main  thread");

            o.Wait();
            
        }

        static void TaskExample()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("From background thread");
                Console.WriteLine("Calculating");
                Thread.Sleep(1000);
                Console.WriteLine("Background thread completed");
            });
            
            Console.WriteLine("From Main  thread");
            task.Wait();
        }


        public static void GroupByEx4()
        {
            // Create a list of pets.
            List<Pet> petsList =
                new List<Pet>{ new Pet { Name="Barley", Age=8.3,Grade = "1"},
                       new Pet { Name="Boots", Age=4.9,Grade = "1" },
                       new Pet { Name="Whiskers", Age=1.5,Grade = "2" },
                       new Pet { Name="Daisy", Age=4.3 ,Grade = "2"} };



            var resultList = new List<Pet>();
            foreach (var byGrade in petsList.GroupBy(x=>x.Grade))
            {

                foreach (var byAge in byGrade.GroupBy( pet=>Math.Floor(pet.Age)))
                {
                    var newPet = new Pet()
                    {
                        Grade = byGrade.Key,
                        Age = byAge.Average(x => x.Age),
                    };

                }
                
            }
            // Group Pet.Age values by the Math.Floor of the age.
            // Then project an anonymous type from each group
            // that consists of the key, the count of the group's
            // elements, and the minimum and maximum age in the group.
            var query = petsList.GroupBy(
                pet => Math.Floor(pet.Age),
                pet => pet,
                (baseAge, ages) => new
                {
                    Key = baseAge,

                    Count = ages.Count(),
                    Min = ages.Min(),
                    Max = ages.Max()
                });

            // Iterate over each anonymous type.
            foreach (var result in query)
            {
                Console.WriteLine("\nAge group: " + result.Key);
                Console.WriteLine("Number of pets in this age group: " + result.Count);
                Console.WriteLine("Minimum age: " + result.Min);
                Console.WriteLine("Maximum age: " + result.Max);
            }

            /*  This code produces the following output:

                Age group: 8
                Number of pets in this age group: 1
                Minimum age: 8.3
                Maximum age: 8.3

                Age group: 4
                Number of pets in this age group: 2
                Minimum age: 4.3
                Maximum age: 4.9

                Age group: 1
                Number of pets in this age group: 1
                Minimum age: 1.5
                Maximum age: 1.5
            */
        }

        static void DateFormattingExample()
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Today.AddDays(13).AddHours(12);

            TimeSpan diff = date2 - date1;

            var value = Math.Round(diff.TotalDays, 1);

            if (value % 1 == 0)
            {
                Console.WriteLine("{0:#} Days", value);
            }
            else
            {
                Console.WriteLine("{0:N1} Days", value);
            }
        }
    }


    public class Pet
    {
        public string Name { get; set; }
        public double Age { get; set; }
        public string Grade { get; set; }
        public PetExtraAttributes Attributes { get; set; }

        public Pet ShallowCopy()
        {
            return  this.MemberwiseClone() as Pet;
        }

        public Pet DeepCopy()
        {
            var newPet = this.MemberwiseClone() as Pet;
            var newAttr = new PetExtraAttributes();
            newPet.Attributes = newAttr;

            return newPet;
        }
    }

    public class PetExtraAttributes
    {
        public string Color { get; set; }
        public string Gender { get; set; }
    }

    

}

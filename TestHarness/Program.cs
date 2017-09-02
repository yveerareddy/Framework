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


  

    

}

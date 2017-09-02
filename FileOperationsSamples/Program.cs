using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileOperationsSamples
{
    class Program
    {

        static void Main(string[] args)
        {
            
            CreateAppManifest();

        }

        private static string GetJSessionID(string cookieHeader)
        {
           var setcookies= cookieHeader.Split(';');

            foreach (var setcooky in setcookies)
            {
                if (setcooky.StartsWith("JSESSIONID="))
                    return setcooky;

            }

            return string.Empty;
        }



        static void CreateAppManifest()
        {
            string sourcePath =
               @"C:\Projects\VC-Synergy\synergy\VirtualArrival\Bp.Owb.VoyageCalculator\bin\Debug";

            ////get list of files , read them into memory compute their size and hash code write to a file

            if (Directory.Exists(sourcePath) == false) return;

            var files = Directory.GetFiles(sourcePath, "*.dll");


            var fileNames = new List<string>();
            foreach (var file in files)
            {
                fileNames.Add($"<File>{Path.GetFileName(file)}</File>");
            }
            File.WriteAllLines(Path.Combine(sourcePath, "manifest10.txt"), fileNames);


        }

        private static void  StartOperation(object i)
        {

            
            Stopwatch sw=new Stopwatch();
            sw.Start();
            //prepare param data
     
            sw.Stop();
            Console.WriteLine($"Running from thread {i} , Total time taken {sw.ElapsedMilliseconds/1000}");
            
            //Task.Delay(TimeSpan.FromSeconds(20));
            //Thread.Sleep(TimeSpan.FromSeconds(20));

            // Console.WriteLine($"Completed from thread {i}");
        }

        static void TakeBase(BasePerson b)
        {
            var c = b as Person;
        }

        static void TakeDerive(Person p)
        {
            
        }

        //private static async  Task StartOperation(object i)
        //{
        //    Console.WriteLine($"Running from thread {i}");
        //    //Task.Delay(TimeSpan.FromSeconds(20));
        //    await Task.Delay(TimeSpan.FromMinutes(1));
        //    //Thread.Sleep(TimeSpan.FromSeconds(20));

        //    Console.WriteLine($"Completed from thread {i}");
        //}
    }

    public class Person:BasePerson
    {
        public string Name { get; set; }
    }

    public class BasePerson
    {
        public string Color { get; set; }
    }
}

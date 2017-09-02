using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSamples
{
    class Program
    {
        private static List<Product> _products;
        private static List<Customer> _customers;
        private static CityComparer _cityComparer=new CityComparer();

        static void Main(string[] args)
        {
            var repository=new DataRepository();
            var customers = _customers= repository.GetCustomers();
            var products = _products= repository.GetProducts();

            string customercount = $"No of customers {customers.Count}";
            string productcount = $"No of products {products.Count}";

            Console.WriteLine(customercount);
            Console.WriteLine(productcount);

            Linq2();

            Console.ReadLine();

        }

        public static void Linq2()
        {
            var outofstockProducts = _products.Where(p => p.UnitsInStock == 0);
            Console.WriteLine($"Sold out Products");
            PrintProducts(outofstockProducts);

            var increaseStock = from outofstockProduct in outofstockProducts
                                select  outofstockProduct.UnitsInStock= + 1;

            var outofstockProducts2 = _products.Where(p => p.UnitsInStock == 0);

            PrintProducts(outofstockProducts2);
        }

        public static void Linq3()
        {
            var result = from p in _products
                where p.UnitsInStock > 0 && p.UnitPrice > 3
                select p;
        }



        static void Linq4()
        {
            var result = from c in _customers
                where c.Region == "WA"
                select c.Orders;

            var enumerable = result as Order[][] ?? result.ToArray();
            
            foreach (var orderse in enumerable)
            {
                PrintOrders(orderse);
            }

       

            var result2 = from c in _customers
                            from o in c.Orders
                          where c.Region == "WA"
                            select o;

            var orders = result2 as IList<Order> ?? result2.ToList();
            
            PrintOrders(orders);

        }

        static void Linq5()
        {
            var customers = _customers.Where((c, index) => c.CompanyName.Length < index);
            
            PrintCustomers(customers);
            
        }
        public static void PrintProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }

        static void Linq10()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var digitOddEvens = from n in numbers
                select new {Text = strings[n], Even = n%2 == 0};

        }

        static void Linq14()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var pairs =
                from a in numbersA
                from b in numbersB
                where a < b
                orderby a
                select new { a, b };

            Console.WriteLine("Pairs where a < b:");
            foreach (var pair in pairs)
            {
                Console.WriteLine("{0} is less than {1}", pair.a, pair.b);
            }
        }

        static void Linq15()
        {
            
            var customerOrders =
                _customers.SelectMany(
                    (cust, custIndex) =>
                            cust.Orders.Select(o => ($"Customer {custIndex+1} has order {o.OrderID}")));

            _customers.OrderBy(c => c.City, _cityComparer);

        }

        static void Linq16()
        {

            var customerGroups = from c in _customers
                select new 
                {
                    c.CompanyName,
                    YearGroups=
                    from o in c.Orders
                    group o by o.OrderDate.Year into yg
                    select new
                    {
                        Year=yg.Key,
                        MonthGroups=
                        from o in yg
                        group o by o.OrderDate.Month into mg
                        select new
                        {
                            MOnth=mg.Key,
                            Orders=mg
                        }
                    }
                };
        }

        public void Linq45()
        {
            string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };

            var orderGroups = anagrams.GroupBy(
                        w => w.Trim(),
                        a => a.ToUpper(),
                        new AnagramEqualityComparer()
                        );
            
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
            foreach (var byGrade in petsList.GroupBy(x => x.Grade))
            {

                foreach (var byAge in byGrade.GroupBy(pet => Math.Floor(pet.Age)))
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

        public class AnagramEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return getCanonicalString(x) == getCanonicalString(y);
            }

            public int GetHashCode(string obj)
            {
                return getCanonicalString(obj).GetHashCode();
            }

            private string getCanonicalString(string word)
            {
                char[] wordChars = word.ToCharArray();
                Array.Sort<char>(wordChars);
                return new string(wordChars);
            }
        }



        public static void PrintOrders(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order.ToString());
            }
        }

        public static void PrintCustomers(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }
        }
    }

    public class CityComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
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
            return this.MemberwiseClone() as Pet;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExamples
{
    public class Class1
    {
        private static async Task ExampleAsync()
        {
            int result = await Allocate();
            Console.WriteLine("Compuete {0} ", result);

        }


        private static Task<int> Allocate()
        {
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

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
            return tcs.Task;
        }
    }
}

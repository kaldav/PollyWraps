using Polly;
using System;
using System.Threading.Tasks;

namespace PollyWraps
{
    class Program
    {
        static int runs = 0;
        static async Task Main(string[] args)
        {
            var fallbackPolicy = Policy.Handle<Exception>()
                .Fallback(() => Write("körte"));

            var retryPolicy = Policy
                .Handle<Exception>()
                .Retry(5);

            Policy.Wrap(retryPolicy, fallbackPolicy).Execute(() => Write("alma"));
            Console.ReadLine();
        }

        static void Write(string text)
        {
            runs++;
            var rnd = new Random();
            if (text == "alma" || runs<3)
            {
                Console.WriteLine("Ex");
                throw new Exception("ex happened!");
            }

            Console.WriteLine(text);
        }
    }
}

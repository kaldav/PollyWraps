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
            var retryPolicy = Policy
                .Handle<Exception>()
                .Retry(10);

            var fallbackPolicy = Policy.Handle<Exception>()
                .Fallback(() => retryPolicy.Execute(()=>Write("körte")));

            fallbackPolicy.Execute(() => Write("alma"));           
            Console.ReadLine();
        }

        static void Write(string text)
        {
            runs++;
            var rnd = new Random();
            if (text == "alma" || runs<5)
            {
                Console.WriteLine("Ex: "+text);
                throw new Exception("ex happened!");
            }

            Console.WriteLine(text);
        }
    }
}

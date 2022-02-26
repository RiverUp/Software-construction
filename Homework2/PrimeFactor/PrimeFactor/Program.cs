using System;

namespace PrimeFactor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input a number");
            string s = Console.ReadLine();
            int Multiple = Int32.Parse(s);
            if (Multiple < 2)
                Console.WriteLine("it doesn't have a prime factor.");
            for(int i=2;Multiple>=i ;i++)
            {
                int j = 0;
                while(Multiple%i==0)
                {
                    Multiple /= i;
                    if (j == 0)
                    {
                        Console.WriteLine($"{i} is its prime factor");
                        j++;
                    }
                }
            }
        }
    }
}

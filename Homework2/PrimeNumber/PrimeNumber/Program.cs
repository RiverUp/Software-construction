using System;

namespace PrimeNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] nums = new bool[101] ;
            for(int i=2;i<101;i++)
            {
                nums[i] = true;
            }
            for(int i = 2; i < 101; i++)
            {
                for (int j = 2; i * j < 101; j++)
                    {
                        nums[i * j] = false;
                    }
            }
            Console.WriteLine("the prime numbers are:");
            for(int i = 2; i < 101; i++)
            {
                if (nums[i]) 
                {
                    Console.Write($"{i} ");
                }
            }

        }
    }
}

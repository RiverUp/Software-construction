using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input an expression in such a way:1 + 2");
            string all = Console.ReadLine();
            string[] elements = all.Split(" ");
            int[] eles = new int[2];
            int res = 0;
            string op;
            eles[0] = Int32.Parse(elements[0]);
            eles[1] = Int32.Parse(elements[2]);
            op = elements[1];
            switch (op)
            {
                case "+": res = eles[0] + eles[1]; break;
                case "-": res = eles[0] - eles[1]; break;
                case "*": res = eles[0] * eles[1]; break;
                case "/": res = eles[0] / eles[1]; break;
            }
            Console.WriteLine($"the result is {res}");

        }
    }
} 


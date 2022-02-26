using System;

namespace ToeplitzMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please input the matrix's line");
            int a = Int32.Parse(Console.ReadLine());
            Console.WriteLine("please input the matrix's raw");
            int b = Int32.Parse(Console.ReadLine());
            int[,] matrix= new int[a,b];
            Console.WriteLine("Please input a matrix");
            bool bo = true;
            for(int i=0;i<a;i++)
            {
            string s = Console.ReadLine();
                string[] ss = s.Split(" ");
                while (ss.Length != b)
                {
                    Console.WriteLine("input error");
                    if (ss.Length > b)
                    {
                        Console.WriteLine("We will get rid of redundant numbers");
                        break;
                    }
                    if (ss.Length < b)
                    {
                        Console.WriteLine("Please input enough numbers");
                        s += Console.ReadLine();
                        ss = s.Split(" ");
                    }
                }
            for(int j=0;j<b;j++)
                {
                    matrix[i, j] = Int32.Parse(ss[j]);
                }
            }
            for(int i=0;i+1<a&&i+1<b;i++)
            {
                if (matrix[i, i] != matrix[i + 1, i + 1])
                {
                    bo = false;
                    break;
                }
            }
            Console.WriteLine(bo.ToString());
        }
    }
}

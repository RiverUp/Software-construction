using System;

namespace OperateInts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input a set of numbers,split each number with a spacing");
            string s = Console.ReadLine();
            string[] ss = s.Split(" ");
            int[] nums = new int[ss.Length];
            int i = 0;
            foreach(string sss in ss)
            {
                nums[i] = Int32.Parse(sss);
                i++;
            }
            for (int x = 0; x < i-1; x++)
            {
                for (int j = 0; j < i-x-1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int temp = nums[j + 1];
                        nums[j + 1] = nums[j];
                        nums[j] = temp;
                    }
                }
            }
            Console.WriteLine($"the biggest number of this set is {nums[i-1]}");
            Console.WriteLine($"the smallest number of this set is {nums[0]}");
            double res = 0;
            foreach(int a in nums)
            {
                res += a;
            }
            Console.WriteLine($"their sum is {res}");
            Console.WriteLine($"their average is {res / (i)}");
        }
    }
}

using System;

namespace Shape
{
    public interface Shape
    {
        int CalculateArea();
        bool JudgeShape();
    }
    public class Rectangle : Shape
    {
        int a, b;
        public int A
        {
            get => a;
            set => a = value;
        }
        public int B
        {
            get => b;
            set => b = value;
        }
        public Rectangle(int a,int b)
        {
            this.a = a;
            this.b = b;
        }
        public int CalculateArea()
        {
            try
            {
                if (JudgeShape())
                {
                    return a * b;
                }
                throw new Exception();
            }
            catch(Exception e)
            {
                Console.WriteLine("Please input valid side length");
                return 0;
            }
           
        }
        public bool JudgeShape()
        {
            if (a > 0 && b > 0)
            { 
                return true; 
            }
            else
            {
                return false;
            }
        }
    }
    public class Square : Shape
    {
        int a;
        public int A
        {
            get => a;
            set => a = value;
        }
        public Square(int a)
        {
            this.a = a;
        }
        public int CalculateArea()
        {
            try
            {
                if (JudgeShape())
                {
                    return a * a;
                }
                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine("Please input valid side length");
                return 0;
            }
            
        }
        public bool JudgeShape()
        {
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Triangle : Shape
    {
        int a, b, c;
        public int A
        {
            get => a;
            set => a = value;
        }
        public int B
        {
            get => b;
            set => b = value;
        }
        public int C
        {
            get => c;
            set => c = value;
        }
        public Triangle(int a,int b,int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public bool JudgeShape()
        {
            if(a+b>c&&a+c>b&&b+c>a)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int CalculateArea()
        {
            try
            {
                if (JudgeShape())
                {
                    int p = (a + b + c) / 2;
                    return (int)Math.Sqrt(p * (p - a) * (p - b) * (p - c));//海伦公式
                }
                throw new Exception();
            }
            catch(Exception e)
            {
                Console.WriteLine("please input valid side length");
                return 0;
            }
        }
    }
        class Program
    {
        static void Main(string[] args)
        {
            Shape[] shapes = new Shape[4];
            shapes[0] = new Rectangle(3, 4);
            shapes[1] = new Square(5);
            shapes[2] = new Triangle(5, 5, 6);
            shapes[3] = new Triangle(1, 2, 3);
            foreach(Shape sh in shapes)
            {
                Console.WriteLine(sh.CalculateArea());
            }
        }
    }
}

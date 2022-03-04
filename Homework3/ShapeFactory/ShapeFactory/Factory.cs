using System;

namespace ShapeFactory
{
    using Shape;
    class Factory
    {
        class shapeProducer
        {
            public shapeProducer() { }
            public Shape ProduceShape(int n)
            {
                Random r = new Random();
                Shape sh;
                switch (n)
                {
                    case 0: sh =new Triangle(r.Next(1, 10), r.Next(1, 10), r.Next(1, 10));break;
                    case 1:sh =new Square(r.Next(1, 10));break;
                    case 2:sh =new Rectangle(r.Next(1, 10), r.Next(1, 10));break;
                    default:sh = null;break;
                }
                return sh;
            }
        }
        static void Main(string[] args)
        {
            shapeProducer sp = new shapeProducer();
            Random rand = new Random();
            Shape[] shapes = new Shape[10];
            for(int i = 0; i < 10; i++)
            {
                shapes[i]=sp.ProduceShape(rand.Next(0, 3));
            }
            int areasum = 0;
            foreach(Shape sh in shapes)
            {
                areasum += sh.CalculateArea();
            }
            Console.WriteLine(areasum);
        }
    }
}

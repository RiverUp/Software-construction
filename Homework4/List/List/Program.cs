using System;

namespace List
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    public class GenericList<T>
    {
        
        private Node<T> head;
        private Node<T> tail;
        public GenericList()
        {
            tail = head = null;
        }
        public Node<T> Head { get => head; }
        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public static void ForEach<T>(GenericList<T> g,Action<T> action)
        {
            Node<T> node;
            node = g.Head;
            while (node.Next != null)
            {
                action(node.Data);
                node = node.Next;
            }
            action(node.Data);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<double> gener = new GenericList<double>();
            gener.Add(1.4);
            gener.Add(11.6);
            gener.Add(3.0);
            double max = gener.Head.Data,min = gener.Head.Data, sum = 0;
            Action<double> action = (m => Console.WriteLine(m));
            action += (m => sum += m);
            action += m =>
            {
                if (gener.Head.Next.Data < min)
                {
                    min = gener.Head.Next.Data;
                }
            };
            action += m =>
            {
                if (gener.Head.Next.Data > max)
                {
                    max = gener.Head.Next.Data;
                }
            };
            GenericList<double>.ForEach(gener, action);
            Console.WriteLine($"the sum of this list is {sum}");
            Console.WriteLine($"the max of this list is {max}");
            Console.WriteLine($"the min of this list is {min}");
        }
    }
}

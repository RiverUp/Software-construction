using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class ExceptionofDetails : Exception
    {
        string s;
        int code;
        public ExceptionofDetails(int a)
        {
            code = a;
            switch (a)
            {
                case 0: s = "there is already a completelt same order detail."; break;
                case 1: s = "there is no such goods"; break;
                case 2: s = "you dont't buy anything"; break;
                case 3: s = "there is no such order detail"; break;
            }
        }
        public override string ToString()
        {
            return s;
        }
    }
    class ExceptionofOrder : Exception
    {
        string s;
        int code;
        public ExceptionofOrder(int a)
        {
            code = a;
            switch (a)
            {
                case 0: s = "there is already a completelt same order ."; break;
                case 1: s = "there is no order at all"; break;
                case 2: s = "there is no such an order"; break;
            }
        }
        public override string ToString()
        {
            return s;
        }
    }
    public class Client
    {
        string name;
        string adderss;
        public string Name { get => name; set => name = value; }
        public string Address { set => adderss = value; }
        public Client()
        {

        }
        public Client(string n, string a)
        {
            name = n;
            adderss = a;
        }
        public override string ToString()
        {
            return $"client name : {name}; client address : {adderss}";
        }
    }
    public class Goods
    {
        string name;
        int price;
        public int Price { get => price; set => price = value; }
        public string Name { get => name; set => name = value; }
        public Goods()
        {

        }
        public Goods(string name, int price)
        {
            this.name = name;
            this.price = price;
        }
        public override string ToString()
        {
            return $"name : {name}; price : {price}";
        }
    }
    [Serializable]
    public class OrderDetails
    {
        int OrdDetnum;//明细编号
        Goods goods;
        int itemNums;//商品数量
        public int ItemNums { get => itemNums; set => itemNums = value; }
        public int OrdDetNum { get => OrdDetnum; set => OrdDetnum = value; }
        public Goods Goods { get => goods; set => goods = value; }
        public OrderDetails()
        {

        }
        public OrderDetails(Goods go, int i_ns)
        {
            DateTime d = DateTime.Now;
            OrdDetnum = Math.Abs(d.GetHashCode());
            goods = go;
            itemNums = i_ns;
        }
        public override bool Equals(object obj)
        {
            OrderDetails oD = obj as OrderDetails;
            if (this.OrdDetnum != oD.OrdDetnum)
                return false;
            else return true;
            //return this.goods.Name == oD.goods.Name && this.ItemNums == oD.ItemNums;
        }
        public override string ToString()
        {
            return $"Order Detail No : {OrdDetNum} " + Goods.ToString() + $" Quantity : {ItemNums}";
        }
    }
    [Serializable]
    public class Order : IComparable
    {
        int Ordnum;//订单编号
        Client cli;
        int money;
        List<OrderDetails> list;
        public int OrdNum { get => Ordnum; set => Ordnum = value; }
        public int Money { get => money; set => money = value; }
        public List<OrderDetails> List { get => list; set => list = value; }
        public Client Cli { get => cli; set => cli = value; }
        public Order()
        {

        }
        public Order(Client c)
        {
            if (c == null) return;
            list = new List<OrderDetails>();
            DateTime d = DateTime.Now;
            Ordnum = Math.Abs(d.GetHashCode());
            cli = c;
        }
        public void addOrdDet(OrderDetails oD)
        {
            if (oD == null) return;
            foreach (OrderDetails orddet in list)
            {
                if (oD.Equals(orddet)&&oD.Goods.Equals(orddet.Goods))
                {
                    throw new ExceptionofDetails(0);
                    break;
                }
            }
            if (oD.ItemNums == 0)
            {
                throw new ExceptionofDetails(2);
            }
            list.Add(oD);
        }
        public int calMoney()
        {
            money = 0;
            foreach (OrderDetails oD in list)
            {
                money += oD.ItemNums * oD.Goods.Price;
            }
            return money;
        }
        public int CompareTo(Object o)
        {
            if (o == null) return 0;
            Order order = o as Order;
            return this.Ordnum.CompareTo(order.Ordnum);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Order order = obj as Order;
            return this.Ordnum.Equals(order.Ordnum);
        }
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (OrderDetails oD in list)
            {
                s.Append(oD.ToString() + "\n");
            }
            return $"Order No : {Ordnum} Client : " + cli.ToString() + "\n" + s + $"Money : {calMoney()}";
        }
    }
    public class OrderService
    {
        List<Order> orders;
        List<Goods> goods;

        public List<Goods> Goods { get => goods; }
        [XmlElement]
        public List<Order> Orders { get => orders; }
        public OrderService(List<Goods> g)
        {
            if (g == null) return;
            orders = new List<Order>();
            goods = g;
        }
        public void AddOrder(Order order)
        {
            if (order == null) return;
            foreach (Order o in orders)
            {
                if (order.Equals(o))
                {
                    throw new ExceptionofOrder(0);
                    break;
                }
            }
            orders.Add(order);
        }
        public void DeleteOrder(int Ordnum)
        {
            bool j = false;
            if (orders.Count == 0)
            {
                throw new ExceptionofOrder(1);
            }
            else
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    if (Ordnum == orders[i].OrdNum)
                    {
                        j = true;
                        orders.RemoveAt(i);
                    }
                }
                if (!j)
                {
                    throw new ExceptionofOrder(2);
                }

            }
        }
        public List<Order> CheckOrder(int a, string s)//a查找类型，s查找变量值
        {
            switch (a)
            {
                case 1:
                    var q1 = from order in orders
                             where order.OrdNum == Int32.Parse(s)
                             orderby order.Money
                             select order; return q1.ToList();//订单号

                case 2:
                    var q2 = from order in orders
                             where order.List.Any(oD => oD.Goods.Name == s)
                             select order; return q2.ToList();//商品名称
                case 3:
                    var q3 = from order in orders
                             where order.Cli.Name == s
                             orderby order.Money
                             select order; return q3.ToList();//用户名称
                case 4:
                    var q4 = from order in orders
                             where order.Money == Int32.Parse(s)
                             orderby order.Money
                             select order; return q4.ToList();//金额数
                default: return null;

            }

        }
        public void CorrectOrder(int a, int b, string type, string value)//a，订单号，b订单明细号，type，更改什么数据，value更改的值
        {
            var q = from order in orders
                    where order.OrdNum == a
                    select order;
            if (!q.Any())
            {
                throw new ExceptionofOrder(2);
            }
            Order order1 = q.ToList<Order>()[0];
            var q2 = from oD in order1.List
                     where oD.OrdDetNum == b
                     select oD;
            if (!q2.Any())
            {
                throw new ExceptionofDetails(3);
            }
            OrderDetails oD1 = q2.ToList<OrderDetails>()[0];
            switch (type)
            {
                case "num":
                    oD1.ItemNums = Int32.Parse(value);
                    if (oD1.ItemNums == 0) { order1.List.Remove(oD1); }
                    break;
                case "goods":
                    var g = from go in goods
                            where go.Name == value
                            select go;
                    if (g.Count() == 0)
                    {
                        throw new ExceptionofDetails(1);
                    }
                    oD1.Goods = g.ToList<Goods>()[0]; break;
            }
        }
        public void Export()
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream("orders.xml", FileMode.Create))
            {
                xml.Serialize(fs, Orders);
            }
        }
        public List<Order> Import()
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream("orders.xml", FileMode.Open))
            {

                List<Order> orders1 = (List<Order>)xml.Deserialize(fs);
                return orders1;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Goods> glist = new List<Goods>(new Goods[]
            {
              new Goods("noodles", 12),
              new Goods("rice", 3),
              new Goods("mantou", 4),
            });
            OrderService service = new OrderService(glist);
            Console.WriteLine("now you are going to create an account");
            Console.WriteLine("please input your name");
            string name = Console.ReadLine();
            Console.WriteLine("please input your address");
            string address = Console.ReadLine();
            Client client = new Client(name, address);
            Console.WriteLine("输入序号：1.查询账户  2.创建订单  3.查询订单  0.退出");
            int choose = Int32.Parse(Console.ReadLine());
            while (choose != 0)
            {
                switch (choose)
                {
                    case 1:
                        Console.WriteLine(client.ToString()); break;
                    case 2:
                        Order order = new Order(client);
                        for (int i = 0; i < service.Goods.Count(); i++)
                        {
                            Console.WriteLine($"{i + 1}." + service.Goods[i].ToString());
                        }
                        Console.WriteLine("输入商品前序号选择商品，或0退出");
                        string a1 = Console.ReadLine();
                        if (a1 == "0") break;
                        int choose2 = Int32.Parse(a1);
                        while (choose2 != 0)
                        {
                            Console.WriteLine("请输入你想要的数量");
                            int n = Int32.Parse(Console.ReadLine());
                            OrderDetails det = new OrderDetails(service.Goods[choose2 - 1], n);
                            try { order.addOrdDet(det); }
                            catch (ExceptionofDetails e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            Console.WriteLine("输入商品前序号选择商品，或0退出");
                            choose2 = Int32.Parse(Console.ReadLine());
                        }
                        try { service.AddOrder(order); }
                        catch (ExceptionofOrder e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                        break;
                    case 3:
                        Console.WriteLine("请按序号选择您需要的服务：1.展示所有订单 2.查询某个订单 3.删除某个订单 4.更改某个订单 或输入0退出");
                        string a2 = Console.ReadLine();
                        if (a2 == "0") break;
                        int choose3 = Int32.Parse(a2);
                        switch (choose3)
                        {
                            case 1:
                                foreach (Order order1 in service.Orders)
                                {
                                    Console.WriteLine(order1.ToString());
                                }
                                break;
                            case 2:
                                Console.WriteLine("请选择查询方式：1.订单号 2.商品名称 3.用户名称 4.金额数");
                                int choose4 = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("请输入信息");
                                List<Order> orders = service.CheckOrder(choose4, Console.ReadLine());//try
                                if (orders.Count() == 0)
                                {
                                    Console.WriteLine("there is no such an order");
                                }
                                else
                                {
                                    foreach (Order o in orders)
                                    {
                                        Console.WriteLine(o.ToString());
                                    }
                                }
                                break;
                            case 3:
                                Console.WriteLine("请输入您想删除的订单的订单号或输入0退出");
                                try
                                {
                                    string a3 = Console.ReadLine();
                                    if (a3 == "0") break;
                                    service.DeleteOrder(Int32.Parse(a3));
                                }
                                catch (ExceptionofOrder e)
                                {
                                    Console.WriteLine(e.ToString());
                                }
                                break;
                            case 4:
                                Console.WriteLine("请输入您想修改的订单的订单号或输入0退出");
                                string s1 = Console.ReadLine();
                                if (s1 == "0") break;
                                try
                                {
                                    Order ord = service.CheckOrder(1, s1)[0];
                                    Console.WriteLine(ord.ToString());
                                    Console.WriteLine("请输入您想修改的订单明细的编号");
                                    string s2 = Console.ReadLine();
                                    Console.WriteLine("请输入您想更改的项目 num或goods");
                                    string s3 = Console.ReadLine();
                                    Console.WriteLine("请输入您想修改的值");
                                    string s4 = Console.ReadLine();
                                    service.CorrectOrder(Int32.Parse(s1), Int32.Parse(s2), s3, s4);
                                }
                                catch (ExceptionofDetails e)
                                {
                                    Console.WriteLine(e.ToString());
                                }
                                catch (ExceptionofOrder e)
                                {
                                    Console.WriteLine(e.ToString());
                                }
                                break;

                        }
                        break;
                }
                Console.WriteLine("输入序号：1.查询账户  2.创建订单  3.查询订单  0.退出");
                choose = Int32.Parse(Console.ReadLine());
            }
        }
    }

}

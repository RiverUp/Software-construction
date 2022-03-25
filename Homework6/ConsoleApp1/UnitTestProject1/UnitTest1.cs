using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;
using System.Collections.Generic;

namespace UnitTestProject1
{ 
    
    [TestClass]
   
    public class UnitTest1
    {
        Client c;
        List<Goods> glist;
        OrderService service;
        Order order;
        OrderDetails oD1;
        OrderDetails oD2;
        [TestInitialize]
        public void Initial()
        {
           c = new Client("heziang", "benxi");
            glist = new List<Goods>(new Goods[]
           {
              new Goods("noodles", 12),
              new Goods("rice", 3),
              new Goods("mantou", 4),
           });
            service = new OrderService(glist);
            order = new Order(c);
            oD1 = new OrderDetails(glist[0], 10);
            oD2 = new OrderDetails(glist[2], 5);
            order.addOrdDet(oD1);
            order.addOrdDet(oD2);
            service.AddOrder(order);
        }

        [TestMethod]
        public void TestAdd()
        {
            Order order2 = new Order(c);
            OrderDetails oD3 = new OrderDetails(glist[0], 10);
            OrderDetails oD4 = new OrderDetails(glist[1], 5);
            order2.addOrdDet(oD3);
            order2.addOrdDet(oD4);
            service.AddOrder(order2);
            Assert.AreEqual(order2, service.Orders[1]);

        }

        [TestMethod]
        public void TestDelete()
        {
            service.DeleteOrder(order.OrdNum);
            Assert.IsTrue(service.Orders.Count == 0);
        }

        [TestMethod]
        public void TestSearch()
        {
            Assert.AreEqual(service.CheckOrder(3, "heziang")[0], order);
        }
        [TestMethod]
        public void TestCorrect()
        {
            service.CorrectOrder(order.OrdNum, order.List[0].OrdDetNum, "num", "5");
            Assert.IsTrue(order.List[0].ItemNums == 5);
        }
        [TestMethod]
        public void TestExportAndImport()
        {
            service.Export();
            List<Order> orders = service.Import();
            CollectionAssert.AreEqual(service.Orders, orders);

        }
    }
}

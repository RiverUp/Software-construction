using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Goods noodles = new Goods("面条", 12);
            Goods rice = new Goods("米饭", 2);
            int orderId;
            //新建订单
            using (var context = new OrderContext())
            {
                var order = new Order(123);
                order.Details = new List<OrderDetail>() {
                    new OrderDetail(noodles,1,1234) ,
                    new OrderDetail(rice,2,2345)

                 };
                int money = 0;
                foreach (OrderDetail detail in order.Details)
                {
                    money += detail.item.Price * detail.itemNums;
                }
                order.Money = money;
                context.Orders.Add(order);
                context.SaveChanges();
                orderId = order.OrderId;
            }
            
            //添加订单明细
            using (var context = new OrderContext())
            {
                var detail = new OrderDetail(noodles, 3, 3456) { OrderId = orderId };
                context.Entry(detail).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
            //根据id查找订单，输出价钱
            using (var context = new OrderContext())
            {
                var order = context.Orders.SingleOrDefault(o => o.OrderId == 123);
                if (order != null) Console.WriteLine(order.Money);
            }
            //修改id为1234的订单明细
            using (var context = new OrderContext())
            {
                var detail = context.Details.SingleOrDefault(o => o.OrderDetailId == 1234);
                if (detail != null)
                {
                    detail.itemNums = 3;
                    context.SaveChanges();
                }
                //删除id为2345的订单明细
            }
            using (var context = new OrderContext())
            {
                var detail = context.Details.SingleOrDefault(o => o.OrderDetailId == 2345);
                if (detail != null)
                {
                    context.Details.Remove(detail);
                }
            }
            //删除id为123的订单
            using (var context = new OrderContext())
            {
                var order = context.Orders.SingleOrDefault(o => o.OrderId == 123);
                if (order != null)
                {
                    context.Orders.Remove(order);
                }
            }
        }
    }
}
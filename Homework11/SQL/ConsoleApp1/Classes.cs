
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    public class Goods
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Goods(string name,int price)
        {
            Name = name; Price = price; 
        }

    }
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        [Required]
        public int itemNums { get; set; }
        public Goods item { get; set; }
        public Order Order { get; set; }
        public OrderDetail(Goods go, int itemNum,int id)
        {

            OrderDetailId = id;
            this.item = go;
            this.itemNums = itemNum;
        }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public int Money { get; set; }
        public List<OrderDetail> Details { get; set; }
        public Order()
        {      
            OrderId = 123;
        }
    }
}


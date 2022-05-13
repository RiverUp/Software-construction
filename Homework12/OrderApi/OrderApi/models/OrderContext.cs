
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace OrderApi.models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
           : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表

        }
        public  DbSet<Order> Orders { get; set; }
        public  DbSet<OrderDetail> Details { get; set; }
    }
}

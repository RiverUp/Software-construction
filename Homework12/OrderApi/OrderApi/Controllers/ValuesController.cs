using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private  readonly OrderContext _orderContext;
        public ValuesController(OrderContext context)
        {
            _orderContext = context;
        }
        // GET: api/todo/{id}  id为路径参数
        [HttpGet("{id}")]
        //根据id获取订单
        public ActionResult<Order> GetOrder(long id)
        {
            var order = _orderContext.Orders.FirstOrDefault(t => t.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }
        [HttpPost]
        //增加订单
        public ActionResult<Order> AddOrder(Order newOrder)
        {
            try
            {
                _orderContext.Orders.Add(newOrder);
                _orderContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }

            return newOrder;
        }

        [HttpGet]
        //Get全部的Order
        public ActionResult<List<Order>> GetOrders()
        {
            return _orderContext.Orders .ToList();
        }

        [HttpDelete("{id}")]
        //删除某id的订单
        public ActionResult<Order> DeleteOrder(long id)
        {
            try
            {
                var order = _orderContext.Orders .FirstOrDefault(o => o.OrderId == id);
                if (order != null)
                {
                    foreach (OrderDetail detail in order.Details)
                    {
                        detail.item = null;
                    }
                    _orderContext.Orders.Remove(order);
                    _orderContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }
        [HttpGet("pageQuery")]
        //查询某个id的order
        public ActionResult<List<Order>> queryTodoItem(int orderid)
        {
            var query = buildQuery(orderid);
            return query.ToList();
        }
        private IQueryable<Order> buildQuery(int orderid)
        {
            IQueryable<Order> query = _orderContext.Orders;
            if (orderid != null)
            {
                query = query.Where(o => o.OrderId == orderid);
            }
            return query;
        }
        [HttpPut("{id}")]
        public ActionResult<Order> changeOrder(long id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                _orderContext.Entry(order).State = (System.Data.Entity.EntityState)EntityState.Modified;
                _orderContext.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }
    }
}

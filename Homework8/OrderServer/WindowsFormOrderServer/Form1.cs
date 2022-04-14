using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormOrderServer
{
    public partial class Form1 : Form
    {
        List<Goods> glist;
        OrderService service;
        Client client;
        public Form1()
        {
            InitializeComponent();
           glist = new List<Goods>(new Goods[]
{
              new Goods("noodles", 12),
              new Goods("rice", 3),
              new Goods("mantou", 4),
});
            service = new OrderService(glist);
            client = new Client("何子昂", "华夏花园");
            OrderDetails details1 = new OrderDetails(glist[0],2);
            OrderDetails details2 = new OrderDetails(glist[2], 2);
            Order order= new Order(client);
            order.addOrdDet(details2);
            order.addOrdDet(details1);
            service.AddOrder(order);
            bindingSource1.DataSource = service.Orders;
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = bindingSource2;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Form2 form2 =new Form2(glist,client);
            form2.Show();
            service.AddOrder(form2.Order);
            dataGridView1.DataSource = null;
            dataGridView2.DataSource= null; 
            dataGridView1.DataSource= bindingSource1;
            dataGridView2.DataSource= bindingSource2;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Order order = bindingSource1.Current as Order;
            service.DeleteOrder(order.OrdNum);
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = bindingSource1;
            dataGridView2.DataSource = bindingSource2;
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {

        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == null) { return; }
            else 
            {            
                List<Order> orderQueried=service.CheckOrder(1, textBox1.Text);
                bindingSource1.DataSource = orderQueried; 
            }

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = bindingSource1;
            dataGridView2.DataSource = bindingSource2;
        }
    }
}

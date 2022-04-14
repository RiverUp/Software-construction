using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApp1;

namespace WindowsFormOrderServer
{
    
    public partial class Form2 : Form
    {
        private List<Goods> goods;
        private List<OrderDetails> orderDetails;
        private Client client;
        private OrderService orderService;
        public Order Order { get; set; }
        public Form2(List<Goods> g,Client c)
        {
            InitializeComponent();
            goods = g;
            checkBox1.DataBindings.Add("Text",goods[0],"Name");
            checkBox2.DataBindings.Add("Text",goods[1],"Name");
            checkBox3.DataBindings.Add("Text",goods[2],"Name");
            orderDetails = new List<OrderDetails>();
            client = c;
            Order = new Order(client);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (checkBox1.Checked && int.TryParse(textBox1.Text, out i))
            {
                if (i > 0)
                {
                    orderDetails.Add(new OrderDetails(goods[0], i));
                    i=0;
                }
                else
                {
                    textBox1.Text = "please input valid quantity";
                }
            }
            if (checkBox2.Checked && int.TryParse(textBox2.Text, out i))
            {
                if (i > 0)
                {
                    orderDetails.Add(new OrderDetails(goods[1], i));
                    i = 0;
                }
                else
                {
                    textBox2.Text = "please input valid quantity";
                }
            }
            if (checkBox3.Checked && int.TryParse(textBox3.Text, out i))
            {
                if (i > 0)
                {
                    orderDetails.Add(new OrderDetails(goods[2], i));
                    i = 0;
                }
                else
                {
                    textBox3.Text = "please input valid quantity";
                }
            }
            if (orderDetails.Count != 0)
            {               
                foreach(OrderDetails oD in orderDetails)
                {
                    Order.addOrdDet(oD);
                }
                
            }
            this.Close();
        }
    }
}

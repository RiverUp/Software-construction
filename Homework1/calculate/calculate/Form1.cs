using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculate
{
    public partial class Form1 : Form
    {
        double a = 0, b = 0;
        string res="the answer is here";
        string s;
        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            s = "/";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            s = "+";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            switch (s)
            {
                case "+": res = (a + b).ToString(); break;
                case "-": res = (a - b).ToString(); break;
                case "*": res = (a * b).ToString(); break;
                case "/":
                    if (b != 0) { res = (a / b).ToString(); }
                    else { res = "the formula is wrong"; }
                    break;
            }
            textBox3.Text = res;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            a = Double.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            b = Double.Parse(textBox2.Text);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            s = "*";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = res;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            s = "-";
        }


    }
}

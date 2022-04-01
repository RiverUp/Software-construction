using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private double th1 ;
        private double th2;
        private double per1;
        private double per2;
        private int nn;
        private int leng;
        private Pen pen;

        
        private System.ComponentModel.IContainer components = null;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("黄色");
            comboBox1.Items.Add("红色");
            comboBox1.Items.Add("蓝色");
            comboBox1.Items.Add("绿色");
            comboBox1.Items.Add("黑色");
            comboBox1.Items.Add("灰色");
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            try
            {
                nn=Int32.Parse(s);
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (graphics == null) graphics = this.panel1 .CreateGraphics();
            drawCayleyTree(nn, 200, 310, leng, -Math.PI / 2);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string s = textBox2.Text;
            try
            {
                leng = Int32.Parse(s);
            }
            catch { }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        void drawCayleyTree(int n,double x0, double y0, double leng, double th)
        {
            if (n == 0) return;

            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            drawLine(x0, y0, x1, y1);

            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayleyTree(n - 1, x1, y1, per2 * leng, th - th2);
        }
        void drawLine(double x0, double y0, double x1, double y1)
        {
            graphics.DrawLine(
                pen,
                (int)x0, (int)y0, (int)x1, (int)y1);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string s = textBox3.Text;
            try
            {
                per1 = Double.Parse(s);
            }
            catch { }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string s = textBox4.Text;
            try
            {
                per2 = Double.Parse(s);
            }
            catch { }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string s = textBox5.Text;
            try
            {
                th1 = Double.Parse(s) * Math.PI / 180;
            }
            catch { }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string s = textBox6.Text;
            try
            {
                th2 = Double.Parse(s) * Math.PI / 180;
            }
            catch { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "黄色":pen = Pens.Yellow; break;
                case "蓝色": pen = Pens.Blue; break;
                case "红色": pen = Pens.Red; break;
                case "绿色": pen = Pens.Green; break;
                case "黑色": pen = Pens.Blue; break;
                case "灰色": pen = Pens.Gray; break;
            }
        }
    }
}

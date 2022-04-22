using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleCrawler;
using System.Threading;

namespace crawlWinform
{
    public partial class Form1 : Form
    {
        simpleCrawler myCrawler;
        public Form1()
        {
            InitializeComponent();
             myCrawler = new simpleCrawler();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string startUrl = textBox1.Text;
            myCrawler.Urls.Add(startUrl, false);//加入初始页面
            Crawl2();
        }
        public void Crawl2()
        {
            richTextBox1.Text += "开始爬行了.... \n";
            while (true)
            {
                string current = null;
                foreach (string url in myCrawler.Urls.Keys)
                {
                    if ((bool)myCrawler.Urls[url]) continue;
                    current = url;
                }
                if (current == null || myCrawler.Count > 10) break;
                richTextBox1.Text += ("爬行" + current + "页面!\n");
                string html = myCrawler.DownLoad(current); // 下载
                myCrawler.Urls[current] = true;
                myCrawler.Count++;
                myCrawler.Parse(html, current);//解析,并加入新的链接
                richTextBox1.Text += "爬行结束\n";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Crawl2();
        }
    }
}

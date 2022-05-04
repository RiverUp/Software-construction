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
        BindingSource myBindingSource =new BindingSource();
        simpleCrawler myCrawler = new simpleCrawler();
        Thread myThread=null;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = myBindingSource;
            myCrawler.PageDownloaded += Crawler_PageDownloaded;
            myCrawler.CrawlerStopped += Crawler_CrawlerStopped;
        }
        private void Crawler_CrawlerStopped(simpleCrawler obj)
        {
            Action action = () => {
                textBox1.Text = "爬虫已停止";
                button1.Enabled = true;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
        private void Crawler_PageDownloaded(simpleCrawler crawler, int index, string url, string info)
        {
            var pageInfo = new { Index = index, URL = url, Status = info };
            Action action = () => myBindingSource.Add(pageInfo);
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }
        private void button1_Click(object sender, EventArgs e)
        {
            myBindingSource.Clear();
            myCrawler.StartUrl = textBox1.Text;
            if(myThread != null) {
                myThread.Abort();
            }
            myThread = new Thread(myCrawler.Crawl);
            myThread.Start();
            textBox1.Text = "爬虫已启动....";
            button1.Enabled = false;
        }
    }
}

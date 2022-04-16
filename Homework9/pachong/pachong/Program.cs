using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;

namespace SimpleCrawler
{
    public class simpleCrawler
    {
        private Hashtable urls = new Hashtable();
        public Hashtable Urls { get { return urls; } }
        private int count = 0;
        public int Count { get { return count; } set { count = value; } }
        static void Main(string[] args)
        {
            simpleCrawler myCrawler = new simpleCrawler();
            string startUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];
            myCrawler.urls.Add(startUrl, false);//加入初始页面
            new Thread(myCrawler.Crawl).Start();
        }

        public void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach ( string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }
                if (current == null || count > 10) break;
                Console.WriteLine("爬行" + current + "页面!");
                string html = DownLoad(current); // 下载
                urls[current] = true;
                count++;
                Parse(html,current);//解析,并加入新的链接
                Console.WriteLine("爬行结束");
            }
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public void Parse(string html,string current)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[.](html|asxp|jsp)[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if (Regex.IsMatch(strRef,@"^(/|./|../|)") && !(Regex.IsMatch(strRef, @"^(http)")))
                {
                    
                    Uri baseuri = new Uri(current);
                    Uri absoluteUri = new Uri(baseuri, strRef);
                    strRef = absoluteUri.ToString();
                }
                if (urls[strRef] == null) urls[strRef] = false;
            }
        }
    }
}
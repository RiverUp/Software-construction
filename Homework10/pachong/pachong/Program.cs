using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
        public event Action<simpleCrawler> CrawlerStopped;
        public event Action<simpleCrawler, int, string, string> PageDownloaded;
        private ConcurrentDictionary<string, bool> urls = new ConcurrentDictionary<string, bool>();//判断是不是搜索过了,值是是否成功了
        private ConcurrentQueue<string> queue;//线程队列
        private int count = 0;
        public int Count { get { return count; } set { count = value; } }
        public string StartUrl { get; set; }
        static void Main(string[] args)
        {
        }

        public void Crawl()
        {
            urls.Clear();
            queue = new ConcurrentQueue<string>();
            queue.Enqueue(StartUrl);
            List<Task> tasks = new List<Task>();
            PageDownloaded +=(crawler,index,url,info)=> { count++; };
            while (tasks.Count<10)
            {
                if (!queue.TryDequeue(out string url))
                {
                    if (count < tasks.Count)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                    Task task =Task.Run(() =>DownloadAndParse(url));
                    tasks.Add(task);
                
            }
            Task.WaitAll(tasks.ToArray());
            CrawlerStopped(this);
        }
        private void DownloadAndParse(string url)
        {
            try
            {
                string html =DownLoad(url);;
                urls[url] = true;
                Parse(html,url);
                PageDownloaded(this,count,url,"success");
            }
            catch (Exception e)
            {
                PageDownloaded(this,count,url,"Error:"+e.Message);
            }
        }
        private string DownLoad(string url)
        {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
        }

        private void Parse(string html,string current)
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
                if (!urls.ContainsKey(strRef))
                {
                    queue.Enqueue(strRef);
                    urls.TryAdd(strRef,false);
                }
            }
        }
    }
}
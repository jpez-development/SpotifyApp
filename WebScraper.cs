using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApp
{
    class WebScraper
    {
        private readonly HtmlWeb Web;
        public HtmlDocument Site;
        public List<HtmlNode> HeaderNames;

        public WebScraper(string url, string node)
        {
            Web = new HtmlWeb();
            Site = Web.Load(url);
            HeaderNames = Site.DocumentNode.SelectNodes(node).ToList();
        }
    }
}

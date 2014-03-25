using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

using AppStudio.Data;

namespace AppStudio.Data
{
    /// <summary>
    /// Rss data provider class
    /// </summary>
    public class RssDataProvider
    {
        private Uri _uri;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url"></param>
        public RssDataProvider(string url)
        {
            _uri = new Uri(url);
        }

        /// <summary>
        /// Starts loading the feed and initializing the reader for the feed type.
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<RssSchema>> Load()
        {
            string xmlContent = await DownloadAsync(_uri);

            var doc = XDocument.Parse(xmlContent);
            var type = BaseRssReader.GetFeedType(doc);

            BaseRssReader rssReader;
            if (type == RssType.Rss)
                rssReader = new RssReader();
            else
                rssReader = new AtomReader();

            return rssReader.LoadFeed(doc);
        }

        private async Task<string> DownloadAsync(Uri url)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(url.AbsoluteUri);
        }
    }
}

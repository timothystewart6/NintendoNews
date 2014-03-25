using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace AppStudio.Data
{
    public class ServiceDataProvider : WebClient
    {
        const string DATASERVICES_URL_PATTERN = "http://ds.winappstudio.com/api/data/collection?dataRowListId={0}";

        private Uri _uri;

        public ServiceDataProvider(string appId, string dataSourceName, int pageIndex = 0, int blockSize = 40)
        {
            string url = String.Format(DATASERVICES_URL_PATTERN, dataSourceName, appId);
            url = url + String.Format("&pageIndex={0}&blockSize={1}", pageIndex, blockSize);
            _uri = new Uri(url);
        }

        public async Task<IEnumerable<T>> Load<T>()
        {
            string data = await DownloadAsync(_uri);

            IEnumerable<T> records = JsonConvert.DeserializeObject<IEnumerable<T>>(data);

            return records;
        }
        
        public Task<string> DownloadAsync(Uri url)
        {
            var taskCompletionSrc = new TaskCompletionSource<string>();
            DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    taskCompletionSrc.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    taskCompletionSrc.TrySetCanceled();
                }
                else
                {
                    taskCompletionSrc.TrySetResult(e.Result);
                }
            };
            DownloadStringAsync(url);
            return taskCompletionSrc.Task;
        }
    }
}

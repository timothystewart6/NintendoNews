using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppStudio.Data
{
    /// <summary>
    /// Not implemented in current version
    /// </summary>
    public class AppCache
    {
        static public async Task<T> GetItemAsync<T>(string key) where T : BindableSchemaBase
        {
            await Task.Run(() => { });
            return null;
        }

        static public async Task AddItemAsync<T>(string key, T item) where T : BindableSchemaBase
        {
            await Task.Run(() => { });
        }

        static public async Task<IEnumerable<T>> GetItemsAsync<T>(string key) where T : BindableSchemaBase
        {
            await Task.Run(() => { });
            return null;
        }

        static public async Task AddItemsAsync<T>(string key, IEnumerable<T> items) where T : BindableSchemaBase
        {
            await Task.Run(() => { });
        }
    }
}

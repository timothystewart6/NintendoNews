using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public interface IDataSource<T> where T : BindableSchemaBase
    {
        Task<IEnumerable<T>> LoadData();
        Task<IEnumerable<T>> Refresh();
    }
}

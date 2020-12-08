using System.Collections.Generic;

namespace Project.Core
{
    public interface IPagedList<T> : IList<T>
    {
        int TotalRecords { get; }
        int TotalPages { get; }
        int PageIndex { get; }
        int PageSize { get; }
    }
}

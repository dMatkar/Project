using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public class PagedList<T> : List<T>,IPagedList<T>
    {
        #region Properties
        public int TotalPages { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int TotalRecords { get; private set; }

        #endregion

        #region Constructors

        public PagedList(IEnumerable<T> list,int pageIndex,int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = list.Count();
            TotalPages = TotalRecords / PageSize;
            if (TotalRecords % PageSize > 0)
                TotalPages += 1;

            AddRange(list.Skip(pageIndex * pageSize).Take(pageSize));
        }

        public PagedList(IQueryable<T> list,int pageIndex,int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = list.Count();
            TotalPages = TotalRecords / PageSize;
            if (TotalRecords % PageSize > 0)
                TotalPages += 1;

            AddRange(list.Skip(pageIndex * pageSize).Take(pageSize));
        }

        #endregion
    }
}

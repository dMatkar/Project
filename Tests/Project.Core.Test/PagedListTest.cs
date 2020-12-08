using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Test
{
    [TestClass]
    public class PagedListTest
    {
        [TestMethod]
        public void TestData()
        {
            IList<string> list = new List<string>();
            list.Add("dinesh");
            list.Add("dinesh");
            list.Add("dinesh");
            list.Add("dinesh");
            list.Add("dinesh");
            list.Add("dinesh");
            list.Add("dinesh");
            IPagedList<string> obj  = new PagedList<string>(list, 0, 2);
            Assert.IsTrue(obj.Count == 2);
            Assert.IsTrue(obj.TotalPages == 4);
            Assert.IsTrue(obj.TotalRecords == 7);
        }
    }
}

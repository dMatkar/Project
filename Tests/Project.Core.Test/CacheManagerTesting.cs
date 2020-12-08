using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Test
{
    class Student
    {
        public int Id  { get; set; }
        public string Name  { get; set; }
    }

    [TestClass]
    public class CacheManagerTesting
    {
        [TestMethod]
        public void ShouldSetCache()
        {
            MemoryCacheManager memoryCacheManager = new MemoryCacheManager();
            memoryCacheManager.Set("student", new Student() { Id = 1, Name = "dinesh" }, 20);
            Student student = memoryCacheManager.Get<Student>("student");
            student.Name = "dinesh Matkar";
            Student student1= memoryCacheManager.Get<Student>("student");
            Assert.IsTrue(student1.Name == "dinesh Matkar");
        }
    }
}

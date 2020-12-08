using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Infrastructure;
using Project.Core.Infrastructure.Mapper;

namespace Project.Core.Test.AutoMapperTesting
{
    [TestClass]
    public class AutoMapperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            EngineContext.Current.Initialize();

            Student student = new Student() { Id = 1, Name = "dinesh",City="Pune" };
            StudentDTO studentDTO = new StudentDTO() { Id = 1,Name="ramesh",City="Mumbai" };

            Student studentDTO1  = AutoMapperConfiguration.Mapper.Map(studentDTO, student);

            Assert.IsTrue(studentDTO1.Id == 1);
            Assert.IsTrue(studentDTO1.Name == "ramesh");
            Assert.IsTrue(studentDTO1.City  == "Mumbai");
        }
    }
}

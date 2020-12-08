using AutoMapper;
using Project.Core.Infrastructure.Mapper;

namespace Project.Core.Test.AutoMapperTesting
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

    }

    public class MapperConfiguration : IMapperConfiguration
    {
        public int Order => 1;

        public void Register(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<Student, StudentDTO>();

            configurationExpression.CreateMap<StudentDTO, Student>();
               // .ForMember(dest => dest.City, mo => mo.Ignore());
        }
    }
}

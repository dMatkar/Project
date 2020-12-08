using AutoMapper;

namespace Project.Core.Infrastructure.Mapper
{
    public interface IMapperConfiguration
    {
        void Register(IMapperConfigurationExpression configurationExpression);
        int Order { get; }
    }
}

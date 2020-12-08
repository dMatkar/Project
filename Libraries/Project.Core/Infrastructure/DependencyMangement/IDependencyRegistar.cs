using Autofac;

namespace Project.Core.Infrastructure.DependencyMangement
{
    public interface IDependencyRegistar
    {
        void Register(ContainerBuilder builder,ITypeFinder typeFinder);
        int Order { get; }
    }
}

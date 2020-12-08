using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using AutoMapper.Configuration;
using Project.Core.Infrastructure.DependencyMangement;
using Project.Core.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project.Core.Infrastructure
{
    public class ProjectEngine : IEngine
    {
        #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Properties

        public ContainerManager Container
        {
            get => _containerManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Register Mappings 
        /// </summary>
        public void RegisterMappings()
        {
            AppDomainTypeFinder typeFinder = new AppDomainTypeFinder();
            IEnumerable<Type> types = typeFinder.FindClassesOfType<IMapperConfiguration>();

            if (types != null && (!types.Any()))
                return;

            IList<IMapperConfiguration> mappers = new List<IMapperConfiguration>();
            foreach (var mapperType in types)
            {
                object mappertypeObject = Activator.CreateInstance(mapperType);
                mappers.Add(mappertypeObject as IMapperConfiguration);
            }

            IMapperConfigurationExpression configurationExpression = new MapperConfigurationExpression();
            foreach (var mapper in mappers.OrderBy(x => x.Order))
                mapper.Register(configurationExpression);

            AutoMapperConfiguration.Init(configurationExpression);
        }

        /// <summary>
        /// Register dependencies.
        /// </summary>
        public void RegisterDependencies()
        {
            AppDomainTypeFinder appDomainTypeFinder = new AppDomainTypeFinder();
            IEnumerable<Type> types = appDomainTypeFinder.FindClassesOfType<IDependencyRegistar>();
            if (types != null && (!types.Any()))
                return;

            IList<IDependencyRegistar> drInstances = new List<IDependencyRegistar>();
            foreach (var drType in types)
                drInstances.Add(Activator.CreateInstance(drType) as IDependencyRegistar);

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(appDomainTypeFinder).As<ITypeFinder>();
            foreach (var dependencyRegister in drInstances.OrderBy(x => x.Order))
                dependencyRegister.Register(builder, appDomainTypeFinder);

             var container = builder.Build();
            _containerManager = new ContainerManager(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
           var data= DependencyResolver.Current;
        }

        /// <summary>
        /// Initialize project engine.
        /// </summary>
        public void Initialize()
        {
             RegisterDependencies();

            RegisterMappings();
        }

        /// <summary>
        /// Strongly typed resolve method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return _containerManager.Resolve<T>();
        }

        /// <summary>
        /// Weakly typed resolve method.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return _containerManager.Resolve(type);
        }

        /// <summary>
        /// Resolve all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return _containerManager.Resolve<IEnumerable<T>>();
        }

        #endregion
    }
}

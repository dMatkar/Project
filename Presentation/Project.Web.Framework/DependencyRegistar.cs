using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Project.Core.Caching;
using Project.Core.Configuration;
using Project.Core.Data;
using Project.Core.Infrastructure;
using Project.Core.Infrastructure.DependencyMangement;
using Project.Data;
using Project.Service.Catalog;
using Project.Service.Configuration;
using Project.Service.Customers;
using Project.Service.Directory;
using Project.Service.Helper;
using Project.Service.Localization;
using Project.Service.Orders;
using Project.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Project.Web.Framework
{
    public class DependencyRegistar : IDependencyRegistar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterSource<SettingSource>();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();

            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();

            builder.Register<IDbContext>(c => new ProjectDataContext(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=Project.Data.ProjectDataContext;Integrated Security=True")).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<MeasurementService>().As<IMeasurementService>().InstancePerLifetimeScope();
            builder.RegisterType<StateService>().As<IStateService>().InstancePerLifetimeScope();
            builder.RegisterType<StateService>().As<IStateService>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>().InstancePerLifetimeScope();
        }

        public int Order => 1;
    }

    public class SettingSource : IRegistrationSource
    {
        MethodInfo BuildMethod = typeof(SettingSource).GetMethod("BuildRegistration", BindingFlags.Static | BindingFlags.NonPublic);
        public bool IsAdapterForIndividualComponents => false;

        public IEnumerable<IComponentRegistration> RegistrationsFor(
              Autofac.Core.Service service,
              Func<Autofac.Core.Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<T>() where T : ISettings, new()
        {
            return RegistrationBuilder.ForDelegate((c, p) =>
             {
                 var src = c.Resolve<ISettingService>().LoadSettings<T>(0);
                 return src;
             })
              .InstancePerLifetimeScope()
              .CreateRegistration();
        }
    }
}



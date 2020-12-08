using Autofac;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Infrastructure;
using Project.Core.Infrastructure.DependencyMangement;
using Project.Data;
using Project.Service.Catalog;
using Project.Service.Customers;
using Project.Service.Helper;
using Project.Service.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Test
{
    public class DependencyRegistar : IDependencyRegistar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();

            builder.Register<IDbContext>(c => new ProjectDataContext(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=Project.Data.ProjectDataContext;Integrated Security=True"));
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<MeasurementService>().As<IMeasurementService>();
        }

        public int Order => 1;
    }
}

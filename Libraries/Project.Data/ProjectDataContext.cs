using Project.Core;
using Project.Data.Mappings;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Project.Data
{
    public class ProjectDataContext : DbContext,IDbContext
    {
        #region Constructor
        public ProjectDataContext(string connectionStringOrDatabaseName):base(connectionStringOrDatabaseName)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProjectDataContext>());
        }
        #endregion

        #region Properties

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
          return base.Set<TEntity>();
        }

        #endregion

        #region Utilities

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                 .Where(t => !string.IsNullOrWhiteSpace(t.Namespace))
                 .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(ProjectEntityTypeConfiguration<>)).ToList();

            if (types != null && types.Any())
            {
                types.ForEach(type =>
                {
                    dynamic obj = Activator.CreateInstance(type);
                    modelBuilder.Configurations.Add(obj);
                });
            }
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Logging

        public void Log(string data)
        {
            string oldData = File.ReadAllText(@"E:\Final Project\Shopping Project\Source Code\Libraries\Project.Data\Loging.txt");
            StringBuilder stringBuilder = new StringBuilder(oldData);
            stringBuilder.AppendLine(data);
            File.WriteAllText(@"E:\Final Project\Shopping Project\Source Code\Libraries\Project.Data\Loging.txt", stringBuilder.ToString());
        }

        #endregion
    }
}

using Project.Core;
using System.Data.Entity.ModelConfiguration;

namespace Project.Data.Mappings
{
    public abstract class ProjectEntityTypeConfiguration<TEntityType> : EntityTypeConfiguration<TEntityType> where TEntityType : BaseEntity
    {
        protected ProjectEntityTypeConfiguration()
        {
        }
    }
}

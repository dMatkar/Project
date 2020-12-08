using Project.Core.Domain.Directory;

namespace Project.Data.Mappings.Diretory
{
    public class StateMap : ProjectEntityTypeConfiguration<State>
    {
        public StateMap()
        {
            HasKey(s => s.Id);
            ToTable("States");

            Property(s => s.Name).HasMaxLength(50);
            Property(s => s.Abbreviation).HasMaxLength(10);
        }
    }
}

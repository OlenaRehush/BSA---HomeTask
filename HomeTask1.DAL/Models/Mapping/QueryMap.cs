using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HomeTask1.DAL.Models.Mapping
{
    public class QueryMap : EntityTypeConfiguration<Query>
    {
        public QueryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.country)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Queries");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.cityId).HasColumnName("cityId");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.country).HasColumnName("country");
            this.Property(t => t.time).HasColumnName("time");
        }
    }
}

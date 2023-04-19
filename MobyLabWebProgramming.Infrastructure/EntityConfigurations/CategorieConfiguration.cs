using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations
{
    public class CategorieConfiguration : IEntityTypeConfiguration<Categorie>
    {
        public void Configure(EntityTypeBuilder<Categorie> builder)
        {
            builder.Property(e => e.Id) 
                .IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.CreatedAt)
            .IsRequired();
            builder.Property(e => e.UpdatedAt)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(4095)
                .IsRequired();
        }
    }
}

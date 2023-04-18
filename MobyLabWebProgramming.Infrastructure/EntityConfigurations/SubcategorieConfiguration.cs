using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations
{
    public class SubcategorieConfiguration : IEntityTypeConfiguration<Subcategorie>
    {
        public void Configure(EntityTypeBuilder<Subcategorie> builder)
        {
            builder.Property(e => e.Id)
                .IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.CreatedAt)
                .IsRequired();
            builder.Property(e => e.UpdatedAt)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(4095)
                .IsRequired(false);
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.HasOne(e => e.Categorie)
                .WithMany(e => e.Subcategorii) 
                .HasForeignKey(e => e.CategoryId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

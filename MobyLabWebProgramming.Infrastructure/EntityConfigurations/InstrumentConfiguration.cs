using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations
{
    public class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
    {
        public void Configure(EntityTypeBuilder<Instrument> builder)
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
            builder.Property(e => e.Color)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.Price)
                .IsRequired();
            builder.Property(e => e.Reviews)
                .IsRequired(false);
            builder.HasOne(e => e.Subcategorie)
                .WithMany(e => e.Instruments)
                .HasForeignKey(e => e.SubcategorieId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.CosCumparaturi)
                .WithMany(e => e.Instruments)
                .HasForeignKey(e => e.CosId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

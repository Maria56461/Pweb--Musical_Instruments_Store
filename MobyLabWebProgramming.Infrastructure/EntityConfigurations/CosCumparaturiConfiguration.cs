using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class CosCumparaturiConfiguration : IEntityTypeConfiguration<CosCumparaturi>
{
    public void Configure(EntityTypeBuilder<CosCumparaturi> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(x => x.Id); // primary key
        builder.Property(e => e.TotalCost)
            .IsRequired();
        builder.Property(e => e.DeliveryCost)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
        builder.Property(e => e.UserId)
            .IsRequired();
        builder.HasAlternateKey(e => e.UserId);

        builder.HasOne(e => e.User) // This specifies a one-to-one relation
             .WithOne(e => e.CosCumparaturi)
             .HasForeignKey<CosCumparaturi>(e => e.UserId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
    }
}

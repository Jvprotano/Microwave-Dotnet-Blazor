using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Microwave.Core.Models;

namespace Microwave.Api.Data.Mappings;

public class PredefinedProgramMap : EntityBaseMap<PredefinedProgram>
{
    public override void Configure(EntityTypeBuilder<PredefinedProgram> builder)
    {
        builder.ToTable("PredefinedProgram");

        builder.Property(c => c.Name).HasMaxLength(50);

        builder.Property(c => c.Food).HasMaxLength(50);

        builder.Property(c => c.Instructions)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(c => c.LabelHeating)
            .HasColumnType("CHAR(1)")
            .HasMaxLength(1);

        builder.Property(c => c.IsPredefined).HasDefaultValue(false);

        base.Configure(builder);
    }
}
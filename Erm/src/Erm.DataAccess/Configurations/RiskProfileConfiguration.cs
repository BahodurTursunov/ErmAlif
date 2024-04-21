using Erm.src.Erm.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Erm.src.Erm.DataAccess.Configurations;

public sealed class RiskProfileConfiguration : IEntityTypeConfiguration<RiskProfile>
{
    public void Configure(EntityTypeBuilder<RiskProfile> builder)
    {
        builder.ToTable("RiskProfiles");

        builder
        .Property(bp => bp.Id)
        .HasColumnName("Id")
        .IsRequired();

        builder
        .Property(bp => bp.RiskName)
        .HasColumnName("RiskName")
        .HasColumnType("VARCHAR(50)")
        .IsRequired();

        builder
        .Property(bp => bp.Description)
        .HasColumnName("Description")
        .HasColumnType("VARCHAR(50)")
        .IsRequired();

        builder
        .Property(bp => bp.OccurrenceProbability)
        .HasColumnName("OccurrenceProbability")
        .HasColumnType("INTEGER")
        .IsRequired();

        builder
        .Property(bp => bp.PotentialBusinessImpact)
        .HasColumnName("PotentialBusinessImpact")
        .HasColumnType("INTEGER")
        .IsRequired();


        builder
        .HasOne(bp => bp.BusinessProcess)
        .WithMany(bp => bp.RiskProfiles)
        .HasForeignKey(fk => fk.BusinessProcessId)
        .IsRequired();

        builder
            .Property(bp => bp.BusinessProcessId)
            .HasColumnName("BusinessProcessId");

        builder.HasKey(k => k.Id);
    }
}
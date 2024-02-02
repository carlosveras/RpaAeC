using RpaAeC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RpaAeC.Data
{
    public class SearchResultEntityTypeConfiguration : IEntityTypeConfiguration<SearchTraining>
    {
        public void Configure(EntityTypeBuilder<SearchTraining> builder)
        {
            builder.ToTable("Training");

            builder
                .HasKey(r => r.Id)
                .IsClustered(false)
                .HasName("PK_Training");

            builder
                .Property(r => r.Id)
                .ValueGeneratedNever()
                .HasDefaultValueSql("newsequentialid()")
                .IsRequired();

            builder
                .Property(r => r.Titulo)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder
                .Property(r => r.Descricao)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder
                .Property(r => r.CargaHoraria)
                .HasColumnType("nvarchar(50)")
                .IsRequired(false);

            builder
                .Property(r => r.Professor)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
    }
}

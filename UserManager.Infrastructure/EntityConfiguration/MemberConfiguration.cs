using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManager.Domain.Entities;

namespace UserManager.Infrastructure.EntityConfiguration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.FistName).HasMaxLength(100).IsRequired();
            builder.Property(m => m.LastName).HasMaxLength(100).IsRequired();
            builder.Property(m => m.Gender).HasMaxLength(10).IsRequired();
            builder.Property(m => m.Email).HasMaxLength(150).IsRequired();
            builder.Property(m => m.IsActive).IsRequired();


            builder.HasData(
                new Member(1, "Enzo", "Ribeiro", "Masculino", "teste1@teste.com", true),
                new Member(2, "Valentina", "Silva", "FEminino", "teste2@teste.com", true)
                );
        }

        
    }
}

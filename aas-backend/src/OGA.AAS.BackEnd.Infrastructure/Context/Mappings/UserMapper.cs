using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGA.AAS.BackEnd.Domain.Entities;
using OGA.Core.BackEnd.Infrastructure.Mappings;

namespace OGA.AAS.BackEnd.Infrastructure.Context.Mappings
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("UserId");
            builder.Property(e => e.IdentifierTypeId).HasColumnName("IdentifierTypeId");
            builder.Property(e => e.Identifier).HasColumnName("Identifier");
            builder.Property(e => e.Surname).HasColumnName("Surname");
            builder.Property(e => e.SecondSurname).HasColumnName("SecondSurname");
            builder.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber");
            builder.Property(e => e.LanguageId).HasColumnName("LanguageId"); 
            builder.Property(e => e.PasswordHash).HasColumnName("PasswordHash");
            builder.Property(e => e.ActiveChk).HasColumnName("ActiveChk");
            builder.Property(e => e.Email).HasColumnName("Email");
            builder.Property(e => e.ApiKey).HasColumnName("ApiKey");
            builder.HasMany(e => e.UserRole).WithOne(x => x.User).HasForeignKey(e => new { e.Id });
            builder.HasMany(e => e.UserKeys).WithOne(x => x.User).HasForeignKey(e => new { e.IdUser });
            builder.HasOne(l => l.Language).WithMany().HasForeignKey(k => k.LanguageId);
            builder.HasOne(l => l.IdentifierType).WithMany().HasForeignKey(k => k.IdentifierTypeId);
            builder.Configure();
        }
    }
}

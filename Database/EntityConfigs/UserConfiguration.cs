using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigs;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(user => user.Id);
		builder.Property(user => user.UserName).HasMaxLength(20).IsRequired();
		builder.Property(user => user.Email).HasMaxLength(20).IsRequired(false);
		builder.Property(user => user.PhoneNumber).HasMaxLength(20).IsRequired(false);
		builder.Property(user => user.PasswordHash).IsRequired();
		builder.Property(user => user.RoleId).IsRequired();

		builder.HasOne(user => user.Role)
			   .WithMany()
			   .HasForeignKey(user => user.RoleId);
	}
}

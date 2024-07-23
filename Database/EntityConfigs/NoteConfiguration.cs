using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigs;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
	public void Configure(EntityTypeBuilder<Note> builder)
	{
		builder.HasKey(note => note.Id);
		builder.Property(note => note.FirstName).HasMaxLength(20).IsRequired(false);
		builder.Property(note => note.SecondName).HasMaxLength(20).IsRequired(false);
		builder.Property(note => note.ThirdName).HasMaxLength(20).IsRequired(false);
		builder.Property(note => note.PhoneNumber).HasMaxLength(20).IsRequired(false);
		builder.Property(note => note.Address).HasMaxLength(30).IsRequired(false);
		builder.Property(note => note.Description).HasMaxLength(50).IsRequired(false);
	}
}
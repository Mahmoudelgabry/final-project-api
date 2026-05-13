using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ChatMessageConfiguration
        : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(
            EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.Intent)
                .HasMaxLength(100);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("NOW()");

            builder.HasOne(x => x.Session)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
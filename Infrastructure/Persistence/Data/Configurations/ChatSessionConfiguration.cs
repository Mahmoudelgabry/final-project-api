using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ChatSessionConfiguration
        : IEntityTypeConfiguration<ChatSession>
    {
        public void Configure(
            EntityTypeBuilder<ChatSession> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(200);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("NOW()");

            builder.Property(x => x.LastActive)
                .HasDefaultValueSql("NOW()");

            builder.HasOne(x => x.User)
                .WithMany(x => x.ChatSessions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Messages)
                .WithOne(x => x.Session)
                .HasForeignKey(x => x.SessionId);
        }
    }
}
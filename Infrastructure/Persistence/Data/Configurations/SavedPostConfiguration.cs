using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class SavedPostConfiguration : IEntityTypeConfiguration<SavedPost>
    {
        public void Configure(EntityTypeBuilder<SavedPost> builder)
        {
            builder.Property(s => s.CreatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.HasOne(s => s.User)
                   .WithMany()
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Post)
                   .WithMany(p => p.SavedPosts)
                   .HasForeignKey(s => s.PostId);
                
            builder.HasIndex(s => new { s.UserId, s.PostId })
                .IsUnique();
        }
    }
}

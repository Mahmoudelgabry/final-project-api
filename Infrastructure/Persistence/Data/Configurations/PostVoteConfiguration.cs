using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class PostVoteConfiguration : IEntityTypeConfiguration<PostVote>
    {
        public void Configure(EntityTypeBuilder<PostVote> builder)
        {
            builder.HasOne(v => v.Post)
                   .WithMany(p => p.Votes)
                   .HasForeignKey(v => v.PostId);

            builder.HasOne(v => v.User)
                   .WithMany()
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(v => new { v.UserId, v.PostId })
               .IsUnique();
        }
    }
}

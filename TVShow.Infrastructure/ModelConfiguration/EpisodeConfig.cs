using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.Entity;

namespace TVShow.Infrastructure.ModelConfiguration
{
    public class EpisodeConfig : IEntityTypeConfiguration<Episodes>
    {
        public void Configure(EntityTypeBuilder<Episodes> builder)
        {
            builder.ToTable("Episode");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Season);
            builder.Property(x => x.Episode);
            builder.Property(x => x.Name);
            builder.Property(x => x.Air_date);
            builder.HasOne(x => x.TvShow)
                   .WithMany(p => p.Episodes)
                .HasForeignKey(x => x.TvShowId)
                .HasConstraintName("FK_TvShow_Episode");
        }
    }
}

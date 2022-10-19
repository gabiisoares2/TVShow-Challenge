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
    public class TvShowConfig : IEntityTypeConfiguration<TvShow>
    {
        public void Configure(EntityTypeBuilder<TvShow> builder)
        {
            builder.ToTable("Tvshow");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Permalink).IsRequired(false);
            builder.Property(x => x.Url);
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.Description_source).IsRequired(false);
            builder.Property(x => x.Start_date);
            builder.Property(x => x.End_date);
            builder.Property(x => x.Country).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(false);
            builder.Property(x => x.Runtime);
            builder.Property(x => x.Network).IsRequired(false);
            builder.Property(x => x.Youtube_link).IsRequired(false);
            builder.Property(x => x.Image_path).IsRequired(false);
            builder.Property(x => x.Image_thumbnail_path).IsRequired(false);
            builder.Property(x => x.Rating).IsRequired(false);
            builder.Property(x => x.Rating_count).IsRequired(false);
            builder.Property(x => x.Countdown).IsRequired(false);
        }
    }
}

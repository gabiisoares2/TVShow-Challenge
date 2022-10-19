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
    public class PictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("Picture");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Uri);
            builder.HasOne(x => x.TvShow)
                   .WithMany(p => p.Pictures)
                .HasForeignKey(x => x.TvShowId)
                .HasConstraintName("FK_TvShow_Picture");
        }
    }
}

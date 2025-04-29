using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Configurations
{
    public class DoctorsConfig : IEntityTypeConfiguration<Doctors>
    {
        public void Configure(EntityTypeBuilder<Doctors> builder)
        {
            builder.Property(D => D.Dr_Code)
                 .IsRequired()
                 .HasMaxLength(30);

            builder.Property(D => D.Dr_NameAr)
               .IsRequired();

            builder.Property(D => D.Dr_NameEn)
              .IsRequired();

            builder.Property(P => P.Phone)
               .HasMaxLength(20);
        }
    }
}

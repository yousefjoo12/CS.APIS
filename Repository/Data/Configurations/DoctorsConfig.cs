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
            builder.HasKey(d => d.ID);

            builder.Property(d => d.Dr_Code).IsRequired()
                .HasMaxLength(50);
            builder.Property(d => d.Dr_NameAr)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(d => d.Dr_NameEn)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(d => d.Phone)
                .HasMaxLength(15);
        }
    }
}

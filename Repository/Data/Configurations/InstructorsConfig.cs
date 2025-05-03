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
    public class InstructorsConfig : IEntityTypeConfiguration<Instructors>
    {
        public void Configure(EntityTypeBuilder<Instructors> builder)
        {
            

            builder.Property(i => i.Ins_Code)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(i => i.Ins_NameAr)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(i => i.Ins_NameEn)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(i => i.Phone)
                .HasMaxLength(15);
        }
    }
}

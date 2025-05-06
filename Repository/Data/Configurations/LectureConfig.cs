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
    public class LectureConfig : IEntityTypeConfiguration<Lecture_S>
    {
        public void Configure(EntityTypeBuilder<Lecture_S> builder)
        {
            builder.HasKey(l => l.ID);

            builder.Property(l => l.Lecture_Num).HasMaxLength(50);

            //builder.HasOne(l => l.Rooms)
            //       .WithMany()
            //       .HasForeignKey(l => l.Room_ID)
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(l => l.Subjects)
            //       .WithMany()
            //       .HasForeignKey(l => l.Sub_ID)
            //       .OnDelete(DeleteBehavior.Cascade);
            
            //builder.HasOne(l => l.Students)
            //       .WithMany()
            //       .HasForeignKey(l => l.St_ID)
            //       .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.LectureDate)
                   .IsRequired();

            builder.Property(l => l.Degree)
                   .IsRequired();


        }
    }
}

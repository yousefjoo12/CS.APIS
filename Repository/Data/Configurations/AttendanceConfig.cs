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
    public class AttendanceConfig : IEntityTypeConfiguration<Attendance_T>
    {
        public void Configure(EntityTypeBuilder<Attendance_T> builder)
        {

            builder.HasKey(a => a.ID);

            builder.Property(a => a.Atten)
                   .IsRequired();

            //builder.HasOne(a => a.Lecture)
            //       .WithMany()
            //       .HasForeignKey(a => a.LectureID)
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(a => a.Students)
            //       .WithMany()
            //       .HasForeignKey(a => a.St_ID)
            //       .OnDelete(DeleteBehavior.Cascade);

            builder.Property<string>("Sub_Name")
                   .HasMaxLength(200)
                   .IsRequired(false);


        }
    }
}

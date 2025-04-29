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
    public class Studets_RoomsConfig : IEntityTypeConfiguration<Studets_Rooms>
    {
        public void Configure(EntityTypeBuilder<Studets_Rooms> builder)
        {
            builder.HasOne(L => L.Students)
                 .WithMany()
                 .HasForeignKey(L => L.St_ID);

            builder.HasOne(L => L.Rooms)
                 .WithMany()
                 .HasForeignKey(L => L.Room_ID);
        }
    }
}

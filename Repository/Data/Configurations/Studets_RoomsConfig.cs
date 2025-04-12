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
            throw new NotImplementedException();
        }
    }
}

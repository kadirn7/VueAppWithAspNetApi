using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VueAppWithAspNetApiServer.Domain.Entities;

namespace VueAppWithAspNetApi.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            User user1 = new()
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "kadir@kadir.com",
                Password="Kadir123"
            };

            builder.HasData(user1);
        }
    }
}
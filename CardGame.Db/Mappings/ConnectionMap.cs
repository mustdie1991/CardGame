using System;
using System.Collections.Generic;
using System.Text;
using CardGame.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame.Db.Mappings
{
    public class ConnectionMap : IEntityTypeConfiguration<Connection>
    {
        public void Configure(EntityTypeBuilder<Connection> builder)
        {
            builder.HasKey(x => x.EntityId);
            builder.Property(x => x.EntityId).HasColumnName("ConnectionGuid");
        }
    }
}

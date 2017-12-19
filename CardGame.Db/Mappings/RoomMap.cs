using CardGame.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame.Db.Mappings
{
    public class RoomMap: IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.EntityId);

            builder.Property(x => x.EntityId).HasColumnName("RoomId");

            
        }
    }
}

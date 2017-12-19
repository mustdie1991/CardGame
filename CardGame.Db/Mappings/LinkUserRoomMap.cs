using CardGame.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame.Db.Mappings
{
    public class LinkUserRoomMap : IEntityTypeConfiguration<LinkUserRoom>
    {
        public void Configure(EntityTypeBuilder<LinkUserRoom> builder)
        {
            builder.HasKey(x => new {x.GameTableId, x.UserId});

            builder.HasOne(x => x.User);
            builder.HasOne(x => x.GameRoom).WithMany(x => x.ConnectedUsers);
        }
    }
}

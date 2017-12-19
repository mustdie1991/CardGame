using CardGame.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardGame.Db.Mappings
{
    public class GameCardMap: IEntityTypeConfiguration<GameCard>
    {
        public void Configure(EntityTypeBuilder<GameCard> builder)
        {
            builder.HasKey(x => x.EntityId);

            builder.Property(x => x.EntityId).HasColumnName("GameCardId");
        }
    }
}
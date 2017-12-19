using System;
using System.Linq;
using System.Reflection;
using CardGame.Db.Entities;
using CardGame.Db.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CardGame.Db.Contexts
{
    public class MainDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<GameCard> GameCards { get; set; }

        public DbSet<Connection> Connections { get; set; }

        public DbSet<LinkUserRoom> LinkUserRooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CardGameDb;Integrated Security=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameCardMap());
            modelBuilder.ApplyConfiguration(new ConnectionMap());
            modelBuilder.ApplyConfiguration(new RoomMap());
            modelBuilder.ApplyConfiguration(new LinkUserRoomMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

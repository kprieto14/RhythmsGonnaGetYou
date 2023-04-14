using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    public class MusicDatabaseContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=NewMusicDatabase");
        }
    }
}
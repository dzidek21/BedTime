using BedTime.Models;

using Microsoft.EntityFrameworkCore;

namespace BedTime.Entity
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TailModel> Tails { get; set; }
        public DbSet<LikedStory> LikedStories { get; set; }
        public DatabaseContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tails.db3");
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
            //C:\Users\kamil\AppData\Local\Packages\com.companyname.movieapp_9zz4h110yvjzm\LocalState
            //data/user/0/com.companyname.bedtime/files/tails.db3
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

    }
}

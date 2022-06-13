using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static DataAccess.Data.EntitiesConfiguration;

namespace DataAccess.Data
{
    public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new HobbyConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new GuideConfiguration());
            builder.ApplyConfiguration(new PhotoConfiguration<Hobby>());
            builder.ApplyConfiguration(new PhotoConfiguration<Event>());
            builder.ApplyConfiguration(new UserPhotoConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new UserHobbyConfiguration());
            builder.ApplyConfiguration(new UserEventConfiguration());
            builder.ApplyConfiguration(new CategoryHobbyConfiguration());
        }

        public DbSet<Hobby>? Hobbies { get; set; }
        public DbSet<Event>? Events { get; set; }
        public DbSet<Guide>? Guides { get; set; }
        public DbSet<Photo<Hobby>>? PhotosHobby { get; set; }
        public DbSet<Photo<Event>>? PhotosEvent { get; set; }
        public DbSet<Photo<User>>? UserPhotos { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Post>? Posts { get; set; }
        





    }
}

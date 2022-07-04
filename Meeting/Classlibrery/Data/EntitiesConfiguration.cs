using DataAccess.Data.Entities;
using DataAccess.Data.Entities.Bridge_Entities;
using DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data
{
    internal static class EntitiesConfiguration
    {
        internal class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id).ValueGeneratedOnAdd();
                builder.Property(u => u.FirstName).IsRequired();
                builder.Property(u => u.Email).IsRequired();
                builder.Property(u => u.UserName).IsRequired();
                builder.Property(u => u.DateOfBirth).IsRequired();
                builder.Property(u => u.DateCreated).IsRequired();
                builder.Property(u => u.DateModified).IsRequired();
                builder.Property(u => u.Country).IsRequired();
                builder.Property(u => u.City).IsRequired();
                builder.HasMany(u => u.EventsCreated).WithOne(ue => ue.Creator).HasForeignKey(ue => ue.EventCreatorId);
                builder.HasMany(u => u.Guides).WithOne(ue => ue.Creator).HasForeignKey(ue => ue.CreatorId);
                builder.HasOne(u => u.Photo).WithOne(ue => ue.Entity).HasForeignKey<Photo<User>>(ue => ue.EntityId);
            }
        }
        internal class HobbyConfiguration : IEntityTypeConfiguration<Hobby>
        {
            public void Configure(EntityTypeBuilder<Hobby> builder)
            {
                builder.HasKey(h => h.Id);
                builder.Property(h => h.Id).ValueGeneratedOnAdd();
                builder.Property(h => h.HobbyName).IsRequired();
                builder.Property(h => h.Description).IsRequired();
                builder.Property(h => h.Rules).IsRequired();
                builder.Property(h => h.KeyFeatures).IsRequired();
                builder.HasMany(h => h.Events).WithOne(e => e.Hobby).HasForeignKey(e => e.HobbyId);
                builder.HasMany(h => h.Guides).WithOne(e => e.Hobby).HasForeignKey(e => e.HobbyId);
                builder.HasOne(u => u.Photo).WithOne(ue => ue.Entity).HasForeignKey<Photo<Hobby>>(ue => ue.EntityId);
                builder.HasOne(h => h.Category).WithMany(h => h.Hobbies).HasForeignKey(h => h.CategoryId);


            }
        }
        internal class EventConfiguration : IEntityTypeConfiguration<Event>
        {
            public void Configure(EntityTypeBuilder<Event> builder)
            {
                builder.HasKey(e => e.Id);
                builder.Property(e => e.Id).ValueGeneratedOnAdd();
                builder.Property(e => e.EventTitle).IsRequired();
                builder.Property(e => e.EventDescription).IsRequired();
                builder.Property(e => e.EventDate).IsRequired();
                builder.Property(e => e.EventCreated).IsRequired();
                builder.Property(e => e.EventLocation).IsRequired();
                builder.HasOne(e => e.Hobby).WithMany(h => h.Events).HasForeignKey(e => e.HobbyId);
                builder.HasOne(e => e.Creator).WithMany(u => u.EventsCreated).HasForeignKey(e => e.EventCreatorId);
                builder.HasMany(e => e.Photos).WithOne(e => e.Entity).HasForeignKey(e => e.EntityId);
            }

        }
        internal class GuideConfiguration : IEntityTypeConfiguration<Guide>
        {
            public void Configure(EntityTypeBuilder<Guide> builder)
            {
                builder.HasKey(g => g.Id);
                builder.Property(g => g.Id).ValueGeneratedOnAdd();
                builder.Property(g => g.Title).IsRequired();
                builder.Property(g => g.Content).IsRequired();
                builder.Property(g => g.CreationDate).IsRequired();
                builder.HasOne(g => g.Hobby).WithMany(h => h.Guides).HasForeignKey(g => g.HobbyId);
                builder.HasOne(g => g.Creator).WithMany(u => u.Guides).HasForeignKey(g => g.CreatorId);
            }
        }
        internal class PhotoConfiguration<T> : IEntityTypeConfiguration<Photo<T>> where T : class, IPhotoble<T>
        {
            public void Configure(EntityTypeBuilder<Photo<T>> builder)
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Property(p => p.PhotoUrl).IsRequired();
                builder.Property(p => p.PublicId).IsRequired();
                builder.HasOne(p => p.Entity).WithMany(u => u.Photos).HasForeignKey(p => p.EntityId);
                builder.HasOne(p => p.Uploader);
            }
        }
        internal class HobbyPhotoConfiguration : IEntityTypeConfiguration<Photo<Hobby>>
        {
            public void Configure(EntityTypeBuilder<Photo<Hobby>> builder)
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Property(p => p.PhotoUrl).IsRequired();
                builder.Property(p => p.PublicId).IsRequired();
                builder.HasOne(p => p.Entity).WithOne(u => u.Photo);
                builder.HasOne(p => p.Uploader);
            }
        }
        internal class UserPhotoConfiguration : IEntityTypeConfiguration<Photo<User>>
        {
            public void Configure(EntityTypeBuilder<Photo<User>> builder)
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Property(p => p.PhotoUrl).IsRequired();
                builder.Property(p => p.PublicId).IsRequired();
                builder.HasOne(p => p.Entity).WithOne(u => u.Photo);
                builder.HasOne(p => p.Uploader);
            }
        }
        internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Id).ValueGeneratedOnAdd();
                builder.Property(c => c.CategoryName).IsRequired();
                builder.Property(c => c.Description).IsRequired();
            }
        }
        internal class PostConfiguration : IEntityTypeConfiguration<Post>
        {
            public void Configure(EntityTypeBuilder<Post> builder)
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Property(p => p.Content).IsRequired();
                builder.Property(p => p.DateOfCreation).IsRequired();
                builder.HasOne(p => p.Creator).WithMany(u => u.Posts).HasForeignKey(p => p.CreatorId);
            }
        }
        internal class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
        {
            public void Configure(EntityTypeBuilder<UserEvent> builder)
            {
                builder.HasKey(ue => ue.Id);
                builder.Property(ue => ue.Id).ValueGeneratedOnAdd();
                builder.HasOne(ue => ue.User).WithMany(u => u.EventsAttend).HasForeignKey(ue => ue.UserId);
                builder.HasOne(ue => ue.Event).WithMany(e => e.Users).HasForeignKey(ue => ue.EventId);
            }
        }
        internal class UserHobbyConfiguration : IEntityTypeConfiguration<UserHobby>
        {
            public void Configure(EntityTypeBuilder<UserHobby> builder)
            {
                builder.HasKey(uh => uh.Id);
                builder.Property(uh => uh.Id).ValueGeneratedOnAdd();
                builder.HasOne(uh => uh.User).WithMany(u => u.Hobbies).HasForeignKey(uh => uh.UserId);
                builder.HasOne(uh => uh.Hobby).WithMany(h => h.Users).HasForeignKey(uh => uh.HobbyId);
            }
        }
        //internal class CategoryHobbyConfiguration : IEntityTypeConfiguration<CategoryHobby>
        //{
        //    public void Configure(EntityTypeBuilder<CategoryHobby> builder)
        //    {
        //        builder.HasKey(ch => ch.Id);
        //        builder.Property(ch => ch.Id).ValueGeneratedOnAdd();
        //        builder.HasOne(ch => ch.Category).WithMany(c => c.Hobbies).HasForeignKey(ch => ch.CategoryId);
        //        builder.HasOne(ch => ch.Hobby).WithMany(h => h.Categories).HasForeignKey(ch => ch.HobbyId);
        //    }
        //}




    }
}

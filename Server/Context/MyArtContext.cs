using Server.Model;
using Shared.Core.Context;
using Shared.Dtos.Galleries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Context
{
    public class MyArtContext : DbContext, IDatabaseContext
    {
        private const string DB_CONTEXT_NAME = "MyArt";

        public DbSet<User> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<UserDefinable> UserDefinables { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        public MyArtContext()
            : base(DB_CONTEXT_NAME)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>()
            //    .Map<BlogCategory>(m => m.Requires("Discriminator").HasValue(BlogCategory.DISC))
            //    .Map<LinkCategory>(m => m.Requires("Discriminator").HasValue(LinkCategory.DISC))
            //    .Map<ProductCategory>(m => m.Requires("Discriminator").HasValue(ProductCategory.DISC));

            //modelBuilder.Entity<BaseEvent>()
            //    .Map<Event>(m => m.Requires("Discriminator").HasValue(Event.DISC))
            //    .Map<Seminar>(m => m.Requires("Discriminator").HasValue(Seminar.DISC));

            //modelBuilder.Entity<BaseLink>()
            //    .Map<Link>(m => m.Requires("Discriminator").HasValue(Link.DISC))
            //    .Map<Video>(m => m.Requires("Discriminator").HasValue(Video.DISC));

            modelBuilder.Entity<Painting>()
                .HasMany(x => x.Users)
                .WithMany(y => y.Paintings)
                .Map(m =>
                {
                    m.ToTable("JOIN_USER_PAINTING");
                    m.MapLeftKey("PaintingId");
                    m.MapRightKey("UserId");
                }
            );

            modelBuilder.Entity<Exhibition>()
                .HasMany(x => x.Paintings)
                .WithMany(y => y.Exhibitions)
                .Map(m =>
                {
                    m.ToTable("JOIN_PAINTING_EXHIBITION");
                    m.MapLeftKey("ExhibitionId");
                    m.MapRightKey("PaintingId");
                }
            );

            modelBuilder.Entity<Gallery>()
                .Map<ProfileGallery>(m => m.Requires("Discriminator").HasValue(ProfileGalleryDto.DISC))
                .Map<AlbumGallery>(m => m.Requires("Discriminator").HasValue(AlbumGalleryDto.DISC));

            base.OnModelCreating(modelBuilder);
        }
    }
}

using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackId.Common.Constants;
using TrackId.Data.Entities;

namespace TrackId.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public virtual DbSet<Track> Tracks { get; set; }

        public virtual DbSet<Artist> Artists { get; set; }

        public virtual DbSet<ArtistTrack> ArtistTracks { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
            SeedUserRoles(modelBuilder);
            SeedTbaArtist(modelBuilder);
            SeedGenres(modelBuilder);

            modelBuilder.Entity<ArtistTrack>()
                .HasOne(x => x.Artist)
                .WithMany(x => x.Tracks)
                .HasForeignKey(x => x.ArtistId);

            modelBuilder.Entity<ArtistTrack>()
                .HasOne(x => x.Track)
                .WithMany(x => x.Artists)
                .HasForeignKey(x => x.TrackId);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            var user = new ApplicationUser
            {
                Id = Guid.Parse("8A24BFA9-26FE-4BD1-9D84-AB2A83D6F2FB"),
                UserName = "Admin",
                Email = "emileverbunt@gmail.com",
                LockoutEnabled = false,
                NormalizedEmail = "emileverbunt@gmail.com",
                NormalizedUserName = "admin",
                CreateDateTime = DateTime.UtcNow
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin*123");

            modelBuilder.Entity<ApplicationUser>().HasData(user);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.Parse("B3E78BD1-5FA8-4DD9-AC80-19063DBC82E1"), Name = "admin", NormalizedName = "Admin" },
                new Role { Id = Guid.Parse("BBA64C5F-A693-4F88-96A3-7207018BC14E"), Name = "user", NormalizedName = "User" },
                new Role { Id = Guid.Parse("F8DBB603-A0F4-4B38-A1D6-CCB370D58586"), Name = "artist", NormalizedName = "Artist" }
            );
        }

        private static void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasData(new UserRole
                {
                    RoleId = Guid.Parse("B3E78BD1-5FA8-4DD9-AC80-19063DBC82E1"),
                    UserId = Guid.Parse("8A24BFA9-26FE-4BD1-9D84-AB2A83D6F2FB")
                });
        }

        private static void SeedTbaArtist(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasData(new Artist
                {
                    Id = ArtistConstants.TbaGuid,
                    Name = ArtistConstants.TbaName,
                    CreateDateTime = DateTime.UtcNow,
                    IsDeleted = false,
                });
        }

        private static void SeedGenres(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasData(new Genre[]
                {
                    new Genre
                    {
                        Name = "Hardstyle",
                        Id = Guid.Parse("2443687E-B3E6-49AA-87EC-4AD9E7EAE6BD"),
                        ParentGenreId = Guid.Parse("1AF4AF53-05A4-4934-B8B1-758D9750F8D9"),
                        Description = "Hardstyle",
                        CreateDateTime = DateTime.UtcNow
                    },
                    new Genre
                    {
                        Name = "Raw hardstyle",
                        Id = Guid.Parse("FAC53697-439F-48FD-9050-832A981ADF2C"),
                        ParentGenreId = Guid.Parse("2443687E-B3E6-49AA-87EC-4AD9E7EAE6BD"),
                        Description = "Hardstyle",
                        CreateDateTime = DateTime.UtcNow
                    },
                    new Genre
                    {
                        Name = "EDM",
                        Id = Guid.Parse("1AF4AF53-05A4-4934-B8B1-758D9750F8D9"),
                        Description = "Electronic dance music",
                        CreateDateTime = DateTime.UtcNow
                    },
                });
        }
    }
}

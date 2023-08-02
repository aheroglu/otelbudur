using Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = HP\\SQLEXPRESS; Database = OtelBudur; Integrated Security = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(x => x.Room)
                .WithMany(y => y.Bookings)
                .HasForeignKey(z => z.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomImage>()
                .HasOne(x => x.Room)
                .WithMany(y => y.RoomImages)
                .HasForeignKey(z => z.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomRating>()
                .HasOne(x => x.Room)
                .WithMany(y => y.Ratings)
                .HasForeignKey(z => z.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<EmailsSent> EmailsSents { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelOwnerRequest> HotelOwnerRequests { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<RoomRating> RoomRatings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<SocialAccount> SocialAccounts { get; set; }
    }
}

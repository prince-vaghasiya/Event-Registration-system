using Microsoft.EntityFrameworkCore;

namespace EventRegistrationSystem.Models
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName);
            modelBuilder.Entity<Event>()
                .HasMany(e => e.FormFields)
                .WithOne(ff => ff.Event)
                .HasForeignKey(ff => ff.EventID);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Events)
                .WithOne(ff => ff.Organizer)
                .HasForeignKey(ff => ff.OrganizerUserID);

        }
    }
}

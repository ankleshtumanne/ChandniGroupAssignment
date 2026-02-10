using B2BManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agents");
                entity.HasKey(e => e.AgentID);
                entity.Property(e => e.AgentID)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.CompanyName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.ContactPerson)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasIndex(e => e.Email)
                      .IsUnique();
                entity.Property(e => e.Phone)
                      .IsRequired()
                      .HasMaxLength(20);
                entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(255);
                entity.Property(e => e.ApiKey)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasIndex(e => e.ApiKey)
                      .IsUnique();
                entity.Property(e => e.RegisteredOn)
                      .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<HotelBooking>(entity =>
            {
                entity.ToTable("HotelBookings");

                entity.HasKey(e => e.BookingID);

                entity.Property(e => e.BookingID)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.AgentID)
                      .IsRequired();

                entity.Property(e => e.HotelID)
                      .HasMaxLength(50);

                entity.Property(e => e.HotelName)
                      .HasMaxLength(200);

                entity.Property(e => e.City)
                      .HasMaxLength(100);

                entity.Property(e => e.TotalPrice)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.BookingStatus)
                      .HasMaxLength(50);

                entity.Property(e => e.CreatedOn)
                      .HasDefaultValueSql("GETDATE()");

                // ?? Foreign Key
                entity.HasOne(e => e.Agent)
                      .WithMany()
                      .HasForeignKey(e => e.AgentID)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLogs");

                entity.HasKey(e => e.AuditID);

                entity.Property(e => e.AuditID)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Action)
                      .HasMaxLength(100);

                entity.Property(e => e.Description)
                      .HasMaxLength(500);

                entity.Property(e => e.CreatedOn)
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}

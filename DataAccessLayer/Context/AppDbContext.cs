using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<DonationCase> DonationCases { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportImage> ReportImages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwClosedDonationCase> VwClosedDonationCases { get; set; }

    public virtual DbSet<VwCompletedDonationCase> VwCompletedDonationCases { get; set; }

    public virtual DbSet<VwDonationsByCase> VwDonationsByCases { get; set; }

    public virtual DbSet<VwOpenDonationCase> VwOpenDonationCases { get; set; }

    public virtual DbSet<SupportMessage> SupportMessages { get; set; }

    public virtual DbSet<SupportTicket> SupportTicket { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
          optionsBuilder.UseSqlServer("Name=DefaultConnection");
        //optionsBuilder.UseLazyLoadingProxies();
    }
       

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BD072F12E");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Cities__F2D21A96B4BF4909");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(100);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAAF221CA1D");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Report).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Reports");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.DonationsId).HasName("PK__Donation__F72442451127423A");

            entity.Property(e => e.DonationsId).HasColumnName("DonationsID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DonationCaseId).HasColumnName("DonationCaseID");
            entity.Property(e => e.DonorName).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.DonationCase).WithMany(p => p.Donations)
                .HasForeignKey(d => d.DonationCaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donations_Cases");

            entity.HasOne(d => d.User).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Donations_Users");
        });

        modelBuilder.Entity<DonationCase>(entity =>
        {
            entity.HasKey(e => e.DonationCaseId).HasName("PK__Donation__6001A740E78A1666");

            entity.Property(e => e.DonationCaseId).HasColumnName("DonationCaseID");
            entity.Property(e => e.ClosedAt).HasColumnType("datetime");
            entity.Property(e => e.CollectedAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GoalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OpenedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Open");

            entity.HasOne(d => d.Report).WithMany(p => p.DonationCases)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonationCases_Reports");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48E5FCB91E98");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EstimatedCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Title).HasMaxLength(150);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Category).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reports_Categories");

            entity.HasOne(d => d.City).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reports_Cities");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reports_Users");
        });

        modelBuilder.Entity<ReportImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ReportIm__7516F4EC36AAF149");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.ReportId).HasColumnName("ReportID");

            entity.HasOne(d => d.Report).WithMany(p => p.ReportImages)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ReportImages_Reports");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC51AC2B7B");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343DB64C2D").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<VwClosedDonationCase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ClosedDonationCases");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.ClosedAt).HasColumnType("datetime");
            entity.Property(e => e.CollectedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonationCaseId).HasColumnName("DonationCaseID");
            entity.Property(e => e.GoalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReportTitle).HasMaxLength(150);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<VwCompletedDonationCase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_CompletedDonationCases");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.ClosedAt).HasColumnType("datetime");
            entity.Property(e => e.CollectedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonationCaseId).HasColumnName("DonationCaseID");
            entity.Property(e => e.GoalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReportTitle).HasMaxLength(150);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<VwDonationsByCase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DonationsByCase");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CollectedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonatedAt).HasColumnType("datetime");
            entity.Property(e => e.DonationCaseId).HasColumnName("DonationCaseID");
            entity.Property(e => e.DonationsId).HasColumnName("DonationsID");
            entity.Property(e => e.Donor).HasMaxLength(201);
            entity.Property(e => e.GoalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReportTitle).HasMaxLength(150);
        });

        modelBuilder.Entity<VwOpenDonationCase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_OpenDonationCases");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CollectedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DonationCaseId).HasColumnName("DonationCaseID");
            entity.Property(e => e.GoalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RemainingAmount).HasColumnType("decimal(19, 2)");
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.ReportTitle).HasMaxLength(150);
            entity.Property(e => e.ReporterName).HasMaxLength(201);
            entity.Property(e => e.Status).HasMaxLength(50);
        });
        modelBuilder.Entity<User>()
    .HasOne(u => u.City)
    .WithMany(c => c.Users)
    .HasForeignKey(u => u.CityId);


        modelBuilder.Entity<SupportMessage>()
    .HasOne(m => m.Ticket)
    .WithMany(t => t.Messages)
    .HasForeignKey(m => m.TicketId)
    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SupportMessage>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

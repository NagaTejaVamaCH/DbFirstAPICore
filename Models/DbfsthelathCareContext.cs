using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dbhealthcare.Models;

public partial class DbfsthelathCareContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbfsthelathCareContext(DbContextOptions<DbfsthelathCareContext> options, IConfiguration configuration)
        : base(options)
    {
        this._configuration = configuration;
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ConnHealthcare"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__applicat__3213E83F0C2F11CB");

            entity.ToTable("application");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.AppDesc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("appDesc");
            entity.Property(e => e.AppName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("appName");
            entity.Property(e => e.Clientid).HasColumnName("clientid");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Application)
                .HasForeignKey<Application>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__applicati__clien__398D8EEE");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clients__3213E83F92D7AA30");

            entity.ToTable("clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__template__3213E83FF1001CAA");

            entity.ToTable("templates");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.TempName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tempName");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Template)
                .HasForeignKey<Template>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__templates__appId__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

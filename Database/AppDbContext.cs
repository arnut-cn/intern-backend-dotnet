using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InternBackendC_.Database;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<employee> employees { get; set; }

    public virtual DbSet<phone> phones { get; set; }

    public virtual DbSet<position> positions { get; set; }

    public virtual DbSet<team> teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=app.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<employee>(entity =>
        {
            entity.HasKey(e => e.employee_id);

            entity.ToTable("employee");

            entity.Property(e => e.employee_id).HasColumnType("varchar");
            entity.Property(e => e.date_of_birth).IsRequired();
            entity.Property(e => e.email).IsRequired();
            entity.Property(e => e.firstname).IsRequired();
            entity.Property(e => e.lastname).IsRequired();
            entity.Property(e => e.position_id).HasColumnType("varchar");
            entity.Property(e => e.team_id).HasColumnType("varchar");

            entity.HasOne(d => d.position).WithMany(p => p.employees).HasForeignKey(d => d.position_id);

            entity.HasOne(d => d.team).WithMany(p => p.employees).HasForeignKey(d => d.team_id);
        });

        modelBuilder.Entity<phone>(entity =>
        {
            entity.HasKey(e => e.phone_id);

            entity.ToTable("phone");

            entity.Property(e => e.phone_id).HasColumnType("varchar");
            entity.Property(e => e.employee_id).HasColumnType("varchar");
            entity.Property(e => e.phone_number).IsRequired();

            entity.HasOne(d => d.employee).WithMany(p => p.phones).HasForeignKey(d => d.employee_id);
        });

        modelBuilder.Entity<position>(entity =>
        {
            entity.HasKey(e => e.position_id);

            entity.ToTable("position");

            entity.Property(e => e.position_id).HasColumnType("varchar");
        });

        modelBuilder.Entity<team>(entity =>
        {
            entity.HasKey(e => e.team_id);

            entity.ToTable("team");

            entity.Property(e => e.team_id).HasColumnType("varchar");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

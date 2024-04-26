using System;
using System.Collections.Generic;
using BlazorLearn_ExampleApi.Databases.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorLearn_ExampleApi.Databases;

public partial class AuthExampleContext : DbContext
{
    public AuthExampleContext()
    {
    }

    public AuthExampleContext(DbContextOptions<AuthExampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SystemRole> SystemRoles { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<SystemUserGoogleMap> SystemUserGoogleMaps { get; set; }

    public virtual DbSet<SystemUserRole> SystemUserRoles { get; set; }

    public virtual DbSet<SystemUserSecret> SystemUserSecrets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SystemRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemRo__3214EC07DDEFDA2D");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemUs__3214EC07012D1CB4");

            entity.Property(e => e.Name).HasDefaultValue("");
        });

        modelBuilder.Entity<SystemUserGoogleMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemUs__3214EC07133253B3");

            entity.HasOne(d => d.User).WithOne(p => p.SystemUserGoogleMaps)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_UserId");
        });

        modelBuilder.Entity<SystemUserRole>(entity =>
        {
            entity.HasOne(d => d.Role).WithMany(p => p.SystemUserRoles).HasConstraintName("FK__SystemUse__RoleI__2B3F6F97");

            entity.HasOne(d => d.User).WithMany(p => p.SystemUserRoles).HasConstraintName("FK__SystemUse__UserI__2A4B4B5E");
        });

        modelBuilder.Entity<SystemUserSecret>(entity =>
        {
            entity.HasOne(d => d.User).WithOne(p => p.SystemUserSecret);//.WithMany(p => p.SystemUserSecrets).HasConstraintName("FK__SystemUse__UserI__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

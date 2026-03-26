using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WorkLine.Models;

namespace WorkLine.BdContextWorkLine;

public partial class WorkLineContext : DbContext
{
    public WorkLineContext()
    {
    }

    public WorkLineContext(DbContextOptions<WorkLineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Setor> Setors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WorkLineDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__Cargo__6C98562527C395F5");

            entity.Property(e => e.IdCargo).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.IdFuncionario).HasName("PK__Funciona__35CB052AD69F8037");

            entity.Property(e => e.IdFuncionario).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Funcionarios).HasConstraintName("FK__Funcionar__IdCar__656C112C");

            entity.HasOne(d => d.IdSetorNavigation).WithMany(p => p.Funcionarios).HasConstraintName("FK__Funcionar__IdSet__6477ECF3");
        });

        modelBuilder.Entity<Setor>(entity =>
        {
            entity.HasKey(e => e.IdSetor).HasName("PK__Setor__113E4B9EC7BC328B");

            entity.Property(e => e.IdSetor).HasDefaultValueSql("(newid())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VendingMachineWebAPINet5.Models
{
    public partial class VendingmachinefordrinksContext : DbContext
    {
        public VendingmachinefordrinksContext()
        {
        }

        public VendingmachinefordrinksContext(DbContextOptions<VendingmachinefordrinksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<VendingMachine> VendingMachines { get; set; }
        public virtual DbSet<VendingMachineCoin> VendingMachineCoins { get; set; }
        public virtual DbSet<VendingMachineDrink> VendingMachineDrinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Vending machine for drinks;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Coin>(entity =>
            {
                entity.ToTable("Coin");
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.ToTable("Drink");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<VendingMachine>(entity =>
            {
                entity.ToTable("VendingMachine");
            });

            modelBuilder.Entity<VendingMachineCoin>(entity =>
            {
                entity.ToTable("VendingMachineCoin");

                entity.Property(e => e.IdCoin).HasColumnName("Id_Coin");

                entity.Property(e => e.IdVendingMachine).HasColumnName("Id_VendingMachine");

                entity.HasOne(d => d.IdCoinNavigation)
                    .WithMany(p => p.VendingMachineCoins)
                    .HasForeignKey(d => d.IdCoin)
                    .HasConstraintName("FK__VendingMa__Id_Co__5165187F");

                entity.HasOne(d => d.IdVendingMachineNavigation)
                    .WithMany(p => p.VendingMachineCoins)
                    .HasForeignKey(d => d.IdVendingMachine)
                    .HasConstraintName("FK__VendingMa__Id_Ve__52593CB8");
            });

            modelBuilder.Entity<VendingMachineDrink>(entity =>
            {
                entity.ToTable("VendingMachineDrink");

                entity.Property(e => e.IdDrink).HasColumnName("Id_Drink");

                entity.Property(e => e.IdVendingMachine).HasColumnName("Id_VendingMachine");

                entity.HasOne(d => d.IdDrinkNavigation)
                    .WithMany(p => p.VendingMachineDrinks)
                    .HasForeignKey(d => d.IdDrink)
                    .HasConstraintName("FK__VendingMa__Id_Dr__534D60F1");

                entity.HasOne(d => d.IdVendingMachineNavigation)
                    .WithMany(p => p.VendingMachineDrinks)
                    .HasForeignKey(d => d.IdVendingMachine)
                    .HasConstraintName("FK__VendingMa__Id_Ve__5441852A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

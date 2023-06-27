﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpaceTruckerCompany.API.Data;

#nullable disable

namespace SpaceTruckerCompany.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230627122054_AddShipsAndItems")]
    partial class AddShipsAndItems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfShips")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.SpaceShip", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Cargo")
                        .HasColumnType("int");

                    b.Property<int>("Fuel")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxCargo")
                        .HasColumnType("int");

                    b.Property<int>("MaxFuel")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SpaceShips");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.SpaceShipEntry", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CargoSpace")
                        .HasColumnType("int");

                    b.Property<double>("Credits")
                        .HasColumnType("float");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShipId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UsedCargoSpace")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ShipId");

                    b.ToTable("SpaceShipEntries");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.TradeItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BasePrice")
                        .HasColumnType("int");

                    b.Property<int>("BuyPrice")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SellPrice")
                        .HasColumnType("int");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TradeItems");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.TradeItemEntry", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ShipId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SpaceShipEntryId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("ShipId");

                    b.HasIndex("SpaceShipEntryId");

                    b.ToTable("TradeItemEntries");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.SpaceShipEntry", b =>
                {
                    b.HasOne("SpaceTruckerCompany.API.Models.Player", "Owner")
                        .WithMany("Ships")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpaceTruckerCompany.API.Models.SpaceShip", "Ship")
                        .WithMany()
                        .HasForeignKey("ShipId");

                    b.Navigation("Owner");

                    b.Navigation("Ship");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.TradeItemEntry", b =>
                {
                    b.HasOne("SpaceTruckerCompany.API.Models.TradeItem", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("SpaceTruckerCompany.API.Models.SpaceShip", "Ship")
                        .WithMany("TradeItems")
                        .HasForeignKey("ShipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpaceTruckerCompany.API.Models.SpaceShipEntry", null)
                        .WithMany("Cargo")
                        .HasForeignKey("SpaceShipEntryId");

                    b.Navigation("Item");

                    b.Navigation("Ship");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.Player", b =>
                {
                    b.Navigation("Ships");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.SpaceShip", b =>
                {
                    b.Navigation("TradeItems");
                });

            modelBuilder.Entity("SpaceTruckerCompany.API.Models.SpaceShipEntry", b =>
                {
                    b.Navigation("Cargo");
                });
#pragma warning restore 612, 618
        }
    }
}

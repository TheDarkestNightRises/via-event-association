﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;

#nullable disable

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.Migrations
{
    [DbContext(typeof(DmContext))]
    [Migration("20240421131030_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("GuestId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("ViaEventAssociation.Core.Domain.Aggregates.Event.EventAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("EventCapacity", "ViaEventAssociation.Core.Domain.Aggregates.Event.EventAggregate.EventCapacity#EventCapacity", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Capacity")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Capacity");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("EventDescription", "ViaEventAssociation.Core.Domain.Aggregates.Event.EventAggregate.EventDescription#EventDescription", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("EventTitle", "ViaEventAssociation.Core.Domain.Aggregates.Event.EventAggregate.EventTitle#EventTitle", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Title");
                        });

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("GuestFirstName", "ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestAggregate.GuestFirstName#GuestFirstName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("FirstName");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("GuestLastName", "ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestAggregate.GuestLastName#GuestLastName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("LastName");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("GuestPictureUrl", "ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestAggregate.GuestPictureUrl#GuestPictureUrl", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("PictureUrl")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("PictureUrl");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("GuestViaEmail", "ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestAggregate.GuestViaEmail#GuestViaEmail", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("ViaEmail")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Email");
                        });

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("ViaEventAssociation.Core.Domain.Aggregates.Event.Entities.InvitationEntity.Invitation", b =>
                {
                    b.HasOne("ViaEventAssociation.Core.Domain.Aggregates.Event.EventAggregate", null)
                        .WithMany("Invitations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestAggregate", null)
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ViaEventAssociation.Core.Domain.Aggregates.Event.EventAggregate", b =>
                {
                    b.OwnsOne("ViaEventAssociation.Core.Domain.Aggregates.Event.Values.EventTimeInterval", "EventTimeInterval", b1 =>
                        {
                            b1.Property<Guid>("EventAggregateId")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("End")
                                .HasColumnType("TEXT")
                                .HasColumnName("End");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("TEXT")
                                .HasColumnName("Start");

                            b1.HasKey("EventAggregateId");

                            b1.ToTable("Events");

                            b1.WithOwner()
                                .HasForeignKey("EventAggregateId");
                        });

                    b.Navigation("EventTimeInterval");
                });

            modelBuilder.Entity("ViaEventAssociation.Core.Domain.Aggregates.Event.EventAggregate", b =>
                {
                    b.Navigation("Invitations");
                });
#pragma warning restore 612, 618
        }
    }
}
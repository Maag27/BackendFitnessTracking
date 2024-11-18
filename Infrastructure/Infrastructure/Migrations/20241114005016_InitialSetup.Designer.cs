﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241114005016_InitialSetup")]
    partial class InitialSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Cow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("MilkId")
                        .HasColumnType("uuid");

                    b.Property<string>("Race")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MilkId");

                    b.ToTable("Cows");
                });

            modelBuilder.Entity("Domain.Milk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Farm")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Litters")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Milks");
                });

            modelBuilder.Entity("Domain.Models.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExerciseId"));

                    b.Property<string>("ExerciseName")
                        .HasColumnType("text");

                    b.Property<int>("ExerciseTemplateId")
                        .HasColumnType("integer");

                    b.Property<int>("RoutineTemplateId")
                        .HasColumnType("integer");

                    b.HasKey("ExerciseId");

                    b.HasIndex("RoutineTemplateId");

                    b.ToTable("ExerciseTemplates");
                });

            modelBuilder.Entity("Domain.Models.ExerciseDetail", b =>
                {
                    b.Property<int>("ExerciseDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExerciseDetailId"));

                    b.Property<int>("DetailTemplateId")
                        .HasColumnType("integer");

                    b.Property<int>("ExerciseTemplateId")
                        .HasColumnType("integer");

                    b.Property<string>("Repetitions")
                        .HasColumnType("text");

                    b.Property<string>("RestTime")
                        .HasColumnType("text");

                    b.Property<int>("Series")
                        .HasColumnType("integer");

                    b.HasKey("ExerciseDetailId");

                    b.HasIndex("ExerciseTemplateId");

                    b.ToTable("ExerciseDetailTemplates");
                });

            modelBuilder.Entity("Domain.Models.Routine", b =>
                {
                    b.Property<int>("RoutineTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoutineTemplateId"));

                    b.Property<string>("RoutineName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("RoutineTemplateId");

                    b.ToTable("RoutineTemplates");
                });

            modelBuilder.Entity("Domain.Models.UserExercise", b =>
                {
                    b.Property<int>("UserExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserExerciseId"));

                    b.Property<int>("ExerciseTemplateId")
                        .HasColumnType("integer");

                    b.Property<int>("UserRoutineId")
                        .HasColumnType("integer");

                    b.HasKey("UserExerciseId");

                    b.HasIndex("UserRoutineId");

                    b.ToTable("UserExercises");
                });

            modelBuilder.Entity("Domain.Models.UserExerciseDetail", b =>
                {
                    b.Property<int>("UserDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserDetailId"));

                    b.Property<string>("Repetitions")
                        .HasColumnType("text");

                    b.Property<string>("RestTime")
                        .HasColumnType("text");

                    b.Property<int>("Series")
                        .HasColumnType("integer");

                    b.Property<int>("UserExerciseId")
                        .HasColumnType("integer");

                    b.HasKey("UserDetailId");

                    b.HasIndex("UserExerciseId");

                    b.ToTable("UserExerciseDetails");
                });

            modelBuilder.Entity("Domain.Models.UserMetrics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Altura")
                        .HasColumnType("double precision");

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<int>("Edad")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<double>("Peso")
                        .HasColumnType("double precision");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserMetrics");
                });

            modelBuilder.Entity("Domain.Models.UserRoutine", b =>
                {
                    b.Property<int>("UserRoutineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserRoutineId"));

                    b.Property<int>("RoutineTemplateId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("UserRoutineId");

                    b.ToTable("UserRoutines");
                });

            modelBuilder.Entity("Domain.Cow", b =>
                {
                    b.HasOne("Domain.Milk", "Milk")
                        .WithMany()
                        .HasForeignKey("MilkId");

                    b.Navigation("Milk");
                });

            modelBuilder.Entity("Domain.Models.Exercise", b =>
                {
                    b.HasOne("Domain.Models.Routine", null)
                        .WithMany("Exercises")
                        .HasForeignKey("RoutineTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.ExerciseDetail", b =>
                {
                    b.HasOne("Domain.Models.Exercise", null)
                        .WithMany("ExerciseDetails")
                        .HasForeignKey("ExerciseTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.UserExercise", b =>
                {
                    b.HasOne("Domain.Models.UserRoutine", null)
                        .WithMany("UserExercises")
                        .HasForeignKey("UserRoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.UserExerciseDetail", b =>
                {
                    b.HasOne("Domain.Models.UserExercise", null)
                        .WithMany("UserExerciseDetails")
                        .HasForeignKey("UserExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Exercise", b =>
                {
                    b.Navigation("ExerciseDetails");
                });

            modelBuilder.Entity("Domain.Models.Routine", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Domain.Models.UserExercise", b =>
                {
                    b.Navigation("UserExerciseDetails");
                });

            modelBuilder.Entity("Domain.Models.UserRoutine", b =>
                {
                    b.Navigation("UserExercises");
                });
#pragma warning restore 612, 618
        }
    }
}
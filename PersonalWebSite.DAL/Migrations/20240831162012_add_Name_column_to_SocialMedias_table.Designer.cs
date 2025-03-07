﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalWebSite.DAL.Core;

#nullable disable

namespace PersonalWebSite.DAL.Migrations
{
    [DbContext(typeof(PersonalWebSiteDbContext))]
    [Migration("20240831162012_add_Name_column_to_SocialMedias_table")]
    partial class add_Name_column_to_SocialMedias_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonalWebSite.Model.Entities.About", b =>
                {
                    b.Property<int>("AboutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AboutId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CvLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AboutId");

                    b.ToTable("Abouts");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.Banner", b =>
                {
                    b.Property<int>("BannerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BannerId"));

                    b.Property<string>("MeetMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BannerId");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ContactInfo", b =>
                {
                    b.Property<int>("ContactInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactInfoId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactInfoId");

                    b.ToTable("ContactInfos");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ContactMail", b =>
                {
                    b.Property<int>("ContactMailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactMailId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactMailId");

                    b.ToTable("ContactMails");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.Footer", b =>
                {
                    b.Property<int>("FooterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FooterId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FooterId");

                    b.ToTable("Footers");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ItemTech", b =>
                {
                    b.Property<int>("ItemTechId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemTechId"));

                    b.Property<int>("ResumeCategoryItemId")
                        .HasColumnType("int");

                    b.Property<int>("TechIUsedId")
                        .HasColumnType("int");

                    b.HasKey("ItemTechId");

                    b.HasIndex("TechIUsedId");

                    b.HasIndex("ResumeCategoryItemId", "TechIUsedId")
                        .IsUnique();

                    b.ToTable("ItemTeches");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.Resume", b =>
                {
                    b.Property<int>("ResumeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResumeId"));

                    b.Property<string>("StartMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResumeId");

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ResumeCategory", b =>
                {
                    b.Property<int>("ResumeCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResumeCategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.HasKey("ResumeCategoryId");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeCategories");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ResumeCategoryItem", b =>
                {
                    b.Property<int>("ResumeCategoryItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResumeCategoryItemId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResumeCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResumeCategoryItemId");

                    b.HasIndex("ResumeCategoryId");

                    b.ToTable("ResumeCategoryItems");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillId"));

                    b.Property<int>("AboutId")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillId");

                    b.HasIndex("AboutId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.SocialMedia", b =>
                {
                    b.Property<int>("SocialMediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SocialMediaId"));

                    b.Property<int?>("BannerId")
                        .HasColumnType("int");

                    b.Property<string>("IconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMediaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SocialMediaId");

                    b.HasIndex("BannerId");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.TechIUsed", b =>
                {
                    b.Property<int>("TechIUsedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TechIUsedId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TechIUsedId");

                    b.ToTable("TechsIUsed");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ItemTech", b =>
                {
                    b.HasOne("PersonalWebSite.Model.Entities.ResumeCategoryItem", "ResumeCategoryItem")
                        .WithMany("ItemTeches")
                        .HasForeignKey("ResumeCategoryItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalWebSite.Model.Entities.TechIUsed", "TechIUsed")
                        .WithMany("ItemTeches")
                        .HasForeignKey("TechIUsedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ResumeCategoryItem");

                    b.Navigation("TechIUsed");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ResumeCategory", b =>
                {
                    b.HasOne("PersonalWebSite.Model.Entities.Resume", "Resume")
                        .WithMany("ResumeCategories")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resume");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ResumeCategoryItem", b =>
                {
                    b.HasOne("PersonalWebSite.Model.Entities.ResumeCategory", "ResumeCategory")
                        .WithMany("ResumeCategoryItems")
                        .HasForeignKey("ResumeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ResumeCategory");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.Skill", b =>
                {
                    b.HasOne("PersonalWebSite.Model.Entities.About", "About")
                        .WithMany("Skills")
                        .HasForeignKey("AboutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("About");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.SocialMedia", b =>
                {
                    b.HasOne("PersonalWebSite.Model.Entities.Banner", null)
                        .WithMany("SocialMedias")
                        .HasForeignKey("BannerId");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.About", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.Banner", b =>
                {
                    b.Navigation("SocialMedias");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.Resume", b =>
                {
                    b.Navigation("ResumeCategories");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ResumeCategory", b =>
                {
                    b.Navigation("ResumeCategoryItems");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.ResumeCategoryItem", b =>
                {
                    b.Navigation("ItemTeches");
                });

            modelBuilder.Entity("PersonalWebSite.Model.Entities.TechIUsed", b =>
                {
                    b.Navigation("ItemTeches");
                });
#pragma warning restore 612, 618
        }
    }
}

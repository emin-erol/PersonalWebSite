using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalWebSite.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.DAL.Core
{
    public class PersonalWebSiteDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public PersonalWebSiteDbContext(DbContextOptions<PersonalWebSiteDbContext> options) : base(options) { }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactMail> ContactMails { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<ItemTech> ItemTeches { get; set; }
        public DbSet<ResumeCategory> ResumeCategories { get; set; }
        public DbSet<ResumeCategoryItem> ResumeCategoryItems { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<TechIUsed> TechsIUsed { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemTech>()
                .HasKey(it => it.ItemTechId);

            modelBuilder.Entity<ItemTech>()
                .HasIndex(it => new { it.ResumeCategoryItemId, it.TechIUsedId })
                .IsUnique();

            modelBuilder.Entity<ItemTech>()
                .HasOne(rti => rti.ResumeCategoryItem)
                .WithMany(it => it.ItemTeches)
                .HasForeignKey(rti => rti.ResumeCategoryItemId);

            modelBuilder.Entity<ItemTech>()
                .HasOne(tiu => tiu.TechIUsed)
                .WithMany(t => t.ItemTeches)
                .HasForeignKey(tiu => tiu.TechIUsedId);

            modelBuilder.Entity<About>()
                .HasMany(a => a.Skills)
                .WithOne(s => s.About)
                .HasForeignKey(s => s.AboutId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResumeCategoryItem>()
                .HasMany(a => a.ItemTeches)
                .WithOne(s => s.ResumeCategoryItem)
                .HasForeignKey(s => s.ResumeCategoryItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace DiboWeb.Models
{
    public class GxDbContext:DbContext
    {
        public GxDbContext(DbContextOptions<GxDbContext> options):base(options)
        {
        }
      
        public DbSet<Project> Projects { get; set; }
        public DbSet<GxPoint> GxPoints { get; set; }
        public DbSet<GxLine> GxLines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GxProperty> GxProperties { get; set; }

        public DbSet<GeoPoint> GeoPoints { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        public DbSet<PropertyTemplate> PropertyTemplates { get; set; }
        public DbSet<Template_Property> Template_Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Project-Creator :many to one
            modelBuilder.Entity<Project>(p => 
            {
                p.ToTable("Project");
                p.Ignore(u => u.GxProperties);

                p.HasOne(u => u.Creator)
                .WithMany(u => u.CreatedProjects)
                .HasForeignKey(u => u.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);//删除CreatorID时,不删除项目的UserID

                p.HasOne(u => u.PropertyTemplate)
                .WithMany()
                .HasForeignKey(u => u.PropertyTemplateId)
                .OnDelete(DeleteBehavior.Restrict);//删除项目时,不删除属性模版

                p.Property(b => b.CreatedTime).ValueGeneratedOnAdd();
                p.Property(b => b.UpdateTime).ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<GxPoint>(entity => 
            {
                entity.ToTable("Point");
                entity.Property(p => p.ExpNo)
                .HasMaxLength(128)
                .IsRequired();

                entity.Property(p => p.MapNo)
                .HasMaxLength(128)
                .IsRequired();

                entity.HasIndex(p => p.MapNo);

                entity.HasOne(p => p.Project)
                .WithMany(p => p.GxPoints)
                .HasForeignKey(p => p.ProjectId);

                entity.Ignore(p => p.GxProperties);

                entity.Property(b => b.CreateTime).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<GxLine>(entity =>
            {
                entity.ToTable("Line");
                entity.HasOne(p => p.Project)
                .WithMany(p => p.GxLines)
                .HasForeignKey(p => p.ProjectId);

                entity.Ignore(p => p.GxProperties);
                entity.Property(b => b.CreateTime).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<GxProperty>().ToTable("Property");
              
            modelBuilder.Entity<GxProperty>().Ignore(p => p.AlternativeValues);
                        
            modelBuilder.Entity<GeoPoint>(gp =>
            {
                gp.ToTable("GeoPoint").Property(p => p.Name).HasMaxLength(128);
                gp.HasIndex(u => u.Name);

                gp.HasOne(p => p.Project)
                .WithMany(p => p.GeoPoints)
                .HasForeignKey(p => p.ProjectId);

                gp.Property(p => p.Id).ValueGeneratedNever();

                gp.Property(b => b.CreateTime).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.Property(b => b.RegisterTime).ValueGeneratedOnAdd();
                entity.Property(b => b.RegisterIp).ValueGeneratedOnAddOrUpdate();
                entity.Property(b => b.LoginTime).ValueGeneratedOnAddOrUpdate();
                entity.Property(b => b.LoginIp).ValueGeneratedOnAddOrUpdate();
            });

            //many-to-many Project -- User
            modelBuilder.Entity<UserProject>(up => 
            {
                up.ToTable("UserProject");
                up.HasKey(u => new { u.UserId, u.ProjectId });
              
              up.HasOne(u => u.User)
              .WithMany(p => p.UserProjects)
              .HasForeignKey(u => u.UserId);

              up.HasOne(u => u.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(p => p.ProjectId);
            });

            modelBuilder.Entity<PropertyTemplate>(pt => 
            {
                pt.ToTable("Template");
            });

            modelBuilder.Entity<Template_Property>(tp =>
            {
                tp.ToTable("Template_Property")
                .HasKey(u=> new { u.GxPropertyId, u.PropertyTemplateId });

                tp.HasOne(u => u.PropertyTemplate)
                .WithMany(u => u.Template_Properties)
                .HasForeignKey(u => u.PropertyTemplateId);

                tp.HasOne(u => u.GxProperty)
                .WithMany(u => u.Template_Properties)
                .HasForeignKey(u => u.GxPropertyId);
            });
        }
    }

    public class GxContextFactory : IDesignTimeDbContextFactory<GxDbContext>
    {
        public GxDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GxDbContext>();
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=gxdb;user=root;password=123;SSLMode=none");
            return new GxDbContext(optionsBuilder.Options);
        }
    }
}

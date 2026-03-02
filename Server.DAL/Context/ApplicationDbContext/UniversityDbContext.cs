using Microsoft.EntityFrameworkCore;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Entities.Users;

namespace Server.DAL.Context.ApplicationDbContext;


public class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
    { }
    
    public DbSet<Educator> Educators { get; set; }
    public DbSet<EducatorAdditionalInfo> EducatorAdditionalInfos { get; set; }
    public DbSet<EducatorDiscipline> EducatorDisciplines { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<TrainedGroup> TrainedGroups { get; set; }

    //Таблицы пользователей
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admins { get; set; }

    /// <summary>
    /// Связи БД через EF
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Educator - EducatorAdditionalInfo
        modelBuilder.Entity<Educator>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.EducatorAdditionalInfo)
                .WithOne()
                .HasForeignKey<EducatorAdditionalInfo>(eai => eai.EducatorId);
        });

        // EducatorAdditionalInfo - EducatorDiscipline
        modelBuilder.Entity<EducatorDiscipline>(entity =>
        {
            entity.HasKey(ed => ed.Id);
            
            entity.HasOne<EducatorAdditionalInfo>()
                .WithMany(eai => eai.EducatorDisciplines)
                .HasForeignKey(ed => ed.EducatorAdditionalInfoId);
            entity.HasOne(ed => ed.Discipline)
                .WithMany()
                .HasForeignKey(ed => ed.DisciplineId);
        });

        // Discipline - TrainedGroup

        modelBuilder.Entity<Discipline>(entity =>
        {
            modelBuilder.Entity<Discipline>()
                .HasOne(d => d.Group)
                .WithOne(g => g.Discipline)
                .HasForeignKey<TrainedGroup>(g => g.DisciplineId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(d => d.Course);

        });

        // TrainedGroup
        modelBuilder.Entity<TrainedGroup>(entity =>
        {
            entity.HasKey(tg => tg.Id);
        });

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Role).HasConversion<byte>();

            entity.HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.Administrator)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.Educator)
                .WithOne(e => e.User)
                .HasForeignKey<Educator>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Student
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Students");

            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StudentId).IsUnique();
            entity.Property(e => e.StudentId).HasMaxLength(20);
        });

        // Admin
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("Admins");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Position).HasMaxLength(100);
        });

        modelBuilder.Entity<Educator>(entity =>
        {
            entity.HasOne(e => e.User)
                .WithOne(u => u.Educator)
                .HasForeignKey<Educator>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Ignore(e => e.FullName);
            entity.Property(e => e.Profession).IsRequired();
            entity.Property(e => e.AcademicDegree).IsRequired();
        });
    }    
}

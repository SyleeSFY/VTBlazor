using Microsoft.EntityFrameworkCore;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;

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
    }    
}

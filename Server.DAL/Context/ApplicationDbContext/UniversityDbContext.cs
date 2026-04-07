using Microsoft.EntityFrameworkCore;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Education;
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
    
    public DbSet<Group> Groups { get; set; }
    public DbSet<TaskEducation> TaskEducations { get; set; }
    public DbSet<TaskFile> TaskFiles { get; set; }
    
    //Таблицы пользователей
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admins { get; set; }

    //Таблицы для решений
    public DbSet<StudentSolution> StudentSolutions { get; set; }
    public DbSet<SolutionFile> SolutionFiles { get; set; }
    public DbSet<SolutionChat> SolutionChats { get; set; }
    public DbSet<MessageInChat> MessagesInChat { get; set; }
    public DbSet<FileInChat> FilesInChat { get; set; }

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

        // Discipline

        modelBuilder.Entity<Discipline>(entity =>
        {
            modelBuilder.Entity<Discipline>()
                .HasOne(d => d.Group)
                .WithOne(g => g.Discipline)
                .HasForeignKey<TrainedGroup>(g => g.DisciplineId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

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
        
        modelBuilder.Entity<TaskEducation>(entity =>
        {
            entity.ToTable("TaskEducation");
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.Dicipline)
                .WithMany(d => d.TaskEducations) 
                .HasForeignKey(e => e.DiciplineId)
                .OnDelete(DeleteBehavior.Restrict);
    
            entity.HasOne(e => e.Educator)
                .WithMany(ed => ed.TaskEducations)
                .HasForeignKey(e => e.EducatorId)
                .OnDelete(DeleteBehavior.Restrict);
    
            entity.HasMany(e => e.Files)
                .WithOne(f => f.Task)
                .HasForeignKey(f => f.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Groups)
                 .WithMany(g => g.TaskEducations)
                 .UsingEntity(j => j.ToTable("TaskGroups"));

        });
        
        modelBuilder.Entity<TaskFile>(entity =>
        {
            entity.ToTable("TaskFile");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Task).WithMany(t => t.Files).HasForeignKey(e => e.TaskId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Groups");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasMany(g => g.Students)
                  .WithOne(s => s.Group)
                  .HasForeignKey(s => s.GroupId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Student
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Students");

            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.StudentCard).IsUnique();
            entity.Property(e => e.StudentCard).HasMaxLength(20);
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

        // Solution
        modelBuilder.Entity<StudentSolution>(entity =>
        {
            entity.ToTable("StudentSolutions");
            entity.HasKey(e => e.Id);

            // Связь со студентом
            entity.HasOne(e => e.Student)
                .WithMany(s => s.Solutions)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с заданием
            entity.HasOne(e => e.Task)
                .WithMany(t => t.StudentSolutions)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с чатом (один к одному)
            entity.HasOne(e => e.SolutionChat)
                .WithOne(c => c.Solution)
                .HasForeignKey<SolutionChat>(c => c.SolutionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // SolutionFile (файлы решения)
        modelBuilder.Entity<SolutionFile>(entity =>
        {
            entity.ToTable("SolutionFiles");
            entity.HasKey(e => e.Id);

            // Связь с решением
            entity.HasOne(e => e.Solution)
                .WithMany(s => s.SolutionFiles)
                .HasForeignKey(e => e.SolutionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.SolutionId);
        });

        // SolutionChat
        modelBuilder.Entity<SolutionChat>(entity =>
        {
            entity.ToTable("SolutionChats");
            entity.HasKey(e => e.Id);

            // Связь с решением (один к одному)
            entity.HasOne(e => e.Solution)
                .WithOne(s => s.SolutionChat)
                .HasForeignKey<SolutionChat>(c => c.SolutionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.SolutionId).IsUnique();
        });

        // MessageInChat
        modelBuilder.Entity<MessageInChat>(entity =>
        {
            entity.ToTable("MessagesInChat");
            entity.HasKey(e => e.Id);

            // Связь с чатом
            entity.HasOne(e => e.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(e => e.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // FileInChat
        modelBuilder.Entity<FileInChat>(entity =>
        {
            entity.ToTable("FilesInChat");
            entity.HasKey(e => e.Id);

            // Связь с сообщением
            entity.HasOne(e => e.Message)
                .WithMany(m => m.Files)
                .HasForeignKey(e => e.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.MessageId);
        });

    }    
}
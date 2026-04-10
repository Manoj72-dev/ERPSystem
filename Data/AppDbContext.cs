using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassSemesterSubject>()
                .HasOne(c => c.Class)
                .WithMany(c => c.ClassSubjects) 
                .HasForeignKey(c => c.ClassId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClassSemesterSubject>()
                .HasOne(c => c.Semester)
                .WithMany() 
                .HasForeignKey(c => c.SemesterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClassSemesterSubject>()
                .HasOne(c => c.Subject)
                .WithMany(s => s.ClassSubjects) 
                .HasForeignKey(c => c.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<ClassSemesterSubject>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.ClassSubjects)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.StudentId, a.ClassSubjectId, a.Date })
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Subject>()
                .HasIndex(s => s.Code)
                .IsUnique();
        }
            
        public DbSet<Department> Departments { get; set; }
        public DbSet<CourseProgram> Programs { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassSemesterSubject> ClassSubjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<User> Users { get; set; }

    }
}

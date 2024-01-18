using Exercise1.Entity;
using Microsoft.EntityFrameworkCore;
using System.Windows;

public class ScoreContext : DbContext
{
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=Score;User Id=sa;Password=12345;TrustServerCertificate=true;MultipleActiveResultSets=true;");
    }

}
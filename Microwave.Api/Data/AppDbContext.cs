using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Microwave.Core.Models;

namespace Microwave.Api.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<PredefinedProgram> PredefinedPrograms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}

using System.Reflection;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Microwave.Core.Models;

namespace Microwave.Api.Data;

public class AppDbContext(DbContextOptions options) : IdentityDbContext(options)
{
    public DbSet<PredefinedProgram> PredefinedPrograms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}

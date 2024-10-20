using Microsoft.EntityFrameworkCore;

using Microwave.Api.Data;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Requests.PredefinedPrograms;

namespace Microwave.Api.Handlers;

public class PredefinedProgramHandler(AppDbContext appDbContext) : IPredefinedProgramHandler
{
    public async Task<IList<PredefinedProgram>> GetAll()
    {
        var listPrograms = await appDbContext.PredefinedPrograms
                .AsNoTracking()
                .ToListAsync();

        return listPrograms;
    }

    public async Task<IList<PredefinedProgram>> GetCustomPrograms()
    {
        var listPrograms = await appDbContext.PredefinedPrograms
                .Where(p => !p.IsPredefined)
                .AsNoTracking()
                .ToListAsync();

        return listPrograms;
    }

    public async Task<PredefinedProgram> SaveCustomProgram(CreatePredefinedProgramRequest request)
    {
        try
        {
            PredefinedProgram predefinedProgram = new(
                request.Name,
                request.Food,
                request.TimeSeconds,
                request.Power,
                request.LabelHeating[0],
                request.Instructions
            );

            await appDbContext.AddAsync(predefinedProgram);
            await appDbContext.SaveChangesAsync();

            return predefinedProgram;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task DeleteCustomProgram(Guid id)
    {
        var predefinedProgram = await appDbContext.PredefinedPrograms.FirstOrDefaultAsync(c => c.Id == id && !c.IsPredefined);

        if (predefinedProgram == null)
            throw new Exception("Predefined program not found");

        appDbContext.PredefinedPrograms.Remove(predefinedProgram);
        await appDbContext.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;

using Microwave.Api.Data;
using Microwave.Api.Exceptions;
using Microwave.Api.Validators.Requests;
using Microwave.Core.Handlers;
using Microwave.Core.Models;
using Microwave.Core.Requests.PredefinedPrograms;

namespace Microwave.Api.Handlers;

public class PredefinedProgramHandler(AppDbContext appDbContext) : IPredefinedProgramHandler
{
    public async Task<IList<PredefinedProgram>> GetAll()
    {
        var programs = await appDbContext.PredefinedPrograms
                .AsNoTracking()
                .ToListAsync();

        return programs;
    }

    public async Task<IList<PredefinedProgram>> GetCustomPrograms()
    {
        var listPrograms = await appDbContext.PredefinedPrograms
                .Where(p => !p.IsPredefined)
                .AsNoTracking()
                .ToListAsync();

        return listPrograms;
    }
    public async Task<PredefinedProgram> GetByIdAsync(Guid id)
    {
        try
        {
            var program = await appDbContext.PredefinedPrograms
                    .AsNoTracking()
                    .FirstAsync(c => c.Id == id);

            return program;
        }
        catch (ArgumentNullException)
        {
            throw new ArgumentNullException("Predefined program not found");
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("Predefined program not found");
        }
        catch (Exception)
        {
            throw;
        }

    }
    public async Task<PredefinedProgram> SaveCustomProgram(CreatePredefinedProgramRequest request)
    {
        try
        {
            var validator = new CreatePredefinedProgramValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new MicrowaveValidationException(validationResult.ToString());

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
        catch (MicrowaveValidationException)
        {
            throw;
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

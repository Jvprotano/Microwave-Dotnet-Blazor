using Microwave.Core.Models;
using Microwave.Core.Requests.PredefinedPrograms;
using Microwave.Core.Responses.PredefinedProgram;

namespace Microwave.Core.Handlers;

public interface IPredefinedProgramHandler
{
    Task<IList<PredefinedProgram>> GetAll();
    Task<IList<PredefinedProgram>> GetCustomPrograms();
    Task<PredefinedProgram> SaveCustomProgram(CreatePredefinedProgramRequest request);
    Task DeleteCustomProgram(Guid id);
}

using Microwave.Core.Models;
using Microwave.Core.Requests.PredefinedPrograms;
using Microwave.Core.Responses.PredefinedProgram;

namespace Microwave.Core.Handlers;

public interface IHttpPredefinedProgramHandler
{
    Task<IList<PredefinedProgramResponse>> GetAll();
    Task<PredefinedProgramResponse> SaveCustomProgram(CreatePredefinedProgramRequest request);
}

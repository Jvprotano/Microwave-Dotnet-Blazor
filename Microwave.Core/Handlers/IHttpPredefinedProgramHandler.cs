using Microwave.Core.Models;
using Microwave.Core.Requests.PredefinedPrograms;
using Microwave.Core.Responses;
using Microwave.Core.Responses.PredefinedProgram;

namespace Microwave.Core.Handlers;

public interface IHttpPredefinedProgramHandler
{
    Task<IList<PredefinedProgramResponse>> GetAll();
    Task<BaseResponse<PredefinedProgramResponse?>> SaveCustomProgram(CreatePredefinedProgramRequest request);
}

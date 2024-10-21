using Microwave.Core.Requests.Account;
using Microwave.Core.Responses;

namespace Microwave.Core.Handlers;

public interface IAccountHandler
{
    Task<BaseResponse<string>> LoginAsync(LoginRequest request);
    Task<BaseResponse<string>> RegisterAsync(RegisterRequest request);
}
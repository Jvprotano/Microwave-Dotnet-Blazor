using System.ComponentModel.DataAnnotations;

namespace Microwave.Core.Requests.Account;

public class LoginRequest : BaseRequest
{
    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Senha é obrigatória")]
    public string Password { get; set; } = string.Empty;
}

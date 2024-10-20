using FluentValidation;

using Microwave.Core.Requests.Execution;

namespace Microwave.Api.Validators.Requests;

public class StartRequestValidator : AbstractValidator<StartRequest>
{
    public StartRequestValidator()
    {
        RuleFor(x => x.Seconds)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(120)
            .When(s => string.IsNullOrEmpty(s.PredefinedProgramId.ToString()));

        RuleFor(x => x.Power)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(10)
            .When(s => string.IsNullOrEmpty(s.PredefinedProgramId.ToString()));
    }
}
using FluentValidation;

using Microwave.Core.Requests.PredefinedPrograms;

namespace Microwave.Api.Validators.Requests;

public class CreatePredefinedProgramValidator : AbstractValidator<CreatePredefinedProgramRequest>
{
    public CreatePredefinedProgramValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(p => p.Food)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(p => p.TimeSeconds)
            .NotNull()
            .GreaterThan(0)
            .LessThan(120);

        RuleFor(p => p.Power)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(10);

        RuleFor(p => p.LabelHeating)
           .NotNull()
           .NotEmpty()
           .Must(p => p != null && p.Length == 1)
           .WithMessage("LabelHeating must be a single character")
           .NotEqual(".");

        RuleFor(p => p.Instructions)
        .NotEmpty()
        .MaximumLength(250)
        .When(p => !string.IsNullOrEmpty(p.Instructions));
    }
}

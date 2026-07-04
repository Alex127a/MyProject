using FluentValidation;
using MyProjectService.Contracts;

namespace MyProjectService.Core;



public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Имя не валидное");

        RuleFor(x => x.AddressLine).NotEmpty().MaximumLength(200).WithMessage("Адрес не валидный");

       
        
    }
}
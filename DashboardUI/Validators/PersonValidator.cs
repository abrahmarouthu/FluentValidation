using FluentValidation;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://fluentvalidation.net/

namespace DashboardUI.Validators
{
  public class PersonValidator : AbstractValidator<PersonModel>
  {
    public PersonValidator()
    {
      RuleFor(p => p.FirstName)
        .Cascade(CascadeMode.StopOnFirstFailure)
        .NotEmpty().WithMessage("{PropertyName} is Empty")
        .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
        .Must(BeAValidName).WithMessage("{PropertyName} Contains Invalid Characters");

      RuleFor(p => p.LastName)
        .Cascade(CascadeMode.StopOnFirstFailure)
        .NotEmpty().WithMessage("{PropertyName} is Empty")
        .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
        .Must(BeAValidName).WithMessage("{PropertyName} Contains Invalid Characters");

      RuleFor(p => p.DateOfBirth)
        .Must(BeAValidAge).WithMessage("Invalid {PropertyName}");
    }

    // Checks if the name has only numeric values.
    protected bool BeAValidName(string name)
    {
      name = name.Replace(" ", "");
      name = name.Replace("-", "");
      return name.All(Char.IsLetter);
    }

    protected bool BeAValidAge(DateTime date)
    {
      int currentYear = DateTime.Now.Year;
      int dobYear = date.Year;

      if(dobYear <= currentYear && dobYear > (currentYear - 120))
        return true;

      return false;
    }
  }
}

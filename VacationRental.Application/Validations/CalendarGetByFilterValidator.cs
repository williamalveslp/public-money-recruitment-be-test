using FluentValidation;
using VacationRental.Application.ViewModels.Calendars;

namespace VacationRental.Application.Validations
{
    public class CalendarGetByFilterValidator : AbstractValidator<CalendarGetByFilterViewModel>
    {
        private readonly string CODE = nameof(CalendarGetByFilterValidator);

        public CalendarGetByFilterValidator()
        {
            RuleFor(f => f)
              .NotNull()
              .WithMessage("Entity is invalid.")
              .WithErrorCode($"{CODE}-001")
              .WithSeverity(Severity.Error);

            RuleFor(f => f.Nights)
              .GreaterThan(0)
              .WithMessage(f => $"{nameof(f.Nights)} must be positive")
              .WithErrorCode($"{CODE}-002")
              .WithSeverity(Severity.Error);

            RuleFor(f => f.RentalId)
              .GreaterThan(0)
              .WithMessage(f => $"{nameof(f.RentalId)} must be positive")
              .WithErrorCode($"{CODE}-003")
              .WithSeverity(Severity.Error);
        }
    }
}

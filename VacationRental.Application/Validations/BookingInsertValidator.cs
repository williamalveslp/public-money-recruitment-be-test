using FluentValidation;
using VacationRental.Application.ViewModels;

namespace VacationRental.Application.Validations
{
    public class BookingInsertValidator : AbstractValidator<BookingBindingModel>
    {
        private readonly string CODE = "Booking-Code";

        public BookingInsertValidator()
        {
            RuleFor(f => f)
              .NotNull()
              .WithMessage("Entity is invalid.")
              .WithErrorCode($"{CODE}-001")
              .WithSeverity(Severity.Error);

            RuleFor(f => f.Nights)
              .GreaterThan(0)
              .WithMessage("Nigts must be positive")
              .WithErrorCode($"{CODE}-002")
              .WithSeverity(Severity.Error);
        }
    }
}

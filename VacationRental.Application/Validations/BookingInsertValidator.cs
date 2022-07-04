using FluentValidation;
using System;
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
              .WithMessage("Nights must be positive")
              .WithErrorCode($"{CODE}-002")
              .WithSeverity(Severity.Error);

            RuleFor(f => f.RentalId)
              .GreaterThan(0)
              .WithMessage("RentalId must be positive")
              .WithErrorCode($"{CODE}-003")
              .WithSeverity(Severity.Error);

            RuleFor(f => f.Start)
              .Must(f => f.Date >= DateTime.Now.Date)
              .WithMessage("The start date must be bigger than today.")
              .WithErrorCode($"{CODE}-004")
              .WithSeverity(Severity.Error);
        }
    }
}

using FluentValidation.Results;
using System;
using VacationRental.Application.Validations;

namespace VacationRental.Application.ViewModels
{
    public class BookingBindingModel : ViewModelValidatorBase
    {
        public int RentalId { get; set; }

        public DateTime Start
        {
            get => _startIgnoreTime;
            set => _startIgnoreTime = value.Date;
        }

        private DateTime _startIgnoreTime;

        public int Nights { get; set; }

        public override ValidationResult Validator()
        {
            var validate = new BookingInsertValidator().Validate(this);
            return GetValidationResultNormalized(validate);
        }
    }
}

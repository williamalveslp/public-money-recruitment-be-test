using FluentValidation.Results;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace VacationRental.Application.ViewModels
{
    /// <summary>
    /// View Model base with behavior for validators.
    /// </summary>
    public abstract class ViewModelValidatorBase
    {
        private IEnumerable<ValidationFailure> _validatorsFailuresMessage;

        /// <summary>
        /// List of failures from validators.
        /// </summary>
        [JsonProperty("failuresMessage")]
        [JsonIgnore]
        public IEnumerable<ValidationFailure> ValidatorFailuresMessage
        {
            get { return _validatorsFailuresMessage; }
            private set
            {
                if (value == null || !value.Any())
                    this.IsValid = true;

                this._validatorsFailuresMessage = value;
            }
        }

        /// <summary>
        /// List of errors message.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<string> ErrorsMessage =>
            _validatorsFailuresMessage?.Select(f => f?.ErrorMessage);

        /// <summary>
        /// Start as false to wait the validation.
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get; private set; } = false;

        /// <summary>
        /// Makes the validations about the rules to the properties.
        /// </summary>
        /// <returns></returns>
        public abstract ValidationResult Validator();

        /// <summary>
        /// Get the validation normalized.
        /// </summary>
        /// <param name="validationResult">ValidationResult rules.</param>
        /// <returns></returns>
        protected ValidationResult GetValidationResultNormalized(ValidationResult validationResult)
        {
            this.ValidatorFailuresMessage = validationResult?.Errors?.Select(f => f);

            return validationResult;
        }
    }
}

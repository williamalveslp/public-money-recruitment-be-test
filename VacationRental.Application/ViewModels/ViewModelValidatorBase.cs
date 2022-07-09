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
        private IList<string> _validErrorsMessage;
         
        /// <summary>
        /// List of errors from validators.
        /// </summary>
        [JsonProperty("errorsMessage")]
        [JsonIgnore]
        public IList<string> ValidatorErrorsMessage
        {
            get { return _validErrorsMessage; }
            set
            {
                if (value == null || !value.Any())
                    this.IsValid = true;

                this._validErrorsMessage = value;
            }
        }

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

        protected ValidationResult GetValidationResultNormalized(ValidationResult validationResult)
        {
            this.ValidatorErrorsMessage = validationResult?.Errors?.Select(f => f?.ErrorMessage)?.ToList();
            return validationResult;
        }
    }
}

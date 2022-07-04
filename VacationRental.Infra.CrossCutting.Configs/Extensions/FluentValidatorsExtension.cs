using FluentValidation.Results;
using System.Linq;

namespace VacationRental.Infra.CrossCutting.Configs.Extensions
{
    public static class FluentValidatorsExtension
    {
        public static string GetErrors(this ValidationResult validationResult)
        {
            var errorsMessage = validationResult?.Errors?.Select(f => f?.ErrorMessage);

            if (errorsMessage == null)
                return null;

            return string.Join(", ", errorsMessage);
        }
    }
}

using FluentValidation.Results;
using System.Linq;

namespace VacationRental.Infra.CrossCutting.Configs.Extensions
{
    public static class FluentValidatorsExtension
    {
        public static string GetFirstOrDefaultError(this ValidationResult validationResult)
        {
            return validationResult?.Errors?.FirstOrDefault().ErrorMessage;
        }
    }
}

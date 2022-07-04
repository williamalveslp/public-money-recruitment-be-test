using FluentValidation.Results;
using System;
using VacationRental.Domain.Core.Events;

namespace VacationRental.Domain.Core.Commands
{
    public abstract class ResultedCommand<T> : ResultedMessage<T>
    {
        protected ResultedCommand()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; }

        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}

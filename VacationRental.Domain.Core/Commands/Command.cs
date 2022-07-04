using System;
using System.ComponentModel.DataAnnotations;
using VacationRental.Domain.Core.Events;

namespace VacationRental.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }

        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}

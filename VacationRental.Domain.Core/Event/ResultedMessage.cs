using MediatR;

namespace VacationRental.Domain.Core.Events
{
    public abstract class ResultedMessage<T> : Message, IRequest<T>
    {
    }
}

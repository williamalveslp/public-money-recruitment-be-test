using VacationRental.Domain.Core.Commands;
using VacationRental.Domain.Core.Events;
using VacationRental.Domain.Core.Bus;
using System.Threading.Tasks;
using MediatR;

namespace VacationRental.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBusNormalize : IMediatorHandlerNormalize
    {
        private readonly IMediator _mediator;

        public InMemoryBusNormalize(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task<TResult> SendCommand<T, TResult>(T command) where T : ResultedCommand<TResult>
        {
            return _mediator.Send<TResult>(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}

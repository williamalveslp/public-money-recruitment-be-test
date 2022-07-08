using VacationRental.Domain.Core.Events;
using VacationRental.Domain.Core.Interfaces;
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

        public Task RaiseEvent<T>(T @eventRequest)
        {
            return _mediator.Publish(@eventRequest);
        }
    }
}

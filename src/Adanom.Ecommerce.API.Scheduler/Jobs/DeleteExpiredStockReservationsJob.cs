using Adanom.Ecommerce.API.Commands;
using MediatR;
using Quartz;

namespace Adanom.Ecommerce.API.Scheduler.Jobs
{
    public class DeleteExpiredStockReservationsJob : IJob
    {
        private readonly IMediator _mediator;

        public DeleteExpiredStockReservationsJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Publish(new DeleteExpiredStockReservations());
        }
    }
}

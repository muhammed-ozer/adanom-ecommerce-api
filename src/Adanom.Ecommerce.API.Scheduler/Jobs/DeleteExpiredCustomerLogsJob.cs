using Adanom.Ecommerce.API.Commands;
using MediatR;
using Quartz;

namespace Adanom.Ecommerce.API.Scheduler.Jobs
{
    public class DeleteExpiredCustomerLogsJob : IJob
    {
        private readonly IMediator _mediator;

        public DeleteExpiredCustomerLogsJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new DeleteExpiredCustomerLogs());
        }
    }
}

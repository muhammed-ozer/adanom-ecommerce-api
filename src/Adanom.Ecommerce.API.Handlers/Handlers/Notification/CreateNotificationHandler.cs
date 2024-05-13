using Adanom.Ecommerce.API.Data.Models;
using AutoMapper;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateNotificationHandler : INotificationHandler<CreateNotification>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateNotificationHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(CreateNotification command, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<CreateNotification, Notification>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await _applicationDbContext.AddAsync(notification);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"Notification_Create_Failed: {exception.Message}");
            }
        } 

        #endregion
    }
}

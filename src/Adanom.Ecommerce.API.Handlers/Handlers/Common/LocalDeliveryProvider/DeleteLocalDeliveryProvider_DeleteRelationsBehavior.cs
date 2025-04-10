﻿namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteLocalDeliveryProvider_DeleteRelationsBehavior : IPipelineBehavior<DeleteLocalDeliveryProvider, bool>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteLocalDeliveryProvider_DeleteRelationsBehavior(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteLocalDeliveryProvider command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteLocalDeliveryProviderResponse = await next();

            if (deleteLocalDeliveryProviderResponse)
            {
                #region Mappings

                var deleteMappingRequest = new DeleteLocalDeliveryProvider_AddressDistrictRequest()
                {
                    LocalDeliveryProviderId = command.Id,
                };

                var deleteMappingCommand = _mapper.Map(deleteMappingRequest, new DeleteLocalDeliveryProvider_AddressDistrict(command.Identity));

                await _mediator.Send(deleteMappingCommand);

                #endregion

                #region Images

                var deleteImagesRequest = new DeleteImagesRequest()
                {
                    EntityId = command.Id,
                    EntityType = EntityType.LOCALDELIVERYPROVIDER
                };

                var deleteImagesCommand = _mapper
                    .Map(deleteImagesRequest, new DeleteImages(command.Identity));

                await _mediator.Send(deleteImagesCommand);

                #endregion
            }

            return deleteLocalDeliveryProviderResponse;
        }

        #endregion
    }
}

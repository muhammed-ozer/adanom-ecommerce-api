using System.Globalization;
using System.Text;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class Checkout_CreateOrderDocumentsHandler : IRequestHandler<Checkout_CreateOrderDocuments, CreateOrderDocumentsResponse>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public Checkout_CreateOrderDocumentsHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<CreateOrderDocumentsResponse> Handle(Checkout_CreateOrderDocuments command, CancellationToken cancellationToken)
        {
            var user = command.User;
            var checkoutResponse = command.CheckoutResponse;
            var shoppingCartItems = checkoutResponse.Items.ToList();

            if (shoppingCartItems == null || shoppingCartItems.Count == 0)
            {
                throw new Exception("Shopping cart items not found");
            }

            var company = await _mediator.Send(new GetCompany());

            if (company == null)
            {
                throw new Exception("Company not found");
            }

            var companyAddressDitrict = await _mediator.Send(new GetAddressDistrict(company.AddressDistrictId));
            var companyAddressCity = await _mediator.Send(new GetAddressCity(company.AddressCityId));

            if (companyAddressCity == null || companyAddressDitrict == null)
            {
                throw new Exception("Company address city or district not found");
            }

            var userShippingAddress = await _mediator.Send(new GetShippingAddress(command.ShippingAddressId));

            if (userShippingAddress == null)
            {
                throw new Exception("User shipping address not found");
            }

            var userShippingAddressDistrict = await _mediator.Send(new GetAddressDistrict(userShippingAddress.AddressDistrictId));
            var userShippingAddressCity = await _mediator.Send(new GetAddressCity(userShippingAddress.AddressCityId));

            if (userShippingAddressCity == null || userShippingAddressDistrict == null)
            {
                throw new Exception("User shipping address city or district not found");
            }

            BillingAddressResponse? userBillingAddress = null;

            if (command.BillingAddressId != null && command.BillingAddressId > 0)
            {
                userBillingAddress = await _mediator.Send(new GetBillingAddress(command.BillingAddressId.Value));
            }

            var grandTotal = checkoutResponse.GrandTotal;
            var productsRow = await CreateProductsRowAsync(shoppingCartItems, !checkoutResponse.IsFreeShipping ? checkoutResponse.ShippingFeeTotal : null);
            var orderPaymentType = await _mediator.Send(new GetOrderPaymentType(command.OrderPaymentType));
            var createdDate = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("tr-TR"));
            var taxNumber = userBillingAddress != null ? userBillingAddress.TaxNumber : "11111111111";

            var distanceSellingContractHtmlContent = CreateDistanceSellingContract(
                company, companyAddressDitrict.Name, companyAddressCity.Name,
                user, userShippingAddress.Address, userShippingAddressDistrict.Name, userShippingAddressCity.Name,
                productsRow, orderPaymentType.Name, grandTotal, createdDate);

            var preliminaryInformationFormHtmlContent = CreatePreliminaryInformationForm(
                company, companyAddressDitrict.Name, companyAddressCity.Name,
                user, userShippingAddress.Address, userShippingAddressDistrict.Name, userShippingAddressCity.Name, taxNumber,
                productsRow, orderPaymentType.Name, grandTotal);

            return new CreateOrderDocumentsResponse()
            {
                DistanceSellingContractHtmlContent = distanceSellingContractHtmlContent,
                PreliminaryInformationFormHtmlContent = distanceSellingContractHtmlContent,
            };
        }

        #endregion

        #region Private Methods

        #region CreateDistanceSellingContract

        private string CreateDistanceSellingContract(
           CompanyResponse company, string companyAddressDistrictName, string companyAddressCityName,
           UserResponse user, string userShippingAddress, string userShippingAddressDistrictName, string userShippingAddressCityName,
           string productsRow, string orderPaymentTypeName, decimal grandTotal, string createdDate)
        {
            var document = OrderDocumentConstantsConstants.DistanceSellingContract.Document;

            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.CompanyLegalName, company.LegalName);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.CompanyAddress, company.Address);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.CompanyAddressDistrict, companyAddressDistrictName);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.CompanyAddressCity, companyAddressCityName);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.CompanyEmail, company.Email);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.CompanyPhoneNumber, company.PhoneNumber);

            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.UserFullName, $"{user.FirstName} {user.LastName}");
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.UserAddress, userShippingAddress);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.UserAddressDistrict, userShippingAddressDistrictName);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.UserAddressCity, userShippingAddressCityName);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.UserEmail, user.Email);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.UserPhoneNumber, user.PhoneNumber);

            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.Products, productsRow);
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.OrderPaymentType, orderPaymentTypeName);

            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.GrandTotal, grandTotal.ToString());
            document = document.Replace(OrderDocumentConstantsConstants.DistanceSellingContract.Replacements.CreatedDate, createdDate);

            return document;
        }

        #endregion

        #region CreatePreliminaryInformationForm

        private string CreatePreliminaryInformationForm(
           CompanyResponse company, string companyAddressDistrictName, string companyAddressCityName,
           UserResponse user, string userShippingAddress, string userShippingAddressDistrictName, string userShippingAddressCityName, string taxNumber,
           string productsRow, string orderPaymentTypeName, decimal grandTotal)
        {
            var document = OrderDocumentConstantsConstants.PreliminaryInformationForm.Document;

            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.CompanyLegalName, company.LegalName);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.CompanyAddress, company.Address);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.CompanyAddressDistrict, companyAddressDistrictName);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.CompanyAddressCity, companyAddressCityName);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.CompanyEmail, company.Email);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.CompanyPhoneNumber, company.PhoneNumber);

            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.UserFullName, $"{user.FirstName} {user.LastName}");
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.UserAddress, userShippingAddress);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.UserAddressDistrict, userShippingAddressDistrictName);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.UserAddressCity, userShippingAddressCityName);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.UserEmail, user.Email);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.UserPhoneNumber, user.PhoneNumber);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.UserTaxNumber, taxNumber);

            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.Products, productsRow);
            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.OrderPaymentType, orderPaymentTypeName);

            document = document.Replace(OrderDocumentConstantsConstants.PreliminaryInformationForm.Replacements.GrandTotal, grandTotal.ToString());

            return document;
        }

        #endregion

        #region CreateProductsRowAsync

        private async Task<string> CreateProductsRowAsync(List<ShoppingCartItemResponse> shoppingCartItems, decimal? shippingFee = null)
        {
            var productRows = new StringBuilder();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var product = await _mediator.Send(new GetProduct(shoppingCartItem.ProductId));

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                var productRowTemplate = @$"<tr>
                                                <th class=""padding: 8px; border: 1px solid #ddd; text-align: left;"">{product.Name}</th>
                                                <th class=""padding: 8px; border: 1px solid #ddd; text-align: left;"">{shoppingCartItem.Amount}</th>
                                                <th class=""padding: 8px; border: 1px solid #ddd; text-align: left;"">{shoppingCartItem.DiscountedPrice ?? shoppingCartItem.OriginalPrice}</th>
                                                <th class=""padding: 8px; border: 1px solid #ddd; text-align: left;"">{shoppingCartItem.DiscountedPrice ?? shoppingCartItem.OriginalPrice * shoppingCartItem.Amount}</th>
                                            </tr>";

                productRows.Append(productRowTemplate);
            }

            if (shippingFee.HasValue && shippingFee.Value > 0)
            {
                var shippingFeeRowTemplate = @$"<tr>
                                                    <th class=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Teslimat Bedeli</th>
                                                    <th class=""padding: 8px; border: 1px solid #ddd; text-align: left;"">1</th>
                                                    <th class=""padding: 8px; border: 1px solid #ddd; text-align: left;"">{shippingFee}</th>
                                                    <th class=""padding: 8px; border: 1px solid #ddd; text-align: left;"">{shippingFee}</th>
                                                </tr>";

                productRows.Append(shippingFeeRowTemplate);
            }

            return productRows.ToString();
        }

        #endregion

        #endregion
    }
}

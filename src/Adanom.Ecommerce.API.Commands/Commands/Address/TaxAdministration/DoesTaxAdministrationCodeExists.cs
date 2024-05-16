namespace Adanom.Ecommerce.API.Commands
{
    public class DoesTaxAdministrationCodeExists : IRequest<bool>
    {
        #region Ctor

        public DoesTaxAdministrationCodeExists(string code)
        {
            Code = code;
        }

        #endregion

        #region Properties

        public string Code { get; }

        #endregion
    }
}
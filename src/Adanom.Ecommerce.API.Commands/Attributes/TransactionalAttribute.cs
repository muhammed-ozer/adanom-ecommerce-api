namespace Adanom.Ecommerce.API.Commands
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TransactionalAttribute : Attribute
    {
    }
}

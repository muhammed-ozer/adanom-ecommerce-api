namespace Adanom.Ecommerce.API.Data.Models
{
    public enum ReturnRequestStatusType : byte
    {
        RECEIVED,
        PENDING,
        IN_PROGRESS,
        APPROVED,
        DISAPPROVED,
        CANCEL
    }
}

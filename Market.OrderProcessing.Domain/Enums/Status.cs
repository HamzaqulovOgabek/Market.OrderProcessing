namespace Market.OrderProcessing.Domain.Enums;

public enum Status
{
    #region Order
    PENDING = 0,
    PROCESSING = 1,
    SHIPPED = 2,
    DELIVERED = 3,
    CANCELED = 4,
    #endregion

    #region PaymentStatus
    COMPLETED = 5,
    FAILED = 6,
    #endregion

    #region CRUD
    CREATED = 7,
    UPDATED = 8,
    DELETED = 9
    #endregion

}

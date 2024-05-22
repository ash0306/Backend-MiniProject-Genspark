using System.ComponentModel;

namespace CoffeeStoreApplication.Models.Enum
{
    public enum OrderStatus
    {
        [Description("The order has been successfully placed")]Placed,
        [Description("The order is being prepared")]Preparing,
        [Description("The order is ready")]Ready,
        [Description("The order has been completed and ready to pickup")]Completed,
        [Description("The order has been cancelled")]Cancelled
    }
}

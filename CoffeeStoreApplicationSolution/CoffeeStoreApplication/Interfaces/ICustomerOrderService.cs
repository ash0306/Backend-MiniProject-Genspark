using CoffeeStoreApplication.Models.DTOs.CustomerOrder;

namespace CoffeeStoreApplication.Interfaces
{
    public interface ICustomerOrderService
    {
        public Task<IEnumerable<CustomerOrderReturnDTO>> GetCustomerOrderById(int customerId);

    }
}

using CoffeeStoreApplication.Models.Enum;

namespace CoffeeStoreApplication.Interfaces
{
    public interface IAuthRegisterService<T, K> where T : class where K : class
    {
        public Task<T> Register(K registerDTO, RoleType role);
    }
}

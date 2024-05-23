namespace CoffeeStoreApplication.Interfaces
{
    public interface IAuthLoginService<T, K> where T : class where K : class
    {
        public Task<T> Login(K loginDTO);
    }
}

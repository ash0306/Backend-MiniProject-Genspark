using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreApplication.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] PasswordHashKey { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int LoyaltyPoints { get; set; } = 0;

    }
}

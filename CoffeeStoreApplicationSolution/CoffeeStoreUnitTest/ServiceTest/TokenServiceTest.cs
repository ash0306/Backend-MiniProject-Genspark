using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class TokenServiceTest
    {
        ITokenService _tokenService;

        [SetUp]
        public void SetUp()
        {
            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);


            _tokenService = new TokenService(mockConfig.Object);
        }

        [Test, Order(1)]
        public void GenerateFacultyTokenSuccessTest()
        {
            var hmac = new HMACSHA512();
            var employee1 = new Employee()
            {
                Name = "Employee1",
                Email = "employee1@gmail.com",
                DateOfBirth = new DateTime(2000, 01, 01),
                Phone = "1234567899",
                Role = RoleType.Admin,
                Status = EmployeeStatus.Active,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("employee1"))
            };

            var token = _tokenService.GenerateEmployeeToken(employee1);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            Assert.IsNotNull(emailClaim);
            Assert.That(emailClaim.Value.ToString(), Is.EqualTo(employee1.Email));
        }

        [Test, Order(2)]
        public void GenerateCustomerTokenSuccessTest()
        {
            var hmac = new HMACSHA512();
            var customer1 = new Customer()
            {
                Name = "Customer1",
                Email = "Customer1@gmail.com",
                DateOfBirth = new DateTime(2000, 01, 01),
                Phone = "9876523418",
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("customer1"))
            };
            var token = _tokenService.GenerateCustomerToken(customer1);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            Assert.IsNotNull(emailClaim);
            Assert.That(emailClaim.Value.ToString(), Is.EqualTo(customer1.Email));
        }

        [Test, Order(3)]
        public void GenerateEmployeeTokenFailureTest()
        {
            var hmac = new HMACSHA512();
            var employee1 = new Employee()
            {
                Name = "Employee1",
                Email = "employee1@gmail.com",
                DateOfBirth = new DateTime(2000, 01, 01),
                Phone = "1234567899",
                Role = RoleType.Admin,
                Status = EmployeeStatus.Active,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("employee1"))
            };

            var token = _tokenService.GenerateEmployeeToken(employee1);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var claims = jwtToken.Claims;
            //for testing, making it as empty
            claims = Enumerable.Empty<Claim>();

            // Scenario where the email claim is missing
            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            Assert.IsNull(emailClaim);

        }

        [Test, Order(4)]
        public void GenerateCustomerTokenFailureTest()
        {
            var hmac = new HMACSHA512();
            var customer1 = new Customer()
            {
                Name = "Customer1",
                Email = "Customer1@gmail.com",
                DateOfBirth = new DateTime(2000, 01, 01),
                Phone = "9876523418",
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("customer1"))
            };

            var token = _tokenService.GenerateCustomerToken(customer1);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);



            var claims = jwtToken.Claims;
            //for testing, making it as empty
            claims = Enumerable.Empty<Claim>();

            // Scenario where the email claim is missing
            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            Assert.IsNull(emailClaim);

        }
    }
}

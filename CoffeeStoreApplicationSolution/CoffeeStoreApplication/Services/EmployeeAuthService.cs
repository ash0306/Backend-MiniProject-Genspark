using AutoMapper;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Employee;
using CoffeeStoreApplication.Models.Enum;
using System.Security.Cryptography;
using System.Text;

namespace CoffeeStoreApplication.Services
{
    public class EmployeeAuthService : IAuthRegisterService<EmployeeRegisterReturnDTO, EmployeeRegisterDTO>, IAuthLoginService<EmployeeLoginReturnDTO, EmployeeLoginDTO>
    {
        private readonly IRepository<int, Employee> _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public EmployeeAuthService(IRepository<int,Employee> repository, ITokenService tokenService, IMapper mapper)
        {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<EmployeeRegisterReturnDTO> Register(EmployeeRegisterDTO registerDTO, RoleType role)
        {
            Employee employee;

            try
            {
                var alreadyPresent = (await _repository.GetAll()).FirstOrDefault(e => e.Email == registerDTO.Email);
                if (alreadyPresent != null)
                {
                    throw new UserAlreadyExistsException($"User with this Email already exists");
                }

                employee = _mapper.Map<Employee>(registerDTO);
                HMACSHA512 hMACSHA = new HMACSHA512();

                employee.PasswordHashKey = hMACSHA.Key;
                employee.HashedPassword = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
                employee.Role = role;
                if(role == RoleType.Admin)
                {
                    employee.Status = EmployeeStatus.Active;
                }
                employee.Status = EmployeeStatus.Inactive;

                var newEmployee = await _repository.Add(employee);

                EmployeeRegisterReturnDTO returnDTO = _mapper.Map<EmployeeRegisterReturnDTO>(newEmployee);

                return returnDTO;
            }
            catch
            {
                throw new UnableToRegisterException($"Unable to register at the moment");
            }
        }

        public async Task<EmployeeLoginReturnDTO> Login(EmployeeLoginDTO loginDTO)
        {
            Employee employee = (await _repository.GetAll()).FirstOrDefault(c => c.Email == loginDTO.Email);
            if (employee == null)
            {
                throw new UnauthorizedUserException("Invalid email or password");
            }

            if(employee.Status == EmployeeStatus.Inactive)
            {
                throw new UnauthorizedUserException($"User not activated");
            }

            HMACSHA256 hMACSHA256 = new HMACSHA256(employee.PasswordHashKey);
            var encryptedPassword = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isCorrectPassword = ComparePassword(encryptedPassword, loginDTO.Password);

            if (isCorrectPassword)
            {
                EmployeeLoginReturnDTO returnDTO = new EmployeeLoginReturnDTO()
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Token = _tokenService.GenerateEmployeeToken(employee)
                };
                return returnDTO;
            }

            throw new UnauthorizedUserException("Invalid email or password");
        }

        private bool ComparePassword(byte[] encryptedPassword, string password)
        {
            for (int i = 0; i < encryptedPassword.Length; i++)
            {
                if (encryptedPassword[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

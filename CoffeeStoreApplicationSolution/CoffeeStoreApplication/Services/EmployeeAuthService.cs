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

        /// <summary>
        /// Registers a new employee.
        /// </summary>
        /// <param name="registerDTO">EmployeeRegisterDTO object containing registration details</param>
        /// <param name="role">RoleType of the employee</param>
        /// <returns>EmployeeRegisterReturnDTO object containing registered employee details</returns>
        /// <exception cref="UserAlreadyExistsException">If the user with the given email already exists</exception>
        /// <exception cref="UnableToRegisterException">If unable to register the employee</exception>
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

        /// <summary>
        /// Logs in an employee.
        /// </summary>
        /// <param name="loginDTO">EmployeeLoginDTO object containing login details</param>
        /// <returns>EmployeeLoginReturnDTO object containing employee details and token</returns>
        /// <exception cref="UnauthorizedUserException">If the login details are incorrect or the user is not activated</exception>
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

            HMACSHA512 hMACSHA512 = new HMACSHA512(employee.PasswordHashKey);
            var encryptedPassword = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isCorrectPassword = ComparePassword(encryptedPassword, employee.HashedPassword);

            if (isCorrectPassword)
            {
                EmployeeLoginReturnDTO returnDTO = new EmployeeLoginReturnDTO()
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Token = _tokenService.GenerateEmployeeToken(employee),
                    Role = employee.Role.ToString()
                };
                return returnDTO;
            }

            throw new UnauthorizedUserException("Invalid email or password");
        }

        /// <summary>
        /// Compares two password hashes.
        /// </summary>
        /// <param name="encryptedPassword">Byte array of the encrypted password</param>
        /// <param name="password">Byte array of the stored password</param>
        /// <returns>True if passwords match, otherwise false</returns>
        private bool ComparePassword(byte[] encryptedPassword, byte[] password)
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

using AutoMapper;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.ApiGateway.Models.Requests;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace SOC.IoT.ApiGateway.Services
{
    public interface IUserService
    {
        string Login(LoginRequest request);
        string Register(User request);
        List<UserDTO> GetAccounts(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly SOCIoTDbContext _context;
        //private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserService(SOCIoTDbContext context, /*AppSettings appSettings,*/ IMapper mapper)
        {
            _context = context;
            //_appSettings = appSettings;
            _mapper = mapper;
        }

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        public string Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetAccounts(Guid id)
        {
            throw new NotImplementedException();
        }

        public string Register(User request)
        {
            if (_context.Users.Any(x => x.Username == request.Username))
            {
                return "This user exists.";
            }
            User user;

            try
            {
                user = _mapper.Map<User>(request);
            }
            catch
            {
                return "Error!";
            }

            //hash password
            //user.Password = HashPassword(request.Password, out var salt);


            _context.Add(user);
            _context.SaveChanges();

            return "Success";
        }
    }
}

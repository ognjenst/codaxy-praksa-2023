﻿using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Exceptions;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.ApiGateway.Models.Responses;
using SOC.IoT.ApiGateway.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace SOC.IoT.ApiGateway.Services
{
    public interface IUserService
    {
        AuthResponse Login(LoginRequest request);
        string Register(User request);
    }

    public class UserService : IUserService
    {
        private readonly SOCIoTDbContext _context;
        private readonly JwtSecret _jwtSecret;
        private readonly IMapper _mapper;

        public UserService(SOCIoTDbContext context, IOptions<JwtSecret> jwtSecret, IMapper mapper)
        {
            _context = context;
            _jwtSecret = jwtSecret.Value;
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

        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        public AuthResponse Login(LoginRequest request)
        {
            var user = _context
                .Users
                .FirstOrDefault(x => x.Username == request.Username);

            if (user == null)
            {
                throw new AppException("This user doesnt exist!");
            }

            if (!VerifyPassword(request.Password, user.Password, Convert.FromHexString(user.Salt)))
            {
                throw new AppException("Wrong password!");
            }

            string jwtToken = GenerateJwtToken(user);

            var authResponse = new AuthResponse
            {
                Jwt = jwtToken,
                ExpirationTime = DateTime.UtcNow.AddMinutes(15)
            };

            return authResponse;
        }

        public string Register(User request)
        {
            if (_context.Users.Any(x => x.Username == request.Username))
            {
                throw new AppException("This user already exists!");
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
            user.Password = HashPassword(request.Password, out var salt);
            user.Salt = Convert.ToHexString(salt);

            _context.Add(user);
            _context.SaveChanges();

            return "Success";
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( new[] { new Claim("id", user.Id.ToString())} ) ,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    
    }
}
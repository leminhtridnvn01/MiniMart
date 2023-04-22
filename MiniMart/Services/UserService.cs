﻿using MiniMart.Domain.DTOs.User;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MiniMart.API.Services
{
    public partial class UserService : BaseService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public UserService(IConfiguration configuration
            , IUnitOfWork unitOfWork
            , IUserRepository userRepository
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<RegisterResponse> CreateUser(RegisterRequest registerRequest)
        {
            if (await _userRepository.GetAsync(s => s.Email == registerRequest.Email) != null)
            {
                throw new Exception("Email is already existed");
            }
            using var hmac = new HMACSHA512();
            var user = new User()
            {
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerRequest.Password)),
                PasswordSalt = hmac.Key
            };
            await _userRepository.InsertAsync(user);

            await _unitOfWork.SaveChangeAsync();

            return new RegisterResponse()
            {
                Token = CreateToken(user),
                Username = user.Email
            };

        }
    }
}

﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Helpers;
using SOA_CA2_E_Commerce.Models;
using SOA_CA2_E_Commerce.Enums;


namespace SOA_CA2_E_Commerce.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jwtSecret;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _jwtSecret = configuration["JwtSettings:Secret"];
        }

        public async Task<string> Register(RegisterDTO registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var salt = PasswordHelper.GenerateSalt();
            var passwordHash = PasswordHelper.HashPassword(registerDto.Password, salt);

      
            var apiKey = ApiKeyHelper.GenerateApiKey();

            var refreshToken = Guid.NewGuid().ToString();

            var user = new User
            {
                First_Name = registerDto.First_Name,
                Last_Name = registerDto.Last_Name,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Salt = salt,
                Address = registerDto.Address,
                Role = registerDto.Role,
                ApiKey = apiKey, 
                ApiKeyExpiration = DateTime.UtcNow.AddMonths(6),
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return apiKey; 
        }

        public async Task<(string JwtToken, string RefreshToken, string ApiKey, int UserId, UserRole? Role)> Login(LoginDTO loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash, user.Salt))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (string.IsNullOrEmpty(_jwtSecret))
            {
                throw new InvalidOperationException("JWT secret key is missing in the configuration.");
            }

            var jwtToken = JwtHelper.GenerateToken(
                user.Email,
                user.Role.ToString(),
                int.Parse(_configuration["JwtSettings:ExpiryMinutes"]),
                _jwtSecret
            );

          
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7); 
            await _context.SaveChangesAsync();

      
            return (jwtToken, refreshToken, user.ApiKey, user.User_Id, user.Role);
        }

        public async Task<bool> ValidateApiKey(string apiKey)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.ApiKey == apiKey);
            return user != null && ApiKeyHelper.ValidateApiKey(apiKey, user.ApiKeyExpiration);
        }

        public async Task<string> RefreshToken(string oldRefreshToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == oldRefreshToken);
            if (user == null || user.RefreshTokenExpiration <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");

            var newJwtToken = JwtHelper.GenerateToken(
                user.Email,
                user.Role.ToString(),
                int.Parse(_configuration["JwtSettings:ExpiryMinutes"]),
                _jwtSecret
            );
            var newRefreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7); 
            await _context.SaveChangesAsync();

            return newJwtToken;
        }

        public async Task RevokeRefreshToken(string refreshToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null)
                throw new KeyNotFoundException("Invalid refresh token.");

            user.RefreshToken = null; 
            user.RefreshTokenExpiration = null;
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetApiKeyForUser(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return user.ApiKey; 
        }
    }
}

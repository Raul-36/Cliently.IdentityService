using Application.Common;
using Application.Users.DTOs.Request;
using Application.Users.Services.Base;
using AutoMapper;
using Core.Users.Entities.Base;
using Infrastructure.Roles.Entities;
using Infrastructure.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Users.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result<IUser>> CreateUserAsync(CreateUserRequest userRequest)
        {
            var user = mapper.Map<ApplicationUser>(userRequest);
            user.UserName = userRequest.Email;
            var result = await userManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<IUser>.Failure(errors);
            }

            return Result<IUser>.Success(user);
        }

        public async Task<Result<bool>> DeleteUserByIdAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return Result<bool>.Failure("User not found.");

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<bool>.Failure(errors);
            }

            return Result<bool>.Success(true);
        }

        public async Task<Result<IEnumerable<IUser>>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            return Result<IEnumerable<IUser>>.Success(users);
        }

        public async Task<Result<IUser>> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<IUser>.Failure("User not found.");

            return Result<IUser>.Success(user);
        }

        public async Task<Result<IUser>> GetUserByIdAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return Result<IUser>.Failure("User not found.");
                
            return Result<IUser>.Success(user);
        }

        public async Task<Result<IUser>> UpdateUserAsync(IUser user)
        {
            var existingUser = await userManager.FindByIdAsync(user.Id.ToString());
            if (existingUser == null)
                return Result<IUser>.Failure("User not found.");

            existingUser.Email = user.Email;

            var result = await userManager.UpdateAsync(existingUser);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<IUser>.Failure(errors);
            }

            return Result<IUser>.Success(existingUser);
        }
    }
}
using Application.Common.Exceptions;
using Application.Users.DTOs.Request;
using Application.Users.Exceptions;
using Application.Users.Services.Base;
using AutoMapper;
using Core.Users.Entities.Base;
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

        public async Task<IUser> CreateUserAsync(CreateUserRequest userRequest)
        {
            var user = mapper.Map<ApplicationUser>(userRequest);
            user.UserName = userRequest.Email;
            var result = await userManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors);
            }

            return user;
        }

        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new UserNotFoundException(id);

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors);
            }

            return true;
        }

        public async Task<IEnumerable<IUser>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            return users;
        }

        public async Task<IUser> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new UserNotFoundException(email);

            return user;
        }

        public async Task<IUser> GetUserByIdAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new UserNotFoundException(id);
                
            return user;
        }

        public async Task<IUser> UpdateUserAsync(IUser user)
        {
            var existingUser = await userManager.FindByIdAsync(user.Id.ToString());
            if (existingUser == null)
                throw new UserNotFoundException(user.Id);

            existingUser.Email = user.Email;
            existingUser.UserName = user.Email;

            var result = await userManager.UpdateAsync(existingUser);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors);
            }

            return existingUser;
        }
    }
}
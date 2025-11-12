using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.DTOs.Request;
using Core.Users.Entities;
using Core.Users.Entities.Base;

namespace Application.Users.Services.Base
{
    public interface IUserService
    {
        Task<IUser> GetUserByIdAsync(Guid id);
        Task<IUser> GetUserByEmailAsync(string id);
        Task<IEnumerable<IUser>> GetAllUsersAsync();
        Task<IUser> CreateUserAsync(CreateUserRequest user);
        Task<IUser> UpdateUserAsync(IUser user);
        Task<bool> DeleteUserByIdAsync(Guid id);
    }
}
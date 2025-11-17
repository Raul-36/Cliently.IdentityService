using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Users.DTOs.Request;
using Core.Users.Entities;
using Core.Users.Entities.Base;

namespace Application.Users.Services.Base
{
    public interface IUserService
    {
        Task<Result<IUser>> GetUserByIdAsync(Guid id);
        Task<Result<IUser>> GetUserByEmailAsync(string id);
        Task<Result<IEnumerable<IUser>>> GetAllUsersAsync();
        Task<Result<IUser>> CreateUserAsync(CreateUserRequest user);
        Task<Result<IUser>> UpdateUserAsync(IUser user);
        Task<Result<bool>> DeleteUserByIdAsync(Guid id);
    }
}
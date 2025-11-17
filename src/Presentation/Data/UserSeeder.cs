using Application.Identity.Commands;
using Infrastructure.Users.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Presentation.Options;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Presentation.Data
{
    public static class FirstUserSeeder
    {
        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<IOptions<FirstUsersOptions>>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var logger = serviceProvider.GetRequiredService<ILogger<FirstUsersOptions>>();

            if (options.Value.Users == null)
            {
                return;
            }

            foreach (var userCommand in options.Value.Users)
            {
                var result = await mediator.Send(userCommand);
                if (result.IsSuccess)
                {
                    logger.LogInformation($"User {userCommand.CreateUser.Email} created successfully.");
                }
                else
                {
                    logger.LogError($"Failed to create user {userCommand.CreateUser.Email}. Errors: {string.Join(", ", result.Errors ?? new List<string>{"Unknown errors at creating user."})}");
                }
            }
        
        }
    }
}
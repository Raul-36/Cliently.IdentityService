using Application.Identity.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Presentation.Options;
using System.Threading.Tasks;
using System;
using Application.Common.Exceptions;

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
                try
                {
                    await mediator.Send(userCommand);
                    logger.LogInformation($"User {userCommand.CreateUser.Email} created successfully.");
                }
                catch (BadRequestException ex)
                {
                    logger.LogError($"Failed to create user {userCommand.CreateUser.Email}. Errors: {string.Join(", ", ex.Errors)}");
                }
                catch (Exception ex)
                {
                    logger.LogError($"Failed to create user {userCommand.CreateUser.Email}. Error: {ex.Message}");
                }
            }
        }
    }
}
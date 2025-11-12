using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Tokens.DTOs.Request;

namespace Application.Tokens.Services.Base
{
    public interface ITokenGeneratorService
    {
        public string GenerateJWTToken(GenerateJWTTokenRequest request);
    }
}
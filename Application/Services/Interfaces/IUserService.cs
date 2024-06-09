﻿using Domain.Entities;
using Domain.Models.Auth;
using Mommilk88.Data;
using System.Security.Claims;
using static Domain.Models.Auth.Login;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response<LoginResult>> Login(LoginRequest request);
        LoginResult GenerateToken(Customer user, List<Claim> claims, DateTime now);
    }
}

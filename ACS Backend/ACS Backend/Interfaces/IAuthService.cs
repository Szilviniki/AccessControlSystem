﻿using ACS_Backend.Model;

namespace ACS_Backend.Interfaces;

public interface IAuthService
{
    public LoginResponseModel Login(LoginModel login);
}
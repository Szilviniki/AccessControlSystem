﻿namespace ACS_Backend.Model;

public class LoginResponseModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}
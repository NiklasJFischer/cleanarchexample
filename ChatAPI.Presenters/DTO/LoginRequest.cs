﻿namespace ChatAPI.Presenters.DTO;

public class LoginRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

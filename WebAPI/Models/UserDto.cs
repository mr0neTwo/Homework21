﻿namespace WebAPI.Models;

public sealed class UserDto
{
	public int Id { get; set; }
	public string? UserName { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Password { get; set; }
	public int RoleId { get; set; }
}

﻿using CompanyAPI.Data.Entities;
using CompanyAPI.Data;
using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly ContextDAL _context;

    public UserService(ContextDAL context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(u => new UserDto(u));
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? null : new UserDto(user);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        var user = new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            Role = userDto.Role,
            IsActive = userDto.IsActive
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new UserDto(user);
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserDto userDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        user.Username = userDto.Username;
        user.Email = userDto.Email;
        user.Role = userDto.Role;
        user.IsActive = userDto.IsActive;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}

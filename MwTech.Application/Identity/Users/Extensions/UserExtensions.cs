using MwTech.Application.Identity.Users.Queries.GetUsersBasics;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Identity.Users.Extensions;
public static class UserExtensions
{

    public static UserBasicsDto ToUserBasicsDto(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new UserBasicsDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Name = !string.IsNullOrWhiteSpace(user.FirstName) ||
                   !string.IsNullOrWhiteSpace(user.LastName)
                   ? $"{user.FirstName} {user.LastName}" : "-",
            IsDeleted = user.IsDeleted
        };
    }

    /*
    public static ClientDto ToUserDto(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new ClientDto
        {
            Id = user.Id,
            City = user.Address?.City,
            Country = user.Address?.Country,
            Street = user.Address?.Street,
            StreetNumber = user.Address?.StreetNumber,
            ZipCode = user.Address?.ZipCode,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NipNumber = user.Client?.NipNumber,
            IsPrivateAccount = user.Client?.IsPrivateAccount ?? true
        };
    }

    public static EditClientCommand ToEditClientCommand(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new EditClientCommand
        {
            Id = user.Id,
            City = user.Address?.City,
            Country = user.Address?.Country,
            Street = user.Address?.Street,
            StreetNumber = user.Address?.StreetNumber,
            ZipCode = user.Address?.ZipCode,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NipNumber = user.Client?.NipNumber,
            IsPrivateAccount = user.Client?.IsPrivateAccount ?? true
        };
    }

    

    public static EditAdminClientCommand ToEditAdminClientCommand(
        this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new EditAdminClientCommand
        {
            Id = user.Id,
            City = user.Address?.City,
            Country = user.Address?.Country,
            Street = user.Address?.Street,
            StreetNumber = user.Address?.StreetNumber,
            ZipCode = user.Address?.ZipCode,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NipNumber = user.Client?.NipNumber,
            IsPrivateAccount = user.Client?.IsPrivateAccount ?? true
        };
    }

    public static EmployeeBasicsDto ToEmployeeBasicsDto(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new EmployeeBasicsDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = !string.IsNullOrWhiteSpace(user.FirstName) ||
                   !string.IsNullOrWhiteSpace(user.LastName)
                   ? $"{user.FirstName} {user.LastName}" : "-",
            IsDeleted = user.IsDeleted
        };
    }

    public static EditEmployeeCommand ToEmployee(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new EditEmployeeCommand
        {
            City = user.Address?.City,
            Country = user.Address?.Country,
            Street = user.Address?.Street,
            StreetNumber = user.Address?.StreetNumber,
            ZipCode = user.Address?.ZipCode,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            DateOfDismissal = user.Employee?.DateOfDismissal,
            DateOfEmployment = user.Employee?.DateOfEmployment ?? DateTime.MinValue,
            ImageUrl = user.Employee?.ImageUrl,
            PositionId = (int?)user.Employee?.Position ?? (int)Position.Receptionits,
            Salary = user.Employee?.Salary ?? 0,
            WebsiteRaw = user.Employee?.WebsiteRaw,
            WebsiteUrl = user.Employee?.WebsiteUrl
        };
    }

    */
}

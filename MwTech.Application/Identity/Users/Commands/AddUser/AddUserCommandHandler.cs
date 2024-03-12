using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Dictionaries;
using MwTech.Domain.Entities;

namespace MwTech.Application.Identity.Users.Commands.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IUserManagerService _userManagerService;

    public AddUserCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        , IUserManagerService userManagerService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _userManagerService = userManagerService;
    }

    public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {

        var password = request.Password ?? "MwTech27$";

        var userId = await _userManagerService.CreateAsync(request.UserName, request.Email, password, RolesDict.Pracownik);

        var AddedUser = await _context.Users
             .FirstOrDefaultAsync(x => x.Id == userId);

        //.Include(x => x.Client)
        //.Include(x => x.Address)


        AddedUser.FirstName = request.FirstName;
        AddedUser.LastName = request.LastName;
        AddedUser.Possition = request.Possition;
        AddedUser.PhoneNumber = request.PhoneNumber;
        AddedUser.ReferenceNumber = request.ReferenceNumber;
        AddedUser.AdminRights = request.AdminRights;
        AddedUser.SuperAdminRights = request.SuperAdminRights;

        AddedUser.Rfid = request.Rfid;
        AddedUser.EmailConfirmed = true;
        AddedUser.RegisterDateTime = _dateTimeService.Now;

        /*
        AddedUser.Client = new Domain.Entities.Client();
        AddedUser.Client.IsPrivateAccount = request.IsPrivateAccount;
        AddedUser.Client.NipNumber = request.NipNumber;
        AddedUser.Client.UserId = userId;

        AddedUser.Address = new Domain.Entities.Address();
        AddedUser.Address.Country = request.Country;
        AddedUser.Address.City = request.City;
        AddedUser.Address.Street = request.Street;
        AddedUser.Address.ZipCode = request.ZipCode;
        AddedUser.Address.StreetNumber = request.StreetNumber;
        AddedUser.Address.UserId = userId;
        */

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}

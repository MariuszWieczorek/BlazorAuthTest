namespace MwTech.Application.Common.Interfaces;

public interface IAppSettingsService
{
    Task<string> Get(string key);
    Task Update(IApplicationDbContext context);
}

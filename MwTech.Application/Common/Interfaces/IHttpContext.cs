

namespace MwTech.Application.Common.Interfaces;
public interface IHttpContext
{
    string AppBaseUrl { get; }
    string IpAddress { get; }
}

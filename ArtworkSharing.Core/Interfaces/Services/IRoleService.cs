using ArtworkSharing.Core.Domain.Entities;

namespace ArtworkSharing.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<Role> GetRole(string name);
    }
}

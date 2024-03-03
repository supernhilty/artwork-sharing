using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces;
using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.DAL.Extensions;

namespace ArtworkSharing.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _uow;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public async Task<Role> GetRole(string name)
        {
            return await _uow.RoleRepository.FirstOrDefaultAsync(x => x.Name.ToLower() == (name + "").ToLower());
        }
    }
}

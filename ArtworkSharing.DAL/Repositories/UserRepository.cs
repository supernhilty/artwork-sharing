using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces.Repositories;
using ArtworkSharing.Core.ViewModels.Users;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharing.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public void UpdateUser(User u)
        {
            _context.Update(u);
        }

        public void UpdateUser(User u)
        {
            throw new NotImplementedException();
        }
    }
}

using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharing.DAL.Repositories
{
    public class ArtworkRepository : Repository<Artwork>, IArtworkRepository
    {
        private readonly DbContext _context;

        public ArtworkRepository(DbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public void UpdateArtwork(Artwork artwork)
        {
            _context.Update(artwork);
        }
    }
}

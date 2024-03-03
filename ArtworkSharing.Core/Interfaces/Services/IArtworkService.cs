using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.ViewModels.Artworks;

namespace ArtworkSharing.Core.Interfaces.Services
{
    public interface IArtworkService
    {
        Task<IList<Artwork>> GetAll();
        Task<ArtworkViewModel> GetArtwork(Guid artworkId);
        Task<Artwork> GetOne(Guid artworkId);
        Task Update(Artwork uam);
        Task<ArtworkViewModel> Update(UpdateArtworkModel uam, List<Category>? categories = null!, List<MediaContent>? mediaContents = null!);
        Task<ArtworkViewModel> Add(CreateArtworkModel cam);
        Task Add(Artwork artwork);
        Task Delete(Guid artworkId);
    }
}

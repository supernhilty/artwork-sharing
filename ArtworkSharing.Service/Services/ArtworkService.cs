using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces;
using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Core.ViewModels.Artworks;
using ArtworkSharing.Service.AutoMappings;

namespace ArtworkSharing.Service.Services
{
    public class ArtworkService : IArtworkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtworkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ArtworkViewModel> Add(CreateArtworkModel cam)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var repos = _unitOfWork.ArtworkRepository;

                var artwork = AutoMapperConfiguration.Mapper.Map<Artwork>(cam);

                await repos.AddAsync(artwork);

                await _unitOfWork.CommitTransaction();
                return await GetArtwork(artwork.Id);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public Task Add(Artwork artwork)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid artworkId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var repos = _unitOfWork.ArtworkRepository;
                var artwork = await repos.GetAsync(a => a.Id == artworkId);
                if (artwork == null)
                    throw new KeyNotFoundException();

                await repos.DeleteAsync(artwork);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IList<Artwork>> GetAll()
        {
            return await _unitOfWork.ArtworkRepository.GetAllAsync();
        }

        public async Task<Artwork> GetOne(Guid artworkId) => await _unitOfWork.ArtworkRepository.FindAsync(artworkId);


        public async Task<ArtworkViewModel> GetArtwork(Guid artworkId) => AutoMapperConfiguration.Mapper.Map<ArtworkViewModel>(await _unitOfWork.ArtworkRepository.FindAsync(artworkId));


        public async Task<ArtworkViewModel> Update(UpdateArtworkModel uam, List<Category>? categories = null!, List<MediaContent>? mediaContents = null!)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var repos = _unitOfWork.ArtworkRepository;
                var a = await repos.FindAsync(uam.Id);
                if (a == null)
                    throw new KeyNotFoundException();
                a.Name = uam.Name ?? a.Name;
                a.Price = uam.Price;
                a.Description = uam.Description ?? a.Description;
                a.Status = uam.Status;

                if (categories != null && categories.Count > 0)
                {
                    a.Categories!.Clear();
                    a.Categories = categories;
                }

                if (mediaContents != null && mediaContents.Count > 0)
                {
                    a.MediaContents.Clear();
                    a.MediaContents = mediaContents;
                }

                repos.UpdateArtwork(a);
                await _unitOfWork.CommitTransaction();
                return await GetArtwork(uam.Id);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public Task Update(Artwork uam)
        {
            throw new NotImplementedException();
        }

        Task<Artwork> IArtworkService.GetOne(Guid artworkId)
        {
            throw new NotImplementedException();
        }
    }
}

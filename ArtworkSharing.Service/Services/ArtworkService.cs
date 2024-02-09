﻿using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces;
using ArtworkSharing.Core.Interfaces.Services;

namespace ArtworkSharing.Service.Services
{
    public class ArtworkService : IArtworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArtworkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(Artwork artwork)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var repos = _unitOfWork.ArtworkRepository;
                await repos.AddAsync(artwork);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
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

        public async Task<Artwork> GetOne(Guid artworkId)
        {
            return await _unitOfWork.ArtworkRepository.FindAsync(artworkId);
        }

        public async Task Update(Artwork artwork)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var repos = _unitOfWork.ArtistRepository;
                var a = await repos.FindAsync(artwork.Id);
                if (a == null)
                    throw new KeyNotFoundException();

                //a.Name = a.Name;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
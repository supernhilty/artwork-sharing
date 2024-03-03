using System;
using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces;
using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Core.ViewModels.Users;
using ArtworkSharing.DAL.Extensions;
using ArtworkSharing.Service.AutoMappings;

namespace ArtworkSharing.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserViewModel> CreateNewUser(CreateUserModel user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var repo = _unitOfWork.UserRepository;
                var u = AutoMapperConfiguration.Mapper.Map<User>(user);
                u.Id = Guid.NewGuid();
                u.Status = true;
                await repo.AddAsync(u);
                await _unitOfWork.CommitTransaction();
                return await GetUser(u.Id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                throw new Exception();
            }
        }

        public async Task DeleteUser(Guid userId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var repo = _unitOfWork.UserRepository;
                var user = await repo.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new KeyNotFoundException();
                }
                else
                {
                    user.Status = false;
                    _unitOfWork.UserRepository.UpdateUser(user);
                    await _unitOfWork.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                throw new Exception();
            }
        }

        public async Task<User> GetOne(Guid id)
        {
            return await _unitOfWork.UserRepository.FirstOrDefaultAsync(u => u.Id == id && u.Status);
        }

        public async Task<UserViewModel> GetUser(Guid userId)
            => AutoMapperConfiguration.Mapper.Map<UserViewModel>(await _unitOfWork.UserRepository.FirstOrDefaultAsync(u => u.Id == userId && u.Status));

        public async Task<List<User>> GetUsers()
        {
            return (await _unitOfWork.UserRepository.GetAllAsync(x => x.Status)).ToList();
        }

        public async Task<UserViewModel> UpdateUser(Guid id, UpdateUserModel um)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var repo = _unitOfWork.UserRepository;
                var u = await repo.FirstOrDefaultAsync(u => u.Id == id);
                if (u == null)
                {
                    throw new KeyNotFoundException();
                }
                u.Name = um.Name ?? u.Name;
                u.Password = um.Password ?? u.Password;
                u.Email = um.Email ?? u.Email;
                u.Phone = um.Phone ?? u.Phone;
                _unitOfWork.UserRepository.UpdateUser(u);

                await _unitOfWork.CommitTransaction();
                return await GetUser(id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}


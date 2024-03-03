using System;
using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.ViewModels.Users;

namespace ArtworkSharing.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<UserViewModel> GetUser(Guid id);
        Task<User> GetOne(Guid id);
        Task<UserViewModel> CreateNewUser(CreateUserModel user);
        Task<UserViewModel> UpdateUser(Guid id, UpdateUserModel user);
        Task DeleteUser(Guid userId);
    }
}


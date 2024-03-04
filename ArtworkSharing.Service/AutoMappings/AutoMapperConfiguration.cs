﻿using ArtworkSharing.Core.Domain.Entities;

using ArtworkSharing.Core.ViewModels.Artworks;
using ArtworkSharing.Core.ViewModels.Packages;

using ArtworkSharing.Core.ViewModels.ArtworkRequest;
﻿using ArtworkSharing.Core.Domain.Dtos.UserDtos;
using ArtworkSharing.Core.Domain.Entities;

using ArtworkSharing.Core.ViewModels.RefundRequests;
using ArtworkSharing.Core.ViewModels.Transactions;
using ArtworkSharing.Core.ViewModels.User;
using ArtworkSharing.Core.ViewModels.Artists;
using ArtworkSharing.Core.ViewModels.Artworks;
using ArtworkSharing.Core.ViewModels.Categories;
using ArtworkSharing.Core.ViewModels.Comments;
using ArtworkSharing.Core.ViewModels.Likes;
using ArtworkSharing.Core.ViewModels.MediaContents;
using ArtworkSharing.Core.ViewModels.RefundRequests;
using ArtworkSharing.Core.ViewModels.Transactions;
using ArtworkSharing.Core.ViewModels.Users;
using AutoMapper;

namespace ArtworkSharing.Service.AutoMappings
{
    public class AutoMapperConfiguration
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cf =>
            {
                cf.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod!.IsAssembly;
                cf.AddProfile<MapperHandler>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MapperHandler : Profile
    {
        public MapperHandler()
        {
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
            CreateMap<Transaction, UpdateTransactionModel>().ReverseMap();
            CreateMap<RefundRequest, RefundRequestViewModel>().ReverseMap();
            CreateMap<RefundRequest, UpdateRefundRequestModel>().ReverseMap();

            CreateMap<Package, PackageViewModel>().ReverseMap();
            CreateMap<Artwork,ArtworkViewModel>().ReverseMap();

            CreateMap<RefundRequest, CreateRefundRequestModel>().ReverseMap();
            CreateMap<ArtworkService, CreateArtworkRequestModel>().ReverseMap();
            CreateMap<ArtworkService, UpdateArtworkRequestModel>().ReverseMap();
            CreateMap<ArtworkService, ArtworkRequestViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, CreateUserViewModel>().ReverseMap();
            CreateMap<User, UserToLoginDto>().ReverseMap();
            CreateMap<User, UserToRegisterDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();


            CreateMap<Artwork, ArtworkViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Like, LikeViewModel>().ReverseMap();
            CreateMap<Artist, ArtistViewModel>().ReverseMap();
            CreateMap<MediaContent, MediaContentViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UpdateUserModel>().ReverseMap();
            CreateMap<User, CreateUserModel>().ReverseMap();
        }
    }
}

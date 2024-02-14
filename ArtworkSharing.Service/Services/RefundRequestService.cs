﻿using ArtworkSharing.Core.Interfaces;
using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Core.ViewModels.RefundRequests;
using ArtworkSharing.DAL.Extensions;
using ArtworkSharing.Service.AutoMappings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharing.Service.Services
{
    public class RefundRequestService : IRefundRequestService
    {
        private readonly IUnitOfWork _uow;

        public RefundRequestService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public async Task<bool> DeleteRefundRequest(Guid id)
        {
            var refundRequest = await _uow.RefundRequestRepository.FirstOrDefaultAsync(_ => _.Id == id);

            if (refundRequest == null) throw new ArgumentNullException(nameof(refundRequest));

            await _uow.RefundRequestRepository.DeleteAsync(refundRequest);

            var rs = await _uow.SaveChangesAsync();

            return rs > 0;
        }

        public async Task<List<RefundRequestViewModel>> GetAll()
            => AutoMapperConfiguration.Mapper.Map<List<RefundRequestViewModel>>(await (_uow.RefundRequestRepository.GetAll().AsQueryable()).ToListAsync());


        public async Task<RefundRequestViewModel> GetRefundRequest(Guid id)
            => AutoMapperConfiguration.Mapper.Map<RefundRequestViewModel>(await _uow.RefundRequestRepository.FirstOrDefaultAsync(_ => _.Id == id));

        public async Task<RefundRequestViewModel> UpdateRefundRequest(Guid id, UpdateRefundRequestModel urm)
        {
            var refundRequest = await _uow.RefundRequestRepository.FirstOrDefaultAsync(_ => _.Id == id);
            if (refundRequest == null) return null!;

            refundRequest.Description = urm.Description ?? refundRequest.Description;
            refundRequest.Reason = urm.Reason ?? refundRequest.Reason;

            // Add whatever you need

            _uow.RefundRequestRepository.UpdateRefundRequest(refundRequest);

            await _uow.SaveChangesAsync();
            return await GetRefundRequest(id);
        }
    }
}
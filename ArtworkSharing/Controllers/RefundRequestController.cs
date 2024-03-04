using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Core.ViewModels.RefundRequests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Core.ViewModels.RefundRequests;
using ArtworkSharing.Service.AutoMappings;
using Microsoft.AspNetCore.Mvc;
namespace ArtworkSharing.Controllers;

namespace ArtworkSharing.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefundRequestController : ControllerBase
    {
        private readonly IRefundRequestService _refundRequestService;

        public RefundRequestController(IRefundRequestService refundRequestService)
        {
            _refundRequestService = refundRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RefundRequestViewModel>>> GetAllRefundRequests()
        {
            var refundRequests = await _refundRequestService.GetAll();
            return Ok(refundRequests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RefundRequestViewModel>> GetRefundRequest(Guid id)
        {
            var refundRequest = await _refundRequestService.GetRefundRequest(id);
            if (refundRequest == null)
            {
                return NotFound();
            }
            return Ok(refundRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRefundRequest(Guid id, UpdateRefundRequestModel refundRequestInput)
        {
            try
            {
                await _refundRequestService.UpdateRefundRequest(id, refundRequestInput);
                return Ok(refundRequestInput);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRefundRequest(RefundRequest refundRequestInput)
        //{
        //    try
        //    {
        //        await _refundRequestService
        //        return CreatedAtAction(nameof(GetRefundRequest), new { id = refundRequestInput.Id }, refundRequestInput);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500); // Internal Server Error
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefundRequest(Guid id)
        {
            try
            {
                await _refundRequestService.DeleteRefundRequest(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

//[Nhi] March 4 - fix conflict
//Quang'sc ode

[ApiController]
[Route("api/refundrequest")]
public class RefundRequestController : Controller
{
    private IRefundRequestService _requestService;
    public RefundRequestController(IRefundRequestService refundRequestService)
    {
        _requestService = refundRequestService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllRefunds()
    {
        try
        {
            var refundList = await _requestService.GetAll();
            return Ok(refundList);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("{refundId}")]
    public async Task<ActionResult> GetRefund(Guid refundId)
    {
        try
        {
            var refund = await _requestService.GetRefundRequest(refundId);
            return Ok(refund);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost("createRefund")]
    public async Task<ActionResult> CreateRefund(CreateRefundRequestModel crm)
    {
        try
        {
            var createRefund = AutoMapperConfiguration.Mapper.Map<RefundRequest>(crm);
             _requestService.CreateRefundRequest(createRefund);
            return Ok(createRefund);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    
    
}


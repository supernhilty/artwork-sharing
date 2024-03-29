using ArtworkSharing.Core.Domain.Enums;
using ArtworkSharing.Core.ViewModels.MediaContent;
using Microsoft.AspNetCore.Http;

namespace ArtworkSharing.Core.ViewModels.ArtworkRequest;

public class UpdateArtworkRequestModel
{
    public Guid AudienceId { get; set; }
    public Guid ArtistId { get; set; }
    public Guid TransactionId { get; set; }
    public string? Description { get; set; }
    public DateTime RequestedDate { get; set; }
    public float RequestedPrice { get; set; }
    public float RequestedDeposit { get; set; }
    public DateTime RequestedDeadlineDate { get; set; }
    public ArtworkServiceStatus Status { get; set; }
    public ICollection<MediaContentViewModel>? ArtworkProduct { get; set; }
    public List<IFormFile> MediaContents { get; set; } = null!;
}
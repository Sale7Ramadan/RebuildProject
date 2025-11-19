using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class Report
{
    public int ReportId { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public int CityId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? EstimatedCost { get; set; }

    public string? Status { get; set; }
    public double? Latitude { get; set; }   
    public double? Longitude { get; set; } 
    public DateTime? CreatedAt { get; set; }
    public int LikesCount { get; set; }
    public virtual Category Category { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<DonationCase> DonationCases { get; set; } = new List<DonationCase>();

    public virtual ICollection<ReportImage> ReportImages { get; set; } = new List<ReportImage>();

    public virtual User User { get; set; } = null!;
}

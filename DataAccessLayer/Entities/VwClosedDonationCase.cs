using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class VwClosedDonationCase
{
    public int DonationCaseId { get; set; }

    public string ReportTitle { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public decimal GoalAmount { get; set; }

    public decimal? CollectedAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? ClosedAt { get; set; }
}

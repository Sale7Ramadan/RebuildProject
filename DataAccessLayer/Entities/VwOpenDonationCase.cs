using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class VwOpenDonationCase
{
    public int DonationCaseId { get; set; }

    public int ReportId { get; set; }

    public string ReportTitle { get; set; } = null!;

    public string? Description { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public decimal GoalAmount { get; set; }

    public decimal? CollectedAmount { get; set; }

    public decimal? RemainingAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string ReporterName { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class VwDonationsByCase
{
    public int DonationsId { get; set; }

    public int DonationCaseId { get; set; }

    public string ReportTitle { get; set; } = null!;

    public string? Donor { get; set; }

    public decimal Amount { get; set; }

    public DateTime? DonatedAt { get; set; }

    public decimal? CollectedAmount { get; set; }

    public decimal GoalAmount { get; set; }
}
